using Etch.OrchardCore.CivicCookieControl.Settings;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.Admin;
using OrchardCore.Entities;
using OrchardCore.ResourceManagement;
using OrchardCore.Settings;
using System.Threading.Tasks;

namespace Etch.OrchardCore.CivicCookieControl.Filters
{
    public class CivicCookieControlFilter : IAsyncResultFilter
    {
        private readonly IResourceManager _resourceManager;
        private readonly ISiteService _siteService;

        private HtmlString _scriptsCache;

        public CivicCookieControlFilter(IResourceManager resourceManager, ISiteService siteService)
        {
            _resourceManager = resourceManager;
            _siteService = siteService;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if ((context.Result is ViewResult || context.Result is PageResult) && !AdminAttribute.IsApplied(context.HttpContext))
            {
                if (_scriptsCache == null)
                {
                    var settings = (await _siteService.GetSiteSettingsAsync()).As<CivicCookieControlSettings>();

                    if (settings?.IsValid ?? false)
                    {
                        _scriptsCache = new HtmlString($"<script src=\"https://cc.cdn.civiccomputing.com/8/cookieControl-8.x.min.js\"></script><script>var config = {{ apiKey: '{settings.ApiKey}', product: '{settings.Product}', optionalCookies: [], }}; CookieControl.load( config );</script>");
                    }
                }

                if (_scriptsCache != null)
                {
                    _resourceManager.RegisterHeadScript(_scriptsCache);
                }
            }

            await next.Invoke();
        }
    }
}
