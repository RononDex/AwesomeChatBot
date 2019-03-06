using AwesomeChatBot.Commands;
using Microsoft.Extensions.Logging;
using System;

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
        public ApiWrapper.ApiWrapper ApiWrapper { get; private set; }

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

            #endregion

            this.ConfigStore = new Config.ConfigStore(settings.ConfigFolderPath, loggerFactory);
            this.Settings = settings;

            this.ApiWrapper = wrapper;
            this.ApiWrapper.Initialize(this.ConfigStore);

            this.CommandFactory = new CommandFactory(this, this.ConfigStore);
            this.LoggerFactory = loggerFactory;

            // Setup Api events
            this.ApiWrapper.MessageReceived += OnMessageReceived;
            this.ApiWrapper.ServerAvailable += OnServerAvailable;

            loggerFactory.CreateLogger(this.GetType().FullName).LogInformation($"AwesomeChatBot Framework has been loaded using the wrapper \"{wrapper.Name}\"");

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

        /// <summary>
        /// Will be fired when the ApiWrapper reports a new message
        /// </summary>
        /// <param name="receivedMessage"></param>
        protected virtual void OnMessageReceived(ApiWrapper.ReceivedMessage receivedMessage)
        {
            this.LoggerFactory.CreateLogger(this.GetType().FullName).LogDebug($"Message received: {receivedMessage.Content})");
            this.CommandFactory.HandleMessage(receivedMessage);
        }

        /// <summary>
        /// When a server becomes available (connected)
        /// </summary>
        /// <param name="server"></param>
        protected virtual void OnServerAvailable(ApiWrapper.Server server)
        {
            this.LoggerFactory.CreateLogger(this.GetType().FullName).LogInformation($"Server now available: {server.ServerName} ({server.ServerID})");
        }

        /// <summary>
        /// When a server becomes unavailable (disconnected)
        /// </summary>
        /// <param name="server"></param>
        protected virtual void OnServerUnavailable(ApiWrapper.Server server)
        {
            LoggerFactory.CreateLogger(this.GetType().FullName).LogInformation($"Server now unavailable: {server.ServerName} ({server.ServerID})");
        }

    }
}
