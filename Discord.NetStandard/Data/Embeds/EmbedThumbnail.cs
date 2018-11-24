using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data.Embeds {
    public class EmbedThumbnail {

        /// <summary>
        /// source url of thumbnail (only supports http(s) and attachments)
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// a proxied url of the thumbnail
        /// </summary>
        [JsonKey("proxy_url")]
        public string ProxyURL { get; set; }

        /// <summary>
        /// height of thumbnail
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// width of thumbnail
        /// </summary>
        public int Width { get; set; }
    }
}