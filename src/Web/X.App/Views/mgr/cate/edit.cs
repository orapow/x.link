using System.Linq;
using X.Web;

namespace X.App.Views.mgr.cate
{
    public class edit : xmg
    {
        public int id { get; set; }
        public string up { get; set; }
        public string lg { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id-up-lg";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();

            if (id > 0)
            {
                var ent = db.GetDict("link.cate." + lg, id + "");
                if (ent == null) throw new XExcep("0x0005");
                dict.Add("item", ent);
                dict["up"] = ent.upval.Split('-').Last();
            }
        }
    }
}
