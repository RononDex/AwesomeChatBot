using System;
using System.Threading.Tasks;
using AwesomeChatBot.ApiWrapper;
using Microsoft.Extensions.Logging;

namespace AwesomeChatBot
{
    public partial class AwesomeChatBot
    {
        /// <summary>
        /// When e message is received
        /// </summary>
        public event Func<ReceivedMessage, Task> MessageReceived;

        /// <summary>
        /// Will be fired when the ApiWrapper reports a new message
        /// </summary>
        /// <param name="receivedMessage"></param>
        internal async Task OnMessageReceivedAsync(ReceivedMessage receivedMessage)
        {
            LoggerFactory.CreateLogger(GetType().FullName).LogTrace($"Message received: {receivedMessage.Content})");
            await CommandFactory.HandleMessageAsync(receivedMessage).ConfigureAwait(false);
            MessageReceived?.Invoke(receivedMessage);
        }

        /// <summary>
        /// When e message is deleted
        /// </summary>
        public event Func<ChatMessage, Task> MessageDeleted;

        /// <summary>
        /// Will be fired when the ApiWrapper reports that a message was deleted
        /// </summary>
        /// <param name="deletedMessage"></param>
        internal Task OnMessageDeletedAsync(ChatMessage deletedMessage)
        {
            LoggerFactory.CreateLogger(GetType().FullName).LogTrace($"Message deleted: {deletedMessage.Content})");
            return MessageDeleted != null
                ? MessageDeleted.Invoke(deletedMessage)
                : Task.CompletedTask;
        }

        /// <summary>
        /// When a reaction is added
        /// </summary>
        public event Func<Reaction, Task> ReactionAdded;

        /// <summary>
        /// Will be fired when a reaction was added
        /// </summary>
        /// <param name="reaction"></param>
        internal Task OnReactionAddedAsync(Reaction reaction)
        {
            LoggerFactory.CreateLogger(GetType().FullName).LogTrace($"Reaction was added: {reaction.Content}");
            return ReactionAdded != null
                ? ReactionAdded.Invoke(reaction)
                : Task.CompletedTask;
        }

        /// <summary>
        /// When a server becomes unavailable
        /// </summary>
        public event Func<Server, Task> ServerAvailable;

        /// <summary>
        /// When a server becomes available (connected)
        /// </summary>
        /// <param name="server"></param>
        internal Task OnServerAvailableAsync(Server server)
        {
            LoggerFactory.CreateLogger(GetType().FullName).LogTrace($"Server now available: {server.ServerName} ({server.ServerID})");
            return ServerAvailable != null
                ? ServerAvailable.Invoke(server)
                : Task.CompletedTask;
        }

        /// <summary>
        /// When a server becomes unavailable
        /// </summary>
        public event Func<Server, Task> ServerUnavailable;

        /// <summary>
        /// When a server becomes unavailable (disconnected)
        /// </summary>
        /// <param name="server"></param>
        internal Task OnServerUnavailableAsync(Server server)
        {
            LoggerFactory.CreateLogger(GetType().FullName).LogTrace($"Server now unavailable: {server.ServerName} ({server.ServerID})");
            return ServerUnavailable != null
                ? ServerUnavailable.Invoke(server)
                : Task.CompletedTask;
        }

        /// <summary>
        /// When a new user joins a server
        /// </summary>
        public event Func<User, Server, Task> NewUserJoinedServer;

        /// <summary>
        /// When a server becomes unavailable (disconnected)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="server"></param>
        internal Task OnNewUserJoinedServerAsync(User user, Server server)
        {
            LoggerFactory
                .CreateLogger(GetType().FullName)
                .LogTrace($"{nameof(NewUserJoinedServer)}: User: {user.UniqueUserName} Server: ({server.ServerName})");

            return NewUserJoinedServer != null
                ? NewUserJoinedServer.Invoke(user, server)
                : Task.CompletedTask;
        }

        /// <summary>
        /// When a new server is joined
        /// </summary>
        public event Func<Server, Task> NewServerJoined;

        /// <summary>
        /// When the bot user joins a new server
        /// </summary>
        /// <param name="server"></param>
        internal Task OnNewServerJoinedAsync(Server server)
        {
            LoggerFactory
                .CreateLogger(GetType().FullName)
                .LogTrace($"{nameof(NewServerJoined)}: Server: ({server.ServerName})");

            return NewServerJoined != null
                ? NewServerJoined.Invoke(server)
                : Task.CompletedTask;
        }

        /// <summary>
        /// When a new user joins a server
        /// </summary>
        public event Func<ApiWrapper.ApiWrapper, Task> Connected;

        /// <summary>
        /// When the wrapper / bot has connected to an API
        /// </summary>
        /// <param name="wrapper">The wrapper that has connected to the API</param>
        internal Task OnWrapperConnectedAsync(ApiWrapper.ApiWrapper wrapper)
        {
            LoggerFactory
                .CreateLogger(GetType().FullName)
                .LogTrace($"{nameof(Connected)}: Wrapper: {wrapper.Name}");

            return Connected != null
                ? Connected.Invoke(wrapper)
                : Task.CompletedTask;
        }

        /// <summary>
        /// When a new user joins a server
        /// </summary>
        public event Func<ApiWrapper.ApiWrapper, Task> Disconnected;

        /// <summary>
        /// When the wrapper / bot has connected to an API
        /// </summary>
        /// <param name="wrapper">The wrapper that has connected to the API</param>
        internal Task OnWrapperDisconnectedAsync(ApiWrapper.ApiWrapper wrapper)
        {
            LoggerFactory
                .CreateLogger(GetType().FullName)
                .LogTrace($"{nameof(Disconnected)}: Wrapper: {wrapper.Name}");

            return Disconnected != null
                ? Disconnected.Invoke(wrapper)
                : Task.CompletedTask;
        }
    }
}