using System;
using System.Runtime.Serialization;

namespace Data.Exceptions
{
    public class InvalideOperationModeException : Exception
    {
        public InvalideOperationModeException()
        { }

        public InvalideOperationModeException(string message) : base(message)
        { }

        public InvalideOperationModeException(string message, Exception inner) : base(message, inner)
        { }

        protected InvalideOperationModeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
