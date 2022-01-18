using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.EditTestDTOs
{
    public class UpdateTestDto
    {
        [Required]
        public string TestName { get; set; }
        [Required]
        public int TimeInMin { get; set; }
    }
}
