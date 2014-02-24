namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using TProcess;
    using _commen;

    public class QH_OptimizationBak : Page
    {
        protected HtmlInputText Bak1;
        protected HtmlInputText Bak2;
        protected HtmlInputText Bak3;
        private BLL Bll1 = new BLL();
        protected Button BtnSave;
        private CreateStatic CS;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected HtmlInputFile ImgFile1;
        protected HtmlInputFile ImgFile2;
        protected HtmlInputFile ImgFile3;
        protected string strBak1 = "";
        protected string strBak2 = "";
        protected string strBak3 = "";
        protected string strBakImg1 = "";
        protected string strBakImg2 = "";
        protected string strBakImg3 = "";

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string path = base.Request.Form["BakImg1"].Trim();
            string str2 = base.Request.Form["BakImg2"].Trim();
            string str3 = base.Request.Form["BakImg3"].Trim();
            string str4 = this.ImgFile1.Value;
            HttpPostedFile postedFile = this.ImgFile1.PostedFile;
            int contentLength = postedFile.ContentLength;
            string str5 = this.ImgFile2.Value;
            HttpPostedFile file2 = this.ImgFile2.PostedFile;
            int num2 = file2.ContentLength;
            string str6 = this.ImgFile3.Value;
            HttpPostedFile file3 = this.ImgFile3.PostedFile;
            int num3 = file3.ContentLength;
            string str7 = base.Server.MapPath("../upload/");
            if (!Directory.Exists(str7))
            {
                Directory.CreateDirectory(str7);
            }
            if (contentLength != 0)
            {
                string str8 = str4.Substring(str4.LastIndexOf(".") + 1).ToUpper();
                switch (str8)
                {
                    case "JPG":
                    case "BMP":
                    case "GIF":
                    case "PNG":
                        {
                            string strSize = "";
                            if (contentLength > this.Bll1.GetPicSize(ref strSize))
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "12big", "<script>alert('对不起，栏目图片大小不能大于" + strSize + "');</script>");
                                this.GetSiteInfoContents();
                                return;
                            }
                            string str10 = (string)this.ViewState["BakUrl1"];
                            if (str10 != "")
                            {
                                string str11 = base.Server.MapPath(str10);
                                if (File.Exists(str11))
                                {
                                    File.Delete(str11);
                                }
                            }
                            path = "../upload/" + DateTime.Now.ToString("yyyyMMddhhmmssfff1") + "." + str8;
                            postedFile.SaveAs(base.Server.MapPath(path));
                            goto Label_0217;
                        }
                }
                base.Response.Write("<script>alert('对不起，请选择正确的备用图片1！');</script>");
                return;
            }
        Label_0217:
            if (num2 != 0)
            {
                string str12 = str5.Substring(str5.LastIndexOf(".") + 1).ToUpper();
                switch (str12)
                {
                    case "JPG":
                    case "BMP":
                    case "GIF":
                    case "PNG":
                        {
                            string str13 = "";
                            if (num2 > this.Bll1.GetPicSize(ref str13))
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "22big", "<script>alert('对不起，栏目图片大小不能大于" + str13 + "');</script>");
                                this.GetSiteInfoContents();
                                return;
                            }
                            string str14 = (string)this.ViewState["BakUrl2"];
                            if (str14 != "")
                            {
                                string str15 = base.Server.MapPath(str14);
                                if (File.Exists(str15))
                                {
                                    File.Delete(str15);
                                }
                            }
                            str2 = "../upload/" + DateTime.Now.ToString("yyyyMMddhhmmssfff2") + "." + str12;
                            file2.SaveAs(base.Server.MapPath(str2));
                            goto Label_0354;
                        }
                }
                base.Response.Write("<script>alert('对不起，请选择正确的备用图片2！');</script>");
                return;
            }
        Label_0354:
            if (num3 != 0)
            {
                string str16 = str6.Substring(str6.LastIndexOf(".") + 1).ToUpper();
                switch (str16)
                {
                    case "JPG":
                    case "BMP":
                    case "GIF":
                    case "PNG":
                        {
                            string str17 = "";
                            if (num3 > this.Bll1.GetPicSize(ref str17))
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "32big", "<script>alert('对不起，栏目图片大小不能大于" + str17 + "');</script>");
                                this.GetSiteInfoContents();
                                return;
                            }
                            string str18 = (string)this.ViewState["BakUrl3"];
                            if (str18 != "")
                            {
                                string str19 = base.Server.MapPath(str18);
                                if (File.Exists(str19))
                                {
                                    File.Delete(str19);
                                }
                            }
                            str3 = "../upload/" + DateTime.Now.ToString("yyyyMMddhhmmssfff3") + "." + str16;
                            file3.SaveAs(base.Server.MapPath(str3));
                            goto Label_0491;
                        }
                }
                base.Response.Write("<script>alert('对不起，请选择正确的备用图片3！');</script>");
                return;
            }
        Label_0491:
            try
            {
                string strField = "Bak1,Bak2,Bak3,BakImg1,BakImg2,BakImg3,id";
                string[] astrValue = new string[] { this.Bak1.Value.Trim().Replace("&lt;", "<").Replace("&gt;", ">"), this.Bak2.Value.Trim().Replace("&lt;", "<").Replace("&gt;", ">"), this.Bak3.Value.Trim().Replace("&lt;", "<").Replace("&gt;", ">"), path, str2, str3, (string)this.ViewState["id"] };
                string strUpdate = "UPDATE QH_SiteInfo SET Bak1=@Bak1,Bak2=@Bak2,Bak3=@Bak3 ,BakImg1=@BakImg1,BakImg2=@BakImg2,BakImg3=@BakImg3 WHERE ID=@id";
                if (this.Bll1.UpdateTable(strUpdate, strField, astrValue) == 1)
                {
                    string str22 = Convert.ToString(this.Bll1.DAL1.ExecuteScalar("select TemplatePath from TemplateInfo where IsUsed=true and IsMobile=false"));
                    string str23 = base.Server.MapPath(@"..\template\") + str22 + "/Module_home.html";
                    if (File.Exists(str23))
                    {
                        this.CS.CreateHome(str23, "");
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
            string[] astrRet = new string[7];
            string strQuery = "select top 1 id,Bak1,Bak2,Bak3,BakImg1,BakImg2,BakImg3 from QH_SiteInfo ";
            this.Bll1.ReadDataReader(strQuery, ref astrRet, 7);
            this.ViewState["id"] = astrRet[0];
            this.Bak1.Value = this.strBak1 = astrRet[1];
            this.Bak2.Value = this.strBak2 = astrRet[2];
            this.Bak3.Value = this.strBak3 = astrRet[3];
            this.ViewState["BakUrl1"] = this.strBakImg1 = astrRet[4];
            this.ViewState["BakUrl2"] = this.strBakImg2 = astrRet[5];
            this.ViewState["BakUrl3"] = this.strBakImg3 = astrRet[6];
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