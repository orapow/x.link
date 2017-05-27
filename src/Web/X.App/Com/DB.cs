using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using X.Core.Cache;

namespace X.App
{
    partial class x_dict
    {
        public long id { get { return dict_id; } }
    }

    public class ent
    {
        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public string GetTimeDelay(DateTime st)
        {
            var dt = DateTime.Now;
            var sp = dt - st;
            if (sp.TotalDays > 30) return "大于30天";
            else if (sp.TotalDays > 2) return (int)sp.Days + "天前";
            else if (sp.TotalDays == 2) return "前天";
            else if (sp.TotalDays == 1) return "昨天";
            else if (sp.TotalHours > 3) return "今天";
            else if (sp.TotalHours > 0) return (int)sp.Hours + "小时前";
            else if (sp.TotalMinutes > 0) return (int)sp.Minutes + "分钟前";
            else return "刚刚";
        }
    }

    partial class DBDataContext
    {
        public string GetDictName(string code, object value) { return GetDictName(code, value, "、"); }
        /// <summary>
        /// 获取字典文字
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value">
        /// 多个值用 , 隔开
        /// </param>
        /// <returns></returns>
        public string GetDictName(string code, object value, string split)
        {
            if (value == null || string.IsNullOrEmpty(code)) return string.Empty;
            var list = GetDictList(code, "00");
            if (list == null || list.Count == 0) return string.Empty;

            var val = (value + "").Trim(); //.Split(',');
            var dicts = list.FindAll(o =>
            {
                return val == o.value || val.Contains("[" + o.value + "]"); //val.IndexOf(String.Format(",{0},", o.value)) >= 0;
            });
            if (dicts == null || dicts.Count == 0) return string.Empty;
            var ns = string.Empty;
            foreach (var d in dicts)
            {
                if (!string.IsNullOrEmpty(ns)) ns += split;
                ns += d.name;
            }
            return ns;
        }
        /// <summary>
        /// 获取单个字典
        /// </summary>
        /// <param name="code"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public x_dict GetDict(string code, string val)
        {
            var list = GetDictList(code, "00");
            return list.FirstOrDefault(o => o.value == val);
        }
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="upval"></param>
        /// <returns></returns>
        public List<x_dict> GetDictList(string code, string upval)
        {
            var key = "dict." + code;
            var list = CacheHelper.Get<List<x_dict>>(key);
            if (list == null || list.Count == 0)
            {
                var q = from c in x_dict
                        where c.code == code
                        select c;
                list = q.OrderByDescending(o => o.sort).ThenBy(o => o.dict_id).ToList();
                if (list == null || list.Count == 0) return null;
                CacheHelper.Save(key, list);
            }
            if (upval == "00") return list.Where(o => o.upval != null).ToList();
            if (string.IsNullOrEmpty(upval) || upval == "0") return list.FindAll(o => { return o.upval == "0"; });
            else
            {
                var u = list.FirstOrDefault(o => o.value == upval.Split('-').Last());
                return list.FindAll(o => { return o.upval == (u.upval == "0" ? u.value : u.upval + "-" + u.value); });
            }
        }

        public void SubmitDBChanges()
        {
            try
            {
                SubmitChanges(ConflictMode.ContinueOnConflict);
                SubmitChanges(ConflictMode.FailOnFirstConflict);
            }
            catch (ChangeConflictException)
            {
                foreach (ObjectChangeConflict occ in ChangeConflicts)
                {
                    occ.Resolve(RefreshMode.KeepChanges);
                }
            }
        }
    }
}
