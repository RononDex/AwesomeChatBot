using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeChatBot.ApiWrapper;

namespace AwesomeChatBot.Commands.Handlers
{
    /// <summary>
    /// If a Command has this interface, it is registered as a native slash command (e.g. "/weather") with the chat app
    /// </summary>
    public interface ISlashCommand
    {
        /// <summary>
        /// Name of the slash command, lowercase (Discord: ^[a-z0-9_-]{1,32}$)
        /// </summary>
        string SlashName { get; }

        /// <summary>
        /// Description shown in the chat app's command picker
        /// </summary>
        string SlashDescription { get; }

        /// <summary>
        /// The options (parameters) of the command
        /// </summary>
        IReadOnlyList<SlashCommandOption> Options { get; }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="invocation"></param>
        Task ExecuteSlashCommandAsync(SlashCommandInvocation invocation);
    }

    /// <summary>
    /// The type of a slash command option
    /// </summary>
    public enum SlashOptionType
    {
        String,
        Integer,
        Number,
        Boolean,
        User,
        Attachment
    }

    /// <summary>
    /// An option (parameter) of a slash command
    /// </summary>
    public class SlashCommandOption
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public SlashOptionType Type { get; set; }

        public bool Required { get; set; }

        /// <summary>
        /// Fixed values the user picks from. Only for String, Integer and Number options; null or empty allows any value
        /// </summary>
        public IReadOnlyList<SlashCommandChoice> Choices { get; set; }
    }

    /// <summary>
    /// A predefined value of a slash command option. Create it as <see cref="SlashCommandChoice{T}"/>
    /// </summary>
    public abstract class SlashCommandChoice
    {
        /// <summary>
        /// Name shown to the user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value passed to the command, as string, long or double
        /// </summary>
        public object Value => GetValue();

        protected abstract object GetValue();
    }

    /// <summary>
    /// A predefined value of a slash command option. T must match the option type: string, long or double
    /// </summary>
    public class SlashCommandChoice<T> : SlashCommandChoice
    {
        static SlashCommandChoice()
        {
            // Invocation option values are only ever string, long or double, so e.g. an int choice could never be read back
            if (typeof(T) != typeof(string) && typeof(T) != typeof(long) && typeof(T) != typeof(double))
                throw new System.NotSupportedException($"Slash command choices must be string, long or double, not {typeof(T).Name}");
        }

        public new T Value { get; set; }

        protected override object GetValue() => Value;
    }

    /// <summary>
    /// A single execution of a slash command by a user
    /// </summary>
    public abstract class SlashCommandInvocation
    {
        /// <summary>
        /// Name of the invoked slash command
        /// </summary>
        public abstract string CommandName { get; }

        /// <summary>
        /// The user who invoked the command
        /// </summary>
        public abstract User Author { get; }

        /// <summary>
        /// The channel the command was invoked in
        /// </summary>
        public abstract Channel Channel { get; }

        /// <summary>
        /// Option values by name. Values are string, long, double, bool, <see cref="User"/> or <see cref="Attachment"/>
        /// </summary>
        public abstract IReadOnlyDictionary<string, object> Options { get; }

        /// <summary>
        /// Gets an option value, or default if the user did not provide it.
        /// T must match the stored type exactly (e.g. long for Integer options, not int), otherwise default is returned
        /// </summary>
        public T GetOption<T>(string name)
        {
            return Options.TryGetValue(name, out var value) && value is T typed ? typed : default;
        }

        /// <summary>
        /// Replies to the invocation. Can be called multiple times.
        /// </summary>
        public abstract Task RespondAsync(SendMessage message);

        /// <summary>
        /// Replies to the invocation with a text message
        /// </summary>
        public Task RespondAsync(string message)
        {
            return RespondAsync(new SendMessage(message));
        }
    }
}
