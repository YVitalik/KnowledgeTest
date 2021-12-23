using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.AdministrationDTOs
{
    public class AssignUserToRolesDto
    {
        public string Username { get; set; }
        public string[] Roles { get; set; }
    }
}
