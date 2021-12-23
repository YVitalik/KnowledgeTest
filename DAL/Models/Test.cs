using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public int TimeInMin { get; set; }
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
    }
}
