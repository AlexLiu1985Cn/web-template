using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public class Admin_QH_DefaultTemplate : Page
{
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
        Commen1.JudgeLogin(this.Page);
    }
}