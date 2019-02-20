using System;

namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// Represents a received message, used when receiving a message
    /// </summary>
    public abstract class ReceivedMessage : Message
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
        /// The author of the message
        /// </summary>
        public abstract User Author { get; }

        /// <summary>
        /// The time stamp, for when this message was posted
        /// </summary>
        public abstract DateTime PostedOnUtc { get; }

        /// <summary>
        /// The parent Channel
        /// </summary>
        /// <value></value>
        public abstract Channel Channel { get; }

        /// <summary>
        /// true if the bot was mentioned in the message
        /// </summary>
        /// <value></value>
        public abstract bool IsBotMentioned { get; }
    }
}
