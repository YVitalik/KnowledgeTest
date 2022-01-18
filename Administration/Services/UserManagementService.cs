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
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Get id of user which should be deleted, if user exists delete him, if doesn't throws an exception
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<string> DeleteUser(string userId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            if (user is null)
            {
                throw new ArgumentNullException();
            }
            
            await _userManager.DeleteAsync(user);

            string deletedUserId = userId;

            return deletedUserId;
        }

        /// <summary>
        /// Accept UpdateUserDto object if user exists, update his credentials (username, email, passweord if neccessary)
        /// if user does not exist throws an exception
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<UpdateUserDto> UpdateUserCredentials(UpdateUserDto updateUser)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == updateUser.UserId);
            
            if (user is null)
            {
                throw new ArgumentNullException();
            }
            
            user.Email = updateUser.Email;
            user.NormalizedEmail = updateUser.Email.ToUpper();

            user.UserName = updateUser.Username;
            user.NormalizedUserName = updateUser.Username.ToUpper();

            if (updateUser.Password != null && updateUser.Password != "")
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, updateUser.Password);
            }

            await _userManager.UpdateAsync(user);

            return updateUser;
        }

        /// <summary>
        /// Assign some roles to specific user
        /// </summary>
        /// <param name="assignUserToRoles"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Add new user role to database, if role with such name already exists in database throws an exception
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task CreateRole(string roleName)
        {
            var check = await _roleManager.RoleExistsAsync(roleName);
            if (check)
            {
                throw new ArgumentException("Role name is already in use, please choose another!");
            }
            
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        /// <summary>
        /// Get roles of specific user, if user does not exist throws an exception
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<string>> GetRoles(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            
            if (user is null)
            {
                throw new ArgumentNullException("User wasn't found, id is incorrect!");
            }
            else
            {
                return (await _userManager.GetRolesAsync(user)).ToList();
            }
        }

        /// <summary>
        /// Return all roles, if list of user roles is empty throws an exception
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<ReadRolesDto>> GetRoles()
        {
            var roles =  await _roleManager.Roles.ToListAsync();
            var result = _mapper.Map<IEnumerable<ReadRolesDto>>(roles);

            if (result.Count() == 0)
            {
                throw new ArgumentException("List of roles is empty!");
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Return all application users (username, email, id, password) if list is empty throws an exception
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<ReadUserInfoDto>> ShowAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = _mapper.Map<IEnumerable<ReadUserInfoDto>>(users);

            if (result.Count() == 0)
            {
                throw new ArgumentException("User list is empty!");
            }
            else
            {
                return result;
            }
        }
    }
}
