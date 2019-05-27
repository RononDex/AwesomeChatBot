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
        public IReadOnlyList<ApiWrapper.ApiWrapper> ApiWrappers { get; private set; }

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
        public AwesomeChatBot(IReadOnlyList<ApiWrapper.ApiWrapper> wrappers, ILoggerFactory loggerFactory, AwesomeChatBotSettings settings)
        {
            #region PRECONDITIONS

            if (wrappers == null || wrappers.Count == 0)
                throw new ArgumentNullException("No ApiWrapper provided to AwesomeChatBot");
            if (loggerFactory == null)
                throw new ArgumentNullException("No loggerFactory provided to AwesomeChatBot");
            if (settings == null)
                throw new ArgumentNullException("No settings provided to AwesomeChatBot");

            #endregion

            this.ConfigStore = new Config.ConfigStore(settings.ConfigFolderPath, loggerFactory);
            this.Settings = settings;

            var wrappersList = wrappers.ToList();
            this.ApiWrappers = wrappersList;
            wrappersList.ForEach(x => x.Initialize(this.ConfigStore));

            this.CommandFactory = new CommandFactory(this, this.ConfigStore);
            this.LoggerFactory = loggerFactory;

            // Setup Api events
            SetupEvents();

            loggerFactory.CreateLogger(this.GetType().FullName).LogInformation($"AwesomeChatBot Framework has been loaded using the wrappers \"{string.Join(" ", wrappers.Select(x => x.Name))}\"");

            loggerFactory.CreateLogger(this.GetType().FullName).LogInformation("");
            loggerFactory.CreateLogger(this.GetType().FullName).LogInformation("Bot is ready...");
        }

        /// <summary>
        /// Registers a command with the framework
        /// </summary>
        /// <param name="command"></param>
        public void RegisterCommand(Command command)
        {
            this.CommandFactory.RegisterCommand(command);
            this.LoggerFactory.CreateLogger(this.GetType().FullName).LogInformation($"Command {command.Name} registered");
        }

        /// <summary>
        /// Registers the given handler with the command factory
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public void RegisterCommandHandler(Commands.CommandHandler handler)
        {
            this.CommandFactory.RegisterHandler(handler);
            this.LoggerFactory.CreateLogger(this.GetType().FullName).LogInformation($"Handler {handler.Name} registered");
        }
    }
}
