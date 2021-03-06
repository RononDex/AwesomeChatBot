﻿namespace AwesomeChatBot.Config
{
    /// <summary>
    /// Represents a dependency of a configuration value
    /// </summary>
    public interface IConfigurationDependency
    {
        /// <summary>
        /// The id used to identify the config inside the file
        /// In the format [API]_[TYPE]_[ID])
        /// </summary>
        string ConfigId { get; }

        /// <summary>
        /// The order inside the config file (the deeper down the hierarchy, the bigger the value(
        /// For example a channel is below a server hierarchically speaking, so channel should have higher order
        /// </summary>
        int ConfigOrder { get; }
    }
}
