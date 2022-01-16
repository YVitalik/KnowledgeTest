using BLL.DTOs.EditTestDTOs;
using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllTests()
        {
            return Ok(await _testService.GetAllTests());
        }

        [HttpGet("starttest/{id}")]
        public async Task<IActionResult> StartTest(int id)
        {
            return Ok(await _testService.SendTestQuestions(id));
        }

        [HttpPost("finishtest/{id}")]
        public async Task<IActionResult> FinishTest(List<ReceiveAnswersDto> answersDtos, int id)
        {
            return Ok(await _testService.CheckUserAnswers(answersDtos, id));
        }
    }
}
