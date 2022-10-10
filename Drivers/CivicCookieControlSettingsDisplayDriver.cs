using Etch.OrchardCore.CivicCookieControl.Settings;
using Etch.OrchardCore.CivicCookieControl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etch.OrchardCore.CivicCookieControl.Drivers
{
    public class CivicCookieControlSettingsDisplayDriver : SectionDisplayDriver<ISite, CivicCookieControlSettings>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IContentManager _contentManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CivicCookieControlSettingsDisplayDriver(IAuthorizationService authorizationService, IContentManager contentManager, IContentDefinitionManager contentDefinitionManager, IContentItemDisplayManager contentItemDisplayManager, IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
            _contentManager = contentManager;
            _contentDefinitionManager = contentDefinitionManager;
            _contentItemDisplayManager = contentItemDisplayManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<IDisplayResult> EditAsync(CivicCookieControlSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !await _authorizationService.AuthorizeAsync(user, Permissions.ManageCivicCookieControlSettings))
            {
                return null;
            }

            return Initialize<CivicCookieControlSettingsViewModel>("CivicCookieControlSettings_Edit", model =>
            {
                model.ApiKey = settings.ApiKey;
                model.Product = settings.Product;

                model.InitialState = settings.InitialState;
                model.NotifyOnce = settings.NotifyOnce;
                model.RejectButton = settings.RejectButton;
                model.AcceptBehaviour = settings.AcceptBehaviour;
                model.CloseOnGlobalChange = settings.CloseOnGlobalChange;

                model.Title = settings.Title;
                model.Intro = settings.Intro;
                model.NecessaryTitle = settings.NecessaryTitle;
                model.NecessaryDescription = settings.NecessaryDescription;
                model.ThirdPartyTitle = settings.ThirdPartyTitle;
                model.ThirdPartyDescription = settings.ThirdPartyDescription;

                model.NecessaryCookies = JsonConvert.SerializeObject(settings.NecessaryCookies);
                model.Cookies = settings.Cookies;
                model.Subdomains = settings.Subdomains;

                model.StatementDescription = settings.StatementDescription;
                model.StatementName = settings.StatementName;
                model.StatementUpdated = settings.StatementUpdated;
                model.StatementUrl = settings.StatementUrl;

                model.Layout = settings.Layout;
                model.Position = settings.Position;
                model.Theme = settings.Theme;
                model.ToggleType = settings.ToggleType;
                model.CloseStyle = settings.CloseStyle;
                model.SettingsStyle = settings.SettingsStyle;

                model.CookieContentTypes = _contentDefinitionManager.ListTypeDefinitions().Where(t => t.GetSettings<ContentTypeSettings>().Stereotype == "Cookie");

                model.Updater = context.Updater;
            }).Location("Content:5").OnGroup(Constants.GroupId);
        }

        public override async Task<IDisplayResult> UpdateAsync(CivicCookieControlSettings settings, BuildEditorContext context)
        {
            if (context.GroupId == Constants.GroupId)
            {
                var user = _httpContextAccessor.HttpContext?.User;

                if (user == null || !await _authorizationService.AuthorizeAsync(user, Permissions.ManageCivicCookieControlSettings))
                {
                    return null;
                }

                var model = new CivicCookieControlSettingsViewModel();
                await context.Updater.TryUpdateModelAsync(model, Prefix);

                if (context.Updater.ModelState.IsValid)
                {
                    settings.ApiKey = model.ApiKey;
                    settings.Product = model.Product;

                    settings.InitialState = model.InitialState;
                    settings.NotifyOnce = model.NotifyOnce;
                    settings.RejectButton = model.RejectButton;
                    settings.AcceptBehaviour = model.AcceptBehaviour;
                    settings.CloseOnGlobalChange = model.CloseOnGlobalChange;

                    settings.Title = model.Title;
                    settings.Intro = model.Intro;
                    settings.NecessaryTitle = model.NecessaryTitle;
                    settings.NecessaryDescription = model.NecessaryDescription;
                    settings.ThirdPartyTitle = model.ThirdPartyTitle;
                    settings.ThirdPartyDescription = model.ThirdPartyDescription;

                    settings.StatementDescription = model.StatementDescription;
                    settings.StatementName = model.StatementName;
                    settings.StatementUpdated = model.StatementUpdated;
                    settings.StatementUrl = model.StatementUrl;

                    settings.Layout = model.Layout;
                    settings.Position = model.Position;
                    settings.Theme = model.Theme;
                    settings.ToggleType = model.ToggleType;
                    settings.CloseStyle = model.CloseStyle;
                    settings.SettingsStyle = model.SettingsStyle;

                    settings.Subdomains = model.Subdomains;
                    settings.NecessaryCookies = JsonConvert.DeserializeObject<List<string>>(model.NecessaryCookies);
                }

                settings.Cookies.Clear();

                for (var i = 0; i < model.Prefixes.Length; i++)
                {
                    var contentItem = await _contentManager.NewAsync(model.ContentTypes[i]);
                    await _contentItemDisplayManager.UpdateEditorAsync(contentItem, context.Updater, context.IsNew, htmlFieldPrefix: model.Prefixes[i]);

                    settings.Cookies.Add(contentItem);
                }
            }

            return await EditAsync(settings, context);
        }
    }
}
