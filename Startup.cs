﻿using Etch.OrchardCore.CivicCookieControl.Drivers;
using Etch.OrchardCore.CivicCookieControl.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.Add(typeof(CivicCookieControlFilter));
            });
        }
    }
}