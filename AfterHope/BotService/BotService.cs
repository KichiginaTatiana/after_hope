using System;
using System.IO;
using System.Threading.Tasks;
using AfterHope.Commands;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AfterHope.BotService
{
    public class BotService : IBotService
    {
        private readonly TelegramBotClient bot;
        private readonly ICommandManager commandManager;
        private readonly IInlineKeyboardMarkupBuilder inlineKeyboardMarkupBuilder;

        private const long AdminChatId = -350634383;

        public BotService(
            IBotSettings settings,
            ICommandManager commandManager,
            IInlineKeyboardMarkupBuilder inlineKeyboardMarkupBuilder)
        {
            this.commandManager = commandManager;
            this.inlineKeyboardMarkupBuilder = inlineKeyboardMarkupBuilder;

            bot = settings.UseProxy
                ? new TelegramBotClient(settings.Token, new HttpToSocks5Proxy(settings.ProxyHostName, settings.ProxyPort){ResolveHostnamesLocally = true})
                : new TelegramBotClient(settings.Token);

            bot.OnMessage += OnMessageReceived;
            bot.OnMessageEdited += OnMessageReceived;

            bot.OnCallbackQuery += OnBotOnCallbackQueryReceived;
        }

        public void Start() => bot.StartReceiving();

        public void Stop() => bot.StopReceiving();

        public async Task Ping() => await bot.GetMeAsync();

        private async void OnBotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            try
            {
                await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
                var commandResult = commandManager.Execute(callbackQuery.Data, BuildScope(callbackQuery.Message, callbackQuery.From, true));

                if (commandResult.UpdatePreviousMessage)
                    await bot.EditMessageTextAsync(
                        callbackQuery.Message.Chat.Id,
                        callbackQuery.Message.MessageId,
                        commandResult.Success ? commandResult.Response : commandResult.ErrorMessage,
                        commandResult.UseMarkdown ? ParseMode.Markdown : ParseMode.Default,
                        replyMarkup: inlineKeyboardMarkupBuilder.Build(commandResult.InlineMenu));
                else
                    await SendResponse(commandResult, callbackQuery.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static CommandMeta BuildScope(Message message, User sender, bool inlineMenu = false) =>
            new CommandMeta
            {
                UserId = sender.Id.ToString(),
                UserName = sender.FirstName,
                NickName = sender.Username,
                FromInlineMenu = inlineMenu,
                PhotoId = message.Type == MessageType.Photo ? message.Photo[0].FileId : null
            };

        private async void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            await ExecuteCommand(messageEventArgs, message);
        }

        private async Task ExecuteCommand(MessageEventArgs messageEventArgs, Message message)
        {
            if (message?.Type == MessageType.Text && message.Chat.Type == ChatType.Private)
            {
                await ExecutePrivateConversationCommands(messageEventArgs, message);
            }

            if ((message?.Type == MessageType.Text || message?.Type == MessageType.Photo) &&
                (message.Chat.Type == ChatType.Group || message.Chat.Type == ChatType.Supergroup)
                && message.Chat.Id == AdminChatId)
            {
                await AddPerson(messageEventArgs, message);
            }
        }

        private async Task AddPerson(MessageEventArgs messageEventArgs, Message message)
        {
            message.Text = "/add " + (message.Type == MessageType.Photo ? message.Caption : message.Text);
            await ExecutePrivateConversationCommands(messageEventArgs, message);
        }

        private async Task ExecutePrivateConversationCommands(MessageEventArgs messageEventArgs, Message message)
        {
            try
            {
                var commandMeta = BuildScope(messageEventArgs.Message, messageEventArgs.Message.From);
                var commandResult = commandManager.Execute(message.Text, commandMeta);
                await SendResponse(commandResult, message);
            }
            catch (Exception e)
            {
                await bot.SendTextMessageAsync(
                    message.Chat.Id,
                    "Oooops! Something went horribly terribly awfully wrong!"
                );
            }
        }

        private async Task SendResponse(CommandResult commandResult, Message message)
        {
            var chatId = commandResult.ChatId ?? message.Chat.Id;

            if (commandResult.AdditionalMessage != null)
                await SendResponse(commandResult.AdditionalMessage, message);

            if (commandResult.IsDocumentResult)
                await bot.SendDocumentAsync(chatId,
                    new InputMedia(new MemoryStream(commandResult.FileContent), commandResult.FileName));
            else if (commandResult.IsPhotoResult)
                await bot.SendPhotoAsync(chatId, new InputMedia(commandResult.PhotoId),
                    commandResult.Success ? commandResult.Response : commandResult.ErrorMessage);
            else
                await bot.SendTextMessageAsync(
                    chatId,
                    commandResult.Success ? commandResult.Response : commandResult.ErrorMessage,
                    commandResult.UseMarkdown ? ParseMode.Markdown : ParseMode.Default,
                    replyMarkup: inlineKeyboardMarkupBuilder.Build(commandResult.InlineMenu));
        }
    }
}