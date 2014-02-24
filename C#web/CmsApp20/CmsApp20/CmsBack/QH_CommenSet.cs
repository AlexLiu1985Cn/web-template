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

    public class QH_CommenSet : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnSave;
        private CreateStatic CS;
        protected HtmlForm form1;
        protected string LiuyanQQNo = "";
        protected string strAddress = "";
        protected string strAuthor = "";
        protected string strBak1 = "";
        protected string strBak2 = "";
        protected string strBakImg1 = "";
        protected string strBakImg2 = "";
        protected string strContact = "";
        protected string strCopyrightInfo = "";
        protected string strEmail = "";
        protected string strFax = "";
        protected string strICPBackup = "";
        protected string strMobile = "";
        protected string strTel = "";
        protected string ZaixianQQNo = "";

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strField = "LiuyanQQNo,ZaixianQQNo,CopyrightInfo,Tel,Address,ICPBackup,Fax,Mobile,Email,Contact,Author,id";
                string[] astrValue = new string[] { base.Request.Form["LiuyanQQNo1"].Trim(), base.Request.Form["ZaixianQQNo1"].Trim(), base.Request.Form["CopyrightInfo"].Trim(), base.Request.Form["Tel"].Trim(), base.Request.Form["Address"].Trim(), base.Request.Form["ICPBackup"].Trim(), base.Request.Form["Fax"].Trim(), base.Request.Form["Mobile"].Trim(), base.Request.Form["Email"].Trim(), base.Request.Form["Contact"].Trim(), base.Request.Form["Author"].Trim(), (string)this.ViewState["id"] };
                string strUpdate = "UPDATE QH_SiteInfo SET LiuyanQQNo=@LiuyanQQNo,ZaixianQQNo=@ZaixianQQNo ,CopyrightInfo=@CopyrightInfo,Tel=@Tel,Address=@Address,ICPBackup=@ICPBackup ,Fax=@Fax,Mobile=@Mobile,Email=@Email,Contact=@Contact,Author=@Author WHERE ID=@id";
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
                SystemError.CreateErrorLog("修改网站常用信息数据库记录错误：" + exception.ToString());
                base.Response.Write("<script>alert('修改网站常用信息错误！');</script>");
            }
            this.GetSiteInfoContents();
        }

        private void GetSiteInfoContents()
        {
            string[] astrRet = new string[12];
            string strQuery = "select top 1 id,LiuyanQQNo,ZaixianQQNo,CopyrightInfo,[Tel],Address,ICPBackup,Fax,Mobile,Email,Contact,Author from QH_SiteInfo ";
            this.Bll1.ReadDataReader(strQuery, ref astrRet, 12);
            this.ViewState["id"] = astrRet[0];
            this.LiuyanQQNo = astrRet[1];
            this.ZaixianQQNo = astrRet[2];
            this.strCopyrightInfo = astrRet[3];
            this.strTel = astrRet[4];
            this.strAddress = astrRet[5];
            this.strICPBackup = astrRet[6];
            this.strFax = astrRet[7];
            this.strMobile = astrRet[8];
            this.strEmail = astrRet[9];
            this.strContact = astrRet[10];
            this.strAuthor = astrRet[11];
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
