using System;
using System.Runtime.Serialization;

namespace NightlyCode.Discord.Rest {
    public class RateLimitException : Exception {

        public RateLimitException(string message)
            : base(message) {}

        public RateLimitException(string message, Exception innerException)
            : base(message, innerException) {}

        protected RateLimitException(SerializationInfo info, StreamingContext context)
            : base(info, context) {}
    }
}