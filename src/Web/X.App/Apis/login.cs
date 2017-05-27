using System;
using System.Linq;
using X.Core.Cache;
using X.Core.Utility;
using X.Web;
using X.Web.Com;

namespace X.App.Apis
{
    public class login : xapi
    {
        [ParmsAttr(name = "用户名", req = true, len = "6,16")]
        public string uid { get; set; }
        [ParmsAttr(name = "密码", req = true, len = "6,")]
        public string pwd { get; set; }

        protected override XResp Execute()
        {
            var u = db.x_user.FirstOrDefault(o => o.uid == uid);
            if (u == null || Secret.MD5(pwd) != u.pwd) throw new XExcep("0x0007");

            var key = Secret.MD5(Guid.NewGuid().ToString());
            u.ukey = key;

            db.SubmitDBChanges();

            Context.Response.SetCookie(new System.Web.HttpCookie("x.user", key));

            return new XResp();
        }
    }
}
