using System.Linq;
using AwesomeChatBot.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AwesomeChatBot
{
    /// <summary>
    /// The main class (creates an instance of this class to use the framework)
    /// </summary>
    public partial class AwesomeChatBot
    {
        /// <summary>
        /// The Api Wrapper to use to communicate with the API / Chat network
        /// </summary>
        public IReadOnlyList<ApiWrapper.ApiWrapper> ApiWrappers { get; }

        /// <summary>
        /// Holds the reference to the command factory
        /// </summary>
        protected CommandFactory CommandFactory { get; }

        /// <summary>
        /// A reference to the logger
        /// </summary>
        public ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// The settings of this Framework
        /// </summary>
        public AwesomeChatBotSettings Settings { get; private set; }

        /// <summary>
        /// The config store used to access the config of this bot
        /// </summary>
        public Config.ConfigStore ConfigStore { get; }

        /// <summary>
        ///
        /// </summary>
        public AwesomeChatBot(IReadOnlyList<ApiWrapper.ApiWrapper> wrappers, ILoggerFactory loggerFactory, AwesomeChatBotSettings settings)
        {
            #region PRECONDITIONS

            if (wrappers == null || wrappers.Count == 0)
                throw new ArgumentNullException("No ApiWrapper provided to AwesomeChatBot");
            if (loggerFactory == null)
                throw new ArgumentNullException("No loggerFactory provided to AwesomeChatBot");
            if (settings == null)
                throw new ArgumentNullException("No settings provided to AwesomeChatBot");

            #endregion PRECONDITIONS

            ConfigStore = new Config.ConfigStore(settings.ConfigFolderPath, loggerFactory);
            Settings = settings;

            var wrappersList = wrappers.ToList();
            ApiWrappers = wrappersList;
            wrappersList.ForEach(x => x.Initialize(ConfigStore));

            CommandFactory = new CommandFactory(this, ConfigStore);
            LoggerFactory = loggerFactory;

            // Setup Api events
            SetupEvents();

            loggerFactory
                .CreateLogger(GetType().FullName)
                .LogInformation($"AwesomeChatBot Framework has been loaded using the wrappers \"{string.Join(" ", wrappers.Select(x => x.Name))}\"");

            loggerFactory.CreateLogger(GetType().FullName).LogInformation("");
            loggerFactory.CreateLogger(GetType().FullName).LogInformation("Bot is ready...");
        }

        /// <summary>
        /// Registers a command with the framework
        /// </summary>
        /// <param name="command"></param>
        public void RegisterCommand(Command command)
        {
            _ = CommandFactory.RegisterCommand(command);
            LoggerFactory.CreateLogger(GetType().FullName).LogInformation($"Command {command.Name} registered");
        }

        /// <summary>
        /// Registers the given handler with the command factory
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public void RegisterCommandHandler(CommandHandler handler)
        {
            _ = CommandFactory.RegisterHandler(handler);
            LoggerFactory.CreateLogger(GetType().FullName).LogInformation($"Handler {handler.Name} registered");
        }
    }
}
