using System.Collections.Generic;
using NightlyCode.Discord.Rest;

namespace NightlyCode.Discord.Data.Channels {
    public class GetMessagesParameter : IParameterObject {
        public string Around { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public int Limit { get; set; }

        public IEnumerable<Parameter> CreateParameters() {
            if(!string.IsNullOrEmpty(Around))
                yield return new Parameter("around", Around);
            if(!string.IsNullOrEmpty(Before))
                yield return new Parameter("before", Before);
            if(!string.IsNullOrEmpty(After))
                yield return new Parameter("after", After);
            if(Limit>0)
                yield return new Parameter("limit", Limit.ToString());
        }
    }
}