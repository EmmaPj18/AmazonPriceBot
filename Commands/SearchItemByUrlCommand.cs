using System;
using AmazonPriceBot.Models;
using MediatR;

namespace AmazonPriceBot.Commands
{
    public record SearchItemByUrlCommand(Uri Url) : IRequest<AmazonItem>;
}