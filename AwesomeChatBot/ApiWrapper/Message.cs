using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// Represents a message object that is used to send a message
    /// </summary>
    public abstract class Message
    {
        /// <summary>
        /// Internal reference to the ApiWrapper
        /// </summary>
        public ApiWrapper ApiWrapper { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wrapper"></param>
        public Message(ApiWrapper wrapper)
        {
            this.ApiWrapper = wrapper;
        }

        /// <summary>
        /// A list of attachments on the message (if any)
        /// </summary>
        public abstract Task<IList<Attachment>> Attachments { get; set; }

        /// <summary>
        /// Formatted text message
        /// </summary>
        public abstract string Content { get; set; }
    }
}
