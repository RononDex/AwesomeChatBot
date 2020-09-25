namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// Represents a received message, used when receiving a message
    /// </summary>
    public abstract class ReceivedMessage : ChatMessage
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="wrapper"></param>
        public ReceivedMessage(ApiWrapper wrapper) : base(wrapper)
        {
            // Nothing to do here
        }

        /// <summary>
        /// true if the bot was mentioned in the message
        /// </summary>
        /// <value></value>
        public abstract bool IsBotMentioned { get; }
    }
}
