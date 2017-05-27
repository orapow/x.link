using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Core.Utility;
using X.Web;
using X.Web.Com;

namespace X.App.Apis
{
    public class reg : xapi
    {
        [ParmsAttr(name = "用户名", req = true, len = "6,16")]
        public string uid { get; set; }

        [ParmsAttr(name = "密码", req = true, len = "6,")]
        public string pwd { get; set; }

        public string code { get; set; }

        protected override XResp Execute()
        {
            var sc = CacheHelper.Get<string>("img.code." + uid);
            if (sc == null || sc != code) throw new XExcep("0x0006");

            var u = db.x_user.FirstOrDefault(o => o.uid == uid);
            if (u != null) throw new XExcep("0x0011");

            u = new x_user() { ctime = DateTime.Now };
            u.uid = uid;
            u.pwd = Secret.MD5(pwd);

            db.x_user.InsertOnSubmit(u);
            db.SubmitDBChanges();

            return new XResp();
        }
    }
}
