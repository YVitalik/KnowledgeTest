using Administration.Interfaces;
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

        [HttpPost("deleteuser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            return Ok(await _userManagementService.DeleteUser(userId));
        }
    }
}
