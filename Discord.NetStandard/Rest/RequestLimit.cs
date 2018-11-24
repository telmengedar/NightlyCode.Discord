using System;

namespace NightlyCode.Discord.Rest {

    /// <summary>
    /// data for request limit of discord rest api
    /// </summary>
    public class RequestLimit {

        /// <summary>
        /// The number of requests that can be made
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// The number of remaining requests that can be made
        /// </summary>
        public int Remaining { get; set; }

        /// <summary>
        /// Epoch time at which the rate limit resets
        /// </summary>
        public DateTime Reset { get; set; }

        /// <summary>
        /// lock for this path
        /// </summary>
        public object Lock { get; private set; } = new object();
    }
}