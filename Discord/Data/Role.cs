namespace NightlyCode.Discord.Data {

    /// <summary>
    /// Roles represent a set of permissions attached to a group of users
    /// </summary>
    public class Role {

        /// <summary>
        /// role id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// role name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// integer representation of hexadecimal color code
        /// </summary>
        public int Color { get; set; }

        /// <summary>
        /// if this role is pinned in the user listing
        /// </summary>
        public bool Hoist { get; set; }

        /// <summary>
        /// position of this role
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// permission bit set
        /// </summary>
        public int Permissions { get; set; }

        /// <summary>
        /// whether this role is managed by an integration
        /// </summary>
        public bool Managed { get; set; }

        /// <summary>
        /// whether this role is mentionable
        /// </summary>
        public bool Mentionable { get; set; } 
    }
}