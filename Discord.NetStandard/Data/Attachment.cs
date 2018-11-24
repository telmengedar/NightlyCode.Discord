using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data {
    public class Attachment {

        /// <summary>
        /// attachment id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// name of file attached
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// size of file in bytes
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// source url of file
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// a proxied url of file
        /// </summary>
        [JsonKey("proxy_url")]
        public string ProxyURL { get; set; }

        /// <summary>
        /// height of file (if image)
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// width of file (if image)
        /// </summary>
        public int Width { get; set; }
    }
}