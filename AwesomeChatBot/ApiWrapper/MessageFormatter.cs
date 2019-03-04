namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// The base class for the message formatter (is responsible for formatting a message for the chat client)
    /// </summary>
    public abstract class MessageFormatter
    {
        /// <summary>
        /// Formats the given message as a quote
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract string Quote(string message);

        /// <summary>
        /// Formats the given message as bold
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract string Bold(string message);

        /// <summary>
        /// Formats the given message as italic
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract string Italic(string message);

        /// <summary>
        /// Formats a message as a code block
        /// </summary>
        /// <param name="message">The message to display as code block</param>
        /// <param name="language">(optional) programming language, like csharp</param>
        /// <returns></returns>
        public abstract string CodeBlock(string message, string language = null);

        /// <summary>
        /// Adds underline to a given text
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract string Underline(string message);
    }
}