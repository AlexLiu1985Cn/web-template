namespace CmsApp.CmsBack
{
    using _BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using _commen;

    public class Column : Page
    {
        protected Button BDel;
        private BLL Bll1 = new BLL();
        protected Button BSave1;
        protected Button BSaveA;
        protected Button BSaveN;
        private DataTable dt;
        protected HtmlForm form1;
        protected HiddenField HdDel;
        protected HiddenField HdSave;
        protected HiddenField HdSaveID;
        protected HiddenField HdSaveN;
        private int nRNum;
        private StringBuilder sbTRows = new StringBuilder();
        private string strADD;
        protected string strAlertJS = "";
        protected string strClass1NumInfo = "";
        protected string strColumn;
        protected string strMaxIDMark;
        protected string strMsg = "0";
        private string[] strSel;
        protected string strZhP;

        protected void BDel_Click(object sender, EventArgs e)
        {
            string[] strArray = this.HdDel.Value.Trim().Split(new char[] { '|' });
            if (strArray.Length == 6)
            {
                this.DeleteColumn(strArray);
                if (strArray[0] == "0")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "refresh", "<script>window .parent .frames['menu-frame'].location='Left_ContentNew.aspx';</script>");
                }
                this.DataToBind();
                string str = "";
                if (strArray[0] == "0")
                {
                    str = strArray[5];
                }
                else if (strArray[0] == "1")
                {
                    str = this.dt.Select("id=" + strArray[2])[0]["folder"].ToString() + "/" + strArray[5];
                }
                else
                {
                    DataRow[] rowArray = this.dt.Select("id=" + strArray[2]);
                    DataRow[] rowArray2 = this.dt.Select("id=" + rowArray[0]["ParentID"]);
                    str = rowArray2[0]["folder"].ToString() + "/" + rowArray[0]["folder"].ToString() + "/" + strArray[5];
                }
                if (Directory.Exists(base.Server.MapPath("../" + str)))
                {
                    Directory.Delete(base.Server.MapPath("../" + str), true);
                }
            }
        }

        protected void BSave_Click(object sender, EventArgs e)
        {
            string str = (base.Request.Form["CkN"] == null) ? "" : base.Request.Form["CkN"].ToString();
            int num = 0;
            if (str == "on")
            {
                num = this.SaveNewColumn();
                switch (num)
                {
                    case 2:
                        this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('新添栏目在同级栏目目录名称不能相同！');</script>";
                        this.DataToBind();
                        return;

                    case 3:
                        this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('其它栏目已有此新添栏目标识，栏目标识不能相同，标识0除外！');</script>";
                        this.DataToBind();
                        return;

                    case 4:
                        this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('不能使用系统目录" + base.Request.Form["TDir"] + "！');</script>";
                        this.DataToBind();
                        return;
                }
            }
            string[] strArray = this.HdSaveID.Value.Trim().Split(new char[] { '|' });
            List<List<string>> listData = new List<List<string>>();
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i < strArray.Length; i++)
            {
                string str2 = (base.Request.Form["Ck" + i] == null) ? "" : base.Request.Form["Ck" + i].ToString();
                if (str2 == "on")
                {
                    List<string> list3;
                    builder.Append("," + strArray[i]);
                    string str3 = base.Request.Form["IDM" + i];
                    string str4 = base.Request.Form["TOr" + i];
                    string str5 = base.Request.Form["TNm" + i];
                    string str6 = base.Request.Form["Sel" + i];
                    if (((str3 == null) || (str4 == null)) || ((str5 == null) || (str6 == null)))
                    {
                        return;
                    }
                    if (str3 != "0")
                    {
                        foreach (string str7 in this.Bll1.DAL1.ReadDataReaderAL("select IDMark from QH_Column where IDMark<>'0' and id<>" + strArray[i]))
                        {
                            if (str7 == str3)
                            {
                                this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('栏目标识不能相同，标识0除外！');</script>";
                                this.DataToBind();
                                return;
                            }
                        }
                    }
                    list3 = new List<string>(new string[] { strArray[i], str5, str4, str3, str6 })
                    {
                        //list3
                    };
                }
            }
            if (builder.Length != 0)
            {
                string strSelect = "select id,ColumnName,order1,IDMark,Nav from QH_Column where id in (" + builder.ToString().Substring(1) + ")";
                string strUpdate = "UPDATE QH_Column SET ColumnName=@ColumnName,order1=@order1 ,IDMark=@IDMark,Nav=@Nav WHERE ID=@ID";
                string[] astrField = new string[] { "ColumnName", "order1", "IDMark", "Nav" };
                if (this.Bll1.UpdateSelAll(strSelect, strUpdate, astrField, listData) == listData.Count)
                {
                    this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('修改成功！');window .parent .frames['menu-frame'].location='Left_ContentNew.aspx';</script>";
                }
                else
                {
                    this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('修改失败！');</script>";
                }
                this.DataToBind();
            }
            else
            {
                this.DataToBind();
                if (num == 1)
                {
                    this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('修改成功！');</script>";
                }
            }
        }

        protected void BSave1_Click(object sender, EventArgs e)
        {
            string[] strArray = this.HdSave.Value.Trim().Split(new char[] { '|' });
            if (strArray.Length == 2)
            {
                string strIDMark = base.Request.Form["IDM" + strArray[0]];
                string strOrder = base.Request.Form["TOr" + strArray[0]];
                string strName = base.Request.Form["TNm" + strArray[0]];
                string strSel = base.Request.Form["Sel" + strArray[0]];
                if (((strIDMark != null) && (strOrder != null)) && ((strName != null) && (strSel != null)))
                {
                    if (strIDMark != "0")
                    {
                        foreach (string str5 in this.Bll1.DAL1.ReadDataReaderAL("select IDMark from QH_Column where IDMark<>'0' and id<>" + strArray[1]))
                        {
                            if (str5 == strIDMark)
                            {
                                this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('其它栏目已有此标识，栏目标识不能相同，标识0除外！');</script>";
                                this.DataToBind();
                                return;
                            }
                        }
                    }
                    if (this.UpdateColumn(strArray[1], strName, strOrder, strSel, strIDMark) > 0)
                    {
                        this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('修改成功！');window .parent .frames['menu-frame'].location='Left_ContentNew.aspx';</script>";
                    }
                    else
                    {
                        this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('修改失败！');</script>";
                    }
                    this.DataToBind();
                }
            }
        }

        protected void BSaveN_Click(object sender, EventArgs e)
        {
            switch (this.SaveNewColumn())
            {
                case 1:
                    this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('添加成功！');</script>";
                    break;

                case 2:
                    this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('同级栏目目录名称不能相同！');</script>";
                    break;

                case 3:
                    this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('其它栏目已有此标识，栏目标识不能相同，标识0除外！');</script>";
                    break;

                case 4:
                    this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('不能使用系统目录" + base.Request.Form["TDir"] + "！');</script>";
                    break;

                default:
                    this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('添加失败！');</script>";
                    break;
            }
            this.DataToBind();
        }

        private void DataToBind()
        {
            this.HdSaveID.Value = "";
            this.dt = this.Bll1.GetDataTable("select id, ColumnName,ParentID,depth,order1,childNum,IDMark,classlist,Nav,[Module],folder from QH_Column order by CLNG(ParentID) asc,CLNG(depth) asc, CLNG(Order1) asc,id asc");
            if (this.dt != null)
            {
                this.strMsg = Convert.ToString(this.dt.Compute("Count(IDMark)", "[Module]='6'"));
                this.strZhP = Convert.ToString(this.dt.Compute("Count(IDMark)", "[Module]='8'"));
                if (this.dt.Rows.Count > 0)
                {
                    this.strMaxIDMark = this.GetClmnIDMarkMax(ref this.strClass1NumInfo);
                }
                else
                {
                    this.strClass1NumInfo = "'[0,0,0,0,0,0,0,0]'";
                    this.strMaxIDMark = "1";
                }
                this.DisplayColumns(this.dt, "0", 0);
                this.strColumn = this.sbTRows.ToString();
            }
        }

        public int DeleteColumn(string[] astr1)
        {
            if (astr1[0] == "2")
            {
                this.DelUpdateChNum(this.DelUpdateChNum(astr1[2]));
            }
            else if (astr1[0] == "1")
            {
                this.DelUpdateChNum(astr1[2]);
            }
            if (astr1[4] == "3")
            {
                string iNString = this.Bll1.DAL1.GetINString("select id from QH_Product where ColumnID='" + astr1[1] + "'");
                if (iNString != "")
                {
                    this.Bll1.DAL1.ExecuteNonQuery("delete from QH_pList where listid in(" + iNString + ")");
                }
                this.Bll1.DAL1.ExecuteNonQuery("delete from QH_Product where ColumnID='" + astr1[1] + "'");
                string str2 = this.Bll1.DAL1.GetINString("select id from QH_Parameter where columnID='" + astr1[1] + "' and [Module]='3'");
                if (str2 != "")
                {
                    this.Bll1.DAL1.ExecuteNonQuery("delete from QH_List where BigID in(" + str2 + ")");
                }
                this.Bll1.DAL1.ExecuteNonQuery("delete from QH_Parameter where columnID='" + astr1[1] + "' and [module]='3'");
            }
            if (astr1[4] == "2")
            {
                this.Bll1.DAL1.ExecuteNonQuery("delete from QH_News where ColumnID='" + astr1[1] + "'");
            }
            return this.Bll1.DAL1.ExecuteNonQuery("delete from QH_Column where id=" + astr1[1]);
        }

        private string DelUpdateChNum(string strID)
        {
            string[] strValue = new string[] { "", "" };
            string[] strField = new string[] { "ParentID", "childNum" };
            string strQuery = "select ParentID, childNum from QH_Column where id=" + strID;
            try
            {
                this.Bll1.DAL1.ReadDataReader(strQuery, ref strValue, strField);
                int num = int.Parse(strValue[1]) - 1;
                num = (num < 0) ? 0 : num;
                this.Bll1.DAL1.ExecuteNonQuery(string.Concat(new object[] { "UPDATE QH_Column SET childNum='", num, "' WHERE ID=", strID }));
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("删除栏目更新数据库错误： " + exception.ToString());
            }
            return strValue[0];
        }

        private void DisplayColumns(DataTable dt, string pid, int blank)
        {
            string str = " ";
            DataView view = new DataView(dt)
            {
                RowFilter = "ParentID = '" + pid + "'"
            };
            string str2 = "";
            string strColor = "#c3c3c3";
            if (blank > 0)
            {
                if (blank == 1)
                {
                    str2 = "&nbsp;&nbsp;";
                    strColor = "#e6e6e6";
                }
                for (int i = 2; i <= blank; i++)
                {
                    str2 = str2 + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    strColor = "#f9f9f9";
                }
            }
            int num2 = 0;
            foreach (DataRowView view2 in view)
            {
                string str4 = view2["ID"].ToString();
                string s = view2["Depth"].ToString();
                num2++;
                if (blank > 0)
                {
                    if (view.Count != num2)
                    {
                        str = str2 + "├";
                    }
                    else
                    {
                        str = str2 + "└";
                    }
                }
                this.SetColumsRow(str, view2, strColor);
                int num3 = int.Parse(s) + 1;
                this.DisplayColumns(dt, str4, num3);
            }
        }

        private string GetClmnIDMarkMax(ref string strClass1NumInfo)
        {
            string str = Convert.ToString(this.dt.Compute("Count(IDMark)", "depth='0' and [module]='1'"));
            string str2 = Convert.ToString(this.dt.Compute("Count(IDMark)", "depth='0' and [module]='2'"));
            string str3 = Convert.ToString(this.dt.Compute("Count(IDMark)", "depth='0' and [module]='3'"));
            string str4 = Convert.ToString(this.dt.Compute("Count(IDMark)", "depth='0' and [module]='4'"));
            string str5 = Convert.ToString(this.dt.Compute("Count(IDMark)", "depth='0' and [module]='5'"));
            string str6 = Convert.ToString(this.dt.Compute("Count(IDMark)", "depth='0' and [module]='6'"));
            string str7 = Convert.ToString(this.dt.Compute("Count(IDMark)", "depth='0' and [module]='7'"));
            string str8 = Convert.ToString(this.dt.Compute("Count(IDMark)", "depth='0' and [module]='8'"));
            strClass1NumInfo = "'[" + str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6 + "," + str7 + "," + str8 + "]'";
            DataColumn column = new DataColumn("IDMarkInt")
            {
                DataType = Type.GetType("System.Int32"),
                Expression = "Convert(IDMark, 'System.Int32')"
            };
            this.dt.Columns.Add(column);
            object obj2 = this.dt.Compute("Max(IDMarkInt)", "");
            string str9 = "1";
            if (!Convert.IsDBNull(obj2))
            {
                str9 = (Convert.ToInt32(obj2) + 1).ToString();
            }
            return str9;
        }

        private string GetDirInfo(string strId)
        {
            string str = Convert.ToString(this.dt.Compute("Count(IDMark)", "ParentID='" + strId + "' and [module]='1'"));
            string str2 = Convert.ToString(this.dt.Compute("Count(IDMark)", "ParentID='" + strId + "' and [module]='2'"));
            string str3 = Convert.ToString(this.dt.Compute("Count(IDMark)", "ParentID='" + strId + "' and [module]='3'"));
            string str4 = Convert.ToString(this.dt.Compute("Count(IDMark)", "ParentID='" + strId + "' and [module]='4'"));
            string str5 = Convert.ToString(this.dt.Compute("Count(IDMark)", "ParentID='" + strId + "' and [module]='5'"));
            string str6 = Convert.ToString(this.dt.Compute("Count(IDMark)", "ParentID='" + strId + "' and [module]='6'"));
            string str7 = Convert.ToString(this.dt.Compute("Count(IDMark)", "ParentID='" + strId + "' and [module]='7'"));
            string str8 = Convert.ToString(this.dt.Compute("Count(IDMark)", "ParentID='" + strId + "' and [module]='8'"));
            return ("'[" + str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6 + "," + str7 + "," + str8 + "]'");
        }

        private string GetModuleName(string strNum)
        {
            switch (strNum)
            {
                case "1":
                    return "简介模块";

                case "2":
                    return "新闻模块";

                case "3":
                    return "产品模块";

                case "4":
                    return "下载模块";

                case "5":
                    return "图片模块";

                case "6":
                    return "留言反馈";

                case "7":
                    return "外部模块";

                case "8":
                    return "招聘模块";
            }
            return "未知模块";
        }

        public DataTable GetUnlimitDataTable()
        {
            DataSet dataSet = this.Bll1.DAL1.GetDataSet("select id, ColumnName,ParentID,depth,order1,childNum,classlist,Nav,[Module],folder from QH_Column order by CLNG(ParentID) asc,CLNG(depth) asc, CLNG(Order1) asc,id asc");
            if (dataSet == null)
            {
                return null;
            }
            return dataSet.Tables[0];
        }

        public int InsertColumn(string Depth, string strID, string strPID, string strChildNum, string strName, string strOrder, string strSel1, string strSel2, string strDir, string strIDMark)
        {
            string str = "0";
            if (Depth == "2")
            {
                str = strPID + "," + strID;
                string[] strValue = new string[] { "" };
                string[] strField = new string[] { "childNum" };
                string str2 = "select childNum from QH_Column where id=" + strPID;
                try
                {
                    this.Bll1.DAL1.ReadDataReader(str2, ref strValue, strField);
                    this.Bll1.DAL1.ExecuteNonQuery(string.Concat(new object[] { "UPDATE QH_Column SET childNum='", int.Parse(strValue[0]) + 1, "' WHERE ID=", strPID }));
                    this.Bll1.DAL1.ExecuteNonQuery(string.Concat(new object[] { "UPDATE QH_Column SET childNum='", int.Parse(strChildNum) + 1, "' WHERE ID=", strID }));
                }
                catch (Exception exception)
                {
                    SystemError.CreateErrorLog("读栏目数据库错误： " + exception.ToString());
                }
            }
            else if (Depth == "1")
            {
                str = strID;
                this.Bll1.DAL1.ExecuteNonQuery(string.Concat(new object[] { "UPDATE QH_Column SET childNum='", int.Parse(strChildNum) + 1, "' WHERE ID=", strID }));
            }
            string strQuery = "insert into QH_Column(ColumnName,ParentID,depth,order1,childNum,IDMark,classlist,Nav,[Module],folder,NewWin,ListOrder,ListContent,[Access],IsShow,NumInPage,BnrDefault,ProductTMInherit) values(@ColumnName,@ParentID,@depth,@order1,@childNum,@IDMark,@classlist,@Nav,@Module,@folder,@NewWin,@ListOrder,@ListContent,@Access,@IsShow,'20','1','1')";
            try
            {
                OleDbParameter[] olePara = new OleDbParameter[] { new OleDbParameter("@ColumnName", strName), new OleDbParameter("@ParentID", strID), new OleDbParameter("@depth", Depth), new OleDbParameter("@order1", strOrder), new OleDbParameter("@childNum", "0"), new OleDbParameter("@IDMark", strIDMark), new OleDbParameter("@classlist", str), new OleDbParameter("@Nav", strSel1), new OleDbParameter("@Module", strSel2), new OleDbParameter("@folder", strDir), new OleDbParameter("@NewWin", "0"), new OleDbParameter("@ListOrder", "1"), new OleDbParameter("@ListContent", "1"), new OleDbParameter("@Access", "0"), new OleDbParameter("@IsShow", "1") };
                return this.Bll1.DAL1.ExecuteNonQuery(strQuery, olePara);
            }
            catch (Exception exception2)
            {
                SystemError.CreateErrorLog("插入栏目数据库错误： " + exception2.ToString());
            }
            return 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.DataToBind();
            }
        }

        private int SaveNewColumn()
        {
            string[] strArray = this.HdSaveN.Value.Trim().Split(new char[] { '|' });
            if (strArray.Length != 4)
            {
                return 0;
            }
            string strIDMark = base.Request.Form["IDMN"];
            string strOrder = base.Request.Form["TOrN"];
            string strName = base.Request.Form["TNmN"];
            string str4 = base.Request.Form["SelN1"];
            string str5 = base.Request.Form["SelN2"];
            string strDir = base.Request.Form["TDir"];
            string absolutePath = base.Request.Url.AbsolutePath;
            int num = absolutePath.LastIndexOf('/');
            int num2 = absolutePath.LastIndexOf('/', num - 1);
            string str8 = absolutePath.Substring(num2 + 1, (num - num2) - 1);
            string str9 = ",bin,app_data,ajax,upload,DBBAK,UpFile,include,FusionCharts,images,install,template,m,cdqhUpGradeTemp,NewsDetails,ProductDetails,DownloadDetails,PictureDetails,TagsAndSearch," + str8 + ",WebApp,MemberManage,";
            if ((strArray[0] == "0") && str9.ToLower().Contains("," + strDir.ToLower() + ","))
            {
                return 4;
            }
            if ((strIDMark != null) && (strIDMark != "0"))
            {
                foreach (string str10 in this.Bll1.DAL1.ReadDataReaderAL("select IDMark from QH_Column where IDMark<>'0'"))
                {
                    if (str10 == strIDMark)
                    {
                        return 3;
                    }
                }
            }
            foreach (string str11 in this.Bll1.DAL1.ReadDataReaderAL("select folder from QH_Column where ParentID='" + strArray[1] + "'"))
            {
                if (str11 == strDir)
                {
                    return 2;
                }
            }
            if (strDir == null)
            {
                strDir = "Message";
            }
            if ((((strIDMark == null) || (strOrder == null)) || ((strName == null) || (str4 == null))) || ((str5 == null) || (strDir == null)))
            {
                return 0;
            }
            int num3 = this.InsertColumn(strArray[0], strArray[1], strArray[2], strArray[3], strName, strOrder, str4, str5, strDir, strIDMark);
            if ((num3 == 1) && (strArray[0] == "0"))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "refresh", "<script>window .parent .frames['menu-frame'].location='Left_ContentNew.aspx';</script>");
            }
            return num3;
        }

        private void SetColumsRow(string str, DataRowView drv, string strColor)
        {
            this.nRNum++;
            this.HdSaveID.Value = this.HdSaveID.Value + "|" + drv["ID"].ToString();
            this.strSel = new string[] { "", "", "", "" };
            this.strSel[int.Parse(drv["Nav"].ToString())] = "selected";
            if (drv["depth"].ToString() == "2")
            {
                this.strADD = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            else
            {
                this.strADD = string.Concat(new object[] { 
                    "<a href =javascript:AddColumn(", this.nRNum - 1, ",", drv["childNum"], ",", int.Parse(drv["depth"].ToString()) + 1, ",", drv["ID"], ",", drv["ParentID"], ",", this.strMaxIDMark, ",", this.strMsg, ",", this.strZhP, 
                    ",", drv["Module"], ",", this.GetDirInfo(drv["ID"].ToString()), ") >添加子栏目</a>"
                 });
            }
            this.sbTRows.Append("<tr  onmouseover=\"this.style.backgroundColor='#9ec0fe'\" onmouseout=\"this.style.backgroundColor='" + strColor + "'\"  style =\"background-color:" + strColor + " ;\" >\n");
            this.sbTRows.Append(string.Concat(new object[] { "<td><input name=\"Ck", this.nRNum, "\" id=\"Ck", this.nRNum, "\" type=\"checkbox\" /></td>\n" }));
            this.sbTRows.Append(string.Concat(new object[] { "<td><input  name =\"IDM", this.nRNum, "\" id=\"IDM", this.nRNum, "\" type=\"text\" class=or  maxlength=3 value='", drv["IDMark"], "' /></td>\n" }));
            this.sbTRows.Append(string.Concat(new object[] { "<td><input  name =\"TOr", this.nRNum, "\" id=\"TOr", this.nRNum, "\" type=\"text\" class=or  maxlength=3 value='", drv["order1"], "' /></td>\n" }));
            this.sbTRows.Append(string.Concat(new object[] { "<td align=left >", str, "<input name=\"TNm", this.nRNum, "\"  id=\"TNm", this.nRNum, "\" type=\"text\"  class=or1 value='", drv["ColumnName"], "' /></td>\n" }));
            this.sbTRows.Append("<td>\n");
            this.sbTRows.Append("    <select name=\"Sel" + this.nRNum + "\">\n");
            this.sbTRows.Append("        <option  value=0 " + this.strSel[0] + " >不显示</option>\n");
            this.sbTRows.Append("        <option  value=1 " + this.strSel[1] + " >头部主导航条</option>\n");
            this.sbTRows.Append("        <option  value=2 " + this.strSel[2] + " >尾部导航条</option>\n");
            this.sbTRows.Append("        <option  value=3 " + this.strSel[3] + " >都显示</option>\n");
            this.sbTRows.Append("   </select>\n");
            this.sbTRows.Append("</td><td style=\"width:90px;\">" + this.GetModuleName(drv["Module"].ToString()) + "</td>\n");
            this.sbTRows.Append("<td style=\"width:130px;word-wrap: break-word;word-break:break-all;\">" + drv["folder"] + "</td>\n");
            this.sbTRows.Append(string.Concat(new object[] { "<td align=left ><a href =javascript:Save(", this.nRNum, ",", drv["ID"], ")  >保存</a>&nbsp;&nbsp;<a href =ColumnEdit.aspx?id=", drv["ID"], " >编辑</a>&nbsp;&nbsp;\n" }));
            this.sbTRows.Append(string.Concat(new object[] { this.strADD, "&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:Del(", drv["depth"], ",", drv["ID"], ",", drv["ParentID"], ",", drv["childNum"], ",", drv["Module"], ",'", drv["folder"], "') >删除</a>\n" }));
            this.sbTRows.Append("</td>\n");
            this.sbTRows.Append("</tr>\n");
        }

        public int UpdateColumn(string strID, string strName, string strOrder, string strSel, string strIDMark)
        {
            string strQuery = "UPDATE QH_Column SET ColumnName=@ColumnName,order1=@order1 ,IDMark=@IDMark,Nav=@Nav WHERE ID=@ID";
            try
            {
                OleDbParameter[] olePara = new OleDbParameter[] { new OleDbParameter("@ColumnName", strName), new OleDbParameter("@order1", strOrder), new OleDbParameter("@IDMark", strIDMark), new OleDbParameter("@Nav", strSel), new OleDbParameter("@ID", strID) };
                return this.Bll1.DAL1.ExecuteNonQuery(strQuery, olePara);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("更新栏目数据库错误： " + exception.ToString());
            }
            return 0;
        }
    }
}