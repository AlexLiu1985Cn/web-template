using _BLL;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class Admin_Login : Page
{
    private BLL Bll1 = new BLL();
    protected TextBox CheckCode;
    protected HtmlGenericControl ChkCode;
    protected DropDownList DropDownList1;
    protected Button ImageButton1;
    protected HtmlGenericControl Lang;
    protected HtmlForm loginform;
    protected TextBox UserName;
    protected TextBox UserPwd;

    private void DataToBind()
    {
        bool flag = (bool)this.ViewState["ChkOnDB"];
        if (!flag)
        {
            this.ViewState["ChkOn"] = false;
            List<List<string>> list = new List<List<string>>();
            list = this.Bll1.ReadDataReaderList("select top 3 LoginTime,State from AdminLog where ClientIP='" + ((string)this.ViewState["CIP"]) + "' order by LoginTime desc", 2);
            if ((((list.Count == 3) && (list[0][1] == list[1][1])) && ((list[1][1] == list[2][1]) && (list[2][1] == "False"))) && (DateTime.Compare(DateTime.Parse(list[0][0]), DateTime.Now.AddHours(-1.0)) > 0))
            {
                this.ViewState["ChkOn"] = flag = true;
            }
        }
        if (!flag)
        {
            this.ChkCode.Style.Add("display", "none");
        }
        else
        {
            this.ChkCode.Style.Add("display", "");
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = this.DropDownList1.SelectedValue;
        string str2 = (string)this.ViewState["2ndDir"];
        string str3 = ((string)this.ViewState["AdminDir"]) + "/";
        string str4 = "";
        selectedValue = (selectedValue == "/") ? "" : (selectedValue + "/");
        if (str2 == "/")
        {
            str4 = "../" + selectedValue + str3;
        }
        else
        {
            str4 = "../../" + selectedValue + str3;
        }
        if (string.Empty != str4)
        {
            base.Response.Redirect(str4 + "Admin_Login.aspx");
        }
    }

    public static string FunStr(string str)
    {
        str = str.Replace("&", "&amp;");
        str = str.Replace("<", "&lt;");
        str = str.Replace(">", "&gt");
        str = str.Replace("'", "''");
        str = str.Replace("*", "");
        str = str.Replace("\n", "<br/>");
        str = str.Replace("\r\n", "<br/>");
        str = str.Replace("select", "");
        str = str.Replace("insert", "");
        str = str.Replace("update", "");
        str = str.Replace("delete", "");
        str = str.Replace("create", "");
        str = str.Replace("drop", "");
        str = str.Replace("delcare", "");
        str = str.Replace("   ", "&nbsp;");
        str = str.Trim();
        if (str.Trim().ToString() == "")
        {
            str = "未输入";
        }
        return str;
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if ((bool)this.ViewState["ChkOn"])
        {
            if (this.Session["code"] == null)
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "checkcodeexpired", "<script language=javascript> window.alert('验证码已失效，请刷新验证码重新获得！');</script>");
                return;
            }
            if (!this.Session["code"].ToString().ToLower().Trim().Equals(this.CheckCode.Text.ToLower().Trim()))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "checkcode", "<script language=javascript> window.alert('验证码不正确！');</script>");
                return;
            }
        }
        string str = FunStr(base.Request.Form["UserName"].ToString());
        string password = FunStr(base.Request.Form["UserPwd"].ToString());
        string str3 = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        int num = Convert.ToInt32(this.Bll1.ExecuteScalar("select count(*) from admin where admin='" + str + "' and pwd='" + str3 + "'"));
        int num2 = Convert.ToInt32(this.Bll1.ExecuteScalar("select count(*) from AdminLog "));
        string str4 = (num > 0) ? "1" : "0";
        if (num2 < 10)
        {
            string strField = "Name,Pwd,ClientIP,LoginTime,State";
            string[] astrValue = new string[] { str, (str4 == "1") ? str3 : password, (string)this.ViewState["CIP"], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), str4 };
            string strUpdate = "insert into AdminLog(Name,Pwd,ClientIP,LoginTime,State) values(@Name,@Pwd,@ClientIP,@LoginTime,@State)";
            this.Bll1.UpdateTable(strUpdate, strField, astrValue);
        }
        else
        {
            string str7 = Convert.ToInt32(this.Bll1.ExecuteScalar("select Top 1 id from AdminLog order by LoginTime asc ")).ToString();
            string str8 = "Name,Pwd,ClientIP,LoginTime,State,id";
            string[] strArray2 = new string[] { str, (str4 == "1") ? str3 : password, (string)this.ViewState["CIP"], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), str4, str7 };
            string str9 = "UPDATE AdminLog SET Name=@Name,Pwd=@Pwd ,ClientIP=@ClientIP,LoginTime=@LoginTime,State=@State WHERE ID=@id";
            this.Bll1.UpdateTable(str9, str8, strArray2);
        }
        if (num > 0)
        {
            string[] strArray3 = this.Bll1.DAL1.ReadDataReaderStringArray("select top 1 AuthorCode, AdminSsnT from QH_SiteInfo", 2);
            if (strArray3[1] == null)
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "DataErr", "<script language=javascript> window.alert('读取数据库错误。');</script>");
                this.DataToBind();
            }
            else
            {
                int num3 = int.Parse(strArray3[1]);
                HttpCookie cookie = new HttpCookie(Commen1.GetAdminDir(base.Request.Url.AbsoluteUri) + "SessionTOut")
                {
                    Value = num3.ToString()
                };
                base.Response.Cookies.Add(cookie);
                HttpCookie cookie2 = new HttpCookie("95CmsAdmin")
                {
                    Value = HttpUtility.UrlEncode(this.UserName.Text)
                };
                base.Response.Cookies.Add(cookie2);
                if (num3 > 0)
                {
                    this.Session.Timeout = num3;
                    this.Session[Commen1.strLoginID] = this.UserName.Text;
                }
                this.Session["CheckTryCode"] = true;
                if ((strArray3[0].Length == 0x40) && (strArray3[0][0x3f] == '0'))
                {
                    string fileString = this.Bll1.GetFileString(base.Server.MapPath("MainFrameNew.aspx"));
                    if (fileString.Substring(0, fileString.IndexOf('\n')).Trim().ToLower() != "<%@ Page Language=\"C#\" AutoEventWireup=\"true\" CodeBehind=\"MainFrameNew.aspx.cs\" Inherits=\"CmsApp20.CalliBack.MainFrameNew\" %>".ToLower())
                    {
                        this.Page.ClientScript.RegisterStartupScript(base.GetType(), "FileErr", "<script language=javascript> window.alert('系统文件损坏。');</script>");
                        this.DataToBind();
                        return;
                    }
                    fileString = this.Bll1.GetFileString(base.Server.MapPath("topframe.aspx"));
                    if (fileString.Substring(0, fileString.IndexOf('\n')).Trim() != "<%@ Page Language=\"C#\" AutoEventWireup=\"true\" CodeBehind=\"topframe.aspx.cs\" Inherits=\"CmsApp20.CalliBack.topframe\" %>")
                    {
                        this.Page.ClientScript.RegisterStartupScript(base.GetType(), "FileErr", "<script language=javascript> window.alert('系统文件损坏。');</script>");
                        this.DataToBind();
                        return;
                    }
                }
                base.Response.Write("<script language=javascript>window.top.location=('MainFrameNew.aspx');</script>");
            }
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "Name&Pwd", "<script language=javascript> window.alert('请您正确输入用户名和密码！');</script>");
            this.DataToBind();
        }
    }

    private void LanguageSet()
    {
        if (Convert.ToBoolean(this.Bll1.DAL1.ExecuteScalar("select top 1 MultiLang from QH_SiteInfo")))
        {
            List<string[]> list = this.Bll1.DAL1.ReadDataReaderListStr("select Lang, Dir,IDMark ,ImgUrl from QH_MultiLLang", 4);
            if (list.Count != 0)
            {
                string str = "";
                MatchCollection matchs = Regex.Matches(base.Request.Url.AbsolutePath, @"(\w+)/");
                string str3 = "/";
                if (matchs.Count > 1)
                {
                    str3 = matchs[matchs.Count - 2].Groups[1].Value.ToLower();
                }
                this.ViewState["AdminDir"] = matchs[matchs.Count - 1].Groups[1].Value;
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (string[] strArray in list)
                {
                    if (strArray[1].ToLower() == str3)
                    {
                        str = strArray[1];
                    }
                    if (!dictionary.ContainsKey(strArray[0]))
                    {
                        dictionary.Add(strArray[0], strArray[1]);
                    }
                }
                if ((str == "") && (matchs.Count > 1))
                {
                    str3 = "/";
                    foreach (string[] strArray2 in list)
                    {
                        if (strArray2[1].ToLower() == str3)
                        {
                            str = strArray2[1];
                            break;
                        }
                    }
                }
                if (str != "")
                {
                    this.ViewState["2ndDir"] = str3;
                    this.DropDownList1.DataSource = dictionary;
                    this.DropDownList1.DataTextField = "Key";
                    this.DropDownList1.DataValueField = "Value";
                    this.DropDownList1.DataBind();
                    this.DropDownList1.SelectedIndex = this.DropDownList1.Items.IndexOf(this.DropDownList1.Items.FindByValue(str));
                    this.Lang.Style["display"] = "";
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.UserName.Text = "";
            this.UserPwd.Text = "";
            this.ViewState["ChkOnDB"] = this.ViewState["ChkOn"] = Convert.ToBoolean(this.Bll1.ExecuteScalar("select top 1 AdminCH from QH_SiteInfo "));
            this.ViewState["CIP"] = this.Bll1.GetClientIP();
            this.DataToBind();
            this.LanguageSet();
        }
    }
}

