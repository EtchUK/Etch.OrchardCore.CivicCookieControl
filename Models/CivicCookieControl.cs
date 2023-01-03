using Etch.OrchardCore.Fields.Code.Fields;
using Etch.OrchardCore.Fields.Values.Fields;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Flows.Models;
using System;
using System.Collections.Generic;

namespace Etch.OrchardCore.CivicCookieControl.Models
{
    public class CivicCookieControl : ContentPart
    {
        public TextField AcceptBehaviour { get; set; }
        public TextField ApiKey { get; set; }
        public BooleanField CloseOnGlobalChange { get; set; }
        public TextField CloseStyle { get; set; }
        public TextField InitialState { get; set; }
        public TextField Intro { get; set; }
        public TextField Layout { get; set; }
        public ValuesField NecessaryCookies { get; set; }
        public TextField NecessaryDescription { get; set; }
        public TextField NecessaryTitle { get; set; }
        public BooleanField NotifyOnce { get; set; }
        public TextField Position { get; set; }
        public TextField Product { get; set; }
        public BooleanField RejectButton { get; set; }
        public TextField SettingsStyle { get; set; }
        public TextField StatementDescription { get; set; }
        public TextField StatementName { get; set; }
        public DateField StatementUpdated { get; set; }
        public TextField StatementUrl { get; set; }
        public BooleanField Subdomains { get; set; }
        public TextField Theme { get; set; }
        public TextField ThirdPartyDescription { get; set; }
        public TextField ThirdPartyTitle { get; set; }
        public TextField Title { get; set; }
        public TextField ToggleType { get; set; }

        public string ToJson()
        {
            var json = new JObject
            {
                ["apiKey"] = ApiKey.Text,
                ["product"] = Product.Text,
                ["necessaryCookies"] = JArray.FromObject(NecessaryCookies.Data ?? Array.Empty<string>()),
                ["initialState"] = InitialState.Text,
                ["notifyOnce"] = NotifyOnce.Value,
                ["rejectButton"] = RejectButton.Value,
                ["acceptBehaviour"] = AcceptBehaviour.Text,
                ["closeOnGlobalChange"] = CloseOnGlobalChange.Value,
                ["layout"] = Layout.Text,
                ["position"] = Position.Text,
                ["theme"] = Theme.Text,
                ["toggleType"] = ToggleType.Text,
                ["closeStyle"] = CloseStyle.Text,
                ["settingsStyle"] = SettingsStyle.Text,
                ["subDomains"] = Subdomains.Value
            };

            json["text"] = new JObject();

            AddString((JObject)json["text"], "title", Title.Text);
            AddString((JObject)json["text"], "intro", Intro.Text);
            AddString((JObject)json["text"], "necessaryTitle", NecessaryTitle.Text);
            AddString((JObject)json["text"], "necessaryDescription", NecessaryDescription.Text);
            AddString((JObject)json["text"], "thirdPartyTitle", ThirdPartyTitle.Text);
            AddString((JObject)json["text"], "thirdPartyDescription", ThirdPartyDescription.Text);

            json["optionalCookies"] = CreateOptionalCookies(ContentItem.Get<BagPart>("OptionalCookies")?.ContentItems ?? new List<ContentItem>());

            if (HasStatement)
            {
                json["statement"] = new JObject();

                AddString((JObject)json["statement"], "description", StatementDescription.Text);
                AddString((JObject)json["statement"], "name", StatementName.Text);
                AddString((JObject)json["statement"], "url", StatementUrl.Text);
                AddString((JObject)json["statement"], "updated", StatementUpdated.Value.Value.ToString("dd/MM/yyyy"));
            }

            return json.ToString();
        }

        #region Helpers

        public bool HasStatement
        {
            get
            {
                return StatementDescription != null && StatementName != null && StatementUpdated != null && StatementUrl != null &&
                   !string.IsNullOrWhiteSpace(StatementDescription.Text) &&
                   !string.IsNullOrWhiteSpace(StatementName.Text) &&
                   StatementUpdated.Value.HasValue &&
                   !string.IsNullOrWhiteSpace(StatementUrl.Text);
            }
        }

        private static void AddString(JObject json, string propertyName, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                json[propertyName] = value;
            }
        }

        private static JArray CreateOptionalCookies(IList<ContentItem> cookies)
        {
            var json = new JArray();

            foreach (var contentItem in cookies)
            {
                var contentItemJson = new JObject();
                var part = contentItem.Get<ContentPart>("RawCookie");

                if (part == null)
                {
                    continue;
                }

                contentItemJson["name"] = part.Get<TextField>("Name")?.Text;
                contentItemJson["label"] = part.Get<TextField>("Label")?.Text;
                contentItemJson["description"] = part.Get<TextField>("Description")?.Text;
                contentItemJson["cookies"] = new JArray(part.Get<ValuesField>("Cookies")?.Data);
                contentItemJson["onAccept"] = new JRaw($"function () {{ {part.Get<CodeField>("Accept")?.Value}{Environment.NewLine} }}");
                contentItemJson["onRevoke"] = new JRaw($"function () {{ {part.Get<CodeField>("Revoke")?.Value}{Environment.NewLine} }}");

                json.Add(contentItemJson);
            }

            return json;
        }

        #endregion
    }
}
