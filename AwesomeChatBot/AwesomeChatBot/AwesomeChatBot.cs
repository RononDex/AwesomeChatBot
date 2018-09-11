using AwesomeChatBot.Commands;
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
        /// The config store used to access the config of this bot
        /// </summary>
        public Config.ConfigStore ConfigStore { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AwesomeChatBot(ApiWrapper.ApiWrapper wrapper, string configFolder)
        {
            this.ApiWrapper = wrapper;
            this.CommandFactory = new CommandFactory(wrapper);
            this.ApiWrapper.OnMessageRecieved += OnMessageRecieved;
            this.ConfigStore = new Config.ConfigStore(configFolder);
        }

        /// <summary>
        /// Will be fired when the ApiWrapper reports a new message
        /// </summary>
        /// <param name="recievedMessage"></param>
        protected virtual void OnMessageRecieved(ApiWrapper.RecievedMessage recievedMessage)
        {

        }
    }
}
