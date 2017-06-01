using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using X.Web.Com;

namespace X.App.Apis.mgr.url
{
    public class list : xmg
    {
        public int page { get; set; }
        public int limit { get; set; }
        public int cate { get; set; }
        public string remark { get; set; }
        public string key { get; set; }
        public string lg { get; set; }

        protected override XResp Execute()
        {
            var r = new Resp_List();

            var q = from p in db.x_url
                    where p.tags != "[0]"
                    select p;

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.title.Contains(key) || o.url.Contains(key));
            if (cate > 0)
            {
                var ct = db.GetDict("link.cate." + lg, cate + "");
                var cts = db.GetDictList("link.cate." + lg, "00").Where(o => (o.upval == ct.value || o.upval.StartsWith(ct.value + "-") || o.upval.StartsWith(ct.upval + "-" + ct.value))).Select(o => o.value);
                q = q.Where(o => cts.Contains(o.cate + "") || o.cate == cate);
            }
            else
            {
                var cts = db.GetDictList("link.cate." + lg, "00").Where(o => o.value != null).Select(o => o.value);
                q = q.Where(o => cts.Contains(o.cate + "") || o.cate == 0);
            }

            r.page = page;
            r.count = q.Count();
            r.items = q.Skip((page - 1) * limit).Take(limit).Select(o => new
            {
                id = o.url_id,
                o.title,
                cate = db.GetDictName("link.cate." + lg, o.cate),
                o.url,
                remark=getState(o.remark),
                o.icon
            });

            return r;
        }

        private string getState(string s) { 
            string m = "error";
            switch (s)
            {
                case "1": 
                    m = "待审核";
                    break;
                case "2":
                    m = "通过";
                    break;
                case "3":
                    m = "未通过";
                    break;
            }
            return m;
        }

    }
}
