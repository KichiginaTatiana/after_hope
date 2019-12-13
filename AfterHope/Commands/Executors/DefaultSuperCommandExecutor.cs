using AfterHope.Commands.Parsing.Syntax;

namespace AfterHope.Commands.Executors
{
    public class DefaultSuperCommandExecutor : ISuperCommandExecutor
    {
        private ICommandSyntax syntax;

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax1)
        {
            return CommandResult.AsSucceed($"Unknown command. Type '/{syntax.HelpCommandName}' for help");
        }

        public bool AppliesTo(CommandType commandType) => true;

        public ICommandExecutor UseSyntax(ICommandSyntax syntax)
        {
            this.syntax = syntax;
            return this;
        }
    }
}