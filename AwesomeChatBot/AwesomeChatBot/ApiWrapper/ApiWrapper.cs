﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// Represents an ApiWrapper
    /// </summary>
    public abstract class ApiWrapper
    {
        /// <summary>
        /// The name of the wrapper
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Initialises the Wrapper (login into API, ...)
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Gets a list of available servers
        /// </summary>
        /// <returns></returns>
        public abstract List<Server> GetAvailableServers();

        #region Event definitions

        /// <summary>
        /// Event that gets raised when a message is recieved
        /// </summary>
        /// <param name="message"></param>
        public delegate void OnMessageRecievedDeleagte(RecievedMessage message);

        /// <summary>
        /// This event is used to communicate that a message was recieved to the framework
        /// </summary>
        public event OnMessageRecievedDeleagte MessageRecieved;

        protected virtual void OnMessageRecieved(RecievedMessage message)
        {
            this.MessageRecieved(message);
        }



        /// <summary>
        /// Event that gets raised when a message is recieved
        /// </summary>
        /// <param name="message"></param>
        public delegate void OnServerAvilableDelegate(Server server);

        /// <summary>
        /// This event is used to communicate that a message was recieved to the framework
        /// </summary>
        public event OnServerAvilableDelegate ServerAvailable;

        protected virtual void OnServerAvailable(Server server)
        {
            this.ServerAvailable(server);
        }



        /// <summary>
        /// Event that gets raised when a message is recieved
        /// </summary>
        /// <param name="message"></param>
        public delegate void OnServerUnavailableDelegate(Server server);

        /// <summary>
        /// This event is used to communicate that a message was recieved to the framework
        /// </summary>
        public event OnServerUnavailableDelegate ServerUnavailable;

        protected virtual void OnServerUnavailable(Server server)
        {
            this.ServerUnavailable(server);
        }
        #endregion
    }
}
