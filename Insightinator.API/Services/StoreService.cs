using Insightinator.API.Abstractions;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Insightinator.API.Services;

public class StoreService : IStoreService
{
    private readonly IDatabase _database;
    private readonly ILogger<StoreService> _logger;
    private readonly JsonSerializerSettings _serializerSettings;
    private static readonly SemaphoreSlim _semaphore = new(1, 1);

    public StoreService(IConnectionMultiplexer redis, ILogger<StoreService> logger)
    {
        _database = redis.GetDatabase();
        _logger = logger;

        _serializerSettings = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        await _semaphore.WaitAsync();
        var result = await _database.StringGetAsync(key);
        _semaphore.Release();

        if (result.IsNullOrEmpty)
        {
            return default;
        }

        _logger.LogDebug(
            "Fetching metric of type {@Type} from chache by key {@Key}. Data: {@Data}",
            typeof(T),
            key,
            result
        );

        return JsonConvert.DeserializeObject<T>(result);
    }

    public async Task SaveAsync<T>(string key, T value)
    {
        _logger.LogDebug(
            "Saving object of type {@Type} into db with value {@Data}",
            typeof(T),
            value
        );

        await _semaphore.WaitAsync();
        await _database.StringSetAsync(
            key,
            JsonConvert.SerializeObject(value, _serializerSettings)
        );

        _semaphore.Release();
    }
}
