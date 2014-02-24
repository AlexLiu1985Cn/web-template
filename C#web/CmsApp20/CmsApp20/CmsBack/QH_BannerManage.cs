using _BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CmsApp20.CmsBack
{
    public class QH_BannerManage : Page
    {
        private ListItem[] aListItem;
        private BLL Bll1 = new BLL();
        protected Button Button1;
        protected CheckBox Checkbox1;
        private int CurrentPage;
        protected DropDownList DDLClmn;
        protected DropDownList DDLImgType;
        protected DropDownList DDLPage;
        protected DropDownList DropDownListPage;
        protected HtmlForm form1;
        protected HiddenField HdnClmn;
        private ListItem Item;
        protected Label lblCurrentPage;
        protected Label lblPageCount;
        protected Label lblRecordCount;
        protected LinkButton lbnNextPage;
        protected LinkButton lbnPrevPage;
        private int nRNum;
        private int PageCount;
        private int PageSize;
        private int RecordCount;
        protected Repeater RepeaterBanner;
        private string strID1;
        private string strID2;
        protected string strPage;
        protected string strSel;
        private string strWhere = "";

        private bool AfterDeleteNews()
        {
            return true;
        }

        private bool BeforeDeleteNews(string ID)
        {
            string str = "NewsDetails";
            string str2 = str + "_" + ID + ".html";
            string path = base.Server.MapPath("/NewsCenter") + @"\" + str2;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            this.GetInfluenceID(ID, out this.strID1, out this.strID2);
            return true;
        }

        protected void ButtondeleteSV_Clicked(object sender, EventArgs e)
        {
            bool flag = true;
            int deletecount = 0;
            StringBuilder builder = new StringBuilder();
            foreach (RepeaterItem item in this.RepeaterBanner.Items)
            {
                HtmlInputCheckBox box = (HtmlInputCheckBox)item.FindControl("checkbox");
                if (box.Checked)
                {
                    builder.Append("," + ((HtmlInputHidden)item.FindControl("SelectedID")).Value);
                }
            }
            List<List<string>> list = new List<List<string>>();
            if (builder.Length > 0)
            {
                list = this.Bll1.ReadDataReaderList("select ImgUrl,flashUrl from QH_BannerImg where id in(" + builder.ToString().Substring(1) + ")", 4);
                deletecount = this.Bll1.Delete("delete from QH_BannerImg where id in(" + builder.ToString().Substring(1) + ")");
            }
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (list[i][j] != "")
                    {
                        string path = base.Server.MapPath(list[i][j]);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }
                }
            }
            if (deletecount > 0)
            {
                this.ChangePageCount(deletecount);
                if (flag)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "dSuccess", @"<script>alert('删除成功！\n '); </script>");
                }
            }
        }

        private int CalculateRecordNumber()
        {
            return this.Bll1.DAL1.ExecuteReader("select count(*) as co from QH_BannerImg " + ((string)this.ViewState["QWhere"]));
        }

        private void ChangePageCount(int deletecount)
        {
            int num = (int)this.ViewState["PageIndex"];
            int num2 = (int)this.ViewState["RecordCount"];
            int num3 = (int)this.ViewState["PageCount"];
            if ((((num + 1) == num3) && ((num2 - deletecount) == ((num3 - 1) * this.PageSize))) && (num > 0))
            {
                this.ViewState["PageIndex"] = num - 1;
            }
            this.SetPageDisplay((int)this.ViewState["PageIndex"]);
            this.Checkbox1.Checked = false;
            this.ListBind();
        }

        public void CheckboxslctallSV_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            if (box.Checked)
            {
                foreach (RepeaterItem item in this.RepeaterBanner.Items)
                {
                    HtmlInputCheckBox box2 = (HtmlInputCheckBox)item.FindControl("checkbox");
                    box2.Checked = true;
                }
            }
            else
            {
                foreach (RepeaterItem item2 in this.RepeaterBanner.Items)
                {
                    HtmlInputCheckBox box3 = (HtmlInputCheckBox)item2.FindControl("checkbox");
                    box3.Checked = false;
                }
            }
        }

        private ICollection CreateSource()
        {
            string strQuery = "select * ,switch(type='0','图片',type='1','Flash') as type from QH_BannerImg " + ((string)this.ViewState["QWhere"]) + " order by id desc";
            this.PageSize = (int)this.ViewState["PageSize"];
            DataTable table = this.Bll1.GetDataTablePage(this.CurrentPage * this.PageSize, this.PageSize, strQuery);
            if (table == null)
            {
                return null;
            }
            if (table.Rows.Count == 0)
            {
                return null;
            }
            table.Columns.Add("Url");
            table.Columns.Add("Belongs1");
            string[] strArray = this.HdnClmn.Value.Split(new char[] { '|' });
            StringBuilder builder = new StringBuilder();
            foreach (DataRow row in table.Rows)
            {
                if (row["type"].ToString() == "图片")
                {
                    row["Url"] = row["ImgUrl"];
                }
                else
                {
                    row["Url"] = row["flashUrl"];
                }
                builder = new StringBuilder();
                string[] strArray2 = new string[0];
                string str2 = row["Belongs"].ToString();
                if (str2.Length > 2)
                {
                    strArray2 = str2.Substring(0, str2.Length - 1).Substring(1).Split(new char[] { ',' });
                }
                foreach (string str3 in strArray2)
                {
                    if (str3 == "0")
                    {
                        builder.Append("-网站首页");
                    }
                    else
                    {
                        for (int i = 0; i < (strArray.Length / 2); i++)
                        {
                            if (strArray[i << 1] == str3)
                            {
                                builder.Append("-" + strArray[(i << 1) + 1]);
                                break;
                            }
                        }
                    }
                }
                string str4 = "";
                if (builder.Length > 0)
                {
                    str4 = builder.ToString().Substring(1);
                }
                if (str2 == "D")
                {
                    str4 = "默认栏目";
                }
                row["Belongs1"] = str4;
                row["Belongs"] = (str4.Length > 0x12) ? (str4.Substring(0, 0x12) + "...") : str4;
            }
            return table.DefaultView;
        }

        protected void DDLClmn_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.DDLClmn.SelectedIndex)
            {
                case 0:
                    this.strWhere = "";
                    break;

                case 1:
                    this.strWhere = " Belongs like '%,0,%' ";
                    break;

                default:
                    this.strWhere = " Belongs like'%," + this.DDLClmn.SelectedValue + ",%'";
                    break;
            }
            switch (this.DDLImgType.SelectedIndex)
            {
                case 1:
                    this.strWhere = (this.strWhere == "") ? " type='0' " : (this.strWhere + " and type='0' ");
                    break;

                case 2:
                    this.strWhere = (this.strWhere == "") ? " type='1' " : (this.strWhere + " and type='1' ");
                    break;
            }
            this.strWhere = (this.strWhere == "") ? "" : ("where" + this.strWhere);
            this.ViewState["QWhere"] = this.strWhere;
            this.ViewState["Select"] = this.DDLClmn.SelectedIndex.ToString() + "_" + this.DDLImgType.SelectedIndex.ToString();
            this.SetPageDisplay((int)this.ViewState["PageIndex"]);
            this.ListBind();
        }

        private void DDLDataBind()
        {
            this.nRNum = 0;
            this.Item = new ListItem("所属栏目");
            this.DDLClmn.Items.Add(this.Item);
            this.Item = new ListItem("网站首页");
            this.DDLClmn.Items.Add(this.Item);
            DataTable dataTable = this.Bll1.GetDataTable("select id,ColumnName,ParentID,depth from QH_Column order by CLNG(ParentID) asc,CLNG(depth) asc, CLNG(Order1) asc,id asc");
            if (dataTable != null)
            {
                this.aListItem = new ListItem[dataTable.Rows.Count];
                this.DisplayColumns(dataTable, "0", 0);
                this.DDLClmn.Items.AddRange(this.aListItem);
            }
        }

        protected void DDLPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.DDLPage.SelectedIndex)
            {
                case 0:
                    this.ViewState["PageSize"] = this.PageSize = 20;
                    break;

                case 1:
                    this.ViewState["PageSize"] = this.PageSize = 50;
                    break;

                case 2:
                    this.ViewState["PageSize"] = this.PageSize = 100;
                    break;
            }
            this.SetPageDisplay(0);
            this.ListBind();
        }

        private void DDLPageBind()
        {
            string[] strArray = new string[] { "20条", "50条", "100条" };
            this.DDLPage.DataSource = strArray;
            this.DDLPage.DataBind();
            strArray = new string[] { "选择", "图片", "Flash动画" };
            this.DDLImgType.DataSource = strArray;
            this.DDLImgType.DataBind();
        }

        private void DisplayColumns(DataTable dt, string pid, int blank)
        {
            string s = " ";
            DataView view = new DataView(dt)
            {
                RowFilter = "ParentID = '" + pid + "'"
            };
            string str2 = "";
            if (blank > 0)
            {
                if (blank == 1)
                {
                    str2 = "&nbsp;&nbsp;";
                }
                for (int i = 2; i <= blank; i++)
                {
                    str2 = str2 + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
            }
            int num2 = 0;
            foreach (DataRowView view2 in view)
            {
                string str3 = view2["ID"].ToString();
                string str4 = view2["Depth"].ToString();
                num2++;
                if (blank > 0)
                {
                    if (view.Count != num2)
                    {
                        s = str2 + "├";
                    }
                    else
                    {
                        s = str2 + "└";
                    }
                }
                this.aListItem[this.nRNum++] = new ListItem(base.Server.HtmlDecode(s) + view2["ColumnName"], view2["ID"].ToString());
                int num3 = int.Parse(str4) + 1;
                this.DisplayColumns(dt, str3, num3);
            }
        }

        protected void DropDownListPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CurrentPage = this.DropDownListPage.SelectedIndex;
            this.ViewState["PageIndex"] = this.CurrentPage;
            this.PageCount = (int)this.ViewState["PageCount"];
            this.ListBind();
        }

        private void GetClumnName()
        {
            List<List<string>> list = this.Bll1.ReadDataReaderList("select id,ColumnName from QH_Column ", 2);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                builder.Append("|" + list[i][0] + "|" + list[i][1]);
            }
            if (builder.Length > 0)
            {
                this.HdnClmn.Value = builder.ToString().Substring(1);
            }
        }

        private void GetInfluenceID(string ID, out string id1, out string id2)
        {
            id1 = id2 = "No";
            DataTable dataTable = this.Bll1.GetDataTable("select id from QH_News where ColumnID='" + ((string)this.ViewState["ClmnID"]) + "' order by id asc");
            if (dataTable != null)
            {
                int count = dataTable.Rows.Count;
                if (count > 1)
                {
                    if (count == 2)
                    {
                        if (dataTable.Rows[0]["id"].ToString() == ID)
                        {
                            id2 = dataTable.Rows[1]["id"].ToString();
                        }
                        else
                        {
                            id1 = dataTable.Rows[0]["id"].ToString();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (dataTable.Rows[i]["id"].ToString() == ID)
                            {
                                if (i == 0)
                                {
                                    id2 = dataTable.Rows[1]["id"].ToString();
                                    return;
                                }
                                if (i == (count - 1))
                                {
                                    id1 = dataTable.Rows[i - 1]["id"].ToString();
                                    return;
                                }
                                id1 = dataTable.Rows[i - 1]["id"].ToString();
                                id2 = dataTable.Rows[i + 1]["id"].ToString();
                                return;
                            }
                        }
                    }
                }
            }
        }

        public void ListBind()
        {
            this.RepeaterBanner.DataSource = this.CreateSource();
            this.RepeaterBanner.DataBind();
            this.lbnNextPage.Enabled = true;
            this.lbnPrevPage.Enabled = true;
            if (this.CurrentPage == (this.PageCount - 1))
            {
                this.lbnNextPage.Enabled = false;
            }
            if (this.CurrentPage == 0)
            {
                this.lbnPrevPage.Enabled = false;
            }
            this.lblCurrentPage.Text = (this.CurrentPage + 1).ToString();
            this.strPage = this.CurrentPage.ToString();
            this.strSel = (this.ViewState["Select"] == null) ? "" : ((string)this.ViewState["Select"]);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.ViewState["PageSize"] = this.PageSize = 20;
                this.ViewState["PageIndex"] = 0;
                this.CurrentPage = 0;
                string s = (base.Request.QueryString["Page"] == null) ? "" : base.Request.QueryString["Page"];
                if (s != "")
                {
                    this.ViewState["PageIndex"] = this.CurrentPage = int.Parse(s);
                }
                this.DDLPageBind();
                string str2 = (base.Request.QueryString["Sel"] == null) ? "" : base.Request.QueryString["Sel"];
                bool flag = false;
                if ((str2 != "") && (str2 != "0_0"))
                {
                    string[] strArray = str2.Split(new char[] { '_' });
                    this.DDLClmn.SelectedIndex = int.Parse(strArray[0]);
                    this.DDLImgType.SelectedIndex = int.Parse(strArray[1]);
                    this.DDLClmn_SelectedIndexChanged(new object(), new EventArgs());
                    flag = true;
                }
                this.GetClumnName();
                if (!flag)
                {
                    this.SetPageDisplay(this.CurrentPage);
                    this.ListBind();
                }
                this.DDLDataBind();
            }
        }

        public void Page_OnClick(object sender, CommandEventArgs e)
        {
            this.CurrentPage = (int)this.ViewState["PageIndex"];
            this.PageCount = (int)this.ViewState["PageCount"];
            string commandName = e.CommandName;
            if (commandName != null)
            {
                if (!(commandName == "next"))
                {
                    if ((commandName == "prev") && (this.CurrentPage > 0))
                    {
                        this.CurrentPage--;
                    }
                }
                else if (this.CurrentPage < (this.PageCount - 1))
                {
                    this.CurrentPage++;
                }
            }
            this.DropDownListPage.SelectedIndex = this.CurrentPage;
            this.ViewState["PageIndex"] = this.CurrentPage;
            this.ListBind();
        }

        private void SetPageDisplay(int nPage)
        {
            this.PageSize = (int)this.ViewState["PageSize"];
            this.RecordCount = this.CalculateRecordNumber();
            this.ViewState["RecordCount"] = this.RecordCount;
            this.lblRecordCount.Text = this.RecordCount.ToString();
            if ((this.RecordCount % this.PageSize) == 0)
            {
                this.PageCount = this.RecordCount / this.PageSize;
            }
            else
            {
                this.PageCount = (this.RecordCount / this.PageSize) + 1;
            }
            if (this.RecordCount == 0)
            {
                this.PageCount = 1;
            }
            this.lblPageCount.Text = this.PageCount.ToString();
            this.ViewState["PageCount"] = this.PageCount;
            this.CurrentPage = (nPage < this.PageCount) ? nPage : (this.PageCount - 1);
            this.ViewState["PageIndex"] = this.CurrentPage;
            string[] strArray = new string[this.PageCount];
            for (int i = 1; i <= this.PageCount; i++)
            {
                int index = i - 1;
                strArray[index] = "第" + i.ToString() + "页";
            }
            this.DropDownListPage.DataSource = strArray;
            this.DropDownListPage.DataBind();
            if (this.RecordCount > 0)
            {
                this.DropDownListPage.SelectedIndex = this.CurrentPage;
            }
        }
    }
}
