using AmazonPriceBot.Models;
using MediatR;

namespace AmazonPriceBot.Commands
{
    public record SearchItemByNameCommand(string Name) : IRequest<AmazonItem>;

}