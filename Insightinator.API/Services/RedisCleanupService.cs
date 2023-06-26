using StackExchange.Redis;

namespace Insightinator.API.Services;

public class RedisCleanupService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RedisCleanupService> _logger;

    public RedisCleanupService(
        IServiceProvider serviceProvider,
        ILogger<RedisCleanupService> logger
    )
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        await ClearRedisDb();
    }

    private async Task ClearRedisDb()
    {
        using var scope = _serviceProvider.CreateScope();
        var database = scope.ServiceProvider?.GetService<IConnectionMultiplexer>()!.GetDatabase();

        var keys = (string[])await database!.ExecuteAsync("KEYS", "*");

        _logger.LogInformation("Cleaning database and keys {@keys}", keys);

        foreach (var key in keys)
        {
            await database.KeyDeleteAsync(key);
        }
    }
}
