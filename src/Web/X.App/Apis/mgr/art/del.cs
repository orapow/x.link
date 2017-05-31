using System.Linq;
using X.Core.Cache;
using X.Web;
using X.Web.Com;
namespace X.App.Apis.mgr.art
{
    public class del : xmg
    {
        [ParmsAttr(min = 1)]
        public int id { get; set; }

        protected override XResp Execute()
        {
            var ent = db.x_art.FirstOrDefault(o => o.id == id);
            if (ent == null) throw new XExcep("0x0008");

            db.x_art.DeleteOnSubmit(ent);
            db.SubmitDBChanges();

            return new XResp();
        }
    }
}
