using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.AdministrationDTOs
{
    public class CreateRoleDto
    {
        [Required]
        public string RoleName { get; set; }
    }
}
