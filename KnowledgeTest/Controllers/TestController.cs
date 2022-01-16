using BLL.DTOs.EditTestDTOs;
using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using DAL.Interfaces;
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
        private readonly IEditTestService _editTestService;
        public TestController(ITestService testService, IEditTestService editTestService)
        {
            _testService = testService;
            _editTestService = editTestService;
        }
        
        [AllowAnonymous]
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
        public async Task<IActionResult> FinishTest(ReceiveAnswersDto answersDtos, int id)
        {
            return Ok(await _testService.CheckUserAnswers(answersDtos, id));
        }

        [HttpPost("addtest")]
        public async Task<IActionResult> AddNewTest(CreateNewTestDto newTest)
        {
            return Ok(await _editTestService.AddNewTest(newTest));
        }

        [HttpPost("addquestion/{id}")]
        public async Task<IActionResult> AddQuestionToTest(CreateQuestionDto createQuestion, int id)
        {
            return Ok(await _editTestService.AddNewQuestion(createQuestion, id));
        }

        [HttpPost("deletetest/{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            return Ok(await _editTestService.DeleteTest(id));
        }
    }
}
