using _BLL;
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _commen;

namespace CmsApp20.CmsBack
{
    public class MobileSiteSet : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnSave;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected string MobilePath = "";
        protected HtmlInputFile myFile;
        protected string strLogoUrl = "";
        protected string strMOff = "";
        protected string strMOn = "";

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string path = base.Request.Form["LogoUrl"].Trim();
            string str2 = this.myFile.Value;
            HttpPostedFile postedFile = this.myFile.PostedFile;
            int contentLength = postedFile.ContentLength;
            if (contentLength != 0)
            {
                string str3 = base.Server.MapPath("../upload/");
                if (!Directory.Exists(str3))
                {
                    Directory.CreateDirectory(str3);
                }
                string str4 = str2.Substring(str2.LastIndexOf(".") + 1).ToUpper();
                switch (str4)
                {
                    case "JPG":
                    case "BMP":
                    case "GIF":
                    case "PNG":
                        {
                            string strSize = "";
                            if (contentLength > this.Bll1.GetPicSize(ref strSize))
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "2big", "<script>alert('对不起，栏目图片大小不能大于" + strSize + "');</script>");
                                this.GetSiteInfoContents();
                                return;
                            }
                            string str6 = (string)this.ViewState["LogoUrl"];
                            if (str6 != "")
                            {
                                string str7 = base.Server.MapPath(str6);
                                if (File.Exists(str7))
                                {
                                    File.Delete(str7);
                                }
                            }
                            path = "../upload/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "." + str4;
                            postedFile.SaveAs(base.Server.MapPath(path));
                            goto Label_019B;
                        }
                }
                base.Response.Write("<script>alert('对不起，请选择正确的图片！');</script>");
                this.GetSiteInfoContents();
                return;
            }
        Label_019B:
            try
            {
                string strField = "MobileOn,MobileLogo,id";
                string[] astrValue = new string[] { base.Request.Form["MobileOn"].Trim(), path, (string)this.ViewState["id"] };
                string strUpdate = "UPDATE QH_SiteInfo SET MobileOn=@MobileOn,MobileLogo=@MobileLogo WHERE ID=@id";
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
                SystemError.CreateErrorLog("修改手机网站设置数据库记录错误：" + exception.ToString());
                base.Response.Write("<script>alert('修改手机网站设置设置错误！');</script>");
            }
            this.GetSiteInfoContents();
        }

        private void GetSiteInfoContents()
        {
            string[] astrRet = new string[4];
            string strQuery = "select top 1 id,MobileOn,MobilePath,MobileLogo from QH_SiteInfo ";
            this.Bll1.ReadDataReader(strQuery, ref astrRet, 4);
            this.ViewState["id"] = astrRet[0];
            this.MobilePath = astrRet[2];
            this.ViewState["LogoUrl"] = this.strLogoUrl = astrRet[3];
            if (astrRet[1] == "True")
            {
                this.strMOn = "Checked";
                this.strMOff = "";
            }
            else
            {
                this.strMOn = "";
                this.strMOff = "Checked";
            }
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
