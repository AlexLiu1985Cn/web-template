using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace CmsApp20.CmsBack
{
    public class Left_Statistics : Page
    {
        protected HtmlForm f111;

        protected void Page_Load(object sender, EventArgs e)
        {
            Commen1.JudgeLogin(this.Page);
        }
    }
}
