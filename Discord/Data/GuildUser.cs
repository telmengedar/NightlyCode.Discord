using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data {
    public class GuildUser : User {

        /// <summary>
        /// id of the guild
        /// </summary>
        [JsonKey("guild_id")]
        public string GuildID { get; set; }
    }
}