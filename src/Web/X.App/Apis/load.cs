using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis
{
    public class load : xapi
    {
        public string lg { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public int cate { get; set; }
        protected override XResp Execute()
        {
            var r = new back();
            r.page = page;

            var q = from u in db.x_url
                    where u.user_id == 1
                    select u;

            if (page == 0) page = 1;
            if (limit == 0 || limit > 50) limit = 30;

            if (cate > 0)
            {
                var ct = db.GetDict("link.cate." + lg, cate + "");
                var cts = db.GetDictList("link.cate." + lg, "00").Where(o => (o.upval == ct.value || o.upval.StartsWith(ct.value + "-") || o.upval.StartsWith(ct.upval + "-" + ct.value))).Select(o => o.value);
                q = q.Where(o => cts.Contains(o.cate + "") || o.cate == cate);
            }

            r.count = q.Count();
            r.items = q.Skip((page - 1) * limit).Take(limit).Select(o => new { o.title, o.url, o.icon, id = o.url_id });
            r.cates = db.GetDictList("link.cate." + lg, cate + "").Select(o => new { o.name, o.value });

            return r;
        }
        class back : Resp_List
        {
            public object cates { get; set; }
        }
    }
}
