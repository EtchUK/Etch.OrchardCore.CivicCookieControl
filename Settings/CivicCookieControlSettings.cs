using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using System.Collections.Generic;

namespace Etch.OrchardCore.CivicCookieControl.Settings
{
    public class CivicCookieControlSettings
    {
        #region License Properties

        public string ApiKey { get; set; }
        public string Product { get; set; }

        #endregion

        #region Text Properties

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

        #endregion

        #region Statement

        public string StatementDescription { get; set; }
        public string StatementName { get; set; }
        public string StatementUrl { get; set; }
        public string StatementUpdated { get; set; }

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
