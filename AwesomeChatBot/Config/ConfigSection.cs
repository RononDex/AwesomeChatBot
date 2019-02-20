using System.Collections.Generic;

namespace AwesomeChatBot.Config
{
    /// <summary>
    /// Represents a config section
    /// </summary>
    public class ConfigSection
    {
        /// <summary>
        /// The id of the config section (in the format [API]_[TYPE]_[ID])
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// List of subsections
        /// </summary>
        public List<ConfigSection> SubSections { get; set; } = new List<ConfigSection>();

        /// <summary>
        /// List of config entries
        /// </summary>
        public List<ConfigValue> Config { get; set; } = new List<ConfigValue>();
    }
}
