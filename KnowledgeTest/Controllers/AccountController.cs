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
        
        public AccountController(IUserService userService, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            await _userService.Register(model);
            return Created(string.Empty, string.Empty);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userService.Login(model);
            if (user is null) return BadRequest();
            var roles = await _userService.GetRoles(user);
            var token = JwtHelper.GenerateJwt(user, roles, _jwtSettings);
            return Ok(new { Token = token });
        }

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRole)
        {
            await _userService.CreateRole(createRole.RoleName);
            return Ok();
        }

        [HttpGet("getRoles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _userService.GetRoles());
        }

        [HttpPost("assignUserToRole")]
        public async Task<IActionResult> AssignUserToRole(AssignUserToRolesDto model)
        {
            await _userService.AssignUserToRoles(model);

            return Ok();
        }
    }
}
