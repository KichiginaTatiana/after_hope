using AfterHope.Commands.Parsing.Syntax;

namespace AfterHope.Commands.Executors
{
    public interface ISuperCommandExecutor : ICommandExecutor
    {
        ICommandExecutor UseSyntax(ICommandSyntax syntax);
    }
}