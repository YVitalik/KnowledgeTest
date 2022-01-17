using AutoMapper;
using BLL.DTOs.EditTestDTOs;
using BLL.DTOs.TestServiceDTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Test, ReadTestDto>();
            CreateMap<UserTest, ReadUserTestDto>();
            CreateMap<TestDetail, ReadDetailedResults>();
            CreateMap<TestQuestion, UpdateQuestionDto>();
        }
    }
}
