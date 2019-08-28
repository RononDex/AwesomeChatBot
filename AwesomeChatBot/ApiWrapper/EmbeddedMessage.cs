using System.Collections.Generic;

namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// Represents an embedded message
    /// </summary>
    public class EmbeddedMessage
    {
        /// <summary>
        /// The title of the embedded message
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Fields to display in the embedded message
        /// </summary>
        public IList<EmbeddedMessageField> Fields { get; } = new List<EmbeddedMessageField>();
    }
}