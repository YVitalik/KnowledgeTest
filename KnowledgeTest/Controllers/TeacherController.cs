using BLL.DTOs.EditTestDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KnowledgeTest.Controllers
{
    [ApiController]
    //[Authorize(Roles = "teacher")]
    [Route("knowledge/teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly IEditTestService _editTestService;
        public TeacherController(IEditTestService editTestService)
        {
            _editTestService = editTestService;
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

        [HttpPost("updatetest/{id}")]
        public async Task<IActionResult> UpdateTest(int id, UpdateTestDto updateTest)
        {
            return Ok(await _editTestService.UpdateTestData(id, updateTest));
        }

        [HttpGet("testquestions/{id}")]
        public async Task<IActionResult> GetTestQuestions(int id)
        {
            return Ok(await _editTestService.GetTestQuestionsAnswears(id));
        }

        [HttpPost("editquestion")]
        public async Task<IActionResult> EditQuestion(UpdateQuestionDto updateQuestion)
        {
            return Ok(await _editTestService.UpdateQuestion(updateQuestion));
        }
    }
}
