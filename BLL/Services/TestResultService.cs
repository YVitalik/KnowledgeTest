using AutoMapper;
using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using DAL.Interfaces;
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
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile<AutomapperProfile>()
            );
            mapper = new Mapper(configuration);
            _mapper = mapper;
            _userTestRepository = userTestRepository;
            _getUserInfo = getUserInfo;
        }

        public Task<IEnumerable<ReadDetailedResults>> ShowDetailedResults()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ReadUserTestDto>> ShowUserPassedTests()
        {
            var testResults = await _userTestRepository.GetAllAsync();
            var testResultOfUser = testResults.Where(x => x.UserId == _getUserInfo.GetCurrentUserId());
            return _mapper.Map<IEnumerable<ReadUserTestDto>>(testResultOfUser);
        }
    }
}
