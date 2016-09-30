using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailySports.BackOffice.Helpers
{
    public static class Helper
    {
        public static string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/") appUrl += "/";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }
    }
}