using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.art
{
    public class save : xmg
    {
        public int id { get; set; }
        [ParmsAttr(req = true, name = "标题")]
        public string title { get; set; }
        public string content { get; set; }

        protected override XResp Execute()
        {
            x_art ent = null;

            if (id > 0) ent = db.x_art.FirstOrDefault(o => o.id == id);
            if (ent == null) ent = new x_art() { add_time = DateTime.Now };

            ent.title = title;
            ent.content = content;
            ent.add_time = DateTime.Now;
            ent.update_time = DateTime.Now;

            if (id == 0) db.x_art.InsertOnSubmit(ent);

            db.SubmitDBChanges();

            return new XResp();
        }
    }
}
