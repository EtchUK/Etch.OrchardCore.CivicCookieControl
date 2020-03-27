using Etch.OrchardCore.CivicCookieControl.Settings;
using Etch.OrchardCore.CivicCookieControl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;
using System.Threading.Tasks;

namespace Etch.OrchardCore.CivicCookieControl.Drivers
{
    public class CivicCookieControlSettingsDisplayDriver : SectionDisplayDriver<ISite, CivicCookieControlSettings>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CivicCookieControlSettingsDisplayDriver(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
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
                model.Title = settings.Title;
                model.Intro = settings.Intro;
                model.NecessaryTitle = settings.NecessaryTitle;
                model.NecessaryDescription = settings.NecessaryDescription;
                model.ThirdPartyTitle = settings.ThirdPartyTitle;
                model.ThirdPartyDescription = settings.ThirdPartyDescription;
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
                    settings.Title = model.Title;
                    settings.Intro = model.Intro;
                    settings.NecessaryTitle = model.NecessaryTitle;
                    settings.NecessaryDescription = model.NecessaryDescription;
                    settings.ThirdPartyTitle = model.ThirdPartyTitle;
                    settings.ThirdPartyDescription = model.ThirdPartyDescription;
                }
            }
            return await EditAsync(settings, context);
        }
    }
}
