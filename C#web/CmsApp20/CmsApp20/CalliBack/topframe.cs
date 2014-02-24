using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _BLL;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using _commen;


namespace CmsApp20.CalliBack
{
    public class topframe :Page
    {
        private bool bIsTry;
        private BLL Bll1 = new BLL();
        protected string strAdmin;
        protected string strAuthorize;
        private string strDateTicks = "";
        private string strOverdue = "";

        private bool CheckAuthorized(string strDomain)
        {
            string str2;
            string strEnCode = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select top 1 AuthorCode from QH_SiteInfo"));
            if (strEnCode.Length != 0x40)
            {
                return false;
            }
            if (strEnCode[0x3f] == '1')
            {
                str2 = this.Bll1.DecodeAuthorCode(strEnCode);
            }
            else
            {
                str2 = this.Bll1.DecodeAuthorCodeTry(strEnCode);
            }
            string s = str2.Substring(0, 0x20);
            string str4 = "";
            try
            {
                DllInvoke invoke = new DllInvoke(base.Server.MapPath("/bin/Commen2.dll"));
                EncryptPwd pwd = (EncryptPwd) invoke.Invoke("EncryptPwd", typeof(EncryptPwd));
                IntPtr aEncrypt = Marshal.StringToHGlobalAnsi(s);
                IntPtr aKeyID = Marshal.StringToHGlobalAnsi(strDomain);
                str4 = Marshal.PtrToStringAnsi(pwd(aEncrypt, aKeyID));
            }
            catch (Exception exception)
            {
                string str5 = exception.Message.Replace("\"", " ").Replace("'", " ").Replace(@"\", " ");
                SystemError.CreateErrorLog("调用dll错误:" + exception.ToString());
                base.Response.Write("<script>alert('" + str5 + "')</script>");
                return false;
            }
            if (str4.Trim() == "")
            {
                return false;
            }
            string str6 = str2.Substring(0x2a, 0x15);
            string str7 = FormsAuthentication.HashPasswordForStoringInConfigFile(strDomain, "MD5").Substring(0, 0x15);
            if (str6 != str7)
            {
                return false;
            }
            if (str2[0x3f] != '1')
            {
                DateTime time;
                DateTime time2;
                this.bIsTry = true;
                string str8 = this.Bll1.DateDay10Decode(str2.Substring(0x20, 10));
                string input = str8.Substring(0, 6);
                string str10 = str8.Substring(6, 4);
                if (!Regex.IsMatch(input, @"^\d{6}$") || !Regex.IsMatch(str10, @"^\d{4}$"))
                {
                    return false;
                }
                string fileString = this.Bll1.GetFileString(base.Server.MapPath("QH_WaterMarkSet.aspx"));
                if (fileString == null)
                {
                    return false;
                }
                string str12 = this.Bll1.GetContentBetweenTags("pt\" style=\"color:#", ";", fileString);
                if (!Regex.IsMatch(str12, "^[a0c6812f3d]{6}$"))
                {
                    return false;
                }
                string str13 = "";
                char[] chArray = new char[] { 'a', '0', 'c', '6', '8', '1', '2', 'f', '3', 'd' };
                for (int i = 0; i < 6; i++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if (str12[i] == chArray[k])
                        {
                            str13 = str13 + k.ToString();
                            break;
                        }
                    }
                }
                try
                {
                    time = DateTime.Parse("20" + input.Insert(4, "-").Insert(2, "-"));
                    time2 = DateTime.Parse("20" + str13.Insert(4, "-").Insert(2, "-"));
                }
                catch
                {
                    this.strOverdue = "日期格式不对。";
                    return false;
                }
                if (time.AddDays(double.Parse(str10)).CompareTo(time2) < 0)
                {
                    this.strOverdue = "授权期已过";
                    return false;
                }
                string strTempContent = this.Bll1.GetFileString(base.Server.MapPath("Admin_User.aspx"));
                if (strTempContent == null)
                {
                    return false;
                }
                string str15 = this.Bll1.GetContentBetweenTags("<th class=\"th", "\"", strTempContent);
                if (!Regex.IsMatch(str15, "^[abcdeghfik1]{8}$"))
                {
                    return false;
                }
                string str16 = "";
                char[] chArray2 = new char[] { 'a', 'b', 'c', 'd', 'e', 'g', 'h', 'f', 'i', 'k' };
                for (int j = 0; j < 6; j++)
                {
                    for (int m = 0; m < 10; m++)
                    {
                        if (str15[j] == chArray2[m])
                        {
                            str16 = str16 + m.ToString();
                            break;
                        }
                    }
                }
                if (str13 != str16)
                {
                    this.strOverdue = "试用码效验不对。";
                    return false;
                }
                this.bIsTry = true;
            }
            return true;
        }

        public DateTime ConvertTime(long milliTime)
        {
            DateTime time = new DateTime(0x7b2, 1, 1);
            return new DateTime((time.Ticks + (milliTime * 0x2710L)) + 0x430e234000L);
        }

