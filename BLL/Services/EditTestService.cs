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
using System;

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

            if (test is null)
            {
                throw new ArgumentNullException();
            }

            var question = new TestQuestion { Question = newQuestion.Question, Answear = newQuestion.Answear, Test = test, TestId = test.Id };
            await _testQuestionRepository.AddAsync(question);

            return newQuestion;
        }

        public async Task<CreateNewTestDto> AddNewTest(CreateNewTestDto newTest)
        {
            var allTests = await _testRepository.GetAllAsync();
            var checkName = allTests.FirstOrDefault(x => x.TestName == newTest.TestName);

            if (checkName != null)
            {
                throw new TestWithNameExistsException("Test name with such name already exist, please choose other name!");
            }

            var test = new Test { TestName = newTest.TestName, TimeInMin = newTest.TimeInMin };
            await _testRepository.AddAsync(test);

            return newTest;
        }

        public async Task<int> DeleteTest(int testId)
        {
            var test = await _testRepository.GetByIdAsync(testId);

            if (test is null)
            {
                throw new TestDoesNotExistsException("Test with such id does not exist!");
            }

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
