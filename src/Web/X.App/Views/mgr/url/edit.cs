using System.Linq;
using X.Web;

namespace X.App.Views.mgr.url
{
    public class edit : xmg
    {
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id-lg";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();

            if (id > 0)
            {
                var ent = db.x_url.FirstOrDefault(o => o.url_id == id);
                if (ent == null) throw new XExcep("0x0005");
                dict.Add("item", ent);
            }

        }
    }
}
