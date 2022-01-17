using AutoMapper;
using BLL.CustomExceptions;
using BLL.DTOs.EditTestDTOs;
using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BLL.Services
{
    public class EditTestService : IEditTestService
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        public EditTestService(ITestRepository testRepository, ITestQuestionRepository testQuestionRepository, IMapper mapper)
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile<AutomapperProfile>()
            );
            mapper = new Mapper(configuration);
            _mapper = mapper;
            _testRepository = testRepository;
            _testQuestionRepository = testQuestionRepository;
        }

        public async Task<CreateQuestionDto> AddNewQuestion(CreateQuestionDto newQuestion, int testId)
        {
            var test = await _testRepository.GetByIdAsync(testId);

            var question = new TestQuestion { Question = newQuestion.Question, Answear = newQuestion.Answear, Test = test, TestId = test.Id };
            await _testQuestionRepository.AddAsync(question);

            return newQuestion;
        }

        public async Task<CreateNewTestDto> AddNewTest(CreateNewTestDto newTest)
        {
            var check = await _testRepository.GetAllAsync();
            var checkName = check.FirstOrDefault(x => x.TestName == newTest.TestName);

            if (newTest.TestName is null || newTest.TimeInMin == 0)
            {
                throw new AddNewTestErrorException("Testname is empty, or time in min is less then zero");
            }

            if (checkName != null)
            {
                throw new TestAlreadyExistsException("TestName already exists, please choose other");
            }

            var test = new Test { TestName = newTest.TestName, TimeInMin = newTest.TimeInMin };
            await _testRepository.AddAsync(test);

            return newTest;
        }

        public async Task<int> DeleteTest(int testId)
        {
            await _testRepository.DeleteByIdAsync(testId);
            return testId;
        }

        public async Task<IEnumerable<UpdateQuestionDto>> GetTestQuestionsAnswears(int testId)
        {
            var questionsAnswears = await _testQuestionRepository.GetAllAsync();
            var result = questionsAnswears.Where(x => x.TestId == testId);
            return _mapper.Map<IEnumerable<UpdateQuestionDto>>(result);
        }

        public async Task<UpdateQuestionDto> UpdateQuestion(UpdateQuestionDto updateQuestion)
        {
            var questionToUpdate = new TestQuestion { Id = updateQuestion.Id, Question = updateQuestion.Question, Answear = updateQuestion.Answear };
            await _testQuestionRepository.UpdateAsync(questionToUpdate);
            return updateQuestion;
        }

        public async Task<UpdateTestDto> UpdateTestData(int testId, UpdateTestDto update)
        {
            var test = new Test { Id = testId, TestName = update.TestName, TimeInMin = update.TimeInMin };
            await _testRepository.UpdateAsync(test);
            return update;
        }
    }
}
