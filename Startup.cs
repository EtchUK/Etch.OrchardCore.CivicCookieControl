using Etch.OrchardCore.CivicCookieControl.Drivers;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace Etch.OrchardCore.CivicCookieControl
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataMigration, Migrations>();

            services.AddContentPart<Models.CivicCookieControl>()
                .UseDisplayDriver<CivicCookieControlDisplayDriver>();
        }
    }
}
