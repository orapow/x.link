using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.cate
{
    public class bagsave : xmg
    {
        public int id { get; set; }
        [ParmsAttr(req = true, name = "名称")]
        public string name { get; set; }
        public string lg { get; set; }
        public string upv { get; set; }


        protected override XResp Execute()
        {
            var code = "link.cate." + lg;

            var ns = name.Split(' ');
            var u = db.GetDict("link.cate." + lg, upv);
            foreach (var n in ns)
            {
                var ent = new x_dict()
                {
                    code = code,
                    upval = upv != "0" ? (u.upval == "0" ? u.value : u.upval + "-" + u.value) : "0",
                    name = n,
                    sort = 99,
                    f3 = 0
                };
                db.x_dict.InsertOnSubmit(ent);
                db.SubmitDBChanges();

                ent.value = ent.dict_id + "";
                db.SubmitDBChanges();
            }

            db.SubmitDBChanges();

            CacheHelper.Remove("dict." + code);

            return new XResp();
        }
    }
}
