using NightlyCode.Discord.Data.Activities;
using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data {
    public class PresenceUpdate {

        /// <summary>
        /// the user presence is being updated for
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// roles this user is in
        /// </summary>
        public string[] Roles { get; set; }

        /// <summary>
        /// null, or the user's current activity
        /// </summary>
        public Activity Game { get; set; }

        /// <summary>
        /// id of the guild
        /// </summary>
        [JsonKey("guild_id")]
        public string GuildID { get; set; }

        /// <summary>
        /// either "idle", "dnd", "online", or "offline"
        /// </summary>
        public Status Status { get; set; }
    }
}