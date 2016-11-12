using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DailySports.Backend.Helpers
{
    public static class Globals
    {
        private static string CDN = "http://cdn.dailyesports.tv/";

        public static HtmlString CDNContent(string fileName)
        {
            string LableStr = CDN + fileName;
            return new HtmlString(LableStr);
        }

        public static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue)
        {
            return from Enum e in Enum.GetValues(enumValue.GetType())
                   select new SelectListItem
                   {
                       Selected = e.Equals(enumValue),
                       Text = e.ToString(),
                       Value = e.ToString()
                   };
        }
    }
}
