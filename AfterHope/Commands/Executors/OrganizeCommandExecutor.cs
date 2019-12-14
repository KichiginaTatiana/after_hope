using System;
using AfterHope.Commands.Menus.Inline;
using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data;

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
            CommandResult.AsSucceed("Выберите действие", inlineMenu: CreateMenu(command, syntax), update: meta.FromInlineMenu);

        private CommandResult SelectLawsuit(CommandMeta meta, ICommandSyntax syntax) =>
            CommandResult.AsSucceed("Выберите дело",
                inlineMenu: CreateLawsuitsMenu(syntax, GetLawsuits(meta)),
                update: meta.FromInlineMenu);

        protected InlineMenu CreateMenu(Command command, ICommandSyntax commandSyntax)
        {
            var startCommandName = commandSyntax.GetCommandName<DefaultStartSuperCommandExecutor>();
            var organizeCommandName = commandSyntax.GetCommandName<OrganizeCommandExecutor>();
            var downloadListCommandName = commandSyntax.GetCommandName<DownloadListCommandExecutor>();
            var downloadPosterCommandName = commandSyntax.GetCommandName<DownloadPosterCommandExecutor>();

            var lawsuit = command.Args[0];
            var builder = InlineMenu.Build();
            builder.AddRow().WithCell("Найти другую группу", organizeCommandName);
            builder.AddRow().WithCell("Скачать cписок для печати", downloadListCommandName, lawsuit);
            builder.AddRow().WithCell("Скачать плакат для печати", downloadPosterCommandName, lawsuit);
            builder.AddRow().WithCell("В начало", startCommandName);
            return builder.Create();
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.Organize;
    }
}