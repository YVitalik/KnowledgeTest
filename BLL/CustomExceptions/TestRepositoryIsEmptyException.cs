using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class TestRepositoryIsEmptyException : Exception
    {
        public TestRepositoryIsEmptyException()
        {
        }

        public TestRepositoryIsEmptyException(string message) : base(message)
        {
        }

        public TestRepositoryIsEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestRepositoryIsEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
