namespace CmsApp20.CmsBack
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class QH_DefaultTmMobile : Page
    {
        protected HtmlForm form1;
        protected HtmlHead Head1;

        protected void Page_Load(object sender, EventArgs e)
        {
            Commen1.JudgeLogin(this.Page);
        }
    }
}