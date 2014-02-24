using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _BLL;
using _commen;

namespace CmsApp20.CmsBack
{
    public class QH_SystemInfoNew : Page
    {
        private BLL Bll1 = new BLL();
        protected Repeater RP_Log;
        protected string strBrowser;
        protected string strDirRight;
        protected string strDownload;
        protected string strHighestVersion;
        protected string strIP;
        protected string strMessage;
        protected string strNetVer;
        protected string strNews;
        protected string strOPSys;
        protected string strProduct;
        protected string strServerIP;
        protected string strServerNetVer;
        protected string strServerOS;
        protected string strVersion;
        protected string strWaterMark;

        private void DataTobind()
        {
            this.strVersion = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select top 1 Version from QH_SiteInfo "));
            this.strMessage = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select count(*) from QH_Message "));
            this.strNews = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select count(*) from QH_News "));
            this.strProduct = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select count(*) from QH_Product "));
            this.strDownload = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select count(*) from QH_Download "));
            this.strIP = this.Bll1.GetClientIP();
            string userAgent = (base.Request.UserAgent == null) ? "无" : base.Request.UserAgent;
            this.strOPSys = this.GetOSNameByUserAgent(userAgent);
            this.strBrowser = base.Request.Browser.Browser + base.Request.Browser.Version;
            this.strNetVer = base.Request.Browser.ClrVersion.ToString();
            this.strHighestVersion = this.GetHighestVersion();
            this.strServerIP = this.GetServerIP();
            this.strServerOS = this.GetOSNameByUserAgent(this.GetOSVersion());
            this.strServerNetVer = this.GetNetVersion();
            this.strDirRight = this.GetServerDirRight() ? "√" : "\x00d7";
            this.strWaterMark = this.GetServerWMRight() ? "√" : "\x00d7";
            this.Bll1.ReaderBind("SELECT *,switch(State=True,'成功',State=False,'失败') as State ,Format(LoginTime,'yyyy-MM-dd HH:mm:ss') as LoginTime FROM AdminLog order by LoginTime desc", this.RP_Log, "");
        }

        private string GetHighestVersion()
        {
            string str;
            WebClient client = new WebClient();
            try
            {
                str = client.DownloadString("http://www.95c.com.cn/LastVersion/versionPre.txt");
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("读版本号文件错误！" + exception.ToString());
                return "读版本号文件错误！";
            }
            int nStart = 0;
            return this.ReadLineContent(str, ref nStart);
        }

        private string GetNetVersion()
        {
            return Environment.Version.ToString();
        }

        private string GetOSNameByUserAgent(string userAgent)
        {
            string str = "未知";
            if (userAgent.Contains("NT 6.2"))
            {
                return "Windows 8";
            }
            if (userAgent.Contains("NT 6.1"))
            {
                return "Windows 7";
            }
            if (userAgent.Contains("NT 6.0"))
            {
                return "Windows Vista/Server 2008";
            }
            if (userAgent.Contains("NT 5.2"))
            {
                return "Windows Server 2003";
            }
            if (userAgent.Contains("NT 5.1"))
            {
                return "Windows XP";
            }
            if (userAgent.Contains("NT 5"))
            {
                return "Windows 2000";
            }
            if (userAgent.Contains("NT 4"))
            {
                return "Windows NT4";
            }
            if (userAgent.Contains("98"))
            {
                return "Windows 98";
            }
            if (userAgent.Contains("95"))
            {
                return "Windows 95";
            }
            if (userAgent.Contains("Mac"))
            {
                return "Mac";
            }
            if (userAgent.Contains("Unix"))
            {
                return "UNIX";
            }
            if (userAgent.Contains("Linux"))
            {
                return "Linux";
            }
            if (userAgent.Contains("SunOS"))
            {
                return "SunOS";
            }
            if (userAgent.Contains("Android"))
            {
                return "Android";
            }
            if (userAgent.Contains("Me"))
            {
                str = "Windows Me";
            }
            return str;
        }

        private string GetOSVersion()
        {
            return Environment.OSVersion.ToString();
        }

        private bool GetServerDirRight()
        {
            bool flag = true;
            string path = base.Server.MapPath(DateTime.Now.ToString("../yyyyMMDDHHmmssfff"));
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }
            try
            {
                if (Directory.CreateDirectory(path) == null)
                {
                    flag = false;
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("验证目录创建失败！" + exception.ToString());
                flag = false;
            }
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }
            return flag;
        }

        private string GetServerIP()
        {
            string str = "未知";
            foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (address.AddressFamily.ToString() == "InterNetwork")
                {
                    str = address.ToString();
                }
            }
            return str;
        }

        private bool GetServerWMRight()
        {
            DirectoryInfo info = new DirectoryInfo(base.Server.MapPath("../upload"));
            if (!info.Exists)
            {
                info.Create();
            }
            foreach (FileSystemAccessRule rule in info.GetAccessControl().GetAccessRules(true, true, typeof(NTAccount)))
            {
                if ((rule.IdentityReference.Value.Trim() == @"BUILTIN\Users") && ((rule.FileSystemRights & FileSystemRights.Write) != 0))
                {
                    return true;
                }
            }
            return false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.DataTobind();
            }
        }

        private string ReadLineContent(string strTemp, ref int nStart)
        {
            string str = "";
            int index = strTemp.IndexOf("\n", nStart);
            if (index == -1)
            {
                str = strTemp.Substring(nStart, strTemp.Length - nStart).Trim();
                if (str == "")
                {
                    return "未能读取版本号！";
                }
                nStart = strTemp.Length;
                return str;
            }
            str = strTemp.Substring(nStart, index - nStart).Trim();
            nStart = index + 1;
            return str;
        }

        protected void RP_Log_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) && (((DbDataRecord)e.Item.DataItem)["State"].ToString() == "失败"))
            {
                HtmlContainerControl control = (HtmlContainerControl)e.Item.FindControl("State");
                control.InnerHtml = "<font color=red>失败</font> " + ((DbDataRecord)e.Item.DataItem)["Pwd"].ToString();
            }
        }
    }
}
