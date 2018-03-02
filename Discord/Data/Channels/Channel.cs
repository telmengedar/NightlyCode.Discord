using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data.Channels {
    public class Channel {

        /// <summary>
        /// the id of this channel
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// the type of channel
        /// </summary>
        public ChannelType Type { get; set; }

        /// <summary>
        /// the id of the guild
        /// </summary>
        [JsonKey("guild_id")]
        public string GuildID { get; set; }

        /// <summary>
        /// sorting position of the channel
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// explicit permission overwrites for members and roles
        /// </summary>
        [JsonKey("permission_overwrites")]
        public Overwrite[] PermissionOverwrites { get; set; }

        /// <summary>
        /// the name of the channel (2-100 characters)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// the channel topic (0-1024 characters)
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// if the channel is nsfw
        /// </summary>
        public bool Nsfw { get; set; }

        /// <summary>
        /// the id of the last message sent in this channel (may not point to an existing or valid message)
        /// </summary>
        [JsonKey("last_message_id")]
        public string LastMessageID { get; set; }

        /// <summary>
        /// the bitrate (in bits) of the voice channel
        /// </summary>
        public int Bitrate { get; set; }

        /// <summary>
        /// the user limit of the voice channel
        /// </summary>
        [JsonKey("user_limit")]
        public int UserLimit { get; set; }

        /// <summary>
        /// the recipients of the DM
        /// </summary>
        public User[] Recipients { get; set; }

        /// <summary>
        /// icon hash
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// id of the DM creator
        /// </summary>
        [JsonKey("owner_id")]
        public string OwnerID { get; set; }

        /// <summary>
        /// application id of the group DM creator if it is bot-created
        /// </summary>
        [JsonKey("application_id")]
        public string ApplicationID { get; set; }

        /// <summary>
        /// id of the parent category for a channel
        /// </summary>
        [JsonKey("parent_id")]
        public string ParentID { get; set; }

        /// <summary>
        /// when the last pinned message was pinned
        /// </summary>
        [JsonKey("last_pin_timestamp")]
        public string LastPinTimestamp { get; set; }
    }
}