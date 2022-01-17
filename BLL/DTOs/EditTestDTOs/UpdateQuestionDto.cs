using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.EditTestDTOs
{
    public class UpdateQuestionDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answear { get; set; }
    }
}
