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
    public class save : xmg
    {
        public int id { get; set; }
        [ParmsAttr(req = true, name = "名称")]
        public string name { get; set; }
        public string lg { get; set; }
        public string upv { get; set; }
        public int top { get; set; }
        public int sort { get; set; }


        protected override XResp Execute()
        {
            x_dict ent = null;
            var code = "link.cate." + lg;

            if (id > 0) ent = db.x_dict.FirstOrDefault(o => o.dict_id == id);
            if (ent == null) ent = new x_dict() { code = code };

            ent.name = name;
            ent.f3 = top;
            ent.sort = sort;

            if (ent.id == 0)
            {
                ent.upval = upv;
                db.x_dict.InsertOnSubmit(ent);
                db.SubmitDBChanges();
            }
            else if (ent.upval != upv)
            {
                var up = upv;
                if (up != "0") { var u = db.GetDict(code, upv); up = (u.upval == "0" ? u.value : u.upval + "-" + u.value); }
                List<x_dict> list = null;
                if (ent.upval == "0")
                {
                    list = db.x_dict.Where(o => o.code == code && (o.upval.StartsWith(ent.value + "-") || o.upval == ent.value)).OrderBy(o => o.upval).ToList();
                }
                else
                {
                    list = db.x_dict.Where(o => o.code == code && (o.upval.StartsWith(ent.upval + "-") || o.upval == ent.upval)).OrderBy(o => o.upval).ToList();
                }
                ent.upval = up;
                Debug.WriteLine("ent->" + ent.value + "|" + ent.upval);
                if (up != "0") up = up + "-" + ent.value;
                else up = ent.value;
                foreach (var s in list)
                {
                    if (s.value == ent.value) continue;
                    s.upval = up;
                    Debug.WriteLine("up->" + up);
                    if (up == "0") up = s.value;
                    else up = s.upval + "-" + s.value;
                }
            }

            //if (upv == ent.value || string.IsNullOrEmpty(upv)) upv = "0";

            //if (upv != "0")
            //{
            //    var up = db.GetDict(code, upv);
            //    if (up != null)
            //    {
            //        if (up.upval == "0") ent.upval = up.value;
            //        else ent.upval = up.upval + "-" + up.value;
            //    }
            //}
            //else ent.upval = upv;

            ent.value = ent.dict_id + "";

            db.SubmitDBChanges();

            CacheHelper.Remove("dict." + ent.code);

            return new XResp();
        }
    }
}
