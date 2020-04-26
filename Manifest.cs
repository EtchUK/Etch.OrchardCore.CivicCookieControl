using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Etch UK Ltd.",
    Category = "Content",
    Description = "Add Cookie Control by CIVIC.",
    Name = "CIVIC Cookie Control",
    Version = "0.1.0",
    Website = "https://etchuk.com",
    Dependencies = new[]
    {
        "Etch.OrchardCore.Fields.CodeField",
        "OrchardCore.ContentFields"
    }
)]