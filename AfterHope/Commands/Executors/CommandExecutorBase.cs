using System.Collections.Generic;
using System.Linq;
using AfterHope.Commands.Menus.Inline;
using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data.Repositories;

namespace AfterHope.Commands.Executors
{
    public abstract class CommandExecutorBase
    {
        protected readonly IPersonRepository PersonRepository;

        protected CommandExecutorBase(IPersonRepository personRepository)
        {
            this.PersonRepository = personRepository;
        }

        protected IEnumerable<string> GetLawsuits(CommandMeta meta) =>
            PersonRepository.ReadAll().GroupBy(x => x.Lawsuit).Select(x => x.Key);

        protected InlineMenu CreateLawsuitsMenu(ICommandSyntax syntax, IEnumerable<string> lawsuits)
        {
            var commandName = syntax.GetCommandName(GetType());
            var beginCommandName = syntax.GetCommandName<DefaultStartSuperCommandExecutor>();
            var builder = InlineMenu.Build();
            foreach (var lawsuit in lawsuits.OrderBy(x => x))
                builder.AddRow().WithCell(lawsuit, commandName, lawsuit);
            builder.AddRow().WithCell("Все", commandName, "all");
            builder.AddRow().WithCell("В начало", beginCommandName);
            return builder.Create();
        }
    }
}