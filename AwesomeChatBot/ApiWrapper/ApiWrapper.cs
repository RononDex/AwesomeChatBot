using System;
using System.Collections.Generic;
using AwesomeChatBot.Config;

namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// Represents an ApiWrapper
    /// </summary>
    public abstract partial class ApiWrapper
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
        /// <param name="configStore">The configuration store used for the bot instance</param>
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
        public abstract Task<IList<Server>> GetAvailableServersAsync();
    }
}
