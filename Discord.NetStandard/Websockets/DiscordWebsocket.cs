using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using NightlyCode.Discord.Data;
using NightlyCode.Discord.Data.Channels;
using NightlyCode.Japi.Extensions;
using NightlyCode.Japi.Json;
using Websocket.Client;

namespace NightlyCode.Discord.Websockets
{
    public class DiscordWebsocket
    {
        readonly string token;
        readonly WebsocketClient socket = new WebsocketClient(new Uri("wss://gateway.discord.gg/?v=8&encoding=json"));
        int sequence = -1;

        int disconnects;
        int missedheartbeats;

        bool heartbeatacknowledged = true;

        int gatewayversion;
        string sessionid;

        readonly object connectionlock = new object();

        CancellationTokenSource heartbeatsource;
        
        /// <summary>
        /// websocket which connects to discord
        /// </summary>
        /// <param name="token">token identifying the user or bot</param>
        public DiscordWebsocket(string token)
        {
            this.token = token;
            socket.MessageReceived.Subscribe(OnMessage);
            socket.DisconnectionHappened.Subscribe(OnClosed);
            socket.ReconnectionHappened.Subscribe(info => Logger.Info(this, "Reconnected"));
        }

        /// <summary>
        /// determines whether the socket is connected
        /// </summary>
        public bool IsConnected { get; private set; }

        void OnClosed(DisconnectionInfo info) {
            Logger.Warning(this, $"Connection closed: {info.Type}, {info.CloseStatusDescription}", info.Exception?.ToString());
            OnDisconnected(info.Type != DisconnectionType.ByUser);
        }

        public int GatewayVersion => gatewayversion;

        /// <summary>
        /// triggered when discord is connected
        /// </summary>
        public event Action Connected;

        /// <summary>
        /// triggered when client was disconnected
        /// </summary>
        public event Action Disconnected;

        /// <summary>
        /// Sent when a new channel is created, relevant to the current user
        /// </summary>
        public event Action<Channel> ChannelCreated;

        /// <summary>
        /// Sent when a channel is updated
        /// </summary>
        public event Action<Channel> ChannelUpdated;

        /// <summary>
        /// Sent when a channel relevant to the current user is deleted
        /// </summary>
        public event Action<Channel> ChannelDeleted;

        /// <summary>
        /// Sent when a message is pinned or unpinned in a text channel
        /// </summary>
        public event Action<string> ChannelPinUpdated;

        /// <summary>
        /// triggered when a message in a joined channel was created
        /// </summary>
        public event Action<Message> MessageCreated;

        /// <summary>
        /// Sent when a message is updated
        /// </summary>
        /// <remarks>
        /// Unlike creates, message updates may contain only a subset of the full message object payload (but will always contain an id and channel_id).
        /// </remarks>
        public event Action<Message> MessageUpdated;

        /// <summary>
        /// Sent when a message is deleted
        /// </summary>
        public event Action<string, string> MessageDeleted;

        /// <summary>
        /// Sent when multiple messages are deleted at once
        /// </summary>
        public event Action<string, string[]> MessagesDeleted;

        /// <summary>
        /// This event can be sent in three different scenarios:
        ///     1. When a user is initially connecting, to lazily load and backfill information for all unavailable guilds 
        ///     2. When a Guild becomes available again to the client
        ///     3. When the current user joins a new Guild.
        /// </summary>
        public event Action<Guild> GuildCreated;

        /// <summary>
        /// Sent when a guild is updated
        /// </summary>
        public event Action<Guild> GuildUpdated;

        /// <summary>
        /// Sent when a guild becomes unavailable during a guild outage, or when the user leaves or is removed from a guild
        /// </summary>
        public event Action<UnavailableGuild> GuildDeleted;

        /// <summary>
        /// Sent in response to Guild Request Members
        /// </summary>
        public event Action<string, GuildMember[]> GuildMembersChunk;

        /// <summary>
        /// Sent when a guild integration is updated
        /// </summary>
        public event Action<string> GuildIntegrationsUpdated;

        /// <summary>
        /// Sent when a user is banned from a guild
        /// </summary>
        public event Action<GuildUser> UserBanned;

        /// <summary>
        /// Sent when a user is unbanned from a guild
        /// </summary>
        public event Action<GuildUser> UserUnbanned;

        /// <summary>
        /// Sent when a guild's emojis have been updated.
        /// </summary>
        public event Action<string, Emoji[]> EmojisUpdated;

        /// <summary>
        /// Sent when a new user joins a guild
        /// </summary>
        public event Action<string, GuildMember> GuildMemberAdded;

        /// <summary>
        /// Sent when a user is removed from a guild (leave/kick/ban).
        /// </summary>
        public event Action<string, User> GuildMemberRemoved;

        /// <summary>
        /// Sent when a guild member is updated
        /// </summary>
        public event Action<string, Role[], User, string> GuildMemberUpdated;

        /// <summary>
        /// Sent when a guild role is created.
        /// </summary>
        public event Action<string, Role> GuildRoleCreated;

        /// <summary>
        /// Sent when a guild role is updated.
        /// </summary>
        public event Action<string, Role> GuildRoleUpdated;

        /// <summary>
        /// Sent when a guild role is deleted.
        /// </summary>
        public event Action<string, string> GuildRoleDeleted;

        /// <summary>
        /// Sent when a user adds a reaction to a message
        /// </summary>
        public event Action<string, string, string, Emoji> MessageReactionAdded;

        /// <summary>
        /// Sent when a user removes a reaction from a message
        /// </summary>
        public event Action<string, string, string, Emoji> MessageReactionRemoved;

        /// <summary>
        /// Sent when a user explicitly removes all reactions from a message
        /// </summary>
        public event Action<string, string> MessageReactionRemovedAll;

        /// <summary>
        /// This event is sent when a user's presence is updated for a guild.
        /// </summary>
        public event Action<PresenceUpdate> PresenceUpdated;

        /// <summary>
        /// Sent when a user starts typing in a channel
        /// </summary>
        public event Action<string, string, DateTime> TypingStarted;

        /// <summary>
        /// Sent when properties about the user change
        /// </summary>
        public event Action<User> UserUpdated;

        /// <summary>
        /// Sent when someone joins/leaves/moves voice channels
        /// </summary>
        public event Action<VoiceState> VoiceStateUpdated;

        /// <summary>
        /// Sent when a guild's voice server is updated
        /// </summary>
        public event Action<string, string, string> VoiceServerUpdated;

        /// <summary>
        /// Sent when a guild channel's webhook is created, updated, or deleted
        /// </summary>
        public event Action<string, string> WebhooksUpdated;

        /// <summary>
        /// connects the websocket
        /// </summary>
        public void Connect() {
            new Task(InternalConnect).Start();
        }

        /// <summary>
        /// disconnects this socket
        /// </summary>
        public void Disconnect() {
            Disconnect(0xFFFF, "Manual Disconnect", false);
        }

        void InternalConnect() {
            Logger.Info(this, "Connecting to discord");
            try
            {
                socket.Start();
            }
            catch (Exception e) {
                Logger.Error(this, "Error connecting to discord", e);
                OnDisconnected();
            }
        }

        void Disconnect(ushort code, string reason, bool reconnect) {
            Logger.Warning(this, $"Disconnecting: {code}", reason);
            socket.Stop((WebSocketCloseStatus)code, reason);
            OnDisconnected(reconnect);
        }

        void OnConnected() {
            Logger.Info(this, "Connected");
            lock(connectionlock) {
                if(IsConnected)
                    return;

                IsConnected = true;
                disconnects = 0;
                Connected?.Invoke();
            }
        }

        void OnDisconnected(bool reconnect=true) {
            lock(connectionlock) {
                if(!IsConnected)
                    return;

                IsConnected = false;

                if (heartbeatsource != null) {
                    heartbeatsource.Cancel();
                    heartbeatsource = null;
                }

                heartbeatacknowledged = false;
                
                ++disconnects;
                if(reconnect && disconnects < 5) {
                    double time = Math.Pow(2, disconnects);
                    Logger.Info(this, $"Reconnecting in {time:F} seconds.");
                    Thread.Sleep(TimeSpan.FromSeconds(time));
                    Connect();
                }
                else Disconnected?.Invoke();
            }
        }

        async Task OnHeartbeat(int milliseconds, CancellationToken cancellationtoken) {
            //await Task.Delay(milliseconds*0.5, cancellationtoken);
            while (!cancellationtoken.IsCancellationRequested) {
                if (!heartbeatacknowledged) {
                    ++missedheartbeats;
                    if (missedheartbeats >= 3) {
                        Disconnect(1020, "To many heartbeats not acknowledged", true);
                        return;
                    }
                }

                if (IsConnected) {
                    heartbeatacknowledged = false;

                    JsonObject heartbeat = new JsonObject {
                        ["op"] = new JsonValue(1)
                    };
                    if (sequence > -1)
                        heartbeat["d"] = new JsonValue(sequence);

                    SendPayload(Opcode.Heartbeat, heartbeat);
                }

                await Task.Delay(milliseconds, cancellationtoken);
            }
        }

        void SendPayload(Opcode op, JsonObject data)
        {
            using (MemoryStream ms = new MemoryStream()) {
                JSON.WriteNodeToStream(new JsonObject {
                    ["op"] = new JsonValue(op),
                    ["d"] = data
                }, ms);
                socket.Send(ms.ToArray());
            }
        }

        void OnMessage(ResponseMessage message) {
            JsonNode payload = null;
            try {
                switch (message.MessageType) {
                    case WebSocketMessageType.Binary:
                        using (MemoryStream ms = new MemoryStream(message.Binary))
                            payload = JSON.ReadNodeFromStream(ms);
                        break;
                    case WebSocketMessageType.Text:
                        payload = JSON.ReadNodeFromString(message.Text);
                        break;
                    default:
                        return;
                }

                Logger.Info(this, $"payload: {payload}");
                switch (payload.SelectValue<Opcode>("op")) {
                    case Opcode.Dispatch:
                        OnEvent(payload.SelectValue<string>("t"), payload.SelectValue<int>("s"), payload.SelectNode("d"));
                        break;
                    case Opcode.Hello:
                        OnHello(payload.SelectSingle<int>("d/heartbeat_interval"));
                        break;
                    case Opcode.HeartbeatAck:
                        missedheartbeats = 0;
                        heartbeatacknowledged = true;
                        break;
                    case Opcode.InvalidSession:
                        socket.Reconnect();
                        break;
                    default:
                        Logger.Warning(this, $"Unhandled discord opcode: {payload}");
                        break;
                }
            }
            catch (Exception ex) {
                Logger.Error(this, $"Unable to handle discord message: {payload}", ex);
            }
        }

        void OnHello(int heartbeat)
        {
            if(!string.IsNullOrEmpty(sessionid)) {
                JsonObject resume = new JsonObject {
                    ["token"] = new JsonValue(token),
                    ["session_id"] = new JsonValue(sessionid),
                    ["seq"] = new JsonValue(sequence)
                };
                SendPayload(Opcode.Resume, resume);
            }
            else {
                JsonObject identify = new JsonObject
                {
                    ["token"] = new JsonValue(token),
                    ["intents"]=new JsonValue(513),
                    ["properties"] = new JsonObject
                    {
                        ["$os"] = new JsonValue("linux"),
                        ["$browser"] = new JsonValue("Gangolf.DiscordService"),
                        ["$device"] = new JsonValue("Gangolf.DiscordService")
                    },
                    ["presence"] = new JsonObject
                    {
                        ["status"] = new JsonValue("lurking")
                    }
                };
                SendPayload(Opcode.Identify, identify);
            }

            missedheartbeats = 0;
            heartbeatacknowledged = true;
            if (heartbeatsource != null)
                heartbeatsource.Cancel();
            heartbeatsource = new CancellationTokenSource();
                
            Task.Run(() => OnHeartbeat(heartbeat, heartbeatsource.Token));
        }

        void OnEvent(string eventname, int seq, JsonNode data)
        {
            sequence = seq;
            switch (eventname)
            {
                case "READY":
                    gatewayversion = data.SelectValue<int>("v");
                    sessionid = data.SelectValue<string>("session_id");
                    OnConnected();
                    break;
                case "RESUMED":
                    OnConnected();
                    break;
                case "INVALID_SESSION":
                    Disconnect(1021, "Authentication failure", /*data.SelectValue<bool>("d")*/false);
                    break;
                case "CHANNEL_CREATE":
                    ChannelCreated?.Invoke(JSON.Serializer.Read<Channel>(data));
                    break;
                case "CHANNEL_UPDATE":
                    ChannelUpdated?.Invoke(JSON.Serializer.Read<Channel>(data));
                    break;
                case "CHANNEL_DELETE":
                    ChannelDeleted?.Invoke(JSON.Serializer.Read<Channel>(data));
                    break;
                case "CHANNEL_PINS_UPDATED":
                    ChannelPinUpdated?.Invoke(data.SelectValue<string>("channel_id"));
                    break;
                case "MESSAGE_CREATE":
                    MessageCreated?.Invoke(JSON.Serializer.Read<Message>(data));
                    break;
                case "MESSAGE_UPDATE":
                    MessageUpdated?.Invoke(JSON.Serializer.Read<Message>(data));
                    break;
                case "MESSAGE_DELETE":
                    MessageDeleted?.Invoke(data.SelectValue<string>("channel_id"), data.SelectValue<string>("id"));
                    break;
                case "MESSAGE_DELETE_BULK":
                    MessagesDeleted?.Invoke(data.SelectValue<string>("channel_id"), data.SelectValue<string[]>("ids"));
                    break;
                case "GUILD_CREATE":
                    GuildCreated?.Invoke(JSON.Serializer.Read<Guild>(data));
                    break;
                case "GUILD_UPDATE":
                    GuildUpdated?.Invoke(JSON.Serializer.Read<Guild>(data));
                    break;
                case "GUILD_DELETE":
                    GuildDeleted?.Invoke(JSON.Serializer.Read<UnavailableGuild>(data));
                    break;
                case "GUILD_BAN_ADD":
                    UserBanned?.Invoke(JSON.Serializer.Read<GuildUser>(data));
                    break;
                case "GUILD_BAN_REMOVED":
                    UserUnbanned?.Invoke(JSON.Serializer.Read<GuildUser>(data));
                    break;
                case "GUILD_EMOJIS_UPDATE":
                    EmojisUpdated?.Invoke(data.SelectValue<string>("guild_id"), JSON.Serializer.Read<Emoji[]>(data.SelectNode("emojis")));
                    break;
                case "GUILD_INTEGRATIONS_UPDATE":
                    GuildIntegrationsUpdated?.Invoke(data.SelectValue<string>("guild_id"));
                    break;
                case "GUILD_MEMBER_ADD":
                    GuildMemberAdded?.Invoke(data.SelectValue<string>("guild_id"), JSON.Serializer.Read<GuildMember>(data));
                    break;
                case "GUILD_MEMBER_REMOVE":
                    GuildMemberRemoved?.Invoke(data.SelectValue<string>("guild_id"), JSON.Serializer.Read<User>(data));
                    break;
                case "GUILD_MEMBER_UPDATE":
                    GuildMemberUpdated?.Invoke(data.SelectValue<string>("guild_id"), JSON.Serializer.Read<Role[]>(data.SelectNode("roles")), JSON.Serializer.Read<User>(data.SelectNode("user")), data.SelectValue<string>("nick"));
                    break;
                case "GUILD_MEMBERS_CHUNK":
                    GuildMembersChunk?.Invoke(data.SelectValue<string>("guild_id"), JSON.Serializer.Read<GuildMember[]>(data.SelectNode("members")));
                    break;
                case "GUILD_ROLE_CREATE":
                    GuildRoleCreated?.Invoke(data.SelectValue<string>("guild_id"), JSON.Serializer.Read<Role>(data.SelectNode("role")));
                    break;
                case "GUILD_ROLE_UPDATE":
                    GuildRoleUpdated?.Invoke(data.SelectValue<string>("guild_id"), JSON.Serializer.Read<Role>(data.SelectNode("role")));
                    break;
                case "GUILD_ROLE_DELETE":
                    GuildRoleDeleted?.Invoke(data.SelectValue<string>("guild_id"), data.SelectValue<string>("role_id"));
                    break;
                case "MESSAGE_REACTION_ADD":
                    MessageReactionAdded?.Invoke(data.SelectValue<string>("user_id"), data.SelectValue<string>("channel_id"), data.SelectValue<string>("message_id"), JSON.Serializer.Read<Emoji>(data.SelectNode("emoji")));
                    break;
                case "MESSAGE_REACTION_REMOVE":
                    MessageReactionRemoved?.Invoke(data.SelectValue<string>("user_id"), data.SelectValue<string>("channel_id"), data.SelectValue<string>("message_id"), JSON.Serializer.Read<Emoji>(data.SelectNode("emoji")));
                    break;
                case "MESSAGE_REACTION_REMOVE_ALL":
                    MessageReactionRemovedAll?.Invoke(data.SelectValue<string>("channel_id"), data.SelectValue<string>("message_id"));
                    break;
                case "PRESENCE_UPDATE":
                    PresenceUpdated?.Invoke(JSON.Serializer.Read<PresenceUpdate>(data));
                    break;
                case "TYPING_START":
                    TypingStarted?.Invoke(data.SelectValue<string>("guild_id"), data.SelectValue<string>("user_id"), data.SelectValue<int>("timestamp").ToDateTime());
                    break;
                case "USER_UPDATE":
                    UserUpdated?.Invoke(JSON.Serializer.Read<User>(data));
                    break;
                case "VOICE_STATE_UPDATE":
                    VoiceStateUpdated?.Invoke(JSON.Serializer.Read<VoiceState>(data));
                    break;
                case "VOICE_SERVER_UPDATE":
                    VoiceServerUpdated?.Invoke(data.SelectValue<string>("token"), data.SelectValue<string>("guild_id"), data.SelectValue<string>("endpoint"));
                    break;
                case "WEBHOOKS_UPDATE":
                    WebhooksUpdated?.Invoke(data.SelectValue<string>("guild_id"), data.SelectValue<string>("channel_id"));
                    break;
                default:
                    Logger.Warning(this, $"'{eventname}' event not handled", data.ToString());
                    break;
            }
        }
    }
}