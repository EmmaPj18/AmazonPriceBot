using System.Threading;
using System.Threading.Tasks;
using AmazonPriceBot.Commands;
using AmazonPriceBot.Models;
using MediatR;

namespace AmazonPriceBot.Handlers
{
    public class SearchItemByUrlCommandHandler : IRequestHandler<SearchItemByUrlCommand, AmazonItem>
    {
        public Task<AmazonItem> Handle(SearchItemByUrlCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}