using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AwesomeChatBot.Commands;
using AwesomeChatBot.Commands.Handlers;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace AwesomeChatBot.Tests
{
    public class CommandFactorySlashCommandTests : IDisposable
    {
        // ConfigStore creates and reads this folder, so each test gets its own empty one
        private readonly string configFolder = Path.Combine(Path.GetTempPath(), $"AwesomeChatBot.Tests-{Guid.NewGuid()}");

        public void Dispose()
        {
            if (Directory.Exists(configFolder))
                Directory.Delete(configFolder, recursive: true);
        }

        [Fact]
        public async Task HandleSlashCommandAsync_CommandNotRegistered_RespondsUnknownCommand()
        {
            // Arrange
            var (_, factory) = CreateFactory();
            var invocation = new FakeInvocation("weather");

            // Act
            await factory.HandleSlashCommandAsync(invocation);

            // Assert
            Assert.Equal("Unknown command", Assert.Single(invocation.Responses));
        }

        [Fact]
        public async Task HandleSlashCommandAsync_CommandRegistered_ExecutesCommandWithInvocation()
        {
            // Arrange
            var (_, factory) = CreateFactory();
            var command = new FakeSlashCommand("weather");
            factory.RegisterCommand(command);
            var invocation = new FakeInvocation("weather");

            // Act
            await factory.HandleSlashCommandAsync(invocation);

            // Assert
            Assert.Same(invocation, Assert.Single(command.Executions));
        }

        [Fact]
        public async Task HandleSlashCommandAsync_MultipleCommandsRegistered_ExecutesOnlyMatchingCommand()
        {
            // Arrange
            var (_, factory) = CreateFactory();
            var weather = new FakeSlashCommand("weather");
            var time = new FakeSlashCommand("time");
            factory.RegisterCommand(weather);
            factory.RegisterCommand(time);

            // Act
            await factory.HandleSlashCommandAsync(new FakeInvocation("time"));

            // Assert
            Assert.Empty(weather.Executions);
            Assert.Single(time.Executions);
        }

        [Fact]
        public async Task HandleSlashCommandAsync_CommandDisabled_RespondsDisabledAndDoesNotExecute()
        {
            // Arrange
            var (_, factory) = CreateFactory(commandsEnabledByDefault: false);
            var command = new FakeSlashCommand("weather");
            factory.RegisterCommand(command);
            var invocation = new FakeInvocation("weather");

            // Act
            await factory.HandleSlashCommandAsync(invocation);

            // Assert
            Assert.Empty(command.Executions);
            Assert.Equal("This command is disabled here", Assert.Single(invocation.Responses));
        }

        [Fact]
        public async Task HandleSlashCommandAsync_FilterReturnsFalse_DoesNotExecute()
        {
            // Arrange
            var (bot, factory) = CreateFactory();
            var command = new FakeSlashCommand("weather");
            factory.RegisterCommand(command);
            bot.SlashCommandFilter = _ => Task.FromResult(false);

            // Act
            await factory.HandleSlashCommandAsync(new FakeInvocation("weather"));

            // Assert
            Assert.Empty(command.Executions);
        }

        [Fact]
        public async Task HandleSlashCommandAsync_FilterReturnsTrue_Executes()
        {
            // Arrange
            var (bot, factory) = CreateFactory();
            var command = new FakeSlashCommand("weather");
            factory.RegisterCommand(command);
            bot.SlashCommandFilter = _ => Task.FromResult(true);

            // Act
            await factory.HandleSlashCommandAsync(new FakeInvocation("weather"));

            // Assert
            Assert.Single(command.Executions);
        }

        [Fact]
        public async Task HandleSlashCommandAsync_CommandThrows_RespondsWithErrorMessage()
        {
            // Arrange
            var (_, factory) = CreateFactory();
            factory.RegisterCommand(new FakeSlashCommand("weather", new InvalidOperationException("boom")));
            var invocation = new FakeInvocation("weather");

            // Act
            await factory.HandleSlashCommandAsync(invocation);

            // Assert
            Assert.Contains("boom", Assert.Single(invocation.Responses));
        }

        [Fact]
        public void RegisterCommand_SlashCommand_AddsToSlashCommands()
        {
            // Arrange
            var (_, factory) = CreateFactory();
            var command = new FakeSlashCommand("weather");

            // Act
            factory.RegisterCommand(command);

            // Assert
            Assert.Same(command, Assert.Single(factory.SlashCommands));
        }

        [Fact]
        public void SlashCommands_NonSlashCommandRegistered_IsNotIncluded()
        {
            // Arrange
            var (_, factory) = CreateFactory();

            // Act
            factory.RegisterCommand(new FakeRegexCommand("hello"));

            // Assert
            Assert.Empty(factory.SlashCommands);
        }

        private (AwesomeChatBot bot, CommandFactory factory) CreateFactory(bool commandsEnabledByDefault = true)
        {
            var settings = new AwesomeChatBotSettings
            {
                ConfigFolderPath = configFolder,
                CommandsEnabledByDefault = commandsEnabledByDefault,
            };
            var bot = new AwesomeChatBot(new[] { new FakeApiWrapper() }, NullLoggerFactory.Instance, settings);
            return (bot, new CommandFactory(bot, bot.ConfigStore));
        }
    }
}
