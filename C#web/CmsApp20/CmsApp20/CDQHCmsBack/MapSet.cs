namespace CmsApp20.CDQHCmsBack
{
    using _BLL;
    using System;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using TProcess;

    public class MapSet : Page
    {
        protected HtmlInputText Address;
        private BLL Bll1 = new BLL();
        protected Button BtnCreate;
        protected Button BtnSave;
        protected Button BtnSearch;
        protected HtmlInputText City;
        protected HtmlForm form1;
        protected HtmlTextArea info;
        protected HtmlGenericControl Msg;

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            string str7;
            string str8;
            string str10;
            string strMdlName = "";
            string str2 = "";
            string strMdl = "Map";
            string tmpltFileName = this.Bll1.GetTmpltFileName(strMdl, out strMdlName);
            string strTFName = this.Bll1.GetTmpltFileName_mobile(strMdl, out str2);
            strMdlName = str2 = "地图页";
            string strAbsPath = this.Bll1.getTemplateAbsPath(tmpltFileName, base.Server.MapPath(@"..\template\"), strMdlName, out str8, out str7);
            string str9 = this.Bll1.getTemplateAbsPath_Mobile(strTFName, base.Server.MapPath(@"..\template\"), str2, out str8, out str10);
            if ((strAbsPath == "") && (str9 == ""))
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "File", "<script>alert('" + str7 + @"\n" + str10 + "');</script>");
                this.DataToBind();
            }
            else
            {
                CreateStatic static2 = null;
                CreateStatic @static = new CreateStatic(base.Request.Url.AbsoluteUri);
                if (Convert.ToBoolean(this.Bll1.DAL1.ExecuteScalar("select top 1 MobileOn from QH_SiteInfo")) && (str9 != ""))
                {
                    static2 = new CreateStatic(base.Request.Url.AbsoluteUri, "Mobile");
                }
                this.Msg.InnerHtml = "正在生成" + strMdlName + "...";
                this.Msg.InnerHtml = @static.CreateMap(strAbsPath, "");
                if (static2 != null)
                {
                    this.Msg.InnerHtml = this.Msg.InnerHtml + static2.CreateMap(str9, "m");
                }
                this.DataToBind();
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string str2;
            string[] strArray;
            string str3;
            string str = (string)this.ViewState["id"];
            if (str == null)
            {
                str2 = "lng,lat,address,info";
                strArray = new string[] { (string)this.ViewState["lng"], (string)this.ViewState["lat"], this.Address.Value.Trim(), this.info.Value.Trim().Replace("\n", "<br>").Replace("\r", "") };
                str3 = "insert into QH_Map(lng,lat,address,info) values(@lng,@lat,@address,@info)";
            }
            else
            {
                str2 = "lng,lat,address,info,id";
                strArray = new string[] { (string)this.ViewState["lng"], (string)this.ViewState["lat"], this.Address.Value.Trim(), this.info.Value.Trim().Replace("\n", "<br>").Replace("\r", ""), str };
                str3 = "UPDATE QH_Map SET lng=@lng,lat=@lat ,address=@address,info=@info  WHERE ID=@id";
            }
            if (this.Bll1.UpdateTable(str3, str2, strArray) != 1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "SaveErr", "alert('保存失败。');", true);
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "SaveOk", "alert('保存成功。');", true);
            }
            this.DataToBind();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string str = this.Address.Value.Trim();
            if (str == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "Empty", "alert('搜索地址不能为空。');", true);
            }
            else
            {
                string uriString = "";
                string str3 = this.City.Value.Trim();
                if (str3 == "")
                {
                    uriString = "http://api.map.baidu.com/geocoder/v2/?address=" + str + "&ak=PYAQmsU6cHwnEdBax3c8BupF";
                }
                else
                {
                    uriString = "http://api.map.baidu.com/geocoder/v2/?address=" + str + "&ak=PYAQmsU6cHwnEdBax3c8BupF&city=" + str3;
                }
                WebClient client = new WebClient();
                Uri address = new Uri(uriString);
                client.Encoding = Encoding.UTF8;
                string input = client.DownloadString(address);
                string str5 = Regex.Match(input, @"<status>(\d+)</status>").Groups[1].ToString();
                string str6 = Regex.Match(input, @"<lng>(\d+\.\d+)</lng>").Groups[1].ToString();
                string str7 = Regex.Match(input, @"<lat>(\d+\.\d+)</lat>").Groups[1].ToString();
                Regex.Match(input, "<level>([^<]+)</level>").Groups[1].ToString();
                if (str5 != "0")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "Err", "alert('返回错误，请重试。');", true);
                }
                else if ((str6 == "") || (str7 == ""))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "Err", @"alert('搜索地址未找到。原因及可尝试方法如下：\n地址库里无此数据。\n请输入城市重试。\n将过于详细或简单的地址更改至省市区县街道重新解析。');", true);
                }
                else
                {
                    this.ViewState["lng"] = str6;
                    this.ViewState["lat"] = str7;
                    base.ClientScript.RegisterStartupScript(base.GetType(), "Map", "GenerateMap(" + str6 + "," + str7 + ",'" + str + "');", true);
                }
            }
        }

        private void DataToBind()
        {
            string[] strArray = this.Bll1.DAL1.ReadDataReaderStringArray("select top 1 * from QH_Map", 5);
            if (strArray[0] == null)
            {
                string str = "116.37119307207";
                string str2 = "39.978917549401";
                string str3 = "北京市海淀区花园路13号怡和中心401<br>北京创都启航网络科技有限公司<br>电话：010-82034300";
                base.ClientScript.RegisterStartupScript(base.GetType(), "Map", "GenerateMap(" + str + "," + str2 + ",'" + str3 + "');", true);
                this.Address.Value = "北京市海淀区花园路13号怡和中心401";
                this.info.Value = "北京市海淀区花园路13号怡和中心401<br>北京创都启航网络科技有限公司<br>电话：010-82034300".Replace("<br>", "\n");
                this.ViewState["lng"] = "116.37119307207";
                this.ViewState["lat"] = "39.978917549401";
            }
            else
            {
                this.Address.Value = strArray[3];
                base.ClientScript.RegisterStartupScript(base.GetType(), "Map", "GenerateMap(" + strArray[1] + "," + strArray[2] + ",'" + strArray[4] + "');", true);
                this.info.Value = strArray[4].Replace("<br>", "\n");
                this.ViewState["id"] = strArray[0];
                this.ViewState["lng"] = strArray[1];
                this.ViewState["lat"] = strArray[2];
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