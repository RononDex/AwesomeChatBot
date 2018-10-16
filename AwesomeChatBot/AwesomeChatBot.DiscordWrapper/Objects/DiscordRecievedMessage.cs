using System;
using System.Collections.Generic;
using System.Text;
using AwesomeChatBot.ApiWrapper;
using Discord.WebSocket;

namespace AwesomeChatBot.DiscordWrapper.Objects
{
    /// <summary>
    /// A recieved discord message
    /// </summary>
    public class DiscordRecievedMessage : ApiWrapper.RecievedMessage
    {
        /// <summary>
        /// The underlying discord message object
        /// </summary>
        public SocketMessage DiscordMessage { get; private set; }


        private DiscordUser _author;

        /// <summary>
        /// The author of the message
        /// </summary>
        public override User Author
        {
            get => _author;
        }

        /// <summary>
        /// DateTime (UTC) of when the message was posted
        /// </summary>
        public override DateTime PostedOnUtc
        {
            get => DiscordMessage.CreatedAt.UtcDateTime;
        }

        /// <summary>
        /// List of attachements
        /// </summary>
        public override List<Attachement> Attacehemnts { get; set; } = new List<Attachement>();

        /// <summary>
        /// The formatted content of the message
        /// </summary>
        public override string Content { get => DiscordMessage.Content; set => throw new NotSupportedException(); }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="wrapper"></param>
        public DiscordRecievedMessage(ApiWrapper.ApiWrapper wrapper, SocketMessage discordMessage) : base(wrapper)
        {
            #region PRECONDITIONS

            if (discordMessage == null)
                throw new ArgumentNullException("DiscordMessage can not be null!");

            #endregion

            this.DiscordMessage = discordMessage;

            this._author = new DiscordUser(wrapper, DiscordMessage.Author);

            // Load attachements
            if (discordMessage.Attachments != null && discordMessage.Attachments.Count > 0)
            {
                foreach (var attachement in discordMessage.Attachments)
                {
                    this.Attacehemnts.Add(new DiscordAttachement(wrapper, attachement));
                }
            }
        }
    }
}
