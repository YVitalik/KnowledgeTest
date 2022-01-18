using AutoMapper;
using BLL.CustomExceptions;
using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BLL.Services
{
    public class TestService : ITestService
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _repository;
        private readonly IUserTestService _userTestService;
        public TestService(ITestRepository repository, IMapper mapper, IUserTestService userTestService)
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile<AutomapperProfile>()
            );
            mapper = new Mapper(configuration);
            _mapper = mapper;
            _repository = repository;
            _userTestService = userTestService;
        }

        /// <summary>
        /// Return all tests from repository, if repository is empty throws exception TestRepositoryIsEmptyException
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TestRepositoryIsEmptyException"></exception>
        public async Task<IEnumerable<ReadTestDto>> GetAllTests()
        {
            var result = await _repository.GetAllAsync();

            if (result.Count() == 0)
            {
                throw new TestRepositoryIsEmptyException("No tests have not been created!");
            }
            else
            {
                return _mapper.Map<IEnumerable<ReadTestDto>>(result);
            }
        }


        /// <summary>
        /// Return specific test, if such test wasn't found throws an exception
        /// </summary>
        /// <param name="testToFind"></param>
        /// <returns></returns>
        /// <exception cref="NoTestsFoundException"></exception>
        public async Task<IEnumerable<ReadTestDto>> FindTestAsync(string testToFind)
        {
            var result = new List<ReadTestDto>();
            var testList = await _repository.GetAllAsync();
            
            foreach (var test in testList)
            {
                if (test.TestName.ToUpper().Contains(testToFind.ToUpper()))
                {
                    var toAdd = _mapper.Map<ReadTestDto>(test);
                    result.Add(toAdd);
                }
            }

            if (result.Count() == 0)
            {
                throw new NoTestsFoundException("For your query no tests were found!");
            }
            else
            {
                return result;
            }
        }


        /// <summary>
        /// Return list of questions for specific test, is list is empty, throws an exception NoQuestionsInTestException
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        /// <exception cref="NoQuestionsInTestException"></exception>
        public async Task<List<string>> SendTestQuestions(int testId)
        {
            var result = await _userTestService.SendTestQuestions(testId);

            if (result.Count() == 0)
            {
                throw new NoQuestionsInTestException("This test hasn't got questions yet!");
            }
            else
            {
                return result;
            }
        }
        

        /// <summary>
        /// Accept list of answears and testid, and send them into CheckUserTestFunction of userTestServiceRepository
        /// </summary>
        /// <param name="answers"></param>
        /// <param name="testId"></param>
        /// <returns></returns>
        public async Task<ReadUserTestDto> CheckUserAnswers(List<ReceiveAnswersDto> answers, int testId)
        {
            return await _userTestService.CheckUserTest(answers, testId);
        }
    }
}
