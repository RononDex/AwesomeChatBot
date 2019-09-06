using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AwesomeChatBot.Commands
{
    /// <summary>
    /// The Commaand factory is responsible for managing all registered commands, deciding which one to execute etc
    /// </summary>
    public class CommandFactory
    {
        /// <summary>
        /// The timeout for a task in seconds
        /// </summary>
        public int TaskTimeout { get; set; } = 5 * 60;

        /// <summary>
        /// Configstore that manages all the configs
        /// </summary>
        /// <value></value>
        public Config.ConfigStore ConfigStore { get; private set; }

        /// <summary>
        /// Reference to the bot framework
        /// </summary>
        protected AwesomeChatBot BotFramework { get; set; }

        /// <summary>
        /// Internal storage of registered handlers
        /// </summary>
        protected List<CommandHandler> Handlers { get; set; } = new List<CommandHandler>();

        /// <summary>
        /// A list of registered commands
        /// </summary>
        protected List<Command> Commands { get; set; } = new List<Command>();

        /// <summary>
        /// Holds a list of registered event handlers
        /// </summary>
        protected List<EventHandler> EventHandlers => new List<EventHandler>();

        /// <summary>
        /// Constructor of CommandFactory
        /// </summary>
        /// <param name="wrapper"></param>
        public CommandFactory(AwesomeChatBot botFramework, Config.ConfigStore configStore)
        {
            BotFramework = botFramework ?? throw new ArgumentNullException("botFramework parameter can not be null!");
            ConfigStore = configStore ?? throw new ArgumentNullException("configStore parameter can not be null!");
        }

        /// <summary>
        /// Handles a message
        /// </summary>
        /// <param name="receivedMessage"></param>
        /// <returns></returns>
        public async Task HandleMessageAsync(ApiWrapper.ReceivedMessage receivedMessage)
        {
            try
            {
                var configContext = new Config.IConfigurationDependency[] { receivedMessage.Channel.ParentServer, receivedMessage.Channel };

                // Iterate through all commands and check if one of them gets triggered
                foreach (var command in Commands)
                {
                    foreach (var handler in Handlers.Where(x => ConfigStore.IsCommandActive(command, BotFramework.Settings.CommandsEnabledByDefault, configContext)
                                    && command.GetType().GetInterfaces().Contains(x.CommandType)
                                && receivedMessage.IsBotMentioned))
                    {
                        // If command should not execute, ignore command and continue to next
                        var shouldExecute = handler.ShouldExecute(receivedMessage, command);
                        if (!shouldExecute.shouldExecute)
                            continue;

                        var commandResult = await handler.ExecuteCommand(receivedMessage, command, shouldExecute.parameter);
                        if (commandResult)
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                BotFramework.LoggerFactory.CreateLogger(this.GetType().Name).LogError($"Error processing message {receivedMessage.Content}: {ex}");
                await receivedMessage.Channel.SendMessageAsync($"Oh no! Something caused me to crash: {ex.Message}");
            }
        }

        /// <summary>
        /// Registers a handler with the command factory
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public CommandFactory RegisterHandler(CommandHandler handler)
        {
            if (handler == null)
                throw new ArgumentException("handler can not be null!");

            Handlers.Add(handler);

            return this;
        }

        /// <summary>
        /// Registers a new command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public CommandFactory RegisterCommand(Command command)
        {
            if (command == null)
                throw new ArgumentException("command can not be null!");

            Commands.Add(command);

            return this;
        }

        /// <summary>
        /// Registers an event handler on the command factory
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <returns></returns>
        public CommandFactory RegisterEventHandler(EventHandler eventHandler)
        {
            if (eventHandler == null)
                throw new ArgumentException("eventHandler can not be null!");

            EventHandlers.Add(eventHandler);

            return this;
        }
    }
}
