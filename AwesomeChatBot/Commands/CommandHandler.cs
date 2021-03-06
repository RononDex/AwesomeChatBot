﻿using AwesomeChatBot.ApiWrapper;
using System;
using System.Threading.Tasks;

namespace AwesomeChatBot.Commands
{
    public abstract class CommandHandler
    {

        /// <summary>
        /// The unique name of the handler
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// A command has to implement this type (interface) in order to
        /// be executed using this handler
        /// </summary>
        public abstract Type CommandType { get; }

        /// <summary>
        /// Determines wether the given command should be executed
        /// Returns a boolean saying wether the command should execute,
        /// and a list of parameters provided to the command
        /// </summary>
        /// <param name="receivedMessage"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public abstract (bool shouldExecute, object parameter) ShouldExecute(ReceivedMessage receivedMessage, Command command);

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="receivedMessage"></param>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns>true if command was executed (and no other command should be executed), false if not</returns>
        public abstract Task<bool> ExecuteCommandAsync(ReceivedMessage receivedMessage, Command command, object parameters);
    }
}
