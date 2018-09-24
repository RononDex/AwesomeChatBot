using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeChatBot.Commands
{
    /// <summary>
    /// The basic / abstract class for an event handler
    /// </summary>
    public abstract class EventHandler
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

        public EventHandler(CommandFactory factory)
        {
            this.CommandFactory = factory;
        }
    }
}
