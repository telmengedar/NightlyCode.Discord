namespace NightlyCode.Discord.Data.Activities {
    public class ActivityTimestamps {

        /// <summary>
        /// unix time (in milliseconds) of when the activity started
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// unix time (in milliseconds) of when the activity ends
        /// </summary>
        public int End { get; set; }
    }
}