using Administration.Interfaces;
using BLL.CustomExceptions;
using BLL.DTOs.AdministrationDTOs;
using BLL.Helpers.JwtHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<IdentityUser> userManager,
                           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityUser> Login(LoginDto login)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == login.Username);
            if (user is null) throw new UserDoesntExistsException("Username or password is incorrect");
            return await _userManager.CheckPasswordAsync(user, login.Password) ? user : null;
        }
        
        public async Task Register(RegisterDto user)
        {
            var result = await _userManager.CreateAsync(new IdentityUser
            {
                Email = user.Email,
                UserName = user.Username
            }, user.Password);

            if (!result.Succeeded)
            {
                throw new System.Exception(string.Join(';', result.Errors.Select(x => x.Description)));
            }
        }
    }
}
