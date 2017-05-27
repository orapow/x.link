using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.tag
{
    public class save : xmg
    {
        public int id { get; set; }
        [ParmsAttr(req = true, name = "名称")]
        public string name { get; set; }
        public int sort { get; set; }


        protected override XResp Execute()
        {
            x_dict ent = null;
            var code = "link.tag";

            if (id > 0) ent = db.x_dict.FirstOrDefault(o => o.dict_id == id);
            if (ent == null) ent = new x_dict() { code = code };

            ent.upval = "0";
            ent.name = name;
            ent.sort = sort;
        

            if (id == 0) db.x_dict.InsertOnSubmit(ent);
            db.SubmitDBChanges();

            ent.value = ent.dict_id + "";

            db.SubmitDBChanges();

            CacheHelper.Remove("dict." + ent.code);

            return new XResp();
        }
    }
}
