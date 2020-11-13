using System;
using System.Runtime.Serialization;

namespace Data.Exceptions
{
    public class NoCardException : Exception
    {
        public NoCardException()
        { }

        public NoCardException(string message) : base(message)
        { }

        public NoCardException(string message, Exception inner) : base(message, inner)
        { }

        protected NoCardException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
