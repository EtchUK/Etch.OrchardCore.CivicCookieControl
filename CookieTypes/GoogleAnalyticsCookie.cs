using Etch.OrchardCore.CivicCookieControl.Cookies;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.CivicCookieControl.CookieTypes
{
    public class GoogleAnalyticsCookie : ICookieType
    {
        public string ContentType => "GoogleAnalyticsCookie";

        public JToken ToJson(ContentItem contentItem)
        {
            var json = new JObject();
            var part = contentItem.Get<ContentPart>(ContentType);
            var trackingId = part.Get<TextField>("TrackingCode")?.Text ?? string.Empty;

            json["name"] = "analytics";
            json["label"] = part.Get<TextField>("Label")?.Text ?? "Analytical Cookies";
            json["description"] = part.Get<TextField>("Description")?.Text ?? "Analytical cookies help us to improve our website by collecting and reporting information on its usage.";
            json["cookies"] = new JArray(new string[] { "_ga", "_gid", "_gat", "__utma", "__utmt", "__utmb", "__utmc", "__utmz", "__utmv" });
            json["onAccept"] = new JRaw($"function () {{ {CreateEnableAnalyticsScript(trackingId) }}}");
            json["onRevoke"] = new JRaw($"function () {{ {CreateDisableAnalyticsScript(trackingId)} }}");

            return json;
        }

        private string CreateDisableAnalyticsScript(string trackingId)
        {
            return $"window['ga-disable-{trackingId}'] = true;";
        }

        private string CreateEnableAnalyticsScript(string trackingId)
        {
            return $@"var s = document.createElement('script');
s.type = 'text/javascript';
s.src = 'https://www.googletagmanager.com/gtag/js?id={trackingId}';
s.setAttribute('async','');
document.getElementsByTagName('head')[0].appendChild(s);

window.dataLayer = window.dataLayer || [];
function gtag(){{dataLayer.push(arguments);}}
gtag('js', new Date());
gtag('config', '{trackingId}');";
        }
    }
}