        private string GetContentBetweenTags(string strStartTags, string strEndTags, string strTempContent)
        {
            int length = strStartTags.Length;
            int index = strTempContent.IndexOf(strStartTags);
            if (index == -1)
            {
                return "";
            }
            int num2 = strTempContent.IndexOf(strEndTags, (int) (index + length));
            return strTempContent.Substring(index + length, (num2 - index) - length);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                string domainName = this.Bll1.GetDomainName(base.Request.Url.AbsoluteUri);
                if (!this.Bll1.TestIfIsNotIPaddressOrLocalhost(domainName))
                {
                    this.strAuthorize = "<a onclick=\"javascript:window.parent.document.getElementsByTagName('frameset')[2].setAttribute('rows','0,*');\" href=\"QH_Authorized.aspx\" target=\"main-frame\">（未授权）</a>";
                }
                else
                {
                    this.strAdmin = HttpUtility.UrlDecode(base.Request.Cookies["95CmsAdmin"].Value);
                    this.SetAuthorizedState(domainName);
                    if (this.strOverdue != "")
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), "Overdue", "alert('" + this.strOverdue + "');", true);
                    }
                    if (this.bIsTry && (this.Session["CheckTryCode"] != null))
                    {
                        this.Session["CheckTryCode"] = null;
                        ThreadStart start = new ThreadStart(this.Test);
                        new Thread(start) { IsBackground = true }.Start();
                    }
                }
            }
        }

        private void SetAuthorizedState(string strDomain)
        {
            if (this.CheckAuthorized(this.Bll1.GetToEncryptDomain(strDomain)))
            {
                this.strAuthorize = "（已授权）";
            }
            else
            {
                string str = (this.strOverdue == "授权期已过") ? "授权期已过" : "未授权";
                this.strAuthorize = "<a onclick=\"javascript:window.parent.document.getElementsByTagName('frameset')[2].setAttribute('rows','0,*');\" href=\"QH_Authorized.aspx\" target=\"main-frame\">（" + str + "）</a>";
            }
        }

        public void Test()
        {
            string strTempContent = "";
            string strValue = "";
            WebClient client = new WebClient();
            try
            {
                strTempContent = client.DownloadString("http://www.sogou.com/websearch/features/standardtimeadjust.jsp?a=" + new Random().Next());
                this.strDateTicks = this.GetContentBetweenTags("standardtime(", ",", strTempContent);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog(exception.ToString());
            }
            if (Regex.IsMatch(this.strDateTicks, @"^\d+$"))
            {
                strValue = this.ConvertTime(long.Parse(this.strDateTicks)).ToString("yyMMdd");
                this.Bll1.SetAuthorDatabase(strValue, base.Server.MapPath("QH_WaterMarkSet.aspx"));
                this.Bll1.SetAuthorDatabase2(strValue, base.Server.MapPath("Admin_User.aspx"));
            }
            else
            {
                try
                {
                    strTempContent = client.DownloadString("http://open.baidu.com/special/time/?a=" + new Random().Next());
                    this.strDateTicks = this.GetContentBetweenTags("window.baidu_time(", ")", strTempContent);
                }
                catch (Exception exception2)
                {
                    SystemError.CreateErrorLog(exception2.ToString());
                }
                if (Regex.IsMatch(this.strDateTicks, @"^\d+$"))
                {
                    strValue = this.ConvertTime(long.Parse(this.strDateTicks)).ToString("yyMMdd");
                    this.Bll1.SetAuthorDatabase(strValue, base.Server.MapPath("QH_WaterMarkSet.aspx"));
                    this.Bll1.SetAuthorDatabase2(strValue, base.Server.MapPath("Admin_User.aspx"));
                }
                else
                {
                    try
                    {
                        this.strDateTicks = client.DownloadString("http://www.95c.com.cn/ajax/Date.aspx?a=" + new Random().Next());
                    }
                    catch (Exception exception3)
                    {
                        SystemError.CreateErrorLog(exception3.ToString());
                    }
                    if (Regex.IsMatch(this.strDateTicks, @"^\d{6}$"))
                    {
                        this.Bll1.SetAuthorDatabase(this.strDateTicks, base.Server.MapPath("QH_WaterMarkSet.aspx"));
                        this.Bll1.SetAuthorDatabase2(this.strDateTicks, base.Server.MapPath("Admin_User.aspx"));
                    }
                    else
                    {
                        this.Bll1.SetAuthorDatabase("cbcbcb", base.Server.MapPath("QH_WaterMarkSet.aspx"));
                        this.Bll1.SetAuthorDatabase2("eaderText", base.Server.MapPath("Admin_User.aspx"));
                    }
                }
            }
        }

        public class DllInvoke
        {
            private IntPtr hLib;

            public DllInvoke(string DLLPath)
            {
                this.hLib = LoadLibrary(DLLPath);
            }

            ~DllInvoke()
            {
                FreeLibrary(this.hLib);
            }

            [DllImport("kernel32.dll")]
            private static extern bool FreeLibrary(IntPtr lib);
            [DllImport("kernel32.dll")]
            private static extern IntPtr GetProcAddress(IntPtr lib, string funcName);
            public Delegate Invoke(string APIName, Type t)
            {
                return Marshal.GetDelegateForFunctionPointer(GetProcAddress(this.hLib, APIName), t);
            }

            [DllImport("kernel32.dll")]
            private static extern IntPtr LoadLibrary(string path);
        }

        public delegate IntPtr EncryptPwd(IntPtr AEncrypt, IntPtr AKeyID);
    }
}

