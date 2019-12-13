using System.Linq;
using AfterHope.Commands.CommandGeneration;
using AfterHope.Commands.Menus.Inline;
using Telegram.Bot.Types.ReplyMarkups;

namespace AfterHope.Commands
{
    public class InlineKeyboardMarkupBuilder : IInlineKeyboardMarkupBuilder
    {
        private readonly ICommandGenerator commandGenerator;

        public InlineKeyboardMarkupBuilder(ICommandGenerator commandGenerator)
        {
            this.commandGenerator = commandGenerator;
        }

        public InlineKeyboardMarkup Build(InlineMenu menu)
        {
            if (menu == null)
                return null;

            return new InlineKeyboardMarkup(
                menu.Grid.Select(row => row.Cells.Select(cell =>
                    {
                        var commandLine = commandGenerator.Generate(cell.CommandName, cell.CommandArgs);
                        return InlineKeyboardButton.WithCallbackData(cell.Title, commandLine);
                    })
                ));
        }
    }
}