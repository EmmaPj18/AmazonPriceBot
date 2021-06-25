using System;
using System.Threading.Tasks;
using AmazonPriceBot.Models;
using EnumsNET;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AmazonPriceBot.Services
{
    public class TelegramService : ITelegramService
    {
        readonly ITelegramBotClient _bot;
        readonly ILogger<TelegramService> _logger;

        public TelegramService(ITelegramBotClient bot, ILogger<TelegramService> logger)
        {
            _bot = bot;
            _logger = logger;
        }

        public async Task HandleAsync(Update update)
        {
            if (update is null) return;

            if (update.Type != UpdateType.Message) return;

            var chat = update.Message.Chat;
            var message = update.Message;
            var actionString = message.Text.Split(' ')[0];
            var action = Enum.TryParse(actionString, out Actions rAction) ? rAction : Actions.help;

            _logger.LogInformation($"Bot action {action} triggered by {chat.Username}[{chat.Id}]");

            string sendMessage = await (action switch
            {
                Actions.help => Help(message),
                _ => Help(message),
            });

            await _bot.SendTextMessageAsync(chat, sendMessage);
        }

        async Task<string> Help(Message message)
        {
            var chat = message.Chat;
            var me = await _bot.GetMeAsync();
            var sendMessage = $"Hello {chat.Username}!. I'm {me.Username}, an amazon price checker and tracker.\n";
            sendMessage += "These are my list of available commands:\n";
            foreach (var item in Enum.GetValues<Actions>())
            {
                var command = item.GetName();
                var description = item.AsString(EnumFormat.Description);
                sendMessage += $"/{command} - {description}";
            }

            return sendMessage;
        }
    }

    
}