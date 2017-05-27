using System.Linq;
using X.Core.Cache;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.tag
{
    public class del : xmg
    {
        [ParmsAttr(min = 1)]
        public int id { get; set; }

        protected override XResp Execute()
        {
            var ent = db.x_dict.FirstOrDefault(o => o.dict_id == id);
            if (ent == null) throw new XExcep("0x0010");

            db.x_dict.DeleteOnSubmit(ent);

            var us = db.x_url.Where(o => o.tags.Contains("[" + ent.value + "]"));
            foreach (var u in us) u.tags.Replace("[" + ent.value + "]", "");

            db.SubmitDBChanges();

            CacheHelper.Remove("dict." + ent.code);

            return new XResp();
        }
    }
}
