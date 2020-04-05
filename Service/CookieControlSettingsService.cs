using Etch.OrchardCore.CivicCookieControl.Options;
using Etch.OrchardCore.CivicCookieControl.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using System.Collections.Generic;

namespace Etch.OrchardCore.CivicCookieControl.Service
{
    public class CookieControlSettingsService : ICookieControlSettingsService
    {
        #region Dependencies

        private readonly CookieTypeOptions _cookieTypeOptions;

        #endregion

        #region Constructor

        public CookieControlSettingsService(IOptions<CookieTypeOptions> cookieTypeOptions)
        {
            _cookieTypeOptions = cookieTypeOptions.Value;
        }

        #endregion

        #region Implementation

        public string ToJson(CivicCookieControlSettings settings)
        {
            var json = new JObject
            {
                ["apiKey"] = settings.ApiKey,
                ["product"] = settings.Product
            };

            json["text"] = new JObject();

            AddString((JObject)json["text"], "title", settings.Title);
            AddString((JObject)json["text"], "intro", settings.Intro);
            AddString((JObject)json["text"], "necessaryTitle", settings.NecessaryTitle);
            AddString((JObject)json["text"], "necessaryDescription", settings.NecessaryDescription);
            AddString((JObject)json["text"], "thirdPartyTitle", settings.ThirdPartyTitle);
            AddString((JObject)json["text"], "thirdPartyDescription", settings.ThirdPartyDescription);

            json["optionalCookies"] = CreateOptionalCookies(settings.Cookies);

            return json.ToString();
        }

        #endregion

        #region Helpers

        private void AddString(JObject json, string propertyName, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                json[propertyName] = value;
            }
        }

        private JArray CreateOptionalCookies(IList<ContentItem> cookies)
        {
            var json = new JArray();

            foreach (var contentItem in cookies)
            {
                var cookieType = _cookieTypeOptions.GetCookieType(contentItem);

                if (cookieType != null)
                {
                    json.Add(cookieType.ToJson(contentItem));
                }
            }

            return json;
        }

        #endregion
    }

    public interface ICookieControlSettingsService
    {
        string ToJson(CivicCookieControlSettings settings);
    }
}
