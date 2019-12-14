using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data;
using AfterHope.Exporting;

namespace AfterHope.Commands.Executors
{
    public class DownloadListCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        private readonly IPersonListExporter exporter;

        public DownloadListCommandExecutor(IPersonRepository personRepository,
            IPersonListExporter exporter) : base(personRepository)
        {
            this.exporter = exporter;
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            var export = exporter.Export(command.Args[0]);
            return CommandResult.AsSucceed(string.Empty, fileContent: export, fileName: $"{command.Args[0]}.csv");
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.DownloadList;
    }
}