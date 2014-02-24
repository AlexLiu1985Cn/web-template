using _DAL.OleDBHelper;
using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class Admin_User_Login : Page
{
    private DAL DAL1 = new DAL();
    protected HtmlForm form1;
    protected Button ImageButton1;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected TextBox UserName;
    protected TextBox UserPwd;

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        string str = base.Request.Form["UserName"].ToString().Trim();
        string str3 = FormsAuthentication.HashPasswordForStoringInConfigFile(base.Request.Form["UserPwd"].ToString().Trim(), "MD5");
        if (Convert.ToInt32(this.DAL1.ExecuteScalar("select count(*) from admin where admin='" + str + "' and pwd='" + str3 + "'")) > 0)
        {
            base.Response.Redirect("Admin_User.aspx");
        }
        else
        {
            base.Response.Write("<script language=javascript>window.alert('请您正确输入用户名和密码！(普通用户不能登录！)');window.location.href=('Admin_User_Login.aspx');</script>");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
        {
            this.UserName.Text = "";
            this.UserPwd.Text = "";
        }
    }
}

