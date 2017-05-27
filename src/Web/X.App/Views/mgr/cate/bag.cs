using System.Linq;
using X.Web;

namespace X.App.Views.mgr.cate
{
    public class bag : xmg
    {
        public string lg { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "lg";
            }
        }
    }
}
