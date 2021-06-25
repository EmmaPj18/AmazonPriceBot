using System;
using AmazonPriceBot.Models;
using MediatR;
using Telegram.Bot.Types;

namespace AmazonPriceBot.Commands
{
    public record SearchItemCommand(Message Message) : IRequest<string>;
}