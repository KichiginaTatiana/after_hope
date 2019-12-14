using System;
using AfterHope.Commands.Menus.Inline;
using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Storing;

namespace AfterHope.Commands.Executors
{
    public class ShowCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        public ShowCommandExecutor(IPersonRepository personRepository) : base(personRepository)
        {
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            switch (command.Args.Length)
            {
                case 1:
                    return ShowOne(command, meta, syntax);
                default:
                    throw new ArgumentException();
            }
        }

        private CommandResult ShowOne(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            var person = PersonRepository.Read(command.Args[0]);
            return CommandResult.AsSucceed($"{person.Name}", inlineMenu: CreateMenu(syntax), update: true);
        }

        protected InlineMenu CreateMenu(ICommandSyntax commandSyntax)
        {
            var startCommandName = commandSyntax.GetCommandName<DefaultStartSuperCommandExecutor>();
            var findCommandName = commandSyntax.GetCommandName<FindCommandExecutor>();
            var downloadCardCommandName = commandSyntax.GetCommandName<DownloadCardCommandExecutor>();
            var downloadPosterCommandName = commandSyntax.GetCommandName<DownloadPosterCommandExecutor>();

            var builder = InlineMenu.Build();
            builder.AddRow().WithCell("Найти другого узника совести", findCommandName);
            builder.AddRow().WithCell("Скачать карточку для печати", downloadCardCommandName);
            builder.AddRow().WithCell("Скачать плакат для печати", downloadPosterCommandName);
            builder.AddRow().WithCell("В начало", startCommandName);
            return builder.Create();
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.Show;
    }
}