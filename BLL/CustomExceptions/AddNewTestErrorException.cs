using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class AddNewTestErrorException : Exception
    {
        public AddNewTestErrorException()
        {
        }

        public AddNewTestErrorException(string message) : base(message)
        {
        }

        public AddNewTestErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddNewTestErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
