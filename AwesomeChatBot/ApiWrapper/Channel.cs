using System.Threading.Tasks;

namespace AwesomeChatBot.ApiWrapper
{
    public abstract class Channel : Config.IConfigurationDependency
    {
        /// <summary>
        /// A reference to the ApiWrapper for internal usage
        /// </summary>
        protected ApiWrapper ApiWrapper { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wrapper"></param>
        protected Channel(ApiWrapper wrapper)
        {
            this.ApiWrapper = wrapper;
        }

        /// <summary>
        /// True is this channel is a direct message (not linked to a server)
        /// </summary>
        public abstract bool IsDirectMessageChannel { get; }

        /// <summary>
        /// The name of the channel
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// A reference to the parent server (if any)
        /// </summary>
        public abstract Server ParentServer { get; }

        /// <summary>
        /// The id of the channel
        /// </summary>
        public abstract string ChannelId { get; }

        /// <summary>
        /// ID used to identify this channel in the config files
        /// </summary>
        /// <returns></returns>
        public string ConfigId => $"Channel_{this.ChannelId}";

        /// <summary>
        /// The channel is usually at the end of the hierarchy
        /// </summary>
        /// <returns></returns>
        public int ConfigOrder => 1000;

        /// <summary>
        /// Sends a message asynchronously in the current channel
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract Task SendMessageAsync(SendMessage message);

        /// <summary>
        /// Sends a message asynchronously in the current channel
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendMessageAsync(string message)
        {
            return SendMessageAsync(new SendMessage(message));
        }

        /// <summary>
        /// Send an embedded message
        /// </summary>
        /// <param name="embeddedMessage">The embedded message to send</param>
        public Task SendMessageAsync(EmbeddedMessage embeddedMessage)
        {
            return SendMessageAsync(new SendMessage(embeddedMessage));
        }

        /// <summary>
        /// Gets a string to mention this channel in the chat
        /// </summary>
        /// <returns></returns>
        public abstract string GetMention();
    }
}
