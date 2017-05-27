using System.Linq;
using X.Core.Cache;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.url
{
    public class del : xmg
    {
        [ParmsAttr(min = 1)]
        public int id { get; set; }

        protected override XResp Execute()
        {
            var ent = db.x_url.FirstOrDefault(o => o.url_id == id);
            if (ent == null) throw new XExcep("0x0008");

            db.x_url.DeleteOnSubmit(ent);
            db.SubmitDBChanges();

            return new XResp();
        }
    }
}
