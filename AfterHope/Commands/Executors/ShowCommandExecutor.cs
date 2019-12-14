using System;
using System.Linq;
using AfterHope.Commands.Menus.Inline;
using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data.Models;
using AfterHope.Data.Repositories;

namespace AfterHope.Commands.Executors
{
    public class ShowCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        public ShowCommandExecutor(IPersonRepository personRepository, 
            IPosterRepository posterRepository) : base(personRepository, posterRepository)
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
            var personId = command.Args[0];
            var person = PersonRepository.Read(personId);
            return CommandResult.AsSucceed(GetPersonString(person), inlineMenu: CreateMenu(syntax, personId), update: true);
        }

        private static string GetPersonString(Person person) =>
            $"{person.Name}\n{person.Lawsuit}\n{person.Address}\n{person.Birthday}\n{person.Info}\n{person.Type}\n{person.Status}";

        protected InlineMenu CreateMenu(ICommandSyntax commandSyntax, string personId)
        {
            var startCommandName = commandSyntax.GetCommandName<DefaultStartSuperCommandExecutor>();
            var findCommandName = commandSyntax.GetCommandName<FindCommandExecutor>();
            var downloadCardCommandName = commandSyntax.GetCommandName<DownloadCardCommandExecutor>();
            var downloadPosterCommandName = commandSyntax.GetCommandName<DownloadPosterCommandExecutor>();

            var personPosters = GetPersonPosters(personId).ToArray();

            var builder = InlineMenu.Build();
            builder.AddRow().WithCell("Найти другого узника совести", findCommandName);
            builder.AddRow().WithCell("Скачать карточку для печати", downloadCardCommandName);
            if (personPosters.Any())
                builder.AddRow().WithCell("Скачать плакат для печати", downloadPosterCommandName, personPosters[0]);
            builder.AddRow().WithCell("В начало", startCommandName);
            return builder.Create();
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.Show;
    }
}