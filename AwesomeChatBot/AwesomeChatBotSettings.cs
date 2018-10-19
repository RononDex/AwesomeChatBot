using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeChatBot
{
    /// <summary>
    /// A value object used to pass a long settings for the AwesomeChatBot Framework
    /// </summary>
    public class AwesomeChatBotSettings
    {
        /// <summary>
        /// Path to the folder containing all the .json config files
        /// </summary>
        public string ConfigFolderPath { get; set; }
    }
}
