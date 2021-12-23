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
        ReadUserTestDto CheckUserTest(string[] answers, int testId);
    }
}
