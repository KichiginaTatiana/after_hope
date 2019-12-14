using System.Linq;
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

        public byte[] FileContent { get; set; }

        public bool IsDocumentResult => FileContent?.Any() ?? false;

        public string FileName { get; set; }

        public string PhotoId { get; set; }

        public bool IsPhotoResult => PhotoId != null;

        public long? ChatId { get; set; }

        public CommandResult AdditionalMessage { get; set; }

        public static CommandResult AsSucceed(string response = null, bool useMarkdown = false,
            InlineMenu inlineMenu = null, bool update = false, byte[] fileContent = null, string fileName = null,
            string photoId = null, long? chatId = null) =>
            new CommandResult
            {
                Success = true,
                Response = response ?? "Сделано!",
                UseMarkdown = useMarkdown,
                InlineMenu = inlineMenu,
                UpdatePreviousMessage = update,
                FileContent = fileContent,
                FileName = fileName,
                PhotoId = photoId,
                ChatId = chatId
            };

        public static CommandResult AsFailed(string message, InlineMenu inlineMenu = null, bool update = false) =>
            new CommandResult
            {
                ErrorMessage = message,
                InlineMenu = inlineMenu,
                UpdatePreviousMessage = update,
            };
    }
}