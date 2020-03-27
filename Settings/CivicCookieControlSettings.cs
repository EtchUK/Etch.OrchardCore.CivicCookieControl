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

        #region Helper Methods

        public bool IsValid
        {
            get { return !string.IsNullOrWhiteSpace(ApiKey) && !string.IsNullOrWhiteSpace(Product); }
        }

        #endregion
    }
}
