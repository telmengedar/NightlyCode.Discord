using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data {
    public class VoiceState {

        /// <summary>
        /// the guild id this voice state is for
        /// </summary>
        [JsonKey("guild_id")]
        public string GuildID { get; set; }

        /// <summary>
        /// the channel id this user is connected to
        /// </summary>
        [JsonKey("channel_id")]
        public string ChannelID { get; set; }

        /// <summary>
        /// the user id this voice state is for
        /// </summary>
        [JsonKey("user_id")]
        public string UserID { get; set; }

        /// <summary>
        /// the session id for this voice state
        /// </summary>
        [JsonKey("session_id")]
        public string SessionID { get; set; }

        /// <summary>
        /// whether this user is deafened by the server
        /// </summary>
        public bool Deaf { get; set; }

        /// <summary>
        /// whether this user is muted by the server
        /// </summary>
        public bool Mute { get; set; }

        /// <summary>
        /// whether this user is locally deafened
        /// </summary>
        [JsonKey("self_deaf")]
        public bool SelfDeaf { get; set; }

        /// <summary>
        /// whether this user is locally muted
        /// </summary>
        [JsonKey("self_mute")]
        public bool SelfMute { get; set; }

        /// <summary>
        /// whether this user is muted by the current user
        /// </summary>
        public bool Suppress { get; set; }
    }
}