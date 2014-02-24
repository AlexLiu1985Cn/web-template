using System;
using System.Web.UI;

namespace CmsApp20.CmsBack
{
    public class Left_MobileManage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Commen1.JudgeLogin(this.Page);
        }
    }
}
