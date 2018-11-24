using System.Collections.Generic;

namespace NightlyCode.Discord.Rest {

    /// <summary>
    /// interface for a parameter object
    /// </summary>
    public interface IParameterObject {

        /// <summary>
        /// creates parameters for rest request
        /// </summary>
        /// <returns></returns>
        IEnumerable<Parameter> CreateParameters();
    }
}