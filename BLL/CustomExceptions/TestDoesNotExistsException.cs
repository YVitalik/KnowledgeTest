using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class TestDoesNotExistsException : Exception
    {
        public TestDoesNotExistsException()
        {
        }

        public TestDoesNotExistsException(string message) : base(message)
        {
        }

        public TestDoesNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestDoesNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
