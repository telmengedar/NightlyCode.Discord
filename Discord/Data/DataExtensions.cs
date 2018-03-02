using System;

namespace NightlyCode.Discord.Data {
    public static class DataExtensions {
        static DateTime UnixStart = new DateTime(1970, 1, 1);

        public static DateTime ToDateTime(this int seconds) {
            return UnixStart + TimeSpan.FromSeconds(seconds);
        }

        public static int ToUnixSeconds(this DateTime time) {
            return (int)(time - UnixStart).TotalSeconds;
        }
    }
}