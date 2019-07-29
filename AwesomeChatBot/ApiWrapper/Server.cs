using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeChatBot.ApiWrapper
{
    public abstract class Server : Config.IConfigurationDependency
    {
        /// <summary>
        /// ApiWrapper reference for internal usage
        /// </summary>
        protected ApiWrapper ApiWrapper { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wrapper"></param>
        protected Server(ApiWrapper wrapper)
        {
            ApiWrapper = wrapper;
        }

        /// <summary>
        /// The name of the server
        /// </summary>
        public abstract string ServerName { get; }

        /// <summary>
        /// Some ID identifying the server
        /// </summary>
        public abstract string ServerID { get; }

        /// <summary>
        /// A short description of the server
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// The Id used to identify this server in config files
        /// </summary>
        /// <returns></returns>
        public string ConfigId => $"Server_{this.ServerID}";

        /// <summary>
        /// The server is usually one of the highest up in hierarchy, having an order of 10
        /// </summary>
        /// <returns></returns>
        public int ConfigOrder => 10;

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
