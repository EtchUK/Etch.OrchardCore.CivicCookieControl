using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.CivicCookieControl.Cookies
{
    public interface ICookieType
    {
        string ContentType { get; }

        JToken ToJson(ContentItem contentItem);
    }
}
