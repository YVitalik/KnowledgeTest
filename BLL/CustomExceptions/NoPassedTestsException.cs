using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class NoPassedTestsException : Exception
    {
        public NoPassedTestsException()
        {
        }

        public NoPassedTestsException(string message) : base(message)
        {
        }

        public NoPassedTestsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoPassedTestsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
