using System;
using System.Runtime.Serialization;

namespace Data.Exceptions
{
    public class InfucientCardMoneyException : Exception
    {
        public InfucientCardMoneyException()
        { }

        public InfucientCardMoneyException(string message) : base(message)
        { }

        public InfucientCardMoneyException(string message, Exception inner) : base(message, inner)
        { }

        protected InfucientCardMoneyException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
