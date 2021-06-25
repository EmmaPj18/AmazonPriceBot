using System;
using System.Xml.Linq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmazonPriceBot.Commands;
using AmazonPriceBot.Models;
using MediatR;

namespace AmazonPriceBot.Handlers
{
    public class SearchItemCommandHandler : IRequestHandler<SearchItemCommand, string>
    {
        readonly IMediator _mediator;

        public SearchItemCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Handle(SearchItemCommand request, CancellationToken cancellationToken)
        {
            var message = request.Message.Text.Split(" ").Skip(1).ToArray();

            var search = message[0];

            var item = await _mediator.Send<AmazonItem>(
                Uri.TryCreate(search, UriKind.Absolute, out var url)
                ? new SearchItemByUrlCommand(url)
                : new SearchItemByNameCommand(search),
                cancellationToken: cancellationToken);

            return $"[{item.Name}]({item.Url})\nPrice: {item.Price:C}";
        }
    }
}