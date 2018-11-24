using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data {
    public class User {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Discriminator { get; set; }
        public string Avatar { get; set; }
        public bool Bot { get; set; }

        [JsonKey("mfa_enabled")]
        public bool MfaEnabled { get; set; }

        public bool Verified { get; set; }

        public string Email { get; set; }
    }
}