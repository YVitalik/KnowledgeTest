using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TestDetail
    {
        public int Id { get; set; }
        public int RightAnswears { get; set; }
        public double PassTime { get; set; }
        public int UserTestId { get; set; }
        public UserTest UserTest { get; set; }
    }
}
