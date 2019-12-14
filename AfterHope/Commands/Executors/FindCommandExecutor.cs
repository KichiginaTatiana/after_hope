using System;
using System.Linq;
using AfterHope.Commands.Menus.Inline;
using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data;

namespace AfterHope.Commands.Executors
{
    public class FindCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        public FindCommandExecutor(IPersonRepository personRepository) : base(personRepository)
        {
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            switch (command.Args.Length)
            {
                case 0:
                    return Start(meta, syntax);
                case 1:
                    return ShowList(command, meta, syntax);
                default:
                    throw new ArgumentException();
            }
        }

        private CommandResult Start(CommandMeta meta, ICommandSyntax syntax) =>
            CommandResult.AsSucceed("Отправьте боту фамилию узника совести или смотри список по делам",
                inlineMenu: CreateLawsuitsMenu(syntax, GetLawsuits(meta)),
                update: meta.FromInlineMenu);

        private CommandResult ShowList(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            var query = command.Args[0];
            var persons = query == "all"
                ? PersonRepository.ReadAll()
                : PersonRepository.Select(query).Concat(PersonRepository.SelectByLawsuit(query)).GroupBy(x => x.Id)
                    .Select(x => x.First()).ToList();

            var personsString = string.Join("\n", persons.Select((p, i) => $"{i + 1}. {p.Name} /show_{p.Id}"));

            return CommandResult.AsSucceed($"Найдено {persons.Count}:\n{personsString}",
                inlineMenu: CreateMenu(syntax));
        }

        private static InlineMenu CreateMenu(ICommandSyntax commandSyntax)
        {
            var startCommandName = commandSyntax.GetCommandName<DefaultStartSuperCommandExecutor>();
            var findCommandName = commandSyntax.GetCommandName<FindCommandExecutor>();
            var downloadPosterCommandName = commandSyntax.GetCommandName<DownloadPosterCommandExecutor>();

            var builder = InlineMenu.Build();
            builder.AddRow().WithCell("Найти другого узника совести", findCommandName);
            builder.AddRow().WithCell("Скачать плакат для печати", downloadPosterCommandName);
            builder.AddRow().WithCell("В начало", startCommandName);
            return builder.Create();
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.Find;
    }
}
