using AfterHope.Commands.Menus.Inline;

namespace AfterHope.Commands
{
    public class CommandResult
    {
        public bool Success { get; set; }
        public string Response { get; set; }
        public string ErrorMessage { get; set; }
        public bool UseMarkdown { get; set; }
        public InlineMenu InlineMenu { get; set; }
        public bool UpdatePreviousMessage { get; set; }

        public static CommandResult AsSucceed(string response = null, bool useMarkdown = false, InlineMenu inlineMenu = null, bool update = false)
        {
            return new CommandResult
            {
                Success = true,
                Response = response ?? "Сделано!",
                UseMarkdown = useMarkdown,
                InlineMenu = inlineMenu,
                UpdatePreviousMessage = update,
            };
        }

        public static CommandResult AsFailed(string message, InlineMenu inlineMenu = null, bool update = false)
        {
            return new CommandResult
            {
                ErrorMessage = message,
                InlineMenu = inlineMenu,
                UpdatePreviousMessage = update,
            };
        }
    }
}