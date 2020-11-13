using System;
using System.Runtime.Serialization;

namespace Data.Exceptions
{
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException()
        { }

        public InvalidAmountException(string message) : base(message)
        { }

        public InvalidAmountException(string message, Exception inner) : base(message, inner)
        { }

        protected InvalidAmountException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
