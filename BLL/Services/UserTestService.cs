using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using DAL.Interfaces;
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
        public UserTestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        
        public ReadUserTestDto CheckUserTest(string[] answers, int testId)
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
      
            for (int i = 0; i < answearsList.Count; i++)
            {
                if (answers[i] == answearsList[i])
                {
                    rightAnswears++;
                }
            }

            double rightAnswersPercents = CountPercents(answearsList, rightAnswears);

            return new ReadUserTestDto { Passed = true, RightAnswerPercents = 80.0 };
        }

        private double CountPercents(List<string> allAnswears, int rightAnswears)
        {
            return (100 * rightAnswears) / allAnswears.Count;
        }
    }
}
