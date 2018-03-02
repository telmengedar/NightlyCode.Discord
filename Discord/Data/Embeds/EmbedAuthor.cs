using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data.Embeds {
    public class EmbedAuthor {

        /// <summary>
        /// name of author
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// url of author
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// url of author icon (only supports http(s) and attachments)
        /// </summary>
        [JsonKey("icon_url")]
        public string IconURL { get; set; }

        /// <summary>
        /// a proxied url of author icon
        /// </summary>
        [JsonKey("proxy_icon_url")]
        public string ProxyIconURL { get; set; }
    }
}