using System.Linq;
using X.Core.Cache;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.cate
{
    public class del : xmg
    {
        [ParmsAttr(min = 1)]
        public int id { get; set; }

        protected override XResp Execute()
        {
            var ent = db.x_dict.FirstOrDefault(o => o.dict_id == id);
            if (ent == null) throw new XExcep("0x0009");

            var upv = "";
            if (ent.upval != "0") upv = ent.upval + "-" + ent.value;
            else upv = ent.value;

            var childs = db.x_dict.Where(o => o.upval.StartsWith(upv));
            foreach (var e in childs.ToList())
            {
                if (e.upval == ent.value) e.upval = ent.upval;
                if (ent.upval == "0") e.upval = e.upval.Replace(ent.value + "-", "");
                else e.upval = e.upval.Replace("-" + ent.value, "");
            }

            db.x_dict.DeleteOnSubmit(ent);

            var us = db.x_url.Where(o => (o.cate + "") == ent.value);
            foreach (var u in us) u.cate = 0;

            db.SubmitDBChanges();
            CacheHelper.Remove("dict." + ent.code);

            return new XResp();
        }
    }
}
