using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AwesomeChatBot.Commands.Handlers;
using Xunit;

namespace AwesomeChatBot.Tests
{
    public class RegexCommandHandlerTests
    {
        private readonly RegexCommandHandler handler = new RegexCommandHandler();

        [Fact]
        public void ShouldExecute_CommandIsNotRegexCommand_ReturnsFalse()
        {
            // Arrange
            var message = new FakeReceivedMessage("hello");

            // Act
            var (shouldExecute, parameter) = handler.ShouldExecute(message, new FakeCommand());

            // Assert
            Assert.False(shouldExecute);
            Assert.Null(parameter);
        }

        [Fact]
        public void ShouldExecute_ContentMatchesPattern_ReturnsTrueWithMatch()
        {
            // Arrange
            var command = new FakeRegexCommand(@"weather in (?<city>\w+)");
            var message = new FakeReceivedMessage("what is the weather in Bern?");

            // Act
            var (shouldExecute, parameter) = handler.ShouldExecute(message, command);

            // Assert
            Assert.True(shouldExecute);
            Assert.Equal("Bern", Assert.IsType<Match>(parameter).Groups["city"].Value);
        }

        [Fact]
        public void ShouldExecute_ContentMatchesWithDifferentCase_ReturnsTrue()
        {
            // Arrange
            var command = new FakeRegexCommand("hello");
            var message = new FakeReceivedMessage("HELLO");

            // Act
            var (shouldExecute, _) = handler.ShouldExecute(message, command);

            // Assert
            Assert.True(shouldExecute);
        }

        [Fact]
        public void ShouldExecute_OnlySecondPatternMatches_ReturnsMatchOfSecondPattern()
        {
            // Arrange
            var command = new FakeRegexCommand("goodbye", "hel+o");
            var message = new FakeReceivedMessage("hello");

            // Act
            var (shouldExecute, parameter) = handler.ShouldExecute(message, command);

            // Assert
            Assert.True(shouldExecute);
            Assert.Equal("hello", Assert.IsType<Match>(parameter).Value);
        }

        [Fact]
        public void ShouldExecute_NoPatternMatches_ReturnsFalse()
        {
            // Arrange
            var command = new FakeRegexCommand("goodbye", "farewell");
            var message = new FakeReceivedMessage("hello");

            // Act
            var (shouldExecute, parameter) = handler.ShouldExecute(message, command);

            // Assert
            Assert.False(shouldExecute);
            Assert.Null(parameter);
        }

        [Fact]
        public async Task ExecuteCommandAsync_CommandIsNotRegexCommand_Throws()
        {
            // Arrange
            var message = new FakeReceivedMessage("hello");
            var match = Regex.Match("hello", "hello");

            // Act
            var act = () => handler.ExecuteCommandAsync(message, new FakeCommand(), match);

            // Assert
            await Assert.ThrowsAsync<Exception>(act);
        }

        [Fact]
        public async Task ExecuteCommandAsync_ParameterIsNotMatch_Throws()
        {
            // Arrange
            var message = new FakeReceivedMessage("hello");
            var command = new FakeRegexCommand("hello");

            // Act
            var act = () => handler.ExecuteCommandAsync(message, command, "not a match");

            // Assert
            await Assert.ThrowsAsync<Exception>(act);
        }

        [Fact]
        public async Task ExecuteCommandAsync_ValidMatch_PassesMatchToCommand()
        {
            // Arrange
            var message = new FakeReceivedMessage("hello");
            var command = new FakeRegexCommand("hello");
            var match = Regex.Match("hello", "hello");

            // Act
            await handler.ExecuteCommandAsync(message, command, match);

            // Assert
            Assert.Same(match, Assert.Single(command.Executions));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ExecuteCommandAsync_ValidMatch_ReturnsResultOfCommand(bool commandResult)
        {
            // Arrange
            var message = new FakeReceivedMessage("hello");
            var command = new FakeRegexCommand("hello") { Result = commandResult };
            var match = Regex.Match("hello", "hello");

            // Act
            var result = await handler.ExecuteCommandAsync(message, command, match);

            // Assert
            Assert.Equal(commandResult, result);
        }
    }
}
