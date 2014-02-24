namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using _commen;

    public class QH_SafeSet : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnSave;
        protected HtmlForm form1;
        protected LinkButton LB1;
        protected string strAdminDir = "";
        protected string strAdminSsnT = "";
        protected string strAntiOff = "";
        protected string strAntiOn = "";
        protected string strChkOff = "";
        protected string strChkOn = "";
        protected string strDelTip = "";
        protected string strDomian = "";
        protected string strFileExt = "";
        protected string strFileMax = "";
        protected string strImgMax = "";
        protected HtmlTableRow trDel;

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string str = (string)this.ViewState["ADir"];
                string strField = "AdminDir,UpPicSize,UpFileSize,AdminCH,AdminSsnT,FileExt,AntiDown,id";
                string[] astrValue = new string[] { str, base.Request.Form["Img_max"].Trim(), base.Request.Form["file_max"].Trim(), base.Request.Form["ChkCode"].Trim(), base.Request.Form["AdminSsnT"].Trim(), base.Request.Form["FileExt"].Trim(), base.Request.Form["AntiDown"].Trim(), (string)this.ViewState["id"] };
                string strUpdate = "UPDATE QH_SiteInfo SET AdminDir=@AdminDir,UpPicSize=@UpPicSize ,UpFileSize=@UpFileSize,AdminCH=@AdminCH,AdminSsnT=@AdminSsnT,FileExt=@FileExt,AntiDown=@AntiDown WHERE ID=@id";
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
                SystemError.CreateErrorLog("修改安全设置数据库记录错误：" + exception.ToString());
                base.Response.Write("<script>alert('修改安全设置错误！');</script>");
            }
            this.GetSiteInfoContents();
        }

        private void GetSiteInfoContents()
        {
            if (Directory.Exists(base.Server.MapPath("../Install")))
            {
                this.trDel.Style.Add("display", "block");
                this.strDelTip = "<font color=red>安装文件夹install尚未删除，删除后可以增强网站的安全性能，建议</font>";
            }
            else
            {
                this.trDel.Style.Add("display", "none");
            }
            this.strDomian = base.Request.Url.AbsoluteUri;
            this.strChkOn = "Checked";
            string[] astrRet = new string[8];
            string strQuery = "select top 1 id,AdminDir,UpPicSize,UpFileSize,AdminCH,AdminSsnT,FileExt,AntiDown from QH_SiteInfo ";
            this.Bll1.ReadDataReader(strQuery, ref astrRet, 8);
            this.ViewState["id"] = astrRet[0];
            int num = this.strDomian.LastIndexOf('/');
            int num2 = this.strDomian.LastIndexOf('/', num - 1);
            this.ViewState["ADir"] = this.strAdminDir = this.strDomian.Substring(num2 + 1, (num - num2) - 1);
            this.strDomian = this.strDomian.Substring(0, num + 1);
            this.strImgMax = astrRet[2];
            this.strFileMax = astrRet[3];
            if (astrRet[4] == "True")
            {
                this.strChkOn = "Checked";
                this.strChkOff = "";
            }
            else
            {
                this.strChkOn = "";
                this.strChkOff = "Checked";
            }
            this.strAdminSsnT = astrRet[5];
            this.strFileExt = astrRet[6];
            if (astrRet[7] == "True")
            {
                this.strAntiOn = "Checked";
                this.strAntiOff = "";
            }
            else
            {
                this.strAntiOn = "";
                this.strAntiOff = "Checked";
            }
        }

        protected void LB1_Click(object sender, EventArgs e)
        {
            Directory.Delete(base.Server.MapPath("../Install"), true);
            this.GetSiteInfoContents();
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