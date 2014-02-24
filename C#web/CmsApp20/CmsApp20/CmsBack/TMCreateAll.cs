namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using TProcess;
    using _commen;

    public class TMCreateAll : Page
    {
        private string[] astrModule = new string[] { "0", "1", "2", "3", "4", "5", "6", "8", "ND", "PD", "DD", "MD", "TS", "PI" };
        private BLL Bll1 = new BLL();
        protected Button Btn1;
        private CreateStatic CS;
        private CreateStatic CSMobile;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected HtmlGenericControl Msg;
        private int nCreateNumber;
        private const string strFile = "CreateAllError.txt";
        private string strFName;
        private string strFNameMobile;
        private string strMdl = "0";
        private string strPageTop;

        protected void Btn1_Click(object sender, EventArgs e)
        {
            base.Response.Write("<link rel=\"stylesheet\" href=\"css/cdqh.css\" type=\"text/css\" />");
            if (sender is Button)
            {
                this.DataToBind();
            }
            int fileOrder = this.GetFileOrder(this.strMdl);
            if (fileOrder >= 14)
            {
                base.Response.Write("<script language=javascript>alert('全部生成成功。');window.location.href=('TMCreateAll.aspx?Mdl=0&Create=1');</script>");
            }
            else
            {
                string strMdlName = "";
                string str2 = "";
                this.strFName = this.Bll1.GetTmpltFileName(this.strMdl, out strMdlName);
                if (strMdlName == "NoFind")
                {
                    base.Response.Write("<script language=javascript>window.alert('参数错误！');</script>");
                }
                else
                {
                    string str4;
                    string str5;
                    string str7;
                    this.strFNameMobile = this.Bll1.GetTmpltFileName_mobile(this.strMdl, out str2);
                    string strAbsPath = this.Bll1.getTemplateAbsPath(this.strFName, base.Server.MapPath(@"..\template\"), strMdlName, out str5, out str4);
                    string str6 = this.Bll1.getTemplateAbsPath_Mobile(this.strFNameMobile, base.Server.MapPath(@"..\template\"), str2, out str5, out str7);
                    if ((strAbsPath == "") && (str6 == ""))
                    {
                        if (fileOrder >= 13)
                        {
                            base.Response.Write("<script language=javascript>alert('全部生成成功。');window.location.href='TMCreateAll.aspx?Mdl=0&Create=1';</script>");
                        }
                        else
                        {
                            this.strMdl = this.astrModule[fileOrder + 1];
                            base.Response.Write("<script language=javascript>window.location.href=('TMCreateAll.aspx?Mdl=" + this.strMdl + "');</script>");
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(this.Bll1.DAL1.ExecuteScalar("select top 1 MobileOn from QH_SiteInfo")) && (str6 != ""))
                        {
                            this.CSMobile = new CreateStatic(base.Request.Url.AbsoluteUri, "Mobile");
                        }
                        base.Response.Write("<link rel=\"stylesheet\" href=\"css/cdqh.css\" type=\"text/css\" />");
                        base.Response.Write("正在生成" + strMdlName + "...<br>");
                        base.Response.Write("<table width='98%' border='0' cellspacing='0' cellpadding='0' align='center'>");
                        base.Response.Write("  <tr>");
                        base.Response.Write("    <td width='120'><span id='mydiv'  style='font-size:14px; color:red;'>正在生成静态页面</span></td>");
                        base.Response.Write("    <td width='300'><table width='100%' border='0' cellspacing='0' cellpadding='1'>");
                        base.Response.Write("        <tr>");
                        base.Response.Write("          <td style='border-bottom: #ccc 1px solid; border-top: #ccc 1px solid; border-left: #ccc 1px solid; border-right: #ccc 1px solid'><img src='Images/Survey_1.gif' width='0' height='16' id='bar_img' name='bar_img' align='absmiddle'></td>");
                        base.Response.Write("        </tr>");
                        base.Response.Write("      </table></td>");
                        base.Response.Write("    <td><span id='bar_txt2' name='bar_txt2' style='font-size:12px; color:red;'></span><span id='bar_txt1' name='bar_txt1' style='font-size:12px'>&nbsp;</span><span style='font-size:12px'></span></td>");
                        base.Response.Write("  </tr>");
                        base.Response.Write("</table>");
                        base.Response.Write("<script language=javascript>;");
                        base.Response.Write("var dots = 0;var dotmax = 1000;var int;function ShowWait()");
                        base.Response.Write("{var output; output = '正在生成静态页面';dots++;if(dots>dotmax)dots=1;");
                        base.Response.Write("for(var x = 0;x < dots;x++){bar_img.width=dots*0.3;}}");
                        base.Response.Write("function StartShowWait(){mydiv.style.visibility = 'visible'; ");
                        base.Response.Write("int=window.setInterval('ShowWait()',10);}");
                        base.Response.Write("function HideWait(){");
                        base.Response.Write("int=window.clearInterval(int);mydiv.innerText='完成';bar_img.width=300;bar_txt1.innerHTML='100%';}");
                        base.Response.Write("StartShowWait();</script>");
                        base.Response.Flush();
                        string input = "";
                        string str9 = "";
                        switch (this.strMdl)
                        {
                            case "0":
                                if (strAbsPath != "")
                                {
                                    input = this.Msg.InnerHtml = this.CS.CreateHome(strAbsPath, "");
                                }
                                if (this.CSMobile != null)
                                {
                                    this.CSMobile.CreateHome(str6, "m");
                                }
                                this.strMdl = "1";
                                break;

                            case "1":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateJianJie(strAbsPath, "", "", null);
                                }
                                if (this.CSMobile != null)
                                {
                                    this.CSMobile.CreateJianJie(str6, "", "m", null);
                                }
                                this.strMdl = "2";
                                break;

                            case "2":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateNewsCenter(strAbsPath, "", "", null);
                                }
                                if (this.CSMobile != null)
                                {
                                    this.CSMobile.CreateNewsCenter(str6, "", "m", null);
                                }
                                this.strMdl = "3";
                                break;

                            case "3":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateProductCenter(strAbsPath, "", "", null);
                                }
                                if (this.CSMobile != null)
                                {
                                    this.CSMobile.CreateProductCenter(str6, "", "m", null);
                                }
                                this.strMdl = "4";
                                break;

                            case "4":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateDownload(strAbsPath, "", "", null);
                                }
                                this.strMdl = "5";
                                break;

                            case "5":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateImage(strAbsPath, "", "", null);
                                }
                                if (this.CSMobile != null)
                                {
                                    this.CSMobile.CreateImage(str6, "", "m", null);
                                }
                                this.strMdl = "6";
                                break;

                            case "6":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateMessage(strAbsPath, "", "");
                                }
                                if (this.CSMobile != null)
                                {
                                    this.CSMobile.CreateMessage(str6, "", "m");
                                }
                                this.strMdl = "8";
                                break;

                            case "8":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateHrDemand(strAbsPath, "", "");
                                }
                                this.strMdl = "ND";
                                break;

                            case "ND":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateNewsDetails(strAbsPath, this.nCreateNumber, "", "TMCreateAll");
                                }
                                if (this.CSMobile != null)
                                {
                                    this.CSMobile.CreateNewsDetails(str6, this.nCreateNumber, "m", "TMCreateAll");
                                }
                                this.strMdl = "PD";
                                break;

                            case "PD":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateProductDetails(strAbsPath, this.nCreateNumber, "", "TMCreateAll");
                                }
                                if (this.CSMobile != null)
                                {
                                    this.CSMobile.CreateProductDetails(str6, this.nCreateNumber, "m", "TMCreateAll");
                                }
                                this.Msg.InnerHtml = this.Msg.InnerHtml + "<br>" + input;
                                this.strMdl = "DD";
                                break;

                            case "DD":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateDownloadDetails(strAbsPath, this.nCreateNumber, "", "TMCreateAll");
                                }
                                this.Msg.InnerHtml = this.Msg.InnerHtml + "<br>" + input;
                                this.strMdl = "MD";
                                break;

                            case "MD":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateImageDetails(strAbsPath, this.nCreateNumber, "", "TMCreateAll");
                                }
                                if (this.CSMobile != null)
                                {
                                    this.CSMobile.CreateImageDetails(str6, this.nCreateNumber, "m", "TMCreateAll");
                                }
                                this.Msg.InnerHtml = this.Msg.InnerHtml + "<br>" + input;
                                this.strMdl = "TS";
                                break;

                            case "TS":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateTagsAndSearch(strAbsPath, "");
                                }
                                this.strMdl = "PI";
                                break;

                            case "PI":
                                if (strAbsPath != "")
                                {
                                    input = this.CS.CreateProductPriceInterval(strAbsPath, "", "", null);
                                }
                                this.strMdl = "End";
                                break;
                        }
                        str9 = Regex.Match(input, @"<script[\s\S]+?</script>", RegexOptions.IgnoreCase).ToString();
                        input = Regex.Replace(Regex.Replace(input, @"\u606d\u559c[^\uff01]+\uff01", "").Trim(), @"<[\s\S]+?</script>", "", RegexOptions.IgnoreCase).Trim();
                        if (input != "")
                        {
                            input = "<br>" + strMdlName + "：" + input;
                        }
                        if (this.strMdl == "1")
                        {
                            this.WriteFile(input, false);
                        }
                        else if (input != "")
                        {
                            this.WriteFile(input, true);
                        }
                        if ((this.GetFileOrder(this.strMdl) >= 14) && !str9.Contains("<script"))
                        {
                            base.Response.Write("<script language=javascript>HideWait();alert('全部生成成功。');window.location.href=('TMCreateAll.aspx?Mdl=0&Create=1');</script>");
                        }
                        else if (!str9.Contains("<script"))
                        {
                            base.Response.Write("<script language=javascript>window.location.href=('TMCreateAll.aspx?Mdl=" + this.strMdl + "');</script>");
                        }
                        else
                        {
                            base.Response.Write("<script language=javascript>HideWait();</script>" + str9);
                        }
                    }
                }
            }
        }

        private void DataToBind()
        {
            this.strMdl = (base.Request.QueryString["Mdl"] == null) ? "" : base.Request.QueryString["Mdl"];
            if (this.strMdl == "")
            {
                base.Response.Write("<script language=javascript>window.alert('参数错误！');</script>");
            }
            else
            {
                this.strPageTop = "<div class=\"glrihgt\"><div class=\"glrihgtnei\"><div class=\"rightmain\" style =\"font-size :12px; font-family :'Microsoft YaHei';\">当前位置：生成静态页 >> <a href=\"TMCreateAll.aspx?Mdl=0\">生成全部</a></div><div class=\"rightmain1\"> </div></div></div>";
                base.Response.Write(this.strPageTop);
            }
        }

        public string GetFileContent()
        {
            string str = "未能读取。";
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(base.Server.MapPath("CreateAllError.txt"), Encoding.UTF8);
                str = reader.ReadToEnd();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("读生成错误文件错误： " + exception.ToString());
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return str;
        }

        private int GetFileOrder(string strMdl)
        {
            switch (strMdl)
            {
                case "0":
                    return 0;

                case "1":
                    return 1;

                case "2":
                    return 2;

                case "3":
                    return 3;

                case "4":
                    return 4;

                case "5":
                    return 5;

                case "6":
                    return 6;

                case "8":
                    return 7;

                case "ND":
                    return 8;

                case "PD":
                    return 9;

                case "DD":
                    return 10;

                case "MD":
                    return 11;

                case "TS":
                    return 12;

                case "PI":
                    return 13;

                case "End":
                    return 14;
            }
            return 14;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page))
            {
                this.CS = new CreateStatic(base.Request.Url.AbsoluteUri);
                if (!base.IsPostBack)
                {
                    string str = base.Request.QueryString["LastNumber"];
                    if (string.IsNullOrEmpty(str))
                    {
                        this.DataToBind();
                        this.nCreateNumber = 0;
                        if (this.strMdl != "0")
                        {
                            this.Btn1_Click(new object(), new EventArgs());
                        }
                        else
                        {
                            string str2 = (base.Request.QueryString["Create"] == null) ? "" : base.Request.QueryString["Create"];
                            if (str2 == "1")
                            {
                                base.Response.Write(this.GetFileContent());
                            }
                        }
                    }
                    else
                    {
                        this.DataToBind();
                        this.Msg.InnerHtml = "已生成文件数：" + str + "个";
                        this.nCreateNumber = int.Parse(str);
                        this.Btn1_Click(new object(), new EventArgs());
                    }
                }
            }
        }

        private void WriteFile(string strContent, bool bAppend)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(base.Server.MapPath("CreateAllError.txt"), bAppend, Encoding.UTF8);
                writer.Write(strContent);
                writer.Flush();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("写生成错误文件错误： " + exception.ToString());
            }
            finally
            {
                writer.Close();
            }
        }
    }
}