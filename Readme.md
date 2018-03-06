# NightlyCode.Discord

Provides access to Discord. The websocket connection is more or less complete, REST though is lacking a lot of implementation.

## Dependencies

- [NightlyCode.Core](https://github.com/telmengedar/NightlyCode.Core) for data conversion
- [NightlyCode.Japi](https://github.com/telmengedar/japi) for JSON deserialization
- [websocket-sharp](https://github.com/sta/websocket-sharp) for websocket connections

## Websocket connection

Most functionality of discord requires a websocket connection. Some REST calls require a previous websocket authentication aswell before calls are accepted by discord servers.

### Connect Discord using websockets

Connection to Discord doesn't require much explanation. The following code connects to Discord using the websockets-API.

```

DiscordWebsocket websocket = new DiscordWebsocket(DiscordConstants.BotToken);
websocket.Connect();

```

Incoming websocket events are then routed to corresponding events of the DiscordWebsocket class. Subscribe to the events to get informed about whatever is happening at connected servers.

## REST Api

Only some methods of the REST API are implemented yet. Using the REST API you can get information about a channel or send messages to it. Rate-Limiting headers are processed and the API seems to react to them quite well.