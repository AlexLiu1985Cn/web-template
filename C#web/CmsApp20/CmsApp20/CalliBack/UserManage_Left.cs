using System;
using System.Web.UI;

namespace CmsApp20.CalliBack
{
    public class UserManage_Left : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Commen1.JudgeLogin(this.Page);
        }
    }
}
