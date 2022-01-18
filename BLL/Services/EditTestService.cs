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
            _mapper = mapper;
            _testRepository = testRepository;
            _testQuestionRepository = testQuestionRepository;
        }

        /// <summary>
        /// Accept CreateQuestionDto object, and testId of test to which question should be added
        /// if such test exists, new TestQuestion is added to testQuestionRepository
        /// </summary>
        /// <param name="newQuestion"></param>
        /// <param name="testId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// Accept CreateNewTestDto object, if testRepository, does not contain 
        /// test with such name, new test is added, else throws exception
        /// </summary>
        /// <param name="newTest"></param>
        /// <returns></returns>
        /// <exception cref="TestWithNameExistsException"></exception>
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

        /// <summary>
        /// Accept id of the test that should be deleted, is test exists, function removes it from database
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        /// <exception cref="TestDoesNotExistsException"></exception>
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

        /// <summary>
        /// Return IEnumerable<UpdateQuestionDto> objects for the test of specified id
        /// if test does not exist throws exception which is handled in controller
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        /// <exception cref="TestDoesNotExistsException"></exception>
        /// <exception cref="NoQuestionsInTestException"></exception>
        public async Task<IEnumerable<UpdateQuestionDto>> GetTestQuestionsAnswears(int testId)
        {
            var check = await _testRepository.GetByIdAsync(testId);
            if (check is null)
            {
                throw new TestDoesNotExistsException("Test with such id does not exist!");
            }

            var questionsAnswears = await _testQuestionRepository.GetAllAsync();
            var questionsAnswersForTest = questionsAnswears.Where(x => x.TestId == testId);
            var result = _mapper.Map<IEnumerable<UpdateQuestionDto>>(questionsAnswersForTest);

            if (result.Count() == 0)
            {
                throw new NoQuestionsInTestException("This test has not got questions yet!");
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Accept UpdateQuestionDto object, find object with the same id in database, if 
        /// object does not exist throws exception, else update TestQuestion with the same id in database
        /// </summary>
        /// <param name="updateQuestion"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<UpdateQuestionDto> UpdateQuestion(UpdateQuestionDto updateQuestion)
        {
            var check = await _testQuestionRepository.GetByIdAsync(updateQuestion.Id);
            if (check is null)
            {
                throw new ArgumentNullException();
            }

            var questionToUpdate = new TestQuestion { Id = updateQuestion.Id, Question = updateQuestion.Question, Answear = updateQuestion.Answear };
            await _testQuestionRepository.UpdateAsync(questionToUpdate);
            return updateQuestion;
        }

        /// <summary>
        /// Receive id of the test that should be updated, and new data for the test (UpdateTestDto object)
        /// if test exists update that test in database
        /// else throws an exception
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        /// <exception cref="TestDoesNotExistsException"></exception>
        public async Task<UpdateTestDto> UpdateTestData(int testId, UpdateTestDto update)
        {
            var check = await _testRepository.GetByIdAsync(testId);
            if (check is null)
            {
                throw new TestDoesNotExistsException("Test with such id does not exist!");
            }

            var test = new Test { Id = testId, TestName = update.TestName, TimeInMin = update.TimeInMin };
            await _testRepository.UpdateAsync(test);
            return update;
        }
    }
}
