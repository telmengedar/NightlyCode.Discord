namespace NightlyCode.Discord.Data {
    public class Reaction {

        /// <summary>
        /// times this emoji has been used to react
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// whether the current user reacted using this emoji
        /// </summary>
        public bool Me { get; set; }

        /// <summary>
        /// emoji information
        /// </summary>
        public Emoji Emoji { get; set; }
    }
}