using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace AmazonPriceBot.Services
{
    public interface ITelegramService
    {
        Task HandleAsync(Update update);
    }
}