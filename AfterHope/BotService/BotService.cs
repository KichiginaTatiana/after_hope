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

        public BotService(
            IBotSettings settings,
            ICommandManager commandManager,
            IInlineKeyboardMarkupBuilder inlineKeyboardMarkupBuilder)
        {
            this.commandManager = commandManager;
            this.inlineKeyboardMarkupBuilder = inlineKeyboardMarkupBuilder;

            bot = settings.UseProxy
                ? new TelegramBotClient(settings.Token, new HttpToSocks5Proxy(settings.ProxyHostName, settings.ProxyPort))
                : new TelegramBotClient(settings.Token);

            bot.OnMessage += OnMessageReceived;
            bot.OnMessageEdited += OnMessageReceived;

            bot.OnCallbackQuery += OnBotOnCallbackQueryReceived;
        }

        public void Start()
        {
            bot.StartReceiving();
        }

        public void Stop()
        {
            bot.StopReceiving();
        }
        
        public async Task Ping()
        {
            var me = await bot.GetMeAsync();
        }

        private async void OnBotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            try
            {
                await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
                var commandResult = commandManager.Execute(callbackQuery.Data, BuildScope(callbackQuery.From, true));
                if (commandResult.UpdatePreviousMessage)
                {
                    await bot.EditMessageTextAsync(
                        callbackQuery.Message.Chat.Id,
                        callbackQuery.Message.MessageId,
                        commandResult.Success ? commandResult.Response : commandResult.ErrorMessage,
                        commandResult.UseMarkdown ? ParseMode.Markdown : ParseMode.Default,
                        replyMarkup: inlineKeyboardMarkupBuilder.Build(commandResult.InlineMenu));
                }
                else
                {
                    if (commandResult.IsDocumentResult)
                        await bot.SendDocumentAsync(callbackQuery.Message.Chat.Id,
                            new InputMedia(new MemoryStream(commandResult.FileContent), commandResult.FileName));
                    else
                        await bot.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            commandResult.Success ? commandResult.Response : commandResult.ErrorMessage,
                            commandResult.UseMarkdown ? ParseMode.Markdown : ParseMode.Default,
                            replyMarkup: inlineKeyboardMarkupBuilder.Build(commandResult.InlineMenu)
                        );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static CommandMeta BuildScope(User sender, bool inlineMenu = false)
        {
            return new CommandMeta
            {
                UserId = sender.Id.ToString(),
                UserName = sender.FirstName,
                NickName = sender.Username,
                FromInlineMenu = inlineMenu,
            };
        }

        private async void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            await ExecuteCommand(messageEventArgs, message);
        }

        private async Task ExecuteCommand(MessageEventArgs messageEventArgs, Message message)
        {
            if (message?.Type == MessageType.Text)
            {
                var commandMeta = BuildScope(messageEventArgs.Message.From);
                try
                {
                    var commandResult = commandManager.Execute(message.Text, commandMeta);

                    if (commandResult.IsDocumentResult)
                        await bot.SendDocumentAsync(message.Chat.Id,
                            new InputMedia(new MemoryStream(commandResult.FileContent), commandResult.FileName));
                    else
                        await bot.SendTextMessageAsync(
                            message.Chat.Id,
                            commandResult.Success ? commandResult.Response : commandResult.ErrorMessage,
                            commandResult.UseMarkdown ? ParseMode.Markdown : ParseMode.Default,
                            replyMarkup: inlineKeyboardMarkupBuilder.Build(commandResult.InlineMenu));
                }
                catch (Exception e)
                {
                    await bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Oooops! Something went horribly terribly awfully wrong!"
                    );
                }
            }
        }
    }
}