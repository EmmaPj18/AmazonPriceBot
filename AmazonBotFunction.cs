using System;
using System.Net;
using System.Threading.Tasks;
using AmazonPriceBot.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace AmazonPriceBot
{
    public class AmazonBotFunction
    {
        readonly ILogger<AmazonBotFunction> _logger;
        readonly ITelegramService _service;

        public AmazonBotFunction(ITelegramService service, ILogger<AmazonBotFunction> logger)
        {
            _logger = logger;
            _service = service;
        }

        [Function(nameof(AmazonBotFunction))]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestData request,
            FunctionContext context)
        {
            try
            {
                var body = await request.ReadAsStringAsync();
                var update = JsonConvert.DeserializeObject<Update>(body);

                await _service.HandleAsync(update);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Exception: {exception.Message}");
            }

            return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}