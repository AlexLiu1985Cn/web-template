using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace CmsApp20.CmsBack
{
    public class ContentManageAllNav : Page
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "Main", "var Frameset3=window.parent.document.getElementsByTagName(\"frameset\")[2];Frameset3.setAttribute(\"rows\",\"100,*\");var mainFrame=parent.document.getElementById(\"main-frame\");mainFrame.src=\"ContentManageAll.aspx?\"+Math .random ();", true);
            }
        }
    }
}
