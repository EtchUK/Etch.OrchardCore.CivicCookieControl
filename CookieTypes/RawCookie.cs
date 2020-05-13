using Etch.OrchardCore.CivicCookieControl.Cookies;
using Etch.OrchardCore.Fields.Code.Fields;
using Etch.OrchardCore.Fields.Values.Fields;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using System;

namespace Etch.OrchardCore.CivicCookieControl.CookieTypes
{
    public class RawCookie : ICookieType
    {
        public string ContentType => "RawCookie";

        public JToken ToJson(ContentItem contentItem)
        {
            var json = new JObject();
            var part = contentItem.Get<ContentPart>(ContentType);

            json["name"] = part.Get<TextField>("Name")?.Text;
            json["label"] = part.Get<TextField>("Label")?.Text;
            json["description"] = part.Get<TextField>("Description")?.Text;
            json["cookies"] = new JArray(part.Get<ValuesField>("Cookies")?.Data);
            json["onAccept"] = new JRaw($"function () {{ {part.Get<CodeField>("Accept")?.Value}{Environment.NewLine} }}");
            json["onRevoke"] = new JRaw($"function () {{ {part.Get<CodeField>("Revoke")?.Value}{Environment.NewLine} }}");

            return json;
        }
    }
}
