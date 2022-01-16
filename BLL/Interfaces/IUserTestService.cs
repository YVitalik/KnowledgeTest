using BLL.DTOs.TestServiceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserTestService
    {
        Task<List<string>> SendTestQuestions(int testId);
        Task<ReadUserTestDto> CheckUserTest(List<ReceiveAnswersDto> answers, int testId);
    }
}
