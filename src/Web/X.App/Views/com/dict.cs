using System;
using System.Collections.Generic;
using System.Linq;
using X.Web.Views;

namespace X.App.Views.com
{
    public class dict : xview
    {
        [ParmsAttr(name = "代号", req = true)]
        public string cd { get; set; }
        /// <summary>
        /// 上一级
        /// </summary>
        public string up { get; set; }
        /// <summary>
        /// 显示模式
        /// 1、列表
        /// 2、树型
        /// 3、按字母分类
        /// </summary>
        public int md { get; set; }
        /// <summary>
        /// 无值项，不为空时显示
        /// </summary>
        public string no { get; set; }
        public string val { get; set; }

        protected override void InitDict()
        {
            base.InitDict();
            if (md == 1 || md == 0)
            {
                dict.Add("list", db.GetDictList(cd, up));
            }
            else if (md == 2)
            {
                var tree = new XTree();
                tree.LoadList += tree_LoadList;
                tree.InitTree("", 5);
                dict.Add("list", tree.OutTree());
            }
            else if (md == 3)
            {
                dict.Add("list", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList());
            }
            if (!string.IsNullOrEmpty(no)) dict["no"] = Context.Server.HtmlDecode(no);
        }

        List<TreeNode> tree_LoadList(object id)
        {
            var list = db.GetDictList(cd, id + "").Where(o => o.value != val);
            if (list == null) return null;
            return list.Select(m => new TreeNode()
            {
                name = m.name,
                value = m.value
            }).ToList<TreeNode>();
        }

        protected override string GetParmNames
        {
            get
            {
                return "cd-up-md-no-val";
            }
        }

        public List<x_dict> GetByLetter(char l)
        {
            var list = db.GetDictList(cd, up);
            return list.FindAll(d => { return d.jp.ToUpper()[0] == l; });
        }
    }
}
