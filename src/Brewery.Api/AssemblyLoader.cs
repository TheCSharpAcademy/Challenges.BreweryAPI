using System.Reflection;

namespace Brewery.Api;

public static class AssemblyLoader
{
    public static List<Assembly> GetAssemblies()
    {
        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .OrderBy(assembly => assembly.Location)
            .ToList();
        
        var locations = assemblies
            .Where(a => !a.IsDynamic)
            .Select(a => a.Location)
            .ToArray();
        
        var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(f => !locations.Contains(f, StringComparer.InvariantCultureIgnoreCase))
            .ToList();
        
        files.ForEach(f => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(f))));
        
        return assemblies;
    }
}