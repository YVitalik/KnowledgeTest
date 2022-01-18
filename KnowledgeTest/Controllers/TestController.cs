using BLL.CustomExceptions;
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
            try
            {
                if (testName is null) return BadRequest("This field cannot be empty!");

                return Ok(await _testService.FindTestAsync(testName));
            }
            catch (NoTestsFoundException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllTests()
        {
            try
            {
                return Ok(await _testService.GetAllTests());
            }
            catch (TestRepositoryIsEmptyException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("starttest/{id}")]
        public async Task<IActionResult> StartTest(int id)
        {
            try
            {
                return Ok(await _testService.SendTestQuestions(id));
            }
            catch (NoQuestionsInTestException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("finishtest/{id}")]
        public async Task<IActionResult> FinishTest(List<ReceiveAnswersDto> answersDtos, int id)
        {
            return Ok(await _testService.CheckUserAnswers(answersDtos, id));
        }
    }
}
