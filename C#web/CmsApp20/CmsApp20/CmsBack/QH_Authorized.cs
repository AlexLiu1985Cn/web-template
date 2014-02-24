using _BLL;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _commen;

namespace CmsApp20.CmsBack
{
    public class QH_Authorized : Page
    {
        private BLL Bll1 = new BLL();
        protected Button Button1;
        protected HtmlForm form1;
        protected TextBox TbAuthorCode;

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.TbAuthorCode.Text.Trim().Length != 0x40)
            {
                base.Response.Write("<script>alert('授权码应为64位。');</script>");
            }
            else
            {
                this.SetSiteInfoAuthorCode();
                this.SetAuthorCodeState();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Commen1.JudgeLogin(this.Page);
        }

        private void SetAuthorCodeState()
        {
            string domainName = this.Bll1.GetDomainName(base.Request.Url.AbsoluteUri);
            if (!this.Bll1.TestIfIsNotIPaddressOrLocalhost(domainName))
            {
                base.Response.Write("<script>alert('当前网址不是正式域名。');</script>");
            }
            else
            {
                string str3;
                domainName = this.Bll1.GetToEncryptDomain(domainName);
                string strEnCode = this.TbAuthorCode.Text.Trim();
                if (strEnCode[0x3f] == '1')
                {
                    str3 = this.Bll1.DecodeAuthorCode(strEnCode);
                }
                else
                {
                    str3 = this.Bll1.DecodeAuthorCodeTry(strEnCode);
                }
                string s = str3.Substring(0, 0x20);
                DllInvoke invoke = new DllInvoke(base.Server.MapPath("/bin/Commen2.dll"));
                EncryptPwd pwd = (EncryptPwd)invoke.Invoke("EncryptPwd", typeof(EncryptPwd));
                string str5 = "";
                try
                {
                    IntPtr aEncrypt = Marshal.StringToHGlobalAnsi(s);
                    IntPtr aKeyID = Marshal.StringToHGlobalAnsi(domainName);
                    str5 = Marshal.PtrToStringAnsi(pwd(aEncrypt, aKeyID));
                }
                catch (Exception exception)
                {
                    string str6 = exception.Message.Replace("\"", " ").Replace("'", " ").Replace(@"\", " ");
                    base.Response.Write("<script>alert('" + str6 + "')</script>");
                    return;
                }
                if (!(str5.Trim() == ""))
                {
                    string str7 = str3.Substring(0x2a, 0x15);
                    string str8 = FormsAuthentication.HashPasswordForStoringInConfigFile(domainName, "MD5").Substring(0, 0x15);
                    if (str7 != str8)
                    {
                        base.Response.Write("<script>alert('授权码二度校验失败!')</script>");
                    }
                    else
                    {
                        string strValue = "010101";
                        if (strEnCode[0x3f] == '0')
                        {
                            string str9 = this.Bll1.DateDay10Decode(str3.Substring(0x20, 10));
                            if (str9 == "0101010001")
                            {
                                base.Response.Write("<script>alert('授权码校验失败!')</script>");
                                return;
                            }
                            strValue = str9.Substring(0, 6);
                        }
                        if (this.SetSiteInfoAuthorCode())
                        {
                            if (strEnCode[0x3f] == '0')
                            {
                                this.Bll1.SetAuthorDatabase(strValue, base.Server.MapPath("QH_WaterMarkSet.aspx"));
                                this.Bll1.SetAuthorDatabase2(strValue, base.Server.MapPath("Admin_User.aspx"));
                            }
                            this.Session["CheckTryCode"] = true;
                            base.Response.Write("<script>alert('授权成功!');window.parent.frames[0].location.reload();</script>");
                        }
                        else
                        {
                            base.Response.Write("<script>alert('授权失败!')</script>");
                        }
                    }
                }
                else
                {
                    base.Response.Write("<script>alert('授权码校验失败!')</script>");
                }
            }
        }

        private bool SetSiteInfoAuthorCode()
        {
            try
            {
                this.Bll1.DAL1.ExecuteNonQuery("Update QH_SiteInfo Set  AuthorCode='" + this.TbAuthorCode.Text.Trim() + "' where id=1");
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("更新授权码网站信息数据库错误： " + exception.ToString());
                return false;
            }
            return true;
        }

        public class DllInvoke
        {
            private IntPtr hLib;

            public DllInvoke(string DLLPath)
            {
                try
                {
                    this.hLib = LoadLibrary(DLLPath);
                }
                catch (Exception exception)
                {
                    SystemError.CreateErrorLog(exception.ToString());
                }
            }

            ~DllInvoke()
            {
                FreeLibrary(this.hLib);
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern bool FreeLibrary(IntPtr lib);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern uint GetLastError();
            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr GetProcAddress(IntPtr lib, string funcName);
            public Delegate Invoke(string APIName, Type t)
            {
                return Marshal.GetDelegateForFunctionPointer(GetProcAddress(this.hLib, APIName), t);
            }

            [DllImport("kernel32.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, SetLastError = true)]
            private static extern IntPtr LoadLibrary(string path);
        }

        public delegate IntPtr EncryptPwd(IntPtr AEncrypt, IntPtr AKeyID);
    }
}
