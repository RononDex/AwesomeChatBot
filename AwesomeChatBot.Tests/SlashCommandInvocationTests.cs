using System.Collections.Generic;
using AwesomeChatBot.ApiWrapper;
using AwesomeChatBot.Commands.Handlers;
using Xunit;

namespace AwesomeChatBot.Tests
{
    // Option values are stored in the shape the doc comment on SlashCommandInvocation.Options promises
    public class SlashCommandInvocationTests
    {
        [Fact]
        public void GetOption_StringStoredReadAsString_ReturnsValue()
        {
            // Arrange
            var invocation = InvocationWith("option", "hello");

            // Act
            var result = invocation.GetOption<string>("option");

            // Assert
            Assert.Equal("hello", result);
        }

        [Fact]
        public void GetOption_LongStoredReadAsLong_ReturnsValue()
        {
            // Arrange
            var invocation = InvocationWith("option", 42L);

            // Act
            var result = invocation.GetOption<long>("option");

            // Assert
            Assert.Equal(42L, result);
        }

        [Fact]
        public void GetOption_LongStoredReadAsNullableLong_ReturnsValue()
        {
            // Arrange
            var invocation = InvocationWith("option", 42L);

            // Act
            var result = invocation.GetOption<long?>("option");

            // Assert
            Assert.Equal(42L, result);
        }

        [Fact]
        public void GetOption_LongStoredReadAsInt_ReturnsDefault()
        {
            // Arrange
            var invocation = InvocationWith("option", 42L);

            // Act
            var result = invocation.GetOption<int>("option");

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetOption_LongStoredReadAsNullableInt_ReturnsNull()
        {
            // Arrange
            var invocation = InvocationWith("option", 42L);

            // Act
            var result = invocation.GetOption<int?>("option");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetOption_DoubleStoredReadAsDouble_ReturnsValue()
        {
            // Arrange
            var invocation = InvocationWith("option", 1.5d);

            // Act
            var result = invocation.GetOption<double>("option");

            // Assert
            Assert.Equal(1.5d, result);
        }

        [Fact]
        public void GetOption_DoubleStoredReadAsNullableDouble_ReturnsValue()
        {
            // Arrange
            var invocation = InvocationWith("option", 1.5d);

            // Act
            var result = invocation.GetOption<double?>("option");

            // Assert
            Assert.Equal(1.5d, result);
        }

        [Fact]
        public void GetOption_DoubleStoredReadAsFloat_ReturnsDefault()
        {
            // Arrange
            var invocation = InvocationWith("option", 1.5d);

            // Act
            var result = invocation.GetOption<float>("option");

            // Assert
            Assert.Equal(0f, result);
        }

        [Fact]
        public void GetOption_DoubleStoredReadAsDecimal_ReturnsDefault()
        {
            // Arrange
            var invocation = InvocationWith("option", 1.5d);

            // Act
            var result = invocation.GetOption<decimal>("option");

            // Assert
            Assert.Equal(0m, result);
        }

        [Fact]
        public void GetOption_BoolStoredReadAsBool_ReturnsValue()
        {
            // Arrange
            var invocation = InvocationWith("option", true);

            // Act
            var result = invocation.GetOption<bool>("option");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetOption_BoolStoredReadAsNullableBool_ReturnsValue()
        {
            // Arrange
            var invocation = InvocationWith("option", true);

            // Act
            var result = invocation.GetOption<bool?>("option");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetOption_UserStoredReadAsUser_ReturnsSameInstance()
        {
            // Arrange
            var user = new FakeUser();
            var invocation = InvocationWith("option", user);

            // Act
            var result = invocation.GetOption<User>("option");

            // Assert
            Assert.Same(user, result);
        }

        [Fact]
        public void GetOption_AttachmentStoredReadAsAttachment_ReturnsSameInstance()
        {
            // Arrange
            var attachment = new FakeAttachment();
            var invocation = InvocationWith("option", attachment);

            // Act
            var result = invocation.GetOption<Attachment>("option");

            // Assert
            Assert.Same(attachment, result);
        }

        [Fact]
        public void GetOption_AttachmentStoredReadAsUser_ReturnsNull()
        {
            // Arrange
            var invocation = InvocationWith("option", new FakeAttachment());

            // Act
            var result = invocation.GetOption<User>("option");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetOption_OptionMissingReadAsString_ReturnsNull()
        {
            // Arrange
            var invocation = InvocationWith("other", "hello");

            // Act
            var result = invocation.GetOption<string>("option");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetOption_OptionMissingReadAsNullableBool_ReturnsNull()
        {
            // Arrange
            var invocation = InvocationWith("other", true);

            // Act
            var result = invocation.GetOption<bool?>("option");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetOption_OptionMissingReadAsInt_ReturnsDefault()
        {
            // Arrange
            var invocation = InvocationWith("other", 42L);

            // Act
            var result = invocation.GetOption<int>("option");

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetOption_NullStoredReadAsNullableLong_ReturnsNull()
        {
            // Arrange
            var invocation = InvocationWith("option", null);

            // Act
            var result = invocation.GetOption<long?>("option");

            // Assert
            Assert.Null(result);
        }

        private static FakeInvocation InvocationWith(string name, object value) =>
            new FakeInvocation(options: new Dictionary<string, object> { [name] = value });
    }
}
