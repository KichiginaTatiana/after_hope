using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data.Parsers;
using AfterHope.Data.Repositories;

namespace AfterHope.Commands.Executors
{
    public class AddCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        private readonly IPersonParser personParser;
        private readonly IPosterParser posterParser;

        public AddCommandExecutor(IPersonRepository personRepository,
            IPersonParser personParser,
            IPosterRepository posterRepository,
            IPosterParser posterParser) : base(personRepository, posterRepository)
        {
            this.personParser = personParser;
            this.posterParser = posterParser;
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            try
            {
                var person = personParser.Parse(command.Args[0], meta.PhotoId);
                PersonRepository.Write(person);
                return CommandResult.AsSucceed("Карточка добавлена");
            }
            catch
            {
                try
                {
                    var poster = posterParser.Parse(command.Args[0], meta.PhotoId);
                    PosterRepository.Write(poster);
                    return CommandResult.AsSucceed("Плакат добавлен");
                }
                catch
                {
                    return CommandResult.AsFailed(
                        "Ожидаемый формат карточки:\n\n[Фото]\nФИО\nДело\nАдрес\n[День рождения]\n[Информация]\n[Тип]\n[Статус]\n\nОжидаемый формат плаката:\n\nФото\nФИО|Дело");
                }
            }
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.Add;
    }
}