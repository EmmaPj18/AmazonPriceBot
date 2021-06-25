using MediatR;
using Telegram.Bot.Types;

namespace AmazonPriceBot.Commands
{
    public record HelpCommand(User User, Message Message) : IRequest<string>;
}