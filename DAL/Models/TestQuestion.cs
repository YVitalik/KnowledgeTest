using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answear { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
