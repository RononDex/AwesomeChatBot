using System;
using System.Collections.Generic;

namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// A message that is supposed to be sent
    /// </summary>
    public class SendMessage : Message
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="content">The content of the message</param>
        /// <returns></returns>
        public SendMessage(string content) : base(wrapper: null)
        {
            Content = content;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="content">The content of the message</param>
        /// <param name="attachments">Attachments that are to be sent with the message</param>
        /// <returns></returns>
        public SendMessage(string content, List<Attachment> attachments) : base(wrapper: null)
        {
            Content = content;
            Attachments = attachments;
        }

        /// <summary>
        /// Send a EmbeddedMessage
        /// </summary>
        /// <param name="embeddedMessage"></param>
        /// <returns></returns>
        public SendMessage(EmbeddedMessage embeddedMessage) : base(wrapper: null)
        {
            this.EmbeddedMessage = embeddedMessage ?? throw new ArgumentNullException(nameof(embeddedMessage));
        }

        /// <summary>
        /// A list of attachments
        /// </summary>
        /// <value></value>
        public override List<Attachment> Attachments { get; set; }

        /// <summary>
        /// The message content
        /// </summary>
        /// <value></value>
        public override string Content { get; set; }

        /// <summary>
        /// If you want to send an EmbeddedMessage, set this property
        /// </summary>
        /// <value></value>
        public EmbeddedMessage EmbeddedMessage { get; set; }
    }
}