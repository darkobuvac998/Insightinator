using System.Reflection;

namespace Insightinator.API;

public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
