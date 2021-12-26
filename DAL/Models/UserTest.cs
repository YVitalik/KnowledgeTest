using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserTest
    {
        public int Id { get; set; }
        public bool Passed { get; set; }
        public double RightAnswerPercents { get; set; }
        public string UserId { get; set; }
        public TestDetail TestDetail { get; set; }
    }
}
