using _DAL.OleDBHelper;
using System;
using System.Web.UI;

namespace CmsApp20.CDQHCmsBack
{
    public class Left_WebAppManage : Page
    {
        private DAL DAL1 = new DAL();
        protected string strWeixinFuwuSet = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && (!base.IsPostBack && (Convert.ToString(this.DAL1.ExecuteScalar("select top 1 WeixinType from QH_WeixinSet")) == "1")))
            {
                this.strWeixinFuwuSet = "<div class=\"left02down01\"><a href=\"WeixinFuwuSet.aspx\" target=\"main-frame\"  onclick=SetNow(2) id=a2 >服务号设置</a></div>";
            }
        }
    }
}
