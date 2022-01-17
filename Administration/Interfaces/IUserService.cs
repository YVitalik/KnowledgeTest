using BLL.DTOs.AdministrationDTOs;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Interfaces
{
    public interface IUserService
    {
        Task Register(RegisterDto user);
        Task<IdentityUser> Login(LoginDto login);
    }
}
