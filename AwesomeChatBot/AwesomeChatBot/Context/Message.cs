using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeChatBot.Context
{
    public abstract class Message
    {
        /// <summary>
        /// Internal reference to the ApiWrapper
        /// </summary>
        protected ApiWrapper ApiWrapper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wrapper"></param>
        public Message(ApiWrapper wrapper)
        {
            this.ApiWrapper = wrapper;
        }

        /// <summary>
        /// A list of attachements on the message (if any)
        /// </summary>
        public List<Attachement> Attacehemnts { get; set; }

        /// <summary>
        /// Raw text message
        /// </summary>
        public string TextRaw { get; set; }

        /// <summary>
        /// Formatted text message (user friendly to read)
        /// </summary>
        public string TextFormatted { get; set; }

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
