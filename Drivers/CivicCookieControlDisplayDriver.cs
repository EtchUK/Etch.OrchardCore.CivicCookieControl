using Microsoft.AspNetCore.Html;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.ResourceManagement;

namespace Etch.OrchardCore.CivicCookieControl.Drivers
{
    public class CivicCookieControlDisplayDriver : ContentPartDisplayDriver<Models.CivicCookieControl>
    {
        private readonly IResourceManager _resourceManager;

        public CivicCookieControlDisplayDriver(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public override IDisplayResult Display(Models.CivicCookieControl part, BuildPartDisplayContext context)
        {
            if (context.DisplayType != "Detail")
            {
                return null;
            }

            _resourceManager.RegisterHeadScript(new HtmlString($"<script>window.loadCivic=function(){{var config = {part.ToJson()};CookieControl.load(config);}}</script><script src=\"https://cc.cdn.civiccomputing.com/9/cookieControl-9.x.min.js\" onload=\"window.loadCivic()\" async></script>"));

            return null;
        }
    }
}
