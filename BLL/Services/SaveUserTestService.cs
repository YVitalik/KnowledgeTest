﻿using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SaveUserTestService : ISaveUserTestService
    {
        private readonly IUserTestRepository _userTestRepository;
        private readonly ITestDetailRepository _testDetailRepository;
        public SaveUserTestService(IUserTestRepository userTestRepository, ITestDetailRepository testDetailRepository)
        {
            _userTestRepository = userTestRepository;
            _testDetailRepository = testDetailRepository;
        }
        
        public async Task<ReadUserTestDto> AddTestResult(UserTest userTest, int rightAnswears)
        {
            var testDetails = new TestDetail { PassTime = 20, RightAnswears = rightAnswears, UserTest = userTest };

            await _userTestRepository.AddAsync(userTest);
            await _testDetailRepository.AddAsync(testDetails);

            return new ReadUserTestDto { Passed = userTest.Passed, RightAnswerPercents = userTest.RightAnswerPercents };
        }
    }
}