using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public abstract MessageFormatter MessageFormatter { get; }

        /// <summary>
        /// The internal reference to the config store
        /// </summary>
        public ConfigStore ConfigStore { get; private set; }

        /// <summary>
        /// Initializes the Wrapper (login into API, ...)
        /// </summary>
        /// <param name="configStore">The configuration store used for the bot instance</param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Initialize(ConfigStore configStore)
        {
            ConfigStore = configStore ?? throw new ArgumentNullException(nameof(configStore));
        }

        /// <summary>
        /// Gets a list of available servers
        /// </summary>
        public abstract Task<IList<Server>> GetAvailableServersAsync();

        /// <summary>
        /// An internal reference to the botFramework instance
        /// </summary>
        internal AwesomeChatBot BotFramework { get; set; }
    }
}
