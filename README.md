# Flowsy Localization

Basic functionality for multi-lingual support.

## Requirements
In order to offer multi-lingual support, applications must provide resource files with translations for every supported culture.

To load translations we need to specify an assembly and its resource file base name.

The resource file base name is built taking its path from the root of our project,
using dots instead of directory separators and excluding the culture name and extension.

## Load Translations Using an Extension Method

```csharp
// Given the resource files for en-US and es-MX:
// /path/to/the/project/Resources/Shared.en-us.resx
// /path/to/the/project/Resources/Shared.es-mx.resx

// Load translation specifying and assembly and the resource file base name.
var localizedString1 = "StringId".Localize(typeof(SomeClass).Assembly, "Resources.Shared");

// Load translation specifying only the assembly containing the resource files.
// If we don't specify a resource file base name, the default is "Resources.Shared".
var localizedString2 = "StringId".Localize(typeof(SomeClass).Assembly);

// Load translation from the assembly calling Localize using the default base name for resource files. 
var localizedString3 = "StringId".Localize();
```

## Load Translations Using IStringLocalizer
Another convenient way of loading translations is to stick to the rules defined for [resource file naming](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-6.0#resource-file-naming),
so we can inject instances of IStringLocalizer and get a translated string value directly by doing:
```csharp
[ApiController]
[Route("/api/[controller]")
public class ExampleController : ControllerBase
{
    private readonly IStringLocalizer<ExampleController> _stringLocalizer;
    
    public ExampleController(IStringLocalizer<ExampleController> stringLocalizer)
    {
        _stringLocalizer = stringLocalizer;
    }
    
    [HttpGet]
    public string Get()
    {
        return _stringLocalizer["ExampleMessage"];
    }
}

```