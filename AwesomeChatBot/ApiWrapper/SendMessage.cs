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
        public SendMessage(string content) : base(null)
        {
            this.Content = content;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="content">The content of the message</param>
        /// <returns></returns>
        public SendMessage(string content, List<Attachment> attachments) : base(null)
        {
            this.Content = content;
            this.Attachments = attachments;
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
    }
}