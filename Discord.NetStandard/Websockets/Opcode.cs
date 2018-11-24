namespace NightlyCode.Discord.Websockets
{
    public enum Opcode
    {

        /// <summary>
        /// dispatches an event
        /// </summary>
        Dispatch = 0,

        /// <summary>
        /// used for ping checking
        /// </summary>
        Heartbeat = 1,

        /// <summary>
        /// used for client handshake
        /// </summary>
        Identify = 2,

        /// <summary>
        /// used to update the client status
        /// </summary>
        StatusUpdate = 3,

        /// <summary>
        /// used to join/move/leave voice channels
        /// </summary>
        VoiceStateUpdate = 4,

        /// <summary>
        /// used for voice ping checking
        /// </summary>
        VoiceServerPing = 5,

        /// <summary>
        /// used to resume a closed connection
        /// </summary>
        Resume = 6,

        /// <summary>
        /// used to tell clients to reconnect to the gateway
        /// </summary>
        Reconnect = 7,

        /// <summary>
        /// used to request guild members
        /// </summary>
        RequestGuildMembers = 8,

        /// <summary>
        /// used to notify client they have an invalid session id
        /// </summary>
        InvalidSession = 9,

        /// <summary>
        /// sent immediately after connecting, contains heartbeat and server debug information
        /// </summary>
        Hello = 10,

        /// <summary>
        /// sent immediately following a client heartbeat that was received
        /// </summary>
        HeartbeatAck = 11,

        /// <summary>
        /// We're not sure what went wrong. Try reconnecting?
        /// </summary>
        UnknownError = 4000,

        /// <summary>
        /// You sent an invalid Gateway opcode or an invalid payload for an opcode. Don't do that!
        /// </summary>
        UnknownOpcode = 4001,

        /// <summary>
        /// You sent an invalid payload to us. Don't do that!
        /// </summary>
        DecodeError = 4002,

        /// <summary>
        /// You sent us a payload prior to identifying.
        /// </summary>
        NotAuthenticated = 4003,

        /// <summary>
        /// The account token sent with your identify payload is incorrect.
        /// </summary>
        AuthenticationFailed = 4004,

        /// <summary>
        /// You sent more than one identify payload. Don't do that!
        /// </summary>
        AlreadyAuthenticated = 4005,

        /// <summary>
        /// The sequence sent when resuming the session was invalid. Reconnect and start a new session.
        /// </summary>
        InvalidSeq = 4007,

        /// <summary>
        /// Woah nelly! You're sending payloads to us too quickly. Slow it down!
        /// </summary>
        RateLimited = 4008,

        /// <summary>
        /// Your session timed out. Reconnect and start a new one.
        /// </summary>
        SessionTimedOut = 4009,

        /// <summary>
        /// You sent us an invalid shard when identifying.
        /// </summary>
        InvalidShard = 4010,

        /// <summary>
        /// The session would have handled too many guilds - you are required to shard your connection in order to connect.
        /// </summary>
        ShardingRequired = 4011
    }
}