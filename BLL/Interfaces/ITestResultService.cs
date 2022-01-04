using BLL.DTOs.TestServiceDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITestResultService
    {
        Task<IEnumerable<ReadUserTestDto>> ShowUserPassedTests();
        Task<IEnumerable<ReadDetailedResults>> ShowDetailedResults(int testId);
    }
}
