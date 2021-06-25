using System.Threading;
using System.Threading.Tasks;
using AmazonPriceBot.Commands;
using AmazonPriceBot.Models;
using MediatR;

namespace AmazonPriceBot.Handlers
{
    public class SearchItemByNameCommandHandler : IRequestHandler<SearchItemByNameCommand, AmazonItem>
    {
        public Task<AmazonItem> Handle(SearchItemByNameCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}