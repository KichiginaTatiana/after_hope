using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data.Repositories;

namespace AfterHope.Commands.Executors
{
    public class ProcessLetterCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        private readonly ILetterRepository letterRepository;
        private const long ProcessedLettersChatId = -366350033;

        public ProcessLetterCommandExecutor(IPersonRepository personRepository,
            IPosterRepository posterRepository,
            ILetterRepository letterRepository) : base(personRepository, posterRepository)
        {
            this.letterRepository = letterRepository;
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            var letterId = command.Args[0];
            var letter = letterRepository.Read(letterId);
            var person = PersonRepository.Read(letter.PersonId);

            letter.IsProcessed = true;
            letterRepository.Write(letter);

            var response = $"Письмо для {person.Name} от [{letter.SenderUserName}](tg://user?id={letter.SenderId}) обработано\n\n{letter.Text}";
            return CommandResult.AsSucceed(response, useMarkdown: true, chatId: ProcessedLettersChatId);
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.ProcessLetter;
    }
}