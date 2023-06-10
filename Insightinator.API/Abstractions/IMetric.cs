namespace Insightinator.API.Abstractions;

public interface IMetric<T>
{
    public T Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }
    protected IMetric<T> Initialize();
}
