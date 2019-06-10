# AwesomeChatBot

Nuget package: https://www.nuget.org/packages/AwesomeChatBot/

AwesomeChatBot is a chat bot framework that can work with any chat program.
It is built on .net core, and thus OS independent.

**Extensibility**


The framework was built with extensibility in mind. Pretty much any part from this framework can be overriden and tailored to the personal needs. Basically everything is decoupled and talks through standardized interfaces to each other
____________________________________

The set of classes / modules this framework provides by default, will give you a good start and should allow you to implement the common use cases.

**Modules**


This framework uses modules to allow you to define, how your chat commands will be handled, how the bot reacts to them, basically how the user interacts with the bot.

## Getting started
For starters and a quick reference, check my own bot https://github.com/RononDex/Astrobot on how the framework is implemented, it's pretty straight forward!

The most important parts for the chat bot developer are
 - ApiWrappers
 - CommandHandlers
 - Commands

 Here some quick code to initialize the framework:
 ```csharp
var discordWrapper = new DiscordWrapper(discordToken, loggerFactory);
var chatbotFramework = new AwesomeChatBot.AwesomeChatBot(new List<ApiWrapper> { discordWrapper }, loggerFactory, chatbotSettings);
```

This code initializes the framework using the discord API wrapper. `loggerFactory` is the factory used to create loggers, this allows you to use your logging framework of choice (loggerFactory is of type ILoggerFactory from the Microsoft.Extensions.Logging nuget package).
And last `chatBotSettings` is a value object passing on the different config values.

## API Wrappers
Since the framework is decoupled from the chat app API, the framework won't be able to use any chat application by default. For each chat application one will need an API Wrapper doing the talking between your bot / the bot framework and the chat app API.

One API wrapper that was also written by me, is the AwesomeChatBot.Discord wrapper,
 - see github here: https://github.com/RononDex/AwesomeChatBot.Discord
 - or Nuget here: https://www.nuget.org/packages/AwesomeChatBot.Discord/

If a wrapper for yout chat app does not exist, you can create your own wrapper, it's pretty easy, but more on that later.

## Command Handlers
Command handlers are a central part of the framework, they decided when and which command has to be executed. When a message gets passed down from the API wrapper to the framework, it will go though every registered CommandHandler, and check wether one of them says "yes, that message has to execute that command". Every handler has a function `ShouldExecuteCommand` that determines wether a chat message should trigger a given command.
_____________________
Every command handler has a corresponding interface type, that a command has to implement in order to get triggered by that handler. For example the regex command handler has the interface `IRegexCommand` associated with it, which the command then can derive from.

As an example, see the RegexCommandHandler implementation here:
https://github.com/RononDex/AwesomeChatBot/blob/master/AwesomeChatBot/Commands/Handlers/RegexCommandHandler.cs

## Commands
And last but not least the commands. Every command represents an action or several actions that a user can trigger. To create a new command, simply create a new class, and derive it from `AwesomeChatBot.Commands.Command` (which will only add a "Name" property for the command) and also from every command handler type that you want the command to be triggered from. For example:
```csharp
public class TestCommand : AwesomeChatBot.Commands.Command, IRegexCommand
{
    /// <summary>
    /// A list of regex patterns that trigger the command
    /// </summary>
    public List<string> Regex => new List<string>() { "test (?'TestParam'.*\\w)" };

    /// <summary>
    ///  Unique name of the command
    /// </summary>
    public override string Name => "Test";

    /// <summary>
    /// Execute the command
    /// </summary>
    /// <param name="recievedMessage"></param>
    /// <param name="regexMatch"></param>
    /// <returns></returns>
    public Task<bool> ExecuteRegexCommand(ReceivedMessage receivedMessage, Match regexMatch) {
        return Task<bool>.Factory.StartNew(() => {

            var testParam = regexMatch.Groups["TestParam"].Value;

            receivedMessage.Channel.SendMessageAsync(new SendMessage($"IT'S WORKING!!! You entered {testParam}")).Wait();

            return true;
        });

    }
}
```

## Registering Commands and Command Handlers
The commands and the command handlers need to be registered with the framework. These can be registered in the following way:
```csharp
var chatbotFramework = new AwesomeChatBot.AwesomeChatBot(discordWrapper, loggerFactory, chatbotSettings);
chatbotFramework.RegisterCommand(new Commands.TestCommand());
chatbotFramework.RegisterCommandHandler(new AwesomeChatBot.Commands.Handlers.RegexCommandHandler());
```
