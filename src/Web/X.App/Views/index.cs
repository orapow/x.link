using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;

namespace X.App.Views
{
    public class index : xview
    {
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("tags", db.GetDictList("link.tag", "0"));
            dict.Add("tops", db.x_url.Where(o => o.top == 1).ToList());

            var uk = GetReqParms("x.user");
            if (!string.IsNullOrEmpty(uk))
            {
                var u = db.x_user.FirstOrDefault(o => o.ukey == uk);
                if (u != null)
                {
                    dict.Add("mylinks", db.x_url.Where(o => o.user_id == u.user_id).ToList());
                    dict.Add("cu", u);
                }
            }

        }
        public List<x_url> getLinksByTag(string t)
        {
            return db.x_url.Where(o => o.tags.Contains("[" + t + "]")).ToList();
        }
        public List<x_dict> getCates(string lg)
        {
            return db.GetDictList("link.cate." + lg, "0");
        }
    }
}
