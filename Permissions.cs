using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Linq;

namespace Etch.OrchardCore.CivicCookieControl
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageCivicCookieControlSettings = new Permission(nameof(ManageCivicCookieControlSettings), "Manage CIVIC Cookie Control settings");

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] { ManageCivicCookieControlSettings }.AsEnumerable();
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            yield return new PermissionStereotype
            {
                Name = "Administrator",
                Permissions = new[]
                {
                    ManageCivicCookieControlSettings
                }
            };
        }
    }
}
