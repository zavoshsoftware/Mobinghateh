using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MobinGhateAsia
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected string StyleSheetPathFa
        {
            get
            {
                // pull the stylesheet name from a database or xml file...
                return ApplicationPath + "css/rightStyle.css";
            }
        }
        protected string StyleSheetPath
        {
            get
            {
                // pull the stylesheet name from a database or xml file...
                return ApplicationPath + "css/leftStyle.css";
            }
        }

        private string ApplicationPath
        {
            get
            {
                string result = Request.ApplicationPath;
                if (!result.EndsWith("/"))
                {
                    result += "/";
                }
                return result;
            }
        }
    }
}
