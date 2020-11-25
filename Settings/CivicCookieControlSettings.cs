using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using System.Collections.Generic;

namespace Etch.OrchardCore.CivicCookieControl.Settings
{
    public class CivicCookieControlSettings
    {
        #region License

        public string ApiKey { get; set; }
        public string Product { get; set; }

        #endregion

        #region Behaviour

        public string InitialState { get; set; } = "closed";
        public bool NotifyOnce { get; set; } = false;
        public bool RejectButton { get; set; } = false;
        public string AcceptBehaviour { get; set; } = "all";
        public bool CloseOnGlobalChange { get; set; } = true;

        #endregion

        #region Text

        public string Title { get; set; }
        public string Intro { get; set; }
        public string NecessaryTitle { get; set; }
        public string NecessaryDescription { get; set; }
        public string ThirdPartyTitle { get; set; }
        public string ThirdPartyDescription { get; set; }

        #endregion

        #region Cookies

        [BindNever]
        public List<ContentItem> Cookies { get; } = new List<ContentItem>();

        public bool Subdomains { get; set; } = true;

        #endregion

        #region Statement

        public string StatementDescription { get; set; }
        public string StatementName { get; set; }
        public string StatementUrl { get; set; }
        public string StatementUpdated { get; set; }

        #endregion

        #region Theme

        public string Layout { get; set; } = "slideout";
        public string Position { get; set; } = "right";
        public string Theme { get; set; } = "dark";
        public string ToggleType { get; set; } = "slider";
        public string CloseStyle { get; set; } = "icon";
        public string SettingsStyle { get; set; } = "button";

        #endregion

        #region Helper Methods

        public bool HasStatement
        {
            get
            {
                return !string.IsNullOrWhiteSpace(StatementDescription) &&
                       !string.IsNullOrWhiteSpace(StatementName) &&
                       !string.IsNullOrWhiteSpace(StatementUpdated) &&
                       !string.IsNullOrWhiteSpace(StatementUrl);
            }
        }

        public bool IsValid
        {
            get { return !string.IsNullOrWhiteSpace(ApiKey) && !string.IsNullOrWhiteSpace(Product); }
        }

        #endregion
    }
}
