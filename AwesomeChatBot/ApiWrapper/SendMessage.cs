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
        public SendMessage(string content) : base (null)
        {
            this.Content = content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content">The content of the message</param>
        /// <returns></returns>
        public SendMessage(string content, List<Attachement> attachements) : base (null)
        {
            this.Content = content;
            this.Attacehemnts = attachements;
        }

        /// <summary>
        /// A list of attachements
        /// </summary>
        /// <value></value>
        public override List<Attachement> Attacehemnts { get; set; }

        /// <summary>
        /// The message content
        /// </summary>
        /// <value></value>
        public override string Content { get; set; }
    }
}