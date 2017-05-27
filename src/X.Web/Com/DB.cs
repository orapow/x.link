using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace X.Web.Com
{
    public abstract class DB : DataContext
    {
        public static MappingSource mappingSource = new AttributeMappingSource();

        public DB() :
                base("", mappingSource)
        { }

        public DB(string connection) :
                base(connection, mappingSource)
        { }

        public DB(IDbConnection connection) :
                base(connection, mappingSource)
        { }

        public DB(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
                base(connection, mappingSource)
        { }

        public DB(IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
                base(connection, mappingSource)
        { }

        /// <summary>
        /// 获取字典文字
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value">
        /// 多个值用 , 隔开
        /// </param>
        /// <returns></returns>
        public abstract string GetDictName(string code, object value, string split);

        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="upval"></param>
        /// <returns></returns>
        public abstract Dictionary<string, string> GetDictList(string code, string upval);

        public static string Get_Time(string t)
        {
            if (string.IsNullOrEmpty(t)) return "";
            DateTime st;
            var b = DateTime.TryParse(t, out st);
            if (!b) return "";
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
}
