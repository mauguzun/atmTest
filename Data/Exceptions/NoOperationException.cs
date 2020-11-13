using System;
using System.Runtime.Serialization;

namespace Data.Exceptions
{
    public class NoOperationException : Exception
    {
        public NoOperationException()
        { }

        public NoOperationException(string message) : base(message)
        { }

        public NoOperationException(string message, Exception inner) : base(message, inner)
        { }

        protected NoOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
