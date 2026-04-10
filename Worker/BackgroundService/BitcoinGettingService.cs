using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BitcoinApi.Entities;
using System.Text.Json;
using BitcoinApi.Context;
public class BitcoinGettingService : BackgroundService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BitcoinGettingService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public BitcoinGettingService(HttpClient httpClient, ILogger<BitcoinGettingService> logger, IServiceScopeFactory scopeFactory)
    {
        _httpClient = httpClient;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var json = await _httpClient.GetStringAsync(
                    "https://api.coingecko.com/api/v3/simple/price?vs_currencies=usd&ids=bitcoin&x_cg_demo_api_key=CG-d2eQxANr4YpM2fgp75AGfh56",
                    stoppingToken
                );

                _logger.LogInformation("Bitcoin has been loaded: {Json}", json);

                var getBitcoin = JsonSerializer.Deserialize<BitcoinResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
                
                Random random = new Random();
                int randomDelay = random.Next(0, 1000);
                int final = 0;
                if (getBitcoin != null && getBitcoin.Bitcoin != null)
                {
                    final += randomDelay + (int)getBitcoin.Bitcoin.Usd;
                }
                else
                {
                    final += 0;
                }
                _logger.LogInformation($"Calculated Bitcoin price with random delay: {final}, Random Delay: {randomDelay}ms");
                Bitcoin bitcoin = new Bitcoin(final, DateTime.UtcNow);
                final = 0;
                _logger.LogInformation($"Bitcoin deserialized: {bitcoin?.Price}, Time: {bitcoin?.Timestamp}");
                
                if (bitcoin == null)
                    throw new ArgumentNullException("Bitcoin data is null after deserialization");
                
                using var scope = _scopeFactory.CreateScope();
                var workerService = scope.ServiceProvider.GetRequiredService<WorkerService>();
                workerService.AddToDatabase(bitcoin);
                _logger.LogInformation("Bitcoin data has been added to the database");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading Bitcoin data");
            }

            await Task.Delay(20000, stoppingToken);
        }
    }
}