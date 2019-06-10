using System.Linq;
using AwesomeChatBot.ApiWrapper;
using Microsoft.Extensions.Logging;

namespace AwesomeChatBot
{
    public partial class AwesomeChatBot
    {
        /// <summary>
        /// Sets up the events for the wrappers
        /// </summary>
        private void SetupEvents()
        {
            var wrapperList = ApiWrappers.ToList();
            wrapperList.ForEach(x => x.MessageReceived += OnMessageReceived);
            wrapperList.ForEach(x => x.ServerAvailable += OnServerAvailable);
            wrapperList.ForEach(x => x.NewUserJoinedServer += OnNewUserJoinedServer);
            wrapperList.ForEach(x => x.ServerUnavailable += OnServerUnavailable);
        }

        /// <summary>
        /// The delegate to use when a message is received
        /// </summary>
        /// <param name="receivedMessage"></param>
        public delegate void OnMessageReceivedDelegate(ReceivedMessage receivedMessage);

        /// <summary>
        /// When e message is received
        /// </summary>
        public event OnMessageReceivedDelegate MessageReceived;

        /// <summary>
        /// Will be fired when the ApiWrapper reports a new message
        /// </summary>
        /// <param name="receivedMessage"></param>
        protected virtual void OnMessageReceived(ReceivedMessage receivedMessage)
        {
            LoggerFactory.CreateLogger(GetType().FullName).LogInformation($"Message received: {receivedMessage.Content})");
            CommandFactory.HandleMessage(receivedMessage);
            MessageReceived?.Invoke(receivedMessage);
        }

        /// <summary>
        /// The delegate when a server becomes unavailable
        /// </summary>
        /// <param name="server"></param>
        public delegate void OnServerAvailableDelegate(Server server);

        /// <summary>
        /// When a server becomes unavailable
        /// </summary>
        public event OnServerAvailableDelegate ServerAvailable;

        /// <summary>
        /// When a server becomes available (connected)
        /// </summary>
        /// <param name="server"></param>
        protected virtual void OnServerAvailable(Server server)
        {
            LoggerFactory.CreateLogger(GetType().FullName).LogInformation($"Server now available: {server.ServerName} ({server.ServerID})");
            ServerAvailable?.Invoke(server);
        }

        /// <summary>
        /// The delegate when a server becomes unavailable
        /// </summary>
        /// <param name="server"></param>
        public delegate void OnServerUnavailableDelegate(Server server);

        /// <summary>
        /// When a server becomes unavailable
        /// </summary>
        public event OnServerUnavailableDelegate ServerUnavailable;

        /// <summary>
        /// When a server becomes unavailable (disconnected)
        /// </summary>
        /// <param name="server"></param>
        protected virtual void OnServerUnavailable(Server server)
        {
            LoggerFactory.CreateLogger(GetType().FullName).LogInformation($"Server now unavailable: {server.ServerName} ({server.ServerID})");
            ServerUnavailable?.Invoke(server);
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
        /// When a server becomes unavailable (disconnected)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="server"></param>
        private void OnNewUserJoinedServer(User user, Server server)
        {
            LoggerFactory
                .CreateLogger(GetType().FullName)
                .LogInformation($"{nameof(NewUserJoinedServer)}: User: {user.UniqueUserName} Server: ({server.ServerName})");
            NewUserJoinedServer?.Invoke(user, server);
        }
    }
}