using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using System;
using System.Collections.Generic;

namespace Etch.OrchardCore.CivicCookieControl.ViewModels
{
    public class CivicCookieControlSettingsViewModel
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

        #region Cookie Properties

        public IList<ContentItem> Cookies { get; set; }

        public string[] Prefixes { get; set; } = Array.Empty<string>();
        public string[] ContentTypes { get; set; } = Array.Empty<string>();

        public IEnumerable<ContentTypeDefinition> CookieContentTypes { get; set; }

        #endregion

        #region Statement Properties

        public string StatementDescription { get; set; }
        public string StatementName { get; set; }
        public string StatementUrl { get; set; }
        public string StatementUpdated { get; set; }

        #endregion

        [BindNever]
        public IUpdateModel Updater { get; set; }
    }
}
