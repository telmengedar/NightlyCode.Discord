namespace NightlyCode.Discord.Data.Activities {
    public class ActivityParty {

        /// <summary>
        /// the id of the party
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// used to show the party's current and maximum size
        /// </summary>
        public int[] Size { get; set; } 
    }
}