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
        public User Author { get; set; }

        /// <summary>
        /// The time stamp, for when this message was posted
        /// </summary>
        public DateTime PostedOn { get; set; }
    }
}
