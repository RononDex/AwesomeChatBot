using System;
using System.Collections.Generic;
using AwesomeChatBot.ApiWrapper;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AwesomeChatBot.Commands.Handlers
{
    public class RegexCommandHandler : CommandHandler
    {
        public RegexCommandHandler() : base()
        {
            // Nothing to do here
        }

        public override string Name => "RegexHandler";

        public override Type CommandType => typeof(IRegexCommand);

        /// <summary>
        ///
        /// </summary>
        /// <param name="receivedMessage"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public override Task<bool> ExecuteCommand(ReceivedMessage receivedMessage, Command command, object parameters)
        {
            #region PRECONDITIONS

            if (!(command is IRegexCommand))
                throw new Exception("Tried to execute the regex handler on a non regex command");
            if (parameters == null || !(parameters is Match))
                throw new Exception("Invalid parameters provided to regex handler");

            #endregion

            var task = (command as IRegexCommand)?.ExecuteRegexCommand(receivedMessage, parameters as Match);
            return task;
        }

        /// <summary>
        /// Determines wether the command should execute for the given message
        /// </summary>
        /// <param name="receivedMessage"></param>
        /// <param name="command"></param>
        public override (bool shouldExecute, object parameter) ShouldExecute(ReceivedMessage receivedMessage, Command command)
        {
            if (!(command is IRegexCommand regexCommand))
                return (false, null);

            foreach (var pattern in regexCommand.Regex)
            {
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);

                var match = regex.Match(receivedMessage.Content);
                if (match.Success)
                    return (true, match);
            }

            // If not regex matches, then this command should not execute
            return (false, null);
        }
    }

    /// <summary>
    /// If a Command has this interface, it means the command will react to some regex message
    /// </summary>
    public interface IRegexCommand
    {
        /// <summary>
        /// A list of regex patterns that trigger the command
        /// </summary>
        List<string> Regex { get; }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="receivedMessage"></param>
        /// <param name="regexMatch"></param>
        /// <returns></returns>
        Task<bool> ExecuteRegexCommand(ReceivedMessage receivedMessage, Match regexMatch);
    }
}
