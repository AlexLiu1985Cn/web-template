namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Data.OleDb;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class QH_StatisticsSet : Page
    {
        private string[] astrStat;
        private string[] astrStat3;
        private BLL Bll1 = new BLL();
        protected Button BtnReset;
        protected Button BtnSave;
        protected HtmlInputRadioButton Close;
        protected HtmlForm form1;
        protected LinkButton LBtn1;
        protected HtmlTableRow LinkUrl;
        protected HtmlInputText max;
        protected HtmlInputRadioButton Open;
        protected HtmlSelect SaveData;
        protected HtmlInputRadioButton stat3Off;
        protected HtmlInputRadioButton stat3On;
        protected string strJScriptStat3 = "";
        protected HtmlTableRow titletd;

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.DataToBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string strField = "open,SaveData,PVMax,id";
            string[] astrValue = new string[] { this.Open.Checked ? "1" : "0", base.Request.Form["SaveData"], base.Request.Form["max"], "1" };
            string strUpdate = "UPDATE QH_StatisticSet SET [open]=@open,SaveData=@SaveData ,PVMax=@PVMax WHERE ID=@id";
            int num = this.Bll1.UpdateTable(strUpdate, strField, astrValue);
            int num2 = this.Bll1.DAL1.ExecuteNonQuery("UPDATE QH_SiteInfo SET JScriptStat3=@JScriptStat3,Stat3On=@Stat3On WHERE ID=1", new OleDbParameter[] { new OleDbParameter("@JScriptStat3", base.Request["JScriptStat3"].Trim().Replace("&lt;", "<").Replace("&gt;", ">")), new OleDbParameter("@Stat3On", this.stat3On.Checked ? "1" : "0") });
            if ((num == 1) && (num2 == 1))
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "saveOk", "<script>alert('修改成功！');</script>");
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "saveFail", "<script>alert('修改失败！');</script>");
            }
            this.DataToBind();
        }

        private void DataToBind()
        {
            this.astrStat = this.Bll1.DAL1.ReadDataReaderStringArray("select [open],SaveData,PVMax from QH_StatisticSet where id=1 ", 3);
            if (this.astrStat[2] != null)
            {
                if (this.astrStat[0] == "True")
                {
                    this.Open.Checked = true;
                    this.Close.Checked = false;
                }
                else
                {
                    this.Open.Checked = false;
                    this.Close.Checked = true;
                }
                this.SaveData.SelectedIndex = int.Parse(this.astrStat[1]) - 1;
                this.max.Value = this.astrStat[2];
                this.astrStat3 = this.Bll1.DAL1.ReadDataReaderStringArray("select JScriptStat3,Stat3On from QH_SiteInfo where id=1 ", 2);
                if (this.astrStat3[1] != null)
                {
                    if (this.astrStat3[1] == "True")
                    {
                        this.stat3On.Checked = true;
                        this.stat3Off.Checked = false;
                    }
                    else
                    {
                        this.stat3On.Checked = false;
                        this.stat3Off.Checked = true;
                    }
                    this.strJScriptStat3 = this.astrStat3[0];
                    this.Page.DataBind();
                }
            }
        }

        protected void LBtn1_Click(object sender, EventArgs e)
        {
            this.Bll1.DAL1.ExecuteNonQuery("delete * from QH_StatDay");
            this.Bll1.DAL1.ExecuteNonQuery("delete from QH_Statistics where ATime<#" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00#");
            this.Bll1.DAL1.ExecuteNonQuery("update QH_StatisticSet set ClearTime='" + DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + "' where id=1 ");
            this.DataToBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.DataToBind();
            }
        }
    }
}