using BLL.DTOs.TestServiceDTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITestService
    {
        Task<IEnumerable<ReadTestDto>> FindTestAsync(string testToFind);
        Task<List<string>> SendTestQuestions(int testId);
        Task<ReadUserTestDto> CheckUserAnswers(List<ReceiveAnswersDto> answers, int testId);
        Task<IEnumerable<ReadTestDto>> GetAllTests();
    }
}
