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
    }
}
