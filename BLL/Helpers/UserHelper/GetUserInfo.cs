using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers.UserHelper
{
    /// <summary>
    /// Class that helps you to get needed user info id, name etc.
    /// </summary>
    public class GetUserInfo : IGetUserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetUserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
