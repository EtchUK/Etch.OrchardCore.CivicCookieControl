using Etch.OrchardCore.CivicCookieControl.Service;
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
        private readonly ICookieControlSettingsService _cookieControlSettingsService;
        private readonly IResourceManager _resourceManager;
        private readonly ISiteService _siteService;

        private HtmlString _scriptsCache;

        public CivicCookieControlFilter(ICookieControlSettingsService cookieControlSettingsService, IResourceManager resourceManager, ISiteService siteService)
        {
            _cookieControlSettingsService = cookieControlSettingsService;
            _resourceManager = resourceManager;
            _siteService = siteService;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (ShouldIncludeCivic(context))
            {
                if (_scriptsCache == null)
                {
                    var settings = (await _siteService.GetSiteSettingsAsync()).As<CivicCookieControlSettings>();

                    if (settings?.IsValid ?? false)
                    {
                        _scriptsCache = new HtmlString($"<script>window.loadCivic=function(){{var config = {_cookieControlSettingsService.ToJson(settings)};CookieControl.load(config);}}</script><script src=\"https://cc.cdn.civiccomputing.com/9/cookieControl-9.x.min.js\" onload=\"window.loadCivic()\" async></script>");
                    }
                }

                if (_scriptsCache != null)
                {
                    _resourceManager.RegisterHeadScript(_scriptsCache);
                }
            }

            await next.Invoke();
        }

        private static bool ShouldIncludeCivic(ResultExecutingContext context)
        {
            return (context.Result is ViewResult || context.Result is PageResult) &&
                !AdminAttribute.IsApplied(context.HttpContext) &&
                !context.HttpContext.Request.Query.ContainsKey("ignore-civic");
        }
    }
}