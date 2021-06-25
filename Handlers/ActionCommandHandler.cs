using System;
using System.Threading;
using System.Threading.Tasks;
using AmazonPriceBot.Commands;
using AmazonPriceBot.Models;
using EnumsNET;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AmazonPriceBot.Handlers
{
    public class ActionCommandHandler : IRequestHandler<ActionCommand, Message>
    {
        readonly IMediator _mediator;
        readonly ITelegramBotClient _bot;

        public ActionCommandHandler(IMediator mediator,ITelegramBotClient bot)
        {   
            _mediator = mediator;
            _bot = bot;
        }

        public async Task<Message> Handle(ActionCommand request, CancellationToken cancellationToken)
        {
            (Actions action, Update update) = request;

            var message = update.Message;
            var user = await _bot.GetMeAsync(cancellationToken: cancellationToken);

            var sendMessage = await _mediator.Send<string>(action switch
            {
                Actions.search => new SearchItemCommand(message),
                Actions.help => new HelpCommand(user, message),
                _ => new HelpCommand(user, message),
            }, cancellationToken: cancellationToken);

            return await _bot.SendTextMessageAsync(
                message.Chat,
                sendMessage,
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken);
        }
    }
}