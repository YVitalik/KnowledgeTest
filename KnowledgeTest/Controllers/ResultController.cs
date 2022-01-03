using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KnowledgeTest.Controllers
{
    [Authorize]
    [ApiController]
    [Route("knowledge/results")]
    public class ResultController : ControllerBase
    {
        private readonly ITestResultService _testResultService;
        public ResultController(ITestResultService testResultService)
        {
            _testResultService = testResultService;
        }
        
        [HttpGet("testsresults")]
        public async Task<IActionResult> ShowTestsResults()
        {
            return Ok(await _testResultService.ShowUserPassedTests());
        }

        [HttpGet("showdetails")]
        public async Task<IActionResult> ShowDetailedResults()
        {
            return Ok();
        }
    }
}
