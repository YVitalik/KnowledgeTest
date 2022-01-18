using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.AdministrationDTOs
{
    public class AssignUserToRolesDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string[] Roles { get; set; }
    }
}
