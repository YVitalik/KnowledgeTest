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

        /// <summary>
        /// Function accept two params Username and Password, both of them are required
        /// if Username and Password is correct, user is logged in and accept JWT token
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <exception cref="UserDoesntExistsException"></exception>
        public async Task<IdentityUser> Login(LoginDto login)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == login.Username);
            
            if (user != null)
            {
                return await _userManager.CheckPasswordAsync(user, login.Password) ? user : null;
            }
            else
            {
                throw new UserDoesntExistsException("Username or password is incorrect!");
            }
        }
        
        /// <summary>
        /// Fucntion accept RegisterDto which has 3 parameters all of them are required,
        /// if username and email are not used in other accounts, the new account is created
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="UsernameAlreadyExistsException"></exception>
        public async Task Register(RegisterDto user)
        {
            var check = _userManager.Users.FirstOrDefault(x => x.UserName == user.Username || x.Email == user.Email);
            if (check != null)
            {
                throw new UsernameAlreadyExistsException("Username is already in use, please choose other!");
            }
            
            var result = await _userManager.CreateAsync(new IdentityUser
            {
                Email = user.Email,
                UserName = user.Username
            }, user.Password);
        }
    }
}
