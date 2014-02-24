namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using _commen;

    public class QH_QQSet : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnSave;
        protected Button Button1;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected HtmlSelect QQStyle;
        protected string strQQOff = "";
        protected string strQQOn = "";

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.DataToBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string str = base.Request.Form["QQOn"].Trim();
            string str2 = base.Request.Form["QQStyle"].Trim();
            str = (str == "1") ? ((str2 == "0") ? "1" : str2) : str;
            string strField = "QQStyle";
            string[] astrValue = new string[] { str };
            string strUpdate = "UPDATE QH_SiteInfo SET QQStyle=@QQStyle WHERE ID=1";
            if (this.Bll1.UpdateTable(strUpdate, strField, astrValue) == 1)
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
            string input = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select top 1 QQStyle from QH_SiteInfo "));
            if (!Regex.IsMatch(input, @"^\d+$"))
            {
                input = "0";
            }
            string str2 = input;
            if (str2 != null)
            {
                if (!(str2 == "0"))
                {
                    if (!(str2 == "1") && !(str2 == "2"))
                    {
                        return;
                    }
                }
                else
                {
                    this.strQQOn = "";
                    this.strQQOff = "Checked";
                    this.QQStyle.SelectedIndex = 0;
                    return;
                }
                this.strQQOn = "Checked";
                this.strQQOff = "";
                this.QQStyle.SelectedIndex = int.Parse(input);
            }
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