namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using _commen;

    public class QH_PicEffect : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnSave;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected HtmlInputText img_x;
        protected HtmlInputText img_y;
        protected HtmlSelect ImgListStyle;
        protected HtmlInputText imgP_x;
        protected HtmlInputText imgP_y;
        protected HtmlSelect ImgStyle;
        protected HtmlSelect picStyle;

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strField = "picStyle,ImgStyle,ImgListStyle,id";
                string[] astrValue = new string[] { base.Request.Form["picStyle"] + "|" + this.imgP_x.Value + "|" + this.imgP_y.Value, base.Request.Form["ImgStyle"] + "|" + this.img_x.Value + "|" + this.img_y.Value, base.Request.Form["ImgListStyle"], (string)this.ViewState["id"] };
                string strUpdate = "UPDATE QH_SiteInfo SET picStyle=@picStyle,ImgStyle=@ImgStyle,ImgListStyle=@ImgListStyle WHERE ID=@id";
                if (this.Bll1.UpdateTable(strUpdate, strField, astrValue) == 1)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "UpdateOk", "<script>alert('修改成功！');</script>");
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "UpdateFail", "<script>alert('修改失败！');</script>");
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("修改图片显示效果记录错误：" + exception.ToString());
                base.Response.Write("<script>alert('修改图片显示效果错误！');</script>");
            }
            this.GetSiteInfoContents();
        }

        private void GetSiteInfoContents()
        {
            string[] astrRet = new string[4];
            string strQuery = "select top 1 id,picStyle,ImgStyle,ImgListStyle from QH_SiteInfo ";
            this.Bll1.ReadDataReader(strQuery, ref astrRet, 4);
            this.ViewState["id"] = astrRet[0];
            string[] strArray2 = ((astrRet[1] == "") ? "0" : astrRet[1]).Split(new char[] { '|' });
            int[] numArray = new int[] { 0, 1, 4, 5, 2, 3 };
            this.picStyle.SelectedIndex = numArray[int.Parse(strArray2[0])];
            if (strArray2.Length == 3)
            {
                this.imgP_x.Value = strArray2[1];
                this.imgP_y.Value = strArray2[2];
            }
            string[] strArray3 = ((astrRet[2] == "") ? "0" : astrRet[2]).Split(new char[] { '|' });
            this.ImgStyle.SelectedIndex = int.Parse(strArray3[0]);
            if (strArray2.Length == 3)
            {
                this.img_x.Value = strArray3[1];
                this.img_y.Value = strArray3[2];
            }
            string s = (astrRet[3] == "") ? "0" : astrRet[3];
            this.ImgListStyle.SelectedIndex = int.Parse(s);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.GetSiteInfoContents();
            }
        }
    }
}