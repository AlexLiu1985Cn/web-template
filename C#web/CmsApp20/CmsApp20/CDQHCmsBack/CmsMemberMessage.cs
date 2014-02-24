namespace CmsApp20.CDQHCmsBack
{
    using _BLL;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class CmsMemberMessage : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnClr;
        protected Button Button1;
        protected CheckBox Checkbox1;
        private int CurrentPage;
        protected DropDownList DDLFps;
        protected DropDownList DDLPage;
        protected DropDownList DropDownListPage;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Label lblCurrentPage;
        protected Label lblPageCount;
        protected Label lblRecordCount;
        protected LinkButton lbnNextPage;
        protected LinkButton lbnPrevPage;
        private int PageCount;
        private int PageSize;
        private int RecordCount;
        protected Repeater RepeaterNews;
        protected string strBackUrl;
        protected string strClmnID;
        private string strID1;
        private string strID2;
        protected string strPage;
        protected string strSel;
        private string strWhere = "";

        protected void BtnClr_Click(object sender, EventArgs e)
        {
            string str = "delete from QH_MessageMember ";
            string str2 = "";
            switch (this.DDLFps.SelectedIndex)
            {
                case 0:
                    str2 = "";
                    break;

                case 1:
                    str2 = "where ISNULL(Reply) or Reply='' ";
                    break;

                case 2:
                    str2 = "where Reply<>'' ";
                    break;

                case 3:
                    str2 = "where readok=False ";
                    break;

                case 4:
                    str2 = "where readok=true ";
                    break;
            }
            this.ChangePageCount(this.Bll1.Delete(str + str2));
        }

        protected void ButtondeleteSV_Clicked(object sender, EventArgs e)
        {
            int deletecount = 0;
            int num2 = 0;
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            foreach (RepeaterItem item in this.RepeaterNews.Items)
            {
                HtmlInputCheckBox box = (HtmlInputCheckBox)item.FindControl("checkbox");
                if (box.Checked)
                {
                    string str = ((HtmlInputHidden)item.FindControl("SelectedID")).Value;
                    builder.Append(",'" + str + "'");
                    builder2.Append("," + str);
                    num2++;
                }
            }
            if (builder2.Length > 0)
            {
                deletecount = this.Bll1.Delete("delete from QH_MessageMember where id in(" + builder2.ToString().Substring(1) + ")");
            }
            if (deletecount > 0)
            {
                this.ChangePageCount(deletecount);
                if (num2 == deletecount)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "dSuccess", @"<script>alert('删除成功！\n'); </script>");
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "dfailure", "<script>alert('删除失败！'); </script>");
                }
            }
        }

        private int CalculateRecordNumber()
        {
            return this.Bll1.DAL1.ExecuteReader("select count(*) as co from QH_MessageMember " + ((string)this.ViewState["QWhere"]));
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
            this.strClmnID = (string)this.ViewState["ClmnID"];
        }

        public void CheckboxslctallSV_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            if (box.Checked)
            {
                foreach (RepeaterItem item in this.RepeaterNews.Items)
                {
                    HtmlInputCheckBox box2 = (HtmlInputCheckBox)item.FindControl("checkbox");
                    box2.Checked = true;
                }
            }
            else
            {
                foreach (RepeaterItem item2 in this.RepeaterNews.Items)
                {
                    HtmlInputCheckBox box3 = (HtmlInputCheckBox)item2.FindControl("checkbox");
                    box3.Checked = false;
                }
            }
        }

        private ICollection CreateSource()
        {
            string strQuery = "select * ,iif(IsNull(Reply)=True,'否','是') as Reply1 from QH_MessageMember " + ((string)this.ViewState["QWhere"]) + " order by id desc";
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
            table.Columns.Add("AddDate1");
            foreach (DataRow row in table.Rows)
            {
                if (row["AddDate"].ToString() != string.Empty)
                {
                    row["AddDate1"] = ((DateTime)row["AddDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            return table.DefaultView;
        }

        protected void DDLFps_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.DDLFps.SelectedIndex)
            {
                case 1:
                    this.strWhere = " ISNULL(Reply) or Reply='' ";
                    break;

                case 2:
                    this.strWhere = " Reply<>'' ";
                    break;

                case 3:
                    this.strWhere = " readok=False ";
                    break;

                case 4:
                    this.strWhere = " readok=true ";
                    break;
            }
            this.strWhere = (this.strWhere == "") ? "" : (" where " + this.strWhere);
            this.ViewState["QWhere"] = this.strWhere;
            this.ViewState["Select"] = this.DDLFps.SelectedIndex.ToString();
            this.SetPageDisplay((int)this.ViewState["PageIndex"]);
            this.ListBind();
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
        }

        protected void DropDownListPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CurrentPage = this.DropDownListPage.SelectedIndex;
            this.ViewState["PageIndex"] = this.CurrentPage;
            this.PageCount = (int)this.ViewState["PageCount"];
            this.ListBind();
        }

        public void ListBind()
        {
            this.RepeaterNews.DataSource = this.CreateSource();
            this.RepeaterNews.DataBind();
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
                if ((str2 != "") && (str2 != "0"))
                {
                    this.DDLFps.SelectedIndex = int.Parse(str2);
                    this.DDLFps_SelectedIndexChanged(new object(), new EventArgs());
                    flag = true;
                }
                if (!flag)
                {
                    this.SetPageDisplay(this.CurrentPage);
                    this.ListBind();
                }
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