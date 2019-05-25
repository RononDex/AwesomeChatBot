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
            var wrapperList = this.ApiWrappers.ToList();
            wrapperList.ForEach(x => x.MessageReceived += OnMessageReceived);
            wrapperList.ForEach(x => x.ServerAvailable += OnServerAvailable);
            wrapperList.ForEach(x => x.NewUserJoinedServer += OnNewUserJoinedServer);
            wrapperList.ForEach(x => x.ServerUnavailable += OnServerUnavailable);
        }

        /// <summary>
        /// The delegate to use when a message is received
        /// </summary>
        /// <param name="user"></param>
        /// <param name="server"></param>
        public delegate void OnMessageReceivedDelegate(ReceivedMessage receivedMessage);

        /// <summary>
        /// When e message is received
        /// </summary>
        public event OnMessageReceivedDelegate MessageReceived;

        /// <summary>
        /// Will be fired when the ApiWrapper reports a new message
        /// </summary>
        /// <param name="receivedMessage"></param>
        protected virtual void OnMessageReceived(ApiWrapper.ReceivedMessage receivedMessage)
        {
            this.LoggerFactory.CreateLogger(this.GetType().FullName).LogDebug($"Message received: {receivedMessage.Content})");
            this.CommandFactory.HandleMessage(receivedMessage);
            this.MessageReceived(receivedMessage);
        }

        /// <summary>
        /// The delegate when a server becomes unavailable
        /// </summary>
        /// <param name="user"></param>
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
        protected virtual void OnServerAvailable(ApiWrapper.Server server)
        {
            this.LoggerFactory.CreateLogger(this.GetType().FullName).LogInformation($"Server now available: {server.ServerName} ({server.ServerID})");
            this.ServerAvailable(server);
        }

        /// <summary>
        /// The delegate when a server becomes unavailable
        /// </summary>
        /// <param name="user"></param>
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
        protected virtual void OnServerUnavailable(ApiWrapper.Server server)
        {
            LoggerFactory.CreateLogger(this.GetType().FullName).LogInformation($"Server now unavailable: {server.ServerName} ({server.ServerID})");
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
        /// When a server becomes unavailable (disconnected)
        /// </summary>
        /// <param name="server"></param>
        private void OnNewUserJoinedServer(User user, Server server)
        {
            LoggerFactory.CreateLogger(this.GetType().FullName).LogInformation($"{nameof(NewUserJoinedServer)}: User: {user.UniqueUserName} Server: ({server.ServerName})");
            this.NewUserJoinedServer(user, server);
        }
    }
}