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
        IEnumerable<ReadTestDto> FindTestAsync(string testToFind);
        List<string> SendTestQuestions(int testId); 
    }
}
