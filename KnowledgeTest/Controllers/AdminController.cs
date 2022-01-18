using Administration.Interfaces;
using BLL.DTOs.AdministrationDTOs;
using BLL.DTOs.UserManagementDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KnowledgeTest.Controllers
{
    [ApiController]
    [Route("knowledge/admin")]
    [Authorize(Roles = "admin")]
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
            try
            {
                if (updateUser.Username.Contains(" ")) return BadRequest("Username can not contain whitespaces!");
                return Ok(await _userManagementService.UpdateUserCredentials(updateUser));
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _userManagementService.DeleteUser(id);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(400, ex.Message); 
            }
        }

        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole(CreateRoleDto model)
        {
            try
            {
                await _userManagementService.CreateRole(model.RoleName);
                return Ok(model.RoleName);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("getroles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                return Ok(await _userManagementService.GetRoles());
            }
            catch (ArgumentException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("userroles/{id}")]
        public async Task<IActionResult> GetUserRoles(string id)
        {
            try
            {
                return Ok(await _userManagementService.GetRoles(id));
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("assignusertorole")]
        public async Task<IActionResult> AssignUserToRole(AssignUserToRolesDto model)
        {
            try
            {
                await _userManagementService.AssignUserToRoles(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplicationUsers()
        {
            try
            {
                return Ok(await _userManagementService.ShowAllUsers());
            }
            catch (ArgumentException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
