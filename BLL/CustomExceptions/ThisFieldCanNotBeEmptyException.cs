using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class ThisFieldCanNotBeEmptyException : Exception
    {
        public ThisFieldCanNotBeEmptyException()
        {
        }

        public ThisFieldCanNotBeEmptyException(string message) : base(message)
        {
        }

        public ThisFieldCanNotBeEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ThisFieldCanNotBeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
