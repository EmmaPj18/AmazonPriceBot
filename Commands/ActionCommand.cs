using AmazonPriceBot.Models;
using MediatR;
using Telegram.Bot.Types;

namespace AmazonPriceBot.Commands
{
    public record ActionCommand(Actions Actions, Update Update) : IRequest<Message>;
}