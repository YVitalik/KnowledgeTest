using BLL.DTOs.AdministrationDTOs;
using BLL.DTOs.UserManagementDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Interfaces
{
    public interface IUserManagementService
    {
        Task<UpdateUserDto> UpdateUserCredentials(UpdateUserDto updateUser);
        Task<string> DeleteUser(string userId);
        Task AssignUserToRoles(AssignUserToRolesDto assignUserToRoles);
        Task CreateRole(string roleName);
        Task<IEnumerable<string>> GetRoles(IdentityUser user);
        Task<IEnumerable<IdentityRole>> GetRoles();
    }
}
