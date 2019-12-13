using System;
using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Commands.Parsing.Tokenizing;

namespace AfterHope.Commands
{
    public class CommandManager : ICommandManager
    {
        private readonly ITokenizer tokenizer;
        private readonly ICommandSyntax commandSyntax;

        public CommandManager(
            ITokenizer tokenizer,
            ICommandSyntax commandSyntax)
        {
            this.tokenizer = tokenizer;
            this.commandSyntax = commandSyntax;
        }

        public CommandResult Execute(string commandLine, CommandMeta commandMeta)
        {
            try
            {
                var tokens = tokenizer.Tokenize(commandLine);
                var executor = commandSyntax.GetExecutor(tokens.CommandName);
                return executor.Execute(new Command {Args = tokens.CommandArgs}, commandMeta, commandSyntax);
            }
            catch (CanNotFindCommandNameTokenException)
            {
                try
                {
                    var command = new Command {Args = new[] {commandLine}, Type = CommandType.Show};
                    return commandSyntax.GetExecutor("find").Execute(command, commandMeta, commandSyntax);
                }
                catch (Exception e)
                {
                    return CommandResult.AsFailed(e.Message);
                }
            }
            catch (Exception e)
            {
                return CommandResult.AsFailed(e.Message);
            }
        }
    }
}