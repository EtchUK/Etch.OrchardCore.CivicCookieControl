using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;

namespace Etch.OrchardCore.CivicCookieControl
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                builder.Add(S["Configuration"], configuration => configuration
                    .Add(S["CIVIC Cookie Control"], S["CIVIC Cookie Control"].PrefixPosition(), settings => settings
                    .AddClass("cookie").Id("cookie")
                    .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = Constants.GroupId })
                        .Permission(Permissions.ManageCivicCookieControlSettings)
                        .LocalNav())
                );
            }
            return Task.CompletedTask;
        }
    }
}
