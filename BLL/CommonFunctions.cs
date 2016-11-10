using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace CanamLiveFA.BLL
{
    public class CommonFunctions
    {
        public static void SetSessionValue(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static object GetSessionValue(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public static object RemoveSessionValue(string key)
        {
            return HttpContext.Current.Session[key] = null;
        }

        public static void SetApplicationValue(string key, object value)
        {
            HttpContext.Current.Application[key] = value;
        }

        public static object GetApplicationValue(string key)
        {
            return HttpContext.Current.Application[key];
        }

        public static object RemoveApplicationValue(string key)
        {
            return HttpContext.Current.Application[key] = null;
        }
    }
}