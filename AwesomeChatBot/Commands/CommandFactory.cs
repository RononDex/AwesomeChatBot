using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Constructor of CommandFacotry
        /// </summary>
        /// <param name="wrapper"></param>
        public CommandFactory(AwesomeChatBot botFramework, Config.ConfigStore configStore)
        {
            #region PRECONDITIONS

            if (botFramework == null)
                throw new ArgumentNullException("botFramework parameter can not be null!");
            if (configStore == null)
                throw new ArgumentNullException("configStore parameter can not be null!");

            #endregion

            this.BotFramework = botFramework;
            this.ConfigStore = configStore;
        }

        /// <summary>
        /// Handles a message
        /// </summary>
        /// <param name="recievedMessage"></param>
        /// <returns></returns>
        public Task HandleMessage(ApiWrapper.RecievedMessage recievedMessage)
        {
            var task = new Task(() =>
            {
                var configContext = new Config.IConfigurationDependency[] { recievedMessage.Channel.ParentServer, recievedMessage.Channel };

                // Iterate through all commands and check if one of them gets triggered
                foreach (var command in this.Commands)
                {
                    foreach (var handler in this.Handlers.Where(x => this.ConfigStore.IsCommandActive(command, this.BotFramework.Settings.CommandsEnabledByDefault, configContext) 
                                    && command.GetType().GetInterfaces().Contains(x.CommandType)
                                && (recievedMessage.IsBotMentioned)))
                    {
                        // If command should not execute, ignore command and continue to next
                        var shouldExecute = handler.ShouldExecute(recievedMessage, command);
                        if (!shouldExecute.Item1)
                            continue;

                        var commandResult = handler.ExecuteCommand(recievedMessage, command, shouldExecute.Item2);
                        commandResult.Wait();
                        if (commandResult.Result)
                            return;
                    }
                }
            });

            task.Start();
            return task;
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

            this.Handlers.Add(handler);

            return this;
        }

        /// <summary>
        /// Registeres a new command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public CommandFactory RegisterCommand(Command command)
        {
            if (command == null)
                throw new ArgumentException("command can not be null!");

            this.Commands.Add(command);

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

            this.EventHandlers.Add(eventHandler);

            return this;
        }
    }
}
