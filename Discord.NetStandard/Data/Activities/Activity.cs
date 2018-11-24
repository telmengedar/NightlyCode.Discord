using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data.Activities {
    public class Activity {

        /// <summary>
        /// the activity's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// activity type
        /// </summary>
        public ActivityType Type { get; set; }

        /// <summary>
        /// stream url, is validated when type is 1
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// unix timestamps for start and/or end of the game
        /// </summary>
        public ActivityTimestamps Timestamps { get; set; }

        /// <summary>
        /// application id for the game
        /// </summary>
        [JsonKey("application_id")]
        public string ApplicationID { get; set; }

        /// <summary>
        /// what the player is currently doing
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// the user's current party status
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// information for the current party of the player
        /// </summary>
        public ActivityParty Party { get; set; }

        /// <summary>
        /// images for the presence and their hover texts
        /// </summary>
        public ActivityAsset Assets { get; set; }
    }
}