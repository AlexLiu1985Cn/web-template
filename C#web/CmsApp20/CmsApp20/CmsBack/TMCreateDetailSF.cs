namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using TProcess;

    public class TMCreateDetailSF : Page
    {
        private string[] astrTitle = new string[] { "<a href=\"TMCreateDetailSF.aspx?Mdl=ND\"> - 生成新闻内容页</a>", "<a href=\"TMCreateDetailSF.aspx?Mdl=PD\"> - 生成产品内容页</a>", "<a href=\"TMCreateDetailSF.aspx?Mdl=DD\"> - 生成下载内容页</a>", "<a href=\"TMCreateDetailSF.aspx?Mdl=MD\"> - 生成图片内容页</a>" };
        private BLL Bll1 = new BLL();
        protected Button Btn1;
        private CreateStatic CS;
        private CreateStatic CSMobile;
        protected HtmlForm form1;
        protected HtmlGenericControl Msg;
        private int nCreateNumber;
        protected string strCreateF;
        private string strFName;
        private string strFNameMobile;
        private string strMdl;
        protected string strMdlName = "";
        private string strPageTop;

        protected void Btn1_Click(object sender, EventArgs e)
        {
            string str2;
            string str3;
            string str5;
            base.Response.Write("<link rel=\"stylesheet\" href=\"css/cdqh.css\" type=\"text/css\" />");
            this.DataToBind();
            string strAbsPath = this.Bll1.getTemplateAbsPath(this.strFName, base.Server.MapPath(@"..\template\"), this.strMdlName, out str3, out str2);
            string str4 = this.Bll1.getTemplateAbsPath_Mobile(this.strFNameMobile, base.Server.MapPath(@"..\template\"), this.strMdlName, out str3, out str5);
            if ((strAbsPath == "") && (str4 == ""))
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "File", "<script>alert('" + str2 + @"\n" + str5 + "');</script>");
                base.Response.Write(str2);
                this.Btn1.Visible = false;
            }
            else
            {
                this.CS = new CreateStatic(base.Request.Url.AbsoluteUri);
                if (Convert.ToBoolean(this.Bll1.DAL1.ExecuteScalar("select top 1 MobileOn from QH_SiteInfo")) && (str4 != ""))
                {
                    this.CSMobile = new CreateStatic(base.Request.Url.AbsoluteUri, "Mobile");
                }
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
                this.Msg.InnerHtml = "";
                string strMdl = this.strMdl;
                if (strMdl != null)
                {
                    if (!(strMdl == "ND"))
                    {
                        if (strMdl == "PD")
                        {
                            if (strAbsPath == "")
                            {
                                this.Msg.InnerHtml = str2;
                            }
                            else
                            {
                                this.Msg.InnerHtml = this.Msg.InnerHtml + "<br>" + this.CS.CreateProductDetails(strAbsPath, this.nCreateNumber, "", "TMCreateDetailSF");
                            }
                            if (this.CSMobile != null)
                            {
                                this.CSMobile.CreateProductDetails(str4, this.nCreateNumber, "m", "TMCreateDetailSF");
                            }
                        }
                        else if (strMdl == "DD")
                        {
                            if (strAbsPath == "")
                            {
                                this.Msg.InnerHtml = str2;
                            }
                            else
                            {
                                this.Msg.InnerHtml = this.Msg.InnerHtml + "<br>" + this.CS.CreateDownloadDetails(strAbsPath, this.nCreateNumber, "", "TMCreateDetailSF");
                            }
                        }
                        else if (strMdl == "MD")
                        {
                            if (strAbsPath == "")
                            {
                                this.Msg.InnerHtml = str2;
                            }
                            else
                            {
                                this.Msg.InnerHtml = this.Msg.InnerHtml + "<br>" + this.CS.CreateImageDetails(strAbsPath, this.nCreateNumber, "", "TMCreateDetailSF");
                            }
                            if (this.CSMobile != null)
                            {
                                this.CSMobile.CreateImageDetails(str4, this.nCreateNumber, "m", "TMCreateDetailSF");
                            }
                        }
                    }
                    else
                    {
                        if (strAbsPath == "")
                        {
                            this.Msg.InnerHtml = str2;
                        }
                        else
                        {
                            this.Msg.InnerHtml = this.Msg.InnerHtml + "<br>" + this.CS.CreateNewsDetails(strAbsPath, this.nCreateNumber, "", "TMCreateDetailSF");
                        }
                        if (this.CSMobile != null)
                        {
                            this.CSMobile.CreateNewsDetails(str4, this.nCreateNumber, "m", "TMCreateDetailSF");
                        }
                    }
                }
                base.Response.Write("<script language=javascript>HideWait();</script>");
            }
        }

        private void DataToBind()
        {
            this.strMdl = (base.Request.QueryString["Mdl"] == null) ? "" : base.Request.QueryString["Mdl"];
            if (this.strMdl == "")
            {
                base.Response.Write("<script language=javascript>window.alert('参数错误！');</script>");
                this.Btn1.Visible = false;
            }
            else
            {
                this.strFName = this.Bll1.GetTmpltFileName(this.strMdl, out this.strMdlName);
                this.Btn1.Text = "生成" + this.strMdlName;
                if (this.strMdlName == "NoFind")
                {
                    base.Response.Write("<script language=javascript>window.alert('参数错误！');</script>");
                    this.Btn1.Visible = false;
                }
                else
                {
                    this.strFNameMobile = this.Bll1.GetTmpltFileName_mobile(this.strMdl, out this.strMdlName);
                    string str = (this.strMdl == "0") ? "" : " - ";
                    this.astrTitle[this.GetFileOrder(this.strMdl)] = str + this.Btn1.Text;
                    StringBuilder builder = new StringBuilder();
                    foreach (string str2 in this.astrTitle)
                    {
                        builder.Append(str2);
                    }
                    this.strCreateF = builder.ToString();
                    this.strPageTop = "<div class=\"glrihgt\"><div class=\"glrihgtnei\"><div class=\"rightmain\" style =\"font-size :12px; font-family :'Microsoft YaHei';\">当前位置：生成静态页 >> <a href=\"TMCreateDetailSF.aspx?Mdl=ND\">生成内容静态页</a></div><div class=\"rightmain1\" style =\"font-size :12px; font-family :'Microsoft YaHei';\">二级位置：" + this.strCreateF + "</div><div class=\"rightmain1\"> </div></div></div>";
                    base.Response.Write(this.strPageTop);
                }
            }
        }

        private int GetFileOrder(string strMdl)
        {
            int num = 0;
            string str = strMdl;
            if (str == null)
            {
                return num;
            }
            if (!(str == "ND"))
            {
                if (str != "PD")
                {
                    if (str == "DD")
                    {
                        return 2;
                    }
                    if (str != "MD")
                    {
                        return num;
                    }
                    return 3;
                }
            }
            else
            {
                return 0;
            }
            return 1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                string str = base.Request.QueryString["LastNumber"];
                if (string.IsNullOrEmpty(str))
                {
                    this.DataToBind();
                    this.nCreateNumber = 0;
                }
                else
                {
                    this.Msg.InnerHtml = "已生成文件数：" + str + "个";
                    this.nCreateNumber = int.Parse(str);
                    this.Btn1_Click(new object(), new EventArgs());
                }
            }
        }
    }
}