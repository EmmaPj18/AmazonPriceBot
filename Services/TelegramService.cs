using System;
using System.Threading.Tasks;
using AmazonPriceBot.Commands;
using AmazonPriceBot.Models;
using EnumsNET;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AmazonPriceBot.Services
{
    public class TelegramService : ITelegramService
    {
       
        readonly ILogger<TelegramService> _logger;
        readonly IMediator _mediator;

        public TelegramService(IMediator mediator, ILogger<TelegramService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task HandleAsync(Update update)
        {
            var chat = update.Message.Chat;
            var message = update.Message;
            var actionString = message.Text.Split(' ')[0];
            var action = Enum.TryParse(actionString, out Actions rAction) ? rAction : Actions.help;

            _logger.LogInformation($"Bot action {action} triggered by {chat.Username}[{chat.Id}]");

            var _ = await _mediator.Send(new ActionCommand(action, update));
        }
    }


}