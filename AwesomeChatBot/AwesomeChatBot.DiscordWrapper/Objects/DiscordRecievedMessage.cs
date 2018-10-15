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
            this.Content = this.DiscordMessage.Content;
        }
    }
}
