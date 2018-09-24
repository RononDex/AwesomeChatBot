using Discord.WebSocket;
using AwesomeChatBot.ApiWrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeChatBot.DiscordWrapper
{
    /// <summary>
    /// Discord API Wrapper for the "AwesomeChatBot" Framework
    /// </summary>
    public class DiscordWrapper : ApiWrapper.ApiWrapper
    {
        /// <summary>
        /// The token used to authenticate against discord API
        /// </summary>
        private string DiscordToken { get; set; }

        private DiscordSocketClient DiscordClient { get; set; }

        /// <summary>
        /// Creates an instance of the DiscordWrapper
        /// </summary>
        /// <param name="token">The token to authenticate with the discord API</param>
        public DiscordWrapper(string token)
        {
            this.DiscordToken = token;
        }

        /// <summary>
        /// Initializes the 
        /// </summary>
        public override void Initialize()
        {
            // Setup the discord client
            this.DiscordClient = new DiscordSocketClient(new DiscordSocketConfig()
            {
                MessageCacheSize = 50,

            });

            // Setup the events
            this.DiscordClient.MessageReceived += OnMessageRecieved;

            // Login into discord
            this.DiscordClient.LoginAsync(Discord.TokenType.Bot, this.DiscordToken).Wait();
        }


        /// <summary>
        /// When a new message is recieved
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected Task OnMessageRecieved(SocketMessage message)
        {
            return Task.Factory.StartNew(() =>
               {
                   // Create the message object
                   var messageObj = new DiscordRecievedMessage(this, message);

                   // Propagate the event
                   base.OnMessageRecieved(messageObj);
               });
        }

        public override List<Server> GetAvailableServers()
        {
            throw new NotImplementedException();
        }
    }
}
