using System;
using System.Runtime.Serialization;

namespace Data.Exceptions
{
    public class InvalidCardReaderException : Exception
    {
        public InvalidCardReaderException()
        { }

        public InvalidCardReaderException(string message) : base(message)
        { }

        public InvalidCardReaderException(string message, Exception inner) : base(message, inner)
        { }

        protected InvalidCardReaderException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
