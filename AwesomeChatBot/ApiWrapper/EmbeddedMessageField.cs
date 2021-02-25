namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// A field to render inside an embedded message
    /// </summary>
    public class EmbeddedMessageField
    {
        /// <summary>
        /// The name of the field
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The content of the field
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Inline the field? Makes it go on the same line as other fields (if enough space)
        /// </summary>
        public bool Inline { get; set; }
    }
}