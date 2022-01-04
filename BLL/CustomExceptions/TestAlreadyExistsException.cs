using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class TestAlreadyExistsException : Exception
    {
        public TestAlreadyExistsException()
        {
        }

        public TestAlreadyExistsException(string message) : base(message)
        {
        }

        public TestAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
