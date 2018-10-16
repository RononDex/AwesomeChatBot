using AwesomeChatBot.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeChatBot
{
    /// <summary>
    /// The main class (create an instance of this class to use the framework)
    /// </summary>
    public class AwesomeChatBot
    {
        /// <summary>
        /// The Api Wrapper to use to communicate with the API / Chat network
        /// </summary>
        protected ApiWrapper.ApiWrapper ApiWrapper { get; private set; }

        /// <summary>
        /// Holds the reference to the command factory
        /// </summary>
        protected CommandFactory CommandFactory { get; set; }

        /// <summary>
        /// A reference to the logger
        /// </summary>
        public ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        /// The settings of this Framework
        /// </summary>
        public AwesomeChatBotSettings Settings { get; private set; }

        /// <summary>
        /// The config store used to access the config of this bot
        /// </summary>
        public Config.ConfigStore ConfigStore { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AwesomeChatBot(ApiWrapper.ApiWrapper wrapper, ILoggerFactory loggerFactory, AwesomeChatBotSettings settings)
        {
            #region PRECONDITIONS

            if (wrapper == null)
                throw new ArgumentNullException("No ApiWrapper provided to AwesomeChatBot");
            if (loggerFactory == null)
                throw new ArgumentNullException("No loggerFactory provided to AwesomeChatBot");
            if (settings == null)
                throw new ArgumentNullException("No settings provided to AwesomeChatBot");
            if (string.IsNullOrEmpty(settings.ConfigFolderPath))
                loggerFactory.CreateLogger(this.GetType().FullName).LogWarning("No ConfigFolderPath provided, will be using the application root directory!");

            #endregion

            this.ApiWrapper = wrapper;
            this.ApiWrapper.Initialize();

            this.CommandFactory = new CommandFactory(wrapper);
            this.LoggerFactory = loggerFactory;
            
            this.ConfigStore = new Config.ConfigStore(settings.ConfigFolderPath);
            this.Settings = settings;

            this.ApiWrapper.MessageRecieved += OnMessageRecieved;

            loggerFactory.CreateLogger(this.GetType().FullName).LogInformation($"AwesomeChatBot Framework has been loaded with wrapper \"{wrapper.Name}\"");
            loggerFactory.CreateLogger(this.GetType().FullName).LogInformation("Bot is ready...");
        }

        /// <summary>
        /// Will be fired when the ApiWrapper reports a new message
        /// </summary>
        /// <param name="recievedMessage"></param>
        protected virtual void OnMessageRecieved(ApiWrapper.RecievedMessage recievedMessage)
        {
            this.CommandFactory.HandleMessage(recievedMessage);
        }
    }
}
