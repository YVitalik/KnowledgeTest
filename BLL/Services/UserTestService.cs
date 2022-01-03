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
        public UserTestService(ITestQuestionRepository testQuestionRepository, ISaveUserTestService saveUserTestService, IGetUserInfo getUserInfo)
        {
            _saveUserTestService = saveUserTestService;
            _testQuestionRepository = testQuestionRepository;
            _getUserInfo = getUserInfo;
        }

        public async Task<List<string>> SendTestQuestions(int testId)
        {
            var allQuestions = await _testQuestionRepository.GetAllAsync();
            var questions = allQuestions.Where(x => x.TestId == testId).Select(x => x.Question).ToList();

            return questions;
        }

        public async Task<ReadUserTestDto> CheckUserTest(ReceiveAnswersDto userAnswers, int testId)
        {
            var allAnswears = await _testQuestionRepository.GetAllAsync();
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

            var userTestResult = new UserTest { RightAnswerPercents = rightAnswersPercents, Passed = passed, UserId = _getUserInfo.GetCurrentUserId() };
            
            await _saveUserTestService.AddTestResult(userTestResult, rightAnswears);

            return new ReadUserTestDto { Passed = passed, RightAnswerPercents = rightAnswersPercents };
        }

        private double CountPercents(List<string> allAnswears, int rightAnswears)
        {
            return (100 * rightAnswears) / allAnswears.Count;
        }

        private List<string> RemoveWhiteSpaces(ReceiveAnswersDto answersDto)
        {
            var result = new List<string>();
            
            foreach (var answear in answersDto.Answears)
            {
                result.Add(String.Concat(answear.Where(c => !Char.IsWhiteSpace(c))));
            }

            return result;
        }

    }
}
