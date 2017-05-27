using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis
{
    public class put : xapi
    {
        public string url { get; set; }
        public string name { get; set; }
        public int id { get; set; }

        protected override XResp Execute()
        {
            var uk = GetReqParms("x.user");
            if (string.IsNullOrEmpty(uk)) throw new XExcep("0x0005");

            var u = db.x_user.FirstOrDefault(o => o.ukey == uk);
            if (u == null) throw new XExcep("0x0005");

            x_url link = null;
            if (id > 0) link = db.x_url.FirstOrDefault(o => o.url_id == id);

            if (link == null) link = new x_url()
            {
                ctime = DateTime.Now,
                title = name,
                url = url,
                icon = url.TrimEnd('/') + "/favicon.ico"
            };

            var link2 = new x_url();
            link2.ctime = DateTime.Now;
            link2.url = link.url;
            link2.title = link.title;
            link2.icon = link.icon;

            u.x_url.Add(link2);
            db.SubmitDBChanges();

            return new XResp();
        }

    }
}
