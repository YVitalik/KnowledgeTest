using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KnowledgeTest.Controllers
{
    [Authorize]
    [ApiController]
    [Route("knowledge/test")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet("find/{testName}")]
        public async Task<IActionResult> FindTestAsync(string testName)
        {
            return Ok(await _testService.FindTestAsync(testName));
        }

        [HttpGet("starttest/{id}")]
        public async Task<IActionResult> StartTest(int id)
        {
            return Ok(await _testService.SendTestQuestions(id));
        }

        [HttpPost("finishtest/{id}")]
        public async Task<IActionResult> FinishTest(ReceiveAnswersDto answersDtos, int id)
        {
            return Ok(await _testService.CheckUserAnswers(answersDtos, id));
        }
    }
}
