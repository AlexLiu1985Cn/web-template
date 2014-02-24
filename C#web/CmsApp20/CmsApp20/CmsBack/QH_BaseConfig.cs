using _BLL;
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TProcess;
using _commen;

namespace CmsApp20.CmsBack
{
    public class QH_BaseConfig : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnSave;
        private CreateStatic CS;
        protected HtmlInputFile FaviconFile;
        protected HtmlForm form1;
        protected HtmlInputFile myFile;
        protected string SiteDescription = "";
        protected string SiteKeyword = "";
        protected string SitePath = "";
        protected string SiteTitle = "";
        protected string strCompanyName = "";
        protected string strFaviconUrl = "";
        protected string strFLOff = "";
        protected string strFLOn = "";
        protected string strLangOff = "";
        protected string strLangOn = "";
        protected string strLogoUrl = "";
        protected HtmlSelect TMShowType;

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string str8;
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
            str8 = base.Request.Form["FaviconUrl"].Trim();
            str2 = this.FaviconFile.Value;
            postedFile = this.FaviconFile.PostedFile;
            contentLength = postedFile.ContentLength;
            if (contentLength != 0)
            {
                string str9 = base.Server.MapPath("../upload/");
                if (!Directory.Exists(str9))
                {
                    Directory.CreateDirectory(str9);
                }
                string str10 = str2.Substring(str2.LastIndexOf(".") + 1).ToUpper();
                if (str10 != "ICO")
                {
                    base.Response.Write("<script>alert('对不起，请选择正确的图片！');</script>");
                    this.GetSiteInfoContents();
                    return;
                }
                string str11 = "";
                if (contentLength > this.Bll1.GetPicSize(ref str11))
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "2big", "<script>alert('对不起，栏目图片大小不能大于" + str11 + "');</script>");
                    this.GetSiteInfoContents();
                    return;
                }
                string str12 = (string)this.ViewState["FaviconUrl"];
                if (str12 != "")
                {
                    string str13 = base.Server.MapPath(str12);
                    if (File.Exists(str13))
                    {
                        File.Delete(str13);
                    }
                }
                str8 = "../upload/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "." + str10;
                postedFile.SaveAs(base.Server.MapPath(str8));
            }
            try
            {
                string strField = "SiteTitle,SiteDescription,SiteKeyword,SiteLogo,FriendL,Favicon ,TMShowType,CompanyName,MultiLang,id";
                string[] astrValue = new string[] { base.Request.Form["SiteTitle1"].Trim(), base.Request.Form["description"].Trim(), base.Request.Form["keywords"].Trim(), path, base.Request.Form["FriendL"].Trim(), str8, this.TMShowType.Value, base.Request.Form["CompanyName"].Trim(), base.Request.Form["MultiLang"].Trim(), (string)this.ViewState["id"] };
                string strUpdate = "UPDATE QH_SiteInfo SET SiteTitle=@SiteTitle,SiteDescription=@SiteDescription ,SiteKeyword=@SiteKeyword,SiteLogo=@SiteLogo,FriendL=@FriendL,Favicon=@Favicon,TMShowType=@TMShowType,CompanyName=@CompanyName,MultiLang=@MultiLang WHERE ID=@id";
                if (this.Bll1.UpdateTable(strUpdate, strField, astrValue) == 1)
                {
                    string str16 = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select TemplatePath from TemplateInfo where IsUsed=true and IsMobile=false"));
                    string str17 = base.Server.MapPath(@"..\template\") + str16 + "/Module_home.html";
                    if (File.Exists(str17))
                    {
                        this.CS.CreateHome(str17, "");
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
                SystemError.CreateErrorLog("修改基本设置数据库记录错误：" + exception.ToString());
                base.Response.Write("<script>alert('修改基本设置错误！');</script>");
            }
            this.GetSiteInfoContents();
        }

        private void changeStatisticJS(string strSitePath)
        {
            string str = base.Server.MapPath("../Ajax/Statistics.js");
            string templateContent = this.CS.GetTemplateContent(str);
            int index = templateContent.IndexOf("var vDomain=\"");
            int num2 = templateContent.IndexOf("\"", (int)(index + 13));
            string oldValue = templateContent.Substring(index, num2 - index);
            templateContent = templateContent.Replace(oldValue, "var vDomain=\"" + strSitePath);
            this.Bll1.WriteFile(str, templateContent);
        }

        private void GetSiteInfoContents()
        {
            string[] astrRet = new string[11];
            string strQuery = "select top 1 id,SiteTitle,SiteDescription,SiteKeyword,SitePath,SiteLogo,FriendL,Favicon,TMShowType,CompanyName,MultiLang from QH_SiteInfo ";
            this.Bll1.ReadDataReader(strQuery, ref astrRet, 11);
            this.ViewState["id"] = astrRet[0];
            this.SiteTitle = astrRet[1];
            this.SiteDescription = astrRet[2];
            this.SiteKeyword = astrRet[3];
            this.ViewState["SitePath"] = this.SitePath = astrRet[4];
            this.ViewState["LogoUrl"] = this.strLogoUrl = astrRet[5];
            this.ViewState["FaviconUrl"] = this.strFaviconUrl = astrRet[7];
            if (astrRet[6] == "True")
            {
                this.strFLOn = "Checked";
                this.strFLOff = "";
            }
            else
            {
                this.strFLOn = "";
                this.strFLOff = "Checked";
            }
            if (astrRet[10] == "True")
            {
                this.strLangOn = "Checked";
                this.strLangOff = "";
            }
            else
            {
                this.strLangOn = "";
                this.strLangOff = "Checked";
            }
            this.TMShowType.Value = astrRet[8];
            this.strCompanyName = astrRet[9];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CS = new CreateStatic(base.Request.Url.AbsoluteUri);
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.GetSiteInfoContents();
            }
        }
    }
}
