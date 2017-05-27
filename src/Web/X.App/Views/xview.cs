using System.Collections.Generic;
using X.Web.Views;
using System.Linq;

namespace X.App.Views
{
    public class xview : View
    {
        /// <summary>
        /// 系统配置文件
        /// </summary>
        protected Config cfg = null;
        protected DBDataContext db = null;

        protected override void InitView()
        {
            base.InitView();
            cfg = Config.LoadConfig();
            dict.Add("cfg", cfg);
            db = new DBDataContext();
        }

        protected override Dictionary<string, string> GetDictList(string cd, string up)
        {
            var list = db.GetDictList(cd, up).Where(o => o.value != null);
            return list?.ToDictionary(k => k.value, v => v.name);
        }
    }
}
