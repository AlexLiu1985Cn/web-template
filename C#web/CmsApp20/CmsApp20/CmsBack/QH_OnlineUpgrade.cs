namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using _commen;

    public class QH_OnlineUpgrade : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnSave;
        protected HtmlForm form1;
        private WebClient myWebClient = new WebClient();
        private int nStart;
        private static string remoteDir = "LastVersion";
        private static string remoteWeb = "http://www.95c.com.cn/";
        protected string strAllVer = "";
        protected string strLastHighVersion;
        protected string strLastVersion;
        protected string strNextVersion = "";
        protected string strNextVersionDisplay;
        private string strPageTop = "<div class=\"glrihgt\"><div class=\"glrihgtnei\"><div class=\"rightmain\">当前位置：在线升级 >> 在线升级</div><div class=\"rightmain1\"> </div></div></div>";
        private string strRemoteAdminDir = "cmsback";
        private string strSize = "";
        private string strTemp = "";
        protected string strVersion;

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            base.Response.Write("    <style type=\"text/css\">body{font-family: Microsoft Yahei;font-size: 12px;line-height :16px;}</style>");
            base.Response.Write(this.strPageTop);
            base.Response.Flush();
            this.strVersion = (string)this.ViewState["NextVersion"];
            this.strNextVersion = this.strVersion;
            bool flag = false;
            string path = base.Server.MapPath("../cdqhUpGradeTemp/" + this.strVersion + "/");
            int index = 0;
            if (Directory.Exists(path))
            {
                base.Response.Write("正在校验...");
                base.Response.Flush();
                index = this.CheckDirSize(path, "");
                if (index > 0)
                {
                    base.Response.Write("  校验失败！");
                    base.Response.Flush();
                }
                else
                {
                    base.Response.Write("  校验成功！");
                    base.Response.Flush();
                }
                flag = true;
            }
            if (!flag)
            {
                base.Response.Write("正在下载升级文件...");
                base.Response.Flush();
                index = this.DownloadUpgradePackage(remoteWeb + remoteDir + "/" + this.strVersion + "/", this.strVersion);
                if (index > 0)
                {
                    base.Response.Write("  失败（请重试或换个时间升级）");
                    base.Response.Flush();
                }
                else
                {
                    base.Response.Write("  完成。");
                    base.Response.Flush();
                }
                if (index == 0)
                {
                    base.Response.Write("<br>正在校验...");
                    base.Response.Flush();
                    index = this.CheckDirSize(path, this.strSize);
                    if (index > 0)
                    {
                        base.Response.Write("  校验失败！");
                        base.Response.Flush();
                    }
                    else
                    {
                        base.Response.Write("  校验成功！");
                        base.Response.Flush();
                    }
                }
                if ((index > 0) && Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            if (index > 0)
            {
                string[] strArray = new string[] { "", "下载文件错误，请重试。", "读Lastversion.txt文件错误。", "读取版本号与升级版本号不一致。", "读取的升级目录大小为零。", "升级目录大小校验失败。" };
                base.ClientScript.RegisterStartupScript(base.GetType(), "DownFailure", "alert('版本" + this.strVersion + "升级中" + strArray[index] + "');", true);
                this.strVersion = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select top 1 Version from QH_SiteInfo "));
                this.DataTobindAllVerSion();
                return;
            }
            base.Response.Write("<br>正在升级...");
            base.Response.Flush();
            string str2 = base.Server.MapPath("../");
            string str3 = base.Server.MapPath("/");
            string absoluteUri = base.Request.Url.AbsoluteUri;
            int num2 = absoluteUri.LastIndexOf('/');
            int num3 = absoluteUri.LastIndexOf('/', num2 - 1);
            string str5 = absoluteUri.Substring(num3 + 1, (num2 - num3) - 1);
            this.strTemp = System.IO.File.ReadAllText(path + "Lastversion.txt", Encoding.Default);
            if (string.IsNullOrEmpty(this.strTemp))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "NoVer", "alert('版本" + this.strVersion + "暂时无法升级，请与我们联系咨询。');", true);
                this.strVersion = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select top 1 Version from QH_SiteInfo "));
                this.DataTobindAllVerSion();
                base.Response.Write("<script language=javascript>HideWait();</script>");
                return;
            }
            string strQuery = "";
            string input = "";
            string str8 = "";
            string str9 = "";
            string sourceFileName = "";
            string destFileName = "";
            string strTable = "";
            string strField = "";
            int length = 0;
            for (int i = 0; i < 200; i++)
            {
                strQuery = this.ReadLineContent(this.strTemp, ref this.nStart).Replace(@"\", "/");
                switch (strQuery)
                {
                    case "readEnd":
                    case "":
                        goto Label_0877;

                    default:
                        input = strQuery.ToLower();
                        if (Regex.IsMatch(input, @"alter\s+table\s+\w+\s+add\b.*"))
                        {
                            this.GetAddTableAndField(input, ref strTable, ref strField);
                            if (!this.CheckTableFieldNotExist(input, strTable, strField))
                            {
                                break;
                            }
                            try
                            {
                                this.Bll1.DAL1.ExecuteNonQuery(strQuery);
                                break;
                            }
                            catch (Exception exception)
                            {
                                base.Response.Write("更新数据库错误！" + exception.Message);
                                SystemError.CreateErrorLog(exception.ToString() + "\"（" + strQuery + "）");
                                this.BtnSave.Enabled = false;
                                return;
                            }
                        }
                        if (Regex.IsMatch(input, @"alter\s+table\s+\w+\s+alter\s+column\b.*"))
                        {
                            this.GetAlterTableAndField(input, ref strTable, ref strField);
                            if (this.CheckTableFieldNotExist(input, strTable, strField))
                            {
                                break;
                            }
                            try
                            {
                                this.Bll1.DAL1.ExecuteNonQuery(strQuery);
                                break;
                            }
                            catch (Exception exception2)
                            {
                                base.Response.Write("更新数据库错误！" + exception2.Message);
                                SystemError.CreateErrorLog(exception2.ToString() + "\"（" + strQuery + "）");
                                this.BtnSave.Enabled = false;
                                return;
                            }
                        }
                        if (Regex.IsMatch(input, @"create\s+table\s+\w+\s*\(.*"))
                        {
                            this.GetNewTableName(input, ref strTable);
                            if (!this.CheckTableNotExist(input, strTable))
                            {
                                break;
                            }
                            try
                            {
                                this.Bll1.DAL1.ExecuteNonQuery(strQuery);
                                break;
                            }
                            catch (Exception exception3)
                            {
                                base.Response.Write("生成数据库表错误！" + exception3.Message);
                                SystemError.CreateErrorLog(exception3.ToString() + "\"（" + strQuery + "）");
                                this.BtnSave.Enabled = false;
                                return;
                            }
                        }
                        if (Regex.IsMatch(input, @"alter\s+table\s+\w+\s+drop\s+column\b.*"))
                        {
                            this.GetTableAndDropField(input, ref strTable, ref strField);
                            if (this.CheckTableFieldNotExist(input, strTable, strField))
                            {
                                break;
                            }
                            try
                            {
                                this.Bll1.DAL1.ExecuteNonQuery(strQuery);
                                break;
                            }
                            catch (Exception exception4)
                            {
                                base.Response.Write("更新数据库错误！" + exception4.Message);
                                SystemError.CreateErrorLog(exception4.ToString() + "\"（" + strQuery + "）");
                                this.BtnSave.Enabled = false;
                                return;
                            }
                        }
                        if (Regex.IsMatch(input, @"drop\s+table\s+\w+\b.*"))
                        {
                            this.GetDropTableName(input, ref strTable);
                            if (this.CheckTableNotExist(input, strTable))
                            {
                                break;
                            }
                            try
                            {
                                this.Bll1.DAL1.ExecuteNonQuery(strQuery);
                                break;
                            }
                            catch (Exception exception5)
                            {
                                base.Response.Write("生成数据库表错误！" + exception5.Message);
                                SystemError.CreateErrorLog(exception5.ToString() + "\"（" + strQuery + "）");
                                this.BtnSave.Enabled = false;
                                return;
                            }
                        }
                        if (Regex.IsMatch(input, @"^[\w\\\/]+\.?\w*$"))
                        {
                            strQuery = (strQuery[0] == '/') ? strQuery.Substring(1) : strQuery;
                            str8 = Regex.Replace(input, this.strRemoteAdminDir + @"(?:\/|\\)", str5 + "/");
                            length = str8.LastIndexOf("/");
                            str9 = str2 + ((length > 0) ? str8.Substring(0, length) : "");
                            if (!Directory.Exists(str9))
                            {
                                Directory.CreateDirectory(str9);
                            }
                            str8 = Regex.Replace(Regex.Replace(str8, @"\.htm\b", ".aspx", RegexOptions.IgnoreCase), @"web\.xml\b", "Web.config", RegexOptions.IgnoreCase);
                            sourceFileName = path + strQuery;
                            destFileName = (Regex.IsMatch(str8, @"\bbin(?:\/|\\)") ? str3 : str2) + str8;
                            try
                            {
                                System.IO.File.Copy(sourceFileName, destFileName, true);
                            }
                            catch (Exception exception6)
                            {
                                base.Response.Write("更新文件错误！" + exception6.Message);
                                SystemError.CreateErrorLog(exception6.ToString() + "\"（" + sourceFileName + "）");
                                this.BtnSave.Enabled = false;
                                return;
                            }
                        }
                        break;
                }
            }
        Label_0877:
            this.Bll1.DAL1.ExecuteNonQuery("UPDATE QH_SiteInfo SET Version='" + this.strVersion + "' WHERE ID=1");
            base.Response.Write("  升级成功！");
            base.Response.Flush();
            this.DataTobindAllVerSion();
            this.strTemp = System.IO.File.ReadAllText(path + "Readme.txt", Encoding.Default);
            if (string.IsNullOrEmpty(this.strTemp))
            {
                this.strTemp = "升级成功！";
            }
            else
            {
                this.strTemp = @"升级成功！\n" + Regex.Replace(this.strTemp, "[\"']", "“").Replace("\r", "").Replace("\n", @"\n");
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), "OK", "alert('" + this.strTemp + "');location=document.URL;", true);
        }

        private int CheckDirSize(string strPackdir, string strSize1)
        {
            if (strSize1 == "")
            {
                string str = System.IO.File.ReadAllText(strPackdir + "Lastversion.txt", Encoding.Default);
                if (string.IsNullOrEmpty(str))
                {
                    return 2;
                }
                int nStart = 0;
                Match match = Regex.Match(this.ReadLineContent(str, ref nStart), @"([0-9\.]+)\s*(?:\(|（)?\s*(\d*)\s*(?:\)|）)?");
                this.strSize = strSize1 = match.Groups[2].Value;
                if (match.Groups[1].Value != this.strVersion)
                {
                    return 3;
                }
                if (this.strSize == "")
                {
                    return 4;
                }
            }
            if (this.Bll1.DirectorySize(strPackdir).ToString() != strSize1)
            {
                return 5;
            }
            this.strNextVersion = this.strVersion;
            return 0;
        }

        private bool CheckTableFieldNotExist(string strRemoteFileLower, string strTable, string strField)
        {
            if ((strTable == "") || (strField == ""))
            {
                return false;
            }
            bool flag = true;
            OleDbConnection myConn = null;
            try
            {
                myConn = this.Bll1.DAL1.MyConn;
                myConn.Open();
                object[] restrictions = new object[4];
                restrictions[2] = strTable;
                DataTable oleDbSchemaTable = myConn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, restrictions);
                myConn.Close();
                foreach (DataRow row in oleDbSchemaTable.Rows)
                {
                    if (row["COLUMN_NAME"].ToString().ToLower() == strField)
                    {
                        return false;
                    }
                }
            }
            catch (Exception exception)
            {
                if (myConn != null)
                {
                    myConn.Close();
                }
                flag = false;
                SystemError.CreateErrorLog(exception.ToString());
            }
            return flag;
        }

        private bool CheckTableNotExist(string strRemoteFileLower, string strTable)
        {
            if (strTable == "")
            {
                return false;
            }
            bool flag = true;
            OleDbConnection myConn = null;
            try
            {
                myConn = this.Bll1.DAL1.MyConn;
                myConn.Open();
                object[] restrictions = new object[4];
                restrictions[2] = strTable;
                restrictions[3] = "TABLE";
                DataTable oleDbSchemaTable = myConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);
                myConn.Close();
                return (oleDbSchemaTable.Rows.Count <= 0);
            }
            catch (Exception exception)
            {
                if (myConn != null)
                {
                    myConn.Close();
                }
                flag = false;
                SystemError.CreateErrorLog(exception.ToString());
            }
            return flag;
        }

        private void DataTobind()
        {
            this.nStart = 0;
            string address = string.Concat(new object[] { remoteWeb, remoteDir, "/", this.ViewState["NextVersion"], "/Lastversion.txt" });
            try
            {
                this.strTemp = this.myWebClient.DownloadString(address);
            }
            catch (Exception exception)
            {
                base.Response.Write("读版本文件错误！" + exception.Message);
                SystemError.CreateErrorLog(exception.ToString() + "\"（" + address + "）");
                this.BtnSave.Enabled = false;
                return;
            }
            this.strNextVersion = this.ReadLineContent(this.strTemp, ref this.nStart);
        }

        private void DataTobindAllVerSion()
        {
            float num = float.Parse(this.strVersion);
            this.nStart = 0;
            string address = remoteWeb + remoteDir + "/versionPre.txt";
            try
            {
                this.strTemp = this.myWebClient.DownloadString(address);
            }
            catch (Exception exception)
            {
                base.Response.Write("读版本号文件错误！" + exception.Message);
                this.BtnSave.Enabled = false;
                return;
            }
            this.strLastHighVersion = this.ReadLineContent(this.strTemp, ref this.nStart);
            string input = "";
            bool flag = false;
            for (int i = 0; i < 0x2710; i++)
            {
                input = this.ReadLineContent(this.strTemp, ref this.nStart);
                if (((input == "readEnd") || (input == "")) || !Regex.IsMatch(input, @"^\d+(\.\d+)?$"))
                {
                    break;
                }
                if (float.Parse(input) > num)
                {
                    if (input[0] != this.strVersion[0])
                    {
                        break;
                    }
                    if (!flag)
                    {
                        this.strNextVersion = input;
                        flag = true;
                    }
                    this.strLastVersion = input;
                }
            }
            this.BtnSave.Text = " 升级到版本" + this.strNextVersion + " ";
            this.strNextVersionDisplay = "可升级最新版本：" + this.strNextVersion;
            if (!(this.strNextVersion == this.strVersion) && !(this.strNextVersion == ""))
            {
                this.ViewState["NextVersion"] = this.strNextVersion;
            }
            else
            {
                this.strNextVersionDisplay = "已升级到最新版本" + this.strVersion;
                this.strLastVersion = this.strVersion;
                this.BtnSave.Text = " 最新版本" + this.strLastVersion + " ";
                this.BtnSave.Enabled = false;
            }
        }

        private int DownloadUpgradePackage(string strDownloadDir, string strVersion)
        {
            string path = base.Server.MapPath("../cdqhUpGradeTemp/" + strVersion + "/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                this.myWebClient.DownloadFile(strDownloadDir + "Lastversion.txt", path + "Lastversion.txt");
                if (BLL.RemoteIsExist(strDownloadDir + "Readme.txt"))
                {
                    this.myWebClient.DownloadFile(strDownloadDir + "Readme.txt", path + "Readme.txt");
                }
                if (BLL.RemoteIsExist(strDownloadDir + "robots.txt"))
                {
                    this.myWebClient.DownloadFile(strDownloadDir + "robots.txt", path + "robots.txt");
                }
                if (BLL.RemoteIsExist(strDownloadDir + "Web.xml"))
                {
                    this.myWebClient.DownloadFile(strDownloadDir + "Web.xml", path + "Web.xml");
                }
                string str2 = System.IO.File.ReadAllText(path + "Lastversion.txt", Encoding.Default);
                if (string.IsNullOrEmpty(str2))
                {
                    return 2;
                }
                int nStart = 0;
                Match match = Regex.Match(this.ReadLineContent(str2, ref nStart), @"([0-9\.]+)\s*(?:\(|（)?\s*(\d*)\s*(?:\)|）)?");
                this.strSize = match.Groups[2].Value;
                if (match.Groups[1].Value != strVersion)
                {
                    return 3;
                }
                if (this.strSize == "")
                {
                    return 4;
                }
                string str3 = "";
                string input = "";
                string str5 = "";
                int length = 0;
                for (int i = 0; i < 200; i++)
                {
                    str3 = this.ReadLineContent(str2, ref nStart).Replace(@"\", "/");
                    switch (str3)
                    {
                        case "readEnd":
                        case "":
                            goto Label_02D8;
                    }
                    input = str3.ToLower().Trim();
                    if ((((input != "robots.txt") && (input != "web.xml")) && ((input != "Readme.txt") && !Regex.IsMatch(input, @"^.+?\btable\b.*$"))) && Regex.IsMatch(input, @"^[\w\\\/]+\.?\w*$"))
                    {
                        str3 = (str3[0] == '/') ? str3.Substring(1) : str3;
                        length = str3.LastIndexOf("/");
                        str5 = path + ((length > 0) ? str3.Substring(0, length) : "");
                        if (!Directory.Exists(str5))
                        {
                            Directory.CreateDirectory(str5);
                        }
                        this.myWebClient.DownloadFile(strDownloadDir + str3, path + str3);
                    }
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog(exception.ToString() + "\"（" + strDownloadDir + "）");
                return 1;
            }
        Label_02D8:
            return 0;
        }

        private void GetAddTableAndField(string strRemoteFileLower, ref string strTable, ref string strField)
        {
            string pattern = @".*table\s+(?<table>\w+)\s+add\s+(?<Field>\w+)\s+.*";
            Match match = new Regex(pattern).Match(strRemoteFileLower);
            strTable = match.Groups["table"].Value;
            strField = match.Groups["Field"].Value;
        }

        private void GetAlterTableAndField(string strRemoteFileLower, ref string strTable, ref string strField)
        {
            string pattern = @".*table\s+(?<table>\w+)\s+alter\s+column\s+(?<Field>\w+)\b";
            Match match = new Regex(pattern).Match(strRemoteFileLower);
            strTable = match.Groups["table"].Value;
            strField = match.Groups["Field"].Value;
        }

        private void GetDropTableName(string strRemoteFileLower, ref string strTable)
        {
            string pattern = @"^\s*drop\s+table\s+(\w+)\b";
            Match match = Regex.Match(strRemoteFileLower, pattern);
            strTable = match.Groups[1].Value;
        }

        private void GetNewTableName(string strRemoteFileLower, ref string strTable)
        {
            string pattern = @"^\s*create\s+table\s+(\w+)\s*\(.*";
            Match match = Regex.Match(strRemoteFileLower, pattern);
            strTable = match.Groups[1].Value;
        }

        private void GetTableAndDropField(string strRemoteFileLower, ref string strTable, ref string strField)
        {
            string pattern = @".*table\s+(?<table>\w+)\s+drop\s+column\s+(?<Field>\w+)\b";
            Match match = new Regex(pattern).Match(strRemoteFileLower);
            strTable = match.Groups["table"].Value;
            strField = match.Groups["Field"].Value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.strVersion = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select top 1 Version from QH_SiteInfo "));
                if (!Regex.IsMatch(this.strVersion, @"^\d+(\.\d+)?$"))
                {
                    base.Response.Write("<script>alert(\"读取版本号错误，暂时无法升级，请与我们联系咨询。\");</script>");
                    this.BtnSave.Enabled = false;
                }
                else
                {
                    base.Response.Write(this.strPageTop);
                    this.DataTobindAllVerSion();
                }
            }
        }

        private string ReadLineContent(string strTemp, ref int nStart)
        {
            string str = "";
            int index = strTemp.IndexOf("\n", nStart);
            if (index == -1)
            {
                str = strTemp.Substring(nStart, strTemp.Length - nStart).Trim();
                if (str == "")
                {
                    return "readEnd";
                }
                nStart = strTemp.Length;
                return str;
            }
            str = strTemp.Substring(nStart, index - nStart).Trim();
            nStart = index + 1;
            return str;
        }
    }
}