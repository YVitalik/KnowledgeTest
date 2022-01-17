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
        Task<int> DeleteTest(int testId);
        Task<UpdateTestDto> UpdateTestData(int testId, UpdateTestDto update);
        Task<IEnumerable<UpdateQuestionDto>> GetTestQuestionsAnswears(int testId);
        Task<UpdateQuestionDto> UpdateQuestion(UpdateQuestionDto updateQuestion);
    }
}
