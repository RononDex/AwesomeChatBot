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
    }
}