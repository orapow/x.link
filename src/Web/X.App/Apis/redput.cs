using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis
{
    public class redput : xapi
    {
        public string url { get; set; }
        public string name { get; set; }
        public string con { get; set; }

        protected override XResp Execute()
        {
            var u = db.x_user.FirstOrDefault(o => o.user_id == 1);
            if (u == null) throw new XExcep("0x0005");

            var link = new x_url();
            link.ctime = DateTime.Now;
            link.url = url;
            link.title = name;
            link.alt = con;
            link.remark = "1";
            link.tags = "[0]";
            link.user_id = 1;

            u.x_url.Add(link);
            db.SubmitDBChanges();
            return new XResp();
        }

    }
}
