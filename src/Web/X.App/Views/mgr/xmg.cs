using System;
using System.Collections.Generic;
using System.Linq;
using X.Core.Cache;
using X.Web;
using X.Web.Com;
using X.Web.Views;

namespace X.App.Views.mgr
{
    public class xmg : xview
    {
        protected x_user mg = null;

        protected override void InitDict()
        {
            base.InitDict();

            var key = GetReqParms("mg_ad");// Context.Request.Cookies["ad"];
            if (string.IsNullOrEmpty(key)) throw new XExcep("0x0005");

            mg = CacheHelper.Get<x_user>("mgr." + key);
            if (mg == null) throw new XExcep("0x0005");

            CacheHelper.Save("mgr." + key, mg, 60 * 60);
            dict.Add("mg", mg);
        }
    }
}
