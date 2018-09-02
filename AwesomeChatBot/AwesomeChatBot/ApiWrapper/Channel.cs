using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeChatBot.ApiWrapper
{
    public abstract class Channel
    {
        /// <summary>
        /// A reference to the ApiWrapper for internal usage
        /// </summary>
        protected ApiWrapper ApiWrapper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wrapper"></param>
        public Channel(ApiWrapper wrapper)
        {
            this.ApiWrapper = wrapper;
        }

        /// <summary>
        /// Sends a message asynchroniously in the current channel
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract Task SendMessageAsync(string message);

        /// <summary>
        /// Sends a file in the current channel asynchroniously
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="File"></param>
        /// <returns></returns>
        public abstract Task SendFileAsync(string filename, byte File);

        /// <summary>
        /// True is this channel is a direct message (not linked to a server)
        /// </summary>
        public abstract bool IsDirectMessage { get; set; }

        /// <summary>
        /// Gets a string to mention this channel in the chat
        /// </summary>
        /// <returns></returns>
        public abstract string GetMention();
    }
}
