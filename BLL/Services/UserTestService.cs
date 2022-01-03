using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserTestService : IUserTestService
    {
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly ISaveUserTestService _saveUserTestService;
        public UserTestService(ITestQuestionRepository testQuestionRepository, ISaveUserTestService saveUserTestService)
        {
            _saveUserTestService = saveUserTestService;
            _testQuestionRepository = testQuestionRepository;
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
            
            var rightAnswears = 0;

            if (userAnswers.Answears.Length > 0)
            {
                for (int i = 0; i < userAnswers.Answears.Length; i++)
                {
                    if (userAnswers.Answears[i] == testAnswears[i])
                    {
                        rightAnswears++;
                    }
                }
            }

            double rightAnswersPercents = CountPercents(testAnswears, rightAnswears);
            bool passed = rightAnswersPercents > 80 ? true : false;

            var userTestResult = new UserTest { RightAnswerPercents = rightAnswersPercents, Passed = passed, UserId = "test" };
            
            await _saveUserTestService.AddTestResult(userTestResult, rightAnswears);

            return new ReadUserTestDto { Passed = passed, RightAnswerPercents = rightAnswersPercents };
        }

        private double CountPercents(List<string> allAnswears, int rightAnswears)
        {
            return (100 * rightAnswears) / allAnswears.Count;
        }
    }
}
