using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using X.Web.Com;

namespace X.App.Apis.mgr.cate
{
    public class list : xmg
    {
        public string key { get; set; }
        public string lg { get; set; }

        string code = "link.cate.";

        protected override XResp Execute()
        {
            var r = new Resp_List();
            var tree = new XTree();
            tree.LoadList += tree_LoadList;
            tree.InitTree("", 5);

            var list = tree.OutTree();
            if (!string.IsNullOrEmpty(key)) list = list.Where(o => o.name.Contains(key)).ToList();
            r.items = list;

            return r;
        }

        List<TreeNode> tree_LoadList(object id)
        {
            var list = db.GetDictList(code + lg, id + "");
            if (list == null) return null;
            return list.OrderByDescending(o => o.sort).Select(m => new item()
            {
                id = m.dict_id,
                name = m.name,
                value = m.value,
                top = m.f3.Value == 1 ? "是" : "否",
                sort = m.sort.Value
            }).ToList<TreeNode>();
        }

        public class item : TreeNode
        {
            public long id { get; set; }
            public string top { get; set; }
            public int sort { get; set; }
            public item() : base("") { }
        }

    }
}
