using AfterHope.Commands.Menus.Inline;
using Telegram.Bot.Types.ReplyMarkups;

namespace AfterHope.Commands
{
    public interface IInlineKeyboardMarkupBuilder
    {
        InlineKeyboardMarkup Build(InlineMenu menu);
    }
}