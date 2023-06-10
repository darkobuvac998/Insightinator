namespace Insightinator.API.Abstractions;

public interface IMetricCompute<out T, in U>
    where T : IMetric<U>
{
    public T Metric { get; }
    public void ComputeMetric();
    public void ResetMetric();
    public T GetMetricValue();
    public virtual void AddValueToBuffer(params U[] values) { }
}
