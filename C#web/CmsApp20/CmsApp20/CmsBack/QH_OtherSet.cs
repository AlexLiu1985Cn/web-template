namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using TProcess;
    using _commen;

    public class QH_OtherSet : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnSave;
        private CreateStatic CS;
        protected HtmlForm form1;
        protected string strAlert = "";
        protected string strBottom1 = "";
        protected string strBottom2 = "";
        protected string strBottom3 = "";
        protected string strBottomQT = "";
        protected string strJScript = "";
        protected HtmlTextArea TxtContent;

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strField = "FirstPageCnt,Bottom1,Bottom2,Bottom3,BottomQT,JScript,id";
                string[] astrValue = new string[] { this.TxtContent.Value.Replace("&lt;", "<").Replace("&gt;", ">").Replace("#39;", "'"), base.Request.Form["Bottom1"].Trim(), base.Request.Form["Bottom2"].Trim(), base.Request.Form["Bottom3"].Trim(), base.Request.Form["BottomQT"].Trim(), base.Request["JScript"].Trim().Replace("&lt;", "<").Replace("&gt;", ">"), (string)this.ViewState["id"] };
                string strUpdate = "UPDATE QH_SiteInfo SET FirstPageCnt=@FirstPageCnt,Bottom1=@Bottom1 ,Bottom2=@Bottom2,Bottom3=@Bottom3,BottomQT=@BottomQT,JScript=@JScript WHERE ID=@id";
                if (this.Bll1.UpdateTable(strUpdate, strField, astrValue) == 1)
                {
                    string str3 = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select TemplatePath from TemplateInfo where IsUsed=true and IsMobile=false"));
                    string path = base.Server.MapPath(@"..\template\") + str3 + "/Module_home.html";
                    if (File.Exists(path))
                    {
                        this.CS.CreateHome(path, "");
                    }
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "UpdateOk", "<script>alert('修改成功！');</script>");
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "UpdateFail", "<script>alert('修改失败！');</script>");
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("修改其它设置数据库记录错误：" + exception.ToString());
                base.Response.Write("<script>alert('修改其它设置错误！');</script>");
            }
            this.GetSiteInfoContents();
        }

        private void GetSiteInfoContents()
        {
            string[] astrRet = new string[7];
            string strQuery = "select top 1 id,FirstPageCnt,Bottom1,Bottom2,Bottom3,BottomQT,JScript from QH_SiteInfo ";
            this.Bll1.ReadDataReader(strQuery, ref astrRet, 7);
            this.ViewState["id"] = astrRet[0];
            this.TxtContent.Value = astrRet[1];
            this.strBottom1 = astrRet[2];
            this.strBottom2 = astrRet[3];
            this.strBottom3 = astrRet[4];
            this.strBottomQT = astrRet[5];
            this.strJScript = astrRet[6];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page))
            {
                this.CS = new CreateStatic(base.Request.Url.AbsoluteUri);
                if (!base.IsPostBack)
                {
                    this.GetSiteInfoContents();
                }
            }
        }
    }
}
