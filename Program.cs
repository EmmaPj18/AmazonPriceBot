using System.Threading.Tasks;
using AmazonPriceBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace AmazonPriceBot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureAppConfiguration(config => {
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) => {
                    services.AddHttpClient();
                    var token = context.Configuration["TelegramToken"];
                    services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(token));
                    services.AddScoped<ITelegramService, TelegramService>();
                })                
                .Build();

            await host.RunAsync();
        }


    }
}
