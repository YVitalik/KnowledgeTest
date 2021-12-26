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
        private readonly ITestRepository _testRepository;
        private readonly ISaveUserTestService _saveUserTestService;
        public UserTestService(ITestRepository testRepository, ISaveUserTestService saveUserTestService)
        {
            _saveUserTestService = saveUserTestService;
            _testRepository = testRepository;
        }
        
        public async Task<ReadUserTestDto> CheckUserTest(ReceiveAnswersDto userAnswers, int testId)
        {
            var testAnswers = _testRepository.GetAll().Where(x => x.Id == testId).Include(x => x.TestQuestions);
            var answearsList = new List<string>();
            var rightAnswears = 0;

            foreach (var item in testAnswers)
            {
                foreach (var question in item.TestQuestions)
                {
                    answearsList.Add(question.Answear);
                }
            }

            if (userAnswers.Answears.Length > 0)
            {
                for (int i = 0; i < userAnswers.Answears.Length; i++)
                {
                    if (userAnswers.Answears[i] == answearsList[i])
                    {
                        rightAnswears++;
                    }
                }
            }

            double rightAnswersPercents = CountPercents(answearsList, rightAnswears);
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
