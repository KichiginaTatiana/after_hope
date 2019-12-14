using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data.Repositories;
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
            var exportAll = command.Args[0] == "all";
            var export = exporter.Export(exportAll ? null : command.Args[0]);
            var fileName = exportAll ? "Все" : command.Args[0];
            return CommandResult.AsSucceed(string.Empty, fileContent: export, fileName: $"{fileName}.csv");
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.DownloadList;
    }
}