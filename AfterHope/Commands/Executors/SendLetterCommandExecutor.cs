using AfterHope.Commands.Menus.Inline;
using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data.Models;
using AfterHope.Data.Repositories;

namespace AfterHope.Commands.Executors
{
    public class SendLetterCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        private readonly ILetterRepository letterRepository;
        private const long UnprocessedLettersChatId = -273013152;
        
        public SendLetterCommandExecutor(IPersonRepository personRepository, 
            IPosterRepository posterRepository,
            ILetterRepository letterRepository) : base(personRepository, posterRepository)
        {
            this.letterRepository = letterRepository;
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            var args = command.Args[0].Split(" ", 2);
            var personId = args[0];
            var text = args[1];
            var person = PersonRepository.Read(personId);

            var letter = new Letter
            {
                Id = text.GetHashCode().ToString(),
                Text = text,
                PersonId = personId,
                SenderId = meta.UserId,
                SenderUserName = meta.NickName
            };
            letterRepository.Write(letter);

            var response = $"Письмо для {person.Name} от [{meta.NickName}](tg://user?id={meta.UserId}) \n\n{text}";
            var result = CommandResult.AsSucceed("Отправлено");
            result.AdditionalMessage = CommandResult.AsSucceed(response, useMarkdown: true,
                inlineMenu: CreateMenu(syntax, letter.Id),
                chatId: UnprocessedLettersChatId);
            return result;
        }

        private static InlineMenu CreateMenu(ICommandSyntax commandSyntax, string letterId)
        {
            var processCommandName = commandSyntax.GetCommandName<ProcessLetterCommandExecutor>();
            var builder = InlineMenu.Build();
            builder.AddRow().WithCell("Обработано", processCommandName, letterId);
            return builder.Create();
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.SendLetter;
    }
}