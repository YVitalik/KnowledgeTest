﻿using Administration.Interfaces;
using BLL.CustomExceptions;
using BLL.DTOs.AdministrationDTOs;
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

        public async Task AssignUserToRoles(AssignUserToRolesDto assignUserToRoles)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == assignUserToRoles.Username || u.Email == assignUserToRoles.Username);
            var roles = _roleManager.Roles.ToList().Where(r => assignUserToRoles.Roles.Contains(r.Name, StringComparer.OrdinalIgnoreCase))
                .Select(r => r.NormalizedName).ToList();

            var result = await _userManager.AddToRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                throw new System.Exception(string.Join(';', result.Errors.Select(x => x.Description)));
            }
        }

        public async Task CreateRole(string roleName)
        {
            if (roleName is null) throw new ArgumentNullException("Rolename ca not be null");
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task<IEnumerable<string>> GetRoles(IdentityUser user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }

        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IdentityUser> Login(LoginDto login)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == login.Username || x.UserName == login.Username);
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
                throw new UsernameAlreadyExistsException("Please choose other username");
            }
        }
    }
}
