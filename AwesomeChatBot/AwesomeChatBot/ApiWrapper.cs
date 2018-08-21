using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeChatBot
{
    /// <summary>
    /// Represents an ApiWrapper
    /// </summary>
    public abstract class ApiWrapper
    {
        /// <summary>
        /// Initialises the Wrapper (login into API, ...)
        /// </summary>
        public abstract void Initialize();



        #region Event definitions

        /// <summary>
        /// Event that gets raised when a message is recieved
        /// </summary>
        /// <param name="message"></param>
        public delegate void OnMessageRecievedDeleagte(Context.Message message);

        /// <summary>
        /// This event is used to communicate that a message was recieved to the framework
        /// </summary>
        public event OnMessageRecievedDeleagte OnMessageRecieved;

        #endregion
    }
}
