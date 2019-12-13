using System;
using System.Linq;
using AfterHope.Commands.Parsing.Syntax;

namespace AfterHope.Commands.Executors
{
    public class DefaultHelpSuperCommandExecutor : ISuperCommandExecutor
    {
        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax1)
        {
            var commands = syntax
                .GetCommands()
                .Select(c => $"/{c.Name} {c.Help}")
                .ToArray();

            return CommandResult.AsSucceed(string.Join(Environment.NewLine, commands));
        }

        public bool AppliesTo(CommandType commandType) => true;

        public ICommandExecutor UseSyntax(ICommandSyntax syntax)
        {
            this.syntax = syntax;
            return this;
        }

        private ICommandSyntax syntax;
    }
}