using System;
using System.Runtime.Serialization;

namespace Data.Exceptions
{
    public class InfucientATMMoneyException : Exception
    {
        public InfucientATMMoneyException()
        { }

        public InfucientATMMoneyException(string message) : base(message)
        { }

        public InfucientATMMoneyException(string message, Exception inner) : base(message, inner)
        { }

        protected InfucientATMMoneyException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
