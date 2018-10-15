using Discord.WebSocket;
using AwesomeChatBot.ApiWrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AwesomeChatBot.DiscordWrapper.Objects;

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
        /// The logging factory used to create new loggers
        /// </summary>
        public ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        /// Creates an instance of the DiscordWrapper
        /// </summary>
        /// <param name="token">The token to authenticate with the discord API</param>
        public DiscordWrapper(string token, ILoggerFactory loggingFactory)
        {
            #region  PRECONDITIONS

            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException("Parameter \"token\" can not be null!");
            if (loggingFactory == null)
                throw new ArgumentNullException("Parameter \"loggingFactory\" can not be null!");

            #endregion

            this.DiscordToken = token;
            this.LoggerFactory = loggingFactory;
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

            this.LoggerFactory.CreateLogger(this.GetType().FullName).LogInformation("Loging into discord");

            try
            {
                // Login into discord
                this.DiscordClient.LoginAsync(Discord.TokenType.Bot, this.DiscordToken).Wait();
            }
            catch (Exception)
            {
                this.LoggerFactory.CreateLogger(this.GetType().FullName).LogCritical("Login failed, check if discord token is valid!");
                throw;
            }

            this.LoggerFactory.CreateLogger(this.GetType().FullName).LogInformation("Login successfull");

            this.DiscordClient.StartAsync().Wait();

            // Setup the events
            this.DiscordClient.MessageReceived += OnMessageRecieved;
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
