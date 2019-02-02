using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebApiODataServiceTest
{

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            EnableCrossDomain();
        }

        static void EnableCrossDomain()
        {
            string origin = HttpContext.Current.Request.Headers["Origin"];
            if (string.IsNullOrEmpty(origin)) return;
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", origin);
            string method = HttpContext.Current.Request.Headers["Access-Control-Request-Method"];
            if (!string.IsNullOrEmpty(method))
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", method);
            string headers = HttpContext.Current.Request.Headers["Access-Control-Request-Headers"];
            if (!string.IsNullOrEmpty(headers))
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", headers);
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.StatusCode = 204;
                HttpContext.Current.Response.End();
            }
        }
    }
}