namespace Etch.OrchardCore.CivicCookieControl.Settings
{
    public class CivicCookieControlSettings
    {
        public string ApiKey { get; set; }
        public string Product { get; set; }

        public bool IsValid
        {
            get { return !string.IsNullOrWhiteSpace(ApiKey) && !string.IsNullOrWhiteSpace(Product); }
        }
    }
}
