namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class MobileBannerSet : Page
    {
        private BLL Bll1 = new BLL();
        protected HtmlSelect BnrModeM;
        protected Button BtnSave;
        protected Button Button1;
        protected HtmlForm form1;
        protected HtmlInputText HeighthM;
        protected HtmlInputText WidthM;

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.DataToBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string strField = "BnrMode,BnrWidth,BnrHeight,id";
            string[] astrValue = new string[] { base.Request.Form["BnrModeM"], base.Request.Form["WidthM"], base.Request.Form["HeighthM"], "3" };
            string strUpdate = "UPDATE QH_ClmnBanner SET BnrMode=@BnrMode,BnrWidth=@BnrWidth ,BnrHeight=@BnrHeight WHERE ID=@id";
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
            string[] strArray = this.Bll1.DAL1.ReadDataReaderStringArray("select BnrMode,BnrWidth,BnrHeight from QH_ClmnBanner where id=3", 3);
            string str = strArray[0];
            if (str != null)
            {
                if (!(str == "0"))
                {
                    if (str == "1")
                    {
                        this.BnrModeM.SelectedIndex = 1;
                    }
                    else if (str == "3")
                    {
                        this.BnrModeM.SelectedIndex = 2;
                    }
                }
                else
                {
                    this.BnrModeM.SelectedIndex = 0;
                }
            }
            this.WidthM.Value = strArray[1];
            this.HeighthM.Value = strArray[2];
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