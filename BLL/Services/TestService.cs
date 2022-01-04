using AutoMapper;
using BLL.CustomExceptions;
using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<ReadTestDto>> GetAllTests()
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadTestDto>>(result);
        }

        public async Task<IEnumerable<ReadTestDto>> FindTestAsync(string testToFind)
        {
            if (testToFind is null || testToFind.Length == 0)
            {
                throw new ThisFieldCanNotBeEmptyException("This field can not be empty, please enter smth here");
            }
            
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
            
            return result;
        }

        public async Task<List<string>> SendTestQuestions(int testId)
        {
            return await _userTestService.SendTestQuestions(testId);
        }
        
        public async Task<ReadUserTestDto> CheckUserAnswers(ReceiveAnswersDto answers, int testId)
        {
            return await _userTestService.CheckUserTest(answers, testId);
        }
    }
}
