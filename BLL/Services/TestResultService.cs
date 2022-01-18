using AutoMapper;
using BLL.CustomExceptions;
using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TestResultService : ITestResultService
    {
        private readonly IUserTestRepository _userTestRepository;
        private readonly IMapper _mapper;
        private readonly IGetUserInfo _getUserInfo;
        public TestResultService(IUserTestRepository userTestRepository, IMapper mapper, IGetUserInfo getUserInfo)
        {
            _mapper = mapper;
            _userTestRepository = userTestRepository;
            _getUserInfo = getUserInfo;
        }

        /// <summary>
        /// Accept usertestid and show detailed results of this test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NoPassedTestsException"></exception>
        public async Task<IEnumerable<ReadDetailedResults>> ShowDetailedResults(int id)
        {
            var testsResults = await _userTestRepository.GetTestsResultsWithDetails();
            var testsResultsOfUser = testsResults.Where(x => x.UserId == _getUserInfo.GetCurrentUserId()).Select(x => x.TestDetail)
                                                                                                         .Where(x => x.UserTestId == id);
            var result = _mapper.Map<IEnumerable<ReadDetailedResults>>(testsResultsOfUser);

            if (result.Count() == 0)
            {
                throw new NoPassedTestsException("Test details with such id does not exist!");
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Get current user id, and show all tests that he passed, if no user hasn't passed any tests yet, throws an exception
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoPassedTestsException"></exception>
        public async Task<IEnumerable<ReadUserTestDto>> ShowUserPassedTests()
        {
            var testResults = await _userTestRepository.GetAllAsync();
            var testResultOfUser = testResults.Where(x => x.UserId == _getUserInfo.GetCurrentUserId());

            var result = _mapper.Map<IEnumerable<ReadUserTestDto>>(testResultOfUser);

            if (result.Count() == 0)
            {
                throw new NoPassedTestsException("You have not pass any tests yet!");
            }
            else
            {
                return result;
            }
        }
    }
}
