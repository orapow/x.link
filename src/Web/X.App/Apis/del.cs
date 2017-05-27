using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis
{
    public class del : xapi
    {
        [ParmsAttr(name = "网址id", min = 1)]
        public int id { get; set; }

        protected override XResp Execute()
        {
            var uk = GetReqParms("x.user");
            if (string.IsNullOrEmpty(uk)) throw new XExcep("0x0005");

            var u = db.x_user.FirstOrDefault(o => o.ukey == uk);
            if (u == null) throw new XExcep("0x0005");

            var link = u.x_url.FirstOrDefault(o => o.url_id == id);
            if (link == null) throw new XExcep("0x0008");

            db.x_url.DeleteOnSubmit(link);
            db.SubmitDBChanges();

            return new XResp();
        }

    }
}
