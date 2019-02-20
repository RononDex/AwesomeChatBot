using Newtonsoft.Json;
using System.Collections.Generic;

namespace AwesomeChatBot.Config
{
    /// <summary>
    /// Represents a config file
    /// </summary>
    public class ConfigFile
    {
        /// <summary>
        /// The name of the config file
        /// </summary>
        [JsonIgnore]
        public string Name { get; set; }

        /// <summary>
        /// A list of sections
        /// </summary>
        public List<ConfigSection> Sections { get; set; }
    }
}
