using Etch.OrchardCore.CivicCookieControl.Cookies;
using Etch.OrchardCore.CivicCookieControl.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Etch.OrchardCore.CivicCookieControl.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCookieType<T>(this IServiceCollection services) where T : class, ICookieType
        {
            services.Configure<CookieTypeOptions>(options => options.AddCookieType<T>());
        }
    }
}
