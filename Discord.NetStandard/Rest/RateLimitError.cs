using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Rest {
    internal class RateLimitError : RequestError{

        [JsonKey("retry_after")]
        public int RetryAfter { get; set; }

        public bool Global { get; set; }
    }
}