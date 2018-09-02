using AwesomeChatBot.ApiWrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeChatBot.Commands
{
    /// <summary>
    /// A command represents an entity that reacts to defined messages
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Unique name of the command
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="recievedMessage"></param>
        /// <returns></returns>
        public abstract Task<bool> ExecuteCommand(RecievedMessage recievedMessage);
    }
}
