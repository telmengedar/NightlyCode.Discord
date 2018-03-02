namespace NightlyCode.Discord.Rest
{

    /// <summary>
    /// parameter to use with requests
    /// </summary>
    public class Parameter
    {

        /// <summary>
        /// creates a new <see cref="Parameter"/>
        /// </summary>
        /// <param name="key">parameter name</param>
        /// <param name="value">parameter value</param>
        public Parameter(string key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// parameter key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// parameter value
        /// </summary>
        public string Value { get; }
    }
}