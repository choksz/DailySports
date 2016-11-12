using Microsoft.AspNetCore.Html;

namespace DailySports.Helpers
{
    public static class Globals
    {
        private static string CDN = "http://cdn.dailyesports.tv/";

        public static HtmlString CDNContent(string fileName)
        {
            string LableStr = CDN + fileName;
            return new HtmlString(LableStr);
        }
    }
}
