using System;
using System.Collections.Generic;
using System.Text;
using AwesomeChatBot.ApiWrapper;

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

        protected override bool CheckIfCommandShouldExecute(RecievedMessage recievedMessage, Command command)
        {
            
        }
    }

    /// <summary>
    /// If a Command has this interface, it means the command will react to some regex message
    /// </summary>
    public interface IRegexCommand
    {
        List<string> Regex { get; set; }
    }
}
