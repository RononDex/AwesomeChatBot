using System.Linq;
using System.Threading.Tasks;
using AwesomeChatBot.ApiWrapper;
using Microsoft.Extensions.Logging;

namespace AwesomeChatBot
{
    public partial class AwesomeChatBot
    {
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
        internal Task OnMessageReceivedAsync(ReceivedMessage receivedMessage)
        {
            return Task.Run(async () =>
            {
                LoggerFactory.CreateLogger(GetType().FullName).LogInformation($"Message received: {receivedMessage.Content})");
                await CommandFactory.HandleMessageAsync(receivedMessage).ConfigureAwait(false);
                MessageReceived?.Invoke(receivedMessage);
            });
        }

        /// <summary>
        /// The delegate to use when a message is deleted
        /// </summary>
        /// <param name="deletedMessage"></param>
        public delegate void OnMessageDeletedDelegate(ChatMessage deletedMessage);

        /// <summary>
        /// When e message is deleted
        /// </summary>
        public event OnMessageDeletedDelegate MessageDeleted;

        /// <summary>
        /// Will be fired when the ApiWrapper reports that a message was deleted
        /// </summary>
        /// <param name="deletedMessage"></param>
        internal Task OnMessageDeletedAsync(ChatMessage deletedMessage)
        {
            return Task.Run(() =>
            {
                LoggerFactory.CreateLogger(GetType().FullName).LogInformation($"Message deleted: {deletedMessage.Content})");
                MessageDeleted?.Invoke(deletedMessage);
            });
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

        /// <summary>
        /// Will be fired when a reaction was added
        /// </summary>
        /// <param name="reaction"></param>
        internal Task OnReactionAddedAsync(Reaction reaction)
        {
            return Task.Run(() => ReactionAdded?.Invoke(reaction));
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
        internal Task OnServerAvailableAsync(Server server)
        {
            return Task.Run(() =>
            {
                LoggerFactory.CreateLogger(GetType().FullName).LogInformation($"Server now available: {server.ServerName} ({server.ServerID})");
                ServerAvailable?.Invoke(server);
            });
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
        internal Task OnServerUnavailableAsync(Server server)
        {
            return Task.Run(() =>
            {
                LoggerFactory.CreateLogger(GetType().FullName).LogInformation($"Server now unavailable: {server.ServerName} ({server.ServerID})");
                ServerUnavailable?.Invoke(server);
            });
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
        internal Task OnNewUserJoinedServerAsync(User user, Server server)
        {
            return Task.Run(() =>
            {
                LoggerFactory
                    .CreateLogger(GetType().FullName)
                    .LogInformation($"{nameof(NewUserJoinedServer)}: User: {user.UniqueUserName} Server: ({server.ServerName})");
                NewUserJoinedServer?.Invoke(user, server);
            });
        }

        /// <summary>
        /// The delegate for the server joined event
        /// </summary>
        /// <param name="server"></param>
        public delegate void OnNewServerJoinedDelegate(Server server);

        /// <summary>
        /// When a new server is joined
        /// </summary>
        public event OnNewServerJoinedDelegate NewServerJoined;

        /// <summary>
        /// When the bot user joins a new server
        /// </summary>
        /// <param name="server"></param>
        internal Task OnNewServerJoinedAsync(Server server)
        {
            return Task.Run(() =>
            {
                LoggerFactory
                    .CreateLogger(GetType().FullName)
                    .LogInformation($"{nameof(NewServerJoined)}: Server: ({server.ServerName})");
                NewServerJoined?.Invoke(server);
            });
        }

        /// <summary>
        /// Delegate for the OnConnected event
        /// </summary>
        /// <param name="wrapper">The wrapper that has connected</param>
        public delegate void OnConnected(ApiWrapper.ApiWrapper wrapper);

        /// <summary>
        /// When a new user joins a server
        /// </summary>
        public event OnConnected Connected;

        /// <summary>
        /// When the wrapper / bot has connected to an API
        /// </summary>
        /// <param name="wrapper">The wrapper that has connected to the API</param>
        internal Task OnWrapperConnectedAsync(ApiWrapper.ApiWrapper wrapper)
        {
            return Task.Run(() =>
            {
                LoggerFactory
                    .CreateLogger(GetType().FullName)
                    .LogInformation($"{nameof(OnConnected)}: Wrapper: {wrapper.Name}");
                Connected?.Invoke(wrapper);
            });
        }

        /// <summary>
        /// Delegate for the OnDisconnected event
        /// </summary>
        /// <param name="wrapper">The wrapper that has disconnected</param>
        public delegate void OnDisconnected(ApiWrapper.ApiWrapper wrapper);

        /// <summary>
        /// When a new user joins a server
        /// </summary>
        public event OnDisconnected Disconnected;

        /// <summary>
        /// When the wrapper / bot has connected to an API
        /// </summary>
        /// <param name="wrapper">The wrapper that has connected to the API</param>
        internal Task OnWrapperDisconnectedAsync(ApiWrapper.ApiWrapper wrapper)
        {
            return Task.Run(() =>
            {
                LoggerFactory
                    .CreateLogger(GetType().FullName)
                    .LogInformation($"{nameof(OnDisconnected)}: Wrapper: {wrapper.Name}");
                Disconnected?.Invoke(wrapper);
            });
        }
    }
}