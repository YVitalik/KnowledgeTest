using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.TestServiceDTOs
{
    public class ReadUserTestDto
    {
        public bool Passed { get; set; }
        public double RightAnswerPercents { get; set; }
    }
}
