using Etch.OrchardCore.CivicCookieControl.Cookies;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.CivicCookieControl.CookieTypes
{
    public class FacebookPixelCookie : ICookieType
    {
        public string ContentType => "FacebookPixelCookie";

        public JToken ToJson(ContentItem contentItem)
        {
            var json = new JObject();
            var part = contentItem.Get<ContentPart>(ContentType);
            var trackingId = part.Get<TextField>("PixelID")?.Text ?? string.Empty;

            json["name"] = "marketing";
            json["label"] = part.Get<TextField>("Label")?.Text ?? "Marketing Cookies";
            json["description"] = part.Get<TextField>("Description")?.Text ?? "We use marketing cookies to help us improve the relevancy of advertising campaigns you receive.";
            json["cookies"] = new JArray(new string[] { "fr" });
            json["onAccept"] = new JRaw($"function () {{ {CreateEnablePixelScript(trackingId) }}}");
            json["onRevoke"] = new JRaw($"function () {{ {CreateDisablePixelScript()} }}");

            return json;
        }

        private string CreateDisablePixelScript()
        {
            return $"fbq('consent', 'revoke');";
        }

        private string CreateEnablePixelScript(string trackingId)
        {
            return $@"!function(f,b,e,v,n,t,s)
{{if(f.fbq)return;n=f.fbq=function(){{n.callMethod?
    n.callMethod.apply(n,arguments):n.queue.push(arguments)}};
    if(!f._fbq)f._fbq=n;n.push=n;n.loaded=!0;n.version='2.0';
    n.queue=[];t=b.createElement(e);t.async=!0;
    t.src=v;s=b.getElementsByTagName(e)[0];
    s.parentNode.insertBefore(t,s)}}(window,document,'script',
    'https://connect.facebook.net/en_US/fbevents.js');
fbq('init', '{trackingId}');
fbq('track', 'PageView');
fbq('consent', 'grant');";
        }
    }
}
