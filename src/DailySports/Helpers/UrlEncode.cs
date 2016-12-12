using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DailySports.Helpers
{
    public class UrlEncode : ActionFilterAttribute
    {
        private static Dictionary<string, string> encodedDictionary = new Dictionary<string, string>
        {
            { "\"", WebUtility.UrlDecode("\"") },
            { "'", WebUtility.UrlDecode("'") },
            { "<", WebUtility.UrlDecode("<") },
            { ">", WebUtility.UrlDecode(">") }
        };

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (filterContext != null)
                {
                    var actionParams = filterContext.ActionArguments;
                    var actionParamsKeys = actionParams.Keys.ToList();
                    foreach (var key in actionParamsKeys)
                    {
                        if (actionParams[key] != null)
                        {
                            object paramReference = actionParams[key];
                            if (paramReference != null && paramReference.GetType().Equals(typeof(string)))
                            {
                                StringBuilder sb = new StringBuilder(paramReference.ToString());
                                foreach (var encodedKeyval in encodedDictionary)
                                {
                                    sb.Replace(encodedKeyval.Key, encodedKeyval.Value);
                                }
                                filterContext.ActionArguments[key] = Regex.Replace(sb.ToString(), @"\s+", "");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
