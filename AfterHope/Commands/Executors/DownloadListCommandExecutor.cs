using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Storing;

namespace AfterHope.Commands.Executors
{
    public class DownloadListCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        public DownloadListCommandExecutor(IPersonRepository personRepository) : base(personRepository)
        {
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            return CommandResult.AsSucceed("Список");
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.DownloadList;
    }
}