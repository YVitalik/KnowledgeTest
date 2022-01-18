using BLL.CustomExceptions;
using BLL.DTOs.EditTestDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                if (newTest.TimeInMin <= 0) return BadRequest("Time for passing must be >= 1");
                return Ok(await _editTestService.AddNewTest(newTest));
            }
            catch (TestWithNameExistsException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("addquestion/{id}")]
        public async Task<IActionResult> AddQuestionToTest(CreateQuestionDto createQuestion, int id)
        {
            try
            {
                return Ok(await _editTestService.AddNewQuestion(createQuestion, id));
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("deletetest/{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            try
            {
                return Ok(await _editTestService.DeleteTest(id));
            }
            catch (TestDoesNotExistsException ex)
            {
                return StatusCode(400, ex.Message);
            }
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
