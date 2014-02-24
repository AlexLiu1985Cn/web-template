namespace CmsApp20.CDQHCmsBack
{
    using _BLL;
    using System;
    using System.Collections;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using _commen;

    public class CmsMemberManage : Page
    {
        private BLL Bll1 = new BLL();
        protected Button ButtonSelect;
        private int CurrentPage;
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
        protected Repeater RepeaterMember;
        protected string strClmnID;
        protected string strPage;
        protected string strRandom;
        protected string strSel;
        protected HtmlInputText user_name;

        protected void BUserNameSrch_Click(object sender, EventArgs e)
        {
            if (this.user_name.Value.Trim() == "")
            {
                this.ViewState["QWhere"] = "";
            }
            else
            {
                this.ViewState["QWhere"] = " where UserName='" + this.user_name.Value.Trim() + "'";
            }
            this.SetPageDisplay(0);
            this.ListBind();
        }

        private int CalculateRecordNumber()
        {
            return this.Bll1.DAL1.ExecuteReader("select count(*) as co from MemberUser " + ((string)this.ViewState["QWhere"]));
        }

        private ICollection CreateSource()
        {
            string strQuery = "select  *,switch(Sex='1','男',Sex='2','女',true, '未知') as Sex from MemberUser " + ((string)this.ViewState["QWhere"]) + " order by UserID desc ";
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
            return table.DefaultView;
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
            this.RepeaterMember.DataSource = this.CreateSource();
            this.RepeaterMember.DataBind();
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page))
            {
                this.strRandom = new Random().Next().ToString();
                if (!base.IsPostBack)
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

        protected void RepeaterMember_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string str;
            HtmlContainerControl control = (HtmlContainerControl)e.Item.FindControl("UserID");
            if (((str = e.CommandName) != null) && (str == "Del"))
            {
                try
                {
                    this.Bll1.DAL1.ExecuteNonQuery("delete from MemberUser where UserID=" + control.InnerText);
                }
                catch (Exception exception)
                {
                    SystemError.CreateErrorLog("删除会员错误： " + exception.ToString());
                }
            }
            this.SetPageDisplay((int)this.ViewState["PageIndex"]);
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