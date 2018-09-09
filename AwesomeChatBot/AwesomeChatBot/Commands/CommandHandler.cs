﻿using AwesomeChatBot.ApiWrapper;
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
        /// 
        /// </summary>
        /// <param name="recievedMessage"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public abstract Task<bool> ExecuteCommand(RecievedMessage recievedMessage, Command command);
    }
}