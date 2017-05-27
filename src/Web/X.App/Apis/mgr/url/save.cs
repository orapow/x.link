using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.url
{
    public class save : xmg
    {
        public int id { get; set; }
        [ParmsAttr(req = true, name = "名称")]
        public string name { get; set; }
        public int cate { get; set; }
        public int top { get; set; }
        public string url { get; set; }
        public string icon { get; set; }

        public string tags { get; set; }
        public string remark { get; set; }

        protected override XResp Execute()
        {
            x_url ent = null;

            if (id > 0) ent = db.x_url.FirstOrDefault(o => o.url_id == id);
            if (ent == null) ent = new x_url() { ctime = DateTime.Now };

            ent.title = name;
            ent.user_id = 1;
            ent.cate = cate;
            ent.icon = string.IsNullOrEmpty(icon) ? url.TrimEnd('/') + "/favicon.ico" : icon;
            ent.top = top;
            ent.url = url;
            ent.remark = remark;
            ent.tags = tags;

            if (id == 0) db.x_url.InsertOnSubmit(ent);

            db.SubmitDBChanges();

            return new XResp();
        }
    }
}
