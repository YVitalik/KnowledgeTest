using Administration.Interfaces;
using BLL.CustomExceptions;
using BLL.DTOs.AdministrationDTOs;
using BLL.Helpers.JwtHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeTest.Controllers
{
    [ApiController]
    [Route("knowledge/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;
        private readonly IUserManagementService _userManagementService;
        public AccountController(IUserService userService, IOptionsSnapshot<JwtSettings> jwtSettings, IUserManagementService userManagementService)
        {
            _jwtSettings = jwtSettings.Value;
            _userService = userService;
            _userManagementService = userManagementService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                await _userService.Register(model);
                return Ok(model);
            }
            catch (UsernameAlreadyExistsException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            try
            {
                var user = await _userService.Login(model);
                
                if (user is null) return BadRequest("Password is incorrect");
                
                var roles = await _userManagementService.GetRoles(user);
                return Ok(new { Token = JwtHelper.GenerateJwt(user, roles, _jwtSettings) });
            }
            catch (UserDoesntExistsException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
