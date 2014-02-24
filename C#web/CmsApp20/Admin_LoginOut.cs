using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public class Admin_LoginOut : Page
{
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Session[Commen1.strLoginID] = null;
        base.Response.Cookies[Commen1.GetAdminDir(base.Request.Url.AbsoluteUri) + "SessionTOut"].Expires = DateTime.Now.AddDays(-1.0);
        base.Response.Write("<script language=javascript>window.alert('已安全退出，单击确定返回首页！');window.parent.location.href=('Admin_Login.aspx')</script>");
        this.Session["CheckTryCode"] = null;
    }
}
