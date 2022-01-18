using Administration.Interfaces;
using BLL.DTOs.AdministrationDTOs;
using BLL.DTOs.UserManagementDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KnowledgeTest.Controllers
{
    [ApiController]
    [Route("knowledge/admin")]
    //[Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        public AdminController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpPost("updateuser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUser)
        {
            return Ok(await _userManagementService.UpdateUserCredentials(updateUser));
        }

        [HttpPost("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userManagementService.DeleteUser(id);
            return Ok();
        }

        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole(CreateRoleDto model)
        {
            await _userManagementService.CreateRole(model.RoleName);
            return Ok();
        }

        [HttpGet("getroles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _userManagementService.GetRoles());
        }

        [HttpPost("assignusertorole")]
        public async Task<IActionResult> AssignUserToRole(AssignUserToRolesDto model)
        {
            await _userManagementService.AssignUserToRoles(model);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplicationUsers()
        {
            return Ok(await _userManagementService.ShowAllUsers());
        }
    }
}
