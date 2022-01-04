using BLL.DTOs.EditTestDTOs;
using BLL.DTOs.TestServiceDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEditTestService
    {
        Task<CreateQuestionDto> AddNewQuestion(CreateQuestionDto newQuestion, int testId);
        Task<CreateNewTestDto> AddNewTest(CreateNewTestDto newTest);
    }
}
