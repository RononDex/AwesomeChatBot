using System;
using System.Collections.Generic;
using System.Text;
using AwesomeChatBot.ApiWrapper;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AwesomeChatBot.Commands.Handlers
{
    public class RegexCommandHandler : CommandHandler
    {
        public RegexCommandHandler(CommandFactory factory) : base(factory)
        {
            // Nothing to do here
        }

        public override string Name { get => "RegexHandler"; }

        public override Type CommandType => typeof(IRegexCommand);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recievedMessage"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public override Task<bool> ExecuteCommand(RecievedMessage recievedMessage, Command command, object parameters)
        {
            #region PRECONDITIONS

            if (!(command is IRegexCommand))
                throw new Exception("Tried to execute the regex handler on a non regex command");
            if (parameters == null || !(parameters is Match))
                throw new Exception("Invalid parameters provided to regex handler");

            #endregion


                return (command as IRegexCommand).ExecuteRegexCommand(recievedMessage, parameters as Match);
        }

        /// <summary>
        /// Determines wether the command should execute for the givem message
        /// </summary>
        /// <param name="recievedMessage"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public override (bool, object) ShouldExecute(RecievedMessage recievedMessage, Command command)
        {
            var regexCommand = command as IRegexCommand;
            if (regexCommand == null)
                return (false, null);

            foreach (var pattern in regexCommand.Regex)
            {
                var regex = new Regex(pattern);

                var match = regex.Match(recievedMessage.Content);
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
        /// <param name="recievedMessage"></param>
        /// <param name="regexMatch"></param>
        /// <returns></returns>
        Task<bool> ExecuteRegexCommand(RecievedMessage recievedMessage, Match regexMatch);
    }
}
