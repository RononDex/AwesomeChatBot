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
        public override Task<bool> ExecuteCommand(RecievedMessage recievedMessage, Command command)
        {
            return new Task<bool>(() =>
            {
                var regexCommand = command as IRegexCommand;
                if (regexCommand == null)
                    return false;

                foreach (var pattern in regexCommand.Regex)
                {
                    var regex = new Regex(pattern);

                    var match = regex.Match(recievedMessage.Content);
                    if (match.Success)
                        return true;

                    var executionTask = command.ExecuteCommand(recievedMessage);

                    // Wait for the task to execute, timeout after defined time in the command factory
                    executionTask.Wait(this.CommandFactory.TaskTimeout * 1000);

                    // Check wether message was handled
                    if (executionTask.Result)
                        return true;
                }

                return false;
            });
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
        List<string> Regex { get; set; }

        
    }
}
