namespace Insightinator.API.Extensions;

public static class UtilExtensions
{
    public static double Round(this double value, int decimals)
    {
        return Math.Round(value, decimals);
    }
}
