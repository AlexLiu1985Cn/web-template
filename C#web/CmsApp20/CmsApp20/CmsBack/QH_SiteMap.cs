namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Data;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    public class QH_SiteMap : Page
    {
        private BLL Bll1 = new BLL();
        protected Button Button1;
        private XmlDocument dom;
        private DataTable dTableClmn;
        protected HtmlForm form1;
        private XmlElement root;
        private string strRootDir;
        private string strRootDir1;
        private string strSiteName;
        private string strUrlPath;
        protected TextBox TBRootDir;

        private void AppendUrl(string strSFileName)
        {
            XmlElement newChild = this.dom.CreateElement("url");
            this.root.AppendChild(newChild);
            XmlElement element2 = this.dom.CreateElement("loc");
            element2.InnerText = this.strSiteName + strSFileName;
            newChild.AppendChild(element2);
            XmlElement element3 = this.dom.CreateElement("lastmod");
            element3.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
            newChild.AppendChild(element3);
            XmlElement element4 = this.dom.CreateElement("changefreq");
            element4.InnerText = "weekly";
            newChild.AppendChild(element4);
        }

        private void AppendUrl(string strSFileName, string strPriority)
        {
            XmlElement newChild = this.dom.CreateElement("url");
            this.root.AppendChild(newChild);
            XmlElement element2 = this.dom.CreateElement("loc");
            element2.InnerText = this.strSiteName + strSFileName;
            newChild.AppendChild(element2);
            XmlElement element3 = this.dom.CreateElement("lastmod");
            element3.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
            newChild.AppendChild(element3);
            XmlElement element4 = this.dom.CreateElement("changefreq");
            element4.InnerText = "weekly";
            newChild.AppendChild(element4);
            XmlElement element5 = this.dom.CreateElement("priority");
            element5.InnerText = strPriority;
            newChild.AppendChild(element5);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.strRootDir = this.TBRootDir.Text.Trim();
            if ((this.strRootDir != "") && !Directory.Exists(base.Server.MapPath("../../" + this.strRootDir)))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "NoDir", "alert('二级目录不存在，请正确输入。');", true);
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
                string strQuery = "select id,ParentID,depth,[Module],folder,fileName,outLink,ListContent,IsShow,NumInPage from QH_Column order by CLNG(Order1) asc, CLNG(ParentID) asc,CLNG(depth) asc,id asc";
                this.dTableClmn = this.Bll1.GetDataTable(strQuery);
                bool flag = true;
                this.strSiteName = this.GetSiteName();
                this.dom = new XmlDocument();
                XmlDeclaration newChild = this.dom.CreateXmlDeclaration("1.0", "utf-8", null);
                this.dom.AppendChild(newChild);
                this.root = this.dom.CreateElement("urlset");
                this.dom.AppendChild(this.root);
                this.root.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                this.strRootDir1 = (this.strRootDir == "") ? this.strRootDir : (this.strRootDir + "/");
                this.AppendUrl(this.strRootDir1 + "index.html", "1");
                this.SetDanyeUrl();
                this.SetListModule("2", "QH_News");
                this.SetListModule("3", "QH_Product");
                this.SetListModule("4", "QH_Download");
                this.SetListModule("5", "QH_Img");
                this.SetMessage("6");
                this.SetHrDemand("8", "QH_ZhaoPin");
                this.SetNewsDetails();
                this.SetProductDetails();
                this.SetDownloadDetails();
                this.SetPictureDetails();
                string filename = base.Server.MapPath("../sitemap.xml");
                this.dom.Save(filename);
                string str3 = "sitemap.xml";
                Random random = new Random();
                if (flag)
                {
                    this.strRootDir = (this.strRootDir == "") ? this.strRootDir : ("../" + this.strRootDir + "/");
                    base.Response.Write("恭喜<a href=../" + this.strRootDir + str3 + "?" + random.Next().ToString() + " target=_blank>网站地图" + str3 + "</a>已经生成，保存在网站根目录下！");
                }
                else
                {
                    base.Response.Write("生成网站地图错误！");
                }
                base.Response.Write("<script language=javascript>HideWait();</script>");
            }
        }

        private void Get3ClassClmnData(ref string strValue1, ref string strValue2, ref string strValue3, string strField, DataRow dRow)
        {
            string str = dRow["depth"].ToString();
            if (str != null)
            {
                DataRow[] rowArray;
                if (!(str == "0"))
                {
                    if (!(str == "1"))
                    {
                        if (str == "2")
                        {
                            rowArray = this.dTableClmn.Select("id=" + dRow["ParentID"]);
                            DataRow[] rowArray2 = this.dTableClmn.Select("id=" + rowArray[0]["ParentID"]);
                            strValue1 = rowArray2[0][strField].ToString();
                            strValue2 = rowArray[0][strField].ToString();
                            strValue3 = dRow[strField].ToString();
                        }
                        return;
                    }
                }
                else
                {
                    strValue1 = dRow[strField].ToString();
                    return;
                }
                rowArray = this.dTableClmn.Select("id=" + dRow["ParentID"]);
                strValue1 = rowArray[0][strField].ToString();
                strValue2 = dRow[strField].ToString();
            }
        }

        private string GetCreatedFileName(string strfileName)
        {
            if (strfileName != "")
            {
                strfileName = (strfileName.IndexOf(".") < 0) ? (strfileName + ".html") : strfileName;
                return strfileName;
            }
            strfileName = "Index.html";
            return strfileName;
        }

        private string GetSiteName()
        {
            string absoluteUri = base.Request.Url.AbsoluteUri;
            absoluteUri = absoluteUri.Substring(absoluteUri.IndexOf(":") + 3);
            return ("http://" + absoluteUri.Substring(0, absoluteUri.IndexOf('/') + 1));
        }

        private bool GetStaticFUrlPath(out string strUrlPath, DataRow dRow, DataTable dTableClmn, string strRootDir)
        {
            strRootDir = (strRootDir == "") ? "" : (strRootDir + "/");
            bool flag = true;
            strUrlPath = "";
            string str = "";
            string str2 = "";
            string str3 = "";
            this.Get3ClassClmnData(ref str, ref str2, ref str3, "folder", dRow);
            str = strRootDir + str;
            string str4 = dRow["depth"].ToString();
            if (str4 != null)
            {
                if (!(str4 == "0"))
                {
                    if (str4 == "1")
                    {
                        strUrlPath = str + "/" + str2;
                        return flag;
                    }
                    if (str4 == "2")
                    {
                        strUrlPath = str + "/" + str2 + "/" + str3;
                    }
                    return flag;
                }
                strUrlPath = str;
            }
            return flag;
        }

        private string GetSubClassID(string strID, string strdepth, string strListContent)
        {
            StringBuilder builder;
            if (strListContent == "1")
            {
                builder = new StringBuilder("'" + strID + "'");
            }
            else
            {
                builder = new StringBuilder();
            }
            string str = strdepth;
            if (str != null)
            {
                if (!(str == "0"))
                {
                    if (str == "1")
                    {
                        foreach (DataRow row3 in this.dTableClmn.Select("ParentID='" + strID + "'"))
                        {
                            builder.Append(",'" + row3["id"] + "'");
                        }
                    }
                }
                else
                {
                    foreach (DataRow row in this.dTableClmn.Select("ParentID='" + strID + "'"))
                    {
                        builder.Append(",'" + row["id"] + "'");
                        foreach (DataRow row2 in this.dTableClmn.Select("ParentID='" + row["id"] + "'"))
                        {
                            builder.Append(",'" + row2["id"] + "'");
                        }
                    }
                }
            }
            if ((strListContent != "1") && (builder.Length > 1))
            {
                return builder.ToString().Substring(1);
            }
            return builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Commen1.JudgeLogin(this.Page);
        }

        private void SetDanyeUrl()
        {
            foreach (DataRow row in this.dTableClmn.Select("Module='1'"))
            {
                if (!(row["outLink"].ToString().Trim() != string.Empty) && !(row["IsShow"].ToString().Trim() == "0"))
                {
                    string createdFileName = this.GetCreatedFileName(row["fileName"].ToString().Trim());
                    this.GetStaticFUrlPath(out this.strUrlPath, row, this.dTableClmn, this.strRootDir);
                    this.AppendUrl(this.strUrlPath + "/" + createdFileName);
                }
            }
        }

        private void SetDownloadDetails()
        {
            foreach (string str in this.Bll1.DAL1.ReadDataReaderAL("select id from QH_Download order by id desc"))
            {
                this.AppendUrl(this.strRootDir1 + "DownloadDetails/DownloadDetails_" + str + ".html");
            }
        }

        private void SetHrDemand(string strMdl, string strTable)
        {
            foreach (DataRow row in this.dTableClmn.Select("Module='" + strMdl + "'"))
            {
                if ((row["outLink"].ToString().Trim() == string.Empty) && (row["IsShow"].ToString().Trim() != "0"))
                {
                    string createdFileName = this.GetCreatedFileName(row["fileName"].ToString().Trim());
                    this.GetStaticFUrlPath(out this.strUrlPath, row, this.dTableClmn, this.strRootDir);
                    this.AppendUrl(this.strUrlPath + "/" + createdFileName);
                    int num = Convert.ToInt32(this.Bll1.DAL1.ExecuteScalar("select count(*) from " + strTable));
                    string input = row["NumInPage"].ToString();
                    if (!Regex.IsMatch(input, @"^\d+$"))
                    {
                        input = "20";
                    }
                    int num2 = int.Parse(input);
                    int num3 = (int)Math.Ceiling((double)((num * 1.0) / ((double)num2)));
                    for (int i = 1; i < num3; i++)
                    {
                        this.AppendUrl(this.strUrlPath + "/" + createdFileName.Insert(createdFileName.IndexOf('.'), "_" + i));
                    }
                    this.AppendUrl(this.strUrlPath + "/HrDemandAccept.html");
                }
            }
        }

        private void SetListModule(string strMdl, string strTable)
        {
            foreach (DataRow row in this.dTableClmn.Select("Module='" + strMdl + "'"))
            {
                if ((row["outLink"].ToString().Trim() == string.Empty) && (row["IsShow"].ToString().Trim() != "0"))
                {
                    int num;
                    string createdFileName = this.GetCreatedFileName(row["fileName"].ToString().Trim());
                    this.GetStaticFUrlPath(out this.strUrlPath, row, this.dTableClmn, this.strRootDir);
                    this.AppendUrl(this.strUrlPath + "/" + createdFileName);
                    if (row["ListContent"].ToString() == "0")
                    {
                        num = Convert.ToInt32(this.Bll1.DAL1.ExecuteScalar("select count(*) from " + strTable + " where ColumnID='" + row["id"].ToString() + "'"));
                    }
                    else
                    {
                        string str2 = this.GetSubClassID(row["id"].ToString(), row["depth"].ToString(), row["ListContent"].ToString());
                        if (str2 == "")
                        {
                            num = 0;
                        }
                        else
                        {
                            num = Convert.ToInt32(this.Bll1.DAL1.ExecuteScalar("select count(*) from " + strTable + " where ColumnID in(" + str2 + ")  "));
                        }
                    }
                    string input = row["NumInPage"].ToString();
                    if (!Regex.IsMatch(input, @"^\d+$"))
                    {
                        input = "20";
                    }
                    int num2 = int.Parse(input);
                    int num3 = (int)Math.Ceiling((double)((num * 1.0) / ((double)num2)));
                    for (int i = 1; i < num3; i++)
                    {
                        this.AppendUrl(this.strUrlPath + "/" + createdFileName.Insert(createdFileName.IndexOf('.'), "_" + i));
                    }
                }
            }
        }

        private void SetMessage(string strMdl)
        {
            foreach (DataRow row in this.dTableClmn.Select("Module='" + strMdl + "'"))
            {
                if (!(row["outLink"].ToString().Trim() != string.Empty) && !(row["IsShow"].ToString().Trim() == "0"))
                {
                    string createdFileName = this.GetCreatedFileName(row["fileName"].ToString().Trim());
                    this.GetStaticFUrlPath(out this.strUrlPath, row, this.dTableClmn, this.strRootDir);
                    this.AppendUrl(this.strUrlPath + "/" + createdFileName);
                }
            }
        }

        private void SetNewsDetails()
        {
            foreach (string[] strArray in this.Bll1.DAL1.ReadDataReaderListStr("select id,LinkUrl from QH_News order by id desc", 2))
            {
                if (strArray[1].Trim() == "")
                {
                    this.AppendUrl(this.strRootDir1 + "NewsDetails/NewsDetails_" + strArray[0] + ".html");
                }
            }
        }

        private void SetPictureDetails()
        {
            foreach (string str in this.Bll1.DAL1.ReadDataReaderAL("select id from QH_Img order by id desc"))
            {
                this.AppendUrl(this.strRootDir1 + "PictureDetails/PictureDetails_" + str + ".html");
            }
        }

        private void SetProductDetails()
        {
            foreach (string[] strArray in this.Bll1.DAL1.ReadDataReaderListStr("select id,LinkUrl from QH_Product order by id desc", 2))
            {
                if (strArray[1].Trim() == "")
                {
                    this.AppendUrl(this.strRootDir1 + "ProductDetails/ProductDetails_" + strArray[0] + ".html");
                }
            }
        }
    }
}