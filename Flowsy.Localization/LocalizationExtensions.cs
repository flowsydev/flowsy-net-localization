using System.Globalization;
using System.Reflection;
using System.Resources;
using Microsoft.Extensions.Localization;

namespace Flowsy.Localization;

public static class LocalizationExtensions
{
    private static readonly IDictionary<Assembly, IDictionary<string, ResourceManager>> SharedResourceManagerGroups 
        = new Dictionary<Assembly, IDictionary<string, ResourceManager>>();
    
    public static ResourceManager GetSharedResourceManager(string baseName, Assembly? assembly = default)
    {
        if (string.IsNullOrEmpty(baseName))
            throw new ArgumentException(null, nameof(baseName));
        
        assembly ??= Assembly.GetCallingAssembly();
        
        if (!SharedResourceManagerGroups.TryGetValue(assembly, out var resourceManagers))
        {
            resourceManagers = new Dictionary<string, ResourceManager>();
            SharedResourceManagerGroups.Add(assembly, resourceManagers);
        }

        if (resourceManagers.TryGetValue(baseName, out var resourceManager))
            return resourceManager;

        resourceManager = new ResourceManager(baseName, assembly);
        resourceManagers.Add(baseName, resourceManager);
        return resourceManager;
    }

    public static string Localize(this string str, string? resourceBaseName = default)
        => str.Localize(Assembly.GetCallingAssembly(), resourceBaseName);

    public static string Localize(this string str, Assembly assembly, string? resourceBaseName = default)
    {
        try
        {
            if (resourceBaseName is not null)
                return GetSharedResourceManager(resourceBaseName, assembly).GetString(str) ?? str;
            
            var assemblyName = assembly.GetName();
            var baseName = $"{assemblyName.Name}.{LocalizationResources.DefaultResourceBaseName}";
            return GetSharedResourceManager(baseName, assembly).GetString(str) ?? str;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.ToString());
            return str;
        }
    }
    
    public static string GetStringValue(this IStringLocalizer stringLocalizer, string name) =>
        stringLocalizer.GetString(name).Value;
}