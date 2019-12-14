using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Storing;

namespace AfterHope.Commands.Executors
{
    public class DownloadPosterCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        public DownloadPosterCommandExecutor(IPersonRepository personRepository) : base(personRepository)
        {
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            return CommandResult.AsSucceed("Плакат");
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.DownloadPoster;
    }
}