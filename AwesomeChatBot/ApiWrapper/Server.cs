using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeChatBot.ApiWrapper
{
    public abstract class Server
    {
        /// <summary>
        /// ApiWrapper reference for internal usage
        /// </summary>
        protected ApiWrapper ApiWrapper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wrapper"></param>
        public Server(ApiWrapper wrapper)
        {
            this.ApiWrapper = wrapper;
        }

        /// <summary>
        /// The name of the server
        /// </summary>
        public abstract string ServerName { get; }

        /// <summary>
        /// Some ID identifiyng the server
        /// </summary>
        public abstract string ServerID { get; }

        /// <summary>
        /// A short description of the server
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Resolves a channel given its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract Task<Channel> ResolveChannelAsync(string name);

        /// <summary>
        /// Gets a list of all to the bot available channels
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<Channel>> GetAllChannelsAsync();
    }
}
