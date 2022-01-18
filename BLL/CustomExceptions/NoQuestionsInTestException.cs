using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class NoQuestionsInTestException : Exception
    {
        public NoQuestionsInTestException()
        {
        }

        public NoQuestionsInTestException(string message) : base(message)
        {
        }

        public NoQuestionsInTestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoQuestionsInTestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
