using System;
using System.Threading;
using System.Threading.Tasks;
using AmazonPriceBot.Commands;
using AmazonPriceBot.Models;
using EnumsNET;
using MediatR;

namespace AmazonPriceBot.Handlers
{
    public class HelpCommandHandler : IRequestHandler<HelpCommand, string>
    {
        public Task<string> Handle(HelpCommand request, CancellationToken cancellationToken)
        {
            (var user, var message) = request;

            var chat = message.Chat;
            
            var sendMessage = $"Hello {chat.Username}!. I'm {user.Username}, an amazon price checker and tracker.\n";
            sendMessage += "These are my list of available commands:\n";
            foreach (var item in Enum.GetValues<Actions>())
            {
                var command = item.GetName();
                var description = item.AsString(EnumFormat.Description);
                sendMessage += $"/{command} - {description}\n";
            }

            return Task.FromResult(sendMessage);
        }
    }
}