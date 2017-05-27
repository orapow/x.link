using System;
using System.Linq;
using X.Core.Cache;
using X.Core.Utility;
using X.Web;
using X.Web.Com;

namespace X.App.Apis
{
    public class logout : xapi
    {
        protected override XResp Execute()
        {
            var r = new XResp();
            var uk = GetReqParms("x.user");
            if (string.IsNullOrEmpty(uk)) return r;

            var u = db.x_user.FirstOrDefault(o => o.ukey == uk);
            if (u == null) return r;

            u.ukey = "";
            db.SubmitDBChanges();

            Context.Response.Cookies.Remove(uk);

            return new XResp();
        }
    }
}
