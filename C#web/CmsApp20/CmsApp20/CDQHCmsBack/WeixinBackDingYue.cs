namespace CmsApp20.CDQHCmsBack
{
    using _BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class WeixinBackDingYue : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnAdd1;
        protected Button BtnAuto;
        protected Button BtnCancel;
        protected Button BtnDefault;
        protected Button BtnNews;
        protected Button BtnPicture;
        protected Button BtnProduct;
        protected Button BtnSave1;
        protected Button BtnsaveAuto;
        protected Button BtnWelcomSave;
        private DataTable dTableClmn;
        protected HtmlForm form1;
        protected HiddenField HdnAutoSave;
        protected HiddenField HdnInfo;
        protected HtmlHead Head1;
        protected string strDefaultBack = "";
        private string strDomainURL = "";
        protected string strEventWelcom = "";
        protected string strJsHide = "";
        private string strNewsNum;
        private string strPictureNum;
        private string strPicUrl;
        private string strProductNum;
        private string strTextTr = "<tr bgcolor=\"#{1}\" onmouseover=\"this.style.backgroundColor='#9ec0fe'\" onmouseout=\"this.style.backgroundColor=''\">\n     <td style =\"background :White ; text-align:right;\"></td>\n     <td  align=center ><input  name =\"Eff{0}\" id=\"Eff{0}\" type=\"checkbox\" {Eff} /></td>\n   <td><input  name =\"key{0}\" id=\"key{0}\" type=\"text\"  class =\"td60\" value=\"{key}\"  /></td>\n   <td colspan =4><textarea name=\"Text{0}\" id=\"Text{0}\" cols=\"70\" rows=\"3\" >{text}</textarea></td>\n   <td><a href =javascript:SaveText({0},'{2}')>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:Del({2})>删除</a></td>\n</tr>\n";
        private string strTextTrNew = "<tr bgcolor=\"#f9f9f9\" onmouseover=\"this.style.backgroundColor='#9ec0fe'\" onmouseout=\"this.style.backgroundColor=''\">\n     <td style =\"background :White ; text-align:right;\"></td>     <td  align=center ><input  name =\"EffN\" id=\"EffN\" type=\"checkbox\" checked /></td>\n   <td><input  name =\"keyN\" id=\"keyN\" type=\"text\"  class =\"td60\" /></td>\n   <td colspan =4><textarea name=\"TextN\" id=\"TextN\" cols=\"70\" rows=\"3\" ></textarea></td>\n   <td><a href =javascript:SaveTextNew()>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:Cancel()>取消</a></td>\n</tr>\n";
        protected string strTr = "";
        private string strTuWenTr = "<form action=\"\" enctype =\"multipart/form-data\" method =post target=UpFrame id=FormUp{0} >\n  <tr bgcolor=\"#{1}\" onmouseover=\"this.style.backgroundColor='#9ec0fe'\" onmouseout=\"this.style.backgroundColor=''\">\n     <td style =\"background :White ; text-align:right;\"></td>\n     <td  align=center ><input  name =\"Eff{0}\" id=\"Eff{0}\" type=\"checkbox\" {Eff} /></td>\n     <td><input  name =\"key{0}\" id=\"key{0}\" type=\"text\"  class =\"td60\" value=\"{key}\" /></td>\n     <td><input  name =\"Title{0}\" id=\"Title{0}\" type=\"text\" class =\"td80\" value=\"{Title}\" /></td>\n     <td><input  name =\"Descrpt{0}\" id=\"Descrpt{0}\" type=\"text\" class =\"td100\" value=\"{Descrpt}\" /></td>\n\t\t<td><input  name =\"PicUrl{0}\" id=\"PicUrl{0}\" type=\"text\" class =\"td240\" value=\"{PicUrl}\" />\n\t\t<input  type=\"file\" id=\"File{0}\" name=\"File{0}\" onchange=CheckUp({0}) class=\"NB\" size=1 /><a href=\"#\">浏览上传</a>\n\t\t<img src=\"images/ld.gif\" class=\"Wt\" style=\"display:none;\" id=UpWt{0} /></td>\n\t\t<td><input  name =\"Url{0}\" id=\"Url{0}\" type=\"text\" class =\"td220\" value=\"{Url}\" /></td>\n     <td><a href =javascript:SaveTuWen({0},'{2}')>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:Del({2})>删除</a></td>\n\t </tr>\n</form>\n";
        private string strTuWenTrAuto = "<form action=\"\" enctype =\"multipart/form-data\" method =post target=UpFrame id=FormUp{0} >\n  <tr bgcolor=\"#{1}\" onmouseover=\"this.style.backgroundColor='#9ec0fe'\" onmouseout=\"this.style.backgroundColor=''\" {BranchID} >\n     <td style =\"background :White ; text-align:right;\">{Branch}</td>     <td  align=center ><input  name =\"Eff{0}\" id=\"Eff{0}\" type=\"checkbox\" checked /></td>\n     <td><input  name =\"key{0}\" id=\"key{0}\" type=\"text\"  class =\"td60\" value=\"{key}\" /><input id=\"HdnCID{0}\" type=\"hidden\" value=\"{CID}\" /></td>\n     <td><input  name =\"Title{0}\" id=\"Title{0}\" type=\"text\" class =\"td80\" value=\"{Title}\" /></td>\n     <td><input  name =\"Descrpt{0}\" id=\"Descrpt{0}\" type=\"text\" class =\"td100\" value=\"{Descrpt}\" /></td>\n\t\t<td><input  name =\"PicUrl{0}\" id=\"PicUrl{0}\" type=\"text\" class =\"td240\" value=\"{PicUrl}\" />\n\t\t<input  type=\"file\" id=\"File{0}\" name=\"File{0}\" onchange=CheckUp({0}) class=\"NB\" size=1 /><a href=\"#\">浏览上传</a>\n\t\t<img src=\"images/ld.gif\" class=\"Wt\" style=\"display:none;\" id=UpWt{0} /></td>\n\t\t<td><input  name =\"Url{0}\" id=\"Url{0}\" type=\"text\" class =\"td220\" value=\"{Url}\" /></td>\n     <td></td>\n\t </tr>\n</form>\n";
        private string strTuWenTrNew = "<form action=\"\" enctype =\"multipart/form-data\" method =post target=UpFrame id=FormUpN >\n  <tr bgcolor=\"#f9f9f9\" onmouseover=\"this.style.backgroundColor='#9ec0fe'\" onmouseout=\"this.style.backgroundColor=''\">\n     <td style =\"background :White ; text-align:right;\"></td>     <td  align=center ><input  name =\"EffN\" id=\"EffN\" type=\"checkbox\" checked /></td>\n     <td><input  name =\"keyN\" id=\"keyN\" type=\"text\"  class =\"td60\" value=\"\" /></td>\n     <td><input  name =\"TitleN\" id=\"TitleN\" type=\"text\" class =\"td80\" value=\"\" /></td>\n     <td><input  name =\"DescrptN\" id=\"DescrptN\" type=\"text\" class =\"td100\" value=\"\" /></td>\n\t\t<td><input  name =\"PicUrlN\" id=\"PicUrlN\" type=\"text\" class =\"td240\" value=\"\" />\n\t\t<input  type=\"file\" id=\"FileN\" name=\"FileN\" onchange=CheckUp('N') class=\"NB\" size=1 /><a href=\"#\">浏览上传</a>\n\t\t<img src=\"images/ld.gif\" class=\"Wt\" style=\"display:none;\" id=UpWtN /></td>\n\t\t<td><input  name =\"UrlN\" id=\"UrlN\" type=\"text\" class =\"td220\" value=\"\" /></td>\n     <td><a href =javascript:SaveTuWenNew()>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:Cancel()>取消</a></td>\n\t </tr>\n</form>\n";

        private void AddClmnOderInt(DataTable dTableClmn)
        {
            DataColumn column = new DataColumn("Order")
            {
                DataType = Type.GetType("System.Int32"),
                Expression = "Convert(Order1, 'System.Int32')"
            };
            dTableClmn.Columns.Add(column);
        }

        private int AutoGenerateInfo()
        {
            DataTable dataTable = this.Bll1.GetDataTable("select id,ColumnID,title,ImageUrl ,BriefIntro from QH_News where WeixinShow=true ");
            DataTable table2 = this.Bll1.GetDataTable("select id,ColumnID,title,ThumbUrl,Memo1 from QH_Product where WeixinShow=true");
            DataTable table3 = this.Bll1.GetDataTable("select id,ColumnID,title,ImageUrl ,BriefIntro from QH_Img where WeixinShow=true ");
            this.dTableClmn = this.Bll1.GetDataTable("select id,ColumnName,ParentID,depth,order1,[Module],folder,fileName,outLink,IsShow,BriefIntro,NavMobile from QH_Column order by CLNG(Order1) asc, CLNG(ParentID) asc,CLNG(depth) asc,id asc");
            if (this.dTableClmn.Rows.Count > 0)
            {
                this.AddClmnOderInt(this.dTableClmn);
            }
            DataRow[] rowArray2 = null;
            DataRow[] rowArray = this.dTableClmn.Select(" depth='0' and (NavMobile='3' or NavMobile='1')", "order asc,id asc");
            this.strDomainURL = this.Bll1.GetDomainUrl(1) + "m/";
            int num = 0;
            int num2 = int.Parse(this.strNewsNum);
            int num3 = int.Parse(this.strProductNum);
            int num4 = int.Parse(this.strPictureNum);
            int num5 = 0;
            int nBranch = 0;
            string str6 = "";
            this.strEventWelcom = Regex.Replace(this.strEventWelcom, @"\n?\s*回复[\s\S]+?(?:(\n|$))", "");
            this.strDefaultBack = Regex.Replace(this.strDefaultBack, @"\n?\s*回复[\s\S]+?(?:(\n|$))", "");
            for (int i = 0; i < rowArray.Length; i++)
            {
                object strEventWelcom = this.strEventWelcom;
                this.strEventWelcom = string.Concat(new object[] { strEventWelcom, "\n 回复 ", i + 1, " ", rowArray[i]["ColumnName"] });
                object strDefaultBack = this.strDefaultBack;
                this.strDefaultBack = string.Concat(new object[] { strDefaultBack, "\n 回复 ", i + 1, " ", rowArray[i]["ColumnName"] });
                string strMdl = rowArray[i]["Module"].ToString();
                string strID = rowArray[i]["id"].ToString();
                string strColumnIDIn = "";
                nBranch = 0;
                if (((strMdl == "2") || (strMdl == "3")) || (strMdl == "5"))
                {
                    strColumnIDIn = "'" + strID + "'";
                    this.GetIDMarkSubIN(ref strColumnIDIn, strID, strMdl, this.dTableClmn);
                    if (strMdl == "2")
                    {
                        rowArray2 = dataTable.Select("ColumnID in(" + strColumnIDIn + ")", "id desc");
                        str6 = "NewsDetails/NewsDetails_";
                        num5 = num2;
                    }
                    else if (strMdl == "5")
                    {
                        rowArray2 = table3.Select("ColumnID in(" + strColumnIDIn + ")", "id desc");
                        str6 = "PictureDetails/PictureDetails_";
                        num5 = num4;
                    }
                    else
                    {
                        rowArray2 = table2.Select("ColumnID in(" + strColumnIDIn + ")", "id desc");
                        str6 = "ProductDetails/ProductDetails_";
                        num5 = num3;
                    }
                    nBranch = Math.Min(rowArray2.Length, num5);
                }
                string strFolder = "";
                string str = rowArray[i]["outLink"].ToString().Trim();
                if (str != "")
                {
                    strFolder = str.Contains("http://") ? str : this.PageUrlSet(str);
                }
                else
                {
                    strFolder = this.GetIsShowLink(rowArray[i]["folder"].ToString(), rowArray[i]);
                }
                this.AutoGenerateTuWen(i + 1, "Auto", rowArray[i]["ColumnName"].ToString(), rowArray[i]["BriefIntro"].ToString(), strFolder, ++num, nBranch, 0, this.strPicUrl, "$" + strID);
                switch (strMdl)
                {
                    case "2":
                    case "3":
                    case "5":
                        for (int j = 0; (j < rowArray2.Length) && (j < num5); j++)
                        {
                            string strTitle = rowArray2[j]["title"].ToString();
                            string strDescrpt = (strMdl != "3") ? rowArray2[j]["BriefIntro"].ToString() : rowArray2[j]["Memo1"].ToString();
                            string str7 = (strMdl != "3") ? rowArray2[j]["ImageUrl"].ToString() : rowArray2[j]["ThumbUrl"].ToString();
                            str7 = (str7 == "") ? this.strPicUrl : str7.Replace("../", this.strDomainURL.Replace("/m", ""));
                            this.AutoGenerateTuWen(i + 1, "Auto", strTitle, strDescrpt, str6 + rowArray2[j]["id"] + ".html", ++num, nBranch, j + 1, str7, strID);
                        }
                        break;
                }
            }
            this.ViewState["AutoNum"] = this.HdnAutoSave.Value = num.ToString();
            this.strJsHide = "<script type =\"text/javascript\" >$('Add').style.display='none';</script>";
            return num;
        }

        private void AutoGenerateTuWen(int nKey, string strId, string strTitle, string strDescrpt, string strFolder, int nNum, int nBranch, int nBranchSubNum, string strPicUrl1, string strCID)
        {
            string newValue = "";
            string str2 = "";
            if ((nBranch != 0) && (nBranchSubNum != 0))
            {
                newValue = string.Concat(new object[] { "id=\"tr", nKey, "_", nBranchSubNum, "\" style=\"display:none\" " });
            }
            if ((nBranch != 0) && (nBranchSubNum == 0))
            {
                str2 = string.Concat(new object[] { "<a href =javascript:Open(", nKey, ",", nBranch, ")><img id=\"", nKey, "\" src=\"images/jia.jpg\" /></a>" });
            }
            this.strTr = this.strTr + this.strTuWenTrAuto.Replace("{0}", nNum.ToString()).Replace("{1}", ((nNum % 2) == 1) ? "e6e6e6" : "f9f9f9").Replace("{2}", strId).Replace("{key}", nKey.ToString()).Replace("{Title}", strTitle).Replace("{Descrpt}", strDescrpt).Replace("{PicUrl}", strPicUrl1).Replace("{Url}", strFolder.Contains("http://") ? strFolder : (this.strDomainURL + strFolder)).Replace("{BranchID}", newValue).Replace("{Branch}", str2).Replace("{CID}", strCID);
        }

        protected void BtnAdd1_Click(object sender, EventArgs e)
        {
            this.DataToBind();
            if (this.HdnInfo.Value == "TW")
            {
                this.strTr = this.strTr + this.strTuWenTrNew;
                this.strJsHide = "<script type =\"text/javascript\" >$('Add').style.display='none';</script>";
            }
            if (this.HdnInfo.Value == "Text")
            {
                this.strTr = this.strTr + this.strTextTrNew;
                this.strJsHide = "<script type =\"text/javascript\" >$('Add').style.display='none';</script>";
            }
        }

        protected void BtnAuto_Click(object sender, EventArgs e)
        {
            this.strTr = "";
            this.DataToBindAuto();
            if (this.AutoGenerateInfo() == 0)
            {
                this.DataToBind();
                base.ClientScript.RegisterStartupScript(base.GetType(), "EmptyClmn", "alert('手机栏目为空，请在《手机管理》－《栏目设置》中设置手机栏目。');", true);
            }
            else
            {
                string str;
                this.BtnCancel.Style["display"] = "";
                this.BtnsaveAuto.Style["display"] = "";
                this.BtnWelcomSave.Style["display"] = "none";
                this.BtnDefault.Style["display"] = "none";
                this.BtnPicture.Style["display"] = str = "none";
                this.BtnNews.Style["display"] = this.BtnProduct.Style["display"] = str;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            this.strTr = "";
            this.BtnCancel.Style["display"] = "none";
            this.BtnsaveAuto.Style["display"] = "none";
            this.BtnWelcomSave.Style["display"] = "";
            this.BtnDefault.Style["display"] = "";
            this.DataToBind();
        }

        protected void BtnDefault_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (((string)this.ViewState["DefaultBack"]) == "1")
            {
                num = this.Bll1.DAL1.ExecuteNonQuery("Update QH_WeixinAuto Set [Text]=@Text where AutoType='2'", new OleDbParameter[] { new OleDbParameter("@Text", base.Request.Form["DefaultBack"]) });
            }
            else
            {
                num = this.Bll1.DAL1.ExecuteNonQuery("insert into QH_WeixinAuto(AutoType,Type,[Text]) values('2','0',@Text)", new OleDbParameter[] { new OleDbParameter("@Text", base.Request.Form["DefaultBack"]) });
            }
            if (num == 1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "OK", "alert('保存成功');", true);
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "Fail", "alert('保存失败');", true);
            }
            this.DataToBind();
        }

        protected void BtnNews_Click(object sender, EventArgs e)
        {
            List<string[]> list = this.Bll1.DAL1.ReadDataReaderListStr("select [key],ColumnID from QH_WeixinAuto where AutoType='0' and Type='1' order by [key] asc, id asc", 2);
            if (list.Count == 0)
            {
                this.DataToBind();
            }
            else
            {
                DataTable dataTable = this.Bll1.GetDataTable("select id,ColumnID,title,ImageUrl,BriefIntro from QH_News where WeixinShow=true ");
                this.dTableClmn = this.Bll1.GetDataTable("select id,ParentID,depth,[Module],folder,NavMobile from QH_Column order by CLNG(Order1) asc, CLNG(ParentID) asc,CLNG(depth) asc,id asc");
                this.DataToBindAuto();
                int num = int.Parse(this.strNewsNum);
                this.strDomainURL = this.Bll1.GetDomainUrl(1) + "m/";
                List<string[]> listData = new List<string[]>();
                string str = "";
                DataRow[] rowArray2 = null;
                foreach (string[] strArray in list)
                {
                    if (strArray[1].Contains("$"))
                    {
                        DataRow[] rowArray = this.dTableClmn.Select(" id=" + strArray[1].TrimStart(new char[] { '$' }) + " and [Module]='2' and depth='0'");
                        if (rowArray.Length == 1)
                        {
                            string strID = rowArray[0]["id"].ToString();
                            str = str + "'" + strID + "',";
                            string strColumnIDIn = "'" + strID + "'";
                            this.GetIDMarkSubIN(ref strColumnIDIn, strID, "2", this.dTableClmn);
                            rowArray2 = dataTable.Select("ColumnID in(" + strColumnIDIn + ")", "id desc");
                            for (int i = 0; (i < rowArray2.Length) && (i < num); i++)
                            {
                                string str4 = rowArray2[i]["title"].ToString();
                                string str5 = rowArray2[i]["BriefIntro"].ToString();
                                string str6 = rowArray2[i]["ImageUrl"].ToString();
                                str6 = (str6 == "") ? this.strPicUrl : str6.Replace("../", this.strDomainURL.Replace("/m", ""));
                                listData.Add(new string[] { "0", "1", strArray[0], str4, str5, str6, string.Concat(new object[] { this.strDomainURL, "NewsDetails/NewsDetails_", rowArray2[i]["id"], ".html" }), "", strID });
                            }
                        }
                    }
                }
                if (str.TrimEnd(new char[] { ',' }) != "")
                {
                    this.Bll1.DAL1.ExecuteNonQuery("delete from QH_WeixinAuto where ColumnID in(" + str + ") ");
                }
                if (listData.Count > 0)
                {
                    string strTableSchema = "select top 1 * from QH_WeixinAuto where 1<>1";
                    string strInsert = "insert into QH_WeixinAuto(AutoType,Type,[key],Title,Descrpt,PicUrl,Url,[Text],ColumnID) values(@AutoType,@Type,@key,@Title,@Descrpt,@PicUrl,@Url,@Text,@ColumnID)";
                    string[] astrFieldAll = new string[] { "AutoType", "Type", "key", "Title", "Descrpt", "PicUrl", "Url", "Text", "ColumnID" };
                    string[] strCommen = new string[0];
                    string[] strCommenType = new string[0];
                    string[] strCmnValue = new string[0];
                    string[] astrField = new string[] { "AutoType", "Type", "key", "Title", "Descrpt", "PicUrl", "Url", "Text", "ColumnID" };
                    string[] astrFieldType = new string[] { "text", "text", "text", "text", "text", "text", "text", "memo", "text" };
                    string[] astrFieldAllType = new string[] { "text", "text", "text", "text", "text", "text", "text", "memo", "text" };
                    if (this.Bll1.DAL1.BatchInsert(strTableSchema, strInsert, strCommen, strCommenType, strCmnValue, astrField, astrFieldType, listData, astrFieldAll, astrFieldAllType))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), "OK", "alert('更新成功');", true);
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), "Fail", "alert('更新失败');", true);
                    }
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "No", "alert('当前设置中没有一级新闻栏目或新闻中没有设置为微信显示的新闻。');", true);
                }
                this.DataToBind();
            }
        }

        protected void BtnPicture_Click(object sender, EventArgs e)
        {
            List<string[]> list = this.Bll1.DAL1.ReadDataReaderListStr("select [key],ColumnID from QH_WeixinAuto where AutoType='0' and Type='1' order by [key] asc, id asc", 2);
            if (list.Count == 0)
            {
                this.DataToBind();
            }
            else
            {
                DataTable dataTable = this.Bll1.GetDataTable("select id,ColumnID,title,ImageUrl ,BriefIntro from QH_Img where WeixinShow=true ");
                this.dTableClmn = this.Bll1.GetDataTable("select id,ParentID,depth,[Module],folder,NavMobile from QH_Column order by CLNG(Order1) asc, CLNG(ParentID) asc,CLNG(depth) asc,id asc");
                this.DataToBindAuto();
                int num = int.Parse(this.strPictureNum);
                this.strDomainURL = this.Bll1.GetDomainUrl(1) + "m/";
                List<string[]> listData = new List<string[]>();
                string str = "";
                DataRow[] rowArray2 = null;
                foreach (string[] strArray in list)
                {
                    if (strArray[1].Contains("$"))
                    {
                        DataRow[] rowArray = this.dTableClmn.Select(" id=" + strArray[1].TrimStart(new char[] { '$' }) + " and [Module]='5' and depth='0'");
                        if (rowArray.Length == 1)
                        {
                            string strID = rowArray[0]["id"].ToString();
                            str = str + "'" + strID + "',";
                            string strColumnIDIn = "'" + strID + "'";
                            this.GetIDMarkSubIN(ref strColumnIDIn, strID, "2", this.dTableClmn);
                            rowArray2 = dataTable.Select("ColumnID in(" + strColumnIDIn + ")", "id desc");
                            for (int i = 0; (i < rowArray2.Length) && (i < num); i++)
                            {
                                string str4 = rowArray2[i]["title"].ToString();
                                string str5 = rowArray2[i]["BriefIntro"].ToString();
                                string str6 = rowArray2[i]["ImageUrl"].ToString();
                                str6 = (str6 == "") ? this.strPicUrl : str6.Replace("../", this.strDomainURL.Replace("/m", ""));
                                listData.Add(new string[] { "0", "1", strArray[0], str4, str5, str6, string.Concat(new object[] { this.strDomainURL, "PictureDetails/PictureDetails_", rowArray2[i]["id"], ".html" }), "", strID });
                            }
                        }
                    }
                }
                if (str.TrimEnd(new char[] { ',' }) != "")
                {
                    this.Bll1.DAL1.ExecuteNonQuery("delete from QH_WeixinAuto where ColumnID in(" + str + ") ");
                }
                if (listData.Count > 0)
                {
                    string strTableSchema = "select top 1 * from QH_WeixinAuto where 1<>1";
                    string strInsert = "insert into QH_WeixinAuto(AutoType,Type,[key],Title,Descrpt,PicUrl,Url,[Text],ColumnID) values(@AutoType,@Type,@key,@Title,@Descrpt,@PicUrl,@Url,@Text,@ColumnID)";
                    string[] astrFieldAll = new string[] { "AutoType", "Type", "key", "Title", "Descrpt", "PicUrl", "Url", "Text", "ColumnID" };
                    string[] strCommen = new string[0];
                    string[] strCommenType = new string[0];
                    string[] strCmnValue = new string[0];
                    string[] astrField = new string[] { "AutoType", "Type", "key", "Title", "Descrpt", "PicUrl", "Url", "Text", "ColumnID" };
                    string[] astrFieldType = new string[] { "text", "text", "text", "text", "text", "text", "text", "memo", "text" };
                    string[] astrFieldAllType = new string[] { "text", "text", "text", "text", "text", "text", "text", "memo", "text" };
                    if (this.Bll1.DAL1.BatchInsert(strTableSchema, strInsert, strCommen, strCommenType, strCmnValue, astrField, astrFieldType, listData, astrFieldAll, astrFieldAllType))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), "OK", "alert('更新成功');", true);
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), "Fail", "alert('更新失败');", true);
                    }
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "No", "alert('当前设置中没有一级图片栏目或图片中没有设置为微信显示的图片。');", true);
                }
                this.DataToBind();
            }
        }

        protected void BtnProduct_Click(object sender, EventArgs e)
        {
            List<string[]> list = this.Bll1.DAL1.ReadDataReaderListStr("select [key],ColumnID from QH_WeixinAuto where AutoType='0' and Type='1' order by [key] asc, id asc", 2);
            if (list.Count == 0)
            {
                this.DataToBind();
            }
            else
            {
                DataTable dataTable = this.Bll1.GetDataTable("select id,ColumnID,title,ThumbUrl,Memo1 from QH_Product where WeixinShow=true");
                this.dTableClmn = this.Bll1.GetDataTable("select id,ParentID,depth,[Module],folder,NavMobile from QH_Column order by CLNG(Order1) asc, CLNG(ParentID) asc,CLNG(depth) asc,id asc");
                this.DataToBindAuto();
                int num = int.Parse(this.strProductNum);
                this.strDomainURL = this.Bll1.GetDomainUrl(1) + "m/";
                List<string[]> listData = new List<string[]>();
                string str = "";
                DataRow[] rowArray2 = null;
                foreach (string[] strArray in list)
                {
                    if (strArray[1].Contains("$"))
                    {
                        DataRow[] rowArray = this.dTableClmn.Select(" id=" + strArray[1].TrimStart(new char[] { '$' }) + " and [Module]='3' and depth='0'");
                        if (rowArray.Length == 1)
                        {
                            string strID = rowArray[0]["id"].ToString();
                            str = str + "'" + strID + "',";
                            string strColumnIDIn = "'" + strID + "'";
                            this.GetIDMarkSubIN(ref strColumnIDIn, strID, "3", this.dTableClmn);
                            rowArray2 = dataTable.Select("ColumnID in(" + strColumnIDIn + ")", "id desc");
                            for (int i = 0; (i < rowArray2.Length) && (i < num); i++)
                            {
                                string str4 = rowArray2[i]["title"].ToString();
                                string str5 = rowArray2[i]["Memo1"].ToString();
                                string str6 = rowArray2[i]["ThumbUrl"].ToString();
                                str6 = (str6 == "") ? this.strPicUrl : str6.Replace("../", this.strDomainURL.Replace("/m", ""));
                                listData.Add(new string[] { "0", "1", strArray[0], str4, str5, str6, string.Concat(new object[] { this.strDomainURL, "ProductDetails/ProductDetails_", rowArray2[i]["id"], ".html" }), "", strID });
                            }
                        }
                    }
                }
                if (str.TrimEnd(new char[] { ',' }) != "")
                {
                    this.Bll1.DAL1.ExecuteNonQuery("delete from QH_WeixinAuto where ColumnID in(" + str + ") ");
                }
                if (listData.Count > 0)
                {
                    string strTableSchema = "select top 1 * from QH_WeixinAuto where 1<>1";
                    string strInsert = "insert into QH_WeixinAuto(AutoType,Type,[key],Title,Descrpt,PicUrl,Url,[Text],ColumnID) values(@AutoType,@Type,@key,@Title,@Descrpt,@PicUrl,@Url,@Text,@ColumnID)";
                    string[] astrFieldAll = new string[] { "AutoType", "Type", "key", "Title", "Descrpt", "PicUrl", "Url", "Text", "ColumnID" };
                    string[] strCommen = new string[0];
                    string[] strCommenType = new string[0];
                    string[] strCmnValue = new string[0];
                    string[] astrField = new string[] { "AutoType", "Type", "key", "Title", "Descrpt", "PicUrl", "Url", "Text", "ColumnID" };
                    string[] astrFieldType = new string[] { "text", "text", "text", "text", "text", "text", "text", "memo", "text" };
                    string[] astrFieldAllType = new string[] { "text", "text", "text", "text", "text", "text", "text", "memo", "text" };
                    if (this.Bll1.DAL1.BatchInsert(strTableSchema, strInsert, strCommen, strCommenType, strCmnValue, astrField, astrFieldType, listData, astrFieldAll, astrFieldAllType))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), "OK", "alert('更新成功');", true);
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), "Fail", "alert('更新失败');", true);
                    }
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "No", "alert('当前设置中没有一级产品栏目或产品中没有设置为微信显示的产品。');", true);
                }
                this.DataToBind();
            }
        }

        protected void BtnSave1_Click(object sender, EventArgs e)
        {
            int num = 0;
            string[] strArray = this.HdnInfo.Value.Split(new char[] { '|' });
            if ((strArray[0] == "TW") || (strArray[0] == "TWNew"))
            {
                string str = (strArray[0] == "TWNew") ? strArray[1] : strArray[2];
                if (Convert.ToInt32(this.Bll1.DAL1.ExecuteScalar("select count(*) from QH_WeixinAuto where AutoType='0' and Type='0' and [key]='" + str + "'")) >= 1)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "full", "alert('关键字（" + str + @"）已存在！\n图文消息和文本消息不能同时使用一个关键词。');", true);
                    this.DataToBind();
                    return;
                }
                if (Convert.ToInt32(this.Bll1.DAL1.ExecuteScalar("select count(*) from QH_WeixinAuto where AutoType='0' and Type='1' and [key]='" + str + "'")) >= 10)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "full", "alert('关键字（" + str + "）已超过10条！同一图文关键字最多10条。');", true);
                    this.DataToBind();
                    return;
                }
            }
            if (strArray[0] == "TextNew")
            {
                string str2 = (strArray[0] == "TextNew") ? strArray[1] : strArray[2];
                if (Convert.ToInt32(this.Bll1.DAL1.ExecuteScalar("select count(*) from QH_WeixinAuto where [key]='" + str2 + "'")) >= 1)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "full", "alert('关键字（" + str2 + @"）已存在！\n图文消息和文本消息不能同时使用一个关键词。');", true);
                    this.DataToBind();
                    return;
                }
            }
            if (strArray[0] == "Text")
            {
                string str3 = (strArray[0] == "TextNew") ? strArray[1] : strArray[2];
                if (Convert.ToInt32(this.Bll1.DAL1.ExecuteScalar("select count(*) from QH_WeixinAuto where [key]='" + str3 + "' and id<>" + strArray[1])) >= 1)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "full", "alert('关键字（" + str3 + @"）已存在！\n图文消息和文本消息不能同时使用一个关键词。');", true);
                    this.DataToBind();
                    return;
                }
            }
            if (strArray[0] == "TW")
            {
                num = this.Bll1.DAL1.ExecuteNonQuery("Update QH_WeixinAuto Set [key]=@key ,Title=@Title ,Descrpt=@Descrpt ,PicUrl=@PicUrl ,Url=@Url,Effect=@Effect where id=" + strArray[1], new OleDbParameter[] { new OleDbParameter("@key", strArray[2]), new OleDbParameter("@Title", strArray[3]), new OleDbParameter("@Descrpt", strArray[4]), new OleDbParameter("@PicUrl", strArray[5]), new OleDbParameter("@Url", strArray[6]), new OleDbParameter("@Effect", strArray[7]) });
            }
            else if (strArray[0] == "TWNew")
            {
                num = this.Bll1.DAL1.ExecuteNonQuery("insert into QH_WeixinAuto(AutoType,Type,[key],Title,Descrpt,PicUrl,Url,Effect) values('0','1',@key,@Title,@Descrpt,@PicUrl,@Url,@Effect)", new OleDbParameter[] { new OleDbParameter("@key", strArray[1]), new OleDbParameter("@Title", strArray[2]), new OleDbParameter("@Descrpt", strArray[3]), new OleDbParameter("@PicUrl", strArray[4]), new OleDbParameter("@Url", strArray[5]), new OleDbParameter("@Effect", strArray[6]) });
            }
            else if (strArray[0] == "Text")
            {
                num = this.Bll1.DAL1.ExecuteNonQuery("Update QH_WeixinAuto Set [key]=@key ,[Text]=@Text ,Effect=@Effect where id=" + strArray[1], new OleDbParameter[] { new OleDbParameter("@key", strArray[2]), new OleDbParameter("@Text", strArray[3]), new OleDbParameter("@Effect", strArray[4]) });
            }
            else if (strArray[0] == "TextNew")
            {
                num = this.Bll1.DAL1.ExecuteNonQuery("insert into QH_WeixinAuto(AutoType,Type,[key],[Text],Effect) values('0','0',@key,@Text,@Effect)", new OleDbParameter[] { new OleDbParameter("@key", strArray[1]), new OleDbParameter("@Text", strArray[2]), new OleDbParameter("@Effect", strArray[3]) });
            }
            if (strArray[0] == "Del")
            {
                if (this.Bll1.DAL1.ExecuteNonQuery("delete from QH_WeixinAuto where id=@id", new OleDbParameter[] { new OleDbParameter("@id", strArray[1]) }) == 1)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "OK", "alert('删除成功');", true);
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "Fail", "alert('删除失败');", true);
                }
                this.DataToBind();
            }
            else
            {
                if (num == 1)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "OK", "alert('保存成功');", true);
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "Fail", "alert('保存失败');", true);
                }
                this.DataToBind();
            }
        }

        protected void BtnsaveAuto_Click(object sender, EventArgs e)
        {
            string str = (string)this.ViewState["AutoNum"];
            if (string.IsNullOrEmpty(str) || (str == "0"))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "Empty", "alert('手机栏目为空');", true);
            }
            else
            {
                this.Bll1.DAL1.ExecuteNonQuery("delete from QH_WeixinAuto where AutoType in('0','1','2') ");
                string strTableSchema = "select top 1 * from QH_WeixinAuto where 1<>1";
                string strInsert = "insert into QH_WeixinAuto(AutoType,Type,[key],Title,Descrpt,PicUrl,Url,[Text],ColumnID) values(@AutoType,@Type,@key,@Title,@Descrpt,@PicUrl,@Url,@Text,@ColumnID)";
                string[] astrFieldAll = new string[] { "AutoType", "Type", "key", "Title", "Descrpt", "PicUrl", "Url", "Text", "ColumnID" };
                string[] strCommen = new string[0];
                string[] strCommenType = new string[0];
                string[] strCmnValue = new string[0];
                string[] astrField = new string[] { "AutoType", "Type", "key", "Title", "Descrpt", "PicUrl", "Url", "Text", "ColumnID" };
                string[] astrFieldType = new string[] { "text", "text", "text", "text", "text", "text", "text", "memo", "text" };
                string[] astrFieldAllType = new string[] { "text", "text", "text", "text", "text", "text", "text", "memo", "text" };
                List<string[]> listData = new List<string[]> {
                    new string[] { "1", "", "", "", "", "", "", base.Request.Form["eventWelcom"], "" },
                    new string[] { "2", "", "", "", "", "", "", base.Request.Form["DefaultBack"], "" }
                };
                string[] strArray8 = this.HdnAutoSave.Value.Split(new char[] { '|' });
                int index = 0;
                for (int i = 0; i < (strArray8.Length / 6); i++)
                {
                    index = i * 6;
                    listData.Add(new string[] { "0", "1", strArray8[index], strArray8[index + 1], strArray8[index + 2], strArray8[index + 3], strArray8[index + 4], "", strArray8[index + 5] });
                }
                if (this.Bll1.DAL1.BatchInsert(strTableSchema, strInsert, strCommen, strCommenType, strCmnValue, astrField, astrFieldType, listData, astrFieldAll, astrFieldAllType))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "OK", "alert('保存成功');", true);
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "Fail", "alert('保存失败');", true);
                }
                this.BtnAuto.Style["display"] = "";
                this.BtnCancel.Style["display"] = "none";
                this.BtnsaveAuto.Style["display"] = "none";
                this.BtnWelcomSave.Style["display"] = "";
                this.BtnDefault.Style["display"] = "";
                this.DataToBind();
            }
        }

        protected void BtnWelcomSave_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (((string)this.ViewState["EventWelcom"]) == "1")
            {
                num = this.Bll1.DAL1.ExecuteNonQuery("Update QH_WeixinAuto Set [Text]=@Text where AutoType='1'", new OleDbParameter[] { new OleDbParameter("@Text", base.Request.Form["eventWelcom"]) });
            }
            else
            {
                num = this.Bll1.DAL1.ExecuteNonQuery("insert into QH_WeixinAuto(AutoType,Type,[Text]) values('1','0',@Text)", new OleDbParameter[] { new OleDbParameter("@Text", base.Request.Form["eventWelcom"]) });
            }
            if (num == 1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "OK", "alert('保存成功');", true);
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "Fail", "alert('保存失败');", true);
            }
            this.DataToBind();
        }

        private void DataToBind()
        {
            this.strTr = "";
            int num = 0;
            List<string[]> list = this.Bll1.DAL1.ReadDataReaderListStr("select id,AutoType,Type,[key],Title,Descrpt,PicUrl,Url,[Text],Effect from QH_WeixinAuto where AutoType in('0','1','2') order by [key] asc, id asc", 10);
            foreach (string[] strArray in list)
            {
                if (strArray[1] == "1")
                {
                    this.strEventWelcom = strArray[8];
                    this.ViewState["EventWelcom"] = "1";
                }
                else if (strArray[1] == "2")
                {
                    this.strDefaultBack = strArray[8];
                    this.ViewState["DefaultBack"] = "1";
                }
                else if (strArray[2] == "1")
                {
                    this.SetTuwenRow(strArray, num++);
                }
            }
            foreach (string[] strArray2 in list)
            {
                if ((strArray2[1] == "0") && (strArray2[2] == "0"))
                {
                    this.SetTextRow(strArray2, num++);
                }
            }
            if (num == 0)
            {
                string str;
                this.BtnPicture.Style["display"] = str = "none";
                this.BtnNews.Style["display"] = this.BtnProduct.Style["display"] = str;
            }
            else
            {
                string str3;
                this.BtnPicture.Style["display"] = str3 = "";
                this.BtnNews.Style["display"] = this.BtnProduct.Style["display"] = str3;
            }
        }

        private void DataToBindAuto()
        {
            this.strPicUrl = this.Bll1.GetDomainUrl(1) + "images/WeixinPic.jpg";
            this.strNewsNum = "9";
            this.strProductNum = "9";
            this.strPictureNum = "9";
            foreach (string[] strArray in this.Bll1.DAL1.ReadDataReaderListStr("select AutoType,PicUrl,Url,Text from QH_WeixinAuto where AutoType<>'0'", 4))
            {
                if (strArray[0] == "1")
                {
                    this.strEventWelcom = strArray[3];
                }
                else if (strArray[0] == "2")
                {
                    this.strDefaultBack = strArray[3];
                }
                else if (strArray[0] == "3")
                {
                    this.strPicUrl = strArray[1];
                }
                else if (strArray[0] == "4")
                {
                    this.strNewsNum = strArray[1];
                    this.strProductNum = strArray[2];
                    this.strPictureNum = strArray[3];
                }
            }
        }

        private void GetIDMarkSubIN(ref string strColumnIDIn, string strID, string strMdl, DataTable dTableClmn)
        {
            DataRow[] rowArray = dTableClmn.Select("ParentID='" + strID + "' and Module='" + strMdl + "'");
            for (int i = 0; i < rowArray.Length; i++)
            {
                object obj2 = strColumnIDIn;
                strColumnIDIn = string.Concat(new object[] { obj2, ",'", rowArray[i]["id"], "'" });
                DataRow[] rowArray2 = dTableClmn.Select(string.Concat(new object[] { "ParentID='", rowArray[i]["id"], "' and Module='", strMdl, "'" }));
                for (int j = 0; j < rowArray2.Length; j++)
                {
                    object obj3 = strColumnIDIn;
                    strColumnIDIn = string.Concat(new object[] { obj3, ",'", rowArray2[j]["id"], "'" });
                }
            }
        }

        private string GetIsShowLink(string strDir, DataRow dRow)
        {
            string titleLink = "";
            string str2 = dRow["depth"].ToString();
            if ((dRow["IsShow"].ToString() == "0") && (str2 != "2"))
            {
                DataRow[] rowArray = this.dTableClmn.Select("ParentID='" + dRow["ID"] + "'");
                if (rowArray.Length > 0)
                {
                    switch (str2)
                    {
                        case "0":
                            if (rowArray[0]["IsShow"].ToString() == "0")
                            {
                                string str3 = rowArray[0]["folder"].ToString();
                                rowArray = this.dTableClmn.Select("ParentID='" + rowArray[0]["ID"] + "'");
                                if (rowArray.Length > 0)
                                {
                                    titleLink = this.GetTitleLink(rowArray[0], string.Concat(new object[] { strDir, "/", str3, "/", rowArray[0]["folder"] }));
                                }
                                return titleLink;
                            }
                            return this.GetTitleLink(rowArray[0], strDir + "/" + rowArray[0]["folder"]);

                        case "1":
                            titleLink = this.GetTitleLink(rowArray[0], strDir + "/" + rowArray[0]["folder"]);
                            break;
                    }
                }
                return titleLink;
            }
            return this.GetTitleLink(dRow, strDir);
        }

        private string GetTitleLink(DataRow dRow, string strUrlPath)
        {
            string str = dRow["outLink"].ToString().Trim();
            string str2 = dRow["fileName"].ToString().Trim();
            if (str2 != string.Empty)
            {
                str2 = (str2.IndexOf(".") < 0) ? (str2 + ".html") : str2;
            }
            if (!(str != ""))
            {
                return (this.strDomainURL + strUrlPath + "/" + str2);
            }
            if (str.Contains("http://"))
            {
                return str;
            }
            return this.PageUrlSet(str);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.BtnCancel.Style["display"] = "none";
                this.BtnsaveAuto.Style["display"] = "none";
                this.DataToBind();
            }
        }

        private string PageUrlSet(string str)
        {
            if (str.Contains("http://"))
            {
                return str;
            }
            string str2 = str.Trim();
            if (str2 == "")
            {
                return "";
            }
            if (str2[0] == '/')
            {
                return (this.strDomainURL + str2.Substring(1));
            }
            if (str2[0] == '.')
            {
                return (this.strDomainURL + str2.Substring(3));
            }
            return (this.strDomainURL + str2);
        }

        private void SetTextRow(string[] astr1, int nNum)
        {
            this.strTr = this.strTr + this.strTextTr.Replace("{0}", nNum.ToString()).Replace("{1}", ((nNum % 2) == 1) ? "e6e6e6" : "f9f9f9").Replace("{2}", astr1[0]).Replace("{key}", astr1[3]).Replace("{text}", astr1[8]).Replace("{Eff}", (astr1[9] == "True") ? "checked" : "");
        }

        private void SetTuwenRow(string[] astr1, int nNum)
        {
            this.strTr = this.strTr + this.strTuWenTr.Replace("{0}", nNum.ToString()).Replace("{1}", ((nNum % 2) == 1) ? "e6e6e6" : "f9f9f9").Replace("{2}", astr1[0]).Replace("{key}", astr1[3]).Replace("{Title}", astr1[4]).Replace("{Descrpt}", astr1[5]).Replace("{PicUrl}", astr1[6]).Replace("{Url}", astr1[7]).Replace("{Eff}", (astr1[9] == "True") ? "checked" : "");
        }
    }
}