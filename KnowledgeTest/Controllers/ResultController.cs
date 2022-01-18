using BLL.CustomExceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KnowledgeTest.Controllers
{
    [Authorize(Roles = "student, teacher")]
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
            try
            {
                return Ok(await _testResultService.ShowUserPassedTests());
            }
            catch (NoPassedTestsException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("showdetails/{id}")]
        public async Task<IActionResult> ShowDetailedResults(int id)
        {
            try
            {
                return Ok(await _testResultService.ShowDetailedResults(id));
            }
            catch (NoPassedTestsException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
