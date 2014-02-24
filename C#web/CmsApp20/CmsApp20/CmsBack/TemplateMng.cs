namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using _commen;

    public class TemplateMng : Page
    {
        private string[] astrTitle = new string[] { "<a href=\"TemplateMng.aspx?Mdl=0\">首页模板</a>", "<a href=\"TemplateMng.aspx?Mdl=1\" > - 简介模块模板</a>", "<a href=\"TemplateMng.aspx?Mdl=2\" > - 新闻模块模板</a>", "<a href=\"TemplateMng.aspx?Mdl=3\" > - 产品模块模板</a>", "<a href=\"TemplateMng.aspx?Mdl=4\" > - 下载模块模板</a>", "<a href=\"TemplateMng.aspx?Mdl=5\" > - 图片模块模板</a>", "<a href=\"TemplateMng.aspx?Mdl=6\" > - 留言模块模板</a>", "<a href=\"TemplateMng.aspx?Mdl=8\" > - 招聘模块模板</a>", "<a href=\"TemplateMng.aspx?Mdl=ND\" > - 新闻内容页模板</a>", "<a href=\"TemplateMng.aspx?Mdl=PD\" > - 产品内容页模板</a>", "<a href=\"TemplateMng.aspx?Mdl=DD\" > - 下载内容页模板</a>", "<a href=\"TemplateMng.aspx?Mdl=MD\" > - 图片内容页模板</a>", "<a href=\"TemplateMng.aspx?Mdl=NS\" > - 新闻标贴搜索页模板</a>", "<a href=\"TemplateMng.aspx?Mdl=PS\" > - 产品标贴搜索页模板</a>", "<a href=\"TemplateMng.aspx?Mdl=PI\" > - 产品价格区间页模板</a>" };
        private BLL Bll1 = new BLL();
        protected HtmlGenericControl Btn;
        protected Button BtnCreateEmp;
        protected Button BtnEdit;
        protected Button BtnSaveClass;
        protected Button BtnSaveList;
        protected Button BtnSaveLM;
        private Encoding code = Encoding.UTF8;
        private DataTable dtColumn;
        protected HtmlForm form1;
        protected HiddenField HdnNumClass;
        protected HiddenField HdnNumList;
        protected HiddenField HdnNumLM;
        protected HiddenField HdnPath;
        private StreamReader sr;
        protected string strClass;
        protected string strLanmu;
        protected string strList;
        protected string strMdl;
        protected string strMdlFName;
        private string strTemp;
        protected string strTemplate;
        private StreamWriter sw;

        protected void BtnCreateEmp_Click(object sender, EventArgs e)
        {
            Encoding encoding = Encoding.UTF8;
            StreamWriter writer = null;
            bool flag = true;
            try
            {
                this.strTemp = this.SetNewTemplateContent();
                writer = new StreamWriter(base.Server.MapPath(@"..\template\" + this.HdnPath.Value), false, encoding);
                writer.Write(this.strTemp);
                writer.Flush();
            }
            catch (Exception exception)
            {
                flag = false;
                SystemError.CreateErrorLog("生成新模板文件错误： " + exception.ToString());
            }
            finally
            {
                writer.Close();
            }
            if (!flag)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "C", "<script>alert('写模板文件错误,未能生成/template/" + this.HdnPath.Value + "！');</script>");
            }
            else
            {
                this.dtColumn = this.Bll1.GetDataTable("select ColumnName,IDMark,[Module],depth from QH_Column ");
                if (this.dtColumn != null)
                {
                    this.DataToBind();
                }
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            base.Server.Transfer("ModuleTmplt.aspx?Mdl=" + base.Request.QueryString["Mdl"]);
        }

        protected void BtnSaveClass_Click(object sender, EventArgs e)
        {
            int num = int.Parse(this.HdnNumClass.Value);
            string[] astrValue = new string[num];
            for (int i = 0; i < num; i++)
            {
                astrValue[i] = base.Request.Form["IDMCL" + i];
            }
            if (this.ReadTemplatePostBack())
            {
                this.Bll1.ReadColumnID(ref this.strTemp, astrValue, "[QH:loopProductListBig ");
                bool flag = true;
                try
                {
                    this.sw = new StreamWriter(base.Server.MapPath(@"..\template\" + ((string)this.ViewState["TPath"])), false, this.code);
                    this.sw.Write(this.strTemp);
                    this.sw.Flush();
                }
                catch (Exception exception)
                {
                    flag = false;
                    SystemError.CreateErrorLog("写模板文件错误： " + exception.ToString());
                }
                finally
                {
                    if (this.sw != null)
                    {
                        this.sw.Close();
                    }
                }
                if (!flag)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "C", "<script>alert('写模板文件错误,未能修改/template/" + ((string)this.ViewState["TPath"]) + "！');</script>");
                }
                if (this.ReadTemplatePostBack())
                {
                    this.DataToBind();
                }
            }
        }

        protected void BtnSaveList_Click(object sender, EventArgs e)
        {
            int num = int.Parse(this.HdnNumList.Value);
            string[,] strArray = new string[num, 2];
            for (int i = 0; i < num; i++)
            {
                strArray[i, 0] = base.Request.Form["IDM" + i];
                strArray[i, 1] = base.Request.Form["Count" + i];
            }
            if (this.ReadTemplatePostBack())
            {
                this.Bll1.ReadListModule(ref this.strTemp, strArray);
                bool flag = true;
                try
                {
                    this.sw = new StreamWriter(base.Server.MapPath(@"..\template\" + ((string)this.ViewState["TPath"])), false, this.code);
                    this.sw.Write(this.strTemp);
                    this.sw.Flush();
                }
                catch (Exception exception)
                {
                    flag = false;
                    SystemError.CreateErrorLog("写模板文件错误： " + exception.ToString());
                }
                finally
                {
                    if (this.sw != null)
                    {
                        this.sw.Close();
                    }
                }
                if (!flag)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "C", "<script>alert('写模板文件错误,未能修改/template/" + ((string)this.ViewState["TPath"]) + "！');</script>");
                }
                if (this.ReadTemplatePostBack())
                {
                    this.DataToBind();
                }
            }
        }

        protected void BtnSaveLM_Click(object sender, EventArgs e)
        {
            int num = int.Parse(this.HdnNumLM.Value);
            string[] astrValue = new string[num];
            for (int i = 0; i < num; i++)
            {
                astrValue[i] = base.Request.Form["IDMLM" + i];
            }
            if (this.ReadTemplatePostBack())
            {
                this.Bll1.ReadColumnID(ref this.strTemp, astrValue, "[QH:loopLanmu ");
                bool flag = true;
                try
                {
                    this.sw = new StreamWriter(base.Server.MapPath(@"..\template\" + ((string)this.ViewState["TPath"])), false, this.code);
                    this.sw.Write(this.strTemp);
                    this.sw.Flush();
                }
                catch (Exception exception)
                {
                    flag = false;
                    SystemError.CreateErrorLog("写模板文件错误： " + exception.ToString());
                }
                finally
                {
                    if (this.sw != null)
                    {
                        this.sw.Close();
                    }
                }
                if (!flag)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "C", "<script>alert('写模板文件错误,未能修改/template/" + ((string)this.ViewState["TPath"]) + "！');</script>");
                }
                if (this.ReadTemplatePostBack())
                {
                    this.DataToBind();
                }
            }
        }

        private void DataToBind()
        {
            string str;
            this.strMdl = (base.Request.QueryString["Mdl"] == null) ? "" : base.Request.QueryString["Mdl"];
            this.Bll1.GetTmpltFileName(this.strMdl, out str);
            string str2 = (this.strMdl == "0") ? "" : " - ";
            this.astrTitle[this.GetFileOrder(this.strMdl)] = str2 + str + "模板";
            StringBuilder builder = new StringBuilder();
            foreach (string str3 in this.astrTitle)
            {
                builder.Append(str3);
            }
            this.strTemplate = builder.ToString();
            this.DataToBindList();
            this.DataToBindLM();
            this.DataToBindClass();
        }

        private void DataToBindClass()
        {
            ArrayList list = this.Bll1.ReadColumnID(this.strTemp, "[QH:loopProductListBig ");
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (string str2 in list)
            {
                string str;
                DataRow[] rowArray = this.dtColumn.Select("IDMark='" + str2 + "'");
                if (rowArray.Length == 0)
                {
                    str = "当前栏目设置中没有标识为" + str2 + "的栏目";
                }
                else if (rowArray[0]["depth"].ToString() != "0")
                {
                    str = string.Concat(new object[] { "当前栏目设置中标识为", str2, "的栏目是《", rowArray[0][0], "》,但不是一级栏目。（必须为一级栏目）" });
                }
                else
                {
                    str = string.Concat(new object[] { "当前栏目设置中标识为", str2, "的栏目是《", rowArray[0][0], "》" });
                }
                builder.Append(string.Concat(new object[] { "栏目标识&nbsp;<input  name =\"IDMCL", num, "\" id=\"IDMLM", num, "\" type=\"text\" class=or  maxlength=3 value='", str2, "' />&nbsp;&nbsp;", str, "<br />\n" }));
                num++;
            }
            this.strClass = (builder.Length == 0) ? "此模板没有分类列表。" : builder.ToString();
            if (builder.Length == 0)
            {
                this.BtnSaveClass.Visible = false;
            }
            this.HdnNumClass.Value = num.ToString();
        }

        private void DataToBindList()
        {
            List<string[]> list = this.Bll1.ReadListModule(this.strTemp);
            StringBuilder builder = new StringBuilder();
            int num = 0;
            string str3 = "空";
            foreach (string[] strArray2 in list)
            {
                string str;
                if (strArray2[0] == "")
                {
                    strArray2[0] = "2";
                }
                string tmpltFileName = this.Bll1.GetTmpltFileName(strArray2[0]);
                string[] strArray = strArray2[1].Split(new char[] { '|' });
                foreach (string str4 in strArray)
                {
                    if (str4.Trim() != "")
                    {
                        str3 = str4.Trim();
                        break;
                    }
                }
                DataRow[] rowArray = this.dtColumn.Select("IDMark in('" + str3 + "')");
                if (rowArray.Length == 0)
                {
                    str = "当前栏目设置中没有标识为" + str3 + "的栏目";
                }
                else if (rowArray[0]["Module"].ToString() != strArray2[0])
                {
                    str = string.Concat(new object[] { "当前栏目设置中标识在", strArray2[1], "中的第一个栏目是《", rowArray[0][0], "》,但不是", tmpltFileName });
                }
                else
                {
                    string str5 = "";
                    foreach (string str6 in strArray)
                    {
                        if (str6.Trim() != "")
                        {
                            rowArray = this.dtColumn.Select("IDMark in('" + str6.Trim() + "')");
                            if (rowArray.Length == 0)
                            {
                                str5 = str5 + "|未设置";
                            }
                            else
                            {
                                object obj2 = str5;
                                str5 = string.Concat(new object[] { obj2, "|《", rowArray[0][0], "》" });
                            }
                        }
                    }
                    str = "当前栏目设置中标识为" + strArray2[1] + "的栏目是" + str5.Substring(1) + ",为" + tmpltFileName;
                }
                builder.Append(string.Concat(new object[] { tmpltFileName, "：标识&nbsp;<input  name =\"IDM", num, "\" id=\"IDM", num, "\" type=\"text\" class=or1  maxlength=30 value='", strArray2[1], "' />&nbsp;&nbsp;显示条数&nbsp;<input  name =\"Count", num, "\" id='\"Count'", num, "\" type=\"text\" class=or  maxlength=3 value='", strArray2[2], "' />&nbsp;&nbsp;", str, "<br />\n" }));
                num++;
            }
            this.strList = (builder.Length == 0) ? "此模板没有列表模块。" : builder.ToString();
            if (builder.Length == 0)
            {
                this.BtnSaveList.Visible = false;
            }
            this.HdnNumList.Value = num.ToString();
        }

        private void DataToBindLM()
        {
            ArrayList list = this.Bll1.ReadColumnID(this.strTemp, "[QH:loopLanmu ");
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (string str2 in list)
            {
                string str;
                DataRow[] rowArray = this.dtColumn.Select("IDMark='" + str2 + "'");
                if (rowArray.Length == 0)
                {
                    str = "当前栏目设置中没有标识为" + str2 + "的栏目";
                }
                else
                {
                    str = string.Concat(new object[] { "当前栏目设置中标识为", str2, "的栏目是《", rowArray[0][0], "》" });
                }
                builder.Append(string.Concat(new object[] { "栏目标识&nbsp;<input  name =\"IDMLM", num, "\" id=\"IDMLM", num, "\" type=\"text\" class=or  maxlength=3 value='", str2, "' />&nbsp;&nbsp;", str, "<br />\n" }));
                num++;
            }
            this.strLanmu = (builder.Length == 0) ? "此模板没有栏目显示。" : builder.ToString();
            if (builder.Length == 0)
            {
                this.BtnSaveLM.Visible = false;
            }
            this.HdnNumLM.Value = num.ToString();
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

                case "NS":
                    return 12;

                case "PS":
                    return 13;

                case "PI":
                    return 14;
            }
            return 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Commen1.JudgeLogin(this.Page) && !base.IsPostBack) && this.ReadTemplate())
            {
                this.DataToBind();
            }
        }

        private bool ReadTemplate()
        {
            string str;
            this.strMdl = (base.Request.QueryString["Mdl"] == null) ? "" : base.Request.QueryString["Mdl"];
            if (this.strMdl == "")
            {
                base.Response.Write("<script language=javascript>window.alert('参数错误！');</script>");
                return false;
            }
            string tmpltFileName = this.Bll1.GetTmpltFileName(this.strMdl, out str);
            string str3 = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select TemplatePath from TemplateInfo where IsUsed=true and IsMobile=false"));
            this.ViewState["TPath"] = str3 + "/" + tmpltFileName;
            this.strMdlFName = str + "模板：" + str3 + @"\" + tmpltFileName;
            string path = base.Server.MapPath(@"..\template\" + str3);
            if (!Directory.Exists(path))
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Dir", "<script>alert('模板路径/template/" + str3 + "不存在！');history.back();</script>");
                base.Response.Write("模板路径/template/" + str3 + "不存在！");
                this.Btn.Style.Add("display", "none");
                this.BtnSaveList.Visible = false;
                this.BtnSaveLM.Visible = false;
                this.BtnSaveClass.Visible = false;
                return false;
            }
            string str5 = path + "/" + tmpltFileName;
            if (!File.Exists(str5))
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "File", "<script>var con=confirm('" + str + "模板文件/template/" + str3 + "/" + tmpltFileName + "不存在！是否生成空白模板？');if(con==true){$('HdnPath').value='" + str3 + "/" + tmpltFileName + "';$('BtnCreateEmp').click();} else history.back();</script>");
                return false;
            }
            try
            {
                this.sr = new StreamReader(str5, this.code);
                this.strTemp = this.sr.ReadToEnd();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("读自定义模版错误： " + exception.ToString());
            }
            finally
            {
                if (this.sr != null)
                {
                    this.sr.Close();
                }
            }
            if (this.strTemp == null)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "File", "<script>alert('未能读取" + str + "模板文件/template/" + str3 + "/" + tmpltFileName + "！');history.back();</script>");
                base.Response.Write("未能读取" + str + "模板文件/template/" + str3 + "/" + tmpltFileName + "！");
                this.BtnSaveList.Visible = false;
                this.BtnSaveLM.Visible = false;
                this.BtnSaveClass.Visible = false;
                return false;
            }
            this.dtColumn = this.Bll1.GetDataTable("select ColumnName,IDMark,[Module],depth from QH_Column ");
            if (this.dtColumn == null)
            {
                return false;
            }
            return true;
        }

        private bool ReadTemplatePostBack()
        {
            string str;
            this.strMdl = (base.Request.QueryString["Mdl"] == null) ? "" : base.Request.QueryString["Mdl"];
            if (this.strMdl == "")
            {
                base.Response.Write("<script language=javascript>window.alert('参数错误！');</script>");
                return false;
            }
            this.Bll1.GetTmpltFileName(this.strMdl, out str);
            this.strMdlFName = str + "模板：" + ((string)this.ViewState["TPath"]);
            try
            {
                this.sr = new StreamReader(base.Server.MapPath(@"..\template\" + ((string)this.ViewState["TPath"])), this.code);
                this.strTemp = this.sr.ReadToEnd();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("读自定义模版错误： " + exception.ToString());
            }
            finally
            {
                if (this.sr != null)
                {
                    this.sr.Close();
                }
            }
            if (this.strTemp == null)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "File", "<script>alert('未能读取模板文件/template/" + ((string)this.ViewState["TPath"]) + "！');</script>");
                base.Response.Write("未能读取模板文件/template/" + ((string)this.ViewState["TPath"]) + "！");
                this.Btn.Style.Add("display", "none");
                this.BtnSaveList.Visible = false;
                this.BtnSaveLM.Visible = false;
                this.BtnSaveClass.Visible = false;
                return false;
            }
            this.dtColumn = this.Bll1.GetDataTable("select ColumnName,IDMark,[Module],depth from QH_Column ");
            if (this.dtColumn == null)
            {
                return false;
            }
            return true;
        }

        private string SetNewTemplateContent()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
            builder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
            builder.Append("<head>\n");
            builder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\n");
            builder.Append("<title>[QH:SiteTitle]</title>\n");
            builder.Append("<meta content=\"[QH:SiteDescription]\" name=\"description\"/>\n");
            builder.Append("<meta content=\"[QH:SiteKeyword]\" name=\"keywords\" />\n");
            builder.Append("<link rel=\"icon\" type=\"image/x-icon\" href=\"[QH:Favicon]\" />\n");
            builder.Append("</head>\n<body>\n");
            builder.Append("\n\n");
            builder.Append("[QH:JSStatistic]\n[QH:CDQHLink]\n");
            builder.Append("</body>\n</html>\n");
            return builder.ToString();
        }
    }
}