using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data.Embeds {
    public class EmbedImage {

        /// <summary>
        /// source url of image (only supports http(s) and attachments)
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// a proxied url of the image
        /// </summary>
        [JsonKey("proxy_url")]
        public string ProxyURL { get; set; }

        /// <summary>
        /// height of image
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// width of image
        /// </summary>
        public int Width { get; set; }
    }
}