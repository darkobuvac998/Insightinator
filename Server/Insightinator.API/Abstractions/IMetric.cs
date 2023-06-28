namespace Insightinator.API.Abstractions;

public interface IMetric<T>
{
    public Guid Key { get; set; }
    public T Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }
}
