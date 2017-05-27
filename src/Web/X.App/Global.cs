using System;
using System.Collections.Generic;
using System.Linq;
using X.Core;
using X.Core.Cache;
using X.Core.Plugin;
using X.Web.Com;

namespace X.App
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var cfg = Config.LoadConfig();
            Tpl.Configuration(Server.MapPath("~/tpl/"));
            Loger.Init();
            CacheHelper.Init(cfg.cache);
        }
    }
}
