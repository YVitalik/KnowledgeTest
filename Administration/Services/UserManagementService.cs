using Administration.Interfaces;
using AutoMapper;
using BLL;
using BLL.DTOs.AdministrationDTOs;
using BLL.DTOs.UserManagementDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UserManagementService(UserManager<IdentityUser> userManager,
                                     RoleManager<IdentityRole> roleManager,
                                     IMapper mapper)
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile<AutomapperProfile>()
            );
            mapper = new Mapper(configuration);
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> DeleteUser(string userId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            await _userManager.DeleteAsync(user);

            string deletedUserId = userId;

            return deletedUserId;
        }

        public async Task<UpdateUserDto> UpdateUserCredentials(UpdateUserDto updateUser)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == updateUser.UserId);
            
            user.Email = updateUser.Email;
            user.NormalizedEmail = updateUser.Email.ToUpper();

            user.UserName = updateUser.Username;
            user.NormalizedUserName = updateUser.Username.ToUpper();

            user.PasswordHash =  _userManager.PasswordHasher.HashPassword(user, updateUser.Password);

            await _userManager.UpdateAsync(user);

            return updateUser;
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

        public async Task<IEnumerable<ReadRolesDto>> GetRoles()
        {
            var roles =  await _roleManager.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<ReadRolesDto>>(roles);
        }

        public async Task<IEnumerable<ReadUserInfoDto>> ShowAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<ReadUserInfoDto>>(users);
        }
    }
}
