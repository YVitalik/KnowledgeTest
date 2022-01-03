﻿using BLL.DTOs.TestServiceDTOs;
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
        Task<ReadUserTestDto> CheckUserTest(ReceiveAnswersDto answers, int testId);
    }
}
