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

        protected virtual void OnMessageReceived(ReceivedMessage message)
        {
            this.MessageReceived(message);
        }

        /// <summary>
        /// Event that gets raised when a message is received
        /// </summary>
        /// <param name="message"></param>
        public delegate void OnServerAvailableDelegate(Server server);

        /// <summary>
        /// This event is used to communicate that a message was received to the framework
        /// </summary>
        public event OnServerAvailableDelegate ServerAvailable;

        protected virtual void OnServerAvailable(Server server)
        {
            this.ServerAvailable(server);
        }

        /// <summary>
        /// Event that gets raised when a message is received
        /// </summary>
        /// <param name="message"></param>
        public delegate void OnServerUnavailableDelegate(Server server);

        /// <summary>
        /// This event is used to communicate that a message was received to the framework
        /// </summary>
        public event OnServerUnavailableDelegate ServerUnavailable;

        /// <summary>
        /// Raises the OnServerUnavailable event
        /// </summary>
        /// <param name="server"></param>
        protected virtual void OnServerUnavailable(Server server)
        {
            this.ServerUnavailable(server);
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
        protected virtual void OnNewUserJoinedServer(User user, Server server)
        {
            this.NewUserJoinedServer(user, server);
        }

        /// <summary>
        /// The delegate used for when the bot joins a new server
        /// </summary>
        /// <param name="user"></param>
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
        protected virtual void OnJoinedNewServer(Server server)
        {
            this.JoinedNewServer(server);
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
        protected virtual void OnConnected(ApiWrapper wrapper)
        {
            this.Connected(wrapper);
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
        protected virtual void OnDisconnected(ApiWrapper wrapper)
        {
            this.Disconnected(wrapper);
        }
    }
}