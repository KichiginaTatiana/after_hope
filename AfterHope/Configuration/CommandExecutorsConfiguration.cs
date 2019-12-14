using AfterHope.Commands.Executors;
using AfterHope.Commands.Parsing.Syntax;
using Grace.DependencyInjection;

namespace AfterHope.Configuration
{
    public static class CommandExecutorsConfiguration
    {
        public static ICommandSyntax Configure(DependencyInjectionContainer container)
        {
            var commandSyntax = container.Locate<ICommandSyntax>();
            commandSyntax.DefineCommand<OrganizeCommandExecutor>("organize", "организовать письма пзк");
            commandSyntax.DefineCommand<FindCommandExecutor>("find", "найти пзк");
            commandSyntax.DefineCommand<DefaultStartSuperCommandExecutor>("start", "start");
            commandSyntax.DefineCommand<ShowCommandExecutor>("show", "show");
            commandSyntax.DefineCommand<DownloadListCommandExecutor>("downloadlist", "downloadlist");
            commandSyntax.DefineCommand<DownloadCardCommandExecutor>("downloadcard", "downloadcard");
            commandSyntax.DefineCommand<DownloadPosterCommandExecutor>("downloadposter", "downloadposter");
            commandSyntax.DefineCommand<AddPersonCommandExecutor>("add", "add");
            return commandSyntax;
        }
    }
}