using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data {
    public class Emoji {

        /// <summary>
        /// emoji id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// emoji name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// roles this emoji is whitelisted to
        /// </summary>
        public Role[] Roles { get; set; }

        /// <summary>
        /// user that created this emoji
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// whether this emoji must be wrapped in colons
        /// </summary>
        [JsonKey("require_colons")]
        public bool RequireColons { get; set; }

        /// <summary>
        /// whether this emoji is managed
        /// </summary>
        public bool Managed { get; set; }

        /// <summary>
        /// whether this emoji is animated
        /// </summary>
        public bool Animated { get; set; }
    }
}