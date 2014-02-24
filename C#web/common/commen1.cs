using System;
using System.Web;
using System.Web.UI;

public class Commen1
{
    public static bool bTestDomain = false;
    public static string strDomain = "bjzcxc.com";
    public static string strLoginID = "CmsUserID";
    public static string strTips = "已绑定域名，当前域名不对！";

    public static string GetAdminDir(string AbsoluteUri)
    {
        int index = AbsoluteUri.IndexOf('/', 8);
        return (AbsoluteUri.Substring(index + 1, (AbsoluteUri.IndexOf('/', index + 1) - index) - 1) + AbsoluteUri.Substring(7, index - 7));
    }

    public static bool IsDomainRight(string AbsoluteUri)
    {
        return AbsoluteUri.Contains(strDomain);
    }

    public static bool JudgeLogin(Page page)
    {
        string absoluteUri = page.Request.Url.AbsoluteUri;
        HttpCookie cookie = page.Request.Cookies[GetAdminDir(absoluteUri) + "SessionTOut"];
        if (cookie == null)
        {
            page.Response.Write("<script language=javascript>window.alert('为了系统安全，请您重新登陆');window.location.href=('Admin_Login.aspx')</script>");
            return false;
        }
        if (int.Parse(cookie.Value) > 0)
        {
            page.Response.CacheControl = "no-cache";
            bool flag = false;
            if (page.Session[strLoginID] == null)
            {
                flag = true;
            }
            else if (((string)page.Session[strLoginID]) == "")
            {
                flag = true;
            }
            if (flag)
            {
                page.Response.Write("<script language=javascript>window.alert('为了系统安全，请您重新登陆');window.location.href=('Admin_Login.aspx')</script>");
                return false;
            }
        }
        if (bTestDomain && !IsDomainRight(absoluteUri))
        {
            page.Response.Write("<script>alert(\"" + strTips + "\");location='Admin_Login.aspx';</script>");
            return false;
        }
        return true;
    }
}
