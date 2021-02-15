using System.Threading.Tasks;

namespace AwesomeChatBot.ApiWrapper
{
    public partial class ApiWrapper
    {
        /// <summary>
        /// Event that gets raised when a message is received
        /// </summary>
        /// <param name="message"></param>
        public delegate void OnMessageReceivedDelegate(ReceivedMessage message);

        /// <summary>
        /// This event is used to communicate that a message was received to the framework
        /// </summary>
        public event OnMessageReceivedDelegate MessageReceived;

        protected virtual Task OnMessageReceivedAsync(ReceivedMessage message)
        {
            return Task.Run(() => MessageReceived?.Invoke(message));
        }

        /// <summary>
        /// Event that gets raised when a message is deleted
        /// </summary>
        /// <param name="deletedMessage"></param>
        public delegate void OnMessageDeletedDelegate(ChatMessage deletedMessage);

        /// <summary>
        /// This event is used to communicate that a message was received to the framework
        /// </summary>
        public event OnMessageDeletedDelegate MessageDeleted;

        protected virtual Task OnMessageDeletedAsync(ChatMessage message)
        {
            return Task.Run(() => MessageDeleted?.Invoke(message));
        }

        /// <summary>
        /// The delegate to use when a reaction is added
        /// </summary>
        /// <param name="addedReaction"></param>
        public delegate void OnMessageReactionAddedDelegate(Reaction addedReaction);

        /// <summary>
        /// When a reaction is added
        /// </summary>
        public event OnMessageReactionAddedDelegate ReactionAdded;

        protected virtual Task OnReactionAddedAsync(Reaction reaction)
        {
            return Task.Run(() => ReactionAdded?.Invoke(reaction));
        }

        /// <summary>
        /// Event that gets raised when a message is received
        /// </summary>
        /// <param name="server"></param>
        public delegate void OnServerAvailableDelegate(Server server);

        /// <summary>
        /// This event is used to communicate that a message was received to the framework
        /// </summary>
        public event OnServerAvailableDelegate ServerAvailable;

        protected virtual Task OnServerAvailableAsync(Server server)
        {
            return Task.Run(() => ServerAvailable?.Invoke(server));
        }

        /// <summary>
        /// Event that gets raised when a message is received
        /// </summary>
        /// <param name="server"></param>
        public delegate void OnServerUnavailableDelegate(Server server);

        /// <summary>
        /// This event is used to communicate that a message was received to the framework
        /// </summary>
        public event OnServerUnavailableDelegate ServerUnavailable;

        /// <summary>
        /// Raises the OnServerUnavailable event
        /// </summary>
        /// <param name="server"></param>
        protected virtual Task OnServerUnavailableAsync(Server server)
        {
            return Task.Run(() => ServerUnavailable?.Invoke(server));
        }

        /// <summary>
        /// The delegate for the new user joined server event
        /// </summary>
        /// <param name="user"></param>
        /// <param name="server"></param>
        public delegate void OnNewUserJoinedServerDelegate(User user, Server server);

        /// <summary>
        /// When a new user joins a server
        /// </summary>
        public event OnNewUserJoinedServerDelegate NewUserJoinedServer;

        /// <summary>
        /// Raises the "NewUserJoined" event
        /// </summary>
        /// <param name="user"></param>
        /// <param name="server"></param>
        protected virtual Task OnNewUserJoinedServerAsync(User user, Server server)
        {
            return Task.Run(() => NewUserJoinedServer?.Invoke(user, server));
        }

        /// <summary>
        /// The delegate used for when the bot joins a new server
        /// </summary>
        /// <param name="server"></param>
        public delegate void OnJoinedNewServerDelegate(Server server);

        /// <summary>
        /// When a new user joins a server
        /// </summary>
        public event OnJoinedNewServerDelegate JoinedNewServer;

        /// <summary>
        /// Raises the "JoinedNewServer" event
        /// </summary>
        /// <param name="server">The server which the bot joined</param>
        protected virtual Task OnJoinedNewServerAsync(Server server)
        {
            return Task.Run(() => JoinedNewServer?.Invoke(server));
        }

        /// <summary>
        /// Delegate for the Connected event
        /// </summary>
        /// <param name="wrapper">The wrapper instance that connected to the API</param>
        public delegate void OnConnectedDelegate(ApiWrapper wrapper);

        /// <summary>
        /// When the wrapper connects to its API
        /// </summary>
        public event OnConnectedDelegate Connected;

        /// <summary>
        /// Raises the "Connected" event
        /// </summary>
        /// <param name="wrapper">The wrapper instance that connected to the API</param>
        protected virtual Task OnConnectedAsync(ApiWrapper wrapper)
        {
            return Task.Run(() => Connected?.Invoke(wrapper));
        }

        /// <summary>
        /// Delegate for the disconnected event
        /// </summary>
        /// <param name="wrapper">The wrapper instance that connected to the API</param>
        public delegate void OnDisconnectedDelegate(ApiWrapper wrapper);

        /// <summary>
        /// When the wrapper connects to its API
        /// </summary>
        public event OnDisconnectedDelegate Disconnected;

        /// <summary>
        /// Raises the "Connected" event
        /// </summary>
        /// <param name="wrapper">The wrapper instance that connected to the API</param>
        protected virtual Task OnDisconnectedAsync(ApiWrapper wrapper)
        {
            return Task.Run(() => Disconnected?.Invoke(wrapper));
        }
    }
}
