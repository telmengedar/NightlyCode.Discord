using NightlyCode.Discord.Data.Channels;
using NightlyCode.Discord.Websockets;
using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Data {
    public class Guild {

        /// <summary>
        /// guild id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// guild name (2-100 characters)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// icon hash
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// splash hash
        /// </summary>
        public string Splash { get; set; }

        /// <summary>
        /// whether or not the user is the owner of the guild
        /// </summary>
        public bool Owner { get; set; }

        /// <summary>
        /// id of owner
        /// </summary>
        [JsonKey("owner_id")]
        public string OwnerID { get; set; }

        /// <summary>
        /// total permissions for the user in the guild (does not include channel overrides)
        /// </summary>
        public int Permissions { get; set; }

        /// <summary>
        /// voice region id for the guild
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// id of afk channel
        /// </summary>
        [JsonKey("afk_channel_id")]
        public string AfkChannelID { get; set; }

        /// <summary>
        /// afk timeout in seconds
        /// </summary>
        [JsonKey("afk_timeout")]
        public int AfkTimeout { get; set; }

        /// <summary>
        /// is this guild embeddable (e.g. widget)
        /// </summary>
        [JsonKey("embed_enable")]
        public bool EmbedEnable { get; set; }

        /// <summary>
        /// id of embedded channel
        /// </summary>
        [JsonKey("embed_channel_id")]
        public string EmbedChannelID { get; set; }

        /// <summary>
        /// verification level required for the guild
        /// </summary>
        [JsonKey("verification_level")]
        public int VerificationLevel { get; set; }

        /// <summary>
        /// default message notifications level
        /// </summary>
        [JsonKey("default_message_notifications")]
        public int DefaultMessageNotifications { get; set; }

        /// <summary>
        /// explicit content filter level
        /// </summary>
        [JsonKey("explicit_content_filter")]
        public int ExplicitContentFilter { get; set; }

        /// <summary>
        /// roles in the guild
        /// </summary>
        public Role[] Roles { get; set; }

        /// <summary>
        /// custom guild emojis
        /// </summary>
        public Emoji[] Emojis { get; set; }

        /// <summary>
        /// enabled guild features
        /// </summary>
        public string[] Features { get; set; }

        /// <summary>
        /// required MFA level for the guild
        /// </summary>
        [JsonKey("mfa_level")]
        public int MfaLevel { get; set; }

        /// <summary>
        /// application id of the guild creator if it is bot-created
        /// </summary>
        [JsonKey("application_id")]
        public string ApplicationID { get; set; }

        /// <summary>
        /// whether or not the server widget is enabled
        /// </summary>
        [JsonKey("widget_enabled")]
        public bool WidgetEnabled { get; set; }

        /// <summary>
        /// the channel id for the server widget
        /// </summary>
        [JsonKey("widget_channel_id")]
        public string WidgetChannelID { get; set; }

        /// <summary>
        /// the id of the channel to which system messages are sent
        /// </summary>
        [JsonKey("system_channel_id")]
        public string SystemChannelID { get; set; }

        /// <summary>
        /// when this guild was joined at
        /// </summary>
        /// <remarks>This field is only sent within the <see cref="DiscordWebsocket.GuildCreated"/> event</remarks>
        [JsonKey("joined_at")]
        public string JoinedAt { get; set; }

        /// <summary>
        /// whether this is considered a large guild
        /// </summary>
        /// <remarks>This field is only sent within the <see cref="DiscordWebsocket.GuildCreated"/> event</remarks>
        public bool Large { get; set; }

        /// <summary>
        /// is this guild unavailable
        /// </summary>
        /// <remarks>This field is only sent within the <see cref="DiscordWebsocket.GuildCreated"/> event</remarks>
        public bool Unavailable { get; set; }

        /// <summary>
        /// total number of members in this guild
        /// </summary>
        /// <remarks>This field is only sent within the <see cref="DiscordWebsocket.GuildCreated"/> event</remarks>
        [JsonKey("member_count")]
        public int MemberCount { get; set; }

        /// <summary>
        /// (without the guild_id key)
        /// </summary>
        /// <remarks>This field is only sent within the <see cref="DiscordWebsocket.GuildCreated"/> event</remarks>
        public VoiceState[] VoiceStates { get; set; }

        /// <summary>
        /// users in the guild
        /// </summary>
        /// <remarks>This field is only sent within the <see cref="DiscordWebsocket.GuildCreated"/> event</remarks>
        public GuildMember[] Members { get; set; }

        /// <summary>
        /// channels in the guild
        /// </summary>
        /// <remarks>This field is only sent within the <see cref="DiscordWebsocket.GuildCreated"/> event</remarks>
        public Channel[] Channels { get; set; }

        /// <summary>
        /// presences of the users in the guild
        /// </summary>
        /// <remarks>This field is only sent within the <see cref="DiscordWebsocket.GuildCreated"/> event</remarks>
        public PresenceUpdate[] Presences { get; set; }
    }
}