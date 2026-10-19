using System;
using AwesomeChatBot.Commands.Handlers;
using Xunit;

namespace AwesomeChatBot.Tests
{
    public class SlashCommandChoiceTests
    {
        [Fact]
        public void Value_ReadThroughBaseClass_ReturnsTypedValue()
        {
            // Arrange
            SlashCommandChoice choice = new SlashCommandChoice<long> { Name = "Ten", Value = 10 };

            // Act
            var result = choice.Value;

            // Assert
            Assert.Equal(10L, result);
        }

        [Fact]
        public void Create_UnsupportedType_Throws()
        {
            // Act
            var exception = Assert.Throws<TypeInitializationException>(() => new SlashCommandChoice<int>());

            // Assert
            Assert.IsType<NotSupportedException>(exception.InnerException);
        }
    }
}
