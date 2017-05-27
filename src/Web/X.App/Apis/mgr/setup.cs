using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X.Core.Utility;
using X.App;
using X.Web.Com;
using X.Web;

namespace X.App.Apis.mgr
{
    /// <summary>
    /// 常规配置
    /// </summary>
    public class setup : xmg
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string domain { get; set; }//


        /// <summary>
        /// 系统名称
        /// </summary>
        public string name { get; set; }//网站名
     
        protected override Web.Com.XResp Execute()
        {
            cfg.domain = domain;
            cfg.name = name;
            Config.SaveConfig(cfg);
            return new XResp();
        }
    }
}
