namespace Insightinator.API.Abstractions;

public interface IStoreService
{
    Task<T?> GetAsync<T>(string key);

    Task SaveAsync<T>(string key, T value);
}
