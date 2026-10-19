using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AwesomeChatBot.ApiWrapper;
using AwesomeChatBot.Commands;
using AwesomeChatBot.Commands.Handlers;

namespace AwesomeChatBot.Tests
{
    internal class FakeInvocation : SlashCommandInvocation
    {
        public FakeInvocation(string commandName = "test", IReadOnlyDictionary<string, object> options = null)
        {
            CommandName = commandName;
            Options = options ?? new Dictionary<string, object>();
        }

        public List<string> Responses { get; } = new List<string>();

        public override string CommandName { get; }
        public override User Author => null;
        public override Channel Channel => null;
        public override IReadOnlyDictionary<string, object> Options { get; }

        public override Task RespondAsync(SendMessage message)
        {
            Responses.Add(message.Content);
            return Task.CompletedTask;
        }
    }

    internal class FakeSlashCommand : Command, ISlashCommand
    {
        public FakeSlashCommand(string slashName = "test", Exception throws = null)
        {
            SlashName = slashName;
            Throws = throws;
        }

        public Exception Throws { get; }

        public List<SlashCommandInvocation> Executions { get; } = new List<SlashCommandInvocation>();

        public override string Name => $"FakeSlash-{SlashName}";
        public string SlashName { get; }
        public string SlashDescription => "A fake slash command";
        public IReadOnlyList<SlashCommandOption> Options => Array.Empty<SlashCommandOption>();

        public Task ExecuteSlashCommandAsync(SlashCommandInvocation invocation)
        {
            Executions.Add(invocation);
            if (Throws != null)
                throw Throws;
            return Task.CompletedTask;
        }
    }

    internal class FakeRegexCommand : Command, IRegexCommand
    {
        public FakeRegexCommand(params string[] patterns) => Regex = new List<string>(patterns);

        public bool Result { get; set; } = true;

        public List<Match> Executions { get; } = new List<Match>();

        public override string Name => "FakeRegex";
        public List<string> Regex { get; }

        public Task<bool> ExecuteRegexCommandAsync(ReceivedMessage receivedMessage, Match regexMatch)
        {
            Executions.Add(regexMatch);
            return Task.FromResult(Result);
        }
    }

    internal class FakeCommand : Command
    {
        public override string Name => "FakePlain";
    }

    internal class FakeReceivedMessage : ReceivedMessage
    {
        public FakeReceivedMessage(string content)
            : base(null) => Content = content;

        public override IList<Attachment> Attachments { get; set; } = new List<Attachment>();
        public override string Content { get; set; }
        public override string Id => "1";
        public override User Author => null;
        public override DateTime PostedOnUtc => DateTime.UtcNow;
        public override Channel Channel => null;
        public override bool IsBotMentioned => true;

        public override Task PublishAsync() => Task.CompletedTask;
    }

    internal class FakeApiWrapper : ApiWrapper.ApiWrapper
    {
        public override string Name => "Fake";
        public override MessageFormatter MessageFormatter => null;

        public override Task<IList<Server>> GetAvailableServersAsync() =>
            Task.FromResult<IList<Server>>(new List<Server>());
    }

    internal class FakeUser : User
    {
        public FakeUser()
            : base(null) { }

        public override string UserID => "1";
        public override string Name => "user";
        public override string UniqueUserName => "user";
        public override IReadOnlyList<UserRole> Roles => Array.Empty<UserRole>();

        public override string GetMention() => "@user";

        public override Task AddRoleAsync(string roleName) => Task.CompletedTask;

        public override Task RemoveRoleAsync(string roleName) => Task.CompletedTask;
    }

    internal class FakeAttachment : Attachment
    {
        public FakeAttachment()
            : base(null) { }

        public override string Name { get; set; } = "file.txt";
        public override byte[] Content { get; set; } = Array.Empty<byte>();
    }
}
