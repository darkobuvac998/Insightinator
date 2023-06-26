using Insightinator.API.Abstractions;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Insightinator.API.Services;

public class StoreService : IStoreService
{
    private readonly IDatabase _database;
    private readonly ILogger<StoreService> _logger;
    private readonly JsonSerializerSettings _serializerSettings;

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
        var result = await _database.StringGetAsync(key);

        if (result.IsNullOrEmpty)
        {
            return default;
        }

        var keys = (string[])await _database.ExecuteAsync("KEYS", "*");

        _logger.LogInformation(
            "Fetching metric of type {@Type} from chache by key {@Key}. Data: {@Data}",
            typeof(T),
            key,
            result
        );

        return JsonConvert.DeserializeObject<T>(result);
    }

    public async Task SaveAsync<T>(string key, T value)
    {
        _logger.LogInformation(
            "Saving object of type {@Type} into db with value {@Data}",
            typeof(T),
            value
        );

        await _database.StringSetAsync(
            key,
            JsonConvert.SerializeObject(value, _serializerSettings)
        );
    }
}
