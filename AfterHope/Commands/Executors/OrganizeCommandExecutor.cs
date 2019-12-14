using System;
using AfterHope.Commands.Menus.Inline;
using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Storing;

namespace AfterHope.Commands.Executors
{
    public class OrganizeCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        public OrganizeCommandExecutor(IPersonRepository personRepository) : base(personRepository)
        {
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            switch (command.Args.Length)
            {
                case 0:
                    return SelectLawsuit(meta, syntax);
                case 1:
                    return CreateDocument(command, meta, syntax);
                default:
                    throw new ArgumentException();
            }
        }

        private CommandResult CreateDocument(Command command, CommandMeta meta, ICommandSyntax syntax) =>
            CommandResult.AsSucceed("Выберите", inlineMenu: CreateMenu(syntax), update: meta.FromInlineMenu);

        private CommandResult SelectLawsuit(CommandMeta meta, ICommandSyntax syntax) =>
            CommandResult.AsSucceed("Выберите дело",
                inlineMenu: CreateLawsuitsMenu(syntax, GetLawsuits(meta)),
                update: meta.FromInlineMenu);

        protected InlineMenu CreateMenu(ICommandSyntax commandSyntax)
        {
            var startCommandName = commandSyntax.GetCommandName<DefaultStartSuperCommandExecutor>();
            var findCommandName = commandSyntax.GetCommandName<FindCommandExecutor>();
            var downloadListCommandName = commandSyntax.GetCommandName<DownloadListCommandExecutor>();
            var downloadPosterCommandName = commandSyntax.GetCommandName<DownloadPosterCommandExecutor>();

            var builder = InlineMenu.Build();
            builder.AddRow().WithCell("Найти другого узника совести", findCommandName);
            builder.AddRow().WithCell("Скачать cписок для печати", downloadListCommandName);
            builder.AddRow().WithCell("Скачать плакат для печати", downloadPosterCommandName);
            builder.AddRow().WithCell("В начало", startCommandName);
            return builder.Create();
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.Organize;
    }
}