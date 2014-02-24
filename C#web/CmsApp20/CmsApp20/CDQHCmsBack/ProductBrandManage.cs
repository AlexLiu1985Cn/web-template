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

    public class ProductBrandManage : Page
    {
        private BLL Bll1 = new BLL();
        private int CurrentPage;
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
        protected Repeater RepeaterProductBrand;

        private ICollection CreateSource()
        {
            bool flag = true;
            int nStart = this.CurrentPage * this.PageSize;
            DataSet set = new DataSet();
            try
            {
                set = this.Bll1.DAL1.GetDataTablePage(nStart, this.PageSize, "select id ,Brand,Logo,Link,[Memo] from QH_ProductBrand order by id asc");
            }
            catch (Exception exception)
            {
                flag = false;
                SystemError.CreateErrorLog("读产品品牌错误： " + exception.ToString());
            }
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                string str = set.Tables[0].Rows[i]["Memo"].ToString();
                if (str.Length > 14)
                {
                    set.Tables[0].Rows[i]["Memo"] = str.Substring(0, 14) + "...";
                }
            }
            if (flag)
            {
                return set.Tables[0].DefaultView;
            }
            base.Response.Write("<script>alert('读产品品牌数据库失败，详细信息请查看错误日志！');</script>");
            return null;
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
            this.RepeaterProductBrand.DataSource = this.CreateSource();
            this.RepeaterProductBrand.DataBind();
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page))
            {
                this.PageSize = 20;
                if (!base.IsPostBack)
                {
                    this.CurrentPage = 0;
                    this.ViewState["PageIndex"] = 0;
                    this.RecordCount = this.Bll1.DAL1.ExecuteReader("select count(*) as co from QH_ProductBrand ");
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
                    this.lblPageCount.Text = this.PageCount.ToString();
                    this.ViewState["PageCount"] = this.PageCount;
                    string[] strArray = new string[this.PageCount];
                    for (int i = 1; i <= this.PageCount; i++)
                    {
                        int index = i - 1;
                        strArray[index] = "第" + i.ToString() + "页";
                    }
                    this.DropDownListPage.DataSource = strArray;
                    this.DropDownListPage.DataBind();
                    this.DropDownListPage.SelectedIndex = this.CurrentPage;
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
    }
}