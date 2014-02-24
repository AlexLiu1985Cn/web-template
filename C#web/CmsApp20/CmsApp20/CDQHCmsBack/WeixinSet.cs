using _BLL;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _commen;

namespace CmsApp20.CDQHCmsBack
{
    public class WeixinSet : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnSave;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected HtmlInputFile myFile;
        protected string strDefaultBack = "";
        protected string strEventWelcom = "";
        protected string strNewsNum = "";
        protected string strPictureNum = "";
        protected string strPicUrl = "";
        protected string strProductNum = "";
        protected string strURL = "";
        protected string strURLErr = "";
        protected string strWeixinType0 = "";
        protected string strWeixinType1 = "";
        protected string strWOff = "";
        protected string strWOn = "";
        protected Label subHistory;
        protected Label subToday;
        protected Label subTotle;
        protected string Token = "";

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string str = base.Request.Form["URL"].Trim();
            string str2 = base.Request.Form["Token"].Trim();
            string str3 = base.Request.Form["WeixinOn"].Trim();
            string str4 = base.Request.Form["WeixinType"];
            bool flag = true;
            if ((((string)this.ViewState["WeixinOn"]) != str3) || (((string)this.ViewState["Token"]) != str2))
            {
                try
                {
                    string str5 = base.Server.MapPath("../WebApp/WeiXin/CdqhWeiXin.aspx");
                    string contents = Regex.Replace(Regex.Replace(File.ReadAllText(str5, Encoding.UTF8), "string\\s+strOpen\\s+=\\s+\"\\d\";", "string strOpen = \"" + str3 + "\";"), "string\\s+_Token\\s+=\\s+\"\\w+\";", "string _Token = \"" + str2 + "\";");
                    File.WriteAllText(str5, contents, Encoding.UTF8);
                }
                catch (Exception exception)
                {
                    SystemError.CreateErrorLog("修改微信设置文件错误：" + exception.ToString());
                    flag = false;
                }
            }
            if (!flag)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "UpdateFileErr", "<script>alert('修改文件错误，请稍后重试！');</script>");
                this.PostbackSet();
                return;
            }
            string path = base.Request.Form["PicUrl"].Trim();
            string str8 = this.myFile.Value;
            HttpPostedFile postedFile = this.myFile.PostedFile;
            int contentLength = postedFile.ContentLength;
            if (contentLength != 0)
            {
                string str9 = base.Server.MapPath("../upload/");
                if (!Directory.Exists(str9))
                {
                    Directory.CreateDirectory(str9);
                }
                string str10 = str8.Substring(str8.LastIndexOf(".") + 1).ToUpper();
                switch (str10)
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
                                this.PostbackSet();
                                return;
                            }
                            string str12 = ((string)this.ViewState["OldPicUrl"]).Replace(this.Bll1.GetDomainUrl(1), "../");
                            if ((str12 != "") && (str12 != "../images/WeixinPic.jpg"))
                            {
                                string str13 = base.Server.MapPath(str12);
                                if (File.Exists(str13))
                                {
                                    File.Delete(str13);
                                }
                            }
                            path = "../upload/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "." + str10;
                            postedFile.SaveAs(base.Server.MapPath(path));
                            goto Label_032B;
                        }
                }
                base.Response.Write("<script>alert('对不起，请选择正确的图片！');</script>");
                this.PostbackSet();
                return;
            }
        Label_032B:
            try
            {
                string str14;
                string[] strArray;
                string str15;
                if (((string)this.ViewState["id"]) == null)
                {
                    str14 = "WeixinOn,URL,Token,WeixinType";
                    strArray = new string[] { str3, str, str2, str4 };
                    str15 = "insert into QH_WeixinSet(WeixinOn,URL,Token,WeixinType) values(@WeixinOn,@URL,@Token,@WeixinType)";
                }
                else
                {
                    str14 = "WeixinOn,URL,Token,WeixinType,id";
                    strArray = new string[] { str3, str, str2, str4, (string)this.ViewState["id"] };
                    str15 = "UPDATE QH_WeixinSet SET WeixinOn=@WeixinOn,URL=@URL,Token=@Token,WeixinType=@WeixinType WHERE ID=@id";
                }
                if ((this.Bll1.UpdateTable(str15, str14, strArray) == 1) && this.SaveEventWelcom(path.Replace("../", this.Bll1.GetDomainUrl(1))))
                {
                    if (((string)this.ViewState["WeixinType"]) != str4)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Dsp", "<script>window .parent .frames['menu-frame'].location='Left_WebAppManage.aspx';</script>");
                    }
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "UpdateOk", "<script>alert('修改成功！');</script>");
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "UpdateFail", "<script>alert('修改失败！');</script>");
                }
            }
            catch (Exception exception2)
            {
                SystemError.CreateErrorLog("修改微信设置数据库记录错误：" + exception2.ToString());
                base.Response.Write("<script>alert('修改微信设置错误！');</script>");
            }
            this.GetSiteInfoContents();
        }

        private void GetSiteInfoContents()
        {
            string[] astrRet = new string[9];
            string strQuery = "select top 1 id,WeixinOn,URL,Token,WeixinType,subscribeToday,Format(TodayDate,'yyyy-MM-dd'),subscribeTotle,subscribeHistory from QH_WeixinSet ";
            this.Bll1.ReadDataReader(strQuery, ref astrRet, 9);
            if (astrRet[0] == null)
            {
                this.strWOn = "";
                this.strWOff = "Checked";
                this.strWeixinType1 = "";
                this.strWeixinType0 = "Checked";
                string absoluteUri = base.Request.Url.AbsoluteUri;
                int num = absoluteUri.LastIndexOf('/');
                absoluteUri = absoluteUri.Substring(0, absoluteUri.LastIndexOf('/', num - 1) + 1);
                string domainName = this.Bll1.GetDomainName(base.Request.Url.AbsoluteUri);
                if ((domainName.IndexOf("localhost") != -1) || (domainName.IndexOf("127.0.0.1") != -1))
                {
                    absoluteUri = absoluteUri.Replace(domainName, "xxxx");
                }
                this.strURL = absoluteUri + "WebApp/WeiXin/CdqhWeiXin.aspx";
                this.strURLErr = "域名必须为真实域名。";
                this.Token = "cdqh" + new Random().Next().ToString("00000000");
                this.subToday.Text = "0";
                this.subTotle.Text = "0";
                this.subHistory.Text = "0";
                this.SetEventWelcom();
            }
            else
            {
                this.ViewState["id"] = astrRet[0];
                this.ViewState["Token"] = this.Token = astrRet[3];
                this.strURL = astrRet[2];
                if (astrRet[1] == "True")
                {
                    this.ViewState["WeixinOn"] = "1";
                    this.strWOn = "Checked";
                    this.strWOff = "";
                }
                else
                {
                    this.ViewState["WeixinOn"] = "0";
                    this.strWOn = "";
                    this.strWOff = "Checked";
                }
                if (astrRet[4] == "1")
                {
                    this.ViewState["WeixinType"] = "1";
                    this.strWeixinType1 = "Checked";
                    this.strWeixinType0 = "";
                }
                else
                {
                    this.ViewState["WeixinType"] = "0";
                    this.strWeixinType1 = "";
                    this.strWeixinType0 = "Checked";
                }
                if (this.strURL == "")
                {
                    string str4 = base.Request.Url.AbsoluteUri;
                    int num2 = str4.LastIndexOf('/');
                    str4 = str4.Substring(0, str4.LastIndexOf('/', num2 - 1) + 1);
                    string oldValue = this.Bll1.GetDomainName(base.Request.Url.AbsoluteUri);
                    if ((oldValue.IndexOf("localhost") != -1) || (oldValue.IndexOf("127.0.0.1") != -1))
                    {
                        str4 = str4.Replace(oldValue, "xxxx");
                    }
                    this.strURL = str4 + "WebApp/WeiXin/CdqhWeiXin.aspx";
                    this.strURLErr = "域名必须为真实域名。";
                }
                if (this.Token == "")
                {
                    this.Token = "cdqh" + new Random().Next().ToString("00000000");
                }
                string str6 = DateTime.Now.ToString("yyyy-MM-dd");
                if (astrRet[6] == str6)
                {
                    this.subToday.Text = astrRet[5];
                }
                else
                {
                    this.subToday.Text = "0";
                }
                this.subTotle.Text = astrRet[7];
                this.subHistory.Text = astrRet[8];
                this.SetEventWelcom();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.GetSiteInfoContents();
            }
        }

        private void PostbackSet()
        {
            this.Token = base.Request.Form["URL"].Trim();
            this.strURL = base.Request.Form["Token"].Trim();
            if (base.Request.Form["WeixinOn"].Trim() == "1")
            {
                this.strWOn = "Checked";
                this.strWOff = "";
            }
            else
            {
                this.strWOn = "";
                this.strWOff = "Checked";
            }
            this.strEventWelcom = base.Request.Form["EventWelcom"];
            this.strDefaultBack = base.Request.Form["DefaultBack"];
            this.strPicUrl = base.Request.Form["PicUrl"];
            this.strNewsNum = base.Request.Form["NewsNum"];
            this.strProductNum = base.Request.Form["ProductNum"];
        }

        private bool SaveEventWelcom(string strDefaultPicUrl)
        {
            if (((((string)this.ViewState["EventWelcom"]) == "1") && (((string)this.ViewState["DefaultBack"]) == "1")) && ((((string)this.ViewState["PicUrl"]) == "1") && (((string)this.ViewState["Num"]) == "1")))
            {
                List<string[]> listData = new List<string[]> {
                    new string[] { "", "", base.Request.Form["eventWelcom"], "1" },
                    new string[] { "", "", base.Request.Form["DefaultBack"], "2" },
                    new string[] { strDefaultPicUrl, "", "", "3" },
                    new string[] { base.Request.Form["NewsNum"], base.Request.Form["ProductNum"], base.Request.Form["PictureNum"], "4" }
                };
                string strSelect = "select AutoType,PicUrl,Url,[Text] from QH_WeixinAuto where AutoType<>'0'";
                string strUpdate = "UPDATE QH_WeixinAuto SET PicUrl=@PicUrl,Url=@Url,[Text]=@Text WHERE AutoType=@AutoType";
                string[] astrField = new string[] { "PicUrl", "Url", "Text", "AutoType" };
                string[] astrFieldType = new string[] { "text", "text", "memo", "text" };
                return (this.Bll1.DAL1.AdpaterUpdate(strSelect, strUpdate, astrField, astrFieldType, listData) == listData.Count);
            }
            if (((((string)this.ViewState["EventWelcom"]) != "1") && (((string)this.ViewState["DefaultBack"]) != "1")) && ((((string)this.ViewState["PicUrl"]) != "1") && (((string)this.ViewState["Num"]) != "1")))
            {
                string strTableSchema = "select top 1 * from QH_WeixinAuto where 1<>1";
                string strInsert = "insert into QH_WeixinAuto(AutoType,PicUrl,Url,[Text]) values(@AutoType,@PicUrl,@Url,@Text)";
                string[] astrFieldAll = new string[] { "AutoType", "PicUrl", "Url", "Text" };
                string[] strCommen = new string[0];
                string[] strCommenType = new string[0];
                string[] strCmnValue = new string[0];
                string[] strArray7 = new string[] { "AutoType", "PicUrl", "Url", "Text" };
                string[] strArray8 = new string[] { "text", "text", "text", "memo" };
                string[] astrFieldAllType = new string[] { "text", "text", "text", "memo" };
                List<string[]> list2 = new List<string[]> {
                    new string[] { "1", "", "", base.Request.Form["eventWelcom"] },
                    new string[] { "2", "", "", base.Request.Form["DefaultBack"] },
                    new string[] { "3", strDefaultPicUrl, "", "" },
                    new string[] { "4", base.Request.Form["NewsNum"], base.Request.Form["ProductNum"], base.Request.Form["PictureNum"] }
                };
                return this.Bll1.DAL1.BatchInsert(strTableSchema, strInsert, strCommen, strCommenType, strCmnValue, strArray7, strArray8, list2, astrFieldAll, astrFieldAllType);
            }
            int num2 = 0;
            if (((string)this.ViewState["EventWelcom"]) == "1")
            {
                num2 += this.Bll1.DAL1.ExecuteNonQuery("Update QH_WeixinAuto Set [Text]=@Text where AutoType='1'", new OleDbParameter[] { new OleDbParameter("@Text", base.Request.Form["eventWelcom"]) });
            }
            else
            {
                num2 += this.Bll1.DAL1.ExecuteNonQuery("insert into QH_WeixinAuto(AutoType,Type,[Text]) values('1','0',@Text)", new OleDbParameter[] { new OleDbParameter("@Text", base.Request.Form["eventWelcom"]) });
            }
            if (((string)this.ViewState["DefaultBack"]) == "1")
            {
                num2 += this.Bll1.DAL1.ExecuteNonQuery("Update QH_WeixinAuto Set [Text]=@Text where AutoType='2'", new OleDbParameter[] { new OleDbParameter("@Text", base.Request.Form["DefaultBack"]) });
            }
            else
            {
                num2 += this.Bll1.DAL1.ExecuteNonQuery("insert into QH_WeixinAuto(AutoType,Type,[Text]) values('2','0',@Text)", new OleDbParameter[] { new OleDbParameter("@Text", base.Request.Form["DefaultBack"]) });
            }
            if (((string)this.ViewState["PicUrl"]) == "1")
            {
                num2 += this.Bll1.DAL1.ExecuteNonQuery("Update QH_WeixinAuto Set PicUrl=@PicUrl where AutoType='3'", new OleDbParameter[] { new OleDbParameter("@PicUrl", strDefaultPicUrl) });
            }
            else
            {
                num2 += this.Bll1.DAL1.ExecuteNonQuery("insert into QH_WeixinAuto(AutoType,PicUrl) values('3',@PicUrl)", new OleDbParameter[] { new OleDbParameter("@PicUrl", strDefaultPicUrl) });
            }
            if (((string)this.ViewState["Num"]) == "1")
            {
                num2 += this.Bll1.DAL1.ExecuteNonQuery("Update QH_WeixinAuto Set PicUrl=@PicUrl ,Url=@Url,[Text]=@Text where AutoType='4'", new OleDbParameter[] { new OleDbParameter("@PicUrl", base.Request.Form["NewsNum"]), new OleDbParameter("@Url", base.Request.Form["ProductNum"]), new OleDbParameter("@Text", base.Request.Form["PictureNum"]) });
            }
            else
            {
                num2 += this.Bll1.DAL1.ExecuteNonQuery("insert into QH_WeixinAuto(AutoType,PicUrl,Url,[Text]) values('4',@PicUrl,@Url,@Text)", new OleDbParameter[] { new OleDbParameter("@PicUrl", base.Request.Form["NewsNum"]), new OleDbParameter("@Url", base.Request.Form["ProductNum"]), new OleDbParameter("@Text", base.Request.Form["PictureNum"]) });
            }
            return (num2 == 4);
        }

        private void SetEventWelcom()
        {
            foreach (string[] strArray in this.Bll1.DAL1.ReadDataReaderListStr("select AutoType,PicUrl,Url,Text from QH_WeixinAuto where AutoType<>'0'", 4))
            {
                if (strArray[0] == "1")
                {
                    this.strEventWelcom = strArray[3];
                    this.ViewState["EventWelcom"] = "1";
                }
                if (strArray[0] == "2")
                {
                    this.strDefaultBack = strArray[3];
                    this.ViewState["DefaultBack"] = "1";
                }
                if (strArray[0] == "3")
                {
                    this.ViewState["OldPicUrl"] = this.strPicUrl = strArray[1];
                    this.ViewState["PicUrl"] = "1";
                }
                if (strArray[0] == "4")
                {
                    this.strNewsNum = strArray[1];
                    this.strProductNum = strArray[2];
                    this.strPictureNum = strArray[3];
                    this.ViewState["Num"] = "1";
                }
            }
            string[] strArray2 = null;
            if (((string)this.ViewState["EventWelcom"]) != "1")
            {
                strArray2 = this.Bll1.DAL1.ReadDataReaderStringArray("select top 1 SitePath,Tel, CompanyName from QH_SiteInfo", 3);
                this.strEventWelcom = "欢迎关注" + strArray2[2] + "！我们将为您提供最优质的服务，更多信息请登陆官网！\n" + strArray2[0] + "\n ";
            }
            if (((string)this.ViewState["DefaultBack"]) != "1")
            {
                if (strArray2 == null)
                {
                    strArray2 = this.Bll1.DAL1.ReadDataReaderStringArray("select top 1 SitePath,Tel, CompanyName from QH_SiteInfo", 3);
                }
                this.strDefaultBack = "感谢您对" + strArray2[2] + "的支持，我们将为您提供更便捷的服务，了解更多信息请登陆官网！\n" + strArray2[0] + "\n ";
            }
            if (((string)this.ViewState["PicUrl"]) != "1")
            {
                this.strPicUrl = this.Bll1.GetDomainUrl(1) + "images/WeixinPic.jpg";
            }
            if (((string)this.ViewState["Num"]) != "1")
            {
                this.strNewsNum = this.strProductNum = this.strPictureNum = "9";
            }
        }
    }
}
