using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// Represents a recieved message, used when recieving a message
    /// </summary>
    public abstract class RecievedMessage : Message
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wrapper"></param>
        public RecievedMessage(ApiWrapper wrapper) : base(wrapper)
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
    }
}
