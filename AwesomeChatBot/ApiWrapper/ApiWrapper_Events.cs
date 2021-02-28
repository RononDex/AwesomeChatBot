using System.Threading.Tasks;

namespace AwesomeChatBot.ApiWrapper
{
    public partial class ApiWrapper
    {
        /// <summary>
        /// When a message was received by the api wrapper
        /// </summary>
        /// <param name="message">The materialized received message</param>
        protected Task OnMessageReceivedAsync(ReceivedMessage message)
        {
            return BotFramework.OnMessageReceivedAsync(message);
        }

        /// <summary>
        /// When a message was deleted
        /// </summary>
        /// <param name="message">The materialized message</param>
        protected Task OnMessageDeletedAsync(ChatMessage message)
        {
            return BotFramework.OnMessageDeletedAsync(message);
        }

        /// <summary>
        /// When a reaction was added
        /// </summary>
        /// <param name="reaction">The materialized reaction</param>
        protected Task OnReactionAddedAsync(Reaction reaction)
        {
            return BotFramework.OnReactionAddedAsync(reaction);
        }

        /// <summary>
        /// When a server became available
        /// </summary>
        /// <param name="server">The materialized server that became available</param>
        protected Task OnServerAvailableAsync(Server server)
        {
            return BotFramework.OnServerAvailableAsync(server);
        }

        /// <summary>
        /// When a server became unavailable
        /// </summary>
        /// <param name="server">The materialized server that became unavailable</param>
        protected Task OnServerUnavailableAsync(Server server)
        {
            return BotFramework.OnServerUnavailableAsync(server);
        }

        /// <summary>
        /// Raises the "NewUserJoined" event
        /// </summary>
        /// <param name="user"></param>
        /// <param name="server"></param>
        protected Task OnNewUserJoinedServerAsync(User user, Server server)
        {
            return BotFramework.OnNewUserJoinedServerAsync(user, server);
        }

        /// <summary>
        /// Raises the "JoinedNewServer" event
        /// </summary>
        /// <param name="server">The server which the bot joined</param>
        protected Task OnJoinedNewServerAsync(Server server)
        {
            return BotFramework.OnNewServerJoinedAsync(server);
        }

        /// <summary>
        /// Raises the "Connected" event
        /// </summary>
        protected Task OnConnectedAsync()
        {
            return BotFramework.OnWrapperConnectedAsync(this);
        }

        /// <summary>
        /// Raises the "Connected" event
        /// </summary>
        protected virtual Task OnDisconnectedAsync()
        {
            return BotFramework.OnWrapperDisconnectedAsync(this);
        }
    }
}
