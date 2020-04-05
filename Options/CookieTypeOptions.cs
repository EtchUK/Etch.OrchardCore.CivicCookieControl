using Etch.OrchardCore.CivicCookieControl.Cookies;
using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Etch.OrchardCore.CivicCookieControl.Options
{
    public class CookieTypeOptions
    {
        private IList<ICookieType> Types { get; }

        public CookieTypeOptions()
        {
            Types = new List<ICookieType>();
        }

        public void AddCookieType<T>() where T : class, ICookieType
        {
            Types.Add((ICookieType)Activator.CreateInstance(typeof(T)));
        }

        public ICookieType GetCookieType(ContentItem contentItem)
        {
            return Types.Where(x => x.ContentType == contentItem.ContentType).FirstOrDefault();
        }
    }
}
