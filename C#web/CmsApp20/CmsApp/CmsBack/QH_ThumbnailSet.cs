namespace CmsApp.CmsBack
{
    using _BLL;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class QH_ThumbnailSet : Page
    {
        protected HtmlTableRow A;
        private BLL Bll1 = new BLL();
        protected Button BtnReset;
        protected Button BtnSave;
        protected HtmlForm form1;
        protected HtmlTableRow I;
        protected HtmlInputText img_x;
        protected HtmlInputText img_y;
        protected HtmlInputText imgI_x;
        protected HtmlInputText imgI_y;
        protected HtmlInputText imgN_x;
        protected HtmlInputText imgN_y;
        protected HtmlInputText imgP_x;
        protected HtmlInputText imgP_y;
        protected HtmlInputRadioButton kind1;
        protected HtmlInputRadioButton kind2;
        protected HtmlInputRadioButton kind3;
        protected HtmlTableRow N;
        protected HtmlTableRow P;
        protected HtmlInputRadioButton SizeS1;
        protected HtmlInputRadioButton SizeS2;

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.DataToBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string strField = "thumb_kind,SizeSet,thumbAll,thumbPt,thumbImg,thumbNews,id";
            string[] astrValue = new string[] { base.Request.Form["thumb_kind"], base.Request.Form["img_size"], this.img_x.Value.Trim() + "x" + this.img_y.Value.Trim(), this.imgP_x.Value.Trim() + "x" + this.imgP_y.Value.Trim(), this.imgI_x.Value.Trim() + "x" + this.imgI_y.Value.Trim(), this.imgN_x.Value.Trim() + "x" + this.imgN_y.Value.Trim(), "1" };
            string strUpdate = "UPDATE QH_ImgSet SET thumb_kind=@thumb_kind,SizeSet=@SizeSet ,thumbAll=@thumbAll,thumbPt=@thumbPt,thumbImg=@thumbImg,thumbNews=@thumbNews WHERE ID=@id";
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
            DataTable dataTable = this.Bll1.GetDataTable("select thumb_kind,SizeSet,thumbAll,thumbPt,thumbImg,thumbNews from QH_ImgSet where id=1");
            if ((dataTable != null) && (dataTable.Rows.Count != 0))
            {
                string str = dataTable.Rows[0]["thumb_kind"].ToString();
                if (str != null)
                {
                    if (!(str == "1"))
                    {
                        if (str == "2")
                        {
                            this.kind2.Checked = true;
                            this.kind1.Checked = false;
                            this.kind3.Checked = false;
                        }
                        else if (str == "3")
                        {
                            this.kind3.Checked = true;
                            this.kind2.Checked = false;
                            this.kind1.Checked = false;
                        }
                    }
                    else
                    {
                        this.kind1.Checked = true;
                        this.kind2.Checked = false;
                        this.kind3.Checked = false;
                    }
                }
                string str2 = dataTable.Rows[0]["SizeSet"].ToString();
                if (str2 != null)
                {
                    if (!(str2 == "0"))
                    {
                        if (str2 == "1")
                        {
                            this.SizeS2.Checked = true;
                            this.SizeS1.Checked = false;
                            this.A.Style.Add("display", "none");
                            this.P.Style.Add("display", "");
                            this.I.Style.Add("display", "");
                            this.N.Style.Add("display", "");
                        }
                    }
                    else
                    {
                        this.SizeS1.Checked = true;
                        this.SizeS2.Checked = false;
                        this.A.Style.Add("display", "");
                        this.P.Style.Add("display", "none");
                        this.I.Style.Add("display", "none");
                        this.N.Style.Add("display", "none");
                    }
                }
                string[] strArray = dataTable.Rows[0]["thumbAll"].ToString().Split(new char[] { 'x' });
                this.img_x.Value = strArray[0];
                this.img_y.Value = strArray[1];
                string[] strArray2 = dataTable.Rows[0]["thumbPt"].ToString().Split(new char[] { 'x' });
                this.imgP_x.Value = strArray2[0];
                this.imgP_y.Value = strArray2[1];
                string[] strArray3 = dataTable.Rows[0]["thumbImg"].ToString().Split(new char[] { 'x' });
                this.imgI_x.Value = strArray3[0];
                this.imgI_y.Value = strArray3[1];
                string[] strArray4 = dataTable.Rows[0]["thumbNews"].ToString().Split(new char[] { 'x' });
                this.imgN_x.Value = strArray4[0];
                this.imgN_y.Value = strArray4[1];
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