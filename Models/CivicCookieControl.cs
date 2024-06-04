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
        private const string StatementPropertyName = "statement";
        private const string TextPropertyName = "text";

        public TextField AcceptAllButtonLabel { get; set; }
        public TextField AcceptBehaviour { get; set; }
        public TextField ApiKey { get; set; }
        public TextField CloseButtonLabel { get; set; }
        public BooleanField CloseOnGlobalChange { get; set; }
        public TextField CloseStyle { get; set; }
        public TextField InitialState { get; set; }
        public TextField Intro { get; set; }
        public TextField Layout { get; set; }
        public ValuesField NecessaryCookies { get; set; }
        public TextField NecessaryDescription { get; set; }
        public TextField NecessaryTitle { get; set; }
        public TextField NotifyDescription { get; set; }
        public BooleanField NotifyOnce { get; set; }
        public TextField NotifyTitle { get; set; }
        public TextField OffToggleLabel { get; set; }
        public TextField OnToggleLabel { get; set; }
        public TextField Position { get; set; }
        public TextField Product { get; set; }
        public TextField RejectAllButtonLabel { get; set; }
        public BooleanField RejectButton { get; set; }
        public TextField SettingsLabel { get; set; }
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

            json[TextPropertyName] = new JObject();

            AddString((JObject)json[TextPropertyName], "title", Title.Text);
            AddString((JObject)json[TextPropertyName], "intro", Intro.Text);
            AddString((JObject)json[TextPropertyName], "necessaryTitle", NecessaryTitle.Text);
            AddString((JObject)json[TextPropertyName], "necessaryDescription", NecessaryDescription.Text);
            AddString((JObject)json[TextPropertyName], "thirdPartyTitle", ThirdPartyTitle.Text);
            AddString((JObject)json[TextPropertyName], "thirdPartyDescription", ThirdPartyDescription.Text);
            AddString((JObject)json[TextPropertyName], "acceptSettings", AcceptAllButtonLabel?.Text);
            AddString((JObject)json[TextPropertyName], "rejectSettings", RejectAllButtonLabel?.Text);
            AddString((JObject)json[TextPropertyName], "closeLabel", CloseButtonLabel?.Text);
            AddString((JObject)json[TextPropertyName], "on", OnToggleLabel?.Text);
            AddString((JObject)json[TextPropertyName], "off", OffToggleLabel?.Text);
            AddString((JObject)json[TextPropertyName], "settings", SettingsLabel?.Text);
            AddString((JObject)json[TextPropertyName], "notifyTitle", NotifyTitle?.Text);
            AddString((JObject)json[TextPropertyName], "notifyDescription", NotifyDescription?.Text);
            AddString((JObject)json[TextPropertyName], "accept", AcceptAllButtonLabel?.Text);
            AddString((JObject)json[TextPropertyName], "reject", RejectAllButtonLabel?.Text);

            json["optionalCookies"] = CreateOptionalCookies(ContentItem.Get<BagPart>("OptionalCookies")?.ContentItems ?? new List<ContentItem>());

            if (HasStatement)
            {
                json[StatementPropertyName] = new JObject();

                AddString((JObject)json[StatementPropertyName], "description", StatementDescription.Text);
                AddString((JObject)json[StatementPropertyName], "name", StatementName.Text);
                AddString((JObject)json[StatementPropertyName], "url", StatementUrl.Text);
                AddString((JObject)json[StatementPropertyName], "updated", StatementUpdated.Value.Value.ToString("dd/MM/yyyy"));
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
