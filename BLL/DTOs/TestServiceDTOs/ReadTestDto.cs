using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.TestServiceDTOs
{
    public class ReadTestDto
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public int TimeInMin { get; set; }
    }
}
