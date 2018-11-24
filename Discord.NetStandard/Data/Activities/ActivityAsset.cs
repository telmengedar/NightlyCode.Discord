using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data.Activities {
    public class ActivityAsset {

        /// <summary>
        /// the id for a large asset of the activity, usually a snowflake
        /// </summary>
        [JsonKey("large_image")]
        public string LargeImage { get; set; }

        /// <summary>
        /// text displayed when hovering over the large image of the activity
        /// </summary>
        [JsonKey("large_text")]
        public string LargeText { get; set; }

        /// <summary>
        /// the id for a small asset of the activity, usually a snowflake
        /// </summary>
        [JsonKey("small_image")]
        public string SmallImage { get; set; }

        /// <summary>
        /// text displayed when hovering over the small image of the activity
        /// </summary>
        [JsonKey("small_text")]
        public string SmallText { get; set; }
    }
}