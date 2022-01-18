using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class NoTestsFoundException : Exception
    {
        public NoTestsFoundException()
        {
        }

        public NoTestsFoundException(string message) : base(message)
        {
        }

        public NoTestsFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoTestsFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
