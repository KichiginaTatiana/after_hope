using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data;
using AfterHope.Data.Models;

namespace AfterHope.Commands.Executors
{
    public class AddPersonCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        private readonly IPersonParser parser;

        public AddPersonCommandExecutor(IPersonRepository personRepository,
            IPersonParser parser) : base(personRepository)
        {
            this.parser = parser;
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            Person person;
            try
            {
                person = parser.Parse(command.Args[0], meta.PhotoId);
            }
            catch
            {
                return CommandResult.AsFailed("Ожидаемый формат:\n\n[Фото]\nФИО\nДело\nАдрес\n[День рождения]\n[Информация]\n[Тип]\n[Статус]");
            }

            PersonRepository.Write(person);

            return CommandResult.AsSucceed("Добавлено");
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.Add;
    }
}