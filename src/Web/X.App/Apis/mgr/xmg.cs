using System;
using System.Collections.Generic;
using System.Linq;
using X.Core.Cache;
using X.Web;

namespace X.App.Apis.mgr
{
    public class xmg : xapi
    {
        protected x_user mg = null;
        protected override void InitApi()
        {
            base.InitApi();

            var key = GetReqParms("mg_ad");
            if (string.IsNullOrEmpty(key)) throw new XExcep("0x0005");

            mg = CacheHelper.Get<x_user>("mgr." + key);
            if (mg == null) throw new XExcep("0x0005");
        }
    }
}
