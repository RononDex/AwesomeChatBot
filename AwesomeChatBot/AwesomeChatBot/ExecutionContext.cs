using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeChatBot
{
    /// <summary>
    /// Execution Context class, provides context to executed commands
    /// </summary>
    public abstract class ExecutionContext
    {
        /// <summary>
        /// A reference to the ApiWrapper for internal usage
        /// </summary>
        public ApiWrapper ApiWrapper { get; private set; }

        public ExecutionContext(ApiWrapper wrapper)
        {
            // Make sure wrapper is not null
            if (wrapper == null)
                throw new ArgumentNullException("Wrapper provided to ExecutionContext can not be NULL!");

            this.ApiWrapper = wrapper;
        }

        /// <summary>
        /// Will send a message in the current context of the command
        /// </summary>
        public abstract Task SendMessageAsync();
    }
}
