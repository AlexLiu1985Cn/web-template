namespace TProcess
{
    using _BLL;
    using Microsoft.JScript;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using _DAL.OleDBHelper;
    using _commen;

    public class CreateStatic
    {
        private string[] astr1;
        private string[] astrBnrSet;
        private string[] astrColumn;
        private string[] astrtags;
        private bool bIsAuthorized;
        private bool bIsDefault;
        private bool bIsMobile;
        private BLL Bll1;
        private Page CSPage;
        private DataTable dTableClmn;
        private DataTable dTableProductAll;
        private DataTable dtBnrImg;
        private DataTable dtLink;
        private DataTable dtpListLoop;
        private string[][] ja2strBnrDefault;
        private List<string[]> listStrCusField;
        private List<string[]> liststrLang;
        private clLoopAttribute LoopAttribute;
        private int nBigLength;
        private int nMediumLength;
        private int nSmallLength;
        private string strBannerTemp;
        private string strClmnQuery;
        private string strColumnName;
        private string strFriendLinkImg;
        private string strGlobalRootDirPre;
        private string strJQueryNum;
        private string strLangDir;
        private string strLinkInsert;
        private string strLinkInsertDown;
        private string strPicTempS;

        public CreateStatic(string strUrl)
        {
            this.Bll1 = new BLL();
            this.LoopAttribute = new clLoopAttribute();
            this.CSPage = new Page();
            this.strGlobalRootDirPre = "";
            this.strLangDir = "";
            this.strJQueryNum = "0";
            this.strLinkInsertDown = "";
            this.astr1 = this.Bll1.DAL1.ReadDataReaderStringArray("select top 1 SiteTitle,SiteDescription,SiteKeyword,LiuyanQQNo,ZaixianQQNo,SitePath,SiteLogo,CopyrightInfo,[Tel],Address,ICPBackup,[Fax],Mobile,Email,Contact,Bak1,Bak2,BakImg1,BakImg2,FirstPageCnt,JScript,picStyle,ImgStyle,ImgListStyle,FriendL,MobileOn,MobilePath,MobileLogo,Bottom1,Bottom2,Bottom3,BottomQT,Bak3,BakImg3,AntiDown,Favicon,JScriptStat3,Stat3On ,CompanyName,MultiLang from QH_SiteInfo", 40);
            this.strClmnQuery = "select id,ColumnName,ParentID,depth,Order1,IDMark,Nav,[Module],folder,TemplateName,fileName,outLink,NewWin,ListOrder,ListContent,IsShow,Access,ctitle,KeyW,Description,C_img,C_EnTitle,content,NumInPage,BnrMode,BnrStyle,BnrWidth,BnrHeight,BnrDefault,BriefIntro,ImageUrl,DetailTemplate,ProductTMInherit from QH_Column order by CLNG(Order1) asc, CLNG(ParentID) asc,CLNG(depth) asc,id asc";
            this.dTableClmn = this.Bll1.GetDataTable(this.strClmnQuery);
            if (this.dTableClmn.Rows.Count > 0)
            {
                this.AddClmnOderInt(this.dTableClmn);
            }
            this.bIsAuthorized = this.IsAuthorized(strUrl);
            this.Bll1.GetDataTable("select Tags,Link from QH_RelatedLink ");
            this.SetLanguageIDMark(strUrl);
        }

        public CreateStatic(string strUrl, string strMobile)
        {
            this.Bll1 = new BLL();
            this.LoopAttribute = new clLoopAttribute();
            this.CSPage = new Page();
            this.strGlobalRootDirPre = "";
            this.strLangDir = "";
            this.strJQueryNum = "0";
            this.strLinkInsertDown = "";
            this.bIsMobile = true;
            this.astr1 = this.Bll1.DAL1.ReadDataReaderStringArray("select top 1 SiteTitle,SiteDescription,SiteKeyword,LiuyanQQNo,ZaixianQQNo,SitePath,SiteLogo,CopyrightInfo,[Tel],Address,ICPBackup,[Fax],Mobile,Email,Contact,Bak1,Bak2,BakImg1,BakImg2,FirstPageCnt,JScript,picStyle,ImgStyle,ImgListStyle,FriendL,MobileOn,MobilePath,MobileLogo,Bottom1,Bottom2,Bottom3,BottomQT,Bak3,BakImg3,AntiDown,Favicon,JScriptStat3,Stat3On,CompanyName,MultiLang from QH_SiteInfo", 40);
            this.strClmnQuery = "select id,ColumnName,ParentID,depth,Order1,IDMark,Nav,[Module],folder,TemplateName,fileName,outLink,NewWin,ListOrder,ListContent,IsShow,Access,ctitle,KeyW,Description,C_img,C_EnTitle,content,NumInPage,BnrMode,BnrStyle,BnrWidth,BnrHeight,BnrDefault,BriefIntro,ImageUrl,NavMobile,DetailTemplate,ProductTMInherit from QH_Column where [Module]<>'4' order by CLNG(Order1) asc, CLNG(ParentID) asc,CLNG(depth) asc,id asc";
            this.dTableClmn = this.Bll1.GetDataTable(this.strClmnQuery);
            if (this.dTableClmn.Rows.Count > 0)
            {
                this.AddClmnOderInt(this.dTableClmn);
            }
            this.bIsAuthorized = this.IsAuthorized(strUrl);
            this.Bll1.GetDataTable("select Tags,Link from QH_RelatedLink ");
            this.SetLanguageIDMark(strUrl);
        }

        private void AddClmnIDMarkInt(DataTable dTableClmn)
        {
            DataColumn column = new DataColumn("IDMarkInt")
            {
                DataType = Type.GetType("System.Int32"),
                Expression = "Convert(IDMark, 'System.Int32')"
            };
            dTableClmn.Columns.Add(column);
        }

        private void AddClmnOderInt(DataTable dTableClmn)
        {
            DataColumn column = new DataColumn("Order")
            {
                DataType = Type.GetType("System.Int32"),
                Expression = "Convert(Order1, 'System.Int32')"
            };
            dTableClmn.Columns.Add(column);
        }

        private void AddPriceColumn(ref DataTable dTable)
        {
            DataColumn column = new DataColumn("DblPrice")
            {
                DataType = Type.GetType("System.Double")
            };
            dTable.Columns.Add(column);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                if (dTable.Rows[i]["Price"].ToString().Trim() == "")
                {
                    dTable.Rows[i]["Price"] = "0.0";
                }
                try
                {
                    dTable.Rows[i]["DblPrice"] = double.Parse(dTable.Rows[i]["Price"].ToString());
                }
                catch (Exception exception)
                {
                    SystemError.CreateErrorLog(exception.ToString());
                    break;
                }
            }
        }

        private bool CheckAuthorized(string strDomain)
        {
            string strEnCode = System.Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select top 1 AuthorCode from QH_SiteInfo"));
            if (strEnCode.Length != 0x40)
            {
                return false;
            }
            if (strEnCode[0x3f] == '1')
            {
                strEnCode = this.Bll1.DecodeAuthorCode(strEnCode);
            }
            else
            {
                strEnCode = this.Bll1.DecodeAuthorCodeTry(strEnCode);
            }
            if (strEnCode.Substring(0x2a, 0x15) != FormsAuthentication.HashPasswordForStoringInConfigFile(strDomain, "MD5").Substring(0, 0x15))
            {
                return false;
            }
            if (strEnCode[0x3f] != '1')
            {
                DateTime time;
                DateTime time2;
                string input = this.Bll1.DateDay10Decode(strEnCode.Substring(0x20, 10));
                string str3 = input.Substring(0, 6);
                string s = input.Substring(6, 4);
                if (!Regex.IsMatch(input, @"^\d{10}$"))
                {
                    return false;
                }
                string fileString = this.Bll1.GetFileString(this.CSPage.Server.MapPath("QH_WaterMarkSet.aspx"));
                if (fileString == null)
                {
                    return false;
                }
                string str6 = this.Bll1.GetContentBetweenTags("pt\" style=\"color:#", ";", fileString);
                if (!Regex.IsMatch(str6, "^[a0c6812f3d]{6}$"))
                {
                    return false;
                }
                string str7 = "";
                char[] chArray = new char[] { 'a', '0', 'c', '6', '8', '1', '2', 'f', '3', 'd' };
                for (int i = 0; i < 6; i++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if (str6[i] == chArray[k])
                        {
                            str7 = str7 + k.ToString();
                            break;
                        }
                    }
                }
                try
                {
                    time = DateTime.Parse("20" + str3.Insert(4, "-").Insert(2, "-"));
                    time2 = DateTime.Parse("20" + str7.Insert(4, "-").Insert(2, "-"));
                }
                catch
                {
                    return false;
                }
                if (time.AddDays(double.Parse(s)).CompareTo(time2) < 0)
                {
                    return false;
                }
                string strTempContent = this.Bll1.GetFileString(this.CSPage.Server.MapPath("Admin_User.aspx"));
                if (strTempContent == null)
                {
                    return false;
                }
                string str9 = this.Bll1.GetContentBetweenTags("<th class=\"th", "\"", strTempContent);
                if (!Regex.IsMatch(str9, "^[abcdeghfik1]{8}$"))
                {
                    return false;
                }
                string str10 = "";
                char[] chArray2 = new char[] { 'a', 'b', 'c', 'd', 'e', 'g', 'h', 'f', 'i', 'k' };
                for (int j = 0; j < 6; j++)
                {
                    for (int m = 0; m < 10; m++)
                    {
                        if (str9[j] == chArray2[m])
                        {
                            str10 = str10 + m.ToString();
                            break;
                        }
                    }
                }
                if (str7 != str10)
                {
                    return false;
                }
            }
            return true;
        }

        private void CheckRootDir(string strRootDir)
        {
            DirectoryInfo info = new DirectoryInfo(this.CSPage.Server.MapPath("../" + strRootDir));
            if (!info.Exists)
            {
                info.Create();
            }
        }

        private string ColumnUrlPre(DataRow dRow, DataTable dTableClmn)
        {
            string str = "";
            string str2 = dRow["depth"].ToString();
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "1"))
            {
                if (str2 != "2")
                {
                    return str;
                }
            }
            else
            {
                return dTableClmn.Select("id=" + dRow["ParentID"])[0]["folder"].ToString();
            }
            DataRow[] rowArray = dTableClmn.Select("id=" + dRow["ParentID"]);
            return (dTableClmn.Select("id=" + rowArray[0]["ParentID"])[0]["folder"] + "/" + rowArray[0]["folder"]);
        }

        private string CopyRightError(int nCopyRight)
        {
            switch (nCopyRight)
            {
                case 1:
                    return "<BR>由于不包含授权链接标贴，不能生成静态页面。！<BR>请在模板里加上标贴[QH:CDQHLink]，或申请授权！";

                case 2:
                    return "<BR>标贴[QH:CDQHLink]必须在body中间，或申请授权！";

                case 3:
                    return "<BR>不能注释掉标贴[QH:CDQHLink]！或申请授权！";

                case 4:
                    return "<BR>模板格式有问题！";
            }
            return "未知";
        }

        private string CopyRightError(int nCopyRight, ref string strOKInfo)
        {
            switch (nCopyRight)
            {
                case 1:
                    strOKInfo = @"由于不包含授权链接标贴，不能生成静态页面。！\n请在模板里加上标贴[QH:CDQHLink]，或申请授权！";
                    return "<BR>由于不包含授权链接标贴，不能生成静态页面。！<BR>请在模板里加上标贴[QH:CDQHLink]，或申请授权！";

                case 2:
                    strOKInfo = "标贴[QH:CDQHLink]必须在body中间，或申请授权！";
                    return "<BR>标贴[QH:CDQHLink]必须在body中间，或申请授权！";

                case 3:
                    strOKInfo = "不能注释掉标贴[QH:CDQHLink]！或申请授权！";
                    return "<BR>不能注释掉标贴[QH:CDQHLink]！或申请授权！";

                case 4:
                    strOKInfo = "模板格式有问题！";
                    return "<BR>模板格式有问题！";
            }
            return "未知";
        }

        public string CreateDownload(string strAbsPath, string strTPath, string strRootDir, ArrayList alistID)
        {
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "下载模块模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='4'");
            if (rowArray.Length == 0)
            {
                return "没有属于下载模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            foreach (DataRow row in rowArray)
            {
                string str3;
                string str5;
                string str6;
                string str9;
                string[] strArray;
                if (row["IsShow"].ToString() == "0")
                {
                    continue;
                }
                string strColumnID = row["id"].ToString();
                if ((alistID != null) && !this.IsIDInArrayList(strColumnID, alistID))
                {
                    continue;
                }
                if (row["outLink"].ToString().Trim() != string.Empty)
                {
                    string str12 = str2;
                    str2 = str12 + "栏目《" + row["ColumnName"].ToString() + "》为外部链接：" + row["outLink"].ToString().Trim() + "！";
                    continue;
                }
                string newValue = row["TemplateName"].ToString().Trim();
                if ((newValue != string.Empty) && !this.bIsMobile)
                {
                    str6 = this.GetTemplateContent(strAbsPath.Replace("Module_Download.aspx", newValue));
                    if (!string.IsNullOrEmpty(str6))
                    {
                        nCopyRight = this.SetCopyRight(ref str6);
                        if (nCopyRight > 0)
                        {
                            return this.CopyRightError(nCopyRight);
                        }
                        goto Label_0220;
                    }
                    string str13 = str2;
                    str2 = str13 + "《" + row["ColumnName"].ToString() + @"》模板template\" + strTPath + @"\" + newValue + "不存在或为空";
                    continue;
                }
                str6 = templateContent;
            Label_0220:
                str3 = row["depth"].ToString();
                this.GetSiteInfoContents(ref str6, str3, strColumnID);
                this.ReplaceNav(ref str6, str3);
                if (str6.IndexOf("[QH:Banner]") >= 0)
                {
                    this.ReplaceBanner(ref str6, str3, strColumnID);
                }
                this.ReplaceLoop(ref str6, str3, strColumnID);
                this.ReplaceTitle(ref str6, str3, strColumnID);
                this.ReplaceProductListTitle(ref str6, str3, strColumnID);
                this.ReplaceLanmuLoopBig(ref str6, str3, strColumnID);
                this.ReplaceNavLoopBig(ref str6, str3, strColumnID);
                this.ReplaceFenyeTitle(ref str6, row, str3);
                this.ReplaceCommen(ref str6, row, str3);
                this.ReplaceStatisticsJS(ref str6, str3);
                if (str6.IndexOf("[QH:BaiduMap") >= 0)
                {
                    this.ReplaceBaiduMap(ref str6, str3);
                }
                if (str6.IndexOf("[QH:SearchOnclick") >= 0)
                {
                    this.ReplaceSearch(ref str6, str3, "News");
                }
                if (str6.IndexOf("[QH:Advertise") >= 0)
                {
                    this.ReplaceAdvertise(ref str6, str3);
                }
                if ((str6.Contains("[QH:LangLink") || str6.Contains("[QH:LangVersion")) || str6.Contains("[QH:LangImg"))
                {
                    this.ReplaceMultiLang(ref str6, "", str3);
                }
                if (str6.IndexOf("[QH:GuestBook ") >= 0)
                {
                    this.ReplaceMwssage(ref str6, str3, row["ColumnName"].ToString());
                }
                if (!this.GetStaticFUrlPath(out str5, row, this.dTableClmn, strRootDir))
                {
                    return "无法创建目录。";
                }
                string createdFileName = this.GetCreatedFileName(row["fileName"].ToString().Trim());
                string[] astrContent = this.GetPageContent(ref str6, row, out str9, "", out strArray);
                if (astrContent == null)
                {
                    int index = str6.IndexOf("[QH:Pager");
                    if (index != -1)
                    {
                        int num3 = str6.IndexOf(']', index);
                        string oldValue = str6.Substring(index, (num3 - index) + 1);
                        str6 = str6.Replace(oldValue, "");
                    }
                    if (str6.IndexOf("[QH:PageLoop]") > 0)
                    {
                        str2 = str2 + this.ReplacePagerLoop(ref str6);
                    }
                    str2 = str2 + this.CreateStaticFile(str5 + "/" + createdFileName, str6, row["ColumnName"].ToString());
                }
                else
                {
                    str2 = str2 + this.CreateListPager(str6, astrContent, createdFileName, str5, row["ColumnName"].ToString(), str9, "", strArray);
                }
            }
            return str2;
        }

        public string CreateDownloadDetails(string strAbsPath, int nCreateNumber, string strRootDir, string strBackFile)
        {
            string str6;
            int num = 30;
            int num2 = 0;
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "下载内容页模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='4'");
            if (rowArray.Length == 0)
            {
                return "没有属于下载模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            string format = "select id,Title,DownloadUrl,FileSize,BriefIntro,Content,AddDate,ModyDate,ctitle,KeyW,Description,Tags,Author,hits from QH_Download where ColumnID='{0}' order by TopShow asc,{1} {2}";
            if (!this.GetDetailsUrlPath(out str6, "DownloadDetails", strRootDir))
            {
                return "无法创建目录。";
            }
            DataTable dataTable = this.Bll1.GetDataTable("select id,name,no_order,wr_ok,columnID from QH_Parameter where [Module]='4' order by CLNG(columnID) asc,CLNG(no_order) asc");
            string strPageDepth = "0";
            foreach (DataRow row in rowArray)
            {
                string str11;
                string str12;
                string str10 = null;
                string depthTemplate = this.GetDepthTemplate(row, "DetailTemplate");
                if ((depthTemplate != string.Empty) && !this.bIsMobile)
                {
                    str10 = this.GetTemplateContent(strAbsPath.Replace("Module_DownloadDetails.aspx", depthTemplate));
                    if (string.IsNullOrEmpty(str10))
                    {
                        string str15 = str2;
                        str2 = str15 + "《" + row["ColumnName"].ToString() + "》模板" + depthTemplate + "不存在或为空";
                        continue;
                    }
                    nCopyRight = this.SetCopyRight(ref str10);
                    if (nCopyRight > 0)
                    {
                        return this.CopyRightError(nCopyRight);
                    }
                }
                string strClmndepth = row["depth"].ToString();
                string str5 = row["id"].ToString();
                this.GetCustumPara(templateContent, "'" + str5 + "'", "4");
                this.GetOrderby(out str11, out str12, row["ListOrder"].ToString());
                DataTable dtDtls = this.Bll1.GetDataTable(string.Format(format, str5, str12, str11));
                string[,] strArray = null;
                DataTable dtPList = this.GetColumnCusFData(dataTable, str5, ref strArray, "4");
                int nNumber = 0;
                foreach (DataRow row2 in dtDtls.Rows)
                {
                    if (num2 < nCreateNumber)
                    {
                        num2++;
                    }
                    else
                    {
                        if (num2 >= (nCreateNumber + num))
                        {
                            object obj2 = str2;
                            str2 = string.Concat(new object[] { obj2, "<br>此次分段生成", num, "个文件！" });
                            string str16 = str2;
                            string[] strArray3 = new string[] { str16, "<script language=javascript>window.location.href=('", strBackFile, ".aspx?Mdl=DD&LastNumber=", (nCreateNumber + num).ToString(), "')</script>" };
                            return string.Concat(strArray3);
                        }
                        num2++;
                        string strTemp = (str10 == null) ? templateContent : str10;
                        this.GetSiteInfoContents(ref strTemp, strPageDepth, str5, strClmndepth, row2);
                        this.ReplaceNav(ref strTemp, strPageDepth);
                        if (strTemp.IndexOf("[QH:Banner]") >= 0)
                        {
                            this.ReplaceBanner(ref strTemp, strPageDepth, str5);
                        }
                        this.ReplaceLoop(ref strTemp, strPageDepth, str5);
                        this.ReplaceTitle(ref strTemp, strPageDepth, str5);
                        this.ReplaceProductListTitle(ref strTemp, strPageDepth, str5);
                        this.ReplaceLanmuLoopBig(ref strTemp, strPageDepth, str5);
                        this.ReplaceNavLoopBig(ref strTemp, strPageDepth, str5);
                        this.ReplaceFenyeTitle(ref strTemp, row, strPageDepth);
                        this.ReplaceBackList(ref strTemp, row, strPageDepth);
                        this.ReplaceCommen(ref strTemp, row, strPageDepth);
                        this.ReplaceDownloadDetails(ref strTemp, row2, strPageDepth);
                        this.ReplaceCusField(ref strTemp, row2, dtPList, strArray, "4");
                        this.ReplaceDetailsPager(ref strTemp, dtDtls, nNumber, "DownloadDetails_");
                        this.ReplaceStatisticsJS(ref strTemp, strPageDepth);
                        if (strTemp.IndexOf("[QH:BaiduMap") >= 0)
                        {
                            this.ReplaceBaiduMap(ref strTemp, strPageDepth);
                        }
                        if (strTemp.IndexOf("[QH:SearchOnclick") >= 0)
                        {
                            this.ReplaceSearch(ref strTemp, strPageDepth, "News");
                        }
                        if (strTemp.IndexOf("[QH:Advertise") >= 0)
                        {
                            this.ReplaceAdvertise(ref strTemp, strPageDepth);
                        }
                        if ((strTemp.Contains("[QH:LangLink") || strTemp.Contains("[QH:LangVersion")) || strTemp.Contains("[QH:LangImg"))
                        {
                            this.ReplaceMultiLang(ref strTemp, "", strPageDepth);
                        }
                        if (strTemp.IndexOf("[QH:GuestBook ") >= 0)
                        {
                            this.ReplaceMwssage(ref strTemp, strPageDepth, row2["Title"].ToString());
                        }
                        string str13 = "DownloadDetails_" + row2["id"] + ".html";
                        str2 = str2 + this.CreateStaticFile(str6 + "/" + str13, strTemp, row["ColumnName"].ToString() + "详细内容");
                        nNumber++;
                    }
                }
            }
            return str2;
        }

        public string CreateDownloadDetailsOne(string strAbsPath, string strID, string strColumnID, ref string strOKInfo, bool bDelCreate, bool bOneCreate, string strRootDir)
        {
            string str5;
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                strOKInfo = "下载内容页模板不存在或为空";
                return "下载内容页模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight, ref strOKInfo);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='4'");
            if (rowArray.Length == 0)
            {
                return "没有属于下载模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            string format = "select id,Title,DownloadUrl,FileSize,BriefIntro,Content,AddDate,ModyDate,ctitle,KeyW,Description,Tags,Author,hits from QH_Download where ColumnID='{0}' order by TopShow asc,{1} {2}";
            if (!this.GetDetailsUrlPath(out str5, "DownloadDetails", strRootDir))
            {
                strOKInfo = "生成失败！";
                return "无法创建目录。";
            }
            DataTable dataTable = this.Bll1.GetDataTable("select id,name,no_order,wr_ok,columnID from QH_Parameter where [Module]='4' order by CLNG(columnID) asc,CLNG(no_order) asc");
            string strPageDepth = "0";
            foreach (DataRow row in rowArray)
            {
                string str9 = null;
                string depthTemplate = this.GetDepthTemplate(row, "DetailTemplate");
                if ((depthTemplate != string.Empty) && !this.bIsMobile)
                {
                    str9 = this.GetTemplateContent(strAbsPath.Replace("Module_DownloadDetails.aspx", depthTemplate));
                    if (string.IsNullOrEmpty(str9))
                    {
                        string str17 = str2;
                        str2 = str17 + "《" + row["ColumnName"].ToString() + "》模板" + depthTemplate + "不存在或为空";
                        continue;
                    }
                    nCopyRight = this.SetCopyRight(ref str9);
                    if (nCopyRight > 0)
                    {
                        return this.CopyRightError(nCopyRight, ref strOKInfo);
                    }
                }
                string strClmndepth = row["depth"].ToString();
                if (strColumnID == row["id"].ToString())
                {
                    string str10;
                    string str11;
                    this.GetCustumPara(templateContent, "'" + strColumnID + "'", "4");
                    this.GetOrderby(out str10, out str11, row["ListOrder"].ToString());
                    DataTable dtCtnt = this.Bll1.GetDataTable(string.Format(format, strColumnID, str11, str10));
                    string str12 = "";
                    string str13 = "";
                    if (!bOneCreate)
                    {
                        this.GetInfluenceID(strID, ref str12, ref str13, dtCtnt);
                    }
                    string[,] strArray = null;
                    DataTable dtPList = this.GetColumnCusFData(dataTable, strColumnID, ref strArray, "4");
                    if (bDelCreate)
                    {
                        dtCtnt = this.GetNoDelTable(dtCtnt, strID);
                    }
                    int nNumber = 0;
                    foreach (DataRow row2 in dtCtnt.Rows)
                    {
                        string str6;
                        string str14 = row2["id"].ToString();
                        if (bDelCreate)
                        {
                            if ((str12 == str14) || (str13 == str14))
                            {
                                goto Label_02CE;
                            }
                            nNumber++;
                            continue;
                        }
                        if ((!(str12 == str14) && !(str13 == str14)) && !(str14 == strID))
                        {
                            nNumber++;
                            continue;
                        }
                    Label_02CE:
                        str6 = (str9 == null) ? templateContent : str9;
                        this.GetSiteInfoContents(ref str6, strPageDepth, strColumnID, strClmndepth, row2);
                        this.ReplaceNav(ref str6, strPageDepth);
                        if (str6.IndexOf("[QH:Banner]") >= 0)
                        {
                            this.ReplaceBanner(ref str6, strPageDepth, strColumnID);
                        }
                        this.ReplaceLoop(ref str6, strPageDepth, strColumnID);
                        this.ReplaceTitle(ref str6, strPageDepth, strColumnID);
                        this.ReplaceProductListTitle(ref str6, strPageDepth, strColumnID);
                        this.ReplaceLanmuLoopBig(ref str6, strPageDepth, strColumnID);
                        this.ReplaceNavLoopBig(ref str6, strPageDepth, strColumnID);
                        this.ReplaceFenyeTitle(ref str6, row, strPageDepth);
                        this.ReplaceBackList(ref str6, row, strPageDepth);
                        this.ReplaceCommen(ref str6, row, strPageDepth);
                        this.ReplaceDownloadDetails(ref str6, row2, strPageDepth);
                        this.ReplaceCusField(ref str6, row2, dtPList, strArray, "4");
                        this.ReplaceDetailsPager(ref str6, dtCtnt, nNumber, "DownloadDetails_");
                        this.ReplaceStatisticsJS(ref str6, strPageDepth);
                        if (str6.IndexOf("[QH:BaiduMap") >= 0)
                        {
                            this.ReplaceBaiduMap(ref str6, strPageDepth);
                        }
                        if (str6.IndexOf("[QH:SearchOnclick") >= 0)
                        {
                            this.ReplaceSearch(ref str6, strPageDepth, "News");
                        }
                        if (str6.IndexOf("[QH:Advertise") >= 0)
                        {
                            this.ReplaceAdvertise(ref str6, strPageDepth);
                        }
                        if ((str6.Contains("[QH:LangLink") || str6.Contains("[QH:LangVersion")) || str6.Contains("[QH:LangImg"))
                        {
                            this.ReplaceMultiLang(ref str6, "", strPageDepth);
                        }
                        if (str6.IndexOf("[QH:GuestBook ") >= 0)
                        {
                            this.ReplaceMwssage(ref str6, strPageDepth, row2["Title"].ToString());
                        }
                        string str15 = "DownloadDetails_" + row2["id"] + ".html";
                        str2 = str2 + this.CreateStaticFile(str5 + "/" + str15, str6, row["ColumnName"].ToString() + "详细内容");
                        nNumber++;
                    }
                }
            }
            return str2;
        }

        public string CreateHome(string strAbsPath, string strRootDir)
        {
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "首页模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            this.ReplaceHomeLabeling(ref templateContent);
            if (templateContent.IndexOf("[QH:GuestBook ") >= 0)
            {
                this.ReplaceMwssage(ref templateContent, "S", "首页");
            }
            strRootDir = (strRootDir == "") ? "" : (strRootDir + "/");
            if (this.astr1[0x19] == "True")
            {
                if (this.bIsMobile)
                {
                    this.SetJS(ref templateContent, "<script type=\"text/javascript\" >var vComputer=\"" + this.astr1[5] + "\";</script><script type =\"text/javascript\" src=\"../Ajax/pc1.js\" ></script>\n");
                }
                else
                {
                    this.SetJS(ref templateContent, "<script type=\"text/javascript\" >var vMobile=\"" + this.astr1[0x1a] + "\";;</script><script type =\"text/javascript\" src=\"" + strRootDir + "Ajax/pc.js\" ></script>\n");
                }
            }
            if (strRootDir != "")
            {
                this.CheckRootDir(strRootDir);
            }
            return this.CreateStaticFile(strRootDir + "Index.html", templateContent, "首页");
        }

        public string CreateHrDemand(string strAbsPath, string strTPath, string strRootDir)
        {
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "人才招聘模块模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='8'");
            if (rowArray.Length == 0)
            {
                return "没有属于人才招聘模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            foreach (DataRow row in rowArray)
            {
                string str3;
                string str5;
                string str6;
                string str10;
                string[] strArray;
                if (row["outLink"].ToString().Trim() != string.Empty)
                {
                    string str14 = str2;
                    str2 = str14 + "栏目《" + row["ColumnName"].ToString() + "》为外部链接：" + row["outLink"].ToString().Trim() + "！";
                    continue;
                }
                string newValue = row["TemplateName"].ToString().Trim();
                if ((newValue != string.Empty) && !this.bIsMobile)
                {
                    str6 = this.GetTemplateContent(strAbsPath.Replace("Module_ZhaoPin.aspx", newValue));
                    if (!string.IsNullOrEmpty(str6))
                    {
                        nCopyRight = this.SetCopyRight(ref str6);
                        if (nCopyRight > 0)
                        {
                            return this.CopyRightError(nCopyRight);
                        }
                        goto Label_01DA;
                    }
                    string str15 = str2;
                    str2 = str15 + "《" + row["ColumnName"].ToString() + @"》模板template\" + strTPath + @"\" + newValue + "不存在或为空";
                    continue;
                }
                str6 = templateContent;
            Label_01DA:
                str3 = row["depth"].ToString();
                string strColumnID = row["id"].ToString();
                this.GetSiteInfoContents(ref str6, str3, strColumnID);
                this.ReplaceNav(ref str6, str3);
                if (str6.IndexOf("[QH:Banner]") >= 0)
                {
                    this.ReplaceBanner(ref str6, str3, strColumnID);
                }
                this.ReplaceLoop(ref str6, str3, strColumnID);
                this.ReplaceTitle(ref str6, str3, strColumnID);
                this.ReplaceProductListTitle(ref str6, str3, strColumnID);
                this.ReplaceLanmuLoopBig(ref str6, str3, strColumnID);
                this.ReplaceNavLoopBig(ref str6, str3, strColumnID);
                this.ReplaceFenyeTitle(ref str6, row, str3);
                this.ReplaceCommen(ref str6, row, str3);
                this.ReplaceStatisticsJS(ref str6, str3);
                if (str6.IndexOf("[QH:SearchOnclick") >= 0)
                {
                    this.ReplaceSearch(ref str6, str3, "News");
                }
                if (str6.IndexOf("[QH:Advertise") >= 0)
                {
                    this.ReplaceAdvertise(ref str6, str3);
                }
                if ((str6.Contains("[QH:LangLink") || str6.Contains("[QH:LangVersion")) || str6.Contains("[QH:LangImg"))
                {
                    this.ReplaceMultiLang(ref str6, "", str3);
                }
                string str8 = str6;
                string createdFileName = this.GetCreatedFileName(row["fileName"].ToString().Trim());
                if (!this.GetStaticFUrlPath(out str5, row, this.dTableClmn, strRootDir))
                {
                    return "无法创建目录。";
                }
                string[] astrContent = this.GetPageContent(ref str6, row, out str10, "", out strArray);
                string oldValue = "";
                int index = str6.IndexOf("[QH:Pager");
                if (index != -1)
                {
                    int num3 = str6.IndexOf(']', index);
                    oldValue = str6.Substring(index, (num3 - index) + 1);
                }
                if (astrContent == null)
                {
                    if (index != -1)
                    {
                        str6 = str6.Replace(oldValue, "");
                    }
                    if (str6.IndexOf("[QH:PageLoop]") > 0)
                    {
                        str2 = str2 + this.ReplacePagerLoop(ref str6);
                    }
                    str2 = str2 + this.CreateStaticFile(str5 + "/" + createdFileName, str6, row["ColumnName"].ToString());
                }
                else
                {
                    str2 = str2 + this.CreateListPager(str6, astrContent, createdFileName, str5, row["ColumnName"].ToString(), str10, "", strArray);
                    if (index != -1)
                    {
                        str8 = str8.Replace(oldValue, "");
                    }
                    if (str8.IndexOf("[QH:PageLoop]") > 0)
                    {
                        str2 = str2 + this.ReplacePagerLoop(ref str8);
                    }
                    str8 = str8.Replace("[QH:TotlePage]", "1").Replace("[QH:TotleNumber]", "1").Replace("[QH:NumberInPage]", "1").Replace("[QH:CurrentPage]", "1").Replace("[QH:HrDemand]", this.GetHrAcceptUI());
                    string strFilter = System.Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select top 1 RepChar from QH_ZhaoPinSet"));
                    this.SetJS(ref str8, this.GetHrAcceptJsScript(str3, strFilter));
                    str2 = str2 + this.CreateStaticFile(str5 + "/HrDemandAccept.html", str8, "人才应聘");
                }
            }
            return str2;
        }

        public string CreateImage(string strAbsPath, string strTPath, string strRootDir, ArrayList alistID)
        {
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "图片模块模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='5'");
            if (rowArray.Length == 0)
            {
                return "没有属于图片模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            foreach (DataRow row in rowArray)
            {
                string str3;
                string str5;
                string str6;
                string str9;
                string[] strArray;
                string[] strArray2;
                if (row["IsShow"].ToString() == "0")
                {
                    continue;
                }
                string strColumnID = row["id"].ToString();
                if ((alistID != null) && !this.IsIDInArrayList(strColumnID, alistID))
                {
                    continue;
                }
                if (row["outLink"].ToString().Trim() != string.Empty)
                {
                    string str12 = str2;
                    str2 = str12 + "栏目《" + row["ColumnName"].ToString() + "》为外部链接：" + row["outLink"].ToString().Trim() + "！";
                    continue;
                }
                string newValue = row["TemplateName"].ToString().Trim();
                if ((newValue != string.Empty) && !this.bIsMobile)
                {
                    str6 = this.GetTemplateContent(strAbsPath.Replace("Module_Picture.aspx", newValue));
                    if (!string.IsNullOrEmpty(str6))
                    {
                        nCopyRight = this.SetCopyRight(ref str6);
                        if (nCopyRight > 0)
                        {
                            return this.CopyRightError(nCopyRight);
                        }
                        goto Label_0220;
                    }
                    string str13 = str2;
                    str2 = str13 + "《" + row["ColumnName"].ToString() + @"》模板template\" + strTPath + @"\" + newValue + "不存在或为空";
                    continue;
                }
                str6 = templateContent;
            Label_0220:
                str3 = row["depth"].ToString();
                this.GetSiteInfoContents(ref str6, str3, strColumnID);
                this.ReplaceNav(ref str6, str3);
                if (str6.IndexOf("[QH:Banner]") >= 0)
                {
                    this.ReplaceBanner(ref str6, str3, strColumnID);
                }
                this.ReplaceLoop(ref str6, str3, strColumnID);
                this.ReplaceTitle(ref str6, str3, strColumnID);
                this.ReplaceProductListTitle(ref str6, str3, strColumnID);
                this.ReplaceLanmuLoopBig(ref str6, str3, strColumnID);
                this.ReplaceNavLoopBig(ref str6, str3, strColumnID);
                this.ReplaceFenyeTitle(ref str6, row, str3);
                this.ReplaceCommen(ref str6, row, str3);
                this.ReplaceStatisticsJS(ref str6, str3);
                if (str6.IndexOf("[QH:BaiduMap") >= 0)
                {
                    this.ReplaceBaiduMap(ref str6, str3);
                }
                if (str6.IndexOf("[QH:SearchOnclick") >= 0)
                {
                    this.ReplaceSearch(ref str6, str3, "News");
                }
                if (str6.IndexOf("[QH:Advertise") >= 0)
                {
                    this.ReplaceAdvertise(ref str6, str3);
                }
                if ((str6.Contains("[QH:LangLink") || str6.Contains("[QH:LangVersion")) || str6.Contains("[QH:LangImg"))
                {
                    this.ReplaceMultiLang(ref str6, "", str3);
                }
                if (str6.IndexOf("[QH:GuestBook ") >= 0)
                {
                    this.ReplaceMwssage(ref str6, str3, row["ColumnName"].ToString());
                }
                if (!this.GetStaticFUrlPath(out str5, row, this.dTableClmn, strRootDir))
                {
                    return "无法创建目录。";
                }
                string createdFileName = this.GetCreatedFileName(row["fileName"].ToString().Trim());
                if (this.astr1[0x17] == "0")
                {
                    strArray = this.GetPageContent(ref str6, row, out str9, "", out strArray2);
                }
                else
                {
                    strArray = this.GetPageContent(ref str6, row, out str9, "Pubu", out strArray2);
                }
                if (strArray == null)
                {
                    int index = str6.IndexOf("[QH:Pager");
                    if (index != -1)
                    {
                        int num3 = str6.IndexOf(']', index);
                        string oldValue = str6.Substring(index, (num3 - index) + 1);
                        str6 = str6.Replace(oldValue, "");
                    }
                    if (str6.IndexOf("[QH:PageLoop]") > 0)
                    {
                        str2 = str2 + this.ReplacePagerLoop(ref str6);
                    }
                    str2 = str2 + this.CreateStaticFile(str5 + "/" + createdFileName, str6, row["ColumnName"].ToString());
                }
                else
                {
                    str2 = str2 + this.CreateListPager(str6, strArray, createdFileName, str5, row["ColumnName"].ToString(), str9, "Pubu", strArray2);
                }
            }
            return str2;
        }

        public string CreateImageDetails(string strAbsPath, int nCreateNumber, string strRootDir, string strBackFile)
        {
            string str6;
            int num = 30;
            int num2 = 0;
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "图片内容页模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='5'");
            if (rowArray.Length == 0)
            {
                return "没有属于图片模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            string format = "select id,Title,ImageUrl,ThumbUrl,displayImg,BriefIntro,Content,AddDate,ModyDate,ctitle,KeyW,Description,Author,Tags,hits from QH_Img where ColumnID='{0}' order by TopShow asc,{1} {2}";
            if (!this.GetDetailsUrlPath(out str6, "PictureDetails", strRootDir))
            {
                return "无法创建目录。";
            }
            DataTable dataTable = this.Bll1.GetDataTable("select id,name,no_order,wr_ok,columnID from QH_Parameter where [Module]='5' order by CLNG(columnID) asc,CLNG(no_order) asc");
            string strPageDepth = "0";
            this.SetImagePicLinkTM(strPageDepth);
            foreach (DataRow row in rowArray)
            {
                string str11;
                string str12;
                string str10 = null;
                string depthTemplate = this.GetDepthTemplate(row, "DetailTemplate");
                if ((depthTemplate != string.Empty) && !this.bIsMobile)
                {
                    str10 = this.GetTemplateContent(strAbsPath.Replace("Module_PictureDetails.aspx", depthTemplate));
                    if (string.IsNullOrEmpty(str10))
                    {
                        string str15 = str2;
                        str2 = str15 + "《" + row["ColumnName"].ToString() + "》模板" + depthTemplate + "不存在或为空";
                        continue;
                    }
                    nCopyRight = this.SetCopyRight(ref str10);
                    if (nCopyRight > 0)
                    {
                        return this.CopyRightError(nCopyRight);
                    }
                }
                string strClmndepth = row["depth"].ToString();
                string str5 = row["id"].ToString();
                this.GetCustumPara(templateContent, "'" + str5 + "'", "5");
                this.GetOrderby(out str11, out str12, row["ListOrder"].ToString());
                DataTable dtDtls = this.Bll1.GetDataTable(string.Format(format, str5, str12, str11));
                string[,] strArray = null;
                DataTable dtPList = this.GetColumnCusFData(dataTable, str5, ref strArray, "5");
                int nNumber = 0;
                foreach (DataRow row2 in dtDtls.Rows)
                {
                    if (num2 < nCreateNumber)
                    {
                        num2++;
                    }
                    else
                    {
                        if (num2 >= (nCreateNumber + num))
                        {
                            object obj2 = str2;
                            str2 = string.Concat(new object[] { obj2, "<br>此次分段生成", num, "个文件！" });
                            string str16 = str2;
                            string[] strArray3 = new string[] { str16, "<script language=javascript>window.location.href=('", strBackFile, ".aspx?Mdl=MD&LastNumber=", (nCreateNumber + num).ToString(), "')</script>" };
                            return string.Concat(strArray3);
                        }
                        num2++;
                        string strTemp = (str10 == null) ? templateContent : str10;
                        this.GetSiteInfoContents(ref strTemp, strPageDepth, str5, strClmndepth, row2);
                        this.ReplaceNav(ref strTemp, strPageDepth);
                        if (strTemp.IndexOf("[QH:Banner]") >= 0)
                        {
                            this.ReplaceBanner(ref strTemp, strPageDepth, str5);
                        }
                        this.ReplaceLoop(ref strTemp, strPageDepth, str5);
                        this.ReplaceTitle(ref strTemp, strPageDepth, str5);
                        this.ReplaceProductListTitle(ref strTemp, strPageDepth, str5);
                        this.ReplaceLanmuLoopBig(ref strTemp, strPageDepth, str5);
                        this.ReplaceNavLoopBig(ref strTemp, strPageDepth, str5);
                        this.ReplaceFenyeTitle(ref strTemp, row, strPageDepth);
                        this.ReplaceBackList(ref strTemp, row, strPageDepth);
                        this.ReplaceCommen(ref strTemp, row, strPageDepth);
                        this.ReplaceDetailPicture(ref strTemp, row2, strPageDepth, "5");
                        this.ReplaceImageDetails(ref strTemp, row2, strPageDepth);
                        this.ReplaceCusField(ref strTemp, row2, dtPList, strArray, "5");
                        this.ReplaceImagePicture(ref strTemp, row2, strPageDepth);
                        this.ReplaceDetailsPager(ref strTemp, dtDtls, nNumber, "PictureDetails_");
                        this.ReplaceStatisticsJS(ref strTemp, strPageDepth);
                        if (strTemp.IndexOf("[QH:BaiduMap") >= 0)
                        {
                            this.ReplaceBaiduMap(ref strTemp, strPageDepth);
                        }
                        if (strTemp.IndexOf("[QH:SearchOnclick") >= 0)
                        {
                            this.ReplaceSearch(ref strTemp, strPageDepth, "News");
                        }
                        if (strTemp.IndexOf("[QH:Advertise") >= 0)
                        {
                            this.ReplaceAdvertise(ref strTemp, strPageDepth);
                        }
                        if ((strTemp.Contains("[QH:LangLink") || strTemp.Contains("[QH:LangVersion")) || strTemp.Contains("[QH:LangImg"))
                        {
                            this.ReplaceMultiLang(ref strTemp, "", strPageDepth);
                        }
                        if (strTemp.IndexOf("[QH:GuestBook ") >= 0)
                        {
                            this.ReplaceMwssage(ref strTemp, strPageDepth, row2["Title"].ToString());
                        }
                        string str13 = "PictureDetails_" + row2["id"] + ".html";
                        str2 = str2 + this.CreateStaticFile(str6 + "/" + str13, strTemp, row["ColumnName"].ToString() + "详细内容");
                        nNumber++;
                    }
                }
            }
            return str2;
        }

        public string CreateImageDetailsOne(string strAbsPath, string strID, string strColumnID, ref string strOKInfo, bool bDelCreate, bool bOneCreate, string strRootDir)
        {
            string str5;
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                strOKInfo = "图片内容页模板不存在或为空";
                return "图片内容页模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight, ref strOKInfo);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='5'");
            if (rowArray.Length == 0)
            {
                return "没有属于图片模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            string format = "select id,Title,ImageUrl,ThumbUrl,displayImg,BriefIntro,Content,AddDate,ModyDate,ctitle,KeyW,Description,Author,Tags,hits from QH_Img where ColumnID='{0}' order by TopShow asc,{1} {2}";
            if (!this.GetDetailsUrlPath(out str5, "PictureDetails", strRootDir))
            {
                strOKInfo = "生成失败！";
                return "无法创建目录。";
            }
            DataTable dataTable = this.Bll1.GetDataTable("select id,name,no_order,wr_ok,columnID from QH_Parameter where [Module]='5' order by CLNG(columnID) asc,CLNG(no_order) asc");
            string strPageDepth = "0";
            this.SetImagePicLinkTM(strPageDepth);
            foreach (DataRow row in rowArray)
            {
                string str9 = null;
                string depthTemplate = this.GetDepthTemplate(row, "DetailTemplate");
                if ((depthTemplate != string.Empty) && !this.bIsMobile)
                {
                    str9 = this.GetTemplateContent(strAbsPath.Replace("Module_PictureDetails.aspx", depthTemplate));
                    if (string.IsNullOrEmpty(str9))
                    {
                        string str17 = str2;
                        str2 = str17 + "《" + row["ColumnName"].ToString() + "》模板" + depthTemplate + "不存在或为空";
                        continue;
                    }
                    nCopyRight = this.SetCopyRight(ref str9);
                    if (nCopyRight > 0)
                    {
                        return this.CopyRightError(nCopyRight, ref strOKInfo);
                    }
                }
                string strClmndepth = row["depth"].ToString();
                if (strColumnID == row["id"].ToString())
                {
                    string str10;
                    string str11;
                    this.GetCustumPara(templateContent, "'" + strColumnID + "'", "5");
                    this.GetOrderby(out str10, out str11, row["ListOrder"].ToString());
                    DataTable dtCtnt = this.Bll1.GetDataTable(string.Format(format, strColumnID, str11, str10));
                    string str12 = "";
                    string str13 = "";
                    if (!bOneCreate)
                    {
                        this.GetInfluenceID(strID, ref str12, ref str13, dtCtnt);
                    }
                    string[,] strArray = null;
                    DataTable dtPList = this.GetColumnCusFData(dataTable, strColumnID, ref strArray, "5");
                    if (bDelCreate)
                    {
                        dtCtnt = this.GetNoDelTable(dtCtnt, strID);
                    }
                    int nNumber = 0;
                    foreach (DataRow row2 in dtCtnt.Rows)
                    {
                        string str6;
                        string str14 = row2["id"].ToString();
                        if (bDelCreate)
                        {
                            if ((str12 == str14) || (str13 == str14))
                            {
                                goto Label_02D6;
                            }
                            nNumber++;
                            continue;
                        }
                        if ((!(str12 == str14) && !(str13 == str14)) && !(str14 == strID))
                        {
                            nNumber++;
                            continue;
                        }
                    Label_02D6:
                        str6 = (str9 == null) ? templateContent : str9;
                        this.GetSiteInfoContents(ref str6, strPageDepth, strColumnID, strClmndepth, row2);
                        this.ReplaceNav(ref str6, strPageDepth);
                        if (str6.IndexOf("[QH:Banner]") >= 0)
                        {
                            this.ReplaceBanner(ref str6, strPageDepth, strColumnID);
                        }
                        this.ReplaceLoop(ref str6, strPageDepth, strColumnID);
                        this.ReplaceTitle(ref str6, strPageDepth, strColumnID);
                        this.ReplaceProductListTitle(ref str6, strPageDepth, strColumnID);
                        this.ReplaceNavLoopBig(ref str6, strPageDepth, strColumnID);
                        this.ReplaceLanmuLoopBig(ref str6, strPageDepth, strColumnID);
                        this.ReplaceFenyeTitle(ref str6, row, strPageDepth);
                        this.ReplaceBackList(ref str6, row, strPageDepth);
                        this.ReplaceCommen(ref str6, row, strPageDepth);
                        this.ReplaceDetailPicture(ref str6, row2, strPageDepth, "5");
                        this.ReplaceImageDetails(ref str6, row2, strPageDepth);
                        this.ReplaceCusField(ref str6, row2, dtPList, strArray, "5");
                        this.ReplaceImagePicture(ref str6, row2, strPageDepth);
                        this.ReplaceDetailsPager(ref str6, dtCtnt, nNumber, "PictureDetails_");
                        this.ReplaceStatisticsJS(ref str6, strPageDepth);
                        if (str6.IndexOf("[QH:BaiduMap") >= 0)
                        {
                            this.ReplaceBaiduMap(ref str6, strPageDepth);
                        }
                        if (str6.IndexOf("[QH:SearchOnclick") >= 0)
                        {
                            this.ReplaceSearch(ref str6, strPageDepth, "News");
                        }
                        if (str6.IndexOf("[QH:Advertise") >= 0)
                        {
                            this.ReplaceAdvertise(ref str6, strPageDepth);
                        }
                        if ((str6.Contains("[QH:LangLink") || str6.Contains("[QH:LangVersion")) || str6.Contains("[QH:LangImg"))
                        {
                            this.ReplaceMultiLang(ref str6, "", strPageDepth);
                        }
                        if (str6.IndexOf("[QH:GuestBook ") >= 0)
                        {
                            this.ReplaceMwssage(ref str6, strPageDepth, row2["Title"].ToString());
                        }
                        string str15 = "PictureDetails_" + row2["id"] + ".html";
                        str2 = str2 + this.CreateStaticFile(str5 + "/" + str15, str6, row["ColumnName"].ToString() + "详细内容");
                        nNumber++;
                    }
                }
            }
            return str2;
        }

        public string CreateJianJie(string strAbsPath, string strTPath, string strRootDir, ArrayList alistID)
        {
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "简介模块模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='1'");
            if (rowArray.Length == 0)
            {
                return "没有属于简介模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            foreach (DataRow row in rowArray)
            {
                string str3;
                string str5;
                string str6;
                if (row["IsShow"].ToString() == "0")
                {
                    continue;
                }
                string strColumnID = row["id"].ToString();
                if ((alistID != null) && !this.IsIDInArrayList(strColumnID, alistID))
                {
                    continue;
                }
                if (row["outLink"].ToString().Trim() != string.Empty)
                {
                    string str10 = str2;
                    str2 = str10 + "栏目《" + row["ColumnName"].ToString() + "》为外部链接：" + row["outLink"].ToString().Trim() + "！";
                    continue;
                }
                string newValue = row["TemplateName"].ToString().Trim();
                if ((newValue != string.Empty) && !this.bIsMobile)
                {
                    str6 = this.GetTemplateContent(strAbsPath.Replace("Module_JianJie.aspx", newValue));
                    if (!string.IsNullOrEmpty(str6))
                    {
                        nCopyRight = this.SetCopyRight(ref str6);
                        if (nCopyRight > 0)
                        {
                            return this.CopyRightError(nCopyRight);
                        }
                        goto Label_0220;
                    }
                    string str11 = str2;
                    str2 = str11 + "《" + row["ColumnName"].ToString() + @"》模板template\" + strTPath + @"\" + newValue + "不存在或为空";
                    continue;
                }
                str6 = templateContent;
            Label_0220:
                str3 = row["depth"].ToString();
                this.GetSiteInfoContents(ref str6, str3, strColumnID);
                this.ReplaceNav(ref str6, str3);
                if (str6.IndexOf("[QH:Banner]") >= 0)
                {
                    this.ReplaceBanner(ref str6, str3, strColumnID);
                }
                this.ReplaceLoop(ref str6, str3, strColumnID);
                this.ReplaceTitle(ref str6, str3, strColumnID);
                this.ReplaceProductListTitle(ref str6, str3, strColumnID);
                this.ReplaceNavLoopBig(ref str6, str3, strColumnID);
                this.ReplaceLanmuLoopBig(ref str6, str3, strColumnID);
                this.ReplaceFenyeTitle(ref str6, row, str3);
                this.ReplaceCommen(ref str6, row, str3);
                this.ReplaceStatisticsJS(ref str6, str3);
                if (str6.IndexOf("[QH:BaiduMap") >= 0)
                {
                    this.ReplaceBaiduMap(ref str6, str3);
                }
                if (str6.IndexOf("[QH:SearchOnclick") >= 0)
                {
                    this.ReplaceSearch(ref str6, str3, "News");
                }
                if (str6.IndexOf("[QH:Advertise") >= 0)
                {
                    this.ReplaceAdvertise(ref str6, str3);
                }
                if ((str6.Contains("[QH:LangLink") || str6.Contains("[QH:LangVersion")) || str6.Contains("[QH:LangImg"))
                {
                    this.ReplaceMultiLang(ref str6, "", str3);
                }
                if (str6.IndexOf("[QH:GuestBook ") >= 0)
                {
                    this.ReplaceMwssage(ref str6, str3, row["ColumnName"].ToString());
                }
                string createdFileName = this.GetCreatedFileName(row["fileName"].ToString().Trim());
                if (!this.GetStaticFUrlPath(out str5, row, this.dTableClmn, strRootDir))
                {
                    return "无法创建目录。";
                }
                str2 = str2 + this.CreateStaticFile(str5 + "/" + createdFileName, str6, row["ColumnName"].ToString());
            }
            return str2;
        }

        private string CreateListPager(string strTemp1, string[] astrContent, string strSFileName, string strUrlPath, string strColumnName, string strListLoop, string strPubu, string[] astrHitsIdHdn)
        {
            string str3;
            string str4;
            string str5;
            string str6;
            string str7;
            string str8;
            if (strTemp1.IndexOf("[QH:PageLoop]") > 0)
            {
                strTemp1 = Regex.Replace(strTemp1, @"\[QH:Pager:?\w*\]", "");
                return this.CreateListPagerCustom(strTemp1, astrContent, strSFileName, strUrlPath, strColumnName, strListLoop, strPubu, astrHitsIdHdn);
            }
            int index = strTemp1.IndexOf("[QH:Pager");
            if (index == -1)
            {
                strTemp1 = strTemp1.Replace(strListLoop, astrContent[0]);
                string str = (astrContent.Length > 1) ? ("由于模板中没有分页标签[QH:Pager],未能生成《" + strColumnName + "》栏目页分页！") : "";
                return (this.CreateStaticFile(strUrlPath + "/" + strSFileName, strTemp1, strColumnName) + str);
            }
            int num2 = strTemp1.IndexOf(']', index);
            string oldValue = str3 = strTemp1.Substring(index, (num2 - index) + 1);
            str3 = str3.Replace("[QH:Pager", "").Replace(":", "").Replace("]", "").Trim();
            if (str3 == "2")
            {
                str4 = "<a href='{0}.html' class=\"CDQHPage2Pre\">上一页</a>\n";
                str5 = "<a href='{0}.html' class=\"CDQHPage2Next\">下一页</a>\n";
                str6 = "<a href='{0}.html'>{1}</a>\n";
                str7 = "<span class=\"CDQHPage2Now\">{0}</span>\n";
                str8 = "upUrlNumberdownUrl";
            }
            else
            {
                str4 = "<a href='{0}.html'>上一页</a>";
                str5 = "<a href='{0}.html'>下一页</a>";
                str6 = " [<a href='{0}.html'>{1}</a>] ";
                str7 = " [<font color='#cb1c22'><b>{0}</b></font>] ";
                str8 = "upUrl&nbsp;Number&nbsp;downUrl&nbsp;&nbsp;";
            }
            string str9 = "";
            int length = astrContent.Length;
            string newValue = strSFileName.Substring(0, strSFileName.IndexOf('.'));
            try
            {
                for (int i = 0; i < astrContent.Length; i++)
                {
                    string str11 = str8;
                    if ((strListLoop == "[QH:HrDemand]") && (str3 != "2"))
                    {
                        str11 = "<div  style=\"text-align:right;margin-right:40px;\"><table><tr ><td>upUrl</td><td>Number</td><td>downUrl</td></tr></table></div>";
                    }
                    string text1 = strUrlPath + "/";
                    string str12 = "";
                    string strTemp = strTemp1;
                    string str14 = "";
                    for (int j = 1; j <= length; j++)
                    {
                        if (j == 1)
                        {
                            if (i == 0)
                            {
                                str14 = str14 + str7.Replace("{0}", j.ToString());
                            }
                            else
                            {
                                str14 = str14 + str6.Replace("{0}", newValue).Replace("{1}", j.ToString());
                            }
                            if ((length > 14) && (i > 10))
                            {
                                str14 = str14 + "<span>...</span>";
                            }
                            continue;
                        }
                        if ((length > 14) && (j != length))
                        {
                            if (i <= 10)
                            {
                                if (j <= 12)
                                {
                                    goto Label_0263;
                                }
                                continue;
                            }
                            if (i >= (length - 10))
                            {
                                if (j >= (length - 12))
                                {
                                    goto Label_0263;
                                }
                                continue;
                            }
                            if ((j < (i - 4)) || (j > (i + 5)))
                            {
                                continue;
                            }
                        }
                    Label_0263:
                        if (((j == length) && (length > 14)) && ((i < (length - 10)) || (i <= 10)))
                        {
                            str14 = str14 + "<span>...</span>";
                        }
                        int num6 = j - 1;
                        if (i == num6)
                        {
                            str14 = str14 + str7.Replace("{0}", j.ToString());
                        }
                        else
                        {
                            str14 = str14 + str6.Replace("{0}", newValue + "_" + num6).Replace("{1}", j.ToString());
                        }
                    }
                    if (length != 0)
                    {
                        if (i == 0)
                        {
                            str12 = newValue + ".html";
                        }
                        else
                        {
                            str12 = string.Concat(new object[] { newValue, "_", i, ".html" });
                        }
                        if (i == 0)
                        {
                            str11 = str11.Replace("upUrl", "");
                        }
                        if (i <= 1)
                        {
                            str11 = str11.Replace("upUrl", str4.Replace("{0}", newValue));
                        }
                        else
                        {
                            int num7 = i - 1;
                            str11 = str11.Replace("upUrl", str4.Replace("{0}", newValue + "_" + num7));
                        }
                        if (this.bIsMobile)
                        {
                            str11 = str11.Replace("Number", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                        }
                        if (length == 1)
                        {
                            str11 = str11.Replace("Number", "");
                        }
                        else
                        {
                            str11 = str11.Replace("Number", str14);
                        }
                        if (i == (length - 1))
                        {
                            str11 = str11.Replace("downUrl", "");
                        }
                        if (i != (length - 1))
                        {
                            int num8 = i + 1;
                            str11 = str11.Replace("downUrl", str5.Replace("{0}", newValue + "_" + num8));
                        }
                        else
                        {
                            int num9 = length - 1;
                            str11 = str11.Replace("downUrl", str5.Replace("{0}", newValue + "_" + num9));
                        }
                        if (strListLoop == "[QH:HrDemand]")
                        {
                            strTemp = strTemp.Replace(strListLoop, astrContent[i]).Replace(oldValue, str11);
                        }
                        else
                        {
                            strTemp = strTemp.Replace(strListLoop, astrContent[i]).Replace(oldValue, str11);
                            if (astrHitsIdHdn != null)
                            {
                                this.InsertHiddenBottom(ref strTemp, "HdnHitsID", astrHitsIdHdn[i]);
                            }
                        }
                        strTemp = strTemp.Replace("[QH:CurrentPage]", (i + 1).ToString());
                    }
                    if (strPubu == "Pubu")
                    {
                        this.InsertHiddenBottom(ref strTemp, "HdnPage", i.ToString());
                    }
                    str9 = str9 + this.CreateStaticFile(strUrlPath + "/" + str12, strTemp, strColumnName);
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("生成列表页及分页文件错误： " + exception.ToString());
            }
            return str9;
        }

        private string CreateListPagerCustom(string strTemp1, string[] astrContent, string strSFileName, string strUrlPath, string strColumnName, string strListLoop, string strPubu, string[] astrHitsIdHdn)
        {
            string input = Regex.Match(strTemp1, @"\[QH:PageLoop\][\s\S]*?\[\/QH:PageLoop\]").ToString();
            Match match = Regex.Match(input, @"\[QH:PageNow\]([\s\S]*?)\[\/QH:PageNow\]");
            Match match2 = Regex.Match(input, @"\[QH:PageNotNow\]([\s\S]*?)\[\/QH:PageNotNow\]");
            string str2 = match.Groups[1].Value;
            string str3 = match2.Groups[1].Value;
            str2 = (str2 == "") ? " " : str2;
            str3 = (str3 == "") ? " " : str3;
            Match match3 = Regex.Match(strTemp1, @"<a[^>]+\[QH:PageFirstLink\][^>]*>([\s\S]*?)</a>", RegexOptions.IgnoreCase);
            Match match4 = Regex.Match(strTemp1, @"<a[^>]+\[QH:PagePrevLink\][^>]*>([\s\S]*?)</a>", RegexOptions.IgnoreCase);
            Match match5 = Regex.Match(strTemp1, @"<a[^>]+\[QH:PageNextLink\][^>]*>([\s\S]*?)</a>", RegexOptions.IgnoreCase);
            Match match6 = Regex.Match(strTemp1, @"<a[^>]+\[QH:PageLastLink\][^>]*>([\s\S]*?)</a>", RegexOptions.IgnoreCase);
            string oldValue = match3.ToString();
            string str5 = match4.ToString();
            string str6 = match5.ToString();
            string str7 = match6.ToString();
            string newValue = match3.Groups[1].Value;
            string str9 = match4.Groups[1].Value;
            string str10 = match5.Groups[1].Value;
            string str11 = match6.Groups[1].Value;
            string str12 = str5.Replace("[QH:PagePrevLink]", "{0}.html");
            string str13 = str6.Replace("[QH:PageNextLink]", "{0}.html");
            string str14 = str3.Replace("[QH:PageLink]", "{0}.html").Replace("[QH:PageNumber]", "{1}");
            string str15 = str2.Replace("[QH:PageNumber]", "{0}");
            string str16 = oldValue.Replace("[QH:PageFirstLink]", "{0}.html");
            string str17 = str7.Replace("[QH:PageLastLink]", "{0}.html");
            string str18 = "Number";
            Match match7 = Regex.Match(strTemp1, @"\[QH:PageJump:\s*(\w+?)\s*\]", RegexOptions.IgnoreCase);
            string str19 = match7.ToString();
            string str20 = match7.Groups[1].Value;
            if ((str19 != "") && (str20 != ""))
            {
                string str21 = string.Concat(new object[] { "var vPNum=eval('", str20, @"').value.replace( /(^\s*)|(\s*$)/g,'');var vTotle=", astrContent.Length, @";if(!/^\d+$/.test(vPNum)){alert('请输入页码数字！');return;} var n=parseInt(vPNum);var vNum=n>vTotle?vTotle-1:n-1;vNum=vNum<0?0:vNum; var reg=/\/(\w+?)_?\d*?.html/i;var arr=reg.exec(document.URL);var vName=arr?arr[1]:'index' ;var vUrl=vNum==0?vName+'.html':vName+'_'+vNum+'.html'; location=vUrl;" });
                strTemp1 = strTemp1.Replace(str19, str21);
            }
            Match match8 = Regex.Match(strTemp1, @"<select[^>]+(\[QH:SelPage:?(\d*)\])[^>]*>([\s\S]*?)</select>", RegexOptions.IgnoreCase);
            string str22 = match8.ToString();
            string format = (match8.Groups[2].Value == "2") ? "\n<option {0} >第{1}页</option>" : "\n<option {0} >{1}</option>";
            string str24 = @"vNum=this.selectedIndex; var reg=/\/(\w+?)_?\d*?.html/i;var arr=reg.exec(document.URL);var vName=arr?arr[1]:'index' ;var vUrl=vNum==0?vName+'.html':vName+'_'+vNum+'.html'; location=vUrl;";
            string str25 = "";
            int length = astrContent.Length;
            string str26 = strSFileName.Substring(0, strSFileName.IndexOf('.'));
            try
            {
                for (int i = 0; i < astrContent.Length; i++)
                {
                    string str27 = str18;
                    string text1 = strUrlPath + "/";
                    string str28 = "";
                    string strTemp = strTemp1;
                    string str30 = "";
                    string str31 = "";
                    for (int j = 1; j <= length; j++)
                    {
                        if (str22 != "")
                        {
                            if (i == (j - 1))
                            {
                                str31 = str31 + string.Format(format, "selected", j.ToString());
                            }
                            else
                            {
                                str31 = str31 + string.Format(format, "", j.ToString());
                            }
                        }
                        if (j == 1)
                        {
                            if (i == 0)
                            {
                                str30 = str30 + str15.Replace("{0}", j.ToString());
                            }
                            else
                            {
                                str30 = str30 + str14.Replace("{0}", str26).Replace("{1}", j.ToString());
                            }
                            if ((length > 14) && (i > 10))
                            {
                                str30 = str30 + "<span>...</span>";
                            }
                            continue;
                        }
                        if ((length > 14) && (j != length))
                        {
                            if (i <= 10)
                            {
                                if (j <= 12)
                                {
                                    goto Label_03EF;
                                }
                                continue;
                            }
                            if (i >= (length - 10))
                            {
                                if (j >= (length - 12))
                                {
                                    goto Label_03EF;
                                }
                                continue;
                            }
                            if ((j < (i - 4)) || (j > (i + 5)))
                            {
                                continue;
                            }
                        }
                    Label_03EF:
                        if (((j == length) && (length > 14)) && ((i < (length - 10)) || (i <= 10)))
                        {
                            str30 = str30 + "<span>...</span>";
                        }
                        int num4 = j - 1;
                        if (i == num4)
                        {
                            str30 = str30 + str15.Replace("{0}", j.ToString());
                        }
                        else
                        {
                            str30 = str30 + str14.Replace("{0}", str26 + "_" + num4).Replace("{1}", j.ToString());
                        }
                    }
                    if (str22 != "")
                    {
                        strTemp = strTemp.Insert(match8.Groups[3].Index, str31).Replace(match8.Groups[1].Value, str24);
                    }
                    if (length != 0)
                    {
                        if (i == 0)
                        {
                            str28 = str26 + ".html";
                        }
                        else
                        {
                            str28 = string.Concat(new object[] { str26, "_", i, ".html" });
                        }
                        if (i == 0)
                        {
                            if (oldValue != "")
                            {
                                strTemp = strTemp.Replace(oldValue, newValue);
                            }
                            if (str5 != "")
                            {
                                strTemp = strTemp.Replace(str5, str9);
                            }
                        }
                        if (i <= 1)
                        {
                            if (oldValue != "")
                            {
                                strTemp = strTemp.Replace(oldValue, str16.Replace("{0}", str26));
                            }
                            if (str5 != "")
                            {
                                strTemp = strTemp.Replace(str5, str12.Replace("{0}", str26));
                            }
                        }
                        else
                        {
                            int num5 = i - 1;
                            if (oldValue != "")
                            {
                                strTemp = strTemp.Replace(oldValue, str16.Replace("{0}", str26));
                            }
                            if (str5 != "")
                            {
                                strTemp = strTemp.Replace(str5, str12.Replace("{0}", str26 + "_" + num5));
                            }
                        }
                        if (this.bIsMobile)
                        {
                            str27 = str27.Replace("Number", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                        }
                        if (length == 1)
                        {
                            str27 = str27.Replace("Number", str15.Replace("{0}", "1"));
                        }
                        else
                        {
                            str27 = str27.Replace("Number", str30);
                        }
                        if (i == (length - 1))
                        {
                            if (str6 != "")
                            {
                                strTemp = strTemp.Replace(str6, str10);
                            }
                            if (str7 != "")
                            {
                                strTemp = strTemp.Replace(str7, str11);
                            }
                        }
                        if (i != (length - 1))
                        {
                            int num6 = i + 1;
                            if (str6 != "")
                            {
                                strTemp = strTemp.Replace(str6, str13.Replace("{0}", str26 + "_" + num6));
                            }
                            if (str7 != "")
                            {
                                strTemp = strTemp.Replace(str7, str17.Replace("{0}", str26 + "_" + (length - 1)));
                            }
                        }
                        else
                        {
                            int num7 = length - 1;
                            if (str6 != "")
                            {
                                strTemp = strTemp.Replace(str6, str13.Replace("{0}", str26 + "_" + num7));
                            }
                            if (str7 != "")
                            {
                                strTemp = strTemp.Replace(str7, str17.Replace("{0}", str26 + "_" + (length - 1)));
                            }
                        }
                        if (strListLoop == "[QH:HrDemand]")
                        {
                            int num8 = i + 1;
                            strTemp = strTemp.Replace(strListLoop, astrContent[i]).Replace(input, str27).Replace("[QH:CurrentPage]", num8.ToString());
                        }
                        else
                        {
                            strTemp = strTemp.Replace(strListLoop, astrContent[i]).Replace(input, str27);
                            if (astrHitsIdHdn != null)
                            {
                                this.InsertHiddenBottom(ref strTemp, "HdnHitsID", astrHitsIdHdn[i]);
                            }
                            strTemp = strTemp.Replace("[QH:CurrentPage]", (i + 1).ToString());
                        }
                    }
                    if (strPubu == "Pubu")
                    {
                        this.InsertHiddenBottom(ref strTemp, "HdnPage", i.ToString());
                    }
                    str25 = str25 + this.CreateStaticFile(strUrlPath + "/" + str28, strTemp, strColumnName);
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("生成列表页及分页文件错误： " + exception.ToString());
            }
            return str25;
        }

        public string CreateMap(string strAbsPath, string strRootDir)
        {
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "地图模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            this.ReplaceHomeLabeling(ref templateContent);
            if (templateContent.IndexOf("[QH:BaiduMap") >= 0)
            {
                this.ReplaceBaiduMap(ref templateContent, "S");
            }
            if (templateContent.IndexOf("[QH:GuestBook ") >= 0)
            {
                this.ReplaceMwssage(ref templateContent, "S", "地图页");
            }
            strRootDir = (strRootDir == "") ? "" : (strRootDir + "/");
            if (strRootDir != "")
            {
                this.CheckRootDir(strRootDir);
            }
            return this.CreateStaticFile(strRootDir + "Map.html", templateContent, "地图页");
        }

        public string CreateMessage(string strAbsPath, string strTPath, string strRootDir)
        {
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "留言模块模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            switch (nCopyRight)
            {
                case 1:
                    return "<BR>由于不包含授权链接标贴，不能生成静态页面。！<BR>请在模板里加上标贴[QH:CDQHLink]，或申请授权！";

                case 2:
                    return "<BR>标贴[QH:CDQHLink]必须在<body>中，或申请授权！";

                case 3:
                    return "<BR>不能注释掉标贴[QH:CDQHLink]！，或申请授权！";
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='6'");
            if (rowArray.Length == 0)
            {
                return "没有属于留言模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            foreach (DataRow row in rowArray)
            {
                string str3;
                string str5;
                string str6;
                if (row["outLink"].ToString().Trim() != string.Empty)
                {
                    string str10 = str2;
                    str2 = str10 + "栏目《" + row["ColumnName"].ToString() + "》为外部链接：" + row["outLink"].ToString().Trim() + "！";
                    continue;
                }
                string newValue = row["TemplateName"].ToString().Trim();
                if ((newValue != string.Empty) && !this.bIsMobile)
                {
                    str6 = this.GetTemplateContent(strAbsPath.Replace("Module_Message.aspx", newValue));
                    if (!string.IsNullOrEmpty(str6))
                    {
                        nCopyRight = this.SetCopyRight(ref str6);
                        if (nCopyRight > 0)
                        {
                            return this.CopyRightError(nCopyRight);
                        }
                        goto Label_01EC;
                    }
                    string str11 = str2;
                    str2 = str11 + "《" + row["ColumnName"].ToString() + @"》模板template\" + strTPath + @"\" + newValue + "不存在或为空";
                    continue;
                }
                str6 = templateContent;
            Label_01EC:
                str3 = row["depth"].ToString();
                string strColumnID = row["id"].ToString();
                this.GetSiteInfoContents(ref str6, str3, strColumnID);
                bool flag = false;
                if (str6.IndexOf("[QH:GuestBook ") >= 0)
                {
                    this.ReplaceMwssage(ref str6, str3, row["ColumnName"].ToString());
                    flag = true;
                }
                if (!flag)
                {
                    string[] astrMsgSet = this.Bll1.DAL1.ReadDataReaderStringArray("select top 1 RepChar,NameFill,TelFill,EmailFill,MobileFill,ContactFill,TitlelFill,CheckCode,ContentFill,DspName,DspTel,DspEmail,DspMobile,DspContact,DspTitle,DspContent,NameName,TelName,EmailName,MobileName,ContactName,TitleName,ContentName from QH_MsgSet", 0x17);
                    str6 = str6.Replace("[QH:GuestBook]", this.GetGuestBookUI(str3, astrMsgSet));
                    this.SetJS(ref str6, this.GetGuestBookJsScript(str3, astrMsgSet));
                }
                this.ReplaceNav(ref str6, str3);
                if (str6.IndexOf("[QH:Banner]") >= 0)
                {
                    this.ReplaceBanner(ref str6, str3, strColumnID);
                }
                this.ReplaceLoop(ref str6, str3, strColumnID);
                this.ReplaceTitle(ref str6, str3, strColumnID);
                this.ReplaceProductListTitle(ref str6, str3, strColumnID);
                this.ReplaceNavLoopBig(ref str6, str3, strColumnID);
                this.ReplaceLanmuLoopBig(ref str6, str3, strColumnID);
                this.ReplaceFenyeTitle(ref str6, row, str3);
                this.ReplaceCommen(ref str6, row, str3);
                this.ReplaceStatisticsJS(ref str6, str3);
                if (str6.IndexOf("[QH:BaiduMap") >= 0)
                {
                    this.ReplaceBaiduMap(ref str6, str3);
                }
                if (str6.IndexOf("[QH:SearchOnclick") >= 0)
                {
                    this.ReplaceSearch(ref str6, str3, "News");
                }
                if (str6.IndexOf("[QH:Advertise") >= 0)
                {
                    this.ReplaceAdvertise(ref str6, str3);
                }
                if ((str6.Contains("[QH:LangLink") || str6.Contains("[QH:LangVersion")) || str6.Contains("[QH:LangImg"))
                {
                    this.ReplaceMultiLang(ref str6, "", str3);
                }
                string createdFileName = this.GetCreatedFileName(row["fileName"].ToString().Trim());
                if (!this.GetStaticFUrlPath(out str5, row, this.dTableClmn, strRootDir))
                {
                    return "无法创建目录。";
                }
                str2 = str2 + this.CreateStaticFile(str5 + "/" + createdFileName, str6, row["ColumnName"].ToString());
            }
            return str2;
        }

        public string CreateNewsCenter(string strAbsPath, string strTPath, string strRootDir, ArrayList alistID)
        {
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "新闻模块模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='2'");
            if (rowArray.Length == 0)
            {
                return "没有属于新闻模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            foreach (DataRow row in rowArray)
            {
                string str3;
                string str5;
                string str6;
                string str9;
                string[] strArray;
                if (row["IsShow"].ToString() == "0")
                {
                    continue;
                }
                string strColumnID = row["id"].ToString();
                if ((alistID != null) && !this.IsIDInArrayList(strColumnID, alistID))
                {
                    continue;
                }
                if (row["outLink"].ToString().Trim() != string.Empty)
                {
                    string str12 = str2;
                    str2 = str12 + "栏目《" + row["ColumnName"].ToString() + "》为外部链接：" + row["outLink"].ToString().Trim() + "！";
                    continue;
                }
                string newValue = row["TemplateName"].ToString().Trim();
                if ((newValue != string.Empty) && !this.bIsMobile)
                {
                    str6 = this.GetTemplateContent(strAbsPath.Replace("Module_News.aspx", newValue));
                    if (!string.IsNullOrEmpty(str6))
                    {
                        nCopyRight = this.SetCopyRight(ref str6);
                        if (nCopyRight > 0)
                        {
                            return this.CopyRightError(nCopyRight);
                        }
                        goto Label_0220;
                    }
                    string str13 = str2;
                    str2 = str13 + "《" + row["ColumnName"].ToString() + @"》模板template\" + strTPath + @"\" + newValue + "不存在或为空";
                    continue;
                }
                str6 = templateContent;
            Label_0220:
                str3 = row["depth"].ToString();
                this.GetSiteInfoContents(ref str6, str3, strColumnID);
                this.ReplaceNav(ref str6, str3);
                if (str6.IndexOf("[QH:Banner]") >= 0)
                {
                    this.ReplaceBanner(ref str6, str3, strColumnID);
                }
                this.ReplaceLoop(ref str6, str3, strColumnID);
                this.ReplaceTitle(ref str6, str3, strColumnID);
                this.ReplaceProductListTitle(ref str6, str3, strColumnID);
                this.ReplaceNavLoopBig(ref str6, str3, strColumnID);
                this.ReplaceLanmuLoopBig(ref str6, str3, strColumnID);
                this.ReplaceFenyeTitle(ref str6, row, str3);
                this.ReplaceCommen(ref str6, row, str3);
                this.ReplaceStatisticsJS(ref str6, str3);
                if (str6.IndexOf("[QH:BaiduMap") >= 0)
                {
                    this.ReplaceBaiduMap(ref str6, str3);
                }
                if (str6.IndexOf("[QH:SearchOnclick") >= 0)
                {
                    this.ReplaceSearch(ref str6, str3, "News");
                }
                if (str6.IndexOf("[QH:Advertise") >= 0)
                {
                    this.ReplaceAdvertise(ref str6, str3);
                }
                if ((str6.Contains("[QH:LangLink") || str6.Contains("[QH:LangVersion")) || str6.Contains("[QH:LangImg"))
                {
                    this.ReplaceMultiLang(ref str6, "", str3);
                }
                if (str6.IndexOf("[QH:GuestBook ") >= 0)
                {
                    this.ReplaceMwssage(ref str6, str3, row["ColumnName"].ToString());
                }
                if (!this.GetStaticFUrlPath(out str5, row, this.dTableClmn, strRootDir))
                {
                    return "无法创建目录。";
                }
                string createdFileName = this.GetCreatedFileName(row["fileName"].ToString().Trim());
                string[] astrContent = this.GetPageContent(ref str6, row, out str9, "", out strArray);
                if (astrContent == null)
                {
                    int index = str6.IndexOf("[QH:Pager");
                    if (index != -1)
                    {
                        int num3 = str6.IndexOf(']', index);
                        string oldValue = str6.Substring(index, (num3 - index) + 1);
                        str6 = str6.Replace(oldValue, "");
                    }
                    if (str6.IndexOf("[QH:PageLoop]") > 0)
                    {
                        str2 = str2 + this.ReplacePagerLoop(ref str6);
                    }
                    str2 = str2 + this.CreateStaticFile(str5 + "/" + createdFileName, str6, row["ColumnName"].ToString());
                }
                else
                {
                    str2 = str2 + this.CreateListPager(str6, astrContent, createdFileName, str5, row["ColumnName"].ToString(), str9, "", strArray);
                }
            }
            return str2;
        }

        public string CreateNewsDetails(string strAbsPath, int nCreateNumber, string strRootDir, string strBackFile)
        {
            string str6;
            int num = 30;
            int num2 = 0;
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "新闻内容页模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='2'");
            if (rowArray.Length == 0)
            {
                return "没有属于新闻模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            string format = "select id,Title,BriefIntro,Content,AddDate,ModyDate,ImageUrl,ImageLinkUrl,AltText,Author,ctitle,KeyW,Description,Tags,hits from QH_News where ColumnID='{0}' order by TopShow asc,{1} {2}";
            if (!this.GetDetailsUrlPath(out str6, "NewsDetails", strRootDir))
            {
                return "无法创建目录。";
            }
            foreach (DataRow row in rowArray)
            {
                string str11;
                string str12;
                string str10 = null;
                string depthTemplate = this.GetDepthTemplate(row, "DetailTemplate");
                if ((depthTemplate != string.Empty) && !this.bIsMobile)
                {
                    str10 = this.GetTemplateContent(strAbsPath.Replace("Module_NewsDetails.aspx", depthTemplate));
                    if (string.IsNullOrEmpty(str10))
                    {
                        string str15 = str2;
                        str2 = str15 + "《" + row["ColumnName"].ToString() + "》模板" + depthTemplate + "不存在或为空";
                        continue;
                    }
                    nCopyRight = this.SetCopyRight(ref str10);
                    if (nCopyRight > 0)
                    {
                        return this.CopyRightError(nCopyRight);
                    }
                }
                string strPageDepth = "0";
                string strClmndepth = row["depth"].ToString();
                string str5 = row["id"].ToString();
                this.GetOrderby(out str11, out str12, row["ListOrder"].ToString());
                DataTable dataTable = this.Bll1.GetDataTable(string.Format(format, str5, str12, str11));
                int nNumber = 0;
                foreach (DataRow row2 in dataTable.Rows)
                {
                    if (num2 < nCreateNumber)
                    {
                        num2++;
                    }
                    else
                    {
                        if (num2 >= (nCreateNumber + num))
                        {
                            object obj2 = str2;
                            str2 = string.Concat(new object[] { obj2, "<br>此次分段生成", num, "个文件！" });
                            string str16 = str2;
                            string[] strArray2 = new string[] { str16, "<script language=javascript>window.location.href=('", strBackFile, ".aspx?Mdl=ND&LastNumber=", (nCreateNumber + num).ToString(), "')</script>" };
                            return string.Concat(strArray2);
                        }
                        num2++;
                        string strTemp = (str10 == null) ? templateContent : str10;
                        this.GetSiteInfoContents(ref strTemp, strPageDepth, str5, strClmndepth, row2);
                        this.ReplaceNav(ref strTemp, strPageDepth);
                        if (strTemp.IndexOf("[QH:Banner]") >= 0)
                        {
                            this.ReplaceBanner(ref strTemp, strPageDepth, str5);
                        }
                        this.ReplaceLoop(ref strTemp, strPageDepth, str5);
                        this.ReplaceTitle(ref strTemp, strPageDepth, str5);
                        this.ReplaceProductListTitle(ref strTemp, strPageDepth, str5);
                        this.ReplaceNavLoopBig(ref strTemp, strPageDepth, str5);
                        this.ReplaceLanmuLoopBig(ref strTemp, strPageDepth, str5);
                        this.ReplaceFenyeTitle(ref strTemp, row, strPageDepth);
                        this.ReplaceBackList(ref strTemp, row, strPageDepth);
                        this.ReplaceCommen(ref strTemp, row, strPageDepth);
                        this.ReplaceNewsDetails(ref strTemp, row2, strPageDepth);
                        this.ReplaceDetailsPager(ref strTemp, dataTable, nNumber, "NewsDetails_");
                        this.ReplaceStatisticsJS(ref strTemp, strPageDepth);
                        if (strTemp.IndexOf("[QH:BaiduMap") >= 0)
                        {
                            this.ReplaceBaiduMap(ref strTemp, strPageDepth);
                        }
                        if (strTemp.IndexOf("[QH:SearchOnclick") >= 0)
                        {
                            this.ReplaceSearch(ref strTemp, strPageDepth, "News");
                        }
                        if (strTemp.IndexOf("[QH:Advertise") >= 0)
                        {
                            this.ReplaceAdvertise(ref strTemp, strPageDepth);
                        }
                        if ((strTemp.Contains("[QH:LangLink") || strTemp.Contains("[QH:LangVersion")) || strTemp.Contains("[QH:LangImg"))
                        {
                            this.ReplaceMultiLang(ref strTemp, "", strPageDepth);
                        }
                        if (strTemp.IndexOf("[QH:GuestBook ") >= 0)
                        {
                            this.ReplaceMwssage(ref strTemp, strPageDepth, row2["Title"].ToString());
                        }
                        string str13 = "NewsDetails_" + row2["id"] + ".html";
                        str2 = str2 + this.CreateStaticFile(str6 + "/" + str13, strTemp, row["ColumnName"].ToString() + "详细内容");
                        nNumber++;
                    }
                }
            }
            return str2;
        }

        public string CreateNewsDetailsOne(string strAbsPath, string strID, string strColumnID, ref string strOKInfo, bool bDelCreate, bool bOneCreate, string strRootDir)
        {
            string str5;
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                strOKInfo = "新闻内容页模板不存在或为空";
                return "新闻内容页模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight, ref strOKInfo);
            }
            DataRow[] rowArray = this.dTableClmn.Select("id=" + strColumnID);
            if (rowArray.Length == 0)
            {
                strOKInfo = "生成失败！";
                return "没有属于新闻模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            string format = "select id,Title,BriefIntro,Content,AddDate,ModyDate,ImageUrl,ImageLinkUrl,AltText,Author,ctitle,KeyW,Description,Tags,hits from QH_News where ColumnID='{0}' order by TopShow asc,{1} {2}";
            if (!this.GetDetailsUrlPath(out str5, "NewsDetails", strRootDir))
            {
                strOKInfo = "生成失败！";
                return "无法创建目录。";
            }
            foreach (DataRow row in rowArray)
            {
                string str9 = null;
                string depthTemplate = this.GetDepthTemplate(row, "DetailTemplate");
                if ((depthTemplate != string.Empty) && !this.bIsMobile)
                {
                    str9 = this.GetTemplateContent(strAbsPath.Replace("Module_NewsDetails.aspx", depthTemplate));
                    if (string.IsNullOrEmpty(str9))
                    {
                        string str17 = str2;
                        str2 = str17 + "《" + row["ColumnName"].ToString() + "》模板" + depthTemplate + "不存在或为空";
                        continue;
                    }
                    nCopyRight = this.SetCopyRight(ref str9);
                    if (nCopyRight > 0)
                    {
                        return this.CopyRightError(nCopyRight, ref strOKInfo);
                    }
                }
                string strPageDepth = "0";
                string strClmndepth = row["depth"].ToString();
                if (strColumnID == row["id"].ToString())
                {
                    string str10;
                    string str11;
                    this.GetOrderby(out str10, out str11, row["ListOrder"].ToString());
                    DataTable dataTable = this.Bll1.GetDataTable(string.Format(format, strColumnID, str11, str10));
                    string str12 = "";
                    string str13 = "";
                    if (!bOneCreate)
                    {
                        this.GetInfluenceID(strID, ref str12, ref str13, dataTable);
                    }
                    if (bDelCreate)
                    {
                        dataTable = this.GetNoDelTable(dataTable, strID);
                    }
                    int nNumber = 0;
                    foreach (DataRow row2 in dataTable.Rows)
                    {
                        string str6;
                        string str14 = row2["id"].ToString();
                        if (bDelCreate)
                        {
                            if ((str12 == str14) || (str13 == str14))
                            {
                                goto Label_0299;
                            }
                            nNumber++;
                            continue;
                        }
                        if ((!(str12 == str14) && !(str13 == str14)) && !(str14 == strID))
                        {
                            nNumber++;
                            continue;
                        }
                    Label_0299:
                        str6 = (str9 == null) ? templateContent : str9;
                        this.GetSiteInfoContents(ref str6, strPageDepth, strColumnID, strClmndepth, row2);
                        this.ReplaceNav(ref str6, strPageDepth);
                        if (str6.IndexOf("[QH:Banner]") >= 0)
                        {
                            this.ReplaceBanner(ref str6, strPageDepth, strColumnID);
                        }
                        this.ReplaceLoop(ref str6, strPageDepth, strColumnID);
                        this.ReplaceTitle(ref str6, strPageDepth, strColumnID);
                        this.ReplaceProductListTitle(ref str6, strPageDepth, strColumnID);
                        this.ReplaceNavLoopBig(ref str6, strPageDepth, strColumnID);
                        this.ReplaceLanmuLoopBig(ref str6, strPageDepth, strColumnID);
                        this.ReplaceFenyeTitle(ref str6, row, strPageDepth);
                        this.ReplaceBackList(ref str6, row, strPageDepth);
                        this.ReplaceCommen(ref str6, row, strPageDepth);
                        this.ReplaceNewsDetails(ref str6, row2, strPageDepth);
                        this.ReplaceDetailsPager(ref str6, dataTable, nNumber, "NewsDetails_");
                        this.ReplaceStatisticsJS(ref str6, strPageDepth);
                        if (str6.IndexOf("[QH:BaiduMap") >= 0)
                        {
                            this.ReplaceBaiduMap(ref str6, strPageDepth);
                        }
                        if (str6.IndexOf("[QH:SearchOnclick") >= 0)
                        {
                            this.ReplaceSearch(ref str6, strPageDepth, "News");
                        }
                        if (str6.IndexOf("[QH:Advertise") >= 0)
                        {
                            this.ReplaceAdvertise(ref str6, strPageDepth);
                        }
                        if ((str6.Contains("[QH:LangLink") || str6.Contains("[QH:LangVersion")) || str6.Contains("[QH:LangImg"))
                        {
                            this.ReplaceMultiLang(ref str6, "", strPageDepth);
                        }
                        if (str6.IndexOf("[QH:GuestBook ") >= 0)
                        {
                            this.ReplaceMwssage(ref str6, strPageDepth, row2["Title"].ToString());
                        }
                        string str15 = "NewsDetails_" + row2["id"] + ".html";
                        str2 = str2 + this.CreateStaticFile(str5 + "/" + str15, str6, row["ColumnName"].ToString() + "详细内容");
                        nNumber++;
                    }
                }
            }
            return str2;
        }

        public string CreateProductCenter(string strAbsPath, string strTPath, string strRootDir, ArrayList alistID)
        {
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "产品模块模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='3'");
            if (rowArray.Length == 0)
            {
                return "没有属于产品模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            foreach (DataRow row in rowArray)
            {
                string str3;
                string str5;
                string str6;
                string str9;
                string[] strArray;
                if (row["IsShow"].ToString() == "0")
                {
                    continue;
                }
                string strColumnID = row["id"].ToString();
                if ((alistID != null) && !this.IsIDInArrayList(strColumnID, alistID))
                {
                    continue;
                }
                if (row["outLink"].ToString().Trim() != string.Empty)
                {
                    string str12 = str2;
                    str2 = str12 + "栏目《" + row["ColumnName"].ToString() + "》为外部链接：" + row["outLink"].ToString().Trim() + "！";
                    continue;
                }
                string depthTemplate = this.GetDepthTemplate(row, "TemplateName");
                if ((depthTemplate != string.Empty) && !this.bIsMobile)
                {
                    str6 = this.GetTemplateContent(strAbsPath.Replace("Module_Product.aspx", depthTemplate));
                    if (!string.IsNullOrEmpty(str6))
                    {
                        nCopyRight = this.SetCopyRight(ref str6);
                        if (nCopyRight > 0)
                        {
                            return this.CopyRightError(nCopyRight);
                        }
                        goto Label_0217;
                    }
                    string str13 = str2;
                    str2 = str13 + "《" + row["ColumnName"].ToString() + @"》模板template\" + strTPath + @"\" + depthTemplate + "不存在或为空";
                    continue;
                }
                str6 = templateContent;
            Label_0217:
                str3 = row["depth"].ToString();
                this.GetSiteInfoContents(ref str6, str3, strColumnID);
                this.ReplaceNav(ref str6, str3);
                if (str6.IndexOf("[QH:Banner]") >= 0)
                {
                    this.ReplaceBanner(ref str6, str3, strColumnID);
                }
                this.ReplaceLoop(ref str6, str3, strColumnID);
                this.ReplaceTitle(ref str6, str3, strColumnID);
                this.ReplaceProductListTitle(ref str6, str3, strColumnID);
                this.ReplaceNavLoopBig(ref str6, str3, strColumnID);
                this.ReplaceLanmuLoopBig(ref str6, str3, strColumnID);
                this.ReplaceFenyeTitle(ref str6, row, str3);
                this.ReplaceCommen(ref str6, row, str3);
                this.ReplaceStatisticsJS(ref str6, str3);
                if (str6.IndexOf("[QH:BaiduMap") >= 0)
                {
                    this.ReplaceBaiduMap(ref str6, str3);
                }
                if (str6.IndexOf("[QH:SearchOnclick") >= 0)
                {
                    this.ReplaceSearch(ref str6, str3, "Product");
                }
                if (str6.IndexOf("[QH:Advertise") >= 0)
                {
                    this.ReplaceAdvertise(ref str6, str3);
                }
                if ((str6.Contains("[QH:LangLink") || str6.Contains("[QH:LangVersion")) || str6.Contains("[QH:LangImg"))
                {
                    this.ReplaceMultiLang(ref str6, "", str3);
                }
                if (str6.IndexOf("[QH:GuestBook ") >= 0)
                {
                    this.ReplaceMwssage(ref str6, str3, row["ColumnName"].ToString());
                }
                if (!this.GetStaticFUrlPath(out str5, row, this.dTableClmn, strRootDir))
                {
                    return "无法创建目录。";
                }
                string createdFileName = this.GetCreatedFileName(row["fileName"].ToString().Trim());
                string[] astrContent = this.GetPageContent(ref str6, row, out str9, "", out strArray);
                if (astrContent == null)
                {
                    int index = str6.IndexOf("[QH:Pager");
                    if (index != -1)
                    {
                        int num3 = str6.IndexOf(']', index);
                        string oldValue = str6.Substring(index, (num3 - index) + 1);
                        str6 = str6.Replace(oldValue, "");
                    }
                    if (str6.IndexOf("[QH:PageLoop]") > 0)
                    {
                        str2 = str2 + this.ReplacePagerLoop(ref str6);
                    }
                    str2 = str2 + this.CreateStaticFile(str5 + "/" + createdFileName, str6, row["ColumnName"].ToString());
                }
                else
                {
                    str2 = str2 + this.CreateListPager(str6, astrContent, createdFileName, str5, row["ColumnName"].ToString(), str9, "", strArray);
                }
            }
            return str2;
        }

        public string CreateProductDetails(string strAbsPath, int nCreateNumber, string strRootDir, string strBackFile)
        {
            string str6;
            int num = 30;
            int num2 = 0;
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "产品内容页模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='3'");
            if (rowArray.Length == 0)
            {
                return "没有属于产品模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            string format = "select id,Product_id,Title,Price,Memo1,Key,ImageUrl,ThumbUrl,displayImg,Content,AddDate,ModyDate,ctitle,KeyW,Description,Author,hits,Annex,SpecialName from QH_Product where ColumnID='{0}' order by TopShow asc,{1} {2}";
            if (!this.GetDetailsUrlPath(out str6, "ProductDetails", strRootDir))
            {
                return "无法创建目录。";
            }
            DataTable dataTable = this.Bll1.GetDataTable("select id,name,no_order,wr_ok,columnID from QH_Parameter where [Module]='3' order by CLNG(columnID) asc,CLNG(no_order) asc");
            string strPageDepth = "0";
            this.SetPicLinkTM(strPageDepth);
            foreach (DataRow row in rowArray)
            {
                string str11;
                string str12;
                string str10 = null;
                string depthTemplate = this.GetDepthTemplate(row, "DetailTemplate");
                if ((depthTemplate != string.Empty) && !this.bIsMobile)
                {
                    str10 = this.GetTemplateContent(strAbsPath.Replace("Module_ProductDetails.aspx", depthTemplate));
                    if (string.IsNullOrEmpty(str10))
                    {
                        string str15 = str2;
                        str2 = str15 + "《" + row["ColumnName"].ToString() + "》模板" + depthTemplate + "不存在或为空";
                        continue;
                    }
                    nCopyRight = this.SetCopyRight(ref str10);
                    if (nCopyRight > 0)
                    {
                        return this.CopyRightError(nCopyRight);
                    }
                }
                string strClmndepth = row["depth"].ToString();
                string str5 = row["id"].ToString();
                this.GetCustumPara(templateContent, "'" + str5 + "'", "3");
                this.GetOrderby(out str11, out str12, row["ListOrder"].ToString());
                DataTable dtDtls = this.Bll1.GetDataTable(string.Format(format, str5, str12, str11));
                string[,] strArray = null;
                DataTable dtPList = this.GetColumnCusFData(dataTable, str5, ref strArray, "3");
                int nNumber = 0;
                foreach (DataRow row2 in dtDtls.Rows)
                {
                    if (num2 < nCreateNumber)
                    {
                        num2++;
                    }
                    else
                    {
                        if (num2 >= (nCreateNumber + num))
                        {
                            object obj2 = str2;
                            str2 = string.Concat(new object[] { obj2, "<br>此次分段生成", num, "个文件！" });
                            string str16 = str2;
                            string[] strArray3 = new string[] { str16, "<script language=javascript>window.location.href=('", strBackFile, ".aspx?Mdl=PD&LastNumber=", (nCreateNumber + num).ToString(), "')</script>" };
                            return string.Concat(strArray3);
                        }
                        num2++;
                        string strTemp = (str10 == null) ? templateContent : str10;
                        this.GetSiteInfoContents(ref strTemp, strPageDepth, str5, strClmndepth, row2);
                        this.ReplaceNav(ref strTemp, strPageDepth);
                        if (strTemp.IndexOf("[QH:Banner]") >= 0)
                        {
                            this.ReplaceBanner(ref strTemp, strPageDepth, str5);
                        }
                        this.ReplaceLoop(ref strTemp, strPageDepth, str5);
                        this.ReplaceTitle(ref strTemp, strPageDepth, str5);
                        this.ReplaceProductListTitle(ref strTemp, strPageDepth, str5);
                        this.ReplaceNavLoopBig(ref strTemp, strPageDepth, str5);
                        this.ReplaceLanmuLoopBig(ref strTemp, strPageDepth, str5);
                        this.ReplaceFenyeTitle(ref strTemp, row, strPageDepth);
                        this.ReplaceBackList(ref strTemp, row, strPageDepth);
                        this.ReplaceCommen(ref strTemp, row, strPageDepth);
                        this.ReplaceDetailPicture(ref strTemp, row2, strPageDepth, "3");
                        this.ReplaceProductDetails(ref strTemp, row2, strPageDepth);
                        this.ReplaceCusField(ref strTemp, row2, dtPList, strArray, "3");
                        this.ReplaceProductPicture(ref strTemp, row2, strPageDepth);
                        this.ReplaceDetailsPager(ref strTemp, dtDtls, nNumber, "ProductDetails_");
                        this.ReplaceStatisticsJS(ref strTemp, strPageDepth);
                        if (strTemp.IndexOf("[QH:BaiduMap") >= 0)
                        {
                            this.ReplaceBaiduMap(ref strTemp, strPageDepth);
                        }
                        if (strTemp.IndexOf("[QH:SearchOnclick") >= 0)
                        {
                            this.ReplaceSearch(ref strTemp, strPageDepth, "Product");
                        }
                        if (strTemp.IndexOf("[QH:Advertise") >= 0)
                        {
                            this.ReplaceAdvertise(ref strTemp, strPageDepth);
                        }
                        if ((strTemp.Contains("[QH:LangLink") || strTemp.Contains("[QH:LangVersion")) || strTemp.Contains("[QH:LangImg"))
                        {
                            this.ReplaceMultiLang(ref strTemp, "", strPageDepth);
                        }
                        if (strTemp.IndexOf("[QH:GuestBook ") >= 0)
                        {
                            this.ReplaceMwssage(ref strTemp, strPageDepth, row2["Title"].ToString());
                        }
                        string str13 = "ProductDetails_" + row2["id"] + ".html";
                        str2 = str2 + this.CreateStaticFile(str6 + "/" + str13, strTemp, row["ColumnName"].ToString() + "详细内容");
                        nNumber++;
                    }
                }
            }
            return str2;
        }

        public string CreateProductDetailsOne(string strAbsPath, string strID, string strColumnID, ref string strOKInfo, bool bDelCreate, bool bOneCreate, string strRootDir)
        {
            string str5;
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                strOKInfo = "产品内容页模板不存在或为空";
                return "产品内容页模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight, ref strOKInfo);
            }
            DataRow[] rowArray = this.dTableClmn.Select("id=" + strColumnID);
            if (rowArray.Length == 0)
            {
                strOKInfo = "生成失败！";
                return "没有属于产品模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            string format = "select id,Product_id,Title,Price,Memo1,Key,ImageUrl,ThumbUrl,displayImg,Content,AddDate,ModyDate,ctitle,KeyW,Description,Author,hits,Annex,SpecialName from QH_Product where ColumnID='{0}' order by TopShow asc,{1} {2}";
            if (!this.GetDetailsUrlPath(out str5, "ProductDetails", strRootDir))
            {
                strOKInfo = "生成失败！";
                return "无法创建目录。";
            }
            DataTable dataTable = this.Bll1.GetDataTable("select id,name,no_order,wr_ok,columnID from QH_Parameter where [Module]='3' order by CLNG(columnID) asc,CLNG(no_order) asc");
            string strPageDepth = "0";
            this.SetPicLinkTM(strPageDepth);
            foreach (DataRow row in rowArray)
            {
                string str9 = null;
                string depthTemplate = this.GetDepthTemplate(row, "DetailTemplate");
                if ((depthTemplate != string.Empty) && !this.bIsMobile)
                {
                    str9 = this.GetTemplateContent(strAbsPath.Replace("Module_ProductDetails.aspx", depthTemplate));
                    if (string.IsNullOrEmpty(str9))
                    {
                        string str17 = str2;
                        str2 = str17 + "《" + row["ColumnName"].ToString() + "》模板" + depthTemplate + "不存在或为空";
                        continue;
                    }
                    nCopyRight = this.SetCopyRight(ref str9);
                    if (nCopyRight > 0)
                    {
                        return this.CopyRightError(nCopyRight, ref strOKInfo);
                    }
                }
                string strClmndepth = row["depth"].ToString();
                if (strColumnID == row["id"].ToString())
                {
                    string str10;
                    string str11;
                    this.GetCustumPara(templateContent, "'" + strColumnID + "'", "3");
                    this.GetOrderby(out str10, out str11, row["ListOrder"].ToString());
                    DataTable dtCtnt = this.Bll1.GetDataTable(string.Format(format, strColumnID, str11, str10));
                    string str12 = "";
                    string str13 = "";
                    if (!bOneCreate)
                    {
                        this.GetInfluenceID(strID, ref str12, ref str13, dtCtnt);
                    }
                    string[,] strArray = null;
                    DataTable dtPList = this.GetColumnCusFData(dataTable, strColumnID, ref strArray, "3");
                    if (bDelCreate)
                    {
                        dtCtnt = this.GetNoDelTable(dtCtnt, strID);
                    }
                    int nNumber = 0;
                    foreach (DataRow row2 in dtCtnt.Rows)
                    {
                        string str6;
                        string str14 = row2["id"].ToString();
                        if (bDelCreate)
                        {
                            if ((str12 == str14) || (str13 == str14))
                            {
                                goto Label_02E4;
                            }
                            nNumber++;
                            continue;
                        }
                        if ((!(str12 == str14) && !(str13 == str14)) && !(str14 == strID))
                        {
                            nNumber++;
                            continue;
                        }
                    Label_02E4:
                        str6 = (str9 == null) ? templateContent : str9;
                        this.GetSiteInfoContents(ref str6, strPageDepth, strColumnID, strClmndepth, row2);
                        this.ReplaceNav(ref str6, strPageDepth);
                        if (str6.IndexOf("[QH:Banner]") >= 0)
                        {
                            this.ReplaceBanner(ref str6, strPageDepth, strColumnID);
                        }
                        this.ReplaceLoop(ref str6, strPageDepth, strColumnID);
                        this.ReplaceTitle(ref str6, strPageDepth, strColumnID);
                        this.ReplaceProductListTitle(ref str6, strPageDepth, strColumnID);
                        this.ReplaceNavLoopBig(ref str6, strPageDepth, strColumnID);
                        this.ReplaceLanmuLoopBig(ref str6, strPageDepth, strColumnID);
                        this.ReplaceFenyeTitle(ref str6, row, strPageDepth);
                        this.ReplaceBackList(ref str6, row, strPageDepth);
                        this.ReplaceCommen(ref str6, row, strPageDepth);
                        this.ReplaceDetailPicture(ref str6, row2, strPageDepth, "3");
                        this.ReplaceProductDetails(ref str6, row2, strPageDepth);
                        this.ReplaceCusField(ref str6, row2, dtPList, strArray, "3");
                        this.ReplaceProductPicture(ref str6, row2, strPageDepth);
                        this.ReplaceDetailsPager(ref str6, dtCtnt, nNumber, "ProductDetails_");
                        this.ReplaceStatisticsJS(ref str6, strPageDepth);
                        if (str6.IndexOf("[QH:BaiduMap") >= 0)
                        {
                            this.ReplaceBaiduMap(ref str6, strPageDepth);
                        }
                        if (str6.IndexOf("[QH:SearchOnclick") >= 0)
                        {
                            this.ReplaceSearch(ref str6, strPageDepth, "Product");
                        }
                        if (str6.IndexOf("[QH:Advertise") >= 0)
                        {
                            this.ReplaceAdvertise(ref str6, strPageDepth);
                        }
                        if ((str6.Contains("[QH:LangLink") || str6.Contains("[QH:LangVersion")) || str6.Contains("[QH:LangImg"))
                        {
                            this.ReplaceMultiLang(ref str6, "", strPageDepth);
                        }
                        if (str6.IndexOf("[QH:GuestBook ") >= 0)
                        {
                            this.ReplaceMwssage(ref str6, strPageDepth, row2["Title"].ToString());
                        }
                        string str15 = "ProductDetails_" + row2["id"] + ".html";
                        str2 = str2 + this.CreateStaticFile(str5 + "/" + str15, str6, row["ColumnName"].ToString() + "详细内容");
                        nNumber++;
                    }
                }
            }
            return str2;
        }

        public string CreateProductPriceInterval(string strAbsPath, string strTPath, string strRootDir, ArrayList alistID)
        {
            string str5;
            string templateContent = this.GetTemplateContent(strAbsPath);
            if (string.IsNullOrEmpty(templateContent))
            {
                return "产品价格区间页模板不存在或为空";
            }
            int nCopyRight = this.SetCopyRight(ref templateContent);
            if (nCopyRight > 0)
            {
                return this.CopyRightError(nCopyRight);
            }
            DataRow[] rowArray = this.dTableClmn.Select("Module='3'");
            if (rowArray.Length == 0)
            {
                return "没有属于产品模块的栏目。";
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str2 = "";
            if (!this.GetDetailsUrlPath(out str5, "ProductPrice", ""))
            {
                return "无法创建目录。";
            }
            List<string[]> list = this.Bll1.DAL1.ReadDataReaderListStr("select Start,[End] from QH_PriceInterval order by CLNG([order]) asc ", 2);
            foreach (DataRow row in rowArray)
            {
                string strPageDepth = row["depth"].ToString();
                if ((strPageDepth == "0") && (row["outLink"].ToString().Trim() == string.Empty))
                {
                    string strColumnID = row["id"].ToString();
                    string strTemp = templateContent;
                    this.GetSiteInfoContents(ref strTemp, strPageDepth, strColumnID);
                    this.ReplaceNav(ref strTemp, strPageDepth);
                    if (templateContent.IndexOf("[QH:Banner]") >= 0)
                    {
                        this.ReplaceBanner(ref strTemp, strPageDepth, strColumnID);
                    }
                    this.ReplaceLoop(ref strTemp, strPageDepth, strColumnID);
                    this.ReplaceTitle(ref strTemp, strPageDepth, strColumnID);
                    this.ReplaceProductListTitle(ref strTemp, strPageDepth, strColumnID);
                    this.ReplaceFenyeTitle(ref strTemp, row, strPageDepth);
                    this.ReplaceCommen(ref strTemp, row, strPageDepth);
                    this.ReplaceStatisticsJS(ref strTemp, strPageDepth);
                    if (strTemp.IndexOf("[QH:BaiduMap") >= 0)
                    {
                        this.ReplaceBaiduMap(ref strTemp, strPageDepth);
                    }
                    if (strTemp.IndexOf("[QH:SearchOnclick") >= 0)
                    {
                        this.ReplaceSearch(ref strTemp, strPageDepth, "Product");
                    }
                    if (strTemp.IndexOf("[QH:Advertise") >= 0)
                    {
                        this.ReplaceAdvertise(ref strTemp, strPageDepth);
                    }
                    string format = "Price_{0}.html";
                    this.dTableProductAll = null;
                    foreach (string[] strArray3 in list)
                    {
                        string str8;
                        string[] strArray;
                        string str9 = strTemp;
                        string strSFileName = string.Format(format, strArray3[0].Replace('.', 'd') + "_" + strArray3[1].Replace('.', 'd'));
                        string[] astrContent = this.GetPageContent(ref str9, row, out str8, "", out strArray, strArray3);
                        if (astrContent == null)
                        {
                            str9 = str9.Replace("[QH:Pager]", "");
                            str2 = str2 + this.CreateStaticFile(str5 + "/" + strSFileName, str9, row["ColumnName"].ToString() + strSFileName);
                        }
                        else
                        {
                            str2 = str2 + this.CreateListPager(str9, astrContent, strSFileName, str5, row["ColumnName"].ToString() + strSFileName, str8, "", strArray);
                        }
                    }
                }
            }
            return str2;
        }

        private string CreateStaticFile(string strFilePath, string strContent, string strColumnName)
        {
            bool flag = true;
            Encoding encoding = Encoding.UTF8;
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(this.CSPage.Server.MapPath("../") + strFilePath, false, encoding);
                writer.Write(strContent);
                writer.Flush();
            }
            catch (Exception exception)
            {
                flag = false;
                SystemError.CreateErrorLog("写静态文件错误： " + exception.ToString());
            }
            finally
            {
                writer.Close();
            }
            Random random = new Random();
            if (flag)
            {
                return ("恭喜<a href=../" + strFilePath + "?" + random.Next().ToString() + " target=_blank>" + strFilePath + "</a>已经生成，为《" + strColumnName + "》栏目页！");
            }
            return "生成静态文件错误！";
        }

        public string CreateTagsAndSearch(string strAbsPath, string strRootDir)
        {
            string str7;
            string path = strAbsPath.Replace("Module_TagsAndSearch", "Module_TagsAndSearchNews");
            bool flag = false;
            string strTemp = null;
            if (File.Exists(path))
            {
                strTemp = this.GetTemplateContent(path);
                flag = true;
                int nCopyRight = this.SetCopyRight(ref strTemp);
                if (nCopyRight > 0)
                {
                    return this.CopyRightError(nCopyRight);
                }
            }
            string str3 = strAbsPath.Replace("Module_TagsAndSearch", "Module_TagsAndSearchProduct");
            bool flag2 = false;
            string templateContent = null;
            if (File.Exists(str3))
            {
                templateContent = this.GetTemplateContent(str3);
                flag2 = true;
                int num2 = this.SetCopyRight(ref templateContent);
                if (num2 > 0)
                {
                    return this.CopyRightError(num2);
                }
            }
            this.strGlobalRootDirPre = (strRootDir == "") ? "" : "../";
            string str5 = "";
            string strPageDepth = "0";
            if (!this.GetDetailsUrlPath(out str7, "TagsAndSearch", strRootDir))
            {
                return "无法创建目录。";
            }
            string[] strArray = new string[] { "NewsTags.html", "NewsSearch.html", "ProductTags.html", "ProductSearch.html", "ProductBrand.html" };
            string[] strArray2 = new string[] { "新闻标签", "新闻搜索", "产品标签", "产品搜索", "产品品牌" };
            string[] strArray3 = new string[] { "News", "News", "Product", "Product", "Product" };
            this.GetPagePre(strPageDepth);
            DataRow dw = null;
            string strColumnID = "0";
            for (int i = 0; i < 5; i++)
            {
                string str8;
                if (flag2 && (i > 1))
                {
                    str8 = templateContent;
                }
                else
                {
                    if (!flag || (i >= 2))
                    {
                        continue;
                    }
                    str8 = strTemp;
                }
                this.GetSiteInfoContents(ref str8, strPageDepth, "S");
                this.ReplaceNav(ref str8, strPageDepth);
                this.ReplaceSearchResult(ref str8, strPageDepth, strArray3[i], ref dw);
                if (dw != null)
                {
                    strColumnID = dw["id"].ToString();
                }
                if (str8.IndexOf("[QH:Banner]") >= 0)
                {
                    this.ReplaceBanner(ref str8, strPageDepth, strColumnID);
                }
                this.ReplaceLoop(ref str8, strPageDepth, strColumnID);
                this.ReplaceTitle(ref str8, strPageDepth, strColumnID);
                this.ReplaceProductListTitle(ref str8, strPageDepth, strColumnID);
                this.ReplaceLanmuLoopBig(ref str8, strPageDepth, strColumnID);
                this.ReplaceNavLoopBig(ref str8, strPageDepth, strColumnID);
                if (dw != null)
                {
                    this.ReplaceFenyeTitle(ref str8, dw, strPageDepth);
                    this.ReplaceCommen(ref str8, dw, strPageDepth);
                }
                this.ReplaceStatisticsJS(ref str8, strPageDepth);
                if (str8.IndexOf("[QH:BaiduMap") >= 0)
                {
                    this.ReplaceBaiduMap(ref str8, strPageDepth);
                }
                if (str8.IndexOf("[QH:Advertise") >= 0)
                {
                    this.ReplaceAdvertise(ref str8, strPageDepth);
                }
                if ((str8.Contains("[QH:LangLink") || str8.Contains("[QH:LangVersion")) || str8.Contains("[QH:LangImg"))
                {
                    this.ReplaceMultiLang(ref str8, "", strPageDepth);
                }
                str8 = str8.Replace("[QH:SearchValue]", strArray2[i] + "： <span id=\"CDQHT2Search\" ></span>");
                if (str8.IndexOf("[QH:SearchOnclick") >= 0)
                {
                    this.ReplaceSearch(ref str8, strPageDepth, strArray3[i]);
                }
                str5 = str5 + this.CreateStaticFile(str7 + "/" + strArray[i], str8, strArray2[i]);
            }
            return str5;
        }

        private string FilterHotLink(string strContent, string strPageDepth)
        {
            if (this.dtLink != null)
            {
                for (int i = 0; i < this.dtLink.Rows.Count; i++)
                {
                    string oldValue = this.dtLink.Rows[i]["Tags"].ToString();
                    Regex regex = new Regex("<[^>]+(" + oldValue + ")[^>]*>");
                    strContent = regex.Replace(strContent, new MatchEvaluator(this.TempReplace));
                    strContent = new Regex(@"<a[^>]*>[\s\S]*?(" + oldValue + @")[\s\S]*?</a>", RegexOptions.IgnoreCase).Replace(strContent, new MatchEvaluator(this.TempReplace));
                    string str = this.dtLink.Rows[i]["Link"].ToString();
                    if (str.Contains("http://"))
                    {
                        strContent = strContent.Replace(oldValue, "<a href=\"" + str + "\">" + oldValue + "</a>");
                    }
                    else
                    {
                        strContent = strContent.Replace(oldValue, "<a href=\"" + this.PageUrlSet(str, strPageDepth) + "\">" + oldValue + "</a>");
                    }
                    strContent = strContent.Replace("$$@@&&", oldValue);
                }
            }
            return strContent;
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

        private void Get3ClassClmnData(ref string strValue1, ref string strValue2, ref string strValue3, ref string strFenlanPic, ref string strFenLanEnglish, string strPageDepth, DataRow dRow)
        {
            string pagePre = this.GetPagePre(strPageDepth);
            string str2 = dRow["depth"].ToString();
            if (str2 != null)
            {
                DataRow[] rowArray;
                if (!(str2 == "0"))
                {
                    if (!(str2 == "1"))
                    {
                        if (str2 == "2")
                        {
                            rowArray = this.dTableClmn.Select("id=" + dRow["ParentID"]);
                            DataRow[] rowArray2 = this.dTableClmn.Select("id=" + rowArray[0]["ParentID"]);
                            strValue1 = this.GetIsShowLink(rowArray2[0]["folder"].ToString(), rowArray2[0], strPageDepth, pagePre);
                            strValue2 = this.GetIsShowLink(rowArray2[0]["folder"].ToString() + "/" + rowArray[0]["folder"].ToString(), rowArray[0], strPageDepth, pagePre);
                            strValue3 = this.GetIsShowLink(rowArray2[0]["folder"].ToString() + "/" + rowArray[0]["folder"].ToString() + "/" + dRow["folder"].ToString(), dRow, strPageDepth, pagePre);
                            strFenlanPic = rowArray2[0]["C_img"].ToString();
                            strFenLanEnglish = rowArray2[0]["C_EnTitle"].ToString();
                        }
                        return;
                    }
                }
                else
                {
                    strValue1 = this.GetIsShowLink(dRow["folder"].ToString(), dRow, strPageDepth, pagePre);
                    strFenlanPic = dRow["C_img"].ToString();
                    strFenLanEnglish = dRow["C_EnTitle"].ToString();
                    return;
                }
                rowArray = this.dTableClmn.Select("id=" + dRow["ParentID"]);
                strValue1 = this.GetIsShowLink(rowArray[0]["folder"].ToString(), rowArray[0], strPageDepth, pagePre);
                strValue2 = this.GetIsShowLink(rowArray[0]["folder"].ToString() + "/" + dRow["folder"].ToString(), dRow, strPageDepth, pagePre);
                strFenlanPic = rowArray[0]["C_img"].ToString();
                strFenLanEnglish = rowArray[0]["C_EnTitle"].ToString();
            }
        }

        private string[] GetAdvertise(List<string[]> listastrAdv, string strOrder)
        {
            for (int i = 0; i < listastrAdv.Count; i++)
            {
                if (listastrAdv[i][0] == strOrder)
                {
                    return listastrAdv[i];
                }
            }
            return null;
        }

        private DataTable GetBannerUrl(string strType, string strColumnID)
        {
            DataTable table = this.dtBnrImg.Clone();
            foreach (DataRow row in this.dtBnrImg.Select("type='" + strType + "'"))
            {
                if (this.bIsDefault)
                {
                    if (row["Belongs"].ToString().Contains("D") || row["Belongs"].ToString().Contains("," + strColumnID + ","))
                    {
                        table.ImportRow(row);
                    }
                }
                else if (row["Belongs"].ToString().Contains("," + strColumnID + ","))
                {
                    table.ImportRow(row);
                }
            }
            return table;
        }

        private bool GetBnrSetData(ref string strTemp, string strColumnID, string strLoop)
        {
            this.ja2strBnrDefault = this.Bll1.DAL1.ReadDataReaderJagged2DStr("select top 2 BnrMode,BnrStyle,BnrWidth,BnrHeight,BnrDefault from QH_ClmnBanner order by id asc", 2, 5);
            if (strColumnID == "0")
            {
                this.astrBnrSet = (this.ja2strBnrDefault[1][4] == "True") ? this.ja2strBnrDefault[0] : this.ja2strBnrDefault[1];
                this.bIsDefault = this.ja2strBnrDefault[1][4] == "True";
                this.strColumnName = "首页";
            }
            else
            {
                if (this.dTableClmn == null)
                {
                    this.dTableClmn = this.Bll1.GetDataTable(this.strClmnQuery);
                }
                DataRow[] rowArray = this.dTableClmn.Select("ID=" + strColumnID);
                if (rowArray.Length == 0)
                {
                    strTemp = strTemp.Replace(strLoop, "栏目id错误。");
                    return false;
                }
                this.astrBnrSet = (rowArray[0]["BnrDefault"].ToString() == "True") ? this.ja2strBnrDefault[0] : new string[] { rowArray[0]["BnrMode"].ToString(), rowArray[0]["BnrStyle"].ToString(), rowArray[0]["BnrWidth"].ToString(), rowArray[0]["BnrHeight"].ToString(), "False" };
                this.bIsDefault = rowArray[0]["BnrDefault"].ToString() == "True";
                this.strColumnName = rowArray[0]["ColumnName"].ToString();
            }
            if (this.astrBnrSet[0] != "0")
            {
                this.dtBnrImg = this.Bll1.GetDataTable("select Belongs,Title,ImgUrl,ImgLink,flashUrl,type from QH_BannerImg order by CLNG(type) asc,CLNG(no_order) asc");
            }
            return true;
        }

        private bool GetBnrSetDataMobile(ref string strTemp, string strColumnID, string strLoop)
        {
            this.ja2strBnrDefault = this.Bll1.DAL1.ReadDataReaderJagged2DStr("select BnrMode,BnrStyle,BnrWidth,BnrHeight,BnrDefault from QH_ClmnBanner where id=3 ", 1, 5);
            this.astrBnrSet = this.ja2strBnrDefault[0];
            this.dtBnrImg = this.Bll1.GetDataTable("select Belongs,Title,ImgUrl,ImgLink,flashUrl,type from QH_MobileBanner order by CLNG(type) asc,CLNG(no_order) asc");
            return true;
        }

        private DataRow GetClumnDataRow(string strColumnIDSet, string strSearchType)
        {
            DataRow[] rowArray;
            if (strColumnIDSet != "")
            {
                rowArray = this.dTableClmn.Select("IDMark='" + strColumnIDSet.Split(new char[] { '|' })[0] + "'");
                if (rowArray.Length != 0)
                {
                    return rowArray[0];
                }
                return null;
            }
            rowArray = this.dTableClmn.Select("depth='0' and [Module]='" + ((strSearchType == "News") ? "2" : "3") + "'");
            if (rowArray.Length != 0)
            {
                return rowArray[0];
            }
            return null;
        }

        private DataTable GetColumnCusFData(DataTable dtPara, string strColumnID, ref string[,] a2strCusField, string strMdl)
        {
            try
            {
                DataRow[] rowArray = dtPara.Select("columnID='" + strColumnID + "' or columnID='0'");
                if (rowArray.Length == 0)
                {
                    return null;
                }
                a2strCusField = new string[rowArray.Length, 2];
                string str = "";
                for (int i = 0; i < rowArray.Length; i++)
                {
                    object obj2 = str;
                    str = string.Concat(new object[] { obj2, ",'", rowArray[i]["id"], "'" });
                    a2strCusField[i, 0] = rowArray[i]["id"].ToString();
                    a2strCusField[i, 1] = rowArray[i]["name"].ToString();
                }
                str = str.Substring(1);
                return this.Bll1.GetDataTable("select listid,paraid,info from QH_pList where [Module]='" + strMdl + "' and paraid in(" + str + ") order by CLNG(listid) asc");
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("读QH_pList错误： " + exception.ToString());
            }
            return null;
        }

        private string GetColumnID(string strIDMark)
        {
            DataRow[] rowArray;
            strIDMark = (strIDMark == "") ? "0" : strIDMark;
            if (strIDMark == "0")
            {
                rowArray = this.dTableClmn.Select("Module='" + this.LoopAttribute.Module + "'");
                if (rowArray.Length == 0)
                {
                    return "0";
                }
                return rowArray[0]["id"].ToString();
            }
            rowArray = this.dTableClmn.Select("IDMark='" + strIDMark + "'");
            if (rowArray.Length == 0)
            {
                return "0";
            }
            return rowArray[0]["id"].ToString();
        }

        private string[] GetContentArray(string strLoopContent, DataTable dTable, int nPageNumber, int nCNumber, string TableName, string strPageDepth, out string[] astrHitsIdHdn)
        {
            astrHitsIdHdn = null;
            bool flag = strLoopContent.IndexOf("[QH:Hits]") >= 0;
            int num = ((this.LoopAttribute.Condition == "") || (this.LoopAttribute.Condition == null)) ? 0 : int.Parse(this.LoopAttribute.Condition);
            StringBuilder[] builderArray = new StringBuilder[nPageNumber];
            for (int i = 0; i < nPageNumber; i++)
            {
                builderArray[i] = new StringBuilder("");
            }
            string[] strArray = new string[nPageNumber];
            StringBuilder[] builderArray2 = new StringBuilder[nPageNumber];
            if (flag)
            {
                astrHitsIdHdn = new string[nPageNumber];
                for (int k = 0; k < nPageNumber; k++)
                {
                    builderArray2[k] = new StringBuilder(TableName);
                }
            }
            for (int j = 0; j < nPageNumber; j++)
            {
                int num5 = j * nCNumber;
                int count = num5 + nCNumber;
                if (j == (nPageNumber - 1))
                {
                    count = dTable.Rows.Count;
                }
                for (int m = num5; m < count; m++)
                {
                    DataRow dRow = dTable.Rows[m];
                    builderArray[j].Append(this.LoopReplaceContent(strLoopContent, dRow, TableName, strPageDepth, 0));
                    if ((num != 0) && (((m + 1) % num) == 0))
                    {
                        builderArray[j].Append(this.LoopAttribute.Role);
                    }
                    if (flag)
                    {
                        builderArray2[j].Append("|" + dRow["id"]);
                    }
                }
                strArray[j] = builderArray[j].ToString();
                if (flag)
                {
                    astrHitsIdHdn[j] = builderArray2[j].ToString();
                }
            }
            return strArray;
        }

        private string[] GetContentArrayPubu(List<string[]> listastrPic, int nPageNumber, int nCNumber, string strPageDepth)
        {
            string format = "<ul id=\"stage\">\n<li id=l1 >\n{0}</li>\n<li id=l2 >\n{1}</li>\n<li id=l3 >\n{2}</li>\n</ul>\n";
            string str2 = "<div class=\"pbl\"><a href=\"" + this.GetPagePre(strPageDepth) + "PictureDetails/PictureDetails_{0}.html?Or={1}\" title=\"{2}\"><img src=\"{3}\" /><span>{2}</span></a></div>";
            string[] strArray = new string[nPageNumber];
            for (int i = 0; i < nPageNumber; i++)
            {
                string str3 = "";
                string str4 = "";
                string str5 = "";
                int num2 = i * nCNumber;
                int num3 = num2 + 6;
                if (i == (nPageNumber - 1))
                {
                    num3 = Math.Min(num3, listastrPic.Count);
                }
                int num4 = 0;
                for (int j = num2; j < num3; j++)
                {
                    if (num4 < 2)
                    {
                        str3 = str3 + string.Format(str2, new object[] { listastrPic[j][2], listastrPic[j][3], listastrPic[j][0], this.PageUrlSet(listastrPic[j][1], strPageDepth) });
                    }
                    else if (num4 < 4)
                    {
                        str4 = str4 + string.Format(str2, new object[] { listastrPic[j][2], listastrPic[j][3], listastrPic[j][0], this.PageUrlSet(listastrPic[j][1], strPageDepth) });
                    }
                    else if (num4 < 6)
                    {
                        str5 = str5 + string.Format(str2, new object[] { listastrPic[j][2], listastrPic[j][3], listastrPic[j][0], this.PageUrlSet(listastrPic[j][1], strPageDepth) });
                    }
                    num4++;
                    if (num4 >= 6)
                    {
                        break;
                    }
                }
                strArray[i] = string.Format(format, str3, str4, str5);
            }
            return strArray;
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

        private List<string[]> GetCusFieldInfo(string strLoopContent, string strMdl)
        {
            string str;
            string str2;
            this.GetCustomTags(strMdl, out str, out str2);
            str = str.Replace("]", ":");
            str2 = str2.Replace("]", ":");
            List<string[]> list = new List<string[]>();
            int startIndex = 0;
            for (int i = 0; i < 100; i++)
            {
                string[] item = new string[5];
                startIndex = strLoopContent.IndexOf(str, startIndex);
                if (startIndex == -1)
                {
                    return list;
                }
                int index = strLoopContent.IndexOf(']', startIndex);
                item[0] = strLoopContent.Substring(startIndex, (index - startIndex) + 1);
                item[2] = item[0].Replace(str, "").Replace(":", "").Replace("]", "").Trim();
                startIndex = strLoopContent.IndexOf(str2, startIndex);
                if (startIndex == -1)
                {
                    return list;
                }
                index = strLoopContent.IndexOf(']', startIndex);
                item[1] = strLoopContent.Substring(startIndex, (index - startIndex) + 1);
                list.Add(item);
            }
            return list;
        }

        private void GetCusFieldInfoID_Name_pList(List<string[]> listStrCusField, string strMdl, string strLoopClmnID)
        {
            string str = "";
            foreach (string[] strArray in listStrCusField)
            {
                str = str + ",'" + strArray[2] + "'";
            }
            DataTable dataTable = this.Bll1.GetDataTable("select id,name,IDMark from QH_Parameter where [Module]='" + strMdl + "' and columnID in(" + strLoopClmnID + ") and IDMark in(" + str.Substring(1) + ")");
            string str2 = "";
            for (int i = 0; i < listStrCusField.Count; i++)
            {
                DataRow[] rowArray = dataTable.Select("IDMark='" + listStrCusField[i][2] + "'");
                if (rowArray.Length == 0)
                {
                    string str3 = "";
                    listStrCusField[i][4] = (string)(str3 = "");
                    listStrCusField[i][3] = (string)str3;
                }
                else
                {
                    listStrCusField[i][3] = (string)rowArray[0]["name"].ToString();
                    listStrCusField[i][4] = (string)rowArray[0]["id"].ToString();
                    object obj2 = str2;
                    str2 = string.Concat(new object[] { obj2, ",'", rowArray[0]["id"], "'" });
                }
            }
            if (str2.Length > 1)
            {
                this.dtpListLoop = this.Bll1.GetDataTable("select listid,paraid,info from QH_pList where [Module]='" + strMdl + "' and paraid in(" + str2.Substring(1) + ") ");
            }
        }

        private string GetCusFieldLoop(ref string strTemp, ref string strLoopContent, string strLoopTags)
        {
            string str = "";
            int index = strTemp.IndexOf(strLoopTags);
            if (index < 0)
            {
                return "";
            }
            string str2 = strLoopTags.Insert(1, "/");
            int num2 = strTemp.IndexOf(str2, index);
            str = strTemp.Substring(index, (num2 - index) + str2.Length);
            strLoopContent = str.Substring(strLoopTags.Length, (str.Length - strLoopTags.Length) - str2.Length);
            return str;
        }

        private string GetCustomGuestBookJsScript(string strPageDepth, string[] astrField, string[] astrFieldID, string strPage)
        {
            string pagePre = this.GetPagePre(strPageDepth);
            if (this.bIsMobile)
            {
                pagePre = "../" + pagePre;
            }
            string[] strArray = new string[] { "NameFill", "TelFill", "MobileFill", "EmailFill", "ContactFill", "TitlelFill", "Bak1Fill", "Bak2Fill", "ContentFill", "IsCheckCode" };
            StringBuilder builder = new StringBuilder("");
            builder.Append("<script >\n");
            for (int i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case 1:
                    case 2:
                    case 3:
                        string str3 = "";
                        string str4 = "";
                        astrField[i] = (astrField[i] == "1") ? str4 : ((astrField[i] == "4") ? str3 : astrField[i]);
                        break;

                    default:
                        string str5 = "";
                        string str6 = "";
                        astrField[i] = (astrField[i] == "2") ? str6 : ((astrField[i] == "2") ? str5 : astrField[i]);
                        break;
                }
                builder.Append("var " + strArray[i] + "=\"" + astrField[i] + "\";\n");
            }
            builder.Append("function loadXMLDoc()\n");
            builder.Append("{\n");
            builder.Append("    var xmlhttp;\n");
            builder.Append("    if (window.XMLHttpRequest)\n");
            builder.Append("      {// code for IE7+, Firefox, Chrome, Opera, Safari\n");
            builder.Append("          xmlhttp=new XMLHttpRequest();\n");
            builder.Append("      }\n");
            builder.Append("    else\n");
            builder.Append("      {// code for IE6, IE5\n");
            builder.Append("          xmlhttp=new ActiveXObject(\"Microsoft.XMLHTTP\");\n");
            builder.Append("      }\n");
            builder.Append("      xmlhttp.onreadystatechange=function()\n");
            builder.Append("      {\n");
            builder.Append("            //alert (xmlhttp.responseText);\n");
            builder.Append("     if (xmlhttp.readyState==4 && xmlhttp.status==200)\n");
            builder.Append("        {\n");
            builder.Append("            //document.getElementById(\"myDiv\").innerHTML=xmlhttp.responseText;\n");
            for (int j = 0; j < 10; j++)
            {
                if (astrField[j] != "0")
                {
                    builder.Append("            " + astrFieldID[j] + ".value=\"\";\n");
                }
            }
            builder.Append("           alert (xmlhttp.responseText);\n");
            builder.Append("        }\n");
            builder.Append("      }\n");
            builder.Append("    xmlhttp.open(\"POST\",\"" + pagePre + "Ajax/AjaxGuestBookCustom.aspx\",true);\n");
            builder.Append("    xmlhttp.setRequestHeader(\"Content-type\",\"application/x-www-form-urlencoded;charset=utf-8\");\n");
            string str2 = "var vPost=\"UserName={0}&tel={1}&Mbl={2}&email={3}&addr={4}&title={5}&Bak1={6}&Bak2={7}&content={8}&source=" + HttpUtility.UrlEncode(strPage) + "\";\n";
            for (int k = 0; k < 10; k++)
            {
                if (astrField[k] != "0")
                {
                    str2 = str2.Replace("{" + k + "}", "\"+encodeURIComponent(" + astrFieldID[k] + ".value)+\"");
                }
                else
                {
                    str2 = str2.Replace("{" + k + "}", "");
                }
            }
            builder.Append(str2);
            builder.Append("    xmlhttp.send(vPost);\n");
            builder.Append("}\n");
            builder.Append("function Click_submit()\n");
            builder.Append("{\n");
            builder.Append("    \n");
            builder.Append("    if(!CheckData())\n");
            builder.Append("    {\n");
            builder.Append("        loadXMLDoc();\n");
            builder.Append("    }\n");
            builder.Append("    else alert('填写项目验证未通过！\\n带星号是必添项。'+vErrorMessage);\n");
            builder.Append("}\n");
            builder.Append("String.prototype.Trim = function()\n");
            builder.Append("{\n");
            builder.Append("\treturn this.replace( /(^\\s*)|(\\s*$)/g, '' ) ;\n");
            builder.Append("}\n");
            builder.Append("function getimgcode() \n");
            builder.Append("{ \n");
            builder.Append("    var getimagecode = document.getElementById(\"getcode\"); \n");
            builder.Append("    getimagecode.src = \"" + pagePre + "Ajax/GuestBook/VerifyCodeMsg.aspx?\"+Math.random();\n");
            builder.Append("} \n");
            builder.Append("function getCookie(name)   \n");
            builder.Append("{  \n");
            builder.Append("  var arr = document.cookie.match(new RegExp(\"(^| )\"+name+\"=([^;]*)(;|$)\"));\n");
            builder.Append("  if(arr != null) return decodeURIComponent(arr[2]); return null;  \n");
            builder.Append("} \n");
            builder.Append("var vErrorMessage=\"\";\n");
            builder.Append("function CheckData()\n");
            builder.Append("{\n");
            builder.Append("      vErrorMessage=\"\";\n");
            builder.Append("     var bFindError=false;\n");
            builder.Append("     var bFindChar=false;\n");
            builder.Append("     var vFilter=/[" + System.Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select top 1 RepChar from QH_MsgSetCustom")).Replace("|", "") + "]/g;\n");
            builder.Append("     if( " + strArray[0] + "!=\"0\"){\n");
            builder.Append("     GBNameID.value=GBNameID.value.replace(vFilter,'');\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[0] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     if(" + strArray[0] + "==\"1\"&&GBNameID.value.Trim() ==\"\")\n");
            builder.Append("     {\n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         vErrorMessage+=\"\\n请输入名称.\";\n");
            builder.Append("         bFindError=true;\n");
            builder.Append("     }\n");
            builder.Append("     }\n");
            builder.Append("     \n");
            builder.Append("     if( " + strArray[1] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[1] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     var reg = /^$|^\\+?\\d[-\\d]{6,}$/;\n");
            builder.Append("     if(" + strArray[1] + "==\"2\") reg = /^\\+?\\d[-\\d]{6,}$/;\n");
            builder.Append("     if(!reg.test(GBTelID.value.Trim()))\n");
            builder.Append("     {    // w+([-+.]w+)*@w+([-.]w+)*.w+([-.]w+)* \n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         vErrorMessage+=\"\\n请正确输入电话.\";\n");
            builder.Append("         bFindError=true;\n");
            builder.Append("     }\n");
            builder.Append("     }\n");
            builder.Append("     \n");
            builder.Append("     if( " + strArray[2] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[2] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     var reg = /^$|^1[0-9]{10}$/;\n");
            builder.Append("     if(" + strArray[2] + "==\"2\") reg = /^1[0-9]{10}$/;\n");
            builder.Append("     if(!reg.test(GBMobileID.value.Trim()))\n");
            builder.Append("     {    \n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         vErrorMessage+=\"\\n请正确输入手机.\";\n");
            builder.Append("         bFindError=true;\n");
            builder.Append("     }\n");
            builder.Append("     }\n");
            builder.Append("     \n");
            builder.Append("     if( " + strArray[3] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[3] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     var reg = /^$|^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$/;\n");
            builder.Append("     if(" + strArray[3] + "==\"2\") reg = /^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$/;\n");
            builder.Append("     if(!reg.test(GBEmailID.value.Trim()))\n");
            builder.Append("     {     \n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         vErrorMessage+=\"\\n请正确输入邮箱.\";\n");
            builder.Append("         bFindError=true;\n");
            builder.Append("     }\n");
            builder.Append("     }\n");
            builder.Append("     \n");
            builder.Append("     if( " + strArray[4] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[4] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBAddID.value=GBAddID.value.replace(vFilter,'');\n");
            builder.Append("     if(" + strArray[4] + "==\"1\"&&GBAddID.value.Trim() ==\"\")\n");
            builder.Append("     {    ");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         vErrorMessage+=\"\\n请输入文字.\";\n");
            builder.Append("         bFindError=true;\n");
            builder.Append("     }\n");
            builder.Append("     }\n");
            builder.Append("     \n");
            builder.Append("     if( " + strArray[5] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[5] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBTitleID.value=GBTitleID.value.replace(vFilter,'');\n");
            builder.Append("     if(" + strArray[5] + "==\"1\"&&GBTitleID.value.Trim() ==\"\")\n");
            builder.Append("     {\n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         vErrorMessage+=\"\\n请输入文字.\";\n");
            builder.Append("          bFindError=true;\n");
            builder.Append("    }\n");
            builder.Append("    }\n");
            builder.Append("    \n");
            builder.Append("     if( " + strArray[6] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[6] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBBak1ID.value=GBBak1ID.value.replace(vFilter,'');\n");
            builder.Append("     if(" + strArray[6] + "==\"1\"&&GBBak1ID.value.Trim() ==\"\")\n");
            builder.Append("     {\n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         vErrorMessage+=\"\\n请输入文字.\";\n");
            builder.Append("          bFindError=true;\n");
            builder.Append("    }\n");
            builder.Append("    }\n");
            builder.Append("    \n");
            builder.Append("     if( " + strArray[7] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[7] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBBak2ID.value=GBBak2ID.value.replace(vFilter,'');\n");
            builder.Append("     if(" + strArray[7] + "==\"1\"&&GBBak2ID.value.Trim() ==\"\")\n");
            builder.Append("     {\n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         vErrorMessage+=\"\\n请输入文字.\";\n");
            builder.Append("          bFindError=true;\n");
            builder.Append("    }\n");
            builder.Append("    }\n");
            builder.Append("    \n");
            builder.Append("     if( " + strArray[8] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[8] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBContentID.value=GBContentID.value.replace(vFilter,'');\n");
            builder.Append("     if(" + strArray[8] + "==\"1\"&&GBContentID.value.Trim() ==\"\")\n");
            builder.Append("     {\n");
            builder.Append("         //var Name=document.getElementById (\"RequiredFieldValidator1\");\n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         vErrorMessage+=\"\\n请填写内容.\";\n");
            builder.Append("         bFindError=true;\n");
            builder.Append("     }\n");
            builder.Append("     }\n");
            builder.Append("     \n");
            builder.Append("     if(" + strArray[9] + "!=\"0\"){\n");
            builder.Append("     var vCode=getCookie(\"MsgVeriCode\");\n");
            builder.Append("     if( vCode==null)\n");
            builder.Append("     {alert('验证码已失效，请刷新验证码重新获得！');return true;}\n");
            builder.Append("     if(GBVCodeID.value.Trim().toLowerCase()!=vCode.toLowerCase())\n");
            builder.Append("     {alert('验证码不正确！');return true;}\n");
            builder.Append("     }\n");
            builder.Append("     return bFindError;\n");
            builder.Append("}\n");
            builder.Append("function Clear()\n");
            builder.Append("{");
            builder.Append("     if( " + strArray[0] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[0] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBNameID.value =\"\";\n");
            builder.Append("     }\n");
            builder.Append("     if( " + strArray[1] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[1] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBTelID.value =\"\";\n");
            builder.Append("     }\n");
            builder.Append("     if( " + strArray[2] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[2] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBMobileID.value =\"\";\n");
            builder.Append("     }\n");
            builder.Append("     if( " + strArray[3] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[3] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBEmailID.value =\"\";\n");
            builder.Append("     }\n");
            builder.Append("     if( " + strArray[4] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[4] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBAddID.value =\"\";\n");
            builder.Append("     }\n");
            builder.Append("     if( " + strArray[5] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[5] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBTitleID.value =\"\";\n");
            builder.Append("     }\n");
            builder.Append("     if( " + strArray[6] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[6] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBBak1ID.value =\"\";\n");
            builder.Append("     }\n");
            builder.Append("     if( " + strArray[7] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[7] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBBak2ID.value =\"\";\n");
            builder.Append("     }\n");
            builder.Append("     if( " + strArray[8] + "!=\"0\"){\n");
            builder.Append("     var Name=document.getElementById (\"RFV" + astrFieldID[8] + "\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     GBContentID.value =\"\";\n");
            builder.Append("     }\n");
            builder.Append("}\n");
            builder.Append("</script>\n");
            return builder.ToString();
        }

        private void GetCustomTags(string strMdl, out string strCusTagsName, out string strCusTagsInfo)
        {
            strCusTagsName = "[QH:ProductCusName]";
            strCusTagsInfo = "[QH:ProductCusInfo]";
            string str = strMdl;
            if (str != null)
            {
                if (!(str == "4"))
                {
                    if (!(str == "5"))
                    {
                        return;
                    }
                }
                else
                {
                    strCusTagsName = "[QH:DownloadCusName]";
                    strCusTagsInfo = "[QH:DownloadCusInfo]";
                    return;
                }
                strCusTagsName = "[QH:PictureCusName]";
                strCusTagsInfo = "[QH:PictureCusInfo]";
            }
        }

        private void GetCustumPara(string strTemp, string strClmnIDIn, string strMdl)
        {
            if (strTemp.IndexOf("CusName:") >= 0)
            {
                this.listStrCusField = this.GetCusFieldInfo(strTemp, strMdl);
                this.GetCusFieldInfoID_Name_pList(this.listStrCusField, strMdl, strClmnIDIn + ",'0'");
            }
        }

        private string GetDepthTemplate(DataRow dw, string strTMName)
        {
            string str = dw[strTMName].ToString().Trim();
            string str2 = dw["depth"].ToString().Trim();
            string str3 = "";
            if ((str != "") || (str2 == "0"))
            {
                return str;
            }
            DataRow[] rowArray = this.dTableClmn.Select("ID=" + dw["ParentID"].ToString().Trim());
            str = rowArray[0][strTMName].ToString().Trim();
            str3 = rowArray[0]["ProductTMInherit"].ToString().Trim();
            if ((str != "") && (str3 == "1"))
            {
                return str;
            }
            if (str2 != "1")
            {
                rowArray = this.dTableClmn.Select("ID=" + rowArray[0]["ParentID"].ToString().Trim());
                str = rowArray[0][strTMName].ToString().Trim();
                str3 = rowArray[0]["ProductTMInherit"].ToString().Trim();
                if ((str != "") && (str3 == "1"))
                {
                    return str;
                }
            }
            return "";
        }

        private bool GetDetailsUrlPath(out string strUrlPath, string strPath, string strRootDir)
        {
            if (strRootDir != "")
            {
                this.CheckRootDir(strRootDir);
            }
            strRootDir = (strRootDir == "") ? "" : (strRootDir + "/");
            bool flag = true;
            strUrlPath = strRootDir + strPath;
            try
            {
                DirectoryInfo info = new DirectoryInfo(this.CSPage.Server.MapPath("../" + strRootDir + strPath));
                if (!info.Exists)
                {
                    info.Create();
                }
            }
            catch (Exception exception)
            {
                flag = false;
                SystemError.CreateErrorLog("创建目录" + strUrlPath + "错误： " + exception.ToString());
            }
            return flag;
        }

        private string GetFenLanEnglish(DataRow dRow)
        {
            string str = dRow["C_EnTitle"].ToString().Trim();
            if (str != "")
            {
                return str;
            }
            string str2 = dRow["depth"].ToString();
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "1"))
            {
                if (str2 != "2")
                {
                    return str;
                }
            }
            else
            {
                return this.dTableClmn.Select("id=" + dRow["ParentID"])[0]["C_EnTitle"].ToString();
            }
            DataRow[] rowArray = this.dTableClmn.Select("id=" + dRow["ParentID"]);
            str = rowArray[0]["C_EnTitle"].ToString().Trim();
            if (str != "")
            {
                return str;
            }
            return this.dTableClmn.Select("id=" + rowArray[0]["ParentID"])[0]["C_EnTitle"].ToString();
        }

        private string GetGuestBookJsScript(string strPageDepth, string[] astrMsgSet)
        {
            string pagePre = this.GetPagePre(strPageDepth);
            if (this.bIsMobile)
            {
                pagePre = "../" + pagePre;
            }
            StringBuilder builder = new StringBuilder("");
            builder.Append("<script >\n");
            builder.Append("var IsCheckCode=\"" + astrMsgSet[7] + "\";\n");
            builder.Append("var NameFill=\"" + astrMsgSet[1] + "\";\n");
            builder.Append("var TelFill=\"" + astrMsgSet[2] + "\";\n");
            builder.Append("var EmailFill=\"" + astrMsgSet[3] + "\";\n");
            builder.Append("var MobileFill=\"" + astrMsgSet[4] + "\";\n");
            builder.Append("var ContactFill=\"" + astrMsgSet[5] + "\";\n");
            builder.Append("var TitlelFill=\"" + astrMsgSet[6] + "\";\n");
            builder.Append("var ContentFill=\"" + astrMsgSet[8] + "\";\n");
            builder.Append("function loadXMLDoc()\n");
            builder.Append("{\n");
            builder.Append("    var xmlhttp;\n");
            builder.Append("    if (window.XMLHttpRequest)\n");
            builder.Append("      {// code for IE7+, Firefox, Chrome, Opera, Safari\n");
            builder.Append("          xmlhttp=new XMLHttpRequest();\n");
            builder.Append("      }\n");
            builder.Append("    else\n");
            builder.Append("      {// code for IE6, IE5\n");
            builder.Append("          xmlhttp=new ActiveXObject(\"Microsoft.XMLHTTP\");\n");
            builder.Append("      }\n");
            builder.Append("      xmlhttp.onreadystatechange=function()\n");
            builder.Append("      {\n");
            builder.Append("            //alert (xmlhttp.responseText);\n");
            builder.Append("     if (xmlhttp.readyState==4 && xmlhttp.status==200)\n");
            builder.Append("        {\n");
            builder.Append("            //document.getElementById(\"myDiv\").innerHTML=xmlhttp.responseText;\n");
            builder.Append("            UserName.value=\"\";\n");
            builder.Append("            email.value=\"\";\n");
            builder.Append("            tel.value=\"\";\n");
            builder.Append("            mobile.value=\"\";\n");
            builder.Append("            title.value=\"\";\n");
            builder.Append("            content.value=\"\";\n");
            builder.Append("            addr.value=\"\";\n");
            builder.Append("            VCode.value=\"\";\n");
            builder.Append("           alert (xmlhttp.responseText);\n");
            builder.Append("        }\n");
            builder.Append("      }\n");
            builder.Append("    xmlhttp.open(\"POST\",\"" + pagePre + "Ajax/AjaxGuestBook.aspx\",true);\n");
            builder.Append("    xmlhttp.setRequestHeader(\"Content-type\",\"application/x-www-form-urlencoded;charset=utf-8\");\n");
            builder.Append("    var vPost=\"UserName=\"+UserName.value+\"&email=\"+email.value\n");
            builder.Append("     +\"&tel=\"+tel.value+\"&addr=\"+addr.value+\"&title=\"+title.value+\"&content=\"+content.value+\"&Mbl=\"+mobile.value;\n");
            builder.Append("    xmlhttp.send(vPost);\n");
            builder.Append("}\n");
            builder.Append("function Click_submit()\n");
            builder.Append("{\n");
            builder.Append("    \n");
            builder.Append("    if(!CheckData())\n");
            builder.Append("    {\n");
            builder.Append("        loadXMLDoc();\n");
            builder.Append("    }\n");
            builder.Append("}\n");
            builder.Append("String.prototype.Trim = function()\n");
            builder.Append("{\n");
            builder.Append("\treturn this.replace( /(^\\s*)|(\\s*$)/g, '' ) ;\n");
            builder.Append("}\n");
            builder.Append("function getimgcode() \n");
            builder.Append("{ \n");
            builder.Append("    var getimagecode = document.getElementById(\"getcode\"); \n");
            builder.Append("    getimagecode.src = \"" + pagePre + "Ajax/GuestBook/VerifyCodeMsg.aspx?\"+Math.random();\n");
            builder.Append("} \n");
            builder.Append("function getCookie(name)   \n");
            builder.Append("{  \n");
            builder.Append("  var arr = document.cookie.match(new RegExp(\"(^| )\"+name+\"=([^;]*)(;|$)\"));\n");
            builder.Append("  if(arr != null) return decodeURIComponent(arr[2]); return null;  \n");
            builder.Append("} \n");
            builder.Append("function CheckData()\n");
            builder.Append("{\n");
            builder.Append("     var bFindError=false;\n");
            builder.Append("     var bFindChar=false;\n");
            builder.Append("     var vFilter=/[" + astrMsgSet[0].Replace("|", "") + "]/g;\n");
            builder.Append("     var Name=document.getElementById (\"RequiredFieldValidator1\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     UserName.value=UserName.value.replace(vFilter,'');\n");
            builder.Append("     if(NameFill==\"True\"&&UserName.value.Trim() ==\"\")\n");
            builder.Append("     {\n");
            builder.Append("         //var Name=document.getElementById (\"RequiredFieldValidator1\");\n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         bFindError=true;\n");
            builder.Append("     }\n");
            builder.Append("     \n");
            builder.Append("     \n");
            builder.Append("     var email1=document.getElementById (\"RegularExpressionValidator1\");\n");
            builder.Append("     email1.style.visibility=\"hidden\";\n");
            builder.Append("     var reg = /^$|^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$/;\n");
            builder.Append("     if(EmailFill==\"True\") reg = /^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$/;\n");
            builder.Append("     if(!reg.test(email.value.Trim()))\n");
            builder.Append("     {    // w+([-+.]w+)*@w+([-.]w+)*.w+([-.]w+)* \n");
            builder.Append("         //var Name=document.getElementById (\"RequiredFieldValidator1\");\n");
            builder.Append("         email1.style.visibility=\"visible\";\n");
            builder.Append("         bFindError=true;\n");
            builder.Append("     }\n");
            builder.Append("     \n");
            builder.Append("     var Name=document.getElementById (\"TelVerify\");\n");
            builder.Append("     var reg = /^$|^\\+?\\d[-\\d]{6,}$/;\n");
            builder.Append("     if(TelFill==\"True\") reg = /^\\+?\\d[-\\d]{6,}$/;\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     if(!reg.test(tel.value.Trim()))\n");
            builder.Append("     {    // w+([-+.]w+)*@w+([-.]w+)*.w+([-.]w+)* \n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         bFindError=true;\n");
            builder.Append("     }\n");
            builder.Append("     \n");
            builder.Append("     var Name=document.getElementById (\"MobileVerify\");\n");
            builder.Append("     var reg = /^$|^1[0-9]{10}$/;\n");
            builder.Append("     if(MobileFill==\"True\") reg = /^1[0-9]{10}$/;\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     if(!reg.test(mobile.value.Trim()))\n");
            builder.Append("     {    \n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         bFindError=true;\n");
            builder.Append("     }\n");
            builder.Append("     \n");
            builder.Append("     var Name=document.getElementById (\"AddrVerify\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     addr.value=addr.value.replace(vFilter,'');\n");
            builder.Append("     if(ContactFill==\"True\"&&addr.value.Trim() ==\"\")\n");
            builder.Append("     {    ");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         bFindError=true;\n");
            builder.Append("     }\n");
            builder.Append("     \n");
            builder.Append("     var Name=document.getElementById (\"RequiredFieldValidator2\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     title.value=title.value.replace(vFilter,'');\n");
            builder.Append("     if(TitlelFill==\"True\"&&title.value.Trim() ==\"\")\n");
            builder.Append("     {\n");
            builder.Append("         //var Name=document.getElementById (\"RequiredFieldValidator1\");\n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("          bFindError=true;\n");
            builder.Append("    }\n");
            builder.Append("     var Name=document.getElementById (\"RequiredFieldValidator3\");\n");
            builder.Append("     Name.style.visibility=\"hidden\";\n");
            builder.Append("     content.value=content.value.replace(vFilter,'');\n");
            builder.Append("     if(ContentFill==\"True\"&&content.value.Trim() ==\"\")\n");
            builder.Append("     {\n");
            builder.Append("         //var Name=document.getElementById (\"RequiredFieldValidator1\");\n");
            builder.Append("         Name.style.visibility=\"visible\";\n");
            builder.Append("         bFindError=true;\n");
            builder.Append("     }\n");
            builder.Append("     \n");
            builder.Append("     if(UserName.value.indexOf('&') >=0||email.value.indexOf('&') >=0||tel.value.indexOf('&') >=0\n");
            builder.Append("     ||addr.value.indexOf('&') >=0||title.value.indexOf('&') >=0||content.value.indexOf('&') >=0)\n");
            builder.Append("     {\n");
            builder.Append("         bFindChar=true;\n");
            builder.Append("     }\n");
            builder.Append("     if(bFindChar )\n");
            builder.Append("     {\n");
            builder.Append("        bFindError=true;\n");
            builder.Append("        alert(\"不能含有字符 & \");\n");
            builder.Append("     }\n");
            builder.Append("     if(IsCheckCode==\"True\"){\n");
            builder.Append("     var vCode=getCookie(\"MsgVeriCode\");\n");
            builder.Append("     if( vCode==null)\n");
            builder.Append("     {alert('验证码已失效，请刷新验证码重新获得！');return true;}\n");
            builder.Append("     if(VCode.value.Trim().toLowerCase()!=vCode.toLowerCase())\n");
            builder.Append("     {alert('验证码不正确！');return true;}\n");
            builder.Append("     }\n");
            builder.Append("     return bFindError;\n");
            builder.Append("}\n");
            builder.Append("function Clear()\n");
            builder.Append("{");
            builder.Append("   UserName.value =\"\";\n");
            builder.Append("   email.value =\"\";\n");
            builder.Append("   tel.value =\"\";\n");
            builder.Append("   addr.value =\"\";\n");
            builder.Append("   title.value =\"\";\n");
            builder.Append("   content.value =\"\";\n");
            builder.Append("}\n");
            builder.Append("</script>\n");
            return builder.ToString();
        }

        private string GetGuestBookUI(string strPageDepth, string[] astrMsgSet)
        {
            string str = (astrMsgSet[7] == "True") ? "" : "none";
            string pagePre = this.GetPagePre(strPageDepth);
            if (this.bIsMobile)
            {
                pagePre = "../" + pagePre;
            }
            string str3 = this.bIsMobile ? "width=60px" : "";
            string str4 = this.bIsMobile ? "100" : "199";
            string str5 = this.bIsMobile ? "width=180px" : "";
            string str6 = this.bIsMobile ? "140" : "312";
            string str7 = "";
            string str8 = "";
            string str9 = "";
            string str10 = "";
            string str11 = "";
            string str12 = "";
            string str13 = "";
            string str14 = "";
            if (astrMsgSet[9] == "False")
            {
                astrMsgSet[1] = "False";
                str7 = "none";
            }
            else
            {
                str14 = "none";
            }
            if (astrMsgSet[10] == "False")
            {
                astrMsgSet[2] = "False";
                str8 = "none";
            }
            else
            {
                str14 = "none";
            }
            if (astrMsgSet[11] == "False")
            {
                astrMsgSet[3] = "False";
                str9 = "none";
            }
            else
            {
                str14 = "none";
            }
            if (astrMsgSet[12] == "False")
            {
                astrMsgSet[4] = "False";
                str10 = "none";
            }
            else
            {
                str14 = "none";
            }
            if (astrMsgSet[13] == "False")
            {
                astrMsgSet[5] = "False";
                str11 = "none";
            }
            else
            {
                str14 = "none";
            }
            if (astrMsgSet[14] == "False")
            {
                astrMsgSet[6] = "False";
                str12 = "none";
            }
            else
            {
                str14 = "none";
            }
            if (astrMsgSet[15] == "False")
            {
                astrMsgSet[8] = "False";
                str13 = "none";
            }
            else
            {
                str14 = "none";
            }
            string str15 = (astrMsgSet[0x10].Trim() == "") ? "您的姓名" : astrMsgSet[0x10].Trim();
            string str16 = (astrMsgSet[0x11].Trim() == "") ? "联系电话" : astrMsgSet[0x11].Trim();
            string str17 = (astrMsgSet[0x12].Trim() == "") ? "您的邮箱" : astrMsgSet[0x12].Trim();
            string str18 = (astrMsgSet[0x13].Trim() == "") ? "手&nbsp;&nbsp;&nbsp;&nbsp;机" : astrMsgSet[0x13].Trim();
            string str19 = (astrMsgSet[20].Trim() == "") ? "您的地址" : astrMsgSet[20].Trim();
            string str20 = (astrMsgSet[0x15].Trim() == "") ? "留言标题" : astrMsgSet[0x15].Trim();
            string str21 = (astrMsgSet[0x16].Trim() == "") ? "留言内容" : astrMsgSet[0x16].Trim();
            StringBuilder builder = new StringBuilder("");
            builder.Append("<table width=\"98%\" height=\"308\" border=\"0\" align=\"" + (this.bIsMobile ? "left" : "center") + "\" cellpadding=\"0\" cellspacing=\"8\">\n");
            builder.Append("<tr style=\"display:" + str7 + ";\">\n");
            builder.Append("  <td align=\"right\" " + str3 + " >" + str15 + "：</td>\n");
            builder.Append("  <td align=\"left\"><input name=\"UserName\" type=\"text\" id=\"UserName\" style=\"width:" + str4 + "px;\" />\n");
            builder.Append("      <span id=\"RequiredFieldValidator1\" style=\"visibility:hidden;\">请输入您的名字！</span></td>\n");
            builder.Append("  </tr>\n");
            builder.Append("<tr style=\"display:" + str9 + ";\">\n");
            builder.Append("  <td align=\"right\" style=\"height: 24px\" " + str3 + " >" + str17 + "：</td>\n");
            builder.Append("  <td align=\"left\" style=\"height: 24px\"><input name=\"email\" type=\"text\" id=\"email\" style=\"width:" + str4 + "px;\" />\n");
            builder.Append("    <span id=\"RegularExpressionValidator1\" style=\"visibility:hidden;\">请您正确填写邮件地址！</span></td>\n");
            builder.Append("  </tr>\n");
            builder.Append("<tr style=\"display:" + str8 + ";\">\n");
            builder.Append("  <td align=\"right\" style=\"height: 24px\" " + str3 + " >" + str16 + "：</td>\n");
            builder.Append("  <td align=\"left\" style=\"height: 24px\"><input name=\"tel\" type=\"text\" id=\"tel\" style=\"width:" + str4 + "px;\" />\n");
            builder.Append("  <span id=\"TelVerify\" style=\"visibility:hidden;\">请您正确填写联系电话！</span></td>\n");
            builder.Append("  </tr>\n");
            builder.Append("<tr style=\"display:" + str10 + ";\">\n");
            builder.Append("  <td align=\"right\" style=\"height: 24px\" " + str3 + " >" + str18 + "：</td>\n");
            builder.Append("  <td align=\"left\" style=\"height: 24px\"><input name=\"mobile\" type=\"text\" id=\"mobile\" style=\"width:" + str4 + "px;\" />\n");
            builder.Append("  <span id=\"MobileVerify\" style=\"visibility:hidden;\">请您填写正确手机号码！</span></td>\n");
            builder.Append("  </tr>\n");
            builder.Append("<tr style=\"display:" + str11 + ";\">\n");
            builder.Append("  <td align=\"right\" " + str3 + " >" + str19 + "：</td>\n");
            builder.Append("  <td align=\"left\"><input name=\"addr\" type=\"text\" id=\"addr\" style=\"width:" + str4 + "px;\" />\n");
            builder.Append("  <span id=\"AddrVerify\" style=\"visibility:hidden;\">请您填写地址！</span></td>\n");
            builder.Append("  </tr>\n");
            builder.Append("<tr style=\"display:" + str12 + ";\">\n");
            builder.Append("  <td align=\"right\" " + str3 + " >" + str20 + "：</td>\n");
            builder.Append("  <td align=\"left\"><input name=\"title\" type=\"text\" id=\"title\" style=\"width:" + str4 + "px;\" />\n");
            builder.Append("      <span id=\"RequiredFieldValidator2\" style=\"visibility:hidden;\">请填写标题</span></td>\n");
            builder.Append("  </tr>\n");
            builder.Append("<tr style=\"display:" + str13 + ";\">\n");
            builder.Append("  <td align=\"right\" " + str3 + " >" + str21 + "：</td>\n");
            builder.Append("  <td align=\"left\"><textarea name=\"content\" rows=\"2\" cols=\"20\" wrap=\"off\" id=\"content\" style=\"height:148px;width:" + str6 + "px;\">\n");
            builder.Append("</textarea>\n");
            builder.Append("      <span id=\"RequiredFieldValidator3\" style=\"visibility:hidden;\">请填写内容！</span></td>\n");
            builder.Append("  </tr>\n");
            builder.Append("<tr style=\"display:" + str + ";\">\n");
            builder.Append("  <td align=\"right\" " + str3 + " >验证码：</td>\n");
            builder.Append("  <td align=\"left\"><INPUT id=\"VCode\" type=\"text\" size=4 maxlength=4 > \n");
            builder.Append("      <A href=\"javascript:getimgcode()\" ><img src=\"" + pagePre + "Ajax/GuestBook/VerifyCodeMsg.aspx\" id=\"getcode\" style=\"border:1px #919a99 solid; width:52px; height:23px;\" alt=\"看不清，点击换一张\"  ></A></td>\n");
            builder.Append("  </tr>\n");
            builder.Append("<tr style=\"display:" + str14 + ";\">\n");
            builder.Append("  <td colspan=2 >请在《留言系统设置》设置要显示的留言项目。</td>\n");
            builder.Append("  </tr>\n");
            builder.Append("<tr>\n");
            builder.Append("  <td colspan=\"2\"><table width=\"70%\" border=\"0\" cellspacing=\"8\" cellpadding=\"0\">\n");
            builder.Append("      <tr>\n");
            builder.Append("        <td align=\"center\" " + str5 + " ><input type=\"button\" name=\"Button1\" value=\"提交\" onclick=\"Click_submit()\" id=\"Button1\" />\n");
            builder.Append("          &nbsp;&nbsp;&nbsp;&nbsp;<input type=\"reset\" name=\"Breset\" value=\"重置\"  onclick=\"Clear()\" /></td>\n");
            builder.Append("      </tr>\n");
            builder.Append("    </table></td>\n");
            builder.Append("  </tr>\n");
            builder.Append(" </table>\n");
            return builder.ToString();
        }

        private string GetHomeNav(string strLoopContent, string strPageDepth)
        {
            strLoopContent = strLoopContent.Replace("[QH:Title]", "首页");
            strLoopContent = strLoopContent.Replace("[QH:LinkPath]", this.GetPagePre(strPageDepth));
            return strLoopContent;
        }

        private string GetHrAcceptJsScript(string strPageDepth, string strFilter)
        {
            string pagePre = this.GetPagePre(strPageDepth);
            StringBuilder builder = new StringBuilder("");
            builder.Append("<script >\n");
            builder.Append("function loadXMLDoc()\n");
            builder.Append("{\n");
            builder.Append("    var xmlhttp;\n");
            builder.Append("    if (window.XMLHttpRequest)\n");
            builder.Append("      {// code for IE7+, Firefox, Chrome, Opera, Safari\n");
            builder.Append("          xmlhttp=new XMLHttpRequest();\n");
            builder.Append("      }\n");
            builder.Append("    else\n");
            builder.Append("      {// code for IE6, IE5\n");
            builder.Append("          xmlhttp=new ActiveXObject(\"Microsoft.XMLHTTP\");\n");
            builder.Append("      }\n");
            builder.Append("      xmlhttp.onreadystatechange=function()\n");
            builder.Append("      {\n");
            builder.Append("            //alert (xmlhttp.responseText);\n");
            builder.Append("     if (xmlhttp.readyState==4 && xmlhttp.status==200)\n");
            builder.Append("        {\n");
            builder.Append("            //document.getElementById(\"myDiv\").innerHTML=xmlhttp.responseText;\n");
            builder.Append("           alert (xmlhttp.responseText);Clear();\n");
            builder.Append("        }\n");
            builder.Append("      }\n");
            builder.Append("    xmlhttp.open(\"POST\",\"" + pagePre + "Ajax/AjaxHrAccept.aspx\",true);\n");
            builder.Append("    xmlhttp.setRequestHeader(\"Content-type\",\"application/x-www-form-urlencoded;charset=utf-8\");\n");
            builder.Append("     var vFilter=/[" + strFilter.Replace("|", "") + "]/g;\n");
            builder.Append("    var vPost=\"Position=\"+jobname.value+\"&UserName=\"+Name2.value.replace(/\\s/g,'').replace(vFilter,'')+\"&Sex=\"+Sex1.value+\"&Marry=\"+Marry1.value\n");
            builder.Append("     +\"&Birthday=\"+Birthday1.value.replace(/\\s/g,'')+\"&stature=\"+stature1.value.replace(vFilter,'')+\"&School=\"+School1.value.replace(vFilter,'')+\"&Studydegree=\"+Studydegree1.value.replace(vFilter,'')\n");
            builder.Append("     +\"&Specialty=\"+Specialty1.value.replace(vFilter,'')+\"&Gradyear=\"+Gradyear1.value.replace(vFilter,'')+\"&Residence=\"+Residence1.value.replace(vFilter,'')+\"&Edulevel=\"+Edulevel1.value.replace(vFilter,'')\n");
            builder.Append("     +\"&Experience=\"+Experience1.value.replace(vFilter,'')+\"&Phone=\"+Phone1.value+\"&Mobile=\"+Mobile1.value.replace(/\\s/g,'')+\"&Email=\"+Email1.value.replace(/\\s/g,'')\n");
            builder.Append("     +\"&Add=\"+Add1.value.replace(vFilter,'')+\"&Postcode=\"+Postcode1.value;\n");
            builder.Append("    xmlhttp.send(vPost);\n");
            builder.Append("}\n");
            builder.Append("function Click_submit()\n");
            builder.Append("{\n");
            builder.Append("    \n");
            builder.Append("    if(!CheckData())\n");
            builder.Append("    {\n");
            builder.Append("        loadXMLDoc();\n");
            builder.Append("    }\n");
            builder.Append("}\n");
            builder.Append("function CheckData()\n");
            builder.Append("{\n");
            builder.Append("     var bFindError=false;\n");
            builder.Append("     var bFindChar=false;\n");
            builder.Append("    \n");
            builder.Append("     var regK = /^$/;\n");
            builder.Append("     if(regK.test(jobname.value))\n");
            builder.Append("     {\n");
            builder.Append("         alert(\"职位不能为空！\");return true;\n");
            builder.Append("     }\n");
            builder.Append("     if(regK.test(Name2.value.replace(/\\s/g,'')))\n");
            builder.Append("     {\n");
            builder.Append("         alert(\"姓名不能为空！\");return true;\n");
            builder.Append("     }\n");
            builder.Append("     if(regK.test(Birthday1.value.replace(/\\s/g,'')))\n");
            builder.Append("     {\n");
            builder.Append("         alert(\"出生日期不能为空！\");return true;\n");
            builder.Append("     }\n");
            builder.Append("     var regD=/^(?:(?!0000)[0-9]{4}([-/.]?)(?:(?:0?[1-9]|1[0-2])([-/.]?)(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])([-/.]?)(?:29|30)|(?:0?[13578]|1[02])([-/.]?)31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)([-/.]?)0?2([-/.]?)29)$/;\n");
            builder.Append("     if(!regD.test(Birthday1.value.replace(/\\s/g,'')))\n");
            builder.Append("     {\n");
            builder.Append("         alert(\"出生日期格式不对！\");return true;\n");
            builder.Append("     }\n");
            builder.Append("     if(regK.test(Residence1.value.replace(/\\s/g,'')))\n");
            builder.Append("     {\n");
            builder.Append("         alert(\"户籍地不能为空！\");return true;\n");
            builder.Append("     }\n");
            builder.Append("     if(regK.test(Edulevel1.value.replace(/\\s/g,'')))\n");
            builder.Append("     {\n");
            builder.Append("         alert(\"教育经历不能为空！\");return true;\n");
            builder.Append("     }\n");
            builder.Append("     if(regK.test(Experience1.value.replace(/\\s/g,'')))\n");
            builder.Append("     {\n");
            builder.Append("         alert(\"工作经历不能为空！\");return true;\n");
            builder.Append("     }\n");
            builder.Append("     if(regK.test(Mobile1.value.replace(/\\s/g,'')))\n");
            builder.Append("     {\n");
            builder.Append("         alert(\"手机号码不能为空！\");return true;\n");
            builder.Append("     }\n");
            builder.Append("     var regM =/^1[0-9]{10}$/;\n");
            builder.Append("      if(!regM.test(Mobile1.value.replace(/\\s/g,'')))\n");
            builder.Append("     {\n");
            builder.Append("         alert(\"手机号码格式不对！\");return true;\n");
            builder.Append("     }\n");
            builder.Append("     if(regK.test(Email1.value.replace(/\\s/g,'')))\n");
            builder.Append("     {\n");
            builder.Append("         alert(\"E-mail地址不能为空！\");return true;\n");
            builder.Append("     }\n");
            builder.Append("     var regE = /^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$/;\n");
            builder.Append("      if(!regE.test(Email1.value.replace(/\\s/g,'')))\n");
            builder.Append("     {\n");
            builder.Append("         alert(\"E-mail地址格式不对！\");return true;\n");
            builder.Append("     }\n");
            builder.Append("     var regP = /^$|^[1-9]\\d{5}(?!\\d)$/;\n");
            builder.Append("      if(!regP.test(Postcode1.value))\n");
            builder.Append("     {\n");
            builder.Append("         alert(\"邮政编码格式不对！\");return true;\n");
            builder.Append("     }\n");
            builder.Append("     if(Name2.value.indexOf('&') >=0||Birthday1.value.indexOf('&') >=0||stature1.value.indexOf('&') >=0\n");
            builder.Append("     ||School1.value.indexOf('&') >=0||Studydegree1.value.indexOf('&') >=0||Specialty1.value.indexOf('&') >=0\n");
            builder.Append("     ||Gradyear1.value.indexOf('&') >=0||Residence1.value.indexOf('&') >=0||Edulevel1.value.indexOf('&') >=0\n");
            builder.Append("     ||Experience1.value.indexOf('&') >=0||Phone1.value.indexOf('&') >=0||Mobile1.value.indexOf('&') >=0\n");
            builder.Append("     ||Email1.value.indexOf('&') >=0||Add1.value.indexOf('&') >=0||Postcode1.value.indexOf('&') >=0)\n");
            builder.Append("     {\n");
            builder.Append("         bFindChar=true;\n");
            builder.Append("     }\n");
            builder.Append("     if(bFindChar )\n");
            builder.Append("     {\n");
            builder.Append("        bFindError=true;\n");
            builder.Append("        alert(\"所填写的文字中不能含有字符 &    \");\n");
            builder.Append("     }\n");
            builder.Append("     return bFindError;\n");
            builder.Append("}\n");
            builder.Append("function Clear()\n");
            builder.Append("{ \n");
            builder.Append("   Name2.value =\"\";\n");
            builder.Append("   Sex1.selectedIndex=0;\n");
            builder.Append("   Marry1.selectedIndex =0;\n");
            builder.Append("   Birthday1.value =\"\";\n");
            builder.Append("   stature1.value =\"\";\n");
            builder.Append("   School1.value =\"\";\n");
            builder.Append("   Studydegree1.value =\"\";\n");
            builder.Append("   Specialty1.value =\"\";\n");
            builder.Append("   Gradyear1.value =\"\";\n");
            builder.Append("   Residence1.value =\"\";\n");
            builder.Append("   Edulevel1.value =\"\";\n");
            builder.Append("   Experience1.value =\"\";\n");
            builder.Append("   Phone1.value =\"\";\n");
            builder.Append("   Mobile1.value =\"\";\n");
            builder.Append("   Email1.value =\"\";\n");
            builder.Append("   Add1.value =\"\";\n");
            builder.Append("   Postcode1.value =\"\";\n");
            builder.Append("}\n");
            builder.Append("</script>\n");
            return builder.ToString();
        }

        private string GetHrAcceptUI()
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append("      <TABLE border=0 width=\"98%\" height=450>\n");
            builder.Append("        <TBODY>\n");
            builder.Append("        <TR vAlign=top>\n");
            builder.Append("          <TD>\t\t\t  \n");
            builder.Append("            <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\n");
            builder.Append("              <TR vAlign=top > \n");
            builder.Append("                <TD  width=\"80%\" height=\"18\"> \n");
            builder.Append("                    <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\n");
            builder.Append("                      <tr> \n");
            builder.Append("                        <td><div align=\"center\"></div></td>\n");
            builder.Append("                      </tr>\n");
            builder.Append("                    </table>\n");
            builder.Append("                    <table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" >\n");
            builder.Append("                      <tr> \n");
            builder.Append("                        \n");
            builder.Append("                        <td width=\"10%\" > \n");
            builder.Append("                          <div align=\"right\"><font color=#000066>职　　位：&nbsp; </font></div></td>\n");
            builder.Append("                        <td width=\"70%\"><TABLE width=\"100%\" border=0 cellpadding=\"2\" cellspacing=\"1\">\n");
            builder.Append("                            <TBODY>\n");
            builder.Append("                              <TR> \n");
            builder.Append("                                  <td width=\"19%\"  > </td>\n");
            builder.Append("                                  <TD width=\"81%\" height=\"20\" align=left > \n");
            builder.Append("                                  <input name=\"jobname\" type=\"text\" id=\"jobname\" value=\"\" size=\"36\" disabled=\"disabled\" />\n");
            builder.Append("                                  </TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                            </TBODY>\n");
            builder.Append("                          </TABLE></td>\n");
            builder.Append("                      </tr>\n");
            builder.Append("                      <tr> \n");
            builder.Append("                        <td width=\"10%\"  > \n");
            builder.Append("                          <div align=\"right\"><font color=#000066>个人资料：&nbsp;</font></div></td>\n");
            builder.Append("                        <td valign=\"top\"><TABLE width=\"100%\" border=0 cellpadding=\"2\" cellspacing=\"1\">\n");
            builder.Append("                            <TBODY>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD width=\"19%\" height=\"20\" align=right>姓名：</TD>\n");
            builder.Append("                                <TD width=\"81%\" align=left > \n");
            builder.Append("                                   <input name=\"Name2\" type=\"text\" id=\"Name2\" />                                        \n");
            builder.Append("                                       * </TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD align=right \n");
            builder.Append("                          height=20>性别：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <select name=\"Sex1\" id=\"Sex1\">\n");
            builder.Append("                                      <option selected=\"selected\" value=\"男\">男</option>\n");
            builder.Append("                                      <option value=\"女\">女</option>\n");
            builder.Append("                                  </select>\n");
            builder.Append("                                  *</TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD align=right \n");
            builder.Append("                          height=20>婚姻状况：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <select name=\"Marry1\" id=\"Marry1\">\n");
            builder.Append("                                      <option selected=\"selected\" value=\"未婚\">未婚</option>\n");
            builder.Append("                                      <option value=\"已婚\">已婚</option>\n");
            builder.Append("                                  </select> </TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD align=right \n");
            builder.Append("                          height=20>出生日期：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <input name=\"Birthday1\" type=\"text\" id=\"Birthday1\" />\n");
            builder.Append("                                  *出生日期（如：1978-04-24）</TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD align=right \n");
            builder.Append("                          height=20>身高：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <input name=\"stature1\" type=\"text\" id=\"stature1\" size=\"15\" maxlength=\"3\" />\n");
            builder.Append("                                  (cm)（如：填 178）</TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD align=right \n");
            builder.Append("                          height=20>毕业院校：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <input name=\"School1\" type=\"text\" id=\"School1\" size=\"40\" /></TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD align=right \n");
            builder.Append("                          height=20>学历：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <input name=\"Studydegree1\" type=\"text\" id=\"Studydegree1\" size=\"30\" maxlength=\"50\" /></TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD align=right \n");
            builder.Append("                          height=20>专业：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <input name=\"Specialty1\" type=\"text\" id=\"Specialty1\" size=\"30\" maxlength=\"50\" /></TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD align=right \n");
            builder.Append("                          height=20>毕业时间：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <input name=\"Gradyear1\" type=\"text\" id=\"Gradyear1\" size=\"16\" /></TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD align=right \n");
            builder.Append("                          height=20>户籍地：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <input name=\"Residence1\" type=\"text\" id=\"Residence1\" size=\"40\" maxlength=\"100\" />\n");
            builder.Append("                                  *</TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                            </TBODY>\n");
            builder.Append("                          </TABLE></td>\n");
            builder.Append("                      </tr>\n");
            builder.Append("                      <tr> \n");
            builder.Append("                        <td width=\"76\"  > \n");
            builder.Append("                          <div align=\"right\"><font color=#000066>教育经历：&nbsp;</font></div></td>\n");
            builder.Append("                        <td align=\"center\"><TABLE width=\"100%\" height=188 \n");
            builder.Append("                          border=0 align=center cellpadding=\"2\" cellspacing=\"1\">\n");
            builder.Append("                            <TBODY>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD width=\"16%\" height=21>&nbsp;学历</TD>\n");
            builder.Append("                                <TD width=\"22%\">&nbsp;起止时间</TD>\n");
            builder.Append("                                <TD width=\"22%\">&nbsp;专业名称</TD>\n");
            builder.Append("                                <TD width=\"19%\">&nbsp;证书</TD>\n");
            builder.Append("                                <TD width=\"21%\">&nbsp;学校名称</TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR vAlign=top  > \n");
            builder.Append("                                <TD colSpan=5> \n");
            builder.Append("                                  <textarea name=\"Edulevel1\" id=\"Edulevel1\" rows=\"12\" cols=\"62\"></textarea><br>\n");
            builder.Append("                                  * 教育经历必填，且要按上面的格式和发生时间先后填写!</TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                            </TBODY>\n");
            builder.Append("                          </TABLE></td>\n");
            builder.Append("                      </tr>\n");
            builder.Append("                      <tr> \n");
            builder.Append("                        <td width=\"76\"  > \n");
            builder.Append("                          <div align=\"right\"><font color=#000066>工作经历：&nbsp;</font></div></td>\n");
            builder.Append("                        <td align=center><TABLE width=\"100%\" height=188 \n");
            builder.Append("                          border=0 align=center cellpadding=\"2\" cellspacing=\"1\">\n");
            builder.Append("                            <TBODY>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD width=\"25%\" height=21>&nbsp;起止时间</TD>\n");
            builder.Append("                                <TD width=\"25%\">&nbsp;职位名称</TD>\n");
            builder.Append("                                <TD width=\"25%\">&nbsp;服务单位</TD>\n");
            builder.Append("                                <TD width=\"25%\">&nbsp;工作内容</TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR vAlign=top  > \n");
            builder.Append("                                <TD colSpan=4> \n");
            builder.Append("                                  <textarea name=\"Experience1\" id=\"Experience1\" rows=\"12\" cols=\"62\"></textarea>\n");
            builder.Append("                                  <br>\n");
            builder.Append("                                  * 工作经历必填，且要按上面的格式和发生时间先后填写!</TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                            </TBODY>\n");
            builder.Append("                          </TABLE></td>\n");
            builder.Append("                      </tr>\n");
            builder.Append("                      <tr> \n");
            builder.Append("                        <td width=\"76\"  > \n");
            builder.Append("                          <div align=\"right\"><font color=#000066>联系方式：&nbsp;</font> \n");
            builder.Append("                          </div></td>\n");
            builder.Append("                        <td><TABLE width=\"100%\" border=0 cellpadding=\"2\" cellspacing=\"1\">\n");
            builder.Append("                            <TBODY>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD height=\"20\" align=right>联系电话：</TD>\n");
            builder.Append("                                <TD width=\"80%\" align=left > \n");
            builder.Append("                                  <input name=\"Phone1\" type=\"text\" id=\"Phone1\" size=\"25\" />\n");
            builder.Append("                                   </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD \n");
            builder.Append("                          height=20 align=right>手机号码：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <input name=\"Mobile1\" type=\"text\" id=\"Mobile1\" size=\"25\" />\n");
            builder.Append("                               * </TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD \n");
            builder.Append("                          height=20 align=right>E-mail地址：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <input name=\"Email1\" type=\"text\" id=\"Email1\" size=\"25\" />\n");
            builder.Append("                                  *</TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD \n");
            builder.Append("                          height=20 align=right>通信地址：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <input name=\"Add1\" type=\"text\" id=\"Add1\" maxlength=\"140\" size=\"40\" /></TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                              <TR  > \n");
            builder.Append("                                <TD \n");
            builder.Append("                          height=20 align=right>邮政编码：</TD>\n");
            builder.Append("                                <TD align=left > \n");
            builder.Append("                                  <input name=\"Postcode1\" type=\"text\" id=\"Postcode1\" size=\"10\" maxlength=\"6\" /></TD>\n");
            builder.Append("                              </TR>\n");
            builder.Append("                            </TBODY>\n");
            builder.Append("                          </TABLE></td>\n");
            builder.Append("                      </tr>\n");
            builder.Append("                      <tr> \n");
            builder.Append("                        <td width=\"76\">　</td>\n");
            builder.Append("                        <td><div align=\"center\">\n");
            builder.Append("                            <input type=\"button\" name=\"SubmitHr\" value=\" 提 交 \" id=\"SubmitHr\" name=\"submit8\" onclick=Click_submit() />\n");
            builder.Append("                            &nbsp;&nbsp; \n");
            builder.Append("                            <input type=\"reset\" name=\"Submit8\" value=\" 重 置 \" onclick=\"Clear()\" >\n");
            builder.Append("                          </div></td>\n");
            builder.Append("                      </tr>\n");
            builder.Append("                    </table>\n");
            builder.Append("                  </TD>\n");
            builder.Append("              </TR>\n");
            builder.Append("            </table> \n");
            builder.Append("       </TD>\n");
            builder.Append("        </TR></TBODY></TABLE>\n");
            builder.Append("<script language =javascript>\n");
            builder.Append("function getQueryStringByName(name){\n");
            builder.Append("     var result = location.search.match(new RegExp(\"[\\?\\&]\" + name+ \"=([^\\&]+)\",\"i\"));\n");
            builder.Append("     if(result == null || result.length < 1){\n");
            builder.Append("         return \"\";\n");
            builder.Append("     }\n");
            builder.Append("     return decodeURIComponent( result[1]);\n");
            builder.Append("}\n");
            builder.Append(" function init(){jobname.value=getQueryStringByName(\"Position\");}\n");
            builder.Append(" window.onload=init();\n");
            builder.Append(" </script>\n");
            return builder.ToString();
        }

        private string GetHrDemandLoop()
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">\n");
            builder.Append("  <tr bgcolor=\"#DFDFDF\">\n");
            builder.Append("  <td  width=\"13%\" height=\"22\" align=\"center\" >\n");
            builder.Append("    职位名称:\n");
            builder.Append("    </td>\n");
            builder.Append("    <td colspan=\"2\"  >&nbsp;<%#Eval(\"职位名称\")%>\n");
            builder.Append("    </td>\n");
            builder.Append("    <td width=\"20%\"  >&nbsp;<a href=\"HrDemandAccept.html?Position=<%#Eval(\"strEc职位名称\")%>\"><font color=\"#FF0000\">应聘此岗位</font></a>\n");
            builder.Append("    </td>\n");
            builder.Append("    </tr>  \n");
            builder.Append("  <tr bgcolor=\"#DFDFDF\">\n");
            builder.Append("  <td  width=\"13%\" height=\"22\" align=\"center\" >\n");
            builder.Append("    工作地点:\n");
            builder.Append("    </td>\n");
            builder.Append("    <td colspan=\"3\" valign=\"top\"  >&nbsp;<%#Eval(\"工作地点\")%>\n");
            builder.Append("    </td>\n");
            builder.Append("    </tr>\n");
            builder.Append("   <tr bgcolor=\"#DFDFDF\">\n");
            builder.Append("    <td width=\"13%\" height=\"22\" align=\"center\"  >  \n");
            builder.Append("    工资待遇:\n");
            builder.Append("    </td>\n");
            builder.Append("    <td width=\"27%\" >&nbsp;<%#Eval(\"工资待遇\")%>\n");
            builder.Append("    </td>\n");
            builder.Append("    <td height=\"26\" align=\"center\" >\n");
            builder.Append("    发布日期: \n");
            builder.Append("    </td>\n");
            builder.Append("    <td  >&nbsp;<%#Eval(\"发布日期\")%>\n");
            builder.Append("    </td>\n");
            builder.Append("    </tr>\n");
            builder.Append("  <tr bgcolor=\"#DFDFDF\">\n");
            builder.Append("    <td height=\"26\" align=\"center\" >需求人数:\n");
            builder.Append("    </td>\n");
            builder.Append("    <td align=\"center\" ><div align=\"left\">&nbsp;<%#Eval(\"需求人数\")%>\n");
            builder.Append("    </div></td>\n");
            builder.Append("    <td align=\"center\" >有效期限:\n");
            builder.Append("    </td>\n");
            builder.Append("    <td align=\"center\" ><div align=\"left\">&nbsp;<%#Eval(\"有效期限\")%>\n");
            builder.Append("    </div></td>\n");
            builder.Append("    </tr>\n");
            builder.Append("  <tr bgcolor=\"#DFDFDF\">\n");
            builder.Append("    <td width=\"13%\" height=\"26\" align=\"center\"  ><font color=\"#000066\">\n");
            builder.Append("    具体要求:\n");
            builder.Append("    </font></td>\n");
            builder.Append("    <td colspan=\"3\" align=\"left\" bgcolor=\"#eeeeee\"><%#Eval(\"具体要求\")%>\n");
            builder.Append("    </td>\n");
            builder.Append("    </tr>\n");
            builder.Append(" </table><br />\n");
            return builder.ToString();
        }

        private string GetHtmlTags(string strLoopContent, string strTags)
        {
            int index = strLoopContent.IndexOf(strTags);
            if (index == -1)
            {
                return "";
            }
            string str2 = ">";
            int num2 = strLoopContent.IndexOf(str2, index) + str2.Length;
            return strLoopContent.Substring(index, num2 - index);
        }

        private string GetIDForParameter(string strClmnIDMark)
        {
            string[] strArray = strClmnIDMark.Split(new char[] { '|' });
            string str = "'0'";
            foreach (string str2 in strArray)
            {
                str = str + ",'" + this.GetColumnID(str2) + "'";
            }
            return str;
        }

        private void GetIDMarkInAndNotIn(string strColumn, ref string strIDMarkIn, ref string strIDMarkNotIn)
        {
            if (strColumn[0] == '-')
            {
                foreach (string str in strColumn.Replace("-", "").Split(new char[] { '|' }))
                {
                    strIDMarkNotIn = strIDMarkNotIn + ",'" + str + "'";
                }
                if (strIDMarkNotIn.Length > 1)
                {
                    strIDMarkNotIn = strIDMarkNotIn.Substring(1);
                }
            }
            else
            {
                foreach (string str2 in strColumn.Split(new char[] { '|' }))
                {
                    strIDMarkIn = strIDMarkIn + ",'" + str2 + "'";
                }
                if (strIDMarkIn.Length > 1)
                {
                    strIDMarkIn = strIDMarkIn.Substring(1);
                }
            }
        }

        private void GetIDMarkSubIN(ref string strColumnIDIn, string strIDMark1, string strMdl)
        {
            DataRow[] rowArray = this.dTableClmn.Select("IDMark='" + strIDMark1 + "'");
            if (rowArray.Length != 0)
            {
                string str = rowArray[0]["ListContent"].ToString();
                string str2 = rowArray[0]["depth"].ToString();
                string str3 = rowArray[0]["id"].ToString();
                if ((str2 == "2") || (str == "0"))
                {
                    if (strMdl == rowArray[0]["Module"].ToString())
                    {
                        strColumnIDIn = ",'" + str3 + "'";
                    }
                    else
                    {
                        strColumnIDIn = ",'0'";
                    }
                }
                else if (str2 == "1")
                {
                    rowArray = this.dTableClmn.Select("ParentID='" + str3 + "' and Module='" + strMdl + "'");
                    for (int i = 0; i < rowArray.Length; i++)
                    {
                        object obj2 = strColumnIDIn;
                        strColumnIDIn = string.Concat(new object[] { obj2, ",'", rowArray[i]["id"], "'" });
                    }
                    if (str != "2")
                    {
                        strColumnIDIn = strColumnIDIn + ",'" + str3 + "'";
                    }
                }
                else
                {
                    rowArray = this.dTableClmn.Select("ParentID='" + str3 + "' and Module='" + strMdl + "'");
                    for (int j = 0; j < rowArray.Length; j++)
                    {
                        object obj3 = strColumnIDIn;
                        strColumnIDIn = string.Concat(new object[] { obj3, ",'", rowArray[j]["id"], "'" });
                        DataRow[] rowArray2 = this.dTableClmn.Select(string.Concat(new object[] { "ParentID='", rowArray[j]["id"], "' and Module='", strMdl, "'" }));
                        for (int k = 0; k < rowArray2.Length; k++)
                        {
                            object obj4 = strColumnIDIn;
                            strColumnIDIn = string.Concat(new object[] { obj4, ",'", rowArray2[k]["id"], "'" });
                        }
                    }
                    if (str != "2")
                    {
                        strColumnIDIn = strColumnIDIn + ",'" + str3 + "'";
                    }
                }
            }
        }

        private List<string[]> GetImageData(DataTable dTable)
        {
            List<string[]> list = new List<string[]>();
            foreach (DataRow row in dTable.Rows)
            {
                List<string[]> picData = new List<string[]>();
                picData = this.GetPicData(row, 0);
                for (int i = 0; (i < picData.Count) && (i < 6); i++)
                {
                    list.Add(picData[i]);
                }
            }
            return list;
        }

        private string GetImgHtml(string[] astrAdv, string strPageDepth)
        {
            if ((astrAdv[7].Trim() == "") && (astrAdv[8].Trim() == ""))
            {
                return ("<img src=\"" + (astrAdv[4].Contains("http://") ? astrAdv[4] : this.PageUrlSet(astrAdv[4], strPageDepth)) + "\"  border=\"0\" />");
            }
            if (astrAdv[7].Trim() == "")
            {
                return ("<img src=\"" + (astrAdv[4].Contains("http://") ? astrAdv[4] : this.PageUrlSet(astrAdv[4], strPageDepth)) + "\" Height=\"" + astrAdv[8].Trim() + "px\"  border=\"0\" />");
            }
            if (astrAdv[8].Trim() == "")
            {
                return ("<img src=\"" + (astrAdv[4].Contains("http://") ? astrAdv[4] : this.PageUrlSet(astrAdv[4], strPageDepth)) + "\" Width=\"" + astrAdv[7].Trim() + "px\"  border=\"0\" />");
            }
            return ("<img src=\"" + (astrAdv[4].Contains("http://") ? astrAdv[4] : this.PageUrlSet(astrAdv[4], strPageDepth)) + "\" Width=\"" + astrAdv[7].Trim() + "px\" Height=\"" + astrAdv[8].Trim() + "px\"  border=\"0\" />");
        }

        private void GetInfluenceID(string strID, ref string strID1, ref string strID2, DataTable dtCtnt)
        {
            int count = dtCtnt.Rows.Count;
            switch (count)
            {
                case 1:
                    return;

                case 2:
                    if (dtCtnt.Rows[0]["id"].ToString() == strID)
                    {
                        strID2 = dtCtnt.Rows[1]["id"].ToString();
                        return;
                    }
                    strID1 = dtCtnt.Rows[0]["id"].ToString();
                    return;
            }
            for (int i = 0; i < count; i++)
            {
                if (dtCtnt.Rows[i]["id"].ToString() == strID)
                {
                    if (i == 0)
                    {
                        strID2 = dtCtnt.Rows[1]["id"].ToString();
                        return;
                    }
                    if (i == (count - 1))
                    {
                        strID1 = dtCtnt.Rows[i - 1]["id"].ToString();
                        return;
                    }
                    strID1 = dtCtnt.Rows[i - 1]["id"].ToString();
                    strID2 = dtCtnt.Rows[i + 1]["id"].ToString();
                    return;
                }
            }
        }

        private void GetIsShowDataRow(ref DataRow dRow)
        {
            if ((dRow["IsShow"].ToString() == "0") && (dRow["depth"].ToString() != "2"))
            {
                DataRow[] rowArray = this.dTableClmn.Select("ParentID='" + dRow["ID"] + "'");
                if (rowArray.Length > 0)
                {
                    dRow = rowArray[0];
                    this.GetIsShowDataRow(ref dRow);
                }
            }
        }

        private string GetIsShowLink(string strDir, DataRow dRow, string strPageDepth, string strpagePre)
        {
            string str = "";
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
                                    str = this.GetTitleLink(rowArray[0], strPageDepth, string.Concat(new object[] { strDir, "/", str3, "/", rowArray[0]["folder"] }), strpagePre);
                                }
                                return str;
                            }
                            return this.GetTitleLink(rowArray[0], strPageDepth, strDir + "/" + rowArray[0]["folder"], strpagePre);

                        case "1":
                            str = this.GetTitleLink(rowArray[0], strPageDepth, strDir + "/" + rowArray[0]["folder"], strpagePre);
                            break;
                    }
                }
                return str;
            }
            return this.GetTitleLink(dRow, strPageDepth, strDir, strpagePre);
        }

        private string GetJianJieBriefInfo(string strColumn, string strTitleNum, string strPageDepth)
        {
            DataRow[] rowArray = this.dTableClmn.Select("IDMark='" + strColumn + "'");
            if (rowArray.Length == 0)
            {
                return "此标识的栏目不存在，请改正。";
            }
            string str = rowArray[0]["BriefIntro"].ToString();
            int length = (strTitleNum == "") ? 0 : int.Parse(strTitleNum);
            if ((length > 0) && (str.Length > length))
            {
                str = str.Substring(0, length) + "...";
            }
            return str;
        }

        private void GetLabel(ref string strTemp, ref string strLoop, ref string strLoopRule, string strLabel)
        {
            int index = strTemp.IndexOf(strLabel);
            if (index != -1)
            {
                try
                {
                    string str = "]";
                    int num2 = strTemp.IndexOf(str, index) + str.Length;
                    strLoop = strTemp.Substring(index, num2 - index);
                    int num3 = strLoop.IndexOf(']');
                    strLoopRule = strLoop.Substring(strLabel.Length, (num3 - strLabel.Length) + 1);
                    strLoopRule = strLoopRule.Replace(']', ',');
                }
                catch (Exception exception)
                {
                    SystemError.CreateErrorLog("读JianJie标签错误： " + exception.ToString());
                }
            }
        }

        private void GetLabel(ref string strTemp, ref string strLoop, ref string strLoopRule, ref string strLoopContent, string strLabel)
        {
            int index = strTemp.IndexOf(strLabel);
            if (index != -1)
            {
                try
                {
                    string str = strLabel.Replace(" ", "").Insert(1, "/") + "]";
                    int num2 = strTemp.IndexOf(str) + str.Length;
                    strLoop = strTemp.Substring(index, num2 - index);
                    int num3 = strLoop.IndexOf(']');
                    strLoopRule = strLoop.Substring(strLabel.Length, (num3 - str.Length) + 3);
                    strLoopRule = strLoopRule.Replace(']', ',');
                    strLoopContent = strLoop.Substring(num3 + 1, (strLoop.IndexOf(str) - num3) - 1);
                }
                catch (Exception exception)
                {
                    SystemError.CreateErrorLog("读循环标签错误： " + exception.ToString());
                }
            }
        }

        private void GetLabelFenyeTitle(string strLoopContent, ref string strloopTitle, ref string strloopsTitle)
        {
            int length = strLoopContent.IndexOf('\n') + 1;
            strloopTitle = strLoopContent.Substring(0, length).Trim();
            if (strloopTitle == "")
            {
                strLoopContent = strLoopContent.Substring(length);
                length = strLoopContent.IndexOf('\n') + 1;
                strloopTitle = strLoopContent.Substring(0, length);
            }
            strLoopContent = strLoopContent.Substring(length);
            length = strLoopContent.IndexOf('\n') + 1;
            strloopsTitle = strLoopContent.Substring(0, length).Trim();
        }

        private void GetLabelTitle(string strLoopContent, ref string strloopTitle, ref string strloopsTitle, ref string strloopssTitle)
        {
            int length = strLoopContent.IndexOf('\n') + 1;
            strloopTitle = strLoopContent.Substring(0, length).Trim();
            if (strloopTitle == "")
            {
                strLoopContent = strLoopContent.Substring(length);
                length = strLoopContent.IndexOf('\n') + 1;
                strloopTitle = strLoopContent.Substring(0, length);
            }
            strLoopContent = strLoopContent.Substring(length);
            length = strLoopContent.IndexOf('\n') + 1;
            strloopsTitle = strLoopContent.Substring(0, length).Trim();
            if (strloopsTitle == "")
            {
                if (strloopTitle.IndexOf("[QH:sFenLanTitle]") >= 0)
                {
                    strloopsTitle = strloopTitle;
                    strloopTitle = "";
                    return;
                }
                if (strloopTitle.IndexOf("[QH:ssFenLanTitle]") >= 0)
                {
                    strloopssTitle = strloopTitle;
                    strloopTitle = strloopsTitle = "";
                    return;
                }
            }
            strLoopContent = strLoopContent.Substring(length);
            length = strLoopContent.IndexOf('\n') + 1;
            strloopssTitle = strLoopContent.Substring(0, length).Trim();
            if ((strloopssTitle == "") && (strloopTitle.IndexOf("[QH:sFenLanTitle]") >= 0))
            {
                strloopssTitle = strloopsTitle;
                strloopsTitle = strloopTitle;
                strloopTitle = "";
            }
        }

        private string GetLangImg(string strIDMark, List<string[]> liststrLang, string strPageDepth)
        {
            foreach (string[] strArray in liststrLang)
            {
                if (strArray[2] == strIDMark)
                {
                    return this.PageUrlSet(strArray[3], strPageDepth);
                }
            }
            return "没有此标识的语言版本";
        }

        private string GetLangLink(string strIDMark, List<string[]> liststrLang, string strUrl, string strPre)
        {
            string str2 = "";
            foreach (string[] strArray in liststrLang)
            {
                if (strArray[2] == strIDMark)
                {
                    str2 = (strArray[1] == "/") ? "" : (strArray[1] + "/");
                    if (this.strLangDir == "/")
                    {
                        return (strPre + str2);
                    }
                    return ("../" + strPre + str2);
                }
            }
            return "没有此标识的语言版本";
        }

        private string GetLangName(string strIDMark, List<string[]> liststrLang)
        {
            foreach (string[] strArray in liststrLang)
            {
                if (strArray[2] == strIDMark)
                {
                    return strArray[0];
                }
            }
            return "没有此标识的语言版本";
        }

        private string GetLanmuLoopContent(string strLoopContent, string strPageDepth, string strColumn, string strModule, string strColumnID)
        {
            if ((strColumn == "") && ((strModule.Length != 1) || (strModule.IndexOfAny(new char[] { '1', '2', '3', '4', '5' }) == -1)))
            {
                return "栏目标识及模块不正确，请改正。";
            }
            string pagePre = this.GetPagePre(strPageDepth);
            string strLoop = "";
            string strLoopRule = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string strIDMarkIn = "";
            string strIDMarkNotIn = "";
            DataRow[] foundRows = null;
            if (strColumn == "")
            {
                foundRows = this.dTableClmn.Select("Module='" + strModule + "' and depth='0'");
            }
            else
            {
                this.GetIDMarkInAndNotIn(strColumn, ref strIDMarkIn, ref strIDMarkNotIn);
                if (strIDMarkIn != "")
                {
                    if (strModule == "")
                    {
                        foundRows = this.dTableClmn.Select("IDMark in(" + strIDMarkIn + ") and depth='0'");
                    }
                    else
                    {
                        foundRows = this.dTableClmn.Select("IDMark in(" + strIDMarkIn + ") and Module='" + strModule + "' and depth='0'");
                    }
                    foundRows = this.OrderByIDMarkIn(foundRows, strIDMarkIn);
                }
                else if (strIDMarkNotIn != "")
                {
                    if (strModule == "")
                    {
                        return "模块不能为空，请填写模块。";
                    }
                    foundRows = this.dTableClmn.Select("IDMark not in(" + strIDMarkNotIn + ") and Module='" + strModule + "' and depth='0'");
                }
            }
            if (foundRows == null)
            {
                return "栏目不存在或不是一级栏目，请改正。";
            }
            if (foundRows.Length == 0)
            {
                return "栏目不存在或不是一级栏目，请改正。";
            }
            this.GetLabel(ref strLoopContent, ref strLoop, ref strLoopRule, ref str4, "[QH:LanmuloopMedium");
            string input = this.ReadValue(strLoopRule, "TitleNum");
            if (Regex.IsMatch(input, @"^\d+$"))
            {
                this.nMediumLength = int.Parse(input);
            }
            this.GetLabel(ref str4, ref str5, ref str6, ref str7, "[QH:LanmuloopSmall");
            input = this.ReadValue(str6, "TitleNum");
            if (Regex.IsMatch(input, @"^\d+$"))
            {
                this.nSmallLength = int.Parse(input);
            }
            string strUrlPre = "";
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < foundRows.Length; i++)
            {
                builder.Append(this.LoopReplaceBigContent(strLoopContent, foundRows[i], pagePre, strUrlPre, i, str4, strLoop, str7, str5, strPageDepth, strColumnID));
            }
            return builder.ToString();
        }

        private void GetLoopAttribute(string strLoopRule)
        {
            this.LoopAttribute.Module = this.ReadValue(strLoopRule, "Module");
            this.LoopAttribute.NewsCount = this.ReadValue(strLoopRule, "NewsCount");
            this.LoopAttribute.TitleNum = this.ReadValue(strLoopRule, "TitleNum");
            this.LoopAttribute.AddStr = this.ReadValue(strLoopRule, "AddStr");
            this.LoopAttribute.Condition = this.ReadValue(strLoopRule, "Condition");
            this.LoopAttribute.Role = this.ReadValue(strLoopRule, "Role");
            this.LoopAttribute.KeyWord = this.ReadValue(strLoopRule, "KeyWord");
            this.LoopAttribute.Column = this.ReadValue(strLoopRule, "Column");
            this.LoopAttribute.NewsType = this.ReadValue(strLoopRule, "NewsType");
            this.LoopAttribute.Order = this.ReadValue(strLoopRule, "Order");
            this.LoopAttribute.Sort = this.ReadValue(strLoopRule, "Sort");
            this.LoopAttribute.NewsDate = this.ReadValue(strLoopRule, "NewsDate");
            this.LoopAttribute.ProductFilter = this.ReadValue(strLoopRule, "ProductFilter");
            this.LoopAttribute.JStype = this.ReadValue(strLoopRule, "JS_type");
        }

        private void GetLoopAttribute(string strLoopRule, string strName)
        {
            this.LoopAttribute.Column = this.ReadValue(strLoopRule, "Column");
            this.LoopAttribute.NewsType = this.ReadValue(strLoopRule, "NewsType");
        }

        private string GetLoopContent(string strLoopContent, DataTable dTable, string strPageDepth)
        {
            int num = (this.LoopAttribute.Condition == "") ? 0 : int.Parse(this.LoopAttribute.Condition);
            int num2 = (this.LoopAttribute.NewsCount == "") ? 0 : int.Parse(this.LoopAttribute.NewsCount);
            string tableName = this.GetTableName(this.LoopAttribute.Module);
            if (tableName == "QH_FriendLinks")
            {
                this.strFriendLinkImg = this.GetHtmlTags(strLoopContent, "<img ");
            }
            int nloopNum = (num2 == 0) ? dTable.Rows.Count : num2;
            nloopNum = (nloopNum > dTable.Rows.Count) ? dTable.Rows.Count : nloopNum;
            StringBuilder builder = new StringBuilder("");
            if (this.LoopAttribute.JStype == "2")
            {
                nloopNum = (nloopNum > 6) ? 6 : nloopNum;
                builder.Append(this.LoopReplaceJS2Content(strLoopContent, dTable, nloopNum, strPageDepth, this.LoopAttribute.Module));
            }
            else
            {
                for (int i = 0; i < nloopNum; i++)
                {
                    DataRow dRow = dTable.Rows[i];
                    int nNameNumber = i;
                    if ((tableName == "QH_BannerImg") && (dRow["ImgUrl"].ToString().Trim() == ""))
                    {
                        break;
                    }
                    bool flag1 = tableName == "QH_FriendLinks";
                    builder.Append(this.LoopReplaceContent(strLoopContent, dRow, tableName, strPageDepth, nNameNumber));
                    if ((num != 0) && (((i + 1) % num) == 0))
                    {
                        builder.Append(this.LoopAttribute.Role);
                    }
                }
            }
            return builder.ToString();
        }

        private DataTable GetLoopDataTable(string strQuery)
        {
            DataSet dataSet = this.Bll1.DAL1.GetDataSet(strQuery);
            if (dataSet == null)
            {
                return null;
            }
            DataTable table = dataSet.Tables[0].Clone();
            if ((this.LoopAttribute.Module.Length == 1) || (this.LoopAttribute.Module == ""))
            {
                if (this.LoopAttribute.KeyWord == "")
                {
                    table = dataSet.Tables[0].Copy();
                }
                else
                {
                    for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
                    {
                        if (dataSet.Tables[0].Rows[j]["Title"].ToString().IndexOf(this.LoopAttribute.KeyWord) != -1)
                        {
                            table.ImportRow(dataSet.Tables[0].Rows[j]);
                        }
                    }
                }
                int length = (this.LoopAttribute.TitleNum == "") ? 40 : int.Parse(this.LoopAttribute.TitleNum);
                table.Columns.Add(new DataColumn("titleAll"));
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string str = table.Rows[i]["title"].ToString();
                    table.Rows[i]["titleAll"] = str;
                    if (str.Length > length)
                    {
                        table.Rows[i]["title"] = str.Substring(0, length) + this.LoopAttribute.AddStr;
                    }
                }
                return table;
            }
            return dataSet.Tables[0].Copy();
        }

        private string GetLoopFenyeTitleContent(string strloopTitle, string strloopsTitle, DataRow dw, string strShow2, string strPageDepth, string strNowClass, string strNowClassImg, string strFirstClass, string strLastClass)
        {
            string str = dw["depth"].ToString();
            string pagePre = this.GetPagePre(strPageDepth);
            DataRow[] rowArray = null;
            string str3 = "";
            string str4 = dw["ColumnName"].ToString();
            string str17 = str;
            if (str17 != null)
            {
                if (!(str17 == "0"))
                {
                    if (str17 == "1")
                    {
                        rowArray = this.dTableClmn.Select("ID=" + dw["ParentID"]);
                        if (rowArray.Length > 0)
                        {
                            str3 = rowArray[0]["id"].ToString();
                            if (rowArray[0]["IsShow"].ToString() == "0")
                            {
                                str4 = rowArray[0]["ColumnName"].ToString();
                            }
                        }
                    }
                    else if (str17 == "2")
                    {
                        rowArray = this.dTableClmn.Select("ID=" + dw["ParentID"]);
                        if (rowArray.Length > 0)
                        {
                            str3 = rowArray[0]["ParentID"].ToString();
                            if (rowArray[0]["IsShow"].ToString() == "0")
                            {
                                str4 = rowArray[0]["ColumnName"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    str3 = dw["id"].ToString();
                }
            }
            string strUrlPre = "";
            StringBuilder builder = new StringBuilder("");
            bool flag = str4 == dw["ColumnName"].ToString();
            if (strShow2 == "1")
            {
                int startIndex = strloopTitle.LastIndexOf("[QH:FenLanTitle]");
                if (startIndex >= 0)
                {
                    strloopsTitle = (strloopsTitle == "") ? strloopTitle.Insert(startIndex, "...") : strloopsTitle;
                }
                string str6 = "";
                DataRow[] rowArray2 = this.dTableClmn.Select("ParentID='" + str3 + "'");
                if (rowArray2.Length > 0)
                {
                    strUrlPre = this.ColumnUrlPre(rowArray2[0], this.dTableClmn);
                }
                for (int i = 0; i < rowArray2.Length; i++)
                {
                    if ((rowArray2[i]["ColumnName"].ToString() == dw["ColumnName"].ToString()) || (!flag && (rowArray2[i]["ColumnName"].ToString() == str4)))
                    {
                        if ((this.nBigLength != 0) && (rowArray2[i]["ColumnName"].ToString().Length > this.nBigLength))
                        {
                            rowArray2[i]["ColumnName"] = rowArray2[i]["ColumnName"].ToString().Substring(0, this.nBigLength) + "...";
                        }
                        string newValue = strNowClass;
                        if (i == 0)
                        {
                            newValue = strNowClass + " " + strFirstClass;
                        }
                        else if ((i == (rowArray2.Length - 1)) && (i != 0))
                        {
                            newValue = strNowClass + " " + strLastClass;
                        }
                        string strLoopContent = strloopTitle.Replace("[QH:NowClass]", newValue).Replace("[QH:NowClassImg]", strNowClassImg);
                        builder.Append(this.LoopReplaceTitleContent(strLoopContent, rowArray2[i], "[QH:FenLanTitle]", strPageDepth, strUrlPre, pagePre, ""));
                    }
                    else
                    {
                        if ((this.nBigLength != 0) && (rowArray2[i]["ColumnName"].ToString().Length > this.nBigLength))
                        {
                            rowArray2[i]["ColumnName"] = rowArray2[i]["ColumnName"].ToString().Substring(0, this.nBigLength) + "...";
                        }
                        string str9 = "";
                        if (i == 0)
                        {
                            str9 = strFirstClass;
                        }
                        else if ((i == (rowArray2.Length - 1)) && (i != 0))
                        {
                            str9 = strLastClass;
                        }
                        string str10 = strloopTitle.Replace("[QH:NowClass]", str9).Replace("[QH:NowClassImg]", "");
                        builder.Append(this.LoopReplaceTitleContent(str10, rowArray2[i], "[QH:FenLanTitle]", strPageDepth, strUrlPre, pagePre, ""));
                    }
                    DataRow[] rowArray3 = this.dTableClmn.Select("ParentID='" + rowArray2[i]["id"] + "'");
                    if (rowArray3.Length > 0)
                    {
                        str6 = this.ColumnUrlPre(rowArray3[0], this.dTableClmn);
                    }
                    for (int j = 0; j < rowArray3.Length; j++)
                    {
                        if ((this.nBigLength != 0) && (rowArray3[j]["ColumnName"].ToString().Length > this.nBigLength))
                        {
                            rowArray3[j]["ColumnName"] = rowArray3[j]["ColumnName"].ToString().Substring(0, this.nBigLength) + "...";
                        }
                        builder.Append(this.LoopReplaceTitleContent(strloopsTitle, rowArray3[j], "[QH:FenLanTitle]", strPageDepth, str6, pagePre, ""));
                    }
                }
            }
            if (strShow2 == "Self")
            {
                DataRow[] rowArray4;
                int num4 = strloopTitle.LastIndexOf("[QH:FenLanTitle]");
                if (num4 >= 0)
                {
                    strloopsTitle = (strloopsTitle == "") ? strloopTitle.Insert(num4, "...") : strloopsTitle;
                }
                if (str == "0")
                {
                    rowArray4 = new DataRow[] { dw };
                }
                else
                {
                    rowArray4 = this.dTableClmn.Select("ParentID='" + str3 + "'");
                }
                if (rowArray4.Length > 0)
                {
                    strUrlPre = this.ColumnUrlPre(rowArray4[0], this.dTableClmn);
                }
                for (int k = 0; k < rowArray4.Length; k++)
                {
                    if ((rowArray4[k]["ColumnName"].ToString() == dw["ColumnName"].ToString()) || (!flag && (rowArray4[k]["ColumnName"].ToString() == str4)))
                    {
                        if ((this.nBigLength != 0) && (rowArray4[k]["ColumnName"].ToString().Length > this.nBigLength))
                        {
                            rowArray4[k]["ColumnName"] = rowArray4[k]["ColumnName"].ToString().Substring(0, this.nBigLength) + "...";
                        }
                        string str11 = strNowClass;
                        if (k == 0)
                        {
                            str11 = strNowClass + " " + strFirstClass;
                        }
                        else if ((k == (rowArray4.Length - 1)) && (k != 0))
                        {
                            str11 = strNowClass + " " + strLastClass;
                        }
                        string str12 = strloopTitle.Replace("[QH:NowClass]", str11).Replace("[QH:NowClassImg]", strNowClassImg);
                        builder.Append(this.LoopReplaceTitleContent(str12, rowArray4[k], "[QH:FenLanTitle]", strPageDepth, strUrlPre, pagePre, ""));
                        break;
                    }
                }
            }
            else
            {
                DataRow[] rowArray5 = this.dTableClmn.Select("ParentID='" + str3 + "'");
                if (rowArray5.Length > 0)
                {
                    strUrlPre = this.ColumnUrlPre(rowArray5[0], this.dTableClmn);
                }
                for (int m = 0; m < rowArray5.Length; m++)
                {
                    if ((rowArray5[m]["ColumnName"].ToString() == dw["ColumnName"].ToString()) || (!flag && (rowArray5[m]["ColumnName"].ToString() == str4)))
                    {
                        if ((this.nBigLength != 0) && (rowArray5[m]["ColumnName"].ToString().Length > this.nBigLength))
                        {
                            rowArray5[m]["ColumnName"] = rowArray5[m]["ColumnName"].ToString().Substring(0, this.nBigLength) + "...";
                        }
                        string str13 = strNowClass;
                        if (m == 0)
                        {
                            str13 = strNowClass + " " + strFirstClass;
                        }
                        else if ((m == (rowArray5.Length - 1)) && (m != 0))
                        {
                            str13 = strNowClass + " " + strLastClass;
                        }
                        string str14 = strloopTitle.Replace("[QH:NowClass]", str13).Replace("[QH:NowClassImg]", strNowClassImg);
                        builder.Append(this.LoopReplaceTitleContent(str14, rowArray5[m], "[QH:FenLanTitle]", strPageDepth, strUrlPre, pagePre, ""));
                    }
                    else
                    {
                        if ((this.nBigLength != 0) && (rowArray5[m]["ColumnName"].ToString().Length > this.nBigLength))
                        {
                            rowArray5[m]["ColumnName"] = rowArray5[m]["ColumnName"].ToString().Substring(0, this.nBigLength) + "...";
                        }
                        string str15 = "";
                        if (m == 0)
                        {
                            str15 = strFirstClass;
                        }
                        else if ((m == (rowArray5.Length - 1)) && (m != 0))
                        {
                            str15 = strLastClass;
                        }
                        string str16 = strloopTitle.Replace("[QH:NowClass]", str15).Replace("[QH:NowClassImg]", "");
                        builder.Append(this.LoopReplaceTitleContent(str16, rowArray5[m], "[QH:FenLanTitle]", strPageDepth, strUrlPre, pagePre, ""));
                    }
                }
            }
            return builder.ToString();
        }

        private string[] GetLoopLanmuIDMark(string strColumn)
        {
            string str = "";
            string str2 = "";
            if (strColumn.Contains("-"))
            {
                string[] strArray = strColumn.Split(new char[] { '-' });
                str2 = strArray[0];
                if (!this.dTableClmn.Columns.Contains("IDMarkInt"))
                {
                    this.AddClmnIDMarkInt(this.dTableClmn);
                }
                DataRow[] rowArray = this.dTableClmn.Select("IDMark='" + str2 + "'");
                if (rowArray.Length == 0)
                {
                    return new string[] { str2 };
                }
                if (strArray[1].Trim() == "")
                {
                    rowArray = this.dTableClmn.Select(string.Concat(new object[] { "IDMarkInt>=", str2, " and depth='", rowArray[0]["depth"], "' and ParentID='", rowArray[0]["ParentID"], "'" }));
                }
                else
                {
                    rowArray = this.dTableClmn.Select(string.Concat(new object[] { "IDMarkInt>=", str2, " and IDMarkInt<=", strArray[1].Trim(), " and depth='", rowArray[0]["depth"], "' and ParentID='", rowArray[0]["ParentID"], "'" }));
                }
                if (rowArray.Length == 0)
                {
                    return new string[] { str2 };
                }
                for (int i = 0; i < rowArray.Length; i++)
                {
                    str = str + rowArray[i]["IDMark"].ToString() + "|";
                }
                strColumn = str.TrimEnd(new char[] { '|' });
            }
            strColumn = strColumn.TrimEnd(new char[] { '|' });
            return strColumn.Split(new char[] { '|' });
        }

        private string GetLoopListContent(string strLoopContent, string strPageDepth, string strShow1, DataRow dw, string strSelfLanmu)
        {
            DataRow[] rowArray2;
            string pagePre = this.GetPagePre(strPageDepth);
            string strLoop = "";
            string strLoopRule = "";
            string str4 = "";
            string strUrlPre = "";
            DataRow[] rowArray = null;
            string str6 = "";
            string str8 = dw["depth"].ToString();
            if (str8 != null)
            {
                if (!(str8 == "0"))
                {
                    if (str8 == "1")
                    {
                        rowArray = this.dTableClmn.Select("ID=" + dw["ParentID"]);
                        if (rowArray.Length > 0)
                        {
                            str6 = rowArray[0]["id"].ToString();
                        }
                    }
                    else if (str8 == "2")
                    {
                        rowArray = this.dTableClmn.Select("ID=" + dw["ParentID"]);
                        if (rowArray.Length > 0)
                        {
                            str6 = rowArray[0]["ParentID"].ToString();
                        }
                    }
                }
                else
                {
                    str6 = dw["id"].ToString();
                }
            }
            StringBuilder builder = new StringBuilder("");
            if (strSelfLanmu == "1")
            {
                rowArray2 = new DataRow[] { dw };
            }
            else if (strSelfLanmu == "2")
            {
                rowArray2 = this.dTableClmn.Select("ID=" + dw["ParentID"]);
            }
            else
            {
                rowArray2 = this.dTableClmn.Select("ParentID='" + str6 + "'");
            }
            if (rowArray2.Length > 0)
            {
                strUrlPre = this.ColumnUrlPre(rowArray2[0], this.dTableClmn);
                this.GetLabel(ref strLoopContent, ref strLoop, ref strLoopRule, ref str4, "[QH:FenYeTSmall");
                string input = this.ReadValue(strLoopRule, "TitleNum");
                if (Regex.IsMatch(input, @"^\d+$"))
                {
                    this.nSmallLength = int.Parse(input);
                }
            }
            for (int i = 0; i < rowArray2.Length; i++)
            {
                builder.Append(this.LoopReplaceFYBigContent(strLoopContent, rowArray2[i], pagePre, strUrlPre, i, str4, strLoop, strPageDepth, dw["id"].ToString()));
            }
            return builder.ToString();
        }

        private string GetLoopNavContent(string strLoopContent, int nPos, string strPageDepth)
        {
            DataRow[] rowArray;
            if (this.dTableClmn == null)
            {
                this.dTableClmn = this.Bll1.GetDataTable(this.strClmnQuery);
            }
            if (this.dTableClmn.Rows.Count == 0)
            {
                return "";
            }
            if (this.bIsMobile)
            {
                if (nPos == 1)
                {
                    rowArray = this.dTableClmn.Select("NavMobile='1' or NavMobile='3'", "Order asc");
                }
                else
                {
                    rowArray = this.dTableClmn.Select("NavMobile='2' or NavMobile='3'", "Order asc");
                }
            }
            else if (nPos == 1)
            {
                rowArray = this.dTableClmn.Select("Nav='1' or Nav='3'", "Order asc");
            }
            else
            {
                rowArray = this.dTableClmn.Select("Nav='2' or Nav='3'", "Order asc");
            }
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < rowArray.Length; i++)
            {
                builder.Append(this.LoopReplaceNavContent(strLoopContent, rowArray[i], this.dTableClmn, strPageDepth, i + 1));
            }
            return builder.ToString();
        }

        private string GetLoopPageField(string TableName)
        {
            string str = "";
            string str2 = TableName;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "QH_News"))
            {
                if (str2 != "QH_Product")
                {
                    if (str2 == "QH_Download")
                    {
                        return "ID,Title,DownloadUrl,FileSize,BriefIntro,Content,AddDate,ModyDate,Author,hits";
                    }
                    if (str2 != "QH_Img")
                    {
                        return str;
                    }
                    return "ID,Title,ImageUrl,ThumbUrl,displayImg,BriefIntro,Content,AddDate,ModyDate,Author,hits";
                }
            }
            else
            {
                return "ID,Title,BriefIntro,Content,AddDate,ModyDate,ImageUrl,LinkUrl,Author,Tags,hits";
            }
            return "ID,Product_id,Title,SpecialName,Price,Memo1,AddDate,ModyDate,Key,ImageUrl,ThumbUrl,content,LinkUrl,NewProduct,Elite,Author,hits,Brand";
        }

        private string GetLoopPageIDIn(string strIDMarkIn)
        {
            string strIDIn = "";
            DataRow[] foundRows = this.dTableClmn.Select("IDMark in(" + strIDMarkIn + ")");
            if (foundRows.Length == 0)
            {
                return "";
            }
            this.RecursionGetID(ref strIDIn, foundRows);
            if (strIDIn.Length > 1)
            {
                strIDIn = strIDIn.Substring(1);
            }
            return strIDIn;
        }

        private string GetLoopProductListContent(string strLoopContent, string strPageDepth, string strShow1, string strColumnID)
        {
            DataRow[] rowArray2;
            string pagePre = this.GetPagePre(strPageDepth);
            string strLoop = "";
            string strLoopRule = "";
            string str4 = "";
            DataRow[] rowArray = this.dTableClmn.Select("IDMark='" + this.LoopAttribute.Column + "'");
            if (rowArray.Length == 0)
            {
                return "此标识的栏目不存在，请改正。";
            }
            string strUrlPre = "";
            StringBuilder builder = new StringBuilder("");
            if (strShow1 == "1")
            {
                rowArray2 = rowArray;
            }
            else
            {
                rowArray2 = this.dTableClmn.Select("ParentID='" + rowArray[0]["id"] + "'");
            }
            if (rowArray2.Length > 0)
            {
                strUrlPre = this.ColumnUrlPre(rowArray2[0], this.dTableClmn);
                this.GetLabel(ref strLoopContent, ref strLoop, ref strLoopRule, ref str4, "[QH:loopProductListSmall");
                string input = this.ReadValue(strLoopRule, "TitleNum");
                if (Regex.IsMatch(input, @"^\d+$"))
                {
                    this.nSmallLength = int.Parse(input);
                }
            }
            for (int i = 0; i < rowArray2.Length; i++)
            {
                if (i == 0)
                {
                    this.strJQueryNum = rowArray2[0]["id"].ToString();
                }
                builder.Append(this.LoopReplaceBigContent(strLoopContent, rowArray2[i], pagePre, strUrlPre, i, str4, strLoop, strPageDepth, strColumnID));
            }
            return builder.ToString();
        }

        private string GetLoopTitleContent(string strloopTitle, string strPageDepth, string strColumnID, string strNowClass)
        {
            string pagePre = this.GetPagePre(strPageDepth);
            DataRow[] rowArray = this.dTableClmn.Select("IDMark='" + this.LoopAttribute.Column + "'");
            if (rowArray.Length == 0)
            {
                return "此标识的栏目不存在，请改正。";
            }
            string strUrlPre = this.ColumnUrlPre(rowArray[0], this.dTableClmn);
            string fenLanEnglish = this.GetFenLanEnglish(rowArray[0]);
            StringBuilder builder = new StringBuilder("");
            if (rowArray[0]["depth"].ToString() == "2")
            {
                string newValue = (strColumnID == rowArray[0]["id"].ToString()) ? strNowClass : "";
                builder.Append(this.LoopReplaceTitleContent(strloopTitle.Replace("[QH:NowClass]", newValue), rowArray[0], "[QH:FenLanTitle]", strPageDepth, strUrlPre, pagePre, fenLanEnglish));
                return builder.ToString();
            }
            rowArray = this.dTableClmn.Select("ParentID='" + rowArray[0]["id"].ToString() + "'");
            if (rowArray.Length == 0)
            {
                return "此标识的栏目不存在下级分类，请添加。";
            }
            strUrlPre = this.ColumnUrlPre(rowArray[0], this.dTableClmn);
            for (int i = 0; i < rowArray.Length; i++)
            {
                string str5 = (strColumnID == rowArray[i]["id"].ToString()) ? strNowClass : "";
                builder.Append(this.LoopReplaceTitleContent(strloopTitle.Replace("[QH:NowClass]", str5), rowArray[i], "[QH:FenLanTitle]", strPageDepth, strUrlPre, pagePre, fenLanEnglish));
            }
            return builder.ToString();
        }

        private string GetLoopTitleContent(string strloopTitle, string strloopsTitle, string strloopssTitle, string strPageDepth, string strColumnID, string strNowClass)
        {
            string pagePre = this.GetPagePre(strPageDepth);
            DataRow[] rowArray = this.dTableClmn.Select("IDMark='" + this.LoopAttribute.Column + "'");
            if (rowArray.Length == 0)
            {
                return "此标识的栏目不存在，请改正。";
            }
            string strUrlPre = this.ColumnUrlPre(rowArray[0], this.dTableClmn);
            string fenLanEnglish = this.GetFenLanEnglish(rowArray[0]);
            StringBuilder builder = new StringBuilder("");
            if (strloopTitle != "")
            {
                string newValue = (strColumnID == rowArray[0]["id"].ToString()) ? strNowClass : "";
                builder.Append(this.LoopReplaceTitleContent(strloopTitle.Replace("[QH:NowClass]", newValue), rowArray[0], "[QH:FenLanTitle]", strPageDepth, strUrlPre, pagePre, fenLanEnglish));
            }
            if (((strloopsTitle != "") && (strloopssTitle == "")) && (rowArray[0]["depth"].ToString() != "2"))
            {
                rowArray = this.dTableClmn.Select("ParentID='" + rowArray[0]["id"] + "'");
                if (rowArray.Length > 0)
                {
                    strUrlPre = this.ColumnUrlPre(rowArray[0], this.dTableClmn);
                }
                for (int i = 0; i < rowArray.Length; i++)
                {
                    string str5 = (strColumnID == rowArray[i]["id"].ToString()) ? this.LoopAttribute.ProductFilter : "";
                    builder.Append(this.LoopReplaceTitleContent(strloopsTitle.Replace("[QH:sNowClass]", str5), rowArray[i], "[QH:sFenLanTitle]", strPageDepth, strUrlPre, pagePre, fenLanEnglish));
                }
            }
            if (((strloopsTitle != "") && (strloopssTitle != "")) && (rowArray[0]["depth"].ToString() == "0"))
            {
                string str6 = "";
                DataRow[] rowArray2 = this.dTableClmn.Select("ParentID='" + rowArray[0]["id"] + "'");
                if (rowArray2.Length > 0)
                {
                    strUrlPre = this.ColumnUrlPre(rowArray2[0], this.dTableClmn);
                }
                for (int j = 0; j < rowArray2.Length; j++)
                {
                    string str7 = (strColumnID == rowArray2[j]["id"].ToString()) ? this.LoopAttribute.ProductFilter : "";
                    builder.Append(this.LoopReplaceTitleContent(strloopsTitle.Replace("[QH:sNowClass]", str7), rowArray2[j], "[QH:sFenLanTitle]", strPageDepth, strUrlPre, pagePre, fenLanEnglish));
                    DataRow[] rowArray3 = this.dTableClmn.Select("ParentID='" + rowArray2[j]["id"] + "'");
                    if (rowArray3.Length > 0)
                    {
                        str6 = this.ColumnUrlPre(rowArray3[0], this.dTableClmn);
                    }
                    for (int k = 0; k < rowArray3.Length; k++)
                    {
                        string str8 = (strColumnID == rowArray3[j]["id"].ToString()) ? this.LoopAttribute.Sort : "";
                        builder.Append(this.LoopReplaceTitleContent(strloopssTitle.Replace("[QH:ssNowClass]", str8), rowArray3[k], "[QH:ssFenLanTitle]", strPageDepth, str6, pagePre, fenLanEnglish));
                    }
                }
            }
            return builder.ToString();
        }

        private string GetNavLoopContent(string strLoopContent, string strPageDepth, string strColumn, string strModule, string strColumnID, string strUpOrDown, int nNumStart)
        {
            string pagePre = this.GetPagePre(strPageDepth);
            string strLoop = "";
            string strLoopRule = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string strIDMarkIn = "";
            string strIDMarkNotIn = "";
            DataRow[] rowArray = null;
            if (strColumn == "")
            {
                rowArray = this.dTableClmn.Select("Nav='" + strUpOrDown + "' or Nav='3'", "Order asc");
            }
            else
            {
                this.GetIDMarkInAndNotIn(strColumn, ref strIDMarkIn, ref strIDMarkNotIn);
                if (strIDMarkIn != "")
                {
                    rowArray = this.dTableClmn.Select("IDMark in(" + strIDMarkIn + ") and (Nav='" + strUpOrDown + "' or Nav='3')", "Order asc");
                }
                else if (strIDMarkNotIn != "")
                {
                    rowArray = this.dTableClmn.Select("IDMark not in(" + strIDMarkNotIn + ") and (Nav='" + strUpOrDown + "' or Nav='3')", "Order asc");
                }
            }
            if (rowArray == null)
            {
                return "栏目不存在或不是导航栏目，请改正。";
            }
            if (rowArray.Length == 0)
            {
                return "栏目不存在或不是导航栏目，请改正。";
            }
            this.GetLabel(ref strLoopContent, ref strLoop, ref strLoopRule, ref str4, "[QH:NavloopMedium");
            string input = this.ReadValue(strLoopRule, "TitleNum");
            if (Regex.IsMatch(input, @"^\d+$"))
            {
                this.nMediumLength = int.Parse(input);
            }
            this.GetLabel(ref str4, ref str5, ref str6, ref str7, "[QH:NavloopSmall");
            input = this.ReadValue(str6, "TitleNum");
            if (Regex.IsMatch(input, @"^\d+$"))
            {
                this.nSmallLength = int.Parse(input);
            }
            string strUrlPre = "";
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < rowArray.Length; i++)
            {
                builder.Append(this.NavLoopReplaceBigContent(strLoopContent, rowArray[i], pagePre, strUrlPre, i, str4, strLoop, str7, str5, strPageDepth, strColumnID, nNumStart++));
            }
            return builder.ToString();
        }

        private string GetNewsType(string NewsType)
        {
            string str = "";
            string str2 = NewsType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "Fps"))
            {
                if (str2 != "Top")
                {
                    return str;
                }
            }
            else
            {
                return "firstPageShow=true ";
            }
            return "TopShow=true ";
        }

        private DataTable GetNoDelTable(DataTable dt, string strID)
        {
            DataRow[] rowArray = dt.Select("id=" + strID);
            if (rowArray.Length > 0)
            {
                dt.Rows.Remove(rowArray[0]);
            }
            return dt;
        }

        private void GetOrderby(out string strOrder, out string strSort, string strListOrder)
        {
            strSort = "ModyDate";
            strOrder = "desc";
            string str = strListOrder;
            if (str != null)
            {
                if (!(str == "1"))
                {
                    if (!(str == "2"))
                    {
                        if (!(str == "3"))
                        {
                            if (str == "4")
                            {
                                strSort = "id";
                                strOrder = "asc";
                            }
                            return;
                        }
                        strSort = "id";
                        strOrder = "desc";
                        return;
                    }
                }
                else
                {
                    strSort = "AddDate";
                    return;
                }
                strSort = "CLNG(hits)";
            }
        }

        private string[] GetPageContent(ref string strTemp, DataRow dw, out string strListLoop, string strPubu, out string[] astrHitsIdHdn)
        {
            DataTable loopDataTable;
            astrHitsIdHdn = null;
            string strLoop = "";
            string strLoopRule = "";
            string strLoopContent = "";
            string strQuery = "";
            string module = dw["Module"].ToString();
            string tableName = this.GetTableName(module);
            if (tableName != "QH_ZhaoPin")
            {
                this.GetLabel(ref strTemp, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:loopPage ");
                strListLoop = strLoop;
                if (strLoop == "")
                {
                    return null;
                }
                this.GetLoopAttribute(strLoopRule);
                string loopPageField = this.GetLoopPageField(tableName);
                strQuery = string.Format(this.GetQueryingString(dw, tableName, strLoopRule), loopPageField);
                loopDataTable = this.GetLoopDataTable(strQuery);
                if (((module.Length == 1) && (module.IndexOfAny(new char[] { '3', '4', '5' }) != -1)) && (strLoopContent.IndexOf("CusInfo:") >= 0))
                {
                    string iDForParameter = this.GetIDForParameter(dw["IDMark"].ToString());
                    this.listStrCusField = this.GetCusFieldInfo(strLoopContent, module);
                    this.GetCusFieldInfoID_Name_pList(this.listStrCusField, module, iDForParameter);
                }
            }
            else
            {
                strListLoop = "[QH:HrDemand]";
                strLoopContent = this.GetHrDemandLoop();
                loopDataTable = this.GetZhaoPinDataTable();
                this.astrtags = new string[] { "<%#Eval(\"职位名称\")%>", "<%#Eval(\"strEc职位名称\")%>", "<%#Eval(\"工作地点\")%>", "<%#Eval(\"工资待遇\")%>", "<%#Eval(\"发布日期\")%>", "<%#Eval(\"需求人数\")%>", "<%#Eval(\"有效期限\")%>", "<%#Eval(\"具体要求\")%>" };
                this.astrColumn = new string[] { "职位名称", "strEc职位名称", "工作地点", "工资待遇", "发布日期", "需求人数", "有效期限", "具体要求" };
            }
            string input = this.bIsMobile ? this.LoopAttribute.NewsCount : dw["NumInPage"].ToString();
            if (!Regex.IsMatch(input, @"^\d+$"))
            {
                input = "20";
            }
            if (loopDataTable == null)
            {
                if (strLoop != "")
                {
                    strTemp = strTemp.Replace(strLoop, "未能读取数据表。");
                }
                else if (strListLoop != "")
                {
                    strTemp = strTemp.Replace(strListLoop, "未能读取数据表。");
                }
                strTemp = strTemp.Replace("[QH:TotleNumber]", "0");
                strTemp = strTemp.Replace("[QH:TotlePage]", "0");
                strTemp = strTemp.Replace("[QH:NumberInPage]", input);
                strTemp = strTemp.Replace("[QH:CurrentPage]", "0");
                return null;
            }
            if (loopDataTable.Rows.Count == 0)
            {
                if (strLoop != "")
                {
                    strTemp = strTemp.Replace(strLoop, "此分类下暂无数据。");
                }
                else if (strListLoop != "")
                {
                    strTemp = strTemp.Replace(strListLoop, "此分类下暂无数据。");
                }
                strTemp = strTemp.Replace("[QH:TotleNumber]", "0");
                strTemp = strTemp.Replace("[QH:TotlePage]", "0");
                strTemp = strTemp.Replace("[QH:NumberInPage]", input);
                strTemp = strTemp.Replace("[QH:CurrentPage]", "0");
                return null;
            }
            strTemp = strTemp.Replace("[QH:TotleNumber]", loopDataTable.Rows.Count.ToString());
            int nCNumber = int.Parse(input);
            int nPageNumber = (int)Math.Ceiling((double)((loopDataTable.Rows.Count * 1.0) / ((double)nCNumber)));
            strTemp = strTemp.Replace("[QH:TotlePage]", nPageNumber.ToString());
            strTemp = strTemp.Replace("[QH:NumberInPage]", input);
            if (!(strPubu == "Pubu"))
            {
                return this.GetContentArray(strLoopContent, loopDataTable, nPageNumber, nCNumber, tableName, dw["depth"].ToString(), out astrHitsIdHdn);
            }
            string strPageDepth = dw["depth"].ToString();
            nCNumber = 0x30;
            List<string[]> imageData = this.GetImageData(loopDataTable);
            if (imageData.Count == 0)
            {
                strTemp = strTemp.Replace(strLoop, "此分类下暂无图片。");
                return null;
            }
            strTemp = strTemp.Replace("[QH:TotleNumber]", imageData.Count.ToString());
            nPageNumber = (int)Math.Ceiling((double)((imageData.Count * 1.0) / ((double)nCNumber)));
            string pagePre = this.GetPagePre(strPageDepth);
            if (strTemp.IndexOf("jquery") == -1)
            {
                this.InsertLink(ref strTemp, "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + pagePre + "Ajax/css2.css\"/>\n<script type=\"text/javascript\" src=\"" + pagePre + "Ajax/jquery.js\"></script>\n");
            }
            else
            {
                this.InsertLink(ref strTemp, "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + pagePre + "Ajax/css2.css\"/>\n\n");
            }
            this.InsertJsBottom(ref strTemp, strPageDepth, "Ajax/AjaxPubu.js");
            this.InsertHiddenBottom(ref strTemp, "HdnID", dw["id"].ToString());
            return this.GetContentArrayPubu(imageData, nPageNumber, nCNumber, dw["depth"].ToString());
        }

        private string[] GetPageContent(ref string strTemp, DataRow dw, out string strListLoop, string strPubu, out string[] astrHitsIdHdn, string[] astrPInterval)
        {
            astrHitsIdHdn = null;
            string strLoop = "";
            string strLoopRule = "";
            string strLoopContent = "";
            string strQuery = "";
            string module = dw["Module"].ToString();
            string tableName = this.GetTableName(module);
            this.GetLabel(ref strTemp, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:loopPage ");
            strListLoop = strLoop;
            if (strLoop == "")
            {
                return null;
            }
            this.GetLoopAttribute(strLoopRule);
            string loopPageField = this.GetLoopPageField(tableName);
            strQuery = string.Format(this.GetQueryingStringProduct(dw, tableName), loopPageField);
            if (this.dTableProductAll == null)
            {
                this.dTableProductAll = this.GetLoopDataTable(strQuery);
                if (this.dTableProductAll == null)
                {
                    strTemp = strTemp.Replace(strLoop, "未能读取数据表。");
                    return null;
                }
                if (this.dTableProductAll.Rows.Count == 0)
                {
                    strTemp = strTemp.Replace(strLoop, "此分类下暂无数据。");
                    return null;
                }
                this.AddPriceColumn(ref this.dTableProductAll);
            }
            if ((module.IndexOfAny(new char[] { '3', '4', '5' }) != -1) && (strLoopContent.IndexOf("CusName:") >= 0))
            {
                string iDForParameter = this.GetIDForParameter(dw["IDMark"].ToString());
                this.listStrCusField = this.GetCusFieldInfo(strLoopContent, module);
                this.GetCusFieldInfoID_Name_pList(this.listStrCusField, module, iDForParameter);
            }
            DataTable priceIntervalTable = this.GetPriceIntervalTable(ref this.dTableProductAll, astrPInterval);
            if (priceIntervalTable.Rows.Count == 0)
            {
                strTemp = strTemp.Replace(strLoop, "此价格区间暂无数据。");
                return null;
            }
            strTemp = strTemp.Replace("[QH:TotleNumber]", priceIntervalTable.Rows.Count.ToString());
            string input = this.bIsMobile ? this.LoopAttribute.NewsCount : dw["NumInPage"].ToString();
            if (!Regex.IsMatch(input, @"^\d+$"))
            {
                input = "20";
            }
            int nCNumber = int.Parse(input);
            int nPageNumber = (int)Math.Ceiling((double)((priceIntervalTable.Rows.Count * 1.0) / ((double)nCNumber)));
            return this.GetContentArray(strLoopContent, priceIntervalTable, nPageNumber, nCNumber, tableName, dw["depth"].ToString(), out astrHitsIdHdn);
        }

        private string GetPagePre(string strPageDepth)
        {
            string str = "";
            if (strPageDepth == "0")
            {
                str = "../";
            }
            if (strPageDepth == "1")
            {
                str = "../../";
            }
            if (strPageDepth == "2")
            {
                str = "../../../";
            }
            return str;
        }

        private void GetPageSEO(out string strSTitle, out string strSDesc, out string strSKWord, string str1Title, string str1Desc, string str1Kword, string strClmndepth, string strColumnID)
        {
            string str;
            strSTitle = str1Title;
            strSDesc = str1Desc;
            strSKWord = str1Kword;
            if ((strColumnID != "S") && ((str = strClmndepth) != null))
            {
                DataRow[] rowArray;
                if (!(str == "0"))
                {
                    DataRow[] rowArray2;
                    if (!(str == "1"))
                    {
                        if (str == "2")
                        {
                            DataRow[] rowArray3 = this.dTableClmn.Select("ID=" + strColumnID);
                            if (rowArray3.Length > 0)
                            {
                                rowArray2 = this.dTableClmn.Select("ID=" + rowArray3[0]["ParentID"]);
                                if (rowArray2.Length > 0)
                                {
                                    rowArray = this.dTableClmn.Select("ID=" + rowArray2[0]["ParentID"]);
                                    if (rowArray.Length > 0)
                                    {
                                        strSTitle = rowArray[0]["ColumnName"].ToString();
                                        this.SetSEO(ref strSTitle, ref strSDesc, ref strSKWord, rowArray[0]);
                                        this.SetSEO(ref strSTitle, ref strSDesc, ref strSKWord, rowArray2[0]);
                                        this.SetSEO(ref strSTitle, ref strSDesc, ref strSKWord, rowArray3[0]);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        rowArray2 = this.dTableClmn.Select("ID=" + strColumnID);
                        if (rowArray2.Length > 0)
                        {
                            rowArray = this.dTableClmn.Select("ID=" + rowArray2[0]["ParentID"]);
                            if (rowArray.Length > 0)
                            {
                                strSTitle = rowArray[0]["ColumnName"].ToString();
                                this.SetSEO(ref strSTitle, ref strSDesc, ref strSKWord, rowArray[0]);
                                this.SetSEO(ref strSTitle, ref strSDesc, ref strSKWord, rowArray2[0]);
                            }
                        }
                    }
                }
                else
                {
                    rowArray = this.dTableClmn.Select("ID=" + strColumnID);
                    if (rowArray.Length > 0)
                    {
                        strSTitle = rowArray[0]["ColumnName"].ToString();
                        this.SetSEO(ref strSTitle, ref strSDesc, ref strSKWord, rowArray[0]);
                    }
                }
            }
        }

        private List<string[]> GetPicData(DataRow dRow, int nStart)
        {
            List<string[]> list = new List<string[]>();
            string str = dRow["ImageUrl"].ToString().Trim();
            string[] item = new string[4];
            int num = nStart;
            if (str != "")
            {
                item[0] = dRow["Title"].ToString().Trim();
                item[1] = str;
                item[2] = dRow["id"].ToString();
                item[3] = num.ToString();
                num++;
                list.Add(item);
            }
            str = dRow["displayImg"].ToString().Trim();
            if (str != "")
            {
                foreach (string str2 in str.Split(new char[] { ',' }))
                {
                    string[] strArray3 = str2.Split(new char[] { '-' });
                    if (strArray3.Length == 2)
                    {
                        item = new string[] { strArray3[0].Trim(), strArray3[1].Trim(), dRow["id"].ToString(), num.ToString() };
                        num++;
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        private DataTable GetPriceIntervalTable(ref DataTable dTable, string[] astrPInterval)
        {
            string filterExpression = (astrPInterval[1] == "") ? (" DblPrice>" + astrPInterval[0]) : (" DblPrice>" + astrPInterval[0] + " and DblPrice<=" + astrPInterval[1]);
            DataRow[] rows = dTable.Select(filterExpression);
            DataSet set = new DataSet();
            DataTable table = new DataTable();
            table = dTable.Clone();
            set.Merge(table);
            set.Merge(rows);
            return set.Tables[0];
        }

        private string GetProductFilter(string ProductFilter)
        {
            string str = "";
            string str2 = ProductFilter;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "new"))
            {
                if (str2 != "AuProduct")
                {
                    if (str2 != "Passed")
                    {
                        return str;
                    }
                    return "Passed=true ";
                }
            }
            else
            {
                return "NewProduct=true ";
            }
            return "Elite=true ";
        }

        private string GetQueryingString()
        {
            string tableName = this.GetTableName(this.LoopAttribute.Module);
            string[] strArray = this.LoopAttribute.NewsDate.Split(new char[] { '|' });
            string[] strArray2 = this.LoopAttribute.Column.Split(new char[] { '|' });
            string strColumnIDIn = "";
            string columnID = this.GetColumnID(strArray2[0]);
            if (((strArray2.Length == 1) && (this.LoopAttribute.Module.Length == 1)) && (this.LoopAttribute.Module.IndexOfAny(new char[] { '2', '3', '4', '5' }) != -1))
            {
                this.GetIDMarkSubIN(ref strColumnIDIn, strArray2[0], this.LoopAttribute.Module);
            }
            else
            {
                foreach (string str4 in strArray2)
                {
                    strColumnIDIn = strColumnIDIn + ",'" + this.GetColumnID(str4) + "'";
                }
            }
            string str5 = "";
            string str6 = "";
            bool flag = false;
            string strSort = "id";
            string strOrder = "desc";
            if (this.LoopAttribute.Sort == "")
            {
                flag = true;
                DataRow[] rowArray = this.dTableClmn.Select("id=" + columnID);
                if (rowArray.Length == 0)
                {
                    flag = false;
                }
                else
                {
                    this.GetOrderby(out strOrder, out strSort, rowArray[0]["ListOrder"].ToString());
                }
            }
            if (this.LoopAttribute.NewsDate == "Today")
            {
                str5 = str6 = DateTime.Now.ToString("yyyy-M-d");
            }
            else if (strArray.Length > 1)
            {
                str5 = strArray[0];
                str6 = strArray[1];
            }
            else
            {
                str5 = strArray[0];
                str6 = DateTime.Now.ToString("yyyy-M-d");
            }
            if (!flag)
            {
                if (this.LoopAttribute.Order == "UnDesc")
                {
                    strOrder = "asc";
                }
                if (this.LoopAttribute.Sort == "DateTime")
                {
                    strSort = "Adddate";
                }
                else if (this.LoopAttribute.Sort == "Hits")
                {
                    strSort = "hits";
                }
            }
            switch (tableName)
            {
                case "QH_FriendLinks":
                    {
                        string str9 = (this.LoopAttribute.NewsType == "Img") ? "false" : "true";
                        if (this.LoopAttribute.NewsCount == "")
                        {
                            return ("select SiteName,SiteUrl,SiteIntro,LogoUrl from QH_FriendLinks where LinkType=" + str9 + " order by id " + strOrder);
                        }
                        return ("select top " + this.LoopAttribute.NewsCount + " SiteName,SiteUrl,SiteIntro,LogoUrl from QH_FriendLinks where LinkType=" + str9 + " order by id " + strOrder);
                    }
                case "QH_ProductBrand":
                    {
                        string str10 = "";
                        if (this.LoopAttribute.NewsCount == "")
                        {
                            str10 = "select Brand,Logo,Link,[Memo] from QH_ProductBrand order by id " + strOrder;
                        }
                        else
                        {
                            str10 = "select top " + this.LoopAttribute.NewsCount + " Brand,Logo,Link,[Memo] from QH_ProductBrand order by id " + strOrder;
                        }
                        Match match = Regex.Match(this.LoopAttribute.ProductFilter, @"\[QH:BriefIntro:?\s*(\d*)\s*\]");
                        if (match.Groups[0].Value == "")
                        {
                            this.LoopAttribute.JStype = "-1";
                            return str10;
                        }
                        string str11 = match.Groups[1].Value;
                        this.LoopAttribute.JStype = (str11 == "") ? "0" : str11;
                        this.LoopAttribute.ProductFilter = match.Groups[0].Value;
                        return str10;
                    }
            }
            string newsType = this.GetNewsType(this.LoopAttribute.NewsType);
            string str13 = "";
            if ((this.LoopAttribute.Column != "All") && (strColumnIDIn.Length > 1))
            {
                str13 = " where ColumnID in(" + strColumnIDIn.Substring(1) + ") ";
            }
            if ((newsType != "") || (str5 != ""))
            {
                if (str13.Trim() == "")
                {
                    str13 = " where " + newsType;
                }
                else
                {
                    str13 = str13 + " and " + newsType;
                }
                if (str5 != "")
                {
                    if (newsType != "")
                    {
                        string str16 = str13;
                        str13 = str16 + "and AddDate between #" + str5 + "# and #" + str6 + "#";
                    }
                    else
                    {
                        string str17 = str13;
                        str13 = str17 + "AddDate between #" + str5 + "# and #" + str6 + "#";
                    }
                }
            }
            string str14 = ((this.LoopAttribute.Module.Length == 1) && (this.LoopAttribute.Module.IndexOfAny(new char[] { '2', '3', '4', '5' }) == -1)) ? "" : " TopShow asc,";
            if (this.LoopAttribute.NewsCount == "")
            {
                this.LoopAttribute.NewsCount = "0";
            }
            string str15 = "select top " + this.LoopAttribute.NewsCount + " * from " + tableName + str13 + " order by " + str14 + strSort + " " + strOrder;
            if (!(this.LoopAttribute.KeyWord != "") && !(this.LoopAttribute.NewsCount == "0"))
            {
                return str15;
            }
            return ("select  * from " + tableName + str13 + " order by " + str14 + strSort + " " + strOrder);
        }

        private string GetQueryingString(DataRow dw, string TableName, string strLoopRule)
        {
            string str;
            string str2;
            this.LoopAttribute.NewsDate.Split(new char[] { '|' });
            this.GetOrderby(out str, out str2, dw["ListOrder"].ToString());
            string strColumn = this.ReadValue(strLoopRule, "ShowAll");
            if ((strColumn != "") && (dw["depth"].ToString() == "0"))
            {
                if (strColumn == "All")
                {
                    return ("select {0} from " + TableName + " order by TopShow asc, " + str2 + " " + str);
                }
                string strIDMarkIn = "";
                string strIDMarkNotIn = "";
                string loopPageIDIn = "";
                string str8 = "";
                this.GetIDMarkInAndNotIn(strColumn, ref strIDMarkIn, ref strIDMarkNotIn);
                if (strIDMarkIn != "")
                {
                    loopPageIDIn = this.GetLoopPageIDIn(strIDMarkIn);
                    if (loopPageIDIn != "")
                    {
                        return ("select {0} from " + TableName + " where ColumnID in(" + loopPageIDIn + ")  order by TopShow asc, " + str2 + " " + str);
                    }
                }
                else if (strIDMarkNotIn != "")
                {
                    str8 = this.GetLoopPageIDIn(strIDMarkNotIn);
                    if (str8 != "")
                    {
                        return ("select {0} from " + TableName + " where ColumnID not in(" + str8 + ")  order by TopShow asc, " + str2 + " " + str);
                    }
                }
            }
            if (this.LoopAttribute.Column != "")
            {
                string str9 = this.ReadValue(strLoopRule, "Show");
                string strMdl = dw["Module"].ToString();
                string[] strArray = this.LoopAttribute.Column.Split(new char[] { '|' });
                string strColumnIDIn = "";
                string columnID = this.GetColumnID(strArray[0]);
                if (((strArray.Length == 1) && (strMdl.Length == 1)) && (strMdl.IndexOfAny(new char[] { '2', '3', '4', '5' }) != -1))
                {
                    this.GetIDMarkSubIN(ref strColumnIDIn, strArray[0], strMdl);
                }
                else if (str9 == "All")
                {
                    foreach (string str13 in strArray)
                    {
                        string str14 = "";
                        this.GetIDMarkSubIN(ref str14, str13, strMdl);
                        strColumnIDIn = strColumnIDIn + str14;
                    }
                }
                else
                {
                    foreach (string str15 in strArray)
                    {
                        strColumnIDIn = strColumnIDIn + ",'" + this.GetColumnID(str15) + "'";
                    }
                }
                if (this.LoopAttribute.Sort == "")
                {
                    DataRow[] rowArray = this.dTableClmn.Select("id=" + columnID);
                    if (rowArray.Length != 0)
                    {
                        this.GetOrderby(out str, out str2, rowArray[0]["ListOrder"].ToString());
                    }
                }
                strColumnIDIn = (strColumnIDIn == "") ? "'0'" : strColumnIDIn.Substring(1);
                return ("select {0} from " + TableName + " where ColumnID in(" + strColumnIDIn + ")  order by TopShow asc, " + str2 + " " + str);
            }
            if (dw["ListContent"].ToString() == "0")
            {
                return ("select {0} from " + TableName + " where ColumnID='" + dw["id"].ToString() + "'  order by TopShow asc, " + str2 + " " + str);
            }
            return ("select {0} from " + TableName + " where ColumnID in(" + this.GetSubClassID(dw["id"].ToString(), dw["depth"].ToString(), dw["ListContent"].ToString()) + ")  order by TopShow asc, " + str2 + " " + str);
        }

        private string GetQueryingStringProduct(DataRow dw, string TableName)
        {
            string str;
            string str2;
            this.GetOrderby(out str, out str2, dw["ListOrder"].ToString());
            return ("select {0} from " + TableName + " where ColumnID in(" + this.GetSubClassID(dw["id"].ToString(), dw["depth"].ToString(), dw["ListContent"].ToString()) + ")  order by TopShow asc, " + str2 + " " + str);
        }

        private void GetSearchLabelAndID(string strTemp1, ref string strInputID, string strOnclickTags)
        {
            int index = strTemp1.IndexOf(strOnclickTags);
            if (index == -1)
            {
                strInputID = "$$";
            }
            else
            {
                strInputID = "";
                for (int i = 0; i < 10; i++)
                {
                    int startIndex = strTemp1.IndexOf(']', index);
                    string str = strTemp1.Substring(index, (startIndex - index) + 1).Replace(strOnclickTags, "").Replace(":", "").Replace("]", "").Trim();
                    if (strOnclickTags == "[QH:SearchOnclick")
                    {
                        str = str.Split(new char[] { '_' })[0];
                    }
                    strInputID = strInputID + "|" + str;
                    index = strTemp1.IndexOf(strOnclickTags, startIndex);
                    if (index == -1)
                    {
                        break;
                    }
                }
                strInputID = strInputID.TrimStart(new char[] { '|' });
            }
        }

        private string GetSearchLabelAndID(string strTemp1, ref string strInputID, ref bool bQuotes, string strOnclickTags)
        {
            string str2;
            int index = strTemp1.IndexOf(strOnclickTags);
            if (index == -1)
            {
                strInputID = "$$";
                return "0";
            }
            int num2 = strTemp1.IndexOf(']', index);
            if (strTemp1[num2 + 1] == '"')
            {
                bQuotes = true;
            }
            string str = str2 = strTemp1.Substring(index, (num2 - index) + 1);
            str2 = str2.Replace(strOnclickTags, "").Replace(":", "").Replace("]", "").Trim();
            strInputID = (str2 == "") ? strInputID : str2;
            return str;
        }

        private string GetSearchLabelAndIDAll(string strTemp1, ref string strInputID, ref bool bQuotes, string strOnclickTags)
        {
            string str2;
            int index = strTemp1.IndexOf(strOnclickTags);
            if (index == -1)
            {
                strInputID = "$$";
                return "0";
            }
            int num2 = strTemp1.IndexOf(']', index);
            if (strTemp1[num2 + 1] == '"')
            {
                bQuotes = true;
            }
            string str = str2 = strTemp1.Substring(index, (num2 - index) + 1);
            str2 = str2.Replace(strOnclickTags, "").Replace(":", "").Replace("]", "").Trim();
            strInputID = (str2 == "") ? strInputID : str2;
            return str;
        }

        private void GetSiteInfoContents(ref string strTemp, string strPageDepth, string strColumnID)
        {
            if ((this.astr1 != null) && (this.astr1.Length == 40))
            {
                string str;
                string str2;
                string str3;
                this.GetPageSEO(out str, out str2, out str3, this.astr1[0], this.astr1[1], this.astr1[2], strPageDepth, strColumnID);
                strTemp = strTemp.Replace("[QH:SiteTitle]", str);
                strTemp = strTemp.Replace("[QH:SiteDescription]", str2);
                strTemp = strTemp.Replace("[QH:SiteKeyword]", str3);
                strTemp = strTemp.Replace("[QH:QQ1]", this.astr1[3]);
                strTemp = strTemp.Replace("[QH:QQ2]", this.astr1[4]);
                strTemp = strTemp.Replace("[QH:SitePath]", this.astr1[5]);
                strTemp = strTemp.Replace("[QH:Logo]", this.bIsMobile ? this.PageUrlSet(this.astr1[0x1b], strPageDepth) : this.PageUrlSet(this.astr1[6], strPageDepth));
                strTemp = strTemp.Replace("[QH:Favicon]", this.PageUrlSet(this.astr1[0x23], strPageDepth));
                strTemp = strTemp.Replace("[QH:Copyright]", this.astr1[7]);
                strTemp = strTemp.Replace("[QH:Tel]", this.astr1[8]);
                strTemp = strTemp.Replace("[QH:Address]", this.astr1[9]);
                strTemp = strTemp.Replace("[QH:ICP]", this.astr1[10]);
                strTemp = strTemp.Replace("[QH:Fax]", this.astr1[11]);
                strTemp = strTemp.Replace("[QH:Mobile]", this.astr1[12]);
                strTemp = strTemp.Replace("[QH:Email]", this.astr1[13]);
                strTemp = strTemp.Replace("[QH:Contact]", this.astr1[14]);
                strTemp = strTemp.Replace("[QH:Bak1]", this.astr1[15]);
                strTemp = strTemp.Replace("[QH:Bak2]", this.astr1[0x10]);
                strTemp = strTemp.Replace("[QH:Bak3]", this.astr1[0x20]);
                strTemp = strTemp.Replace("[QH:BakImg1]", this.PageUrlSet(this.astr1[0x11], strPageDepth));
                strTemp = strTemp.Replace("[QH:BakImg2]", this.PageUrlSet(this.astr1[0x12], strPageDepth));
                strTemp = strTemp.Replace("[QH:BakImg3]", this.PageUrlSet(this.astr1[0x21], strPageDepth));
                strTemp = strTemp.Replace("[QH:AboutUs]", this.PageContentUrlSet(this.astr1[0x13], strPageDepth));
                strTemp = strTemp.Replace("[QH:JScript]", this.astr1[20]);
                strTemp = strTemp.Replace("[QH:Bottom1]", this.astr1[0x1c]);
                strTemp = strTemp.Replace("[QH:Bottom2]", this.astr1[0x1d]);
                strTemp = strTemp.Replace("[QH:Bottom3]", this.astr1[30]);
                strTemp = strTemp.Replace("[QH:BottomQT]", this.astr1[0x1f]);
                string pagePre = this.GetPagePre(strPageDepth);
                strTemp = strTemp.Replace("[QH:PathPre]", pagePre + this.strGlobalRootDirPre);
                strTemp = strTemp.Replace("[QH:SiteMap]", "<a href=\"" + pagePre + "sitemap.xml\" target=_blank>网站地图</a>");
                strTemp = strTemp.Replace("[QH:HomeLink]", pagePre);
                if (this.astr1[0x25] == "True")
                {
                    strTemp = strTemp.Replace("[QH:JSStatistic]", this.astr1[0x24]);
                }
                else
                {
                    strTemp = strTemp.Replace("[QH:JSStatistic]", "");
                }
                strTemp = strTemp.Replace("[QH:SiteName]", this.astr1[0x26]);
                if (strTemp.IndexOf("[QH:Chinese") >= 0)
                {
                    this.InsertLink(ref strTemp, "<script type=\"text/javascript\" src=\"" + pagePre + "Ajax/js/TransCnLang.js\"></script>\n");
                    strTemp = strTemp.Replace("[QH:ChineseSimple]", "javascript:if(vLang==0)convert();");
                    strTemp = strTemp.Replace("[QH:ChineseTradition]", "javascript:if(vLang==1)convert();");
                }
                if (strTemp.IndexOf("[QH:ToLand]") >= 0)
                {
                    this.ReplaceMemberLand(ref strTemp, pagePre);
                }
            }
        }

        private void GetSiteInfoContents(ref string strTemp, string strPageDepth, string strColumnID, string strClmndepth, DataRow dwDetails)
        {
            if ((this.astr1 != null) && (this.astr1.Length == 40))
            {
                string strSTitle = "";
                string strSDesc = "";
                string strSKWord = "";
                this.SetSEO(ref strSTitle, ref strSDesc, ref strSKWord, dwDetails, "detail");
                strTemp = strTemp.Replace("[QH:SiteTitle]", strSTitle);
                strTemp = strTemp.Replace("[QH:SiteDescription]", strSDesc);
                strTemp = strTemp.Replace("[QH:SiteKeyword]", strSKWord);
                strTemp = strTemp.Replace("[QH:QQ1]", this.astr1[3]);
                strTemp = strTemp.Replace("[QH:QQ2]", this.astr1[4]);
                strTemp = strTemp.Replace("[QH:SitePath]", this.astr1[5]);
                strTemp = strTemp.Replace("[QH:Logo]", this.bIsMobile ? this.PageUrlSet(this.astr1[0x1b], strPageDepth) : this.PageUrlSet(this.astr1[6], strPageDepth));
                strTemp = strTemp.Replace("[QH:Favicon]", this.PageUrlSet(this.astr1[0x23], strPageDepth));
                strTemp = strTemp.Replace("[QH:Copyright]", this.astr1[7]);
                strTemp = strTemp.Replace("[QH:Tel]", this.astr1[8]);
                strTemp = strTemp.Replace("[QH:Address]", this.astr1[9]);
                strTemp = strTemp.Replace("[QH:ICP]", this.astr1[10]);
                strTemp = strTemp.Replace("[QH:Fax]", this.astr1[11]);
                strTemp = strTemp.Replace("[QH:Mobile]", this.astr1[12]);
                strTemp = strTemp.Replace("[QH:Email]", this.astr1[13]);
                strTemp = strTemp.Replace("[QH:Contact]", this.astr1[14]);
                strTemp = strTemp.Replace("[QH:Bak1]", this.astr1[15]);
                strTemp = strTemp.Replace("[QH:Bak2]", this.astr1[0x10]);
                strTemp = strTemp.Replace("[QH:Bak3]", this.astr1[0x20]);
                strTemp = strTemp.Replace("[QH:BakImg1]", this.PageUrlSet(this.astr1[0x11], strPageDepth));
                strTemp = strTemp.Replace("[QH:BakImg2]", this.PageUrlSet(this.astr1[0x12], strPageDepth));
                strTemp = strTemp.Replace("[QH:BakImg3]", this.PageUrlSet(this.astr1[0x21], strPageDepth));
                strTemp = strTemp.Replace("[QH:AboutUs]", this.PageContentUrlSet(this.astr1[0x13], strPageDepth));
                strTemp = strTemp.Replace("[QH:JScript]", this.astr1[20]);
                strTemp = strTemp.Replace("[QH:Bottom1]", this.astr1[0x1c]);
                strTemp = strTemp.Replace("[QH:Bottom2]", this.astr1[0x1d]);
                strTemp = strTemp.Replace("[QH:Bottom3]", this.astr1[30]);
                strTemp = strTemp.Replace("[QH:BottomQT]", this.astr1[0x1f]);
                string pagePre = this.GetPagePre(strPageDepth);
                strTemp = strTemp.Replace("[QH:PathPre]", pagePre + this.strGlobalRootDirPre);
                strTemp = strTemp.Replace("[QH:SiteMap]", "<a href=\"" + pagePre + "sitemap.xml\" target=_blank>网站地图</a>");
                strTemp = strTemp.Replace("[QH:HomeLink]", pagePre);
                if (this.astr1[0x25] == "True")
                {
                    strTemp = strTemp.Replace("[QH:JSStatistic]", this.astr1[0x24]);
                }
                else
                {
                    strTemp = strTemp.Replace("[QH:JSStatistic]", "");
                }
                strTemp = strTemp.Replace("[QH:SiteName]", this.astr1[0x26]);
                if (strTemp.IndexOf("[QH:Chinese") >= 0)
                {
                    this.InsertLink(ref strTemp, "<script type=\"text/javascript\" src=\"" + pagePre + "Ajax/js/TransCnLang.js\"></script>\n");
                    strTemp = strTemp.Replace("[QH:ChineseSimple]", "javascript:if(vLang==0)convert();");
                    strTemp = strTemp.Replace("[QH:ChineseTradition]", "javascript:if(vLang==1)convert();");
                }
                if (strTemp.IndexOf("[QH:ToLand]") >= 0)
                {
                    this.ReplaceMemberLand(ref strTemp, pagePre);
                }
            }
        }

        private bool GetStaticFUrlPath(out string strUrlPath, DataRow dRow, DataTable dTableClmn, string strRootDir)
        {
            if (strRootDir != "")
            {
                this.CheckRootDir(strRootDir);
            }
            strRootDir = (strRootDir == "") ? "" : (strRootDir + "/");
            bool flag = true;
            strUrlPath = "";
            string str = "";
            string str2 = "";
            string str3 = "";
            this.Get3ClassClmnData(ref str, ref str2, ref str3, "folder", dRow);
            str = strRootDir + str;
            string str5 = dRow["depth"].ToString();
            if (str5 != null)
            {
                if (!(str5 == "0"))
                {
                    if (str5 == "1")
                    {
                        strUrlPath = str + "/" + str2;
                    }
                    else if (str5 == "2")
                    {
                        strUrlPath = str + "/" + str2 + "/" + str3;
                    }
                }
                else
                {
                    strUrlPath = str;
                }
            }
            try
            {
                string path = this.CSPage.Server.MapPath("../" + str);
                DirectoryInfo info = new DirectoryInfo(path);
                if (!info.Exists)
                {
                    info.Create();
                }
                if (str2 != "")
                {
                    info = new DirectoryInfo(path + "/" + str2);
                    if (!info.Exists)
                    {
                        info.Create();
                    }
                }
                if (str3 != "")
                {
                    info = new DirectoryInfo(path + "/" + str2 + "/" + str3);
                    if (!info.Exists)
                    {
                        info.Create();
                    }
                }
            }
            catch (Exception exception)
            {
                flag = false;
                SystemError.CreateErrorLog("创建目录" + strUrlPath + "错误： " + exception.ToString());
            }
            return flag;
        }

        private string GetStyleContent(string strPicStyle, string strPageDepth, ref string strJSType, ref string strLinkInsert, ref string strHasJQuery, ref string strHasiepngfix, string strTime, string strEffect, int nNum)
        {
            string str = "";
            string pagePre = this.GetPagePre(strPageDepth);
            string str4 = strPicStyle;
            if (str4 == null)
            {
                return str;
            }
            if (!(str4 == "1"))
            {
                if (str4 != "2")
                {
                    string str3;
                    if (str4 == "3")
                    {
                        strHasJQuery = "1";
                        strHasiepngfix = "1";
                        strLinkInsert = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + pagePre + "Ajax/BannerModule/Style3/dcss.css\"  />\n<script type='text/javascript' src='" + pagePre + "Ajax/BannerModule/Style3/jq.soChange.js'></script>";
                        str3 = (strEffect.IndexOfAny(new char[] { '0', '1' }) >= 0) ? "fade" : "slide";
                        return ("<div class=\"changeBox_a1\" id=\"change_2\"> \n  [QH:loop ]<a href=\"[QH:PicLinkPath]\" title=\"[QH:Title]\" target=\"_blank\" class=\"a_bigImg\"><img src=\"[QH:PicPath]\" alt=\"\"  title=\"\" width=\"[QH:picWidth]px\" height=\"[QH:picHeight]px\"  /> </a>\n[/QH:loop]<a href=\"#\" class=\"a_last\" title=\"上一个\">上一个</a><a href=\"#\" class=\"a_next\" title=\"下一个\">下一个</a></div><script type=\"text/javascript\"> \n$(function () {\n$('#change_2 .a_bigImg').soChange({\n\tbotPrev:'#change_2 .a_last',//按钮，上一个\n\tbotNext:'#change_2 .a_next',//按钮，下一个\n\tchangeType:'" + str3 + "',//定义对象切换方式为slide\n\tchangeTime:" + strTime + "000//自定义切换时间为4000ms\n});\n});\n</script>");
                    }
                    if (str4 == "4")
                    {
                        strHasJQuery = "1";
                        strHasiepngfix = "0";
                        strLinkInsert = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + pagePre + "Ajax/BannerModule/Style4/cn.css\"  />\n";
                        str = "<div id=\"cdqhslide-index\" style=\"width: 1349px; \">\n\t<div class=\"slides\" style=\"width: 5396px; margin-left: 0px; \">\n\t[QH:loop ]\n\t<div class=\"slide autoMaxWidth\" style=\"width: 1349px; \">\n\t\t\t<div class=\"image\" id=\"bi_[QH:Number]\"><a href=\"[QH:PicLinkPath]\" target=\"_blank\" title=\"[QH:Title]\" ><img name=\"banner2\" src=\"[QH:PicPath]\" alt=\"[QH:Title]\" style=\"left: -1px; position: relative; \"></a></div>\n\t\t\t<div class=\"text\" id=\"bt_[QH:Number]\" style=\"left: 204.5px; top: 46px; \"></div>\n\t\t\t<div class=\"button\" id=\"bb_[QH:Number]\" style=\"left: 207.5px; top: 265px; width: 138px; display: block; \"></div>\n\t\t</div>\n    [/QH:loop]\n\t </div>\n\t<div class=\"control\">\n\t\t<a href=\"\" class=\"active\"></a>\n";
                        for (int i = 1; i < nNum; i++)
                        {
                            str = str + "\t\t<a href=\"\" class=\"\"></a>\n";
                        }
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, "\t</div>\n</div>\n<script type=\"text/javascript\">\n$(function(){\n\n\tindexSlides.ini();\n\t\n\tfor(var i=1;i<=", nNum, ";i++){\n\t//var bImgSrc=$('#bi_'+i).find(\"img\").attr(\"src\");\n\t//var bTextSrc=$('#bt_'+i).find(\"img\").attr(\"src\");\n\t//var bButtonSrc=$('#bb_'+i).find(\"img\").attr(\"src\");\n\t//alert(imgSrc);\n\t$('#bt_'+i).find(\"img\").addClass(\"IE6png\");\n\t$('#bb_'+i).find(\"img\").addClass(\"IE6png\");\n\t//$(\"img[src$='.png']\").addClass(\"IE6png\");\n\t}\n\t\n});\n\nvar vBnrHeight=[QH:picHeight];\nvar vBnrTime=", strTime, "000;\nvar indexSlides={};\n\nindexSlides.timer=false;\nindexSlides.total=$('#cdqhslide-index .control a').length;\nindexSlides.current=-1;\nindexSlides.offScreenLeft=2000;\nindexSlides.leaveScreenLeft=4000;\nindexSlides.animating=false;\n\nindexSlides.obj=$('#cdqhslide-index .slide');\n\nindexSlides.style=[];\nindexSlides.style[0]={\n\ttext:{left:'30px',top:'46px'},\n\tbutton:{left:'33px',top:'265px'},\n\tdirection:'lr'\n};\n" });
                        for (int j = 1; j < nNum; j++)
                        {
                            object obj3 = str;
                            str = string.Concat(new object[] { obj3, "indexSlides.style[", j, "]={\n\ttext:{left:'30px',top:'81px'},\n\tbutton:{left:'30px',top:'244px'},\n\tdirection:'lr'\n};\n" });
                        }
                        return (str + "</script>\n<script type='text/javascript' src='" + pagePre + "Ajax/BannerModule/Style4/cn.index.js'></script>");
                    }
                    if (str4 != "5")
                    {
                        return str;
                    }
                    strHasJQuery = "1";
                    strHasiepngfix = "0";
                    strLinkInsert = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + pagePre + "Ajax/BannerModule/Style5/focus.css\"  />\n<script type='text/javascript' src='" + pagePre + "Ajax/BannerModule/Style5/jq.foucs.js'></script>";
                    str3 = (strEffect.IndexOfAny(new char[] { '0', '1' }) >= 0) ? "right" : "left";
                    return ("    <div id=\"cdqhbnrmain\">\n        <div id=\"index_b_hero\">\n            <div class=\"hero-wrap\">\n  <ul class=\"heros clearfix\">\n[QH:loop ]\n                    <li class=\"hero\">\n                        <a href=\"[QH:PicLinkPath]\" target=\"_blank\" title=\"[QH:Title]\">\n                            <img src=\"[QH:PicPath]\" class=\"thumb\" alt=\"[QH:AltText]\" />\n                        </a>\n                    </li>\n[/QH:loop]\n                </ul>\n            </div>\n<div class=\"helper\">\n                <div class=\"mask-left\">\n                </div>\n                <div class=\"mask-right\">\n                </div>\n                <a href=\"#\" class=\"prev icon-arrow-a-left\"></a>\n                <a href=\"#\" class=\"next icon-arrow-a-right\"></a>\n            </div>\n        </div>\n    </div>\n    <script type=\"text/javascript\">\n        $.foucs({ direction: 'right',height:[QH:picHeight],interval:" + strTime + "000 });\n    </script>\n");
                }
            }
            else
            {
                return ("<script language='javascript'>\nlinkarr = new Array();\npicarr = new Array();\ntextarr = new Array();\nvar swf_width=[QH:picWidth];\nvar swf_height=[QH:picHeight];\n//文字颜色|文字位置|文字背景颜色|文字背景透明度|按键文字颜色|按键默认颜色|按键当前颜色|自动播放时间|图片过渡效果|是否显示按钮|打开方式\nvar configtg='0xffffff|'+swf_height+'|0x3FA61F|0|0xffffff|0xC5DDBC|0x000033|" + strTime + "|" + strEffect + "|1|_blank';\nvar files = \"\";\nvar links = \"\";\nvar texts = \"\";\n//这里设置调用标记\n[QH:loop ]linkarr[[QH:Number]] = \"[QH:PicLinkPath]\";\npicarr[[QH:Number]]  = \"[QH:PicPath]\";\n//textarr[[QH:Number]] = \"[QH:Title]\";\n[/QH:loop]for(i=0;i<picarr.length;i++){\nif(files==\"\") files = picarr[i];\nelse files += \"|\"+picarr[i];\n}\nfor(i=0;i<linkarr.length;i++){\nif(links==\"\") links = linkarr[i];\nelse links += \"|\"+linkarr[i];\n}\nfor(i=0;i<textarr.length;i++){\nif(texts==\"\") texts = textarr[i];\nelse texts += \"|\"+textarr[i];\n}\ndocument.write('<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" width=\"'+ swf_width +'\" height=\"'+ swf_height +'\">');\ndocument.write('<param name=\"movie\" value=\"[QH:SitePath]Ajax/BannerModule/Style1/bcastr3.swf\"><param name=\"quality\" value=\"high\">');\ndocument.write('<param name=\"menu\" value=\"false\"><param name=wmode value=\"opaque\">');\ndocument.write('<param name=\"FlashVars\" value=\"bcastr_file='+files+'&bcastr_link='+links+'&bcastr_title='+texts+'&bcastr_config='+configtg+'\">');\ndocument.write('<embed src=\"[QH:SitePath]Ajax/BannerModule/Style1/bcastr3.swf\" wmode=\"opaque\" FlashVars=\"bcastr_file='+files+'&bcastr_link='+links+'&bcastr_title='+texts+'&bcastr_config='+configtg+'&menu=\"false\" quality=\"high\" width=\"'+ swf_width +'\" height=\"'+ swf_height +'\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />'); document.write('</object>');\n</script>");
            }
            strHasJQuery = "1";
            strLinkInsert = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + pagePre + "Ajax/BannerModule/Style2/featured_slider.css\"  />\n<script type='text/javascript' src='" + pagePre + "Ajax/BannerModule/Style2/jq.nivo.slider.js'></script>";
            string[] strArray = new string[] { "random", "fold", "fade", "sliceDown", "sliceUp", "boxRandom", "boxRain", "boxRainReverse", "boxRainGrow", "boxRainGrowReverse", "sliceDownRight", "sliceDownLeft", "sliceUpRight", "sliceUpLeft", "sliceUpDown", "sliceUpDownLeft" };
            int index = int.Parse(strEffect);
            index = (index > strArray.Length) ? (strArray.Length - 1) : index;
            return ("<script type=\"text/javascript\"> \n$(window).load(function() {\n    $('#slider').nivoSlider({\n\t\tcontrolNav:false,\n        effect:'" + strArray[index] + "', //Specify sets like: 'random,fold,fade,sliceDown'\n        animSpeed:500, //Slide transition speed\n\t\tcaptionOpacity:0.9,\n        directionNav:true, //Next &amp; Prev\n\t\tcontrolNav:true, // 1,2,3... navigation\n\t\tpauseTime: " + strTime + "000, // How long each slide will show\n\t\tdirectionNavHide: true,\n        pauseOnHover:true //Stop animation while hovering\n    });\n});\n</script> \n\n<div class=\"featured_slider\"> \n<div id=\"featured_slider_bg\" class=\"featured_slider_pattern\"> \n<div id=\"slider-wrapper\"> \n  <div id=\"slider\" class=\"nivoSlider\"> \n  [QH:loop ]<a href=\"[QH:PicLinkPath]\" title=\"[QH:Title]\" target=\"_blank\"><img src=\"[QH:PicPath]\" alt=\"\"  title=\"\" width=\"[QH:picWidth]px\" height=\"[QH:picHeight]px\"  /> </a>\n[/QH:loop]  </div> \n     <div class=\"slider_border\"></div> \n</div> \n</div> \n</div>\n");
        }

        private string GetStyleContentMobile(string strPicStyle, string strPageDepth, ref string strLinkInsert, ref string strHasJQuery, ref string strHasiepngfix, int nNum)
        {
            string str3;
            string str = "";
            string str2 = "../" + this.GetPagePre(strPageDepth);
            if (((str3 = strPicStyle) != null) && (str3 == "1"))
            {
                strHasJQuery = "1";
                strLinkInsert = "<script type='text/javascript' src='" + str2 + "Ajax/BannerModule/Mobile/pagescroll.js'></script>\n<script type='text/javascript' src='" + str2 + "Ajax/BannerModule/Mobile/jquery.unveil.min.js'></script>\n<script type='text/javascript' src='" + str2 + "Ajax/BannerModule/Mobile/WapCircleImg.js'></script>\n";
                str = "<style type=\"text/css\">\n.roll_img_cdqh_01 { margin: 0px auto; width: [QH:picWidth]; max-width:640px; height:[QH:picHeight]px; overflow: hidden; position: relative; }\n.roll_img_cdqh_01 .img_box { width: [QH:picWidth]; text-align: center; overflow: hidden; }\n.roll_img_cdqh_01 .img_box img { max-width: [QH:picWidth]; height:[QH:picHeight]px; }\n</style>\n<div class=\"roll_img_cdqh_01\" id=\"cdqh_banner1\">\n<div class=\"img_box\">\n<ul>[QH:loop]\n<li><A href=\"[QH:PicLinkPath]\"><IMG src=\"" + str2 + "Ajax/BannerModule/Mobile/loading.jpg\" \n  data-src=\"[QH:PicPath]\"></A></li>[/QH:loop]\n</ul></div></div>\n<SCRIPT type=\"text/javascript\">\n    var cdqh_banner1 = document.getElementById(\"cdqh_banner1\");\n    cdqh_banner1.main_pic_scroll = new TouchSlider({ id: \"cdqh_banner1\", 'auto': 0, fx: 'ease-out', direction: 'left', speed: 600, timeout: 5000 });\n</SCRIPT>\n";
            }
            return str;
        }

        private string GetSubClassID(string strID, string strdepth, string strListContent)
        {
            StringBuilder builder;
            if (strListContent != "2")
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
            if (strListContent != "2")
            {
                return builder.ToString();
            }
            if (builder.Length > 1)
            {
                return builder.ToString().Substring(1);
            }
            return "'0'";
        }

        private string GetTableName(string Module)
        {
            switch (Module)
            {
                case "2":
                    return "QH_News";

                case "3":
                    return "QH_Product";

                case "4":
                    return "QH_Download";

                case "5":
                    return "QH_Img";

                case "8":
                    return "QH_ZhaoPin";

                case "12":
                    return "QH_Column";

                case "13":
                    return "QH_BannerImg";

                case "14":
                    return "QH_BannerImg";

                case "15":
                    return "QH_FriendLinks";

                case "17":
                    return "QH_ProductBrand";
            }
            return "QH_News";
        }

        public string GetTemplateContent(string strAbsPath1)
        {
            string str = null;
            Encoding encoding = Encoding.UTF8;
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(strAbsPath1, encoding);
                str = reader.ReadToEnd();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("读模版错误： " + exception.ToString());
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

        private string GetTitleLink(DataRow dRow, string strPageDepth, string strUrlPath, string strpagePre)
        {
            string str = dRow["outLink"].ToString().Trim();
            string str2 = dRow["fileName"].ToString().Trim();
            if (str2 != string.Empty)
            {
                str2 = (str2.IndexOf(".") < 0) ? (str2 + ".html") : str2;
            }
            if (!(str != ""))
            {
                return (strpagePre + strUrlPath + "/" + str2);
            }
            if (str.Contains("http://"))
            {
                return str;
            }
            return this.PageUrlSet(str, strPageDepth);
        }

        private DataTable GetZhaoPinDataTable()
        {
            string str2;
            if (System.Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select top 1 ExpirationOpen from QH_ZhaoPinSet")) == "True")
            {
                str2 = "select *,Format(发布日期,'yyyy-MM-dd') as 发布日期,Format(有效期限,'yyyy-MM-dd') as 有效期限 from QH_ZhaoPin where 有效期限>Date() order by 发布日期 desc";
            }
            else
            {
                str2 = "select *,Format(发布日期,'yyyy-MM-dd') as 发布日期,Format(有效期限,'yyyy-MM-dd') as 有效期限 from QH_ZhaoPin order by 发布日期 desc";
            }
            DataTable dataTable = this.Bll1.GetDataTable(str2);
            if (dataTable == null)
            {
                return null;
            }
            dataTable.Columns.Add(new DataColumn("strEc职位名称"));
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataTable.Rows[i]["strEc职位名称"] = GlobalObject.encodeURIComponent(dataTable.Rows[i]["职位名称"].ToString());
            }
            return dataTable;
        }

        private void InsertAntiLookOrDown(ref string strTemp)
        {
            string str = "<noscript><iframe src=*.html></iframe></noscript><script language=\"Javascript\">document.oncontextmenu=new Function(\"event.returnValue=false\");document.onselectstart=new Function(\"event.returnValue=false\");</script>";
            Match match = new Regex("<body", RegexOptions.IgnoreCase).Match(strTemp);
            strTemp = strTemp.Insert(strTemp.IndexOf('>', match.Index) + 1, str);
        }

        private void InsertBeforeEndBody(ref string strTemp, string strPageDepth, string strInsertContent)
        {
            Match match = new Regex("</body>", RegexOptions.IgnoreCase).Match(strTemp);
            if (match.Length == 7)
            {
                strTemp = strTemp.Insert(match.Index, strInsertContent);
            }
        }

        private void InsertHiddenBottom(ref string strTemp, string strHdnID, string strHdnValue)
        {
            Match match = Regex.Match(strTemp, @"<script[^>]+Ajax\/Statistics.js[\s\S]+?<\/script>", RegexOptions.IgnoreCase);
            if (match.Index >= 0)
            {
                strTemp = strTemp.Insert(match.Index, "<input id=\"" + strHdnID + "\" type=\"hidden\" value =\"" + strHdnValue + "\" />\n");
            }
            else
            {
                Match match2 = new Regex("<body>", RegexOptions.IgnoreCase).Match(strTemp);
                if (match2.Length == 6)
                {
                    strTemp = strTemp.Insert(match2.Index + 6, "\n<input id=\"" + strHdnID + "\" type=\"hidden\" value =\"" + strHdnValue + "\" />\n");
                }
            }
        }

        private void InsertJsBottom(ref string strTemp, string strJsFile)
        {
            Match match = new Regex("</body>", RegexOptions.IgnoreCase).Match(strTemp);
            if (match.Length == 7)
            {
                strTemp = strTemp.Insert(match.Index + 7, "<script type =\"text/javascript\" src=\"" + this.astr1[5] + strJsFile + "\" ></script>");
            }
        }

        private void InsertJsBottom(ref string strTemp, string strPageDepth, string strJsFile)
        {
            string pagePre = this.GetPagePre(strPageDepth);
            Match match = new Regex("</body>", RegexOptions.IgnoreCase).Match(strTemp);
            if (match.Length == 7)
            {
                strTemp = strTemp.Insert(match.Index + 7, "<script type =\"text/javascript\" src=\"" + pagePre + strJsFile + "\" ></script>");
            }
        }

        private void InsertLink(ref string strTemp, string strLinkInsert)
        {
            Match match = new Regex("</head>", RegexOptions.IgnoreCase).Match(strTemp);
            if (match.Length == 7)
            {
                strTemp = strTemp.Insert(match.Index, strLinkInsert);
            }
        }

        private void InsertLink(ref string strTemp, string strLinkInsertDown, string strPos)
        {
            Match match = new Regex(strPos, RegexOptions.IgnoreCase).Match(strTemp);
            if (match.Length == strPos.Length)
            {
                strTemp = strTemp.Insert(match.Index, strLinkInsertDown);
            }
        }

        private bool IsAuthorized(string strUrl)
        {
            string domainName = this.Bll1.GetDomainName(strUrl);
            return (this.Bll1.TestIfIsNotIPaddressOrLocalhost(domainName) && this.CheckAuthorized(this.Bll1.GetToEncryptDomain(domainName)));
        }

        private bool IsIDInArrayList(string strColumnID, ArrayList alistID)
        {
            foreach (string str in alistID)
            {
                if (str == strColumnID)
                {
                    return true;
                }
            }
            return false;
        }

        private string LoopReplaceBigContent(string strLoopContent, DataRow dRow, string strpagePre, string strUrlPre, int nNameNumber, string strLoopContentSamll, string strLoopSmall, string strPageDepth, string strColumnID)
        {
            bool bBigNow = false;
            strUrlPre = (strUrlPre == "") ? "" : (strUrlPre + "/");
            strLoopContent = this.ReplaceProductListSmallTitle(strLoopContent, strLoopContentSamll, strLoopSmall, dRow, strpagePre, strPageDepth, strColumnID, ref bBigNow);
            string newValue = "";
            string str = dRow["outLink"].ToString().Trim();
            if (str != "")
            {
                newValue = str.Contains("http://") ? str : this.PageUrlSet(str, strPageDepth);
            }
            else
            {
                newValue = this.GetIsShowLink(strUrlPre + dRow["folder"], dRow, strPageDepth, strpagePre);
            }
            strLoopContent = strLoopContent.Replace("[QH:LinkPath]", newValue);
            string str3 = dRow["ColumnName"].ToString();
            if ((this.nBigLength != 0) && (str3.Length > this.nBigLength))
            {
                str3 = str3.Substring(0, this.nBigLength) + "...";
            }
            strLoopContent = strLoopContent.Replace("[QH:Title]", str3);
            string str4 = dRow["id"].ToString();
            if (this.LoopAttribute.Condition == "ID")
            {
                strLoopContent = strLoopContent.Replace("[QH:Number]", str4);
            }
            else
            {
                strLoopContent = strLoopContent.Replace("[QH:Number]", (nNameNumber + int.Parse(this.LoopAttribute.Condition)).ToString());
            }
            strLoopContent = strLoopContent.Replace("[QH:PicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
            if (strColumnID == str4)
            {
                strLoopContent = strLoopContent.Replace("[QH:JSNow]", "cdqhNow1").Replace("[QH:NowClass]", this.LoopAttribute.JStype);
                bBigNow = true;
            }
            else
            {
                strLoopContent = strLoopContent.Replace("[QH:JSNow]", "").Replace("[QH:NowClass]", "");
            }
            if (bBigNow)
            {
                this.strJQueryNum = str4;
                bBigNow = false;
            }
            return strLoopContent;
        }

        private string LoopReplaceBigContent(string strLoopContent, DataRow dRow, string strpagePre, string strUrlPre, int nNameNumber, string strLoopContentMedium, string strLoopMedium, string strLoopContentSmall, string strLoopSmall, string strPageDepth, string strColumnID)
        {
            bool bBigNow = false;
            strUrlPre = (strUrlPre == "") ? "" : (strUrlPre + "/");
            strLoopContent = strLoopContent.Replace("[QH:LinkPath]", this.GetIsShowLink(strUrlPre + dRow["folder"], dRow, strPageDepth, strpagePre));
            string newValue = dRow["ColumnName"].ToString();
            if ((this.nBigLength != 0) && (newValue.Length > this.nBigLength))
            {
                newValue = newValue.Substring(0, this.nBigLength) + "...";
            }
            strLoopContent = strLoopContent.Replace("[QH:Title]", newValue);
            string str2 = dRow["id"].ToString();
            if (this.LoopAttribute.Condition == "ID")
            {
                strLoopContent = strLoopContent.Replace("[QH:Number]", str2);
            }
            else
            {
                strLoopContent = strLoopContent.Replace("[QH:Number]", (nNameNumber + int.Parse(this.LoopAttribute.Condition)).ToString());
            }
            strLoopContent = strLoopContent.Replace("[QH:PicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
            strLoopContent = strLoopContent.Replace("[QH:FenLanPic]", this.PageUrlSet(dRow["C_img"].ToString(), strPageDepth));
            strLoopContent = strLoopContent.Replace("[QH:AltText]", dRow["ColumnName"].ToString());
            strLoopContent = strLoopContent.Replace("[QH:BriefIntro]", this.PageContentUrlSet(dRow["BriefIntro"].ToString(), strPageDepth));
            if (strLoopContent.Contains("[QH:BriefIntro"))
            {
                this.ReplaceContentsBrief(ref strLoopContent, dRow["BriefIntro"].ToString(), "BriefIntro");
            }
            strLoopContent = strLoopContent.Replace("[QH:FenLanEnglish]", dRow["C_EnTitle"].ToString());
            strLoopContent = strLoopContent.Replace("[QH:Contents]", this.FilterHotLink(this.PageContentUrlSet(dRow["content"].ToString(), strPageDepth), strPageDepth));
            if (strLoopContent.Contains("[QH:ContentsBrief"))
            {
                this.ReplaceContentsBrief(ref strLoopContent, dRow["Content"].ToString());
            }
            if (strColumnID == str2)
            {
                strLoopContent = strLoopContent.Replace("[QH:JSNow]", "cdqhNow1").Replace("[QH:NowClass]", this.LoopAttribute.JStype);
                bBigNow = true;
            }
            else
            {
                strLoopContent = strLoopContent.Replace("[QH:JSNow]", "").Replace("[QH:NowClass]", "");
            }
            if (strLoopMedium == "")
            {
                return strLoopContent;
            }
            StringBuilder builder = new StringBuilder("");
            DataRow[] rowArray = this.dTableClmn.Select("ParentID='" + str2 + "'");
            if (rowArray.Length == 0)
            {
                if (bBigNow)
                {
                    this.strJQueryNum = str2;
                    bBigNow = false;
                }
                return Regex.Replace(strLoopContent.Replace(strLoopMedium, ""), @"<ul>[\s\n\r]*</ul>", "", RegexOptions.IgnoreCase);
            }
            strUrlPre = this.ColumnUrlPre(rowArray[0], this.dTableClmn);
            for (int i = 0; i < rowArray.Length; i++)
            {
                builder.Append(this.LoopReplaceMediumContent(strLoopContentMedium, rowArray[i], strpagePre, strUrlPre, i, strLoopContentSmall, strLoopSmall, strPageDepth, strColumnID, ref bBigNow));
            }
            if (bBigNow)
            {
                this.strJQueryNum = str2;
                bBigNow = false;
            }
            return strLoopContent.Replace(strLoopMedium, builder.ToString());
        }

        private string LoopReplaceContent(string strLoopContent, string[] astrTImg, string strPageDepth)
        {
            strLoopContent = strLoopContent.Replace("[QH:Title]", astrTImg[0]);
            strLoopContent = strLoopContent.Replace("[QH:PicPath]", this.PageUrlSet(astrTImg[1], strPageDepth));
            strLoopContent = strLoopContent.Replace("[QH:AltText]", astrTImg[0]);
            strLoopContent = strLoopContent.Replace("[QH:Number]", astrTImg[3]);
            return strLoopContent;
        }

        private string LoopReplaceContent(string strLoopContent, string[] astrTImg, string strPageDepth, int nNum)
        {
            strLoopContent = strLoopContent.Replace("[QH:Title]", astrTImg[0]);
            strLoopContent = strLoopContent.Replace("[QH:PicPath]", this.PageUrlSet(astrTImg[1], strPageDepth));
            strLoopContent = strLoopContent.Replace("[QH:Number]", nNum.ToString());
            return strLoopContent;
        }

        private string LoopReplaceContent(string strLoopContent, DataRow dRow, string TableName, string strPageDepth, int nNameNumber)
        {
            string str2;
            string str3;
            string str5;
            string pagePre = this.GetPagePre(strPageDepth);
            switch (TableName)
            {
                case "QH_News":
                    strLoopContent = strLoopContent.Replace("[QH:Title]", dRow["Title"].ToString());
                    strLoopContent = strLoopContent.Replace("[QH:AltText]", dRow["titleAll"].ToString());
                    str2 = dRow["LinkUrl"].ToString().Trim();
                    str3 = "";
                    if (!(str2 == ""))
                    {
                        str3 = this.PageUrlSet(str2, strPageDepth);
                        break;
                    }
                    str3 = this.PageUrlSet("NewsDetails/NewsDetails_" + dRow["id"].ToString() + ".html", strPageDepth);
                    break;

                case "QH_Product":
                    {
                        this.RepalceLoopCusField(ref strLoopContent, dRow["id"].ToString(), strPageDepth);
                        strLoopContent = strLoopContent.Replace("[QH:Title]", dRow["Title"].ToString());
                        strLoopContent = strLoopContent.Replace("[QH:AltText]", dRow["titleAll"].ToString());
                        str2 = dRow["LinkUrl"].ToString().Trim();
                        str3 = "";
                        if (!(str2 == ""))
                        {
                            str3 = this.PageUrlSet(str2, strPageDepth);
                        }
                        else
                        {
                            str3 = this.PageUrlSet("ProductDetails/ProductDetails_" + dRow["id"].ToString() + ".html", strPageDepth);
                        }
                        if (this.bIsMobile)
                        {
                            str3 = str3.Substring(3);
                        }
                        strLoopContent = strLoopContent.Replace("[QH:NewsPath]", str3);
                        strLoopContent = strLoopContent.Replace("[QH:PicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
                        strLoopContent = strLoopContent.Replace("[QH:ThumbPicPath]", this.PageUrlSet(dRow["ThumbUrl"].ToString(), strPageDepth));
                        if (strLoopContent.Contains("[QH:ContentsBrief"))
                        {
                            this.ReplaceContentsBrief(ref strLoopContent, dRow["Content"].ToString());
                        }
                        str5 = dRow["hits"].ToString().Trim();
                        str5 = (str5 == "") ? "0" : str5;
                        strLoopContent = strLoopContent.Replace("[QH:Hits]", "<span id=\"Hits_" + dRow["id"].ToString() + "\" >" + str5 + "</span>");
                        if (dRow["adddate"].ToString().Trim() != "")
                        {
                            strLoopContent = strLoopContent.Replace("[QH:Date]", ((DateTime)dRow["adddate"]).ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            strLoopContent = strLoopContent.Replace("[QH:Date]", "");
                        }
                        if (dRow["ModyDate"].ToString().Trim() != "")
                        {
                            strLoopContent = strLoopContent.Replace("[QH:MDate]", ((DateTime)dRow["ModyDate"]).ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            strLoopContent = strLoopContent.Replace("[QH:MDate]", "");
                        }
                        strLoopContent = strLoopContent.Replace("[QH:ProductMemo]", dRow["Memo1"].ToString());
                        strLoopContent = strLoopContent.Replace("[QH:BriefIntro]", this.PageContentUrlSet(dRow["Memo1"].ToString(), strPageDepth));
                        if (strLoopContent.Contains("[QH:BriefIntro"))
                        {
                            this.ReplaceContentsBrief(ref strLoopContent, dRow["Memo1"].ToString(), "BriefIntro");
                        }
                        strLoopContent = strLoopContent.Replace("[QH:ProductTags]", this.SetTagsLink(dRow["Key"].ToString(), "ProductTags.html", pagePre));
                        strLoopContent = strLoopContent.Replace("[QH:ProductBrand]", this.SetTagsLink(dRow["Brand"].ToString(), "ProductBrand.html", pagePre));
                        strLoopContent = strLoopContent.Replace("[QH:Author]", dRow["Author"].ToString());
                        strLoopContent = strLoopContent.Replace("[QH:ProductPrice]", dRow["Price"].ToString());
                        strLoopContent = strLoopContent.Replace("[QH:ProductSpecName]", dRow["SpecialName"].ToString());
                        strLoopContent = strLoopContent.Replace("[QH:ProductID]", dRow["Product_id"].ToString());
                        strLoopContent = strLoopContent.Replace("[QH:ID]", dRow["id"].ToString());
                        string str6 = "";
                        if (dRow["Elite"].ToString() == "True")
                        {
                            str6 = " <span><img src='" + pagePre + "Ajax/NewHotImg/reb.gif' /></span>";
                        }
                        if (dRow["Newproduct"].ToString() == "True")
                        {
                            str6 = str6 + " <span><img src='" + pagePre + "Ajax/NewHotImg/newb.gif' /></span>";
                        }
                        if (str6 == "")
                        {
                            str6 = "<span><img src='" + pagePre + "Ajax/NewHotImg/emp.gif' /></span>";
                        }
                        strLoopContent = strLoopContent.Replace("[QH:ProductState]", str6);
                        strLoopContent = strLoopContent.Replace("[QH:ProductDownloadPath]", this.PageUrlSet("Ajax/ProductDownload/ProductDownload.aspx?id=" + dRow["id"].ToString(), strPageDepth));
                        strLoopContent = strLoopContent.Replace("[QH:AddShoppingCart]", string.Concat(new object[] { "location='", pagePre, "MemberManage/Shopping_Cart.aspx?id=", dRow["id"], "'" }));
                        return strLoopContent;
                    }
                case "QH_Download":
                    this.RepalceLoopCusField(ref strLoopContent, dRow["id"].ToString(), strPageDepth);
                    strLoopContent = strLoopContent.Replace("[QH:Title]", dRow["Title"].ToString());
                    strLoopContent = strLoopContent.Replace("[QH:AltText]", dRow["titleAll"].ToString());
                    str3 = this.PageUrlSet("DownloadDetails/DownloadDetails_" + dRow["id"].ToString() + ".html", strPageDepth);
                    if (this.bIsMobile)
                    {
                        str3 = str3.Substring(3);
                    }
                    strLoopContent = strLoopContent.Replace("[QH:NewsPath]", str3);
                    strLoopContent = strLoopContent.Replace("[QH:LinkPath]", this.PageUrlSet("Ajax/DownloadFile.aspx?id=" + dRow["id"].ToString(), strPageDepth));
                    strLoopContent = strLoopContent.Replace("[QH:NewsID]", dRow["id"].ToString());
                    strLoopContent = strLoopContent.Replace("[QH:FileSize]", dRow["FileSize"].ToString());
                    if (strLoopContent.Contains("[QH:ContentsBrief"))
                    {
                        this.ReplaceContentsBrief(ref strLoopContent, dRow["Content"].ToString());
                    }
                    strLoopContent = strLoopContent.Replace("[QH:BriefIntro]", this.PageContentUrlSet(dRow["BriefIntro"].ToString(), strPageDepth));
                    if (strLoopContent.Contains("[QH:BriefIntro"))
                    {
                        this.ReplaceContentsBrief(ref strLoopContent, dRow["BriefIntro"].ToString(), "BriefIntro");
                    }
                    strLoopContent = strLoopContent.Replace("[QH:Contents]", dRow["Content"].ToString().Replace("<p>", "").Replace("</p>", ""));
                    str5 = dRow["hits"].ToString().Trim();
                    str5 = (str5 == "") ? "0" : str5;
                    strLoopContent = strLoopContent.Replace("[QH:Hits]", "<span id=\"Hits_" + dRow["id"].ToString() + "\" >" + str5 + "</span>");
                    if (dRow["adddate"].ToString().Trim() != "")
                    {
                        strLoopContent = strLoopContent.Replace("[QH:Date]", ((DateTime)dRow["adddate"]).ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        strLoopContent = strLoopContent.Replace("[QH:Date]", "");
                    }
                    if (dRow["ModyDate"].ToString().Trim() != "")
                    {
                        strLoopContent = strLoopContent.Replace("[QH:MDate]", ((DateTime)dRow["ModyDate"]).ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        strLoopContent = strLoopContent.Replace("[QH:MDate]", "");
                    }
                    strLoopContent = strLoopContent.Replace("[QH:Author]", dRow["Author"].ToString());
                    return strLoopContent;

                case "QH_Img":
                    this.RepalceLoopCusField(ref strLoopContent, dRow["id"].ToString(), strPageDepth);
                    strLoopContent = strLoopContent.Replace("[QH:Title]", dRow["Title"].ToString());
                    strLoopContent = strLoopContent.Replace("[QH:AltText]", dRow["titleAll"].ToString());
                    str3 = this.PageUrlSet("PictureDetails/PictureDetails_" + dRow["id"].ToString() + ".html", strPageDepth);
                    if (this.bIsMobile)
                    {
                        str3 = str3.Substring(3);
                    }
                    strLoopContent = strLoopContent.Replace("[QH:NewsPath]", str3);
                    strLoopContent = strLoopContent.Replace("[QH:PicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
                    strLoopContent = strLoopContent.Replace("[QH:ThumbPicPath]", this.PageUrlSet(dRow["ThumbUrl"].ToString(), strPageDepth));
                    strLoopContent = strLoopContent.Replace("[QH:AltText]", dRow["Title"].ToString());
                    strLoopContent = strLoopContent.Replace("[QH:Author]", dRow["Author"].ToString());
                    if (strLoopContent.Contains("[QH:ContentsBrief"))
                    {
                        this.ReplaceContentsBrief(ref strLoopContent, dRow["Content"].ToString());
                    }
                    strLoopContent = strLoopContent.Replace("[QH:BriefIntro]", this.PageContentUrlSet(dRow["BriefIntro"].ToString(), strPageDepth));
                    if (strLoopContent.Contains("[QH:BriefIntro"))
                    {
                        this.ReplaceContentsBrief(ref strLoopContent, dRow["BriefIntro"].ToString(), "BriefIntro");
                    }
                    str5 = dRow["hits"].ToString().Trim();
                    str5 = (str5 == "") ? "0" : str5;
                    strLoopContent = strLoopContent.Replace("[QH:Hits]", "<span id=\"Hits_" + dRow["id"].ToString() + "\" >" + str5 + "</span>");
                    if (dRow["adddate"].ToString().Trim() != "")
                    {
                        strLoopContent = strLoopContent.Replace("[QH:Date]", ((DateTime)dRow["adddate"]).ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        strLoopContent = strLoopContent.Replace("[QH:Date]", "");
                    }
                    if (dRow["ModyDate"].ToString().Trim() != "")
                    {
                        strLoopContent = strLoopContent.Replace("[QH:MDate]", ((DateTime)dRow["ModyDate"]).ToString("yyyy-MM-dd"));
                        return strLoopContent;
                    }
                    strLoopContent = strLoopContent.Replace("[QH:MDate]", "");
                    return strLoopContent;

                case "QH_BannerImg":
                    strLoopContent = strLoopContent.Replace("[QH:Title]", dRow["Title"].ToString());
                    strLoopContent = strLoopContent.Replace("[QH:AltText]", dRow["Title"].ToString());
                    strLoopContent = strLoopContent.Replace("[QH:PicPath]", this.PageUrlSet(dRow["ImgUrl"].ToString(), strPageDepth));
                    strLoopContent = strLoopContent.Replace("[QH:PicLinkPath]", this.PageUrlSet(dRow["ImgLink"].ToString(), strPageDepth));
                    strLoopContent = strLoopContent.Replace("[QH:Number]", nNameNumber.ToString());
                    return strLoopContent;

                case "QH_FriendLinks":
                    if (!(this.astr1[0x18] == "False"))
                    {
                        strLoopContent = strLoopContent.Replace("[QH:Title]", dRow["SiteName"].ToString());
                        strLoopContent = strLoopContent.Replace("[QH:PicPath]", this.PageUrlSet(dRow["LogoUrl"].ToString(), strPageDepth));
                        if (dRow["SiteUrl"].ToString().Contains("http://"))
                        {
                            strLoopContent = strLoopContent.Replace("[QH:LinkPath]", dRow["SiteUrl"].ToString());
                        }
                        else
                        {
                            strLoopContent = strLoopContent.Replace("[QH:LinkPath]", this.PageUrlSet(dRow["SiteUrl"].ToString(), strPageDepth));
                        }
                        strLoopContent = strLoopContent.Replace("[QH:AltText]", dRow["SiteIntro"].ToString());
                        return strLoopContent;
                    }
                    return "";

                case "QH_ZhaoPin":
                    for (int i = 0; i < (this.astrtags.Length - 1); i++)
                    {
                        strLoopContent = strLoopContent.Replace(this.astrtags[i], dRow[this.astrColumn[i]].ToString());
                    }
                    strLoopContent = strLoopContent.Replace(this.astrtags[this.astrtags.Length - 1], this.PageContentUrlSet(dRow[this.astrColumn[this.astrtags.Length - 1]].ToString(), strPageDepth));
                    return strLoopContent;

                case "QH_ProductBrand":
                    {
                        string str7 = dRow["Brand"].ToString();
                        string str8 = str7;
                        int length = (this.LoopAttribute.TitleNum == "") ? 40 : int.Parse(this.LoopAttribute.TitleNum);
                        if (str7.Length > length)
                        {
                            str7 = str7.Substring(0, length) + this.LoopAttribute.AddStr;
                        }
                        strLoopContent = strLoopContent.Replace("[QH:Title]", str7);
                        strLoopContent = strLoopContent.Replace("[QH:AltText]", str8);
                        strLoopContent = strLoopContent.Replace("[QH:PicPath]", this.PageUrlSet(dRow["Logo"].ToString(), strPageDepth));
                        if (dRow["Link"].ToString().Contains("http://"))
                        {
                            strLoopContent = strLoopContent.Replace("[QH:LinkPath]", dRow["SiteUrl"].ToString());
                        }
                        else
                        {
                            strLoopContent = strLoopContent.Replace("[QH:LinkPath]", this.PageUrlSet(dRow["Link"].ToString(), strPageDepth));
                        }
                        strLoopContent = strLoopContent.Replace("[QH:ProductBrandLink]", pagePre + "TagsAndSearch/ProductBrand.html?S=" + this.CSPage.Server.UrlEncode(dRow["Brand"].ToString()));
                        if (this.LoopAttribute.JStype != "-1")
                        {
                            if (this.LoopAttribute.JStype == "0")
                            {
                                strLoopContent = strLoopContent.Replace(this.LoopAttribute.ProductFilter, dRow["Memo"].ToString());
                                return strLoopContent;
                            }
                            int num3 = int.Parse(this.LoopAttribute.JStype);
                            string str9 = dRow["Memo"].ToString();
                            if (str9.Length > num3)
                            {
                                str9 = str9.Substring(0, num3) + "...";
                            }
                            strLoopContent = strLoopContent.Replace(this.LoopAttribute.ProductFilter, str9);
                        }
                        return strLoopContent;
                    }
                default:
                    return strLoopContent;
            }
            if (this.bIsMobile)
            {
                str3 = str3.Substring(3);
            }
            strLoopContent = strLoopContent.Replace("[QH:NewsPath]", str3);
            strLoopContent = strLoopContent.Replace("[QH:NewsID]", dRow["id"].ToString());
            string newValue = this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth);
            strLoopContent = strLoopContent.Replace("[QH:PicPath]", newValue).Replace("[QH:ThumbPicPath]", newValue);
            if (strLoopContent.Contains("[QH:ContentsBrief"))
            {
                this.ReplaceContentsBrief(ref strLoopContent, dRow["Content"].ToString());
            }
            strLoopContent = strLoopContent.Replace("[QH:BriefIntro]", this.PageContentUrlSet(dRow["BriefIntro"].ToString(), strPageDepth));
            if (strLoopContent.Contains("[QH:BriefIntro"))
            {
                this.ReplaceContentsBrief(ref strLoopContent, dRow["BriefIntro"].ToString(), "BriefIntro");
            }
            str5 = dRow["hits"].ToString().Trim();
            str5 = (str5 == "") ? "0" : str5;
            strLoopContent = strLoopContent.Replace("[QH:Hits]", "<span id=\"Hits_" + dRow["id"].ToString() + "\" >" + str5 + "</span>");
            if (dRow["adddate"].ToString().Trim() != "")
            {
                strLoopContent = strLoopContent.Replace("[QH:Date]", ((DateTime)dRow["adddate"]).ToString("yyyy-MM-dd"));
            }
            else
            {
                strLoopContent = strLoopContent.Replace("[QH:Date]", "");
            }
            if (dRow["ModyDate"].ToString().Trim() != "")
            {
                strLoopContent = strLoopContent.Replace("[QH:MDate]", ((DateTime)dRow["ModyDate"]).ToString("yyyy-MM-dd"));
            }
            else
            {
                strLoopContent = strLoopContent.Replace("[QH:MDate]", "");
            }
            strLoopContent = strLoopContent.Replace("[QH:Author]", dRow["Author"].ToString());
            strLoopContent = strLoopContent.Replace("[QH:NewsTags]", this.SetTagsLink(dRow["Tags"].ToString(), "NewsTags.html", pagePre));
            return strLoopContent;
        }

        private string LoopReplaceFYBigContent(string strLoopContent, DataRow dRow, string strpagePre, string strUrlPre, int nNameNumber, string strLoopContentSamll, string strLoopSmall, string strPageDepth, string strColumnID)
        {
            bool bBigNow = false;
            strUrlPre = (strUrlPre == "") ? "" : (strUrlPre + "/");
            strLoopContent = this.ReplaceFYSmallTitle(strLoopContent, strLoopContentSamll, strLoopSmall, dRow, strpagePre, strPageDepth, strColumnID, ref bBigNow);
            string newValue = "";
            string str = dRow["outLink"].ToString().Trim();
            if (str != "")
            {
                newValue = str.Contains("http://") ? str : this.PageUrlSet(str, strPageDepth);
            }
            else
            {
                newValue = this.GetIsShowLink(strUrlPre + dRow["folder"], dRow, strPageDepth, strpagePre);
            }
            strLoopContent = strLoopContent.Replace("[QH:LinkPath]", newValue);
            string str3 = dRow["ColumnName"].ToString();
            strLoopContent = strLoopContent.Replace("[QH:AltText]", str3);
            if ((this.nBigLength != 0) && (str3.Length > this.nBigLength))
            {
                str3 = str3.Substring(0, this.nBigLength) + "...";
            }
            strLoopContent = strLoopContent.Replace("[QH:FenLanTitle]", str3);
            string str4 = dRow["id"].ToString();
            if (this.LoopAttribute.Condition == "ID")
            {
                strLoopContent = strLoopContent.Replace("[QH:Number]", str4);
            }
            else
            {
                strLoopContent = strLoopContent.Replace("[QH:Number]", (nNameNumber + int.Parse(this.LoopAttribute.Condition)).ToString());
            }
            strLoopContent = strLoopContent.Replace("[QH:PicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
            if (strColumnID == str4)
            {
                strLoopContent = strLoopContent.Replace("[QH:JSNow]", "cdqhFYNow1").Replace("[QH:NowClass]", this.LoopAttribute.JStype);
                bBigNow = true;
            }
            else
            {
                strLoopContent = strLoopContent.Replace("[QH:JSNow]", "").Replace("[QH:NowClass]", "");
            }
            if (bBigNow)
            {
                this.strJQueryNum = str4;
                bBigNow = false;
            }
            return strLoopContent;
        }

        private string LoopReplaceFYSmallContent(string strLoopContentSamll, DataRow dRow, string strpagePre, string strUrlPre, string strPageDepth, int nj, string strColumnID, ref bool bBigNow)
        {
            strUrlPre = (strUrlPre == "") ? "" : (strUrlPre + "/");
            string newValue = "";
            string str = dRow["outLink"].ToString().Trim();
            if (str != "")
            {
                newValue = str.Contains("http://") ? str : this.PageUrlSet(str, strPageDepth);
            }
            else
            {
                newValue = this.GetIsShowLink(strUrlPre + dRow["folder"], dRow, strPageDepth, strpagePre);
            }
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:sLinkPath]", newValue);
            string str3 = dRow["ColumnName"].ToString();
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:sAltText]", str3);
            if ((this.nSmallLength != 0) && (str3.Length > this.nSmallLength))
            {
                str3 = str3.Substring(0, this.nSmallLength) + "...";
            }
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:sFenLanTitle]", str3);
            string str4 = dRow["id"].ToString();
            if (this.LoopAttribute.Role == "ID")
            {
                strLoopContentSamll = strLoopContentSamll.Replace("[QH:sNumber]", str4);
            }
            else
            {
                strLoopContentSamll = strLoopContentSamll.Replace("[QH:sNumber]", (nj + int.Parse(this.LoopAttribute.Role)).ToString());
            }
            if (strColumnID == str4)
            {
                strLoopContentSamll = strLoopContentSamll.Replace("[QH:sJSNow]", "cdqhFYNow2").Replace("[QH:sNowClass]", this.LoopAttribute.ProductFilter);
                bBigNow = true;
                return strLoopContentSamll;
            }
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:sJSNow]", "").Replace("[QH:sNowClass]", "");
            return strLoopContentSamll;
        }

        private string LoopReplaceJS2Banner(string strLoopContent, DataTable dTable, int nloopNum, string strPageDepth)
        {
            string newValue = "";
            string str = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            if (nloopNum > 0)
            {
                newValue = this.PageUrlSet(dTable.Rows[0]["ImgUrl"].ToString(), strPageDepth);
                str = dTable.Rows[0]["ImgLink"].ToString().Trim();
                str = this.PageUrlSet(str, strPageDepth);
                str5 = dTable.Rows[0]["Title"].ToString().Trim();
            }
            for (int i = 1; i < nloopNum; i++)
            {
                str3 = this.PageUrlSet(dTable.Rows[i]["ImgUrl"].ToString(), strPageDepth);
                str4 = dTable.Rows[i]["ImgLink"].ToString().Trim();
                str4 = this.PageUrlSet(str4, strPageDepth);
                str6 = dTable.Rows[i]["Title"].ToString().Trim();
                newValue = newValue + "|" + str3;
                str = str + "|" + str4;
                str5 = str5 + "|" + str6;
            }
            strLoopContent = strLoopContent.Replace("[QH:JS2pic]", newValue);
            strLoopContent = strLoopContent.Replace("[QH:JS2link]", str);
            strLoopContent = strLoopContent.Replace("[QH:JS2text]", str5);
            return strLoopContent;
        }

        private string LoopReplaceJS2Content(string strLoopContent, DataTable dTable, int nloopNum, string strPageDepth, string strMdl)
        {
            string str = "";
            string newValue = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str9 = strMdl;
            if (str9 != null)
            {
                if (!(str9 == "2"))
                {
                    if (str9 == "3")
                    {
                        str7 = "ProductDetails/ProductDetails_";
                    }
                    else if (str9 == "5")
                    {
                        str7 = "PictureDetails/PictureDetails_";
                    }
                }
                else
                {
                    str7 = "NewsDetails/NewsDetails_";
                }
            }
            string str8 = this.PageUrlSet(str7, strPageDepth);
            if (nloopNum > 0)
            {
                str = dTable.Rows[0]["ID"].ToString();
                newValue = this.PageUrlSet(dTable.Rows[0]["ImageUrl"].ToString(), strPageDepth);
                str3 = (strMdl == "5") ? "" : dTable.Rows[0]["LinkUrl"].ToString().Trim();
                if (str3 == "")
                {
                    str3 = str8 + str + ".html";
                }
                else
                {
                    str3 = this.PageUrlSet(str3, strPageDepth);
                }
            }
            for (int i = 1; i < nloopNum; i++)
            {
                str = dTable.Rows[i]["ID"].ToString();
                str4 = this.PageUrlSet(dTable.Rows[i]["ImageUrl"].ToString(), strPageDepth);
                str5 = (strMdl == "5") ? "" : dTable.Rows[i]["LinkUrl"].ToString().Trim();
                if (str5 == "")
                {
                    str5 = str8 + str + ".html";
                }
                else
                {
                    str5 = this.PageUrlSet(str5, strPageDepth);
                }
                newValue = newValue + "|" + str4;
                str3 = str3 + "|" + str5;
                str6 = str6 + "|";
            }
            strLoopContent = strLoopContent.Replace("[QH:JS2pic]", newValue);
            strLoopContent = strLoopContent.Replace("[QH:JS2link]", str3);
            return strLoopContent;
        }

        private string LoopReplaceMediumContent(string strLoopContentMedium, DataRow dRow, string strpagePre, string strUrlPre, int nNameNumber, string strLoopContentSmall, string strLoopSmall, string strPageDepth, string strColumnID, ref bool bBigNow)
        {
            strUrlPre = (strUrlPre == "") ? "" : (strUrlPre + "/");
            strLoopContentMedium = strLoopContentMedium.Replace("[QH:sLinkPath]", this.GetIsShowLink(strUrlPre + dRow["folder"], dRow, strPageDepth, strpagePre));
            string newValue = dRow["ColumnName"].ToString();
            if ((this.nMediumLength != 0) && (newValue.Length > this.nMediumLength))
            {
                newValue = newValue.Substring(0, this.nMediumLength) + "...";
            }
            strLoopContentMedium = strLoopContentMedium.Replace("[QH:sTitle]", newValue);
            string str2 = dRow["id"].ToString();
            if (this.LoopAttribute.Role == "ID")
            {
                strLoopContentMedium = strLoopContentMedium.Replace("[QH:sNumber]", str2);
            }
            else
            {
                strLoopContentMedium = strLoopContentMedium.Replace("[QH:sNumber]", (nNameNumber + int.Parse(this.LoopAttribute.Role)).ToString());
            }
            strLoopContentMedium = strLoopContentMedium.Replace("[QH:sPicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
            strLoopContentMedium = strLoopContentMedium.Replace("[QH:sFenLanPic]", this.PageUrlSet(dRow["C_img"].ToString(), strPageDepth));
            strLoopContentMedium = strLoopContentMedium.Replace("[QH:sAltText]", dRow["ColumnName"].ToString());
            strLoopContentMedium = strLoopContentMedium.Replace("[QH:sBriefIntro]", this.PageContentUrlSet(dRow["BriefIntro"].ToString(), strPageDepth));
            if (strLoopContentMedium.Contains("[QH:sBriefIntro"))
            {
                this.ReplaceContentsBrief(ref strLoopContentMedium, dRow["BriefIntro"].ToString(), "sBriefIntro");
            }
            strLoopContentMedium = strLoopContentMedium.Replace("[QH:sFenLanEnglish]", dRow["C_EnTitle"].ToString());
            strLoopContentMedium = strLoopContentMedium.Replace("[QH:sContents]", this.FilterHotLink(this.PageContentUrlSet(dRow["content"].ToString(), strPageDepth), strPageDepth));
            if (strLoopContentMedium.Contains("[QH:sContentsBrief"))
            {
                this.ReplaceContentsBrief(ref strLoopContentMedium, dRow["Content"].ToString(), "sContentsBrief");
            }
            if (strColumnID == str2)
            {
                strLoopContentMedium = strLoopContentMedium.Replace("[QH:sJSNow]", "cdqhNow2").Replace("[QH:sNowClass]", this.LoopAttribute.ProductFilter);
                bBigNow = true;
            }
            else
            {
                strLoopContentMedium = strLoopContentMedium.Replace("[QH:sJSNow]", "").Replace("[QH:sNowClass]", "");
            }
            if (strLoopSmall == "")
            {
                return strLoopContentMedium;
            }
            StringBuilder builder = new StringBuilder("");
            DataRow[] rowArray = this.dTableClmn.Select("ParentID='" + str2 + "'");
            if (rowArray.Length == 0)
            {
                return Regex.Replace(strLoopContentMedium.Replace(strLoopSmall, ""), @"<ul>[\s\n\r]*</ul>", "", RegexOptions.IgnoreCase);
            }
            strUrlPre = this.ColumnUrlPre(rowArray[0], this.dTableClmn);
            for (int i = 0; i < rowArray.Length; i++)
            {
                builder.Append(this.LoopReplaceSmallContent(strLoopContentSmall, rowArray[i], strpagePre, strUrlPre, i, strPageDepth, strColumnID, ref bBigNow));
            }
            return strLoopContentMedium.Replace(strLoopSmall, builder.ToString());
        }

        private string LoopReplaceNavContent(string strLoopContent, DataRow dRow, DataTable dTableClmn, string strPageDepth, int nNum)
        {
            strLoopContent = strLoopContent.Replace("[QH:Title]", dRow["ColumnName"].ToString());
            strLoopContent = strLoopContent.Replace("[QH:Number]", nNum.ToString());
            this.GetIsShowDataRow(ref dRow);
            string pagePre = this.GetPagePre(strPageDepth);
            string str2 = this.ColumnUrlPre(dRow, dTableClmn);
            str2 = (str2 == "") ? "" : (str2 + "/");
            string newValue = dRow["outLink"].ToString().Trim();
            string str4 = dRow["fileName"].ToString().Trim();
            if (str4 != string.Empty)
            {
                str4 = (str4.IndexOf(".") < 0) ? (str4 + ".html") : str4;
            }
            if (newValue != "")
            {
                if (newValue.Contains("http://"))
                {
                    strLoopContent = strLoopContent.Replace("[QH:LinkPath]", newValue);
                }
                else
                {
                    strLoopContent = strLoopContent.Replace("[QH:LinkPath]", this.PageUrlSet(newValue, strPageDepth));
                }
            }
            else
            {
                strLoopContent = strLoopContent.Replace("[QH:LinkPath]", string.Concat(new object[] { pagePre, str2, dRow["folder"], "/", str4 }));
            }
            if (dRow["NewWin"].ToString() == "1")
            {
                int index = strLoopContent.IndexOf("href");
                index = (index < 0) ? strLoopContent.IndexOf("HREF") : index;
                index = (index < 0) ? strLoopContent.IndexOf("Href") : index;
                if (index >= 0)
                {
                    strLoopContent = strLoopContent.Insert(index, "target=_blank ");
                }
            }
            return strLoopContent;
        }

        private string LoopReplaceSmallContent(string strLoopContentSamll, DataRow dRow, string strpagePre, string strUrlPre, int nNameNumber, string strPageDepth, string strColumnID, ref bool bBigNow)
        {
            strUrlPre = (strUrlPre == "") ? "" : (strUrlPre + "/");
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssLinkPath]", this.GetIsShowLink(strUrlPre + dRow["folder"], dRow, strPageDepth, strpagePre));
            string newValue = dRow["ColumnName"].ToString();
            if ((this.nSmallLength != 0) && (newValue.Length > this.nSmallLength))
            {
                newValue = newValue.Substring(0, this.nSmallLength) + "...";
            }
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssTitle]", newValue);
            string str2 = dRow["id"].ToString();
            if (this.LoopAttribute.KeyWord == "ID")
            {
                strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssNumber]", str2);
            }
            else
            {
                strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssNumber]", (nNameNumber + int.Parse(this.LoopAttribute.KeyWord)).ToString());
            }
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssPicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssFenLanPic]", this.PageUrlSet(dRow["C_img"].ToString(), strPageDepth));
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssAltText]", dRow["ColumnName"].ToString());
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssBriefIntro]", this.PageContentUrlSet(dRow["BriefIntro"].ToString(), strPageDepth));
            if (strLoopContentSamll.Contains("[QH:ssBriefIntro"))
            {
                this.ReplaceContentsBrief(ref strLoopContentSamll, dRow["BriefIntro"].ToString(), "ssBriefIntro");
            }
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssFenLanEnglish]", dRow["C_EnTitle"].ToString());
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssContents]", this.FilterHotLink(this.PageContentUrlSet(dRow["content"].ToString(), strPageDepth), strPageDepth));
            if (strLoopContentSamll.Contains("[QH:ssContentsBrief"))
            {
                this.ReplaceContentsBrief(ref strLoopContentSamll, dRow["Content"].ToString(), "ssContentsBrief");
            }
            if (strColumnID == str2)
            {
                strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssJSNow]", "cdqhNow3").Replace("[QH:ssNowClass]", this.LoopAttribute.Sort);
                bBigNow = true;
                return strLoopContentSamll;
            }
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssJSNow]", "").Replace("[QH:ssNowClass]", "");
            return strLoopContentSamll;
        }

        private string LoopReplaceSmallContent(string strLoopContentSamll, DataRow dRow, string strpagePre, string strUrlPre, string strPageDepth, int nj, string strColumnID, ref bool bBigNow)
        {
            strUrlPre = (strUrlPre == "") ? "" : (strUrlPre + "/");
            string newValue = "";
            string str = dRow["outLink"].ToString().Trim();
            if (str != "")
            {
                newValue = str.Contains("http://") ? str : this.PageUrlSet(str, strPageDepth);
            }
            else
            {
                newValue = this.GetIsShowLink(strUrlPre + dRow["folder"], dRow, strPageDepth, strpagePre);
            }
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:sLinkPath]", newValue);
            string str3 = dRow["ColumnName"].ToString();
            if ((this.nSmallLength != 0) && (str3.Length > this.nSmallLength))
            {
                str3 = str3.Substring(0, this.nSmallLength) + "...";
            }
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:sTitle]", str3);
            string str4 = dRow["id"].ToString();
            if (this.LoopAttribute.Role == "ID")
            {
                strLoopContentSamll = strLoopContentSamll.Replace("[QH:sNumber]", str4);
            }
            else
            {
                strLoopContentSamll = strLoopContentSamll.Replace("[QH:sNumber]", (nj + int.Parse(this.LoopAttribute.Role)).ToString());
            }
            if (strColumnID == str4)
            {
                strLoopContentSamll = strLoopContentSamll.Replace("[QH:sJSNow]", "cdqhNow2").Replace("[QH:sNowClass]", this.LoopAttribute.ProductFilter);
                bBigNow = true;
                return strLoopContentSamll;
            }
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:sJSNow]", "").Replace("[QH:sNowClass]", "");
            return strLoopContentSamll;
        }

        private string LoopReplaceTitleContent(string strLoopContent, DataRow dRow, string strTitleClass, string strPageDepth, string strUrlPre, string strpagePre, string strFenLanEnglish)
        {
            string newValue = dRow["outLink"].ToString().Trim();
            string str2 = dRow["fileName"].ToString().Trim();
            if (str2 != string.Empty)
            {
                str2 = (str2.IndexOf(".") < 0) ? (str2 + ".html") : str2;
            }
            strUrlPre = (strUrlPre == "") ? "" : (strUrlPre + "/");
            if (newValue != "")
            {
                if (newValue.Contains("http://"))
                {
                    strLoopContent = strLoopContent.Replace("[QH:LinkPath]", newValue);
                }
                else
                {
                    strLoopContent = strLoopContent.Replace("[QH:LinkPath]", this.PageUrlSet(newValue, strPageDepth));
                }
            }
            else if ((dRow["IsShow"].ToString() == "0") && (dRow["depth"].ToString() != "2"))
            {
                DataRow[] rowArray = this.dTableClmn.Select("ParentID='" + dRow["ID"] + "'");
                if (rowArray.Length > 0)
                {
                    strLoopContent = strLoopContent.Replace("[QH:LinkPath]", this.GetTitleLink(rowArray[0], strPageDepth, string.Concat(new object[] { strUrlPre, dRow["folder"], "/", rowArray[0]["folder"] }), strpagePre));
                }
            }
            else
            {
                strLoopContent = strLoopContent.Replace("[QH:LinkPath]", string.Concat(new object[] { strpagePre, strUrlPre, dRow["folder"], "/", str2 }));
            }
            strLoopContent = strLoopContent.Replace(strTitleClass, dRow["ColumnName"].ToString());
            strLoopContent = strLoopContent.Replace("[QH:PicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
            strLoopContent = strLoopContent.Replace("[QH:FenLanPic]", this.PageUrlSet(dRow["C_img"].ToString(), strPageDepth));
            strLoopContent = strLoopContent.Replace("[QH:AltText]", dRow["ColumnName"].ToString());
            strLoopContent = strLoopContent.Replace("[QH:BriefIntro]", this.PageContentUrlSet(dRow["BriefIntro"].ToString(), strPageDepth));
            if (strLoopContent.Contains("[QH:BriefIntro"))
            {
                this.ReplaceContentsBrief(ref strLoopContent, dRow["BriefIntro"].ToString(), "BriefIntro");
            }
            strLoopContent = strLoopContent.Replace("[QH:FenLanEnglish]", strFenLanEnglish);
            strLoopContent = strLoopContent.Replace("[QH:FenLanEnglish1]", dRow["C_EnTitle"].ToString());
            strLoopContent = strLoopContent.Replace("[QH:Contents]", this.FilterHotLink(this.PageContentUrlSet(dRow["content"].ToString(), strPageDepth), strPageDepth));
            if (strLoopContent.Contains("[QH:ContentsBrief"))
            {
                this.ReplaceContentsBrief(ref strLoopContent, dRow["Content"].ToString());
            }
            return strLoopContent;
        }

        private string NavLoopReplaceBigContent(string strLoopContent, DataRow dRow, string strpagePre, string strUrlPre, int nNameNumber, string strLoopContentMedium, string strLoopMedium, string strLoopContentSmall, string strLoopSmall, string strPageDepth, string strColumnID, int nNumber)
        {
            bool bBigNow = false;
            string str = this.ColumnUrlPre(dRow, this.dTableClmn);
            str = (str == "") ? "" : (str + "/");
            string newValue = "";
            string str3 = dRow["outLink"].ToString().Trim();
            if (str3 != "")
            {
                newValue = str3.Contains("http://") ? str3 : this.PageUrlSet(str3, strPageDepth);
            }
            else
            {
                newValue = this.GetIsShowLink(str + dRow["folder"], dRow, strPageDepth, strpagePre);
            }
            strLoopContent = strLoopContent.Replace("[QH:LinkPath]", newValue);
            string str4 = dRow["ColumnName"].ToString();
            if ((this.nBigLength != 0) && (str4.Length > this.nBigLength))
            {
                str4 = str4.Substring(0, this.nBigLength) + "...";
            }
            strLoopContent = strLoopContent.Replace("[QH:Title]", str4);
            string str5 = dRow["id"].ToString();
            strLoopContent = strLoopContent.Replace("[QH:Number]", nNumber.ToString());
            strLoopContent = strLoopContent.Replace("[QH:PicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
            if (strLoopMedium == "")
            {
                return strLoopContent;
            }
            StringBuilder builder = new StringBuilder("");
            DataRow[] rowArray = this.dTableClmn.Select("ParentID='" + str5 + "'");
            if (rowArray.Length == 0)
            {
                return Regex.Replace(strLoopContent.Replace(strLoopMedium, ""), @"<ul>[\s\n\r]*</ul>", "", RegexOptions.IgnoreCase);
            }
            strUrlPre = this.ColumnUrlPre(rowArray[0], this.dTableClmn);
            for (int i = 0; i < rowArray.Length; i++)
            {
                builder.Append(this.NavLoopReplaceMediumContent(strLoopContentMedium, rowArray[i], strpagePre, strUrlPre, i, strLoopContentSmall, strLoopSmall, strPageDepth, strColumnID, ref bBigNow));
            }
            return strLoopContent.Replace(strLoopMedium, builder.ToString());
        }

        private string NavLoopReplaceMediumContent(string strLoopContentMedium, DataRow dRow, string strpagePre, string strUrlPre, int nNameNumber, string strLoopContentSmall, string strLoopSmall, string strPageDepth, string strColumnID, ref bool bBigNow)
        {
            strUrlPre = (strUrlPre == "") ? "" : (strUrlPre + "/");
            string newValue = "";
            string str = dRow["outLink"].ToString().Trim();
            if (str != "")
            {
                newValue = str.Contains("http://") ? str : this.PageUrlSet(str, strPageDepth);
            }
            else
            {
                newValue = this.GetIsShowLink(strUrlPre + dRow["folder"], dRow, strPageDepth, strpagePre);
            }
            strLoopContentMedium = strLoopContentMedium.Replace("[QH:sLinkPath]", newValue);
            string str3 = dRow["ColumnName"].ToString();
            if ((this.nMediumLength != 0) && (str3.Length > this.nMediumLength))
            {
                str3 = str3.Substring(0, this.nMediumLength) + "...";
            }
            strLoopContentMedium = strLoopContentMedium.Replace("[QH:sTitle]", str3);
            string str4 = dRow["id"].ToString();
            strLoopContentMedium = strLoopContentMedium.Replace("[QH:sNumber]", (nNameNumber + 1).ToString());
            strLoopContentMedium = strLoopContentMedium.Replace("[QH:sPicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
            if (strLoopSmall == "")
            {
                return strLoopContentMedium;
            }
            StringBuilder builder = new StringBuilder("");
            DataRow[] rowArray = this.dTableClmn.Select("ParentID='" + str4 + "'");
            if (rowArray.Length == 0)
            {
                return Regex.Replace(strLoopContentMedium.Replace(strLoopSmall, ""), @"<ul>[\s\n\r]*</ul>", "", RegexOptions.IgnoreCase);
            }
            strUrlPre = this.ColumnUrlPre(rowArray[0], this.dTableClmn);
            for (int i = 0; i < rowArray.Length; i++)
            {
                builder.Append(this.NavLoopReplaceSmallContent(strLoopContentSmall, rowArray[i], strpagePre, strUrlPre, i, strPageDepth, strColumnID, ref bBigNow));
            }
            return strLoopContentMedium.Replace(strLoopSmall, builder.ToString());
        }

        private string NavLoopReplaceSmallContent(string strLoopContentSamll, DataRow dRow, string strpagePre, string strUrlPre, int nNameNumber, string strPageDepth, string strColumnID, ref bool bBigNow)
        {
            strUrlPre = (strUrlPre == "") ? "" : (strUrlPre + "/");
            string newValue = "";
            string str = dRow["outLink"].ToString().Trim();
            if (str != "")
            {
                newValue = str.Contains("http://") ? str : this.PageUrlSet(str, strPageDepth);
            }
            else
            {
                newValue = this.GetIsShowLink(strUrlPre + dRow["folder"], dRow, strPageDepth, strpagePre);
            }
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssLinkPath]", newValue);
            string str3 = dRow["ColumnName"].ToString();
            if ((this.nSmallLength != 0) && (str3.Length > this.nSmallLength))
            {
                str3 = str3.Substring(0, this.nSmallLength) + "...";
            }
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssTitle]", str3);
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssNumber]", (nNameNumber + 1).ToString());
            strLoopContentSamll = strLoopContentSamll.Replace("[QH:ssPicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
            return strLoopContentSamll;
        }

        private DataRow[] OrderByIDMarkIn(DataRow[] foundRows, string strIDMarkIn)
        {
            string[] strArray = strIDMarkIn.Replace("'", "").Split(new char[] { ',' });
            DataRow[] rowArray = new DataRow[strArray.Length];
            int num = 0;
            foreach (string str in strArray)
            {
                foreach (DataRow row in foundRows)
                {
                    if (row["IDMark"].ToString() == str)
                    {
                        rowArray[num++] = row;
                        break;
                    }
                }
            }
            return rowArray;
        }

        private string PageContentUrlSet(string str, string strPageDepth)
        {
            MatchCollection matchs = new Regex("<img([^\\/>]+)src=[\"']([^\"']+)[\"']([^\\/>]*)\\/?>", RegexOptions.IgnoreCase).Matches(str);
            string[] strArray = new string[matchs.Count];
            string[] strArray2 = new string[matchs.Count];
            string str2 = this.GetPagePre(strPageDepth) + this.strGlobalRootDirPre;
            for (int i = 0; i < matchs.Count; i++)
            {
                strArray[i] = strArray2[i] = matchs[i].Groups[2].Value.Trim();
                if (!strArray2[i].Contains("http://"))
                {
                    if (strArray2[i].Contains("../../../"))
                    {
                        strArray2[i] = strArray2[i].Replace("../../../", "");
                    }
                    if (strArray[i] != "")
                    {
                        if (strArray2[i][0] == '/')
                        {
                            strArray2[i] = str2 + strArray2[i].Substring(1);
                        }
                        else if (strArray2[i][0] == '.')
                        {
                            strArray2[i] = str2 + strArray2[i].Substring(3);
                        }
                        else
                        {
                            strArray2[i] = str2 + strArray2[i];
                        }
                        str = str.Replace(strArray[i], strArray2[i]);
                    }
                }
            }
            str = this.PageContentUrlSetAnchorUpload(str, strPageDepth);
            return str;
        }

        private string PageContentUrlSetAnchorUpload(string str, string strPageDepth)
        {
            MatchCollection matchs = new Regex("<a([^\\/>]+)href=[\"'](../upload/[^\"']+)[\"']([^\\/>]*)\\/?>", RegexOptions.IgnoreCase).Matches(str);
            string[] strArray = new string[matchs.Count];
            string[] strArray2 = new string[matchs.Count];
            string str2 = this.GetPagePre(strPageDepth) + this.strGlobalRootDirPre;
            for (int i = 0; i < matchs.Count; i++)
            {
                strArray[i] = strArray2[i] = matchs[i].Groups[2].Value.Trim();
                if (!strArray2[i].Contains("http://"))
                {
                    if (strArray2[i].Contains("../../../"))
                    {
                        strArray2[i] = strArray2[i].Replace("../../../", "");
                    }
                    if (strArray[i] != "")
                    {
                        if (strArray2[i][0] == '/')
                        {
                            strArray2[i] = str2 + strArray2[i].Substring(1);
                        }
                        else if (strArray2[i][0] == '.')
                        {
                            strArray2[i] = str2 + strArray2[i].Substring(3);
                        }
                        else
                        {
                            strArray2[i] = str2 + strArray2[i];
                        }
                        str = str.Replace(strArray[i], strArray2[i]);
                    }
                }
            }
            return str;
        }

        private string PageUrlSet(string str, string strPageDepth)
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
            string str4 = this.GetPagePre(strPageDepth) + this.strGlobalRootDirPre;
            if (str2[0] == '/')
            {
                return (str4 + str2.Substring(1));
            }
            if (str2[0] == '.')
            {
                return (str4 + str2.Substring(3));
            }
            return (str4 + str2);
        }

        private string ReadValue(string strLoopRule, string strName)
        {
            int index = strLoopRule.IndexOf(strName);
            string str = "";
            if (index != -1)
            {
                str = strLoopRule.Substring(index);
                index = str.IndexOf('=');
                if (index == -1)
                {
                    return "";
                }
                int num2 = str.IndexOf(',');
                if (num2 != -1)
                {
                    return str.Substring(index + 1, (num2 - index) - 1).Trim();
                }
            }
            return "";
        }

        private void RecursionGetID(ref string strIDIn, DataRow[] foundRows)
        {
            foreach (DataRow row in foundRows)
            {
                string str = row["depth"].ToString();
                string str2 = row["id"].ToString();
                if (str == "2")
                {
                    strIDIn = strIDIn + ",'" + str2 + "'";
                }
                else
                {
                    strIDIn = strIDIn + ",'" + str2 + "'";
                    DataRow[] rowArray = this.dTableClmn.Select("ParentID='" + str2 + "'");
                    this.RecursionGetID(ref strIDIn, rowArray);
                }
            }
        }

        private void RepalceLoopCusField(ref string strLoopContent, string strlistID, string strPageDepth)
        {
            if ((this.listStrCusField != null) && (this.dtpListLoop != null))
            {
                foreach (string[] strArray in this.listStrCusField)
                {
                    strLoopContent = strLoopContent.Replace(strArray[0], strArray[3]);
                    DataRow[] rowArray = this.dtpListLoop.Select("listid='" + strlistID + "' and paraid='" + strArray[4] + "'");
                    if (rowArray.Length == 0)
                    {
                        strLoopContent = strLoopContent.Replace(strArray[1], "");
                    }
                    else
                    {
                        strLoopContent = strLoopContent.Replace(strArray[1], rowArray[0]["info"].ToString());
                    }
                }
            }
        }

        private void ReplaceAdvertise(ref string strTemp, string strPageDepth)
        {
            List<string[]> listastrAdv = this.Bll1.DAL1.ReadDataReaderListStr("select [order],title,linkUrl,WordContent,ImageUrl,JsCode,LinkType,Width,Height from QH_Advertise order by CLNG(order) asc", 9);
            string newValue = "";
            int startIndex = 0;
            int index = 0;
            string oldValue = "";
            string input = "";
            for (int i = 0; i < 100; i++)
            {
                string str4;
                startIndex = strTemp.IndexOf("[QH:Advertise", startIndex);
                if (startIndex == -1)
                {
                    return;
                }
                index = strTemp.IndexOf(']', startIndex);
                oldValue = input = strTemp.Substring(startIndex, (index - startIndex) + 1);
                input = input.Replace("[QH:Advertise", "").Replace(":", "").Replace("]", "").Trim();
                if (!Regex.IsMatch(input, @"^\d+$"))
                {
                    strTemp = strTemp.Replace(oldValue, "广告标签序号不规范，请改正。");
                    continue;
                }
                string[] advertise = this.GetAdvertise(listastrAdv, input);
                if (advertise == null)
                {
                    strTemp = strTemp.Replace(oldValue, "此序号的广告不存在，请改正。");
                    continue;
                }
                string str5 = advertise[6];
                if (str5 != null)
                {
                    if (!(str5 == "0"))
                    {
                        if (str5 == "1")
                        {
                            goto Label_01AC;
                        }
                        if (str5 == "2")
                        {
                            goto Label_0234;
                        }
                    }
                    else if (advertise[2].Trim() == "")
                    {
                        newValue = advertise[3];
                    }
                    else
                    {
                        newValue = "<a href=\"" + (advertise[2].Contains("http://") ? advertise[2] : this.PageUrlSet(advertise[2], strPageDepth)) + "\" title=\"" + advertise[1] + " target=_blank \">" + advertise[3] + "</a>";
                    }
                }
                goto Label_0238;
            Label_01AC:
                str4 = this.GetImgHtml(advertise, strPageDepth);
                if (advertise[2].Trim() == "")
                {
                    newValue = str4;
                }
                else
                {
                    newValue = "<a href=\"" + (advertise[2].Contains("http://") ? advertise[2] : this.PageUrlSet(advertise[2], strPageDepth)) + "\" title=\"" + advertise[1] + " target=_blank \">" + str4 + "</a>";
                }
                goto Label_0238;
            Label_0234:
                newValue = advertise[5];
            Label_0238:
                strTemp = strTemp.Replace(oldValue, newValue);
            }
        }

        private void ReplaceBackList(ref string strTemp1, DataRow dRow, string strPageDepth)
        {
            string pagePre = this.GetPagePre(strPageDepth);
            string str2 = this.ColumnUrlPre(dRow, this.dTableClmn);
            string newValue = dRow["outLink"].ToString().Trim();
            string str4 = dRow["fileName"].ToString().Trim();
            if (str4 != string.Empty)
            {
                str4 = (str4.IndexOf(".") < 0) ? (str4 + ".html") : str4;
            }
            str2 = (str2 == "") ? "" : (str2 + "/");
            if (newValue != "")
            {
                if (newValue.Contains("http://"))
                {
                    strTemp1 = strTemp1.Replace("[QH:BackList]", newValue);
                }
                else
                {
                    strTemp1 = strTemp1.Replace("[QH:BackList]", this.PageUrlSet(newValue, strPageDepth));
                }
            }
            else if ((dRow["IsShow"].ToString() == "0") && (dRow["depth"].ToString() != "2"))
            {
                DataRow[] rowArray = this.dTableClmn.Select("ParentID='" + dRow["ID"] + "'");
                if (rowArray.Length > 0)
                {
                    strTemp1 = strTemp1.Replace("[QH:BackList]", this.GetTitleLink(rowArray[0], strPageDepth, string.Concat(new object[] { str2, dRow["folder"], "/", rowArray[0]["folder"] }), pagePre));
                }
            }
            else
            {
                strTemp1 = strTemp1.Replace("[QH:BackList]", string.Concat(new object[] { pagePre, str2, dRow["folder"], "/", str4 }));
            }
            strTemp1 = strTemp1.Replace("[QH:ProductClass]", dRow["ColumnName"].ToString().Trim());
        }

        private void ReplaceBaiduMap(ref string strTemp, string strPageDepth)
        {
            string input = Regex.Match(strTemp, @"\[QH:BaiduMap([^\]]*)?\]", RegexOptions.IgnoreCase).ToString();
            if (input == "")
            {
                strTemp = strTemp.Replace("[QH:BaiduMap", "地图标贴不正确。");
            }
            else
            {
                string str2 = Regex.Match(input, @"width=(\d+\%?)(?:(,|\]| )), 1").Groups[1].ToString();
                string str3 = Regex.Match(input, @"height=(\d+\%?)(?:(,|\]| )), 1").Groups[1].ToString();
                str2 = (str2 == "") ? "600" : str2;
                str3 = (str3 == "") ? "300" : str3;
                str2 = str2.Contains("%") ? str2 : (str2 + "px");
                str3 = str3.Contains("%") ? str3 : (str3 + "px");
                StringBuilder builder = new StringBuilder("");
                builder.Append("<style type=\"text/css\">\n");
                builder.Append("#cdqhallmap {width: " + str2 + ";height: " + str3 + ";overflow: hidden;margin:0; }\n");
                builder.Append("</style>\n");
                builder.Append("<div id=\"cdqhallmap\"></div>\n");
                builder.Append("<script type=\"text/javascript\">\n");
                builder.Append("function GenerateMap(lng,lat,info){\n");
                builder.Append("var map = new BMap.Map(\"cdqhallmap\");\n");
                builder.Append("map.centerAndZoom(new BMap.Point(lng, lat), 18);\n");
                builder.Append("var marker1 = new BMap.Marker(new BMap.Point(lng, lat));\n");
                builder.Append("map.addOverlay(marker1);\n");
                builder.Append("map.enableScrollWheelZoom();\n");
                builder.Append("var infoWindow1 = new BMap.InfoWindow(info);\n");
                builder.Append("marker1.addEventListener(\"click\", function(){this.openInfoWindow(infoWindow1);});\n");
                builder.Append("}\n");
                string[] strArray = this.Bll1.DAL1.ReadDataReaderStringArray("select top 1 * from QH_Map", 5);
                if (strArray[0] == null)
                {
                    string str4 = "116.37119307207";
                    string str5 = "39.978917549401";
                    string str6 = "北京市海淀区花园路13号怡和中心401<br>北京创都启航网络科技有限公司";
                    builder.Append("GenerateMap(" + str4 + "," + str5 + ",'" + str6 + "');\n");
                }
                else
                {
                    builder.Append("GenerateMap(" + strArray[1] + "," + strArray[2] + ",'" + strArray[4] + "');\n");
                }
                builder.Append("</script>\n");
                this.SetJS(ref strTemp, "<script type=\"text/javascript\" src=\"http://api.map.baidu.com/api?v=2.0&ak=PYAQmsU6cHwnEdBax3c8BupF\"></script>");
                strTemp = strTemp.Replace(input, builder.ToString());
            }
        }

        private void ReplaceBanner(ref string strTemp, string strPageDepth, string strColumnID)
        {
            if (this.bIsMobile)
            {
                this.ReplaceBannerMobile(ref strTemp, strPageDepth, strColumnID);
            }
            else
            {
                string str;
                if (this.GetBnrSetData(ref strTemp, strColumnID, "[QH:Banner]") && ((str = this.astrBnrSet[0]) != null))
                {
                    if (!(str == "0"))
                    {
                        if (!(str == "1"))
                        {
                            if (!(str == "2"))
                            {
                                if (str == "3")
                                {
                                    this.ReplaceBannerMode3(ref strTemp, strPageDepth, strColumnID);
                                }
                            }
                            else
                            {
                                this.ReplaceBannerMode2(ref strTemp, strPageDepth, strColumnID);
                            }
                        }
                        else
                        {
                            this.ReplaceBannerMode1(ref strTemp, strPageDepth, strColumnID);
                        }
                    }
                    else
                    {
                        strTemp = strTemp.Replace("[QH:Banner]", "");
                    }
                }
            }
        }

        private void ReplaceBannerMobile(ref string strTemp, string strPageDepth, string strColumnID)
        {
            string str;
            if (this.GetBnrSetDataMobile(ref strTemp, strColumnID, "") && ((str = this.astrBnrSet[0]) != null))
            {
                if (!(str == "0"))
                {
                    if (!(str == "1"))
                    {
                        if (str == "3")
                        {
                            this.ReplaceBannerMode3Mobile(ref strTemp, strPageDepth, strColumnID);
                        }
                    }
                    else
                    {
                        this.ReplaceBannerMode1Mobile(ref strTemp, strPageDepth, strColumnID);
                    }
                }
                else
                {
                    strTemp = strTemp.Replace("[QH:Banner]", "");
                }
            }
        }

        private void ReplaceBannerMode1(ref string strTemp, string strPageDepth, string strColumnID)
        {
            DataTable bannerUrl = this.GetBannerUrl("0", strColumnID);
            if (bannerUrl.Rows.Count == 0)
            {
                strTemp = strTemp.Replace("[QH:Banner]", "栏目<" + this.strColumnName + ">没有设置Banner图片。");
            }
            else
            {
                string strJSType = "";
                string[,] strArray = new string[,] { { "0", "0" }, { "4", "3" }, { "3", "0" }, { "4", "0" }, { "4", "3" }, { "4", "3" }, { "4", "3" }, { "4", "3" }, { "4", "3" } };
                string[] strArray2 = this.astrBnrSet[1].Split(new char[] { '|' });
                string strTime = (strArray2.Length <= 1) ? "" : strArray2[1];
                string strEffect = (strArray2.Length <= 2) ? "" : strArray2[2];
                strTime = (strTime == "") ? strArray[int.Parse(strArray2[0]), 0] : strTime;
                strEffect = (strEffect == "") ? strArray[int.Parse(strArray2[0]), 1] : strEffect;
                string strLinkInsert = "";
                string strHasJQuery = "";
                string strHasiepngfix = "";
                this.strBannerTemp = this.GetStyleContent(strArray2[0], strPageDepth, ref strJSType, ref strLinkInsert, ref strHasJQuery, ref strHasiepngfix, strTime, strEffect, bannerUrl.Rows.Count);
                if (this.strBannerTemp == "")
                {
                    strTemp = strTemp.Replace("[QH:Banner]", "没有此样式的Banner!");
                }
                else
                {
                    if ((strHasJQuery == "1") && (strTemp.IndexOf("jquery") == -1))
                    {
                        this.InsertLink(ref strTemp, "<script type=\"text/javascript\" src=\"" + this.GetPagePre(strPageDepth) + "Ajax/jquery.js\"></script>\n");
                    }
                    if ((strHasiepngfix == "1") && (strTemp.IndexOf("iepngfix") == -1))
                    {
                        this.InsertLink(ref strTemp, "<script type=\"text/javascript\" src=\"" + this.GetPagePre(strPageDepth) + "Ajax/iepngfix.js\"></script>\n");
                    }
                    if (strLinkInsert != "")
                    {
                        this.InsertLink(ref strTemp, strLinkInsert);
                    }
                    if (strJSType == "2")
                    {
                        int nloopNum = Math.Min(6, bannerUrl.Rows.Count);
                        this.strBannerTemp = this.strBannerTemp.Replace("[QH:picWidth]", this.astrBnrSet[2]);
                        this.strBannerTemp = this.strBannerTemp.Replace("[QH:picHeight]", this.astrBnrSet[3]);
                        this.strBannerTemp = this.LoopReplaceJS2Banner(this.strBannerTemp, bannerUrl, nloopNum, strPageDepth);
                        strTemp = strTemp.Replace("[QH:Banner]", this.strBannerTemp);
                    }
                    else
                    {
                        string strLoop = "";
                        string strLoopRule = "";
                        string strLoopContent = "";
                        this.GetLabel(ref this.strBannerTemp, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:loop ");
                        StringBuilder builder = new StringBuilder("");
                        for (int i = 0; i < bannerUrl.Rows.Count; i++)
                        {
                            builder.Append(this.LoopReplaceContent(strLoopContent, bannerUrl.Rows[i], "QH_BannerImg", strPageDepth, i));
                        }
                        this.strBannerTemp = this.strBannerTemp.Replace(strLoop, builder.ToString());
                        this.strBannerTemp = this.strBannerTemp.Replace("[QH:picWidth]", this.astrBnrSet[2]);
                        this.strBannerTemp = this.strBannerTemp.Replace("[QH:picHeight]", this.astrBnrSet[3]);
                        this.strBannerTemp = this.strBannerTemp.Replace("[QH:SitePath]", this.GetPagePre(strPageDepth));
                        strTemp = strTemp.Replace("[QH:Banner]", this.strBannerTemp);
                    }
                }
            }
        }

        private void ReplaceBannerMode1Mobile(ref string strTemp, string strPageDepth, string strColumnID)
        {
            DataTable dtBnrImg = this.dtBnrImg;
            if (dtBnrImg.Rows.Count == 0)
            {
                strTemp = strTemp.Replace("[QH:Banner]", "没有设置Banner图片。");
            }
            else
            {
                string strLinkInsert = "";
                string strHasJQuery = "";
                string strHasiepngfix = "";
                this.strBannerTemp = this.GetStyleContentMobile("1", strPageDepth, ref strLinkInsert, ref strHasJQuery, ref strHasiepngfix, dtBnrImg.Rows.Count);
                if (this.strBannerTemp == "")
                {
                    strTemp = strTemp.Replace("[QH:Banner]", "没有此样式的Banner!");
                }
                else
                {
                    if ((strHasJQuery == "1") && (strTemp.IndexOf("jquery") == -1))
                    {
                        this.InsertLink(ref strTemp, "<script type=\"text/javascript\" src=\"../" + this.GetPagePre(strPageDepth) + "Ajax/jquery.js\"></script>\n");
                    }
                    if ((strHasiepngfix == "1") && (strTemp.IndexOf("iepngfix") == -1))
                    {
                        this.InsertLink(ref strTemp, "<script type=\"text/javascript\" src=\"../" + this.GetPagePre(strPageDepth) + "Ajax/iepngfix.js\"></script>\n");
                    }
                    if (strLinkInsert != "")
                    {
                        this.InsertLink(ref strTemp, strLinkInsert);
                    }
                    string strLoop = "";
                    string strLoopRule = "";
                    string strLoopContent = "";
                    this.GetLabel(ref this.strBannerTemp, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:loop");
                    StringBuilder builder = new StringBuilder("");
                    for (int i = 0; i < dtBnrImg.Rows.Count; i++)
                    {
                        builder.Append(this.LoopReplaceContent(strLoopContent, dtBnrImg.Rows[i], "QH_BannerImg", strPageDepth, i));
                    }
                    this.strBannerTemp = this.strBannerTemp.Replace(strLoop, builder.ToString());
                    this.strBannerTemp = this.strBannerTemp.Replace("[QH:picWidth]", this.astrBnrSet[2].Contains("%") ? this.astrBnrSet[2] : (this.astrBnrSet[2] + "px"));
                    this.strBannerTemp = this.strBannerTemp.Replace("[QH:picHeight]", this.astrBnrSet[3]);
                    this.strBannerTemp = this.strBannerTemp.Replace("[QH:SitePath]", this.GetPagePre(strPageDepth));
                    strTemp = strTemp.Replace("[QH:Banner]", this.strBannerTemp);
                }
            }
        }

        private void ReplaceBannerMode2(ref string strTemp, string strPageDepth, string strColumnID)
        {
            DataTable bannerUrl = this.GetBannerUrl("1", strColumnID);
            if (bannerUrl.Rows.Count == 0)
            {
                strTemp = strTemp.Replace("[QH:Banner]", "栏目<" + this.strColumnName + ">没有设置Flash。");
            }
            else
            {
                string format = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\" width=\"{1}\" height=\"{2}\">\n<param name=\"movie\" value=\"{0}\" />\n<param name=\"quality\" value=\"high\" />\n<embed src=\"{0}\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"{1}\" height=\"{2}\"></embed></object>";
                string newValue = string.Format(format, this.PageUrlSet(bannerUrl.Rows[0]["flashUrl"].ToString(), strPageDepth), this.astrBnrSet[2], this.astrBnrSet[3]);
                strTemp = strTemp.Replace("[QH:Banner]", newValue);
            }
        }

        private void ReplaceBannerMode3(ref string strTemp, string strPageDepth, string strColumnID)
        {
            DataTable bannerUrl = this.GetBannerUrl("0", strColumnID);
            if (bannerUrl.Rows.Count == 0)
            {
                strTemp = strTemp.Replace("[QH:Banner]", "栏目<" + this.strColumnName + ">没有设置Banner图片。");
            }
            else
            {
                string newValue = string.Format("<img src=\"{0}\" alt =\"{1}\" width=\"{2}\" height=\"{3}\" />", new object[] { this.PageUrlSet(bannerUrl.Rows[0]["ImgUrl"].ToString(), strPageDepth), bannerUrl.Rows[0]["Title"].ToString(), this.astrBnrSet[2], this.astrBnrSet[3] });
                strTemp = strTemp.Replace("[QH:Banner]", newValue);
            }
        }

        private void ReplaceBannerMode3Mobile(ref string strTemp, string strPageDepth, string strColumnID)
        {
            if (this.dtBnrImg.Rows.Count == 0)
            {
                strTemp = strTemp.Replace("[QH:Banner]", "栏目<" + this.strColumnName + ">没有设置Banner图片。");
            }
            else
            {
                string newValue = string.Format("<img src=\"{0}\" alt =\"{1}\" width=\"{2}\" height=\"{3}\" />", new object[] { this.PageUrlSet(this.dtBnrImg.Rows[0]["ImgUrl"].ToString(), strPageDepth), this.dtBnrImg.Rows[0]["Title"].ToString(), this.astrBnrSet[2], this.astrBnrSet[3] });
                strTemp = strTemp.Replace("[QH:Banner]", newValue);
            }
        }

        private void ReplaceBannerPic(ref string strTemp, string strPageDepth, string strColumnID, string strLoop, string strLoopContent, string strLoopRule)
        {
            DataTable dtBnrImg;
            if (this.bIsMobile)
            {
                if (!this.GetBnrSetDataMobile(ref strTemp, strColumnID, strLoop))
                {
                    return;
                }
                dtBnrImg = this.dtBnrImg;
            }
            else
            {
                if (!this.GetBnrSetData(ref strTemp, strColumnID, strLoop))
                {
                    return;
                }
                dtBnrImg = this.GetBannerUrl("0", strColumnID);
            }
            if (dtBnrImg.Rows.Count == 0)
            {
                strTemp = strTemp.Replace(strLoop, "栏目<" + this.strColumnName + ">没有设置图片。");
            }
            else
            {
                strLoopContent = strLoopContent.Replace("[QH:picWidth]", this.astrBnrSet[2]).Replace("[QH:picHeight]", this.astrBnrSet[3]);
                int num = (this.LoopAttribute.NewsCount == "") ? 0 : int.Parse(this.LoopAttribute.NewsCount);
                int nloopNum = (num == 0) ? dtBnrImg.Rows.Count : num;
                nloopNum = (nloopNum > dtBnrImg.Rows.Count) ? dtBnrImg.Rows.Count : nloopNum;
                StringBuilder builder = new StringBuilder("");
                if (this.LoopAttribute.JStype == "2")
                {
                    nloopNum = (nloopNum > 6) ? 6 : nloopNum;
                    builder.Append(this.LoopReplaceJS2Banner(strLoopContent, dtBnrImg, nloopNum, strPageDepth));
                }
                else
                {
                    string input = this.ReadValue(strLoopRule, "NumStart");
                    int num3 = 0;
                    if (Regex.IsMatch(input, @"^\d+$"))
                    {
                        num3 = int.Parse(input);
                    }
                    for (int i = 0; i < nloopNum; i++)
                    {
                        DataRow dRow = dtBnrImg.Rows[i];
                        int nNameNumber = num3 + i;
                        if (dRow["ImgUrl"].ToString().Trim() == "")
                        {
                            break;
                        }
                        builder.Append(this.LoopReplaceContent(strLoopContent, dRow, "QH_BannerImg", strPageDepth, nNameNumber));
                    }
                }
                strTemp = strTemp.Replace(strLoop, builder.ToString());
            }
        }

        private void ReplaceCommen(ref string strTemp, DataRow dRow, string strPageDepth)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string strFenlanPic = "";
            string strFenLanEnglish = "";
            this.Get3ClassClmnData(ref str, ref str2, ref str3, "ColumnName", dRow);
            this.Get3ClassClmnData(ref str4, ref str5, ref str6, ref strFenlanPic, ref strFenLanEnglish, strPageDepth, dRow);
            if (this.bIsMobile && (str.Length > 10))
            {
                str = str.Substring(0, 10) + "...";
            }
            strTemp = strTemp.Replace("[QH:FenLanTitle1NoLink]", str);
            strTemp = strTemp.Replace("[QH:FenLanTitle1LinkSelf]", "<a href=\"#\">" + str + "</a>");
            if (str == "")
            {
                strTemp = strTemp.Replace("[QH:FenLanTitle1]", "");
            }
            else
            {
                strTemp = strTemp.Replace("[QH:FenLanTitle1]", "<a href=\"" + str4 + "\">" + str + "</a>");
            }
            if (str2 == "")
            {
                strTemp = strTemp.Replace("[QH:sFenLanTitle1]", "");
            }
            else
            {
                strTemp = strTemp.Replace("[QH:sFenLanTitle1]", "<a href=\"" + str5 + "\">" + str2 + "</a>");
            }
            if (str3 == "")
            {
                strTemp = strTemp.Replace("[QH:ssFenLanTitle1]", "");
            }
            else
            {
                strTemp = strTemp.Replace("[QH:ssFenLanTitle1]", "<a href=\"" + str6 + "\">" + str3 + "</a>");
            }
            string newValue = (str3 == "") ? ((str2 == "") ? str : str2) : str3;
            strTemp = strTemp.Replace("[QH:FenLanLanmu]", newValue);
            strTemp = strTemp.Replace("[QH:FenLanEnglish]", strFenLanEnglish);
            string pagePre = this.GetPagePre(strPageDepth);
            str = (str == "") ? ("<a href=\"" + pagePre + "\">首页</a>") : ("<a href=\"" + pagePre + "\">首页</a> >> <a href=\"" + str4 + "\">" + str + "</a>");
            str2 = (str2 == "") ? str2 : (" >> <a href=\"" + str5 + "\">" + str2 + "</a>");
            str3 = (str3 == "") ? str3 : (" >> <a href=\"" + str6 + "\">" + str3 + "</a>");
            strTemp = strTemp.Replace("[QH:FenLanTitle]", str);
            strTemp = strTemp.Replace("[QH:sFenLanTitle]", str2);
            strTemp = strTemp.Replace("[QH:ssFenLanTitle]", str3);
            if (dRow["Module"].ToString() == "1")
            {
                strTemp = strTemp.Replace("[QH:Contents]", this.FilterHotLink(this.PageContentUrlSet(dRow["content"].ToString(), strPageDepth), strPageDepth));
            }
            strTemp = strTemp.Replace("[QH:FenLanPic]", this.PageUrlSet(strFenlanPic, strPageDepth));
            strTemp = strTemp.Replace("[QH:FenLanPath]", str4);
        }

        private void ReplaceContentsBrief(ref string strLoopContent, string strContents)
        {
            Match match = Regex.Match(strLoopContent, @"\[QH:ContentsBrief:?\s*(\d*)\s*\]");
            if (match.Groups[0].Value != "")
            {
                string s = match.Groups[1].Value;
                s = (s == "") ? "40" : s;
                int length = int.Parse(s);
                strContents = Regex.Replace(strContents, "<[^>]+>", "");
                strContents = strContents.Replace("&nbsp;", "").Replace("\n", "").Replace("\r", "");
                strContents = Regex.Replace(strContents, @"\s{2,}", " ");
                if (strContents.Length > length)
                {
                    strContents = strContents.Substring(0, length) + "...";
                }
                strLoopContent = strLoopContent.Replace(match.Groups[0].Value, strContents);
            }
        }

        private void ReplaceContentsBrief(ref string strLoopContent, string strContents, string strTags)
        {
            Match match = Regex.Match(strLoopContent, @"\[QH:" + strTags + @":?\s*(\d*)\s*\]");
            if (match.Groups[0].Value != "")
            {
                string s = match.Groups[1].Value;
                s = (s == "") ? "40" : s;
                int length = int.Parse(s);
                strContents = Regex.Replace(strContents, "<[^>]+>", "");
                strContents = strContents.Replace("&nbsp;", "").Replace("\n", "").Replace("\r", "");
                strContents = Regex.Replace(strContents, @"\s{2,}", " ");
                if (strContents.Length > length)
                {
                    strContents = strContents.Substring(0, length) + "...";
                }
                strLoopContent = strLoopContent.Replace(match.Groups[0].Value, strContents);
            }
        }

        private void ReplaceCusField(ref string strTemp, DataRow dwPrdct, DataTable dtPList, string[,] a2strCusField, string strMdl)
        {
            string str;
            string str2;
            this.GetCustomTags(strMdl, out str, out str2);
            string strLoopContent = "";
            string oldValue = this.GetCusFieldLoop(ref strTemp, ref strLoopContent, "[QH:CustomLoop]");
            if (oldValue != "")
            {
                if (dtPList == null)
                {
                    strTemp = strTemp.Replace(oldValue, "");
                }
                else
                {
                    StringBuilder builder = new StringBuilder("");
                    for (int i = 0; i < (a2strCusField.Length >> 1); i++)
                    {
                        DataRow[] rowArray = dtPList.Select(string.Concat(new object[] { "listid='", dwPrdct["id"], "' and paraid='", a2strCusField[i, 0], "'" }));
                        if (rowArray.Length == 0)
                        {
                            builder.Append(strLoopContent.Replace(str, a2strCusField[i, 1]).Replace(str2, ""));
                        }
                        else
                        {
                            builder.Append(strLoopContent.Replace(str, a2strCusField[i, 1]).Replace(str2, rowArray[0]["info"].ToString()));
                        }
                    }
                    strTemp = strTemp.Replace(oldValue, builder.ToString());
                }
            }
        }

        private void ReplaceDetailPicture(ref string strTemp, DataRow dRow, string strPageDepth, string strMdl)
        {
            string strLoop = "";
            string strLoopRule = "";
            string strLoopContent = "";
            this.GetLabel(ref strTemp, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:loopPic");
            if (strLoop != "")
            {
                string input = this.ReadValue(strLoopRule, "NumStart");
                int nStart = 0;
                if (Regex.IsMatch(input, @"^\d+$"))
                {
                    nStart = int.Parse(input);
                }
                List<string[]> picData = this.GetPicData(dRow, nStart);
                if (picData.Count == 0)
                {
                    strTemp = strTemp.Replace(strLoop, "");
                }
                else
                {
                    StringBuilder builder = new StringBuilder("");
                    for (int i = 0; i < picData.Count; i++)
                    {
                        builder.Append(this.LoopReplaceContent(strLoopContent, picData[i], strPageDepth));
                    }
                    strTemp = strTemp.Replace(strLoop, builder.ToString());
                }
            }
        }

        private void ReplaceDetailsPager(ref string strTemp1, DataTable dtDtls, int nNumber, string strGeneralFileName)
        {
            string newValue = "Prev&nbsp;&nbsp;Next&nbsp;&nbsp;";
            int count = dtDtls.Rows.Count;
            int index = strTemp1.IndexOf("[QH:Pager]");
            int num3 = strTemp1.IndexOf("[QH:PrevOne]");
            int num4 = strTemp1.IndexOf("[QH:PrevOneLink]");
            if (index != -1)
            {
                if ((nNumber + 1) >= count)
                {
                    newValue = newValue.Replace("Next", "已到最后页");
                }
                else
                {
                    string str2 = "<a href=" + strGeneralFileName + dtDtls.Rows[nNumber + 1]["ID"].ToString() + ".html>上一条</a>";
                    newValue = newValue.Replace("Next", str2);
                }
                if (nNumber == 0)
                {
                    newValue = newValue.Replace("Prev", "已到最前页");
                }
                else
                {
                    string str3 = "<a href=" + strGeneralFileName + dtDtls.Rows[nNumber - 1]["ID"].ToString() + ".html>下一条</a>";
                    newValue = newValue.Replace("Prev", str3);
                }
                strTemp1 = strTemp1.Replace("[QH:Pager]", newValue);
            }
            if (num3 != -1)
            {
                if ((nNumber + 1) >= count)
                {
                    strTemp1 = strTemp1.Replace("[QH:NextOne]", "已到最后页");
                }
                else
                {
                    string str4 = dtDtls.Rows[nNumber + 1]["Title"].ToString();
                    str4 = (str4.Length > 40) ? (str4.Substring(0, 40) + "...") : str4;
                    string str5 = "<a href=" + strGeneralFileName + dtDtls.Rows[nNumber + 1]["ID"].ToString() + ".html>" + str4 + "</a>";
                    if (this.bIsMobile)
                    {
                        str4 = (str4.Length > 6) ? (str4.Substring(0, 6) + "...") : str4;
                        str5 = "<a href=" + strGeneralFileName + dtDtls.Rows[nNumber + 1]["ID"].ToString() + ".html>上一条：" + str4 + "</a>";
                    }
                    strTemp1 = strTemp1.Replace("[QH:NextOne]", str5);
                }
                if (nNumber == 0)
                {
                    strTemp1 = strTemp1.Replace("[QH:PrevOne]", "已到最前页");
                }
                else
                {
                    string str6 = dtDtls.Rows[nNumber - 1]["Title"].ToString();
                    str6 = (str6.Length > 40) ? (str6.Substring(0, 40) + "...") : str6;
                    string str7 = "<a href=" + strGeneralFileName + dtDtls.Rows[nNumber - 1]["ID"].ToString() + ".html>" + str6 + "</a>";
                    if (this.bIsMobile)
                    {
                        str6 = (str6.Length > 6) ? (str6.Substring(0, 6) + "...") : str6;
                        str7 = "<a href=" + strGeneralFileName + dtDtls.Rows[nNumber - 1]["ID"].ToString() + ".html>下一条：" + str6 + "</a>";
                    }
                    strTemp1 = strTemp1.Replace("[QH:PrevOne]", str7);
                }
            }
            if (num4 != -1)
            {
                if ((nNumber + 1) >= count)
                {
                    strTemp1 = strTemp1.Replace("[QH:NextOneLink]", "#");
                }
                else
                {
                    strTemp1 = strTemp1.Replace("[QH:NextOneLink]", strGeneralFileName + dtDtls.Rows[nNumber + 1]["ID"].ToString() + ".html");
                }
                if (nNumber == 0)
                {
                    strTemp1 = strTemp1.Replace("[QH:PrevOneLink]", "#");
                }
                else
                {
                    strTemp1 = strTemp1.Replace("[QH:PrevOneLink]", strGeneralFileName + dtDtls.Rows[nNumber - 1]["ID"].ToString() + ".html");
                }
            }
        }

        private void ReplaceDownloadDetails(ref string strTemp, DataRow dRow, string strPageDepth)
        {
            this.RepalceLoopCusField(ref strTemp, dRow["id"].ToString(), strPageDepth);
            strTemp = strTemp.Replace("[QH:Author]", dRow["Author"].ToString());
            strTemp = strTemp.Replace("[QH:FileSize]", dRow["FileSize"].ToString());
            strTemp = strTemp.Replace("[QH:BriefIntro]", this.PageContentUrlSet(dRow["BriefIntro"].ToString(), strPageDepth));
            if (strTemp.Contains("[QH:BriefIntro"))
            {
                this.ReplaceContentsBrief(ref strTemp, dRow["BriefIntro"].ToString(), "BriefIntro");
            }
            string str = dRow["hits"].ToString().Trim();
            strTemp = strTemp.Replace("[QH:Hits]", "<span id=\"detailHits\" >" + ((str == "") ? "0" : str) + "</span>");
            strTemp = strTemp.Replace("[QH:NewsTags]", dRow["Tags"].ToString());
            strTemp = strTemp.Replace("[QH:LinkPath]", this.PageUrlSet("Ajax/DownloadFile.aspx?id=" + dRow["id"].ToString(), strPageDepth));
            string newValue = dRow["Title"].ToString();
            strTemp = strTemp.Replace("[QH:Title]", newValue);
            strTemp = strTemp.Replace("[QH:Contents]", this.FilterHotLink(this.PageContentUrlSet(dRow["content"].ToString(), strPageDepth), strPageDepth));
            string str3 = dRow["adddate"].ToString().Trim();
            if (str3 != "")
            {
                str3 = ((DateTime)dRow["adddate"]).ToString("yyyy-MM-dd");
            }
            strTemp = strTemp.Replace("[QH:Date]", str3);
            str3 = dRow["ModyDate"].ToString().Trim();
            if (str3 != "")
            {
                str3 = ((DateTime)dRow["ModyDate"]).ToString("yyyy-MM-dd");
            }
            strTemp = strTemp.Replace("[QH:MDate]", str3);
        }

        private void ReplaceFenyeTitle(ref string strTemp, DataRow dw, string strPageDepth)
        {
            for (int i = 0; i < 100; i++)
            {
                string str2;
                string str3;
                string str4;
                string str5;
                string strLoop = str2 = str3 = str4 = str5 = "";
                this.GetLabel(ref strTemp, ref strLoop, ref str2, ref str3, "[QH:loopFenYeT");
                if (strLoop == "")
                {
                    return;
                }
                if ((i == 0) && (this.dTableClmn == null))
                {
                    this.dTableClmn = this.Bll1.GetDataTable(this.strClmnQuery);
                }
                if (strLoop.Contains("[QH:FenYeTSmall"))
                {
                    this.ReplaceFenyeTitleBigSmall(ref strTemp, dw, strPageDepth, strLoop, str2, str3);
                }
                string str7 = this.ReadValue(str2, "Show2");
                string strNowClass = this.ReadValue(str2, "NowClass");
                string strNowClassImg = this.ReadValue(str2, "NowClassImg");
                string strFirstClass = this.ReadValue(str2, "FirstClass");
                string strLastClass = this.ReadValue(str2, "LastClass");
                string input = this.ReadValue(str2, "TitleNum");
                if (Regex.IsMatch(input, @"^\d+$"))
                {
                    this.nBigLength = int.Parse(input);
                }
                if (str7 == "1")
                {
                    this.GetLabelFenyeTitle(str3, ref str4, ref str5);
                }
                else
                {
                    str4 = str3;
                }
                string newValue = this.GetLoopFenyeTitleContent(str4, str5, dw, str7, strPageDepth, strNowClass, strNowClassImg, strFirstClass, strLastClass);
                strTemp = strTemp.Replace(strLoop, newValue);
            }
        }

        private void ReplaceFenyeTitleBigSmall(ref string strTemp, DataRow dw, string strPageDepth, string strLoop, string strLoopRule, string strLoopContent)
        {
            this.nBigLength = this.nSmallLength = 0;
            this.strJQueryNum = "0";
            string str = "0";
            string input = this.ReadValue(strLoopRule, "TitleNum");
            if (Regex.IsMatch(input, @"^\d+$"))
            {
                this.nBigLength = int.Parse(input);
            }
            this.LoopAttribute.JStype = this.ReadValue(strLoopRule, "NowClass");
            this.LoopAttribute.ProductFilter = this.ReadValue(strLoopRule, "sNowClass");
            this.LoopAttribute.Condition = this.ReadValue(strLoopRule, "NumStart");
            this.LoopAttribute.Role = this.ReadValue(strLoopRule, "sNumStart");
            if (!Regex.IsMatch(this.LoopAttribute.Condition, @"^\d+$"))
            {
                this.LoopAttribute.Condition = "ID";
            }
            if (!Regex.IsMatch(this.LoopAttribute.Role, @"^\d+$"))
            {
                this.LoopAttribute.Role = "ID";
            }
            string newValue = this.GetLoopListContent(strLoopContent, strPageDepth, str, dw, this.ReadValue(strLoopRule, "SelfLanmu"));
            if (this.strJQueryNum != "0")
            {
                strTemp = strTemp.Replace("[QH:JQueryNum]", this.strJQueryNum).Replace(strLoop, newValue);
            }
            else
            {
                strTemp = strTemp.Replace(strLoop, newValue);
            }
        }

        private void ReplaceFlash(ref string strTemp, string strPageDepth, string strColumnID, string strLoop, string strLoopContent)
        {
            if (this.GetBnrSetData(ref strTemp, strColumnID, strLoop))
            {
                DataTable bannerUrl = this.GetBannerUrl("1", strColumnID);
                if (bannerUrl.Rows.Count == 0)
                {
                    strTemp = strTemp.Replace(strLoop, "栏目<" + this.strColumnName + ">没有设置Flash。");
                }
                else
                {
                    strLoopContent = strLoopContent.Replace("[QH:picWidth]", this.astrBnrSet[2]).Replace("[QH:picHeight]", this.astrBnrSet[3]).Replace("[QH:FlashPath]", this.PageUrlSet(bannerUrl.Rows[0]["flashUrl"].ToString(), strPageDepth));
                    strTemp = strTemp.Replace(strLoop, strLoopContent);
                }
            }
        }

        private string ReplaceFYSmallTitle(string strLoopContent, string strLoopContentSamll, string strLoopSmall, DataRow dRow, string strpagePre, string strPageDepth, string strColumnID, ref bool bBigNow)
        {
            string strUrlPre = "";
            DataRow[] rowArray = this.dTableClmn.Select("ParentID='" + dRow["id"] + "'");
            if (rowArray.Length == 0)
            {
                return Regex.Replace(strLoopContent.Replace(strLoopSmall, ""), @"<ul>[\s\n\r]*</ul>", "", RegexOptions.IgnoreCase);
            }
            if (rowArray.Length > 0)
            {
                strUrlPre = this.ColumnUrlPre(rowArray[0], this.dTableClmn);
            }
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < rowArray.Length; i++)
            {
                builder.Append(this.LoopReplaceFYSmallContent(strLoopContentSamll, rowArray[i], strpagePre, strUrlPre, strPageDepth, i, strColumnID, ref bBigNow));
            }
            return strLoopContent.Replace(strLoopSmall, builder.ToString());
        }

        private void ReplaceHomeLabeling(ref string strTemp)
        {
            this.GetSiteInfoContents(ref strTemp, "S", "S");
            this.ReplaceNav(ref strTemp, "S");
            if (strTemp.IndexOf("[QH:Banner]") >= 0)
            {
                this.ReplaceBanner(ref strTemp, "S", "0");
            }
            this.ReplaceLoop(ref strTemp, "S", "0");
            this.ReplaceTitle(ref strTemp, "S", "0");
            this.ReplaceJianJie(ref strTemp, "S");
            this.ReplaceProductListTitle(ref strTemp, "S", "0");
            this.ReplaceLanmuLoopBig(ref strTemp, "S", "0");
            this.ReplaceNavLoopBig(ref strTemp, "S", "0");
            this.ReplaceStatisticsJS(ref strTemp, "S");
            if (strTemp.IndexOf("[QH:BaiduMap") >= 0)
            {
                this.ReplaceBaiduMap(ref strTemp, "S");
            }
            if (strTemp.IndexOf("[QH:SearchOnclick") >= 0)
            {
                this.ReplaceSearch(ref strTemp, "S", "News");
            }
            if (strTemp.IndexOf("[QH:Advertise") >= 0)
            {
                this.ReplaceAdvertise(ref strTemp, "S");
            }
            if ((strTemp.Contains("[QH:LangLink") || strTemp.Contains("[QH:LangVersion")) || strTemp.Contains("[QH:LangImg"))
            {
                this.ReplaceMultiLang(ref strTemp, "", "S");
            }
        }

        private void ReplaceImageDetails(ref string strTemp, DataRow dRow, string strPageDepth)
        {
            this.RepalceLoopCusField(ref strTemp, dRow["id"].ToString(), strPageDepth);
            strTemp = strTemp.Replace("[QH:PicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
            strTemp = strTemp.Replace("[QH:Author]", dRow["Author"].ToString());
            string newValue = dRow["Title"].ToString();
            strTemp = strTemp.Replace("[QH:Title]", newValue);
            strTemp = strTemp.Replace("[QH:BriefIntro]", this.PageContentUrlSet(dRow["BriefIntro"].ToString(), strPageDepth));
            if (strTemp.Contains("[QH:BriefIntro"))
            {
                this.ReplaceContentsBrief(ref strTemp, dRow["BriefIntro"].ToString(), "BriefIntro");
            }
            strTemp = strTemp.Replace("[QH:NewsTags]", dRow["Tags"].ToString());
            string str2 = dRow["hits"].ToString().Trim();
            strTemp = strTemp.Replace("[QH:Hits]", "<span id=\"detailHits\" >" + ((str2 == "") ? "0" : str2) + "</span>");
            strTemp = strTemp.Replace("[QH:Contents]", this.FilterHotLink(this.PageContentUrlSet(dRow["content"].ToString(), strPageDepth), strPageDepth));
            string str3 = dRow["adddate"].ToString().Trim();
            if (str3 != "")
            {
                str3 = ((DateTime)dRow["adddate"]).ToString("yyyy-MM-dd");
            }
            strTemp = strTemp.Replace("[QH:Date]", str3);
            str3 = dRow["ModyDate"].ToString().Trim();
            if (str3 != "")
            {
                str3 = ((DateTime)dRow["ModyDate"]).ToString("yyyy-MM-dd");
            }
            strTemp = strTemp.Replace("[QH:MDate]", str3);
        }

        private void ReplaceImagePicture(ref string strTemp, DataRow dRow, string strPageDepth)
        {
            Match match = Regex.Match(strTemp, @"\[QH:DetailPicture:?\s*(\d*)\s*\]");
            if (match.Groups[0].Value != "")
            {
                string s = match.Groups[1].Value;
                s = (s == "") ? "0" : s;
                this.astr1[0x16] = (this.astr1[0x16] == "") ? "0" : this.astr1[0x16];
                string[] strArray = this.astr1[0x16].Split(new char[] { '|' });
                List<string[]> picData = this.GetPicData(dRow, int.Parse(s));
                if (picData.Count == 0)
                {
                    strTemp = strTemp.Replace("[QH:DetailPicture]", "");
                }
                else
                {
                    string str2 = strArray[0];
                    if (str2 != null)
                    {
                        if (!(str2 == "0"))
                        {
                            if (!(str2 == "1"))
                            {
                                if (str2 == "2")
                                {
                                    strTemp = strTemp.Replace("[QH:DetailPicture]", "");
                                }
                            }
                            else
                            {
                                this.ReplaceImgStyle1(ref strTemp, strPageDepth, picData);
                            }
                        }
                        else
                        {
                            this.ReplaceImgStyle0(ref strTemp, strPageDepth, picData);
                        }
                    }
                }
            }
        }

        private void ReplaceImgStyle0(ref string strTemp, string strPageDepth, List<string[]> listastrPic)
        {
            if (strTemp.IndexOf("jquery") == -1)
            {
                this.InsertLink(ref strTemp, "<script type=\"text/javascript\" src=\"" + this.GetPagePre(strPageDepth) + "Ajax/jquery.js\"></script>\n");
            }
            this.InsertLink(ref strTemp, this.strLinkInsert);
            this.InsertJsBottom(ref strTemp, strPageDepth, "Ajax/slide.js");
            string strPicTempS = this.strPicTempS;
            string strLoop = "";
            string strLoopRule = "";
            string strLoopContent = "";
            for (int i = 0; i < 2; i++)
            {
                this.GetLabel(ref strPicTempS, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:loop ");
                StringBuilder builder = new StringBuilder("");
                for (int k = 0; (k < listastrPic.Count) && (k < 6); k++)
                {
                    builder.Append(this.LoopReplaceContent(strLoopContent, listastrPic[k], strPageDepth, k));
                }
                strPicTempS = strPicTempS.Replace(strLoop, builder.ToString());
            }
            StringBuilder builder2 = new StringBuilder("");
            for (int j = 0; (j < listastrPic.Count) && (j < 6); j++)
            {
                builder2.Append(",'x" + j + "'");
            }
            string newValue = (builder2.Length > 0) ? builder2.ToString().Substring(1) : "";
            strPicTempS = strPicTempS.Replace("{0}", newValue).Replace("{1}", Math.Max(Math.Min(listastrPic.Count, 6) - 1, 0).ToString());
            strTemp = strTemp.Replace("[QH:DetailPicture]", strPicTempS);
        }

        private void ReplaceImgStyle1(ref string strTemp, string strPageDepth, List<string[]> listastrPic)
        {
            if (strTemp.IndexOf("jquery") == -1)
            {
                this.InsertLink(ref strTemp, "<script type=\"text/javascript\" src=\"" + this.GetPagePre(strPageDepth) + "Ajax/jquery.js\"></script>\n");
            }
            this.InsertLink(ref strTemp, this.strLinkInsert);
            string newValue = this.strPicTempS.Replace("[QH:Title]", listastrPic[0][0]).Replace("[QH:PicPath]", this.PageUrlSet(listastrPic[0][1], strPageDepth));
            strTemp = strTemp.Replace("[QH:DetailPicture]", newValue);
        }

        private void ReplaceJianJie(ref string strTemp, string strPageDepth)
        {
            for (int i = 0; i < 100; i++)
            {
                string str2;
                string strLoop = str2 = "";
                this.GetLabel(ref strTemp, ref strLoop, ref str2, "[QH:JianJie ");
                if (strLoop == "")
                {
                    return;
                }
                string strColumn = this.ReadValue(str2, "Column");
                string strTitleNum = this.ReadValue(str2, "TitleNum");
                string newValue = this.GetJianJieBriefInfo(strColumn, strTitleNum, strPageDepth);
                strTemp = strTemp.Replace(strLoop, newValue);
            }
        }

        private void ReplaceLanmuLoopBig(ref string strTemp, string strPageDepth, string strColumnID)
        {
            string strLoop = "";
            string strLoopRule = "";
            string strLoopContent = "";
            for (int i = 0; i < 100; i++)
            {
                this.nBigLength = this.nMediumLength = this.nSmallLength = 0;
                this.strJQueryNum = "0";
                strLoop = strLoopRule = strLoopContent = "";
                this.GetLabel(ref strTemp, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:LanmuloopBig ");
                if (strLoop == "")
                {
                    return;
                }
                if ((i == 0) && (this.dTableClmn == null))
                {
                    this.dTableClmn = this.Bll1.GetDataTable(this.strClmnQuery);
                }
                this.LoopAttribute.JStype = this.ReadValue(strLoopRule, "NowClass");
                this.LoopAttribute.ProductFilter = this.ReadValue(strLoopRule, "sNowClass");
                this.LoopAttribute.Sort = this.ReadValue(strLoopRule, "ssNowClass");
                this.LoopAttribute.Condition = this.ReadValue(strLoopRule, "NumStart");
                this.LoopAttribute.Role = this.ReadValue(strLoopRule, "sNumStart");
                this.LoopAttribute.KeyWord = this.ReadValue(strLoopRule, "ssNumStart");
                if (!Regex.IsMatch(this.LoopAttribute.Condition, @"^\d+$"))
                {
                    this.LoopAttribute.Condition = "ID";
                }
                if (!Regex.IsMatch(this.LoopAttribute.Role, @"^\d+$"))
                {
                    this.LoopAttribute.Role = "ID";
                }
                if (!Regex.IsMatch(this.LoopAttribute.KeyWord, @"^\d+$"))
                {
                    this.LoopAttribute.KeyWord = "ID";
                }
                string strColumn = this.ReadValue(strLoopRule, "Column");
                string strModule = this.ReadValue(strLoopRule, "Module");
                string input = this.ReadValue(strLoopRule, "TitleNum");
                if (Regex.IsMatch(input, @"^\d+$"))
                {
                    this.nBigLength = int.Parse(input);
                }
                string newValue = this.GetLanmuLoopContent(strLoopContent, strPageDepth, strColumn, strModule, strColumnID);
                strTemp = strTemp.Replace("[QH:JQueryNum]", this.strJQueryNum).Replace(strLoop, newValue);
            }
        }

        private void ReplaceLoop(ref string strTemp, string strPageDepth, string strColumnID)
        {
            string strLoop = "";
            string strLoopRule = "";
            string strLoopContent = "";
            for (int i = 0; i < 100; i++)
            {
                strLoop = strLoopRule = strLoopContent = "";
                this.GetLabel(ref strTemp, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:loop ");
                if (strLoop == "")
                {
                    return;
                }
                this.GetLoopAttribute(strLoopRule);
                if (this.LoopAttribute.Module == "13")
                {
                    this.ReplaceBannerPic(ref strTemp, strPageDepth, strColumnID, strLoop, strLoopContent, strLoopRule);
                }
                else if (this.LoopAttribute.Module == "14")
                {
                    this.ReplaceFlash(ref strTemp, strPageDepth, strColumnID, strLoop, strLoopContent);
                }
                else if (this.LoopAttribute.Module == "16")
                {
                    this.ReplaceProductInterval(ref strTemp, strPageDepth, strColumnID, strLoop, strLoopContent);
                }
                else
                {
                    if (((this.LoopAttribute.Module.Length == 1) && (this.LoopAttribute.Module.IndexOfAny(new char[] { '3', '4', '5' }) != -1)) && (strLoopContent.IndexOf("CusName:") >= 0))
                    {
                        string iDForParameter = this.GetIDForParameter(this.LoopAttribute.Column);
                        this.listStrCusField = this.GetCusFieldInfo(strLoopContent, this.LoopAttribute.Module);
                        this.GetCusFieldInfoID_Name_pList(this.listStrCusField, this.LoopAttribute.Module, iDForParameter);
                    }
                    if (this.LoopAttribute.Module == "17")
                    {
                        this.LoopAttribute.ProductFilter = strLoopContent;
                    }
                    string queryingString = this.GetQueryingString();
                    DataTable loopDataTable = this.GetLoopDataTable(queryingString);
                    if (loopDataTable == null)
                    {
                        strTemp = strTemp.Replace(strLoop, "未能读取数据表。");
                    }
                    else if (loopDataTable.Rows.Count == 0)
                    {
                        strTemp = strTemp.Replace(strLoop, "");
                    }
                    else
                    {
                        string newValue = this.GetLoopContent(strLoopContent, loopDataTable, strPageDepth);
                        strTemp = strTemp.Replace(strLoop, newValue);
                    }
                }
            }
        }

        private void ReplaceMemberLand(ref string strTemp, string strPre)
        {
            strTemp = strTemp.Replace("[QH:ToLand]", "cdqh_Toland").Replace("[QH:IsLand]", "cdqh_Exitland").Replace("[QH:UserName]", "cdqh_MemberUser").Replace("[QH:Land]", strPre + "MemberManage/Landing.aspx").Replace("[QH:Regist]", strPre + "MemberManage/Member_Registration.aspx").Replace("[QH:ExitLand]", "javascript:ExitLand()").Replace("[QH:MemberCenter]", strPre + "MemberManage/Member_Account.aspx");
            this.InsertLink(ref strTemp, "<script type=\"text/javascript\" src=\"" + strPre + "MemberManage/js/Land.js\"></script>\n");
            this.InsertLink(ref strTemp, "<script type =\"text/javascript\" >init();</script>", "</body>");
        }

        private void ReplaceMultiLang(ref string strTemp, string strUrl, string strPageDepth)
        {
            if ((this.astr1[0x27] == "False") || (this.strLangDir == ""))
            {
                strTemp = Regex.Replace(strTemp, @"\[QH:LangLink:?.*?\]", "#");
                strTemp = Regex.Replace(strTemp, @"\[QH:LangVersion:?.*?\]", "");
                strTemp = Regex.Replace(strTemp, @"<Img[\s\S]+?\[QH:LangImg[\s\S]+?>", "", RegexOptions.IgnoreCase);
            }
            else
            {
                if (this.liststrLang == null)
                {
                    this.liststrLang = this.Bll1.DAL1.ReadDataReaderListStr("select Lang, Dir,IDMark ,ImgUrl from QH_MultiLLang", 4);
                }
                if (this.liststrLang.Count == 0)
                {
                    strTemp = Regex.Replace(strTemp, @"\[QH:LangLink:?.*?\]", "#");
                    strTemp = Regex.Replace(strTemp, @"\[QH:LangVersion:?.*?\]", "");
                    strTemp = Regex.Replace(strTemp, @"<Img[\s\S]+?\[QH:LangImg[\s\S]+?>", "", RegexOptions.IgnoreCase);
                }
                else
                {
                    string pagePre = this.GetPagePre(strPageDepth);
                    foreach (Match match in Regex.Matches(strTemp, @"\[QH:LangLink:?\s*([^\]]*)?\s*?\]"))
                    {
                        strTemp = strTemp.Replace(match.Groups[0].Value, this.GetLangLink(match.Groups[1].Value.Trim(), this.liststrLang, strUrl, pagePre));
                    }
                    foreach (Match match2 in Regex.Matches(strTemp, @"\[QH:LangVersion:?\s*([^\]]*)?\s*?\]"))
                    {
                        strTemp = strTemp.Replace(match2.Groups[0].Value, this.GetLangName(match2.Groups[1].Value.Trim(), this.liststrLang));
                    }
                    foreach (Match match3 in Regex.Matches(strTemp, @"\[QH:LangImg:?\s*([^\]]*)?\s*?\]"))
                    {
                        strTemp = strTemp.Replace(match3.Groups[0].Value, this.GetLangImg(match3.Groups[1].Value.Trim(), this.liststrLang, strPageDepth));
                    }
                }
            }
        }

        private void ReplaceMwssage(ref string strTemp, string strPageDepth, string strPage)
        {
            string strLoop = "";
            string strLoopRule = "";
            string strLoopContent = "";
            this.GetLabel(ref strTemp, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:GuestBook ");
            if ((strLoop != "") && (strLoopContent != ""))
            {
                string[] astrField = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                string[] strArray2 = new string[] { "[QH:GBName]", "[QH:GBTel]", "[QH:GBMobile]", "[QH:GBEmail]", "[QH:GBAdd]", "[QH:GBTitle]", "[QH:GBBak1]", "[QH:GBBak2]", "[QH:GBContent]", "[QH:GBVcode]" };
                string[] strArray3 = new string[] { "GBName", "GBTel", "GBMobile", "GBEmail", "GBAdd", "GBTitle", "GBBak1", "GBBak2", "GBContent", "[QH:GBVcode]" };
                string[] astrFieldID = new string[] { "GBNameID", "GBTelID", "GBMobileID", "GBEmailID", "GBAddID", "GBTitleID", "GBBak1ID", "GBBak2ID", "GBContentID", "GBVCodeID" };
                int num = 0;
                string str5 = "<input[^>]+{0}[^>]+>";
                string pattern = @"<textarea[^>]+GBContentID[\s\S]+?<\/textarea>";
                string[] strArray5 = new string[] { "*", "*", "*", "*", "*", "*", "*", "*", "*" };
                string format = "<span id=\"RFV{0}\" style=\"visibility:hidden;color:red;\">{1}</span>";
                for (int i = 0; i < 10; i++)
                {
                    Match match;
                    if (strLoopContent.IndexOf(strArray2[i]) <= 0)
                    {
                        continue;
                    }
                    num++;
                    switch (this.ReadValue(strLoopRule, strArray3[i]))
                    {
                        case "1":
                        case "1|":
                        case "1|0":
                            astrField[i] = "1";
                            break;

                        case "1|1":
                            astrField[i] = "2";
                            break;

                        case "0|1":
                            astrField[i] = "3";
                            break;

                        default:
                            astrField[i] = "4";
                            break;
                    }
                    strLoopContent = strLoopContent.Replace(strArray2[i], astrFieldID[i]);
                    if (i < 8)
                    {
                        match = Regex.Match(strLoopContent, str5.Replace("{0}", astrFieldID[i]), RegexOptions.IgnoreCase);
                        strLoopContent = strLoopContent.Insert(match.Index + match.ToString().Length, string.Format(format, astrFieldID[i], strArray5[i]));
                    }
                    else if (i == 8)
                    {
                        match = Regex.Match(strLoopContent, pattern, RegexOptions.IgnoreCase);
                        strLoopContent = strLoopContent.Insert(match.Index + match.ToString().Length, string.Format(format, astrFieldID[i], strArray5[i]));
                    }
                    else if (i == 9)
                    {
                        string pagePre = this.GetPagePre(strPageDepth);
                        if (this.bIsMobile)
                        {
                            pagePre = "../" + pagePre;
                        }
                        match = Regex.Match(strLoopContent, str5.Replace("{0}", astrFieldID[i]), RegexOptions.IgnoreCase);
                        strLoopContent = strLoopContent.Insert(match.Index + match.ToString().Length, " \n<A href=\"javascript:getimgcode()\" ><img src=\"" + pagePre + "Ajax/GuestBook/VerifyCodeMsg.aspx\" id=\"getcode\" style=\"border:1px #919a99 solid; width:52px; height:23px;\" alt=\"看不清，点击换一张\"  ></A>");
                        break;
                    }
                }
                if (num == 0)
                {
                    strTemp = strTemp.Replace(strLoop, "客户留言没有设置显示项或显示项id未设置。");
                }
                else
                {
                    int index = strLoopContent.IndexOf("[QH:GBSubmit]");
                    if (index < 0)
                    {
                        strTemp = strTemp.Replace(strLoop, "客户留言没有设置提交按钮。");
                    }
                    else
                    {
                        index += "[QH:GBSubmit]".Length;
                        if ((strLoopContent[index] == '\'') || (strLoopContent[index] == '"'))
                        {
                            index++;
                        }
                        strLoopContent = strLoopContent.Insert(index, " onclick=\"Click_submit()\" ");
                        index = strLoopContent.IndexOf("[QH:GBReset]");
                        if (index > 0)
                        {
                            index += "[QH:GBReset]".Length;
                            if ((strLoopContent[index] == '\'') || (strLoopContent[index] == '"'))
                            {
                                index++;
                            }
                            strLoopContent = strLoopContent.Insert(index, " onclick=\"Clear()\" ");
                        }
                        strLoopContent = strLoopContent.Replace("[QH:GBSubmit]", "cdqhGBSubmit").Replace("[QH:GBReset]", "cdqhGBReset");
                        strTemp = strTemp.Replace(strLoop, strLoopContent);
                        this.SetJS(ref strTemp, this.GetCustomGuestBookJsScript(strPageDepth, astrField, astrFieldID, strPage));
                    }
                }
            }
        }

        private void ReplaceNav(ref string strTemp, string strPageDepth)
        {
            string strLoop = "";
            string strLoopRule = "";
            string strLoopContent = "";
            this.GetLabel(ref strTemp, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:loopNavUp");
            if (strLoop != "")
            {
                string newValue = this.GetLoopNavContent(strLoopContent, 1, strPageDepth);
                strTemp = strTemp.Replace(strLoop, newValue);
            }
            strLoop = strLoopRule = strLoopContent = "";
            this.GetLabel(ref strTemp, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:loopNavDown");
            if (strLoop != "")
            {
                string str5 = this.GetLoopNavContent(strLoopContent, 2, strPageDepth);
                strTemp = strTemp.Replace(strLoop, str5);
            }
        }

        private void ReplaceNavLoopBig(ref string strTemp, string strPageDepth, string strColumnID)
        {
            string strLoop = "";
            string strLoopRule = "";
            string strLoopContent = "";
            for (int i = 0; i < 100; i++)
            {
                this.nBigLength = this.nMediumLength = this.nSmallLength = 0;
                this.strJQueryNum = "0";
                strLoop = strLoopRule = strLoopContent = "";
                this.GetLabel(ref strTemp, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:NavloopBig ");
                if (strLoop == "")
                {
                    return;
                }
                if ((i == 0) && (this.dTableClmn == null))
                {
                    this.dTableClmn = this.Bll1.GetDataTable(this.strClmnQuery);
                }
                string strColumn = this.ReadValue(strLoopRule, "Column");
                string input = this.ReadValue(strLoopRule, "NumStart");
                int nNumStart = 0;
                if (Regex.IsMatch(input, @"^\d+$"))
                {
                    nNumStart = int.Parse(input);
                }
                string strUpOrDown = this.ReadValue(strLoopRule, "Nav").ToUpper();
                strUpOrDown = (strUpOrDown == "") ? "UP" : strUpOrDown;
                strUpOrDown = (strUpOrDown == "UP") ? "1" : "2";
                string str7 = this.ReadValue(strLoopRule, "TitleNum");
                string strModule = "";
                if (Regex.IsMatch(str7, @"^\d+$"))
                {
                    this.nBigLength = int.Parse(str7);
                }
                string newValue = this.GetNavLoopContent(strLoopContent, strPageDepth, strColumn, strModule, strColumnID, strUpOrDown, nNumStart);
                strTemp = strTemp.Replace(strLoop, newValue);
            }
        }

        private void ReplaceNewsDetails(ref string strTemp, DataRow dRow, string strPageDepth)
        {
            string newValue = dRow["Title"].ToString();
            strTemp = strTemp.Replace("[QH:Title]", newValue);
            strTemp = strTemp.Replace("[QH:BriefIntro]", this.PageContentUrlSet(dRow["BriefIntro"].ToString(), strPageDepth));
            if (strTemp.Contains("[QH:BriefIntro"))
            {
                this.ReplaceContentsBrief(ref strTemp, dRow["BriefIntro"].ToString(), "BriefIntro");
            }
            strTemp = strTemp.Replace("[QH:NewsTags]", this.SetTagsLink(dRow["Tags"].ToString(), "NewsTags.html", "../"));
            strTemp = strTemp.Replace("[QH:Contents]", this.FilterHotLink(this.PageContentUrlSet(dRow["content"].ToString(), strPageDepth), strPageDepth));
            strTemp = strTemp.Replace("[QH:Author]", dRow["Author"].ToString());
            string str2 = dRow["hits"].ToString().Trim();
            strTemp = strTemp.Replace("[QH:Hits]", "<span id=\"detailHits\" >" + ((str2 == "") ? "0" : str2) + "</span>");
            string str3 = dRow["adddate"].ToString().Trim();
            if (str3 != "")
            {
                str3 = ((DateTime)dRow["adddate"]).ToString("yyyy-MM-dd");
            }
            strTemp = strTemp.Replace("[QH:Date]", str3);
            str3 = dRow["ModyDate"].ToString().Trim();
            if (str3 != "")
            {
                str3 = ((DateTime)dRow["ModyDate"]).ToString("yyyy-MM-dd");
            }
            strTemp = strTemp.Replace("[QH:MDate]", str3);
        }

        private string ReplacePagerLoop(ref string strTemp1)
        {
            strTemp1 = Regex.Replace(strTemp1, @"\[QH:PageLoop\][\s\S]*?\[\/QH:PageLoop\]", "");
            strTemp1 = Regex.Replace(strTemp1, @"<a[^>]+\[QH:PageFirstLink\][^>]*>([\s\S]*?)</a>", "");
            strTemp1 = Regex.Replace(strTemp1, @"<a[^>]+\[QH:PagePrevLink\][^>]*>([\s\S]*?)</a>", "");
            strTemp1 = Regex.Replace(strTemp1, @"<a[^>]+\[QH:PageNextLink\][^>]*>([\s\S]*?)</a>", "");
            strTemp1 = Regex.Replace(strTemp1, @"<a[^>]+\[QH:PageLastLink\][^>]*>([\s\S]*?)</a>", "");
            strTemp1 = Regex.Replace(strTemp1, @"<select[^>]+\[QH:SelPage\][^>]*>[\s\S]*?</select>", "");
            return "";
        }

        private void ReplacePicStyle(ref string strTemp, string strPageDepth, List<string[]> listastrPic)
        {
            if (strTemp.IndexOf("jquery") == -1)
            {
                this.InsertLink(ref strTemp, "<script type=\"text/javascript\" src=\"" + this.GetPagePre(strPageDepth) + "Ajax/jquery.js\"></script>\n");
            }
            this.InsertLink(ref strTemp, this.strLinkInsert);
            string strPicTempS = this.strPicTempS;
            string strLoop = "";
            string strLoopRule = "";
            string strLoopContent = "";
            this.GetLabel(ref strPicTempS, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:loop ");
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < listastrPic.Count; i++)
            {
                builder.Append(this.LoopReplaceContent(strLoopContent, listastrPic[i], strPageDepth));
            }
            strPicTempS = strPicTempS.Replace(strLoop, builder.ToString()).Replace("[QH:Title]", listastrPic[0][0]).Replace("[QH:PicPath]", this.PageUrlSet(listastrPic[0][1], strPageDepth));
            strTemp = strTemp.Replace("[QH:DetailPicture]", strPicTempS);
        }

        private void ReplacePicStyle2(ref string strTemp, string strPageDepth, List<string[]> listastrPic)
        {
            if (strTemp.IndexOf("jquery") == -1)
            {
                this.InsertLink(ref strTemp, "<script type=\"text/javascript\" src=\"" + this.GetPagePre(strPageDepth) + "Ajax/jquery.js\"></script>\n");
            }
            this.InsertLink(ref strTemp, this.strLinkInsert);
            if (this.strLinkInsertDown != "")
            {
                this.InsertLink(ref strTemp, this.strLinkInsertDown, "</body>");
            }
            string newValue = this.strPicTempS.Replace("[QH:Title]", listastrPic[0][0]).Replace("[QH:PicPath]", this.PageUrlSet(listastrPic[0][1], strPageDepth));
            strTemp = strTemp.Replace("[QH:DetailPicture]", newValue);
        }

        private void ReplaceProductDetails(ref string strTemp, DataRow dRow, string strPageDepth)
        {
            string pagePre = this.GetPagePre(strPageDepth);
            this.RepalceLoopCusField(ref strTemp, dRow["id"].ToString(), strPageDepth);
            strTemp = strTemp.Replace("[QH:PicPath]", this.PageUrlSet(dRow["ImageUrl"].ToString(), strPageDepth));
            strTemp = strTemp.Replace("[QH:ThumbPicPath]", this.PageUrlSet(dRow["ThumbUrl"].ToString(), strPageDepth));
            strTemp = strTemp.Replace("[QH:ProductID]", dRow["Product_id"].ToString());
            strTemp = strTemp.Replace("[QH:ID]", dRow["id"].ToString());
            strTemp = strTemp.Replace("[QH:ProductTitle]", dRow["title"].ToString());
            strTemp = strTemp.Replace("[QH:ProductPrice]", dRow["Price"].ToString());
            strTemp = strTemp.Replace("[QH:ProductMemo]", dRow["Memo1"].ToString());
            strTemp = strTemp.Replace("[QH:ProductSpecName]", dRow["SpecialName"].ToString());
            strTemp = strTemp.Replace("[QH:BriefIntro]", this.PageContentUrlSet(dRow["Memo1"].ToString(), strPageDepth));
            if (strTemp.Contains("[QH:BriefIntro"))
            {
                this.ReplaceContentsBrief(ref strTemp, dRow["Memo1"].ToString(), "BriefIntro");
            }
            strTemp = strTemp.Replace("[QH:Author]", dRow["Author"].ToString());
            string str2 = dRow["hits"].ToString().Trim();
            strTemp = strTemp.Replace("[QH:Hits]", "<span id=\"detailHits\" >" + ((str2 == "") ? "0" : str2) + "</span>");
            string newValue = dRow["Title"].ToString();
            strTemp = strTemp.Replace("[QH:Title]", newValue);
            strTemp = strTemp.Replace("[QH:ProductTags]", this.SetTagsLink(dRow["Key"].ToString(), "ProductTags.html", "../"));
            strTemp = strTemp.Replace("[QH:Contents]", this.FilterHotLink(this.PageContentUrlSet(dRow["content"].ToString(), strPageDepth), strPageDepth));
            string str4 = dRow["adddate"].ToString().Trim();
            if (str4 != "")
            {
                str4 = ((DateTime)dRow["adddate"]).ToString("yyyy-MM-dd");
            }
            strTemp = strTemp.Replace("[QH:Date]", str4);
            str4 = dRow["ModyDate"].ToString().Trim();
            if (str4 != "")
            {
                str4 = ((DateTime)dRow["ModyDate"]).ToString("yyyy-MM-dd");
            }
            strTemp = strTemp.Replace("[QH:MDate]", str4);
            strTemp = strTemp.Replace("[QH:ProductDownloadPath]", this.PageUrlSet("Ajax/ProductDownload/ProductDownload.aspx?id=" + dRow["id"].ToString(), strPageDepth));
            strTemp = strTemp.Replace("[QH:AddShoppingCart]", string.Concat(new object[] { "location='", pagePre, "MemberManage/Shopping_Cart.aspx?id=", dRow["id"], "'" }));
        }

        private void ReplaceProductInterval(ref string strTemp, string strPageDepth, string strColumnID, string strLoop, string strLoopContent)
        {
            List<string[]> list = this.Bll1.DAL1.ReadDataReaderListStr("select Start,[End],title from QH_PriceInterval order by CLNG([order]) asc ", 3);
            StringBuilder builder = new StringBuilder("");
            foreach (string[] strArray in list)
            {
                builder.Append(strLoopContent.Replace("[QH:Title]", strArray[2]).Replace("[QH:LinkPath]", this.PageUrlSet("ProductPrice/Price_" + strArray[0].Replace('.', 'd') + "_" + strArray[1].Replace('.', 'd') + ".html", strPageDepth)));
            }
            strTemp = strTemp.Replace(strLoop, builder.ToString());
        }

        private string ReplaceProductListSmallTitle(string strLoopContent, string strLoopContentSamll, string strLoopSmall, DataRow dRow, string strpagePre, string strPageDepth, string strColumnID, ref bool bBigNow)
        {
            string strUrlPre = "";
            DataRow[] rowArray = this.dTableClmn.Select("ParentID='" + dRow["id"] + "'");
            if (rowArray.Length == 0)
            {
                return Regex.Replace(strLoopContent.Replace(strLoopSmall, ""), @"<ul>[\s\n\r]*</ul>", "", RegexOptions.IgnoreCase);
            }
            if (rowArray.Length > 0)
            {
                strUrlPre = this.ColumnUrlPre(rowArray[0], this.dTableClmn);
            }
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < rowArray.Length; i++)
            {
                builder.Append(this.LoopReplaceSmallContent(strLoopContentSamll, rowArray[i], strpagePre, strUrlPre, strPageDepth, i, strColumnID, ref bBigNow));
            }
            return strLoopContent.Replace(strLoopSmall, builder.ToString());
        }

        private void ReplaceProductListTitle(ref string strTemp, string strPageDepth, string strColumnID)
        {
            string strLoop = "";
            string strLoopRule = "";
            string strLoopContent = "";
            for (int i = 0; i < 100; i++)
            {
                this.nBigLength = this.nSmallLength = 0;
                strLoop = strLoopRule = strLoopContent = "";
                this.strJQueryNum = "0";
                this.GetLabel(ref strTemp, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:loopProductListBig ");
                if (strLoop == "")
                {
                    return;
                }
                if ((i == 0) && (this.dTableClmn == null))
                {
                    this.dTableClmn = this.Bll1.GetDataTable(this.strClmnQuery);
                }
                this.LoopAttribute.JStype = this.ReadValue(strLoopRule, "NowClass");
                this.LoopAttribute.ProductFilter = this.ReadValue(strLoopRule, "sNowClass");
                this.LoopAttribute.Condition = this.ReadValue(strLoopRule, "NumStart");
                this.LoopAttribute.Role = this.ReadValue(strLoopRule, "sNumStart");
                if (!Regex.IsMatch(this.LoopAttribute.Condition, @"^\d+$"))
                {
                    this.LoopAttribute.Condition = "ID";
                }
                if (!Regex.IsMatch(this.LoopAttribute.Role, @"^\d+$"))
                {
                    this.LoopAttribute.Role = "ID";
                }
                this.GetLoopAttribute(strLoopRule, "Column");
                string str4 = this.ReadValue(strLoopRule, "Show1");
                string input = this.ReadValue(strLoopRule, "TitleNum");
                if (Regex.IsMatch(input, @"^\d+$"))
                {
                    this.nBigLength = int.Parse(input);
                }
                string newValue = this.GetLoopProductListContent(strLoopContent, strPageDepth, str4, strColumnID);
                if (this.strJQueryNum != "0")
                {
                    strTemp = strTemp.Replace("[QH:JQueryNum]", this.strJQueryNum).Replace(strLoop, newValue);
                }
                else
                {
                    strTemp = strTemp.Replace(strLoop, newValue);
                }
            }
        }

        private void ReplaceProductPicture(ref string strTemp, DataRow dRow, string strPageDepth)
        {
            this.astr1[0x15] = (this.astr1[0x15] == "") ? "0" : this.astr1[0x15];
            string[] strArray = this.astr1[0x15].Split(new char[] { '|' });
            Match match = Regex.Match(strTemp, @"\[QH:DetailPicture:?\s*(\d*)\s*\]");
            if (match.Groups[0].Value != "")
            {
                string s = match.Groups[1].Value;
                s = (s == "") ? "0" : s;
                if (strArray[0] != "3")
                {
                    List<string[]> picData = this.GetPicData(dRow, int.Parse(s));
                    if (picData.Count == 0)
                    {
                        strTemp = strTemp.Replace("[QH:DetailPicture]", "");
                    }
                    else
                    {
                        string str2 = strArray[0];
                        if (str2 != null)
                        {
                            if (!(str2 == "0") && !(str2 == "1"))
                            {
                                if (((str2 == "2") || (str2 == "4")) || (str2 == "5"))
                                {
                                    this.ReplacePicStyle2(ref strTemp, strPageDepth, picData);
                                }
                            }
                            else
                            {
                                this.ReplacePicStyle(ref strTemp, strPageDepth, picData);
                            }
                        }
                    }
                }
            }
        }

        private void ReplaceSearch(ref string strTemp1, string strPageDepth, string strSearchType)
        {
            for (int i = 0; i < 10; i++)
            {
                if (strTemp1.IndexOf("[QH:SearchOnclickNews") == -1)
                {
                    break;
                }
                this.ReplaceSearch1(ref strTemp1, strPageDepth, "News", "[QH:SearchOnclickNews");
            }
            for (int j = 0; j < 10; j++)
            {
                if (strTemp1.IndexOf("[QH:SearchOnclickProduct") == -1)
                {
                    break;
                }
                this.ReplaceSearch1(ref strTemp1, strPageDepth, "Product", "[QH:SearchOnclickProduct");
            }
            for (int k = 0; k < 10; k++)
            {
                if (strTemp1.IndexOf("[QH:SearchOnclick") == -1)
                {
                    return;
                }
                this.ReplaceSearch1(ref strTemp1, strPageDepth, "[QH:SearchOnclick");
            }
        }

        private void ReplaceSearch1(ref string strTemp1, string strPageDepth, string strOnclickTags)
        {
            string str = "NewsSearch.html";
            string pagePre = this.GetPagePre(strPageDepth);
            string strInputID = "CDQHSearch";
            bool bQuotes = false;
            string oldValue = this.GetSearchLabelAndIDAll(strTemp1, ref strInputID, ref bQuotes, strOnclickTags);
            string[] strArray = strInputID.Split(new char[] { '_' });
            string newValue = "";
            if (strArray.Length == 1)
            {
                newValue = "if(" + strArray[0] + ".value==''){alert('搜索词不能为空！');return;}location='" + pagePre + "TagsAndSearch/" + str + "?S='+encodeURIComponent(" + strArray[0] + ".value)";
            }
            else if (strArray.Length == 2)
            {
                string str6 = "location='" + pagePre + "TagsAndSearch/NewsSearch.html?S='+encodeURIComponent(" + strArray[0] + ".value);";
                string str7 = "location='" + pagePre + "TagsAndSearch/ProductSearch.html?S='+encodeURIComponent(" + strArray[0] + ".value);";
                newValue = "if(" + strArray[0] + ".value==''){alert('搜索词不能为空！');return;}if(" + strArray[1] + ".selectedIndex==0) " + str6 + " else " + str7;
            }
            if (!bQuotes)
            {
                newValue = "\"" + newValue + "\"";
            }
            strTemp1 = strTemp1.Replace(oldValue, newValue);
        }

        private void ReplaceSearch1(ref string strTemp1, string strPageDepth, string strSearchType, string strOnclickTags)
        {
            strSearchType = (strSearchType == "News") ? "NewsSearch.html" : "ProductSearch.html";
            string pagePre = this.GetPagePre(strPageDepth);
            string strInputID = "CDQHSearch";
            bool bQuotes = false;
            string oldValue = this.GetSearchLabelAndID(strTemp1, ref strInputID, ref bQuotes, strOnclickTags);
            string newValue = bQuotes ? ("if(" + strInputID + ".value==''){alert('搜索词不能为空！');return;}location='" + pagePre + "TagsAndSearch/" + strSearchType + "?S='+encodeURIComponent(" + strInputID + ".value)") : ("\"if(" + strInputID + ".value==''){alert('搜索词不能为空！');return;}location='" + pagePre + "TagsAndSearch/" + strSearchType + "?S='+encodeURIComponent(" + strInputID + ".value)\"");
            strTemp1 = strTemp1.Replace(oldValue, newValue);
        }

        private void ReplaceSearchResult(ref string strTemp1, string strPageDepth, string strSearchType, ref DataRow dw)
        {
            string pagePre = this.GetPagePre(strPageDepth);
            string strLoop = "";
            string strLoopRule = "";
            string strLoopContent = "";
            this.GetLabel(ref strTemp1, ref strLoop, ref strLoopRule, ref strLoopContent, "[QH:loopPage ");
            if ((strLoop != "") && (strLoopContent != ""))
            {
                string str;
                string input = strLoopContent.Replace(@"\", @"\\").Replace("\"", "\\\"").Replace("[QH:Title]", "{1}").Replace("[QH:Date]", "{3}").Replace("[QH:MDate]", "{3}").Replace("[QH:NewsTags]", "{4}").Replace("[QH:ProductTags]", "{4}");
                string str7 = "-1";
                string str8 = "-1";
                if (strSearchType == "News")
                {
                    str = "vHtmlNews";
                    input = input.Replace("[QH:NewsPath]", "../NewsDetails/NewsDetails_{0}.html").Replace("[QH:PicPath]", "{6}").Replace("[QH:ThumbPicPath]", "{6}");
                    Match match = Regex.Match(input, @"\[QH:BriefIntro:?\s*(\d*)\s*\]");
                    if (match.Groups[0].Value != "")
                    {
                        str7 = match.Groups[1].Value;
                        str7 = (str7 == "") ? "0" : str7;
                        input = input.Replace(match.Groups[0].Value, "{2}");
                    }
                    match = Regex.Match(input, @"\[QH:ContentsBrief:?\s*(\d*)\s*\]");
                    if (match.Groups[0].Value != "")
                    {
                        str8 = match.Groups[1].Value;
                        str8 = (str8 == "") ? "0" : str8;
                        input = input.Replace(match.Groups[0].Value, "{5}");
                    }
                }
                else
                {
                    str = "vHtmlProduct";
                    input = input.Replace("[QH:NewsPath]", "../ProductDetails/ProductDetails_{0}.html").Replace("[QH:PicPath]", "{2}").Replace("[QH:ThumbPicPath]", "{5}").Replace("[QH:ProductID]", "{6}").Replace("[QH:ProductPrice]", "{7}").Replace("[QH:ProductSpecName]", "{8}").Replace("[QH:ProductState]", "{910}").Replace("[QH:Author]", "{11}").Replace("[QH:ProductBrand]", "{12}");
                    Match match2 = Regex.Match(input, @"\[QH:BriefIntro:?\s*(\d*)\s*\]");
                    if (match2.Groups[0].Value != "")
                    {
                        str7 = match2.Groups[1].Value;
                        str7 = (str7 == "") ? "0" : str7;
                        input = input.Replace(match2.Groups[0].Value, "{13}");
                    }
                    match2 = Regex.Match(input, @"\[QH:ContentsBrief:?\s*(\d*)\s*\]");
                    if (match2.Groups[0].Value != "")
                    {
                        str8 = match2.Groups[1].Value;
                        str8 = (str8 == "") ? "0" : str8;
                        input = input.Replace(match2.Groups[0].Value, "{14}");
                    }
                }
                string str9 = this.ReadValue(strLoopRule, "TitleNum");
                str9 = (str9 == "") ? "0" : str9;
                string strInputID = "CDQHSearch";
                if (strSearchType == "News")
                {
                    this.GetSearchLabelAndID(strTemp1, ref strInputID, "[QH:SearchOnclickNews");
                }
                else
                {
                    this.GetSearchLabelAndID(strTemp1, ref strInputID, "[QH:SearchOnclickProduct");
                }
                if ((strInputID == "$$") || (strInputID == ""))
                {
                    this.GetSearchLabelAndID(strTemp1, ref strInputID, "[QH:SearchOnclick:");
                }
                if (strInputID == "")
                {
                    strInputID = "$$";
                }
                string strColumnIDSet = this.ReadValue(strLoopRule, "Column");
                dw = this.GetClumnDataRow(strColumnIDSet, strSearchType);
                string str12 = this.ReadValue(strLoopRule, "NewsCount");
                str12 = (str12 == "") ? dw["NumInPage"].ToString() : str12;
                if (!Regex.IsMatch(str12, @"^\d+$"))
                {
                    str12 = "20";
                }
                string newValue = "<span id=\"CDQHSearchPdtNewsResult\" ></span><script type=\"text/javascript\" >var vPre=\"" + pagePre + "\";var vTitleNum=" + str9 + ";var vInput=\"" + strInputID + "\";var vPageSize=" + str12 + ";var " + str + "=\"" + input.Replace("\r", @"\") + "\";\n var vCtnNum=\"" + str8 + "\"; var vBrfNum=\"" + str7 + "\";</script>\n";
                strTemp1 = strTemp1.Replace(strLoop, newValue);
                this.InsertBeforeEndBody(ref strTemp1, strPageDepth, "<script type=\"text/javascript\" src=\"" + pagePre + "Ajax/search/search.js\"></script>");
                int index = strTemp1.IndexOf("[QH:Pager");
                if (index != -1)
                {
                    string str15;
                    int num2 = strTemp1.IndexOf(']', index);
                    string oldValue = str15 = strTemp1.Substring(index, (num2 - index) + 1);
                    if (str15.Replace("[QH:Pager", "").Replace(":", "").Replace("]", "").Trim() == "2")
                    {
                        strTemp1 = strTemp1.Replace(oldValue, "<span id=\"TSPager2\"></span>");
                    }
                    else
                    {
                        strTemp1 = strTemp1.Replace(oldValue, "<span id=\"TSPager\"></span>");
                    }
                }
            }
        }

        private void ReplaceStatisticsJS(ref string strTemp, string strPageDepth)
        {
            string pagePre = this.GetPagePre(strPageDepth);
            Match match = new Regex("</body>", RegexOptions.IgnoreCase).Match(strTemp);
            if (match.Length == 7)
            {
                strTemp = strTemp.Insert(match.Index, "<script type=\"text/javascript\" >var vUrlPre=\"" + pagePre + "\";</script><script type =\"text/javascript\" src=\"" + pagePre + "Ajax/Statistics.js\" ></script>\n");
            }
        }

        private void ReplaceTitle(ref string strTemp, string strPageDepth, string strColumnID)
        {
            for (int i = 0; i < 100; i++)
            {
                string str2;
                string str3;
                string str4;
                string str5;
                string str6;
                string str7;
                string strLoop = str2 = str3 = str4 = str5 = str6 = "";
                this.GetLabel(ref strTemp, ref strLoop, ref str2, ref str3, "[QH:loopLanmu ");
                if (strLoop == "")
                {
                    return;
                }
                if ((i == 0) && (this.dTableClmn == null))
                {
                    this.dTableClmn = this.Bll1.GetDataTable(this.strClmnQuery);
                }
                this.GetLoopAttribute(str2, "Column");
                string strNowClass = this.ReadValue(str2, "NowClass");
                this.LoopAttribute.ProductFilter = this.ReadValue(str2, "sNowClass");
                this.LoopAttribute.Sort = this.ReadValue(str2, "ssNowClass");
                if ((this.LoopAttribute.NewsType != "1") && (this.LoopAttribute.NewsType != "2"))
                {
                    this.GetLabelTitle(str3, ref str4, ref str5, ref str6);
                }
                else
                {
                    str4 = str3;
                    str5 = str6 = "";
                }
                if (this.LoopAttribute.NewsType == "2")
                {
                    str7 = this.GetLoopTitleContent(str4, strPageDepth, strColumnID, strNowClass);
                    strTemp = strTemp.Replace(strLoop, str7);
                }
                else
                {
                    string[] loopLanmuIDMark = this.GetLoopLanmuIDMark(this.LoopAttribute.Column);
                    str7 = "";
                    foreach (string str9 in loopLanmuIDMark)
                    {
                        this.LoopAttribute.Column = str9;
                        str7 = str7 + this.GetLoopTitleContent(str4, str5, str6, strPageDepth, strColumnID, strNowClass);
                    }
                    strTemp = strTemp.Replace(strLoop, str7);
                }
            }
        }

        public int SetCopyRight(ref string strTemp)
        {
            string str = "\r\n\r\n<!--创都启航网站管理系统 Published Date: " + DateTime.Now.ToString("yyyy-M-d HH:mm:ss") + " Power by www.95c.com.cn-->";
            Match match = new Regex("</html>", RegexOptions.IgnoreCase).Match(strTemp);
            strTemp = strTemp.Insert(match.Index + 7, str);
            if (this.astr1[0x22] == "True")
            {
                this.InsertAntiLookOrDown(ref strTemp);
            }
            if (this.bIsAuthorized)
            {
                strTemp = strTemp.Replace("[QH:CDQHLink]", "");
                return 0;
            }
            int index = strTemp.IndexOf("[QH:CDQHLink]");
            if (index == -1)
            {
                return 1;
            }
            Match match2 = new Regex("<body>", RegexOptions.IgnoreCase).Match(strTemp);
            Match match3 = new Regex("</body>", RegexOptions.IgnoreCase).Match(strTemp);
            if ((index < match2.Index) || (index > match3.Index))
            {
                return 2;
            }
            int startIndex = 0;
            for (int i = 0; i < 100; i++)
            {
                startIndex = strTemp.IndexOf("<!--", startIndex);
                if (startIndex == -1)
                {
                    break;
                }
                int num3 = strTemp.IndexOf("-->", startIndex);
                if (num3 == -1)
                {
                    break;
                }
                if ((index > startIndex) && (index < num3))
                {
                    return 3;
                }
                startIndex = num3;
            }
            string str2 = "-创都启航企业网站管理系统";
            string newValue = " Powered by <a href=\"http://www.95c.com.cn\" target=\"_blank\" >创都启航</a>";
            Match match4 = new Regex("</title>", RegexOptions.IgnoreCase).Match(strTemp);
            if (match4.Length == 0)
            {
                return 4;
            }
            strTemp = strTemp.Insert(match4.Index, str2);
            strTemp = strTemp.Replace("[QH:CDQHLink]", newValue);
            return 0;
        }

        private void SetImagePicLinkTM(string strPageDepth)
        {
            string str = "";
            string str2 = "";
            this.astr1[0x16] = (this.astr1[0x16] == "") ? "0" : this.astr1[0x16];
            string[] strArray = this.astr1[0x16].Split(new char[] { '|' });
            if (strArray.Length == 3)
            {
                str = strArray[1];
                str2 = strArray[2];
            }
            str = (str == "") ? "300" : str;
            str2 = (str2 == "") ? "300" : str2;
            string pagePre = this.GetPagePre(strPageDepth);
            string str4 = strArray[0];
            if (str4 != null)
            {
                if (!(str4 == "0"))
                {
                    if (!(str4 == "1"))
                    {
                        if (!(str4 == "2"))
                        {
                            return;
                        }
                        return;
                    }
                }
                else
                {
                    this.strPicTempS = " <DIV id=picarea style=\"text-align:center\">\n<DIV style=\"MARGIN: 0px auto; WIDTH: 600px; HEIGHT: 401px; OVERFLOW: hidden\">\n<DIV style=\"MARGIN: 0px auto; WIDTH: 600px; HEIGHT: 401px; OVERFLOW: hidden\" id=bigpicarea>\n<P class=bigbtnPrev><SPAN id=big_play_prev></SPAN></P>\n[QH:loop Module=]\n<DIV id=image_x[QH:Number] class=image><A href=\"[QH:PicPath]\" target=_blank><IMG title=\"[QH:Title]\"\n src=\"[QH:PicPath]\"  onload='javascript:DrawImage(this);' width=\"" + str + "\" height=\"" + str2 + "\" ></A> \n<DIV class=word></DIV></DIV>[/QH:loop]\n<P class=bigbtnNext><SPAN id=big_play_next></SPAN></P></DIV>\n</DIV>\n<DIV id=smallpicarea>\n<DIV id=thumbs>\n<UL>\n  <LI class=\"first btnPrev\"><IMG id=play_prev src=\"../Ajax/left.png\"></LI>\n[QH:loop Module=]\n  <LI class=slideshowItem>\n  <A id=thumb_x[QH:Number] href=\"#\"><IMG src=\"[QH:PicPath]\" width=\"80\" height=\"60\" alt=\"[QH:Title]\" ></A>\n  </LI>[/QH:loop]\n  <LI class=\"last btnNext\"><IMG id=play_next src=\"../Ajax/right.png\"></LI>\n</UL></DIV></DIV></DIV>\n<SCRIPT>\nvar target = [{0}];\n</SCRIPT>\n<input type=\"hidden\" name=\"HdnNum\" id=\"HdnNum\" value=\"{1}\" />";
                    this.strLinkInsert = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + pagePre + "Ajax/css2.css\"/>\n<!--[if IE 6]><script type=\"text/javascript\" src=\"" + pagePre + "Ajax/ie6bj.js\"></script><![endif]--><script type =\"text/javascript\" >\nvar flag=false;\n function DrawImage(ImgD){\n  var image=new Image();\n image.src=ImgD.src;\n if(image.width>0 && image.height>0){\n  flag=true;\n  if(image.width/image.height>= 600/400){\n    if(image.width>600){\n    ImgD.width=600;\n     ImgD.height=(image.height*600)/image.width;\n   }else{\n     ImgD.width=image.width;\n    ImgD.height=image.height;\n    }\n    ImgD.alt=\".\";\n   }\n   else{\n    if(image.height>400){\n    ImgD.height=400;\n     ImgD.width=(image.width*400)/image.height;\n    }else{\n     ImgD.width=image.width;\n    ImgD.height=image.height;\n    }\n    ImgD.alt=\"\";\n   }\n  }\n if(ImgD.width>600)\n  {\n    ImgD.width=600; \n    ImgD.height=(image.height*400)/image.width;\n  }\n   if(ImgD.height>400){\n    ImgD.height=400; \n    ImgD.width=(image.width*600)/image.height;\n   }\n }\n$(document).ready(function(){$('#picarea').parent().css('height','600px').css('width','100%');});\n</script>\n";
                    return;
                }
                this.strPicTempS = "    <a href='[QH:PicPath]' target =_blank >\n        <img src=\"[QH:PicPath]\" alt='' title=\"[QH:Title]\" width=\"" + str + "\" height=\"" + str2 + "\"  id=\"PdtImgDtl\"  onload='javascript:DrawImage(this);'/>\n    </a>\n";
                this.strLinkInsert = "<script language=\"JavaScript\"> \n<!-- \nvar flag=false; \nfunction DrawImage(ImgD){\n var vWidth=ImgD.width;\n var vHeight=ImgD.height; \n var image=new Image(); \n image.src=ImgD.src; \n if(image.width>0 && image.height>0){ \n  flag=true; \n  if(image.width/image.height>= vWidth/vHeight){ \n   if(image.width>vWidth){\n    ImgD.width=vWidth; \n    ImgD.height=(image.height*vHeight)/image.width;\n   }else{ \n    ImgD.width=image.width;\n    ImgD.height=image.height; \n   } \n   ImgD.alt=\"\"; \n  } \n  else{ \n   if(image.height>vHeight){\n    ImgD.height=vHeight; \n    ImgD.width=(image.width*vWidth)/image.height; \n   }else{ \n    ImgD.width=image.width;\n    ImgD.height=image.height; \n   } \n   ImgD.alt=\"\"; \n  } \n }\n if(ImgD.width>vWidth)\n {\n    ImgD.width=vWidth; \n    ImgD.height=(image.height*vHeight)/image.width;\n }\n   if(ImgD.height>vHeight){\n    ImgD.height=vHeight; \n    ImgD.width=(image.width*vWidth)/image.height; \n   }\n}\n//--> \n</script> \n<script type=\"text/javascript\">$(document).ready(function(){$('#PdtImgDtl').parent().parent().css('height','" + str2 + "px');});\n</script>\n";
            }
        }

        private void SetJS(ref string strTemp, string strJs)
        {
            Match match = new Regex("<body", RegexOptions.IgnoreCase).Match(strTemp);
            if (match.Length == 5)
            {
                strTemp = strTemp.Insert(match.Index, strJs);
            }
        }

        private void SetLanguageIDMark(string strUrl)
        {
            if (this.astr1[0x27] != "False")
            {
                this.liststrLang = this.Bll1.DAL1.ReadDataReaderListStr("select Lang, Dir,IDMark ,ImgUrl from QH_MultiLLang", 4);
                if (this.liststrLang.Count != 0)
                {
                    string str = strUrl.Substring(strUrl.IndexOf(":") + 3);
                    MatchCollection matchs = Regex.Matches(str.Substring(str.IndexOf('/')), @"(\w+)/");
                    string str2 = "/";
                    if (matchs.Count > 1)
                    {
                        str2 = matchs[matchs.Count - 2].Groups[1].Value.ToLower();
                    }
                    foreach (string[] strArray in this.liststrLang)
                    {
                        if (strArray[1].ToLower() == str2)
                        {
                            this.strLangDir = strArray[1];
                            break;
                        }
                    }
                    if ((this.strLangDir == "") && (matchs.Count > 1))
                    {
                        str2 = "/";
                        foreach (string[] strArray2 in this.liststrLang)
                        {
                            if (strArray2[1].ToLower() == str2)
                            {
                                this.strLangDir = strArray2[1];
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void SetPicLinkTM(string strPageDepth)
        {
            string str = "";
            string str2 = "";
            this.astr1[0x15] = (this.astr1[0x15] == "") ? "0" : this.astr1[0x15];
            string[] strArray = this.astr1[0x15].Split(new char[] { '|' });
            if (strArray.Length == 3)
            {
                str = strArray[1];
                str2 = strArray[2];
            }
            str = (str == "") ? "300" : str;
            str2 = (str2 == "") ? "300" : str2;
            string pagePre = this.GetPagePre(strPageDepth);
            string str4 = strArray[0];
            if (str4 != null)
            {
                if (!(str4 == "0"))
                {
                    if (!(str4 == "4"))
                    {
                        if (!(str4 == "1"))
                        {
                            if (!(str4 == "2"))
                            {
                                if (str4 == "5")
                                {
                                    this.strPicTempS = "   <a href=\"[QH:PicPath]\" rel=\"lightbox\"><img src=\"[QH:PicPath]\" width =\"" + str + "px\" height =\"" + str2 + "px\"   id=\"PdtImgDtl\" /></a>\n";
                                    this.strLinkInsert = "<script type=\"text/javascript\">jQuery(document).ready(function(){jQuery('#PdtImgDtl').parent().parent().css('height','" + str2 + "px');});\n</script>\n";
                                    this.strLinkInsertDown = "<script type=\"text/javascript\" src=\"" + pagePre + "Ajax/ProductPicEff/Eff5/prototype.js\"></script>\n<script type=\"text/javascript\" src=\"" + pagePre + "Ajax/ProductPicEff/Eff5/effects.js\"></script>\n<script type=\"text/javascript\" src=\"" + pagePre + "Ajax/ProductPicEff/Eff5/lightbox.js\"></script>\n<link rel=\"stylesheet\" type=\"text/css\" href=\"" + pagePre + "Ajax/ProductPicEff/Eff5/lightbox.css\">\n";
                                }
                                return;
                            }
                            this.strPicTempS = "    <a href='[QH:PicPath]' target =_blank >\n        <img src=\"[QH:PicPath]\" alt='' title=\"[QH:Title]\" width=\"" + str + "\" height=\"" + str2 + "\"  id=\"PdtImgDtl\" />\n    </a>\n";
                            this.strLinkInsert = "<script type=\"text/javascript\">$(document).ready(function(){$('#PdtImgDtl').parent().parent().css('height','" + str2 + "px');});\n</script>\n";
                            return;
                        }
                        this.strPicTempS = "<div style=\"text-align: left;\">\n    <a href='[QH:PicPath]'  id='zoom1'  target =_blank >\n        <img src=\"[QH:PicPath]\" alt='' title=\"[QH:Title]\" width=\"" + str + "\" height=\"" + str2 + "\" />\n    </a>\n    <div id=\"c9\" class=\"mainc\">\n        <span id=\"span_left\"></span>\n        <div class=\"ul_box\">\n            <ul class=\"show\" id=\"ul_one\">\n               [QH:loop Module=]\n                <li><a href='[QH:PicPath]' class='cloud-zoom-gallery' title='[QH:Title]'\n                    rel=\"useZoom: 'zoom1', smallImage: '[QH:PicPath]' \">\n                    <img src=\"[QH:PicPath]\" alt=\"[QH:Title]\"  width=\"50\" height=\"50\" /></a></li>[/QH:loop]\n            </ul>\n        </div>\n        <span id=\"span_right\"></span>\n    </div>\n</div>\n";
                        this.strLinkInsert = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + pagePre + "Ajax/cldzoom.css\"/>\n<script type=\"text/javascript\" src=\"" + pagePre + "Ajax/function.js\"></script>\n<script type =\"text/javascript\" >\n$(document).ready(function () {\n    $('.cloud-zoom-gallery').CloudZoom();\n});    $.fn.CloudZoom = function (options) {\n    // IE6 background image flicker fix\n    try {\n        document.execCommand(\"BackgroundImageCache\", false, true);\n    } catch (e) {}\n    this.each(function () {\n\t\teval('var\ta = {' + $(this).attr('rel') + '}');\n        if ($(this).is('.cloud-zoom-gallery')) {\n            $(this).unbind('click');\n            $(this).bind('click', $(this), function (event) {\n                $('#' + a.useZoom).attr('href', event.data.attr('href'));\n                $('#' + a.useZoom + ' img').attr('src', a.smallImage).attr('title', $(this).attr('title'));\n            return false;\n            });\n        }\n    });\n};\n</script>\n";
                        return;
                    }
                }
                else
                {
                    this.strPicTempS = "<div style=\"text-align: left;\">\n    <a href='[QH:PicPath]' class='cloud-zoom' id='zoom1' rel=\"adjustX: 4, adjustY:0\">\n        <img src=\"[QH:PicPath]\" alt='' title=\"[QH:Title]\" width=\"" + str + "\" height=\"" + str2 + "\" />\n    </a>\n    <div id=\"c9\" class=\"mainc\">\n        <span id=\"span_left\"></span>\n        <div class=\"ul_box\">\n            <ul class=\"show\" id=\"ul_one\">\n               [QH:loop Module=]\n                <li><a href='[QH:PicPath]' class='cloud-zoom-gallery' title='[QH:Title]'\n                    rel=\"useZoom: 'zoom1', smallImage: '[QH:PicPath]' \">\n                    <img src=\"[QH:PicPath]\" alt=\"[QH:Title]\"  width=\"50\" height=\"50\" /></a></li>[/QH:loop]\n            </ul>\n        </div>\n        <span id=\"span_right\"></span>\n    </div>\n</div>\n";
                    this.strLinkInsert = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + pagePre + "Ajax/cldzoom.css\"/>\n<script type=\"text/JavaScript\" src=\"" + pagePre + "Ajax/cldzoom.1.0.2.min.js\"></script>\n<script type=\"text/javascript\" src=\"" + pagePre + "Ajax/function.js\"></script>\n";
                    return;
                }
                this.strPicTempS = "<div style=\"text-align: left;\">\n    <a href='[QH:PicPath]' class='cloud-zoom' id='zoom1' rel=\"adjustX: 4, adjustY:0\">\n        <img src=\"[QH:PicPath]\" alt='' title=\"[QH:Title]\" width=\"" + str + "\" height=\"" + str2 + "\" id=\"PdtImgDtl\" />\n    </a>\n</div>\n";
                this.strLinkInsert = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + pagePre + "Ajax/cldzoom.css\"/>\n<script type=\"text/JavaScript\" src=\"" + pagePre + "Ajax/cldzoom.1.0.2.min.js\"></script>\n<script type=\"text/javascript\" src=\"" + pagePre + "Ajax/function.js\"></script>\n<script type=\"text/javascript\">$(document).ready(function(){$('#PdtImgDtl').parent().parent().parent().parent().css('height','" + str2 + "px');});\n</script>\n";
            }
        }

        private void SetSEO(ref string strSTitle, ref string strSDesc, ref string strSKWord, DataRow DRow)
        {
            string str = DRow["ctitle"].ToString().Trim();
            if (str != string.Empty)
            {
                strSTitle = str;
            }
            str = DRow["Description"].ToString().Trim();
            if (str != string.Empty)
            {
                strSDesc = str;
            }
            str = DRow["KeyW"].ToString().Trim();
            if (str != string.Empty)
            {
                strSKWord = str;
            }
        }

        private void SetSEO(ref string strSTitle, ref string strSDesc, ref string strSKWord, DataRow DRow, string strDetail)
        {
            string str = DRow["ctitle"].ToString().Trim();
            if (str != string.Empty)
            {
                strSTitle = str;
            }
            else
            {
                strSTitle = DRow["Title"].ToString().Trim();
            }
            str = DRow["Description"].ToString().Trim();
            if (str != string.Empty)
            {
                strSDesc = str;
            }
            str = DRow["KeyW"].ToString().Trim();
            if (str != string.Empty)
            {
                strSKWord = str;
            }
        }

        private string SetTagsLink(string strTags, string strPage, string strUrlPre)
        {
            if (strTags.Trim() == "")
            {
                return "";
            }
            if (!File.Exists(this.CSPage.Server.MapPath("../TagsAndSearch/" + strPage)))
            {
                return strTags;
            }
            string str = "";
            foreach (string str2 in strTags.Split(new char[] { ',' }))
            {
                string str3 = str;
                str = str3 + "<a href='" + strUrlPre + "TagsAndSearch/" + strPage + "?S=" + this.CSPage.Server.UrlEncode(str2) + "' >" + str2 + "</a>&nbsp;";
            }
            return str;
        }

        private string TempReplace(Match m)
        {
            return m.Groups[0].Value.Replace(m.Groups[1].Value, "$$@@&&");
        }

        private class clLoopAttribute
        {
            public string AddStr;
            public string Column;
            public string Condition;
            public string JStype;
            public string KeyWord;
            public string Module;
            public string NewsCount;
            public string NewsDate;
            public string NewsType;
            public string Order;
            public string ProductFilter;
            public string Role;
            public string Sort;
            public string TitleNum;
        }
    }
}