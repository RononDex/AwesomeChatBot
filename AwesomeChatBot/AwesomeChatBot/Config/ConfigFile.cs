using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
