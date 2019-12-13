using AfterHope.Commands.Parsing.Syntax;

namespace AfterHope.Commands.Executors
{
    public interface ICommandExecutor
    {
         CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax);
         bool AppliesTo(CommandType commandType);
    }
}