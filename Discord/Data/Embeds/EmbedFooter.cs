using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data.Embeds {
    public class EmbedFooter {

        /// <summary>
        /// footer text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// url of footer icon (only supports http(s) and attachments)
        /// </summary>
        [JsonKey("icon_url")]
        public string IconURL { get; set; }

        /// <summary>
        /// a proxied url of footer icon
        /// </summary>
        [JsonKey("proxy_icon_url")]
        public string ProxyIconURL { get; set; }
    }
}