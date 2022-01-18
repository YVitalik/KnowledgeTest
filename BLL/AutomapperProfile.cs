using AutoMapper;
using BLL.DTOs.AdministrationDTOs;
using BLL.DTOs.EditTestDTOs;
using BLL.DTOs.TestServiceDTOs;
using BLL.DTOs.UserManagementDTOs;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

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
            CreateMap<IdentityRole, ReadRolesDto>();
            CreateMap<IdentityUser, ReadUserInfoDto>();
        }
    }
}
