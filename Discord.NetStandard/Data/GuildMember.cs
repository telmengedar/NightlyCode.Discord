using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data {
    public class GuildMember {

        /// <summary>
        /// user object
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// this users guild nickname (if one is set)
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// array of role object ids
        /// </summary>
        public Role[] Roles { get; set; }

        /// <summary>
        /// when the user joined the guild
        /// </summary>
        [JsonKey("joined_at")]
        public string JoinedAt { get; set; }

        /// <summary>
        /// if the user is deafened
        /// </summary>
        public bool Deaf { get; set; }

        /// <summary>
        /// if the user is muted
        /// </summary>
        public bool Mute { get; set; }
    }
}