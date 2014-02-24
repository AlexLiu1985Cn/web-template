namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using TProcess;

    public class TMCreateStaticF_Mobile : Page
    {
        private string[] astrTitle = new string[] { "<a href=\"TMCreateStaticF_Mobile.aspx?Mdl=0\">生成首页</a>", "<a href=\"TMCreateStaticF_Mobile.aspx?Mdl=1\" > - 生成简介模块</a>", "<a href=\"TMCreateStaticF_Mobile.aspx?Mdl=2\" > - 生成新闻模块</a>", "<a href=\"TMCreateStaticF_Mobile.aspx?Mdl=3\" > - 生成产品模块</a>", "<a href=\"TMCreateStaticF_Mobile.aspx?Mdl=4\" > - 生成下载模块</a>", "<a href=\"TMCreateStaticF_Mobile.aspx?Mdl=5\" > - 生成图片模块</a>", "<a href=\"TMCreateStaticF_Mobile.aspx?Mdl=6\" > - 生成留言模块</a>", "<a href=\"TMCreateStaticF_Mobile.aspx?Mdl=8\" > - 生成招聘模块</a>", "<a href=\"TMCreateStaticF_Mobile.aspx?Mdl=ND\"> - 生成新闻内容页</a>", "<a href=\"TMCreateStaticF_Mobile.aspx?Mdl=PD\"> - 生成产品内容页</a>", "<a href=\"TMCreateStaticF_Mobile.aspx?Mdl=DD\"> - 生成下载内容页</a>", "<a href=\"TMCreateStaticF_Mobile.aspx?Mdl=MD\"> - 生成图片内容页</a>" };
        private BLL Bll1 = new BLL();
        protected Button Btn1;
        private CreateStatic CS;
        protected HtmlForm form1;
        protected HtmlGenericControl Msg;
        private int nCreateNumber;
        protected string strCreateF;
        private string strFName;
        private string strMdl;
        protected string strMdlName = "";
        private string strPageTop;

        protected void Btn1_Click(object sender, EventArgs e)
        {
            string str2;
            string str3;
            base.Response.Write("<link rel=\"stylesheet\" href=\"css/cdqh.css\" type=\"text/css\" />");
            this.DataToBind();
            string strAbsPath = this.Bll1.getTemplateAbsPath_Mobile(this.strFName, base.Server.MapPath(@"..\template\"), this.strMdlName, out str3, out str2);
            if (strAbsPath == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "File", "<script>alert('" + str2 + "');</script>");
                base.Response.Write(str2);
                this.Btn1.Visible = false;
            }
            else
            {
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
                switch (this.strMdl)
                {
                    case "0":
                        base.Response.Write(this.CS.CreateHome(strAbsPath, "m"));
                        break;

                    case "1":
                        base.Response.Write(this.CS.CreateJianJie(strAbsPath, str3, "m", null));
                        break;

                    case "2":
                        base.Response.Write(this.CS.CreateNewsCenter(strAbsPath, str3, "m", null));
                        break;

                    case "3":
                        base.Response.Write(this.CS.CreateProductCenter(strAbsPath, str3, "m", null));
                        break;

                    case "4":
                        base.Response.Write(this.CS.CreateDownload(strAbsPath, str3, "m", null));
                        break;

                    case "5":
                        base.Response.Write(this.CS.CreateImage(strAbsPath, str3, "m", null));
                        break;

                    case "6":
                        base.Response.Write(this.CS.CreateMessage(strAbsPath, str3, "m"));
                        break;

                    case "8":
                        base.Response.Write(this.CS.CreateHrDemand(strAbsPath, str3, "m"));
                        break;

                    case "ND":
                        this.Msg.InnerHtml = this.Msg.InnerHtml + "<br>" + this.CS.CreateNewsDetails(strAbsPath, this.nCreateNumber, "m", "TMCreateStaticF_Mobile");
                        break;

                    case "PD":
                        this.Msg.InnerHtml = this.Msg.InnerHtml + "<br>" + this.CS.CreateProductDetails(strAbsPath, this.nCreateNumber, "m", "TMCreateStaticF_Mobile");
                        break;

                    case "DD":
                        this.Msg.InnerHtml = this.Msg.InnerHtml + "<br>" + this.CS.CreateDownloadDetails(strAbsPath, this.nCreateNumber, "m", "TMCreateStaticF_Mobile");
                        break;

                    case "MD":
                        this.Msg.InnerHtml = this.Msg.InnerHtml + "<br>" + this.CS.CreateImageDetails(strAbsPath, this.nCreateNumber, "m", "TMCreateStaticF_Mobile");
                        break;
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
                this.strFName = this.Bll1.GetTmpltFileName_mobile(this.strMdl, out this.strMdlName);
                this.Btn1.Text = "生成" + this.strMdlName;
                if (this.strMdlName == "NoFind")
                {
                    base.Response.Write("<script language=javascript>window.alert('参数错误！');</script>");
                    this.Btn1.Visible = false;
                }
                else
                {
                    string str = (this.strMdl == "0") ? "" : " - ";
                    this.astrTitle[this.GetFileOrder(this.strMdl)] = str + this.Btn1.Text;
                    this.astrTitle[4] = "";
                    this.astrTitle[7] = "";
                    this.astrTitle[10] = "";
                    StringBuilder builder = new StringBuilder();
                    foreach (string str2 in this.astrTitle)
                    {
                        builder.Append(str2);
                    }
                    this.strCreateF = builder.ToString();
                    this.strPageTop = "<div class=\"glrihgt\"><div class=\"glrihgtnei\"><div class=\"rightmain\" style =\"font-size :12px; font-family :'Microsoft YaHei';\">当前位置：手机管理 >> <a href=\"TMCreateStaticF_Mobile.aspx?Mdl=0\">生成手机静态页</a></div><div class=\"rightmain1\" style =\"font-size :12px; font-family :'Microsoft YaHei';\">二级位置：" + this.strCreateF + "</div><div class=\"rightmain1\"> </div></div></div>";
                    base.Response.Write(this.strPageTop);
                }
            }
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
            }
            return 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page))
            {
                this.CS = new CreateStatic(base.Request.Url.AbsoluteUri, "Mobile");
                if (!base.IsPostBack)
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
}