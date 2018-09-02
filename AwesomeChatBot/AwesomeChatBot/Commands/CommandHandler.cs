using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeChatBot.Commands
{
    public abstract class CommandHandler
    {
        /// <summary>
        /// An internal reference to the Command factory
        /// </summary>
        protected CommandFactory CommandFactory { get; set; }

        /// <summary>
        /// The unique name of the handler
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// A command has to implement this type (interface) in order to
        /// be executed using this handler
        /// </summary>
        public abstract Type CommandType { get; }

        public CommandHandler(CommandFactory factory)
        {
            this.CommandFactory = factory;
        }

        /// <summary>
        /// CHeck if a certain command has to be executed for a specific message
        /// </summary>
        /// <param name="recievedMessage"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task<bool> CheckAndExecuteCommand(ApiWrapper.RecievedMessage recievedMessage, Command command)
        {
            if (this.CheckIfCommandShouldExecute(recievedMessage, command))
            {
                return command.ExecuteCommand(recievedMessage);
            }
            else
            {
                return new Task<bool>(() =>
                {
                    return false;
                });
            }
        }

        /// <summary>
        /// Check wether the given command should be executed for the given message
        /// </summary>
        /// <param name="recievedMessage"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        protected abstract bool CheckIfCommandShouldExecute(ApiWrapper.RecievedMessage recievedMessage, Command command);     
    }
}
