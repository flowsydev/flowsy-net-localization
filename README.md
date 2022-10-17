# Flowsy Localization

Basic functionality for multi-lingual support.

## Requirements
In order to provider multi-lingual support, applications must provide resource files with translations for every supported culture.
For example, if our project contains a directory named **Resources** with the files **Shared.en-us.resx** and **Shared.es-mx.resx**
we can load the right translation for the current culture by calling: 

```csharp
// Given the resource files for en-US and es-MX:
// /path/to/the/project/Resources/Shared.en-us.resx
// /path/to/the/project/Resources/Shared.es-mx.resx
// we can configure the default resource base name to look for strings
// taking its path from the root of our project, using dots instead of directory separators and excluding the culture name and extension
LocalizationResources.DefaultResourceBaseName = "Resources.Shared";

// Load translation specifying the assembly containing the resource files
var localizedString1 = "StringId".Localize(typeof(SomeClass).Assembly);

// Load translation using resource files from the assembly calling Localize 
var localizedString2 = "StringId".Localize();
```

Optionally, we can use a resource file different from LocalizationResources.DefaultResourceBaseName
by calling **Localize** with a custom name as its last argument.
