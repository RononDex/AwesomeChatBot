using System;
using System.Collections.Generic;
using AwesomeChatBot.Config;

namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// Represents an ApiWrapper
    /// </summary>
    public abstract class ApiWrapper
    {
        /// <summary>
        /// The name of the wrapper
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The formatter used to format messages
        /// </summary>
        /// <value></value>
        public abstract MessageFormatter MessageFormatter { get; }

        /// <summary>
        /// The internal reference to the config store
        /// </summary>
        /// <value></value>
        public ConfigStore ConfigStore { get; private set; }

        /// <summary>
        /// Initializes the Wrapper (login into API, ...)
        /// </summary>
        public virtual void Initialize(ConfigStore configStore)
        {
            #region PRECONDITIONS

            if (configStore == null)
                throw new ArgumentNullException("Parameter \"configStore\" can not be null!");

            #endregion

            this.ConfigStore = configStore;
        }

        /// <summary>
        /// Gets a list of available servers
        /// </summary>
        /// <returns></returns>
        public abstract List<Server> GetAvailableServers();

        #region Event definitions

        /// <summary>
        /// Event that gets raised when a message is received
        /// </summary>
        /// <param name="message"></param>
        public delegate void OnMessageReceivedDelegate(ReceivedMessage message);

        /// <summary>
        /// This event is used to communicate that a message was received to the framework
        /// </summary>
        public event OnMessageReceivedDelegate MessageReceived;

        protected virtual void OnMessageReceived(ReceivedMessage message)
        {
            this.MessageReceived(message);
        }

        /// <summary>
        /// Event that gets raised when a message is received
        /// </summary>
        /// <param name="message"></param>
        public delegate void OnServerAvailableDelegate(Server server);

        /// <summary>
        /// This event is used to communicate that a message was received to the framework
        /// </summary>
        public event OnServerAvailableDelegate ServerAvailable;

        protected virtual void OnServerAvailable(Server server)
        {
            this.ServerAvailable(server);
        }

        /// <summary>
        /// Event that gets raised when a message is received
        /// </summary>
        /// <param name="message"></param>
        public delegate void OnServerUnavailableDelegate(Server server);

        /// <summary>
        /// This event is used to communicate that a message was received to the framework
        /// </summary>
        public event OnServerUnavailableDelegate ServerUnavailable;

        protected virtual void OnServerUnavailable(Server server)
        {
            this.ServerUnavailable(server);
        }
        #endregion
    }
}
