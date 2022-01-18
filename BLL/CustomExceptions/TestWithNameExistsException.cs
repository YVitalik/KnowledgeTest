using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class TestWithNameExistsException : Exception
    {
        public TestWithNameExistsException()
        {
        }

        public TestWithNameExistsException(string message) : base(message)
        {
        }

        public TestWithNameExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestWithNameExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
