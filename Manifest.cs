using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Etch UK Ltd.",
    Category = "Content",
    Description = "Add Cookie Control by CIVIC.",
    Name = "CIVIC Cookie Control",
    Version = "$(VersionNumber)",
    Website = "https://etchuk.com",
    Dependencies = new[]
    {
        "Etch.OrchardCore.Fields.Code",
        "Etch.OrchardCore.Fields.Values",
        "OrchardCore.ContentFields",
        "OrchardCore.Flows"
    }
)]