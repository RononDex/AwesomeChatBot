using Discord.WebSocket;
using AwesomeChatBot.ApiWrapper;
using System;
using System.Collections.Generic;

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

            // Login into discord
            this.DiscordClient.LoginAsync(Discord.TokenType.Bot, this.DiscordToken).Wait();
        }

        public override List<Server> GetAvailableServers()
        {
            throw new NotImplementedException();
        }
    }
}
