using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using X.Web.Com;


namespace X.App.Apis.mgr.art
{
    public class list : xmg
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }

        protected override XResp Execute()
        {
            var r = new Resp_List();
            r.page = page;

            var q = from p in db.x_art
                    select p;

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.title.Contains(key) || o.content.Contains(key));

            r.items = q.Skip((page - 1) * limit).Take(limit).ToList();
            r.count = q.Count();

            return r;
        }
    }
}
