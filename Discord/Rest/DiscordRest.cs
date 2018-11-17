using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using NightlyCode.Discord.Data;
using NightlyCode.Discord.Data.Channels;
using NightlyCode.Japi.Json;

namespace NightlyCode.Discord.Rest {

    /// <summary>
    /// rest api of discord
    /// </summary>
    public class DiscordRest {
        const string url = "https://discordapp.com/api";

        readonly bool botaccess;
        readonly string token;

        readonly object limiterlock = new object();
        readonly Dictionary<string, RequestLimit> limiters = new Dictionary<string, RequestLimit>();
         
        /// <summary>
        /// creates a new <see cref="DiscordRest"/> access
        /// </summary>
        /// <param name="token">token used to access discord</param>
        /// <param name="botaccess">whether access is a bot access</param>
        public DiscordRest(string token, bool botaccess) {
            this.botaccess = botaccess;
            this.token = token;
        }

        void EnterRequest(string route) {
            RequestLimit limit;
            lock (limiterlock) {
                if(!limiters.TryGetValue(route, out limit))
                    limiters[route] = limit = new RequestLimit {
                        Limit = int.MaxValue,
                        Remaining = int.MaxValue,
                        Reset = DateTime.Now
                    };

                Monitor.Enter(limit.Lock);
            }

            if(limit.Remaining == 0 && DateTime.Now < limit.Reset) {
                TimeSpan timetoreset = limit.Reset - DateTime.Now;
                if(timetoreset.TotalSeconds > 0.0)
                    Thread.Sleep(timetoreset);
            }
        }

        void ExitRequest(string route) {
            lock(limiterlock)
                Monitor.Exit(limiters[route].Lock);
        }

        T Request<T>(string route, string endpoint, params Parameter[] parameters) {
            return Request<T>(route, endpoint, null, parameters);
        }

        T Request<T>(string route, string endpoint, string post, params Parameter[] parameters) {
            try {
                return RequestInternal<T>(route, endpoint, post, parameters);
            }
            catch(RateLimitException) {

            }

            return default(T);
        }

        T RequestInternal<T>(string route, string endpoint, string post, params Parameter[] parameters) {
            EnterRequest(route);
            try {
                using(WebClient wc = new WebClient()) {
                    wc.Headers.Add("User-Agent", "StreamRC (http://www.nightlycode.de, v0.2)");
                    //wc.Headers.Add("Client-ID", clientid);
                    wc.Headers.Add("Authorization", $"{(botaccess ? "Bot" : "Bearer")} {token}");

                    if(parameters != null)
                        foreach(Parameter parameter in parameters)
                            wc.QueryString.Add(parameter.Key, parameter.Value);

                    string response;
                    if (!string.IsNullOrEmpty(post)) {
                        wc.Headers[HttpRequestHeader.Accept] = "application/json";
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                        response = wc.UploadString($"{url}/{route}/{endpoint}", post);
                    }
                    else response = wc.DownloadString($"{url}/{route}/{endpoint}");

                    ParseRateLimits(route, wc.ResponseHeaders);

                    return JSON.Read<T>(response);
                }
            }
            catch(WebException e) {
                if(e.Response is HttpWebResponse response) {
                    if((int)response.StatusCode == 429) {
                        RateLimitError error = JSON.Read<RateLimitError>(response.GetResponseStream());
                        Logger.Warning(this, $"{response.StatusCode}", error.Message);

                        if (error.Global) {
                            lock(limiterlock) {
                                Thread.Sleep(error.RetryAfter);
                            }
                        }
                        else {
                            limiters[route].Remaining = 0;
                            limiters[route].Reset = DateTime.Now + TimeSpan.FromMilliseconds(error.RetryAfter);
                        }
                    }
                    else {
                        RequestError error = JSON.Read<RequestError>(response.GetResponseStream());
                        Logger.Warning(this, $"{response.StatusCode}", error.Message);
                    }
                    ParseRateLimits(route, response.Headers);
                }

                throw new RateLimitException("Rate limit was hit. Limiters should have been updated, so an immediate retry will sleep until rate limit is supposed to be reset.");
            }
            finally {
                ExitRequest(route);
            }
        }

        void ParseRateLimits(string route, WebHeaderCollection headers) {
            RequestLimit requestlimit = limiters[route];

            string headervalue = headers["X-RateLimit-Limit"];
            if (!string.IsNullOrEmpty(headervalue))
            {
                int.TryParse(headervalue, out var limit);
                requestlimit.Limit = limit;
            }

            headervalue = headers["X-RateLimit-Remaining"];
            if (!string.IsNullOrEmpty(headervalue))
            {
                int.TryParse(headervalue, out var remaining);
                requestlimit.Remaining = remaining;
            }

            headervalue = headers["X-RateLimit-Reset"];
            if (!string.IsNullOrEmpty(headervalue))
            {
                int.TryParse(headervalue, out var seconds);
                requestlimit.Reset = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc) + TimeSpan.FromSeconds(seconds);
            }
        }

        /// <summary>
        /// Get a channel by ID. Returns a <see cref="Channel"/> object.
        /// </summary>
        /// <param name="channelid">id of channel</param>
        /// <returns>channel object</returns>
        public Channel GetChannel(string channelid) {
            return Request<Channel>("channels", channelid);
        }

        public Message[] GetMessages(string channelid, GetMessagesParameter parameters) {
            return Request<Message[]>("channels", $"{channelid}/messages", parameters?.CreateParameters().ToArray());
        }

        /// <summary>
        /// Post a message to a guild text or DM channel.
        /// </summary>
        /// <param name="channelid">id of channel</param>
        /// <param name="message">message to post</param>
        public void CreateMessage(string channelid, CreateMessageBody message) {
            Request<Message>("channels", $"{channelid}/messages", JSON.WriteString(message));
        }
    }
}