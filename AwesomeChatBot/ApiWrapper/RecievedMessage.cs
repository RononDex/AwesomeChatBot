﻿using System.Threading.Tasks;

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
        protected ReceivedMessage(ApiWrapper wrapper) : base(wrapper)
        {
            // Nothing to do here
        }

        /// <summary>
        /// true if the bot was mentioned in the message
        /// </summary>
        public abstract bool IsBotMentioned { get; }

        /// <summary>
        /// Publishes a message to subscribers, if supported
        /// </summary>
        public abstract Task PublishAsync();
    }
}
