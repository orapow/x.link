using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;

namespace X.App.Views
{
    public class content : xview
    {
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();

            if (id > 0)
            {
                var ent = db.x_art.FirstOrDefault(o => o.id == id);
                if (ent == null) throw new XExcep("0x0005");
                dict.Add("item", ent);
            }

        }
    }
}
