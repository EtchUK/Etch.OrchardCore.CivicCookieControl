using Etch.OrchardCore.CivicCookieControl.CookieTypes;
using Etch.OrchardCore.CivicCookieControl.Drivers;
using Etch.OrchardCore.CivicCookieControl.Filters;
using Etch.OrchardCore.CivicCookieControl.Helpers;
using Etch.OrchardCore.CivicCookieControl.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;

namespace Etch.OrchardCore.CivicCookieControl
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IDisplayDriver<ISite>, CivicCookieControlSettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<ICookieControlSettingsService, CookieControlSettingsService>();
            services.AddScoped<IDataMigration, Migrations>();

            services.AddCookieType<FacebookPixelCookie>();
            services.AddCookieType<GoogleAnalyticsCookie>();
            services.AddCookieType<RawCookie>();

            services.AddContentPart<Models.CivicCookieControl>()
                .UseDisplayDriver<CivicCookieControlDisplayDriver>();

            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.Add(typeof(CivicCookieControlFilter));
            });
        }
    }
}
