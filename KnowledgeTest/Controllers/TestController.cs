using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeTest.Controllers
{
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
        public IActionResult FindTestAsync(string testName)
        {
            return Ok(_testService.FindTestAsync(testName));
        }

        [HttpGet("startest/{id}")]
        public IActionResult StartTest(int id)
        {
            return Ok(_testService.SendTestQuestions(id));
        }

        [HttpPost("finishtest/{id}")]
        public async Task<IActionResult> FinishTest(ReceiveAnswersDto answersDtos, int id)
        {
            return Ok(await _testService.CheckUserAnswers(answersDtos, id));
        }
    }
}
