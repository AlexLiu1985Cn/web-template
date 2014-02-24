namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using _commen;

    public class TemplateShow : Page
    {
        private BLL Bll1 = new BLL();
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Repeater RD;
        protected string strTMShowType = "0";

        private void ChangeTemplateUsedState(string strID, string IsUsed)
        {
            try
            {
                this.Bll1.DAL1.ExecuteNonQuery("Update TemplateInfo Set IsUsed=" + IsUsed + " where id=" + strID);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("修改模板使用状态错误： " + exception.ToString());
            }
        }

        private void DataToBind()
        {
            DataSet dataSet;
            object obj2 = this.Bll1.DAL1.ExecuteScalar("select TMShowType from QH_SiteInfo where id=1 ");
            if (obj2 != null)
            {
                this.strTMShowType = Convert.ToString(obj2);
            }
            if (this.strTMShowType == "")
            {
                this.strTMShowType = "0";
            }
            string strQuery = "select * from TemplateInfo where TemplateType in (0,1,2,3) and IsMobile=false ";
            try
            {
                dataSet = this.Bll1.DAL1.GetDataSet(strQuery);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("读模板数据库记录错误：" + exception.ToString());
                return;
            }
            int count = dataSet.Tables[0].Rows.Count;
            if ((count != 0) && (dataSet != null))
            {
                dataSet.Tables[0].Columns.Add(new DataColumn("AddDate1"));
                dataSet.Tables[0].Columns.Add(new DataColumn("UsedID"));
                dataSet.Tables[0].Columns.Add(new DataColumn("Abstract1"));
                string str2 = "";
                string str3 = "";
                for (int i = 0; i < count; i++)
                {
                    if (dataSet.Tables[0].Rows[i]["adddate"].ToString().Trim() != "")
                    {
                        dataSet.Tables[0].Rows[i]["AddDate1"] = ((DateTime)dataSet.Tables[0].Rows[i]["adddate"]).ToString("yyyy-MM-dd");
                    }
                    if (dataSet.Tables[0].Rows[i]["IsUsed"].ToString() == "True")
                    {
                        str2 = dataSet.Tables[0].Rows[i]["id"].ToString();
                    }
                    str3 = dataSet.Tables[0].Rows[i]["Abstract"].ToString();
                    dataSet.Tables[0].Rows[i]["Abstract1"] = (str3.Length > 12) ? str3.Substring(0, 12) : str3;
                }
                for (int j = 0; j < count; j++)
                {
                    dataSet.Tables[0].Rows[j]["UsedID"] = str2;
                }
                this.RD.DataSource = dataSet;
                this.RD.DataBind();
            }
        }

        private void DataTobindBuy()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.DataToBind();
            }
        }

        protected void R1_ItemCommand(object Sender, RepeaterCommandEventArgs e)
        {
            foreach (RepeaterItem item in this.RD.Items)
            {
                LinkButton button = (LinkButton)item.FindControl("LinkBttnUse");
                if (button.Text == "正在现用")
                {
                    HtmlGenericControl control = (HtmlGenericControl)item.FindControl("deflst");
                    control.Style.Add("background-color", "#ffffff");
                    button.Text = "使用此模板";
                    button.Enabled = true;
                    string strID = ((HtmlInputHidden)item.FindControl("SelectedID")).Value;
                    this.ChangeTemplateUsedState(strID, "0");
                }
            }
            LinkButton button2 = (LinkButton)e.Item.FindControl("LinkBttnUse");
            if (button2.Text == "使用此模板")
            {
                HtmlGenericControl control2 = (HtmlGenericControl)e.Item.FindControl("deflst");
                control2.Style.Add("background-color", "#cfe0ed");
                button2.Text = "正在现用";
                button2.Enabled = false;
                button2.Attributes.Add("disabled", "disabled");
                string str2 = ((HtmlInputHidden)e.Item.FindControl("SelectedID")).Value;
                this.ChangeTemplateUsedState(str2, "1");
            }
            this.DataToBind();
        }

        protected void R1_ItemDataBound(object Sender, RepeaterItemEventArgs e)
        {
            if (((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) && (((DataRowView)e.Item.DataItem)["IsUsed"].ToString() == "True"))
            {
                LinkButton button = (LinkButton)e.Item.FindControl("LinkBttnUse");
                HtmlGenericControl control = (HtmlGenericControl)e.Item.FindControl("deflst");
                control.Style.Add("background-color", "#bcdfe9");
                button.Style.Add("color", "#777777");
                button.Text = "正在现用";
                button.Enabled = false;
            }
        }
    }
}