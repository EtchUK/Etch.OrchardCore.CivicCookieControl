using Microsoft.Extensions.Localization;
using OrchardCore.Environment.Shell.Descriptor.Models;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;

namespace Etch.OrchardCore.CivicCookieControl
{
    public class AdminMenu : INavigationProvider
    {
        private readonly ShellDescriptor _shellDescriptor;
        private readonly IStringLocalizer S;

        public AdminMenu(
            IStringLocalizer<AdminMenu> localizer,
            ShellDescriptor shellDescriptor)
        {
            S = localizer;
            _shellDescriptor = shellDescriptor;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                builder.Add(S["Configuration"], configuration => configuration
                    .Add(S["CIVIC Cookie Control"], S["CIVIC Cookie Control"], settings => settings
                    .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = Constants.GroupId })
                        .Permission(Permissions.ManageCivicCookieControlSettings)
                        .LocalNav())
                );
            }
            return Task.CompletedTask;
        }
    }
}
