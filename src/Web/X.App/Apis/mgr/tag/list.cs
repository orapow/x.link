using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using X.Web.Com;

namespace X.App.Apis.mgr.tag
{
    public class list : xmg
    {
        [ParmsAttr(name = "页码", def = 1)]
        public int page { get; set; }
        [ParmsAttr(name = "数量", def = 1)]
        public int limit { get; set; }
        public string key { get; set; }

        string code = "link.tag";

        protected override XResp Execute()
        {
            var r = new Resp_List();
            var tree = new XTree();
            r.items = db.GetDictList(code, "0").Skip((page - 1) * limit).Take(limit).Select(o => new
            {
                o.id,
                o.name,
                o.sort
            });
            return r;
        }

    }
}
