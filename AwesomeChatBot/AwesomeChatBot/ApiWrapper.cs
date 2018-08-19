using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
