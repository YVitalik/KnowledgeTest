using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserTestService : IUserTestService
    {
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly ISaveUserTestService _saveUserTestService;
        private readonly IGetUserInfo _getUserInfo;
        private readonly ITestRepository _testRepository;
        public UserTestService(ITestQuestionRepository testQuestionRepository, ISaveUserTestService saveUserTestService, IGetUserInfo getUserInfo, ITestRepository testRepository)
        {
            _saveUserTestService = saveUserTestService;
            _testQuestionRepository = testQuestionRepository;
            _getUserInfo = getUserInfo; // New variable that allows to take current user id
            _testRepository = testRepository;
        }


        /// <summary>
        /// Send questions of specific test to user
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        public async Task<List<string>> SendTestQuestions(int testId)
        {
            var allQuestions = await _testQuestionRepository.GetAllAsync();
            var questions = allQuestions.Where(x => x.TestId == testId).Select(x => x.Question).ToList();

            return questions;
        }

        /// <summary>
        /// Receive List of objects of type ReceiveAnswersDto, and specific test id, than compares user answears with test answears 
        /// </summary>
        /// <param name="userAnswers"></param>
        /// <param name="testId"></param>
        /// <returns></returns>
        public async Task<ReadUserTestDto> CheckUserTest(List<ReceiveAnswersDto> userAnswers, int testId)
        {
            var allAnswears = await _testQuestionRepository.GetAllAsync();
            var testName = await _testRepository.GetByIdAsync(testId);
            var testAnswears = allAnswears.Where(x => x.TestId == testId).Select(x => x.Answear).ToList();
            var userAnswearsWithoutSpaces = RemoveWhiteSpaces(userAnswers);

            var rightAnswears = 0;

            if (userAnswearsWithoutSpaces.Count > 0)
            {
                for (int i = 0; i < userAnswearsWithoutSpaces.Count; i++)
                {
                    if (userAnswearsWithoutSpaces[i].ToUpper() == testAnswears[i].ToUpper())
                    {
                        rightAnswears++;
                    }
                }
            }

            double rightAnswersPercents = CountPercents(testAnswears, rightAnswears);
            bool passed = rightAnswersPercents > 80 ? true : false;

            var userTestResult = new UserTest
            {
                RightAnswerPercents = rightAnswersPercents,
                Passed = passed,
                UserId = _getUserInfo.GetCurrentUserId(),
                TestName = testName.TestName
            };
            
            await _saveUserTestService.AddTestResult(userTestResult, rightAnswears);

            return new ReadUserTestDto { Passed = passed, RightAnswerPercents = rightAnswersPercents, TestName = testName.TestName };
        }

        /// <summary>
        /// Private function than count right answear's percents
        /// </summary>
        /// <param name="allAnswears"></param>
        /// <param name="rightAnswears"></param>
        /// <returns></returns>
        private double CountPercents(List<string> allAnswears, int rightAnswears)
        {
            return (100 * rightAnswears) / allAnswears.Count;
        }

        /// <summary>
        /// Remove white spaces in user answears (in a case if user made it accidentally
        /// </summary>
        /// <param name="answersDto"></param>
        /// <returns></returns>
        private List<string> RemoveWhiteSpaces(List<ReceiveAnswersDto> answersDto)
        {
            var result = new List<string>();
            
            foreach (var answear in answersDto)
            {
                result.Add(String.Concat(answear.AnswearValue.Where(c => !Char.IsWhiteSpace(c))));
            }

            return result;
        }

    }
}
