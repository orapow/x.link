using System.Linq;
using X.Web;

namespace X.App.Views.mgr.tag
{
    public class edit : xmg
    {
        public int id { get; set; }
        public string up { get; set; }

        protected override string GetParmNames
        {
            get
            {
                return "id-up";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();

            if (id > 0)
            {
                var ent = db.x_dict.FirstOrDefault(o => o.dict_id == id);
                if (ent == null) throw new XExcep("0x0005");
                dict.Add("item", ent);
                dict["up"] = ent.upval.Split('-').Last();
            }
        }
    }
}
