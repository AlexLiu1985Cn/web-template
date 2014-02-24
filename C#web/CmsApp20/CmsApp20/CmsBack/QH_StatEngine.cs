namespace CmsApp20.CmsBack
{
    using _BLL;
    using InfoSoftGlobal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class QH_StatEngine : Page
    {
        private string[] astrColor = new string[] { "0d8ecf", "04d215", "b0de09", "f8ff01", "ff9e01", "ff6600", "ff1f11", "814ee6", "f234b0", "54f034", "3c67e8", "2c36f8", "408080" };
        private string[] astrEnginName = new string[] { "百度", "SOSO搜搜", "搜狗", "谷歌", "360搜索", "有道", "新浪", "雅虎", "必应", "中搜", "lycos", "exactseek", "其它" };
        private BLL Bll1 = new BLL();
        protected Button Btndate;
        protected Button BtnOK;
        protected Literal FCLiteral;
        protected HtmlForm form1;
        protected HiddenField HdnV;
        protected HtmlSelect month;
        protected Repeater REList;
        protected string strBegin;
        protected string strEnd;
        protected HtmlGenericControl table1;
        protected DropDownList year;

        private void BindDay(string strDay)
        {
            string[] strArray;
            string str2 = strDay;
            if (str2 != null)
            {
                if (!(str2 == "1"))
                {
                    if (str2 == "2")
                    {
                        this.strBegin = this.strEnd = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
                        goto Label_00E3;
                    }
                    if (str2 == "3")
                    {
                        this.strBegin = this.strEnd = DateTime.Now.AddDays(-2.0).ToString("yyyy-MM-dd");
                        goto Label_00E3;
                    }
                }
                else
                {
                    this.strBegin = this.strEnd = DateTime.Now.ToString("yyyy-MM-dd");
                    goto Label_00E3;
                }
            }
            this.strBegin = this.strEnd = strDay;
        Label_00E3:
            strArray = this.Bll1.DAL1.ReadDataReaderStringArray("select baidu ,soso,sogou,google,so,youdao,iask,yahoo, bing,zhongsou,lycos, exactseek, otherEngine from QH_StatDay where Day=#" + this.strBegin + "#", 13);
            if (strArray[12] == null)
            {
                this.FCLiteral.Text = "<font color=red>对不起，该时段内无数据。</font>";
                this.table1.Style.Add("display", "none");
            }
            else
            {
                string strXML = "<graph showPercentageInLabel='1' pieSliceDepth='12'  decimalPrecision='0' showNames='1' baseFontSize='13' >";
                StringBuilder builder = new StringBuilder();
                int num = 0;
                int[] numArray = new int[13];
                for (int i = 0; i < 13; i++)
                {
                    numArray[i] = int.Parse(strArray[i]);
                    num += numArray[i];
                    if (strArray[i] != "0")
                    {
                        builder.Append("<set name='" + this.astrEnginName[i] + "' value='" + strArray[i] + "' color='" + this.astrColor[i] + "' />");
                    }
                }
                if (builder.Length == 0)
                {
                    this.FCLiteral.Text = "<font color=red>对不起，该时段内无搜索引擎进入。</font>";
                    this.table1.Style.Add("display", "none");
                }
                else
                {
                    strXML = strXML + builder.ToString() + "</graph>";
                    this.FCLiteral.Text = FusionCharts.RenderChartHTML("../FusionCharts/FCF_Pie3D.swf", "", strXML, "Sales", "600", "350", false);
                    string[][] strArray2 = new string[13][];
                    for (int j = 0; j < 13; j++)
                    {
                        strArray2[j] = new string[] { this.astrEnginName[j], strArray[j], (((float)numArray[j]) / ((float)num)).ToString("#0.00%") };
                    }
                    this.REList.DataSource = strArray2;
                    this.REList.DataBind();
                    this.table1.Style.Add("display", "");
                }
            }
        }

        private void BindPeriod(string strPeriod)
        {
            this.strEnd = DateTime.Now.ToString("yyyy-MM-dd");
            string str3 = strPeriod;
            if (str3 != null)
            {
                if (!(str3 == "4"))
                {
                    if (str3 == "5")
                    {
                        int num = this.GetMondayNum() - 1;
                        this.strBegin = DateTime.Now.AddDays((double)(-6 + num)).ToString("yyyy-MM-dd");
                        this.strEnd = DateTime.Now.AddDays((double)num).ToString("yyyy-MM-dd");
                    }
                    else if (str3 == "6")
                    {
                        this.strBegin = this.strEnd.Substring(0, 8) + "01";
                    }
                    else if (str3 == "7")
                    {
                        this.strBegin = DateTime.Now.AddDays(-29.0).ToString("yyyy-MM-dd");
                    }
                    else if (str3 == "8")
                    {
                        string str = this.year.SelectedValue + "-" + base.Request.Form["month"] + "-";
                        this.strBegin = str + "01";
                        this.strEnd = str + DateTime.DaysInMonth(int.Parse(this.year.SelectedValue), int.Parse(base.Request.Form["month"])).ToString();
                    }
                }
                else
                {
                    this.strBegin = DateTime.Now.AddDays((double)this.GetMondayNum()).ToString("yyyy-MM-dd");
                }
            }
            List<string[]> list = this.Bll1.DAL1.ReadDataReaderListStr("select baidu ,soso,sogou,google,so,youdao,iask,yahoo, bing,zhongsou,lycos, exactseek, otherEngine  from QH_StatDay where Day>=#" + this.strBegin + "# and Day<=#" + this.strEnd + "#", 13);
            if (list.Count == 0)
            {
                this.FCLiteral.Text = "<font color=red>对不起，该时段内无数据。</font>";
                this.table1.Style.Add("display", "none");
            }
            else
            {
                string strXML = "<graph showPercentageInLabel='1' pieSliceDepth='12'  decimalPrecision='0' showNames='1' baseFontSize='13' >";
                StringBuilder builder = new StringBuilder();
                int num2 = 0;
                int[] numArray = new int[13];
                foreach (string[] strArray in list)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        numArray[j] += int.Parse(strArray[j]);
                    }
                }
                for (int i = 0; i < 13; i++)
                {
                    num2 += numArray[i];
                    if (numArray[i] != 0)
                    {
                        builder.Append(string.Concat(new object[] { "<set name='", this.astrEnginName[i], "' value='", numArray[i], "' color='", this.astrColor[i], "' />" }));
                    }
                }
                if (builder.Length == 0)
                {
                    this.FCLiteral.Text = "<font color=red>对不起，该时段内无搜索引擎进入。</font>";
                    this.table1.Style.Add("display", "none");
                }
                else
                {
                    strXML = strXML + builder.ToString() + "</graph>";
                    this.FCLiteral.Text = FusionCharts.RenderChartHTML("../FusionCharts/FCF_Pie3D.swf", "", strXML, "Sales", "600", "350", false);
                    string[][] strArray2 = new string[13][];
                    for (int k = 0; k < 13; k++)
                    {
                        strArray2[k] = new string[] { this.astrEnginName[k], numArray[k].ToString(), (((float)numArray[k]) / ((float)num2)).ToString("#0.00%") };
                    }
                    this.REList.DataSource = strArray2;
                    this.REList.DataBind();
                    this.table1.Style.Add("display", "");
                }
            }
        }

        protected void Btndate_Click(object sender, EventArgs e)
        {
            if ((this.HdnV.Value.Length > 1) || (int.Parse(this.HdnV.Value) < 4))
            {
                this.BindDay(this.HdnV.Value);
            }
            else
            {
                this.BindPeriod(this.HdnV.Value);
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), "show", "SetDisp('" + this.strBegin + "');", true);
        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            this.BindPeriod(this.HdnV.Value);
            base.ClientScript.RegisterStartupScript(base.GetType(), "show", "SetDisp('" + this.strBegin + "');", true);
        }

        private void DataToBind()
        {
            string[] strArray = DateTime.Now.AddDays(-364.0).ToString("yyyy-MM-dd").Split(new char[] { '-' });
            string[] strArray2 = DateTime.Now.ToString("yyyy-MM-dd").Split(new char[] { '-' });
            if (strArray[0] == strArray2[0])
            {
                this.year.Items.Add(new ListItem(strArray2[0]));
                string[] strArray3 = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                this.month.DataSource = strArray3;
                this.month.DataBind();
            }
            else
            {
                this.year.Items.Add(new ListItem(strArray[0]));
                this.year.Items.Add(new ListItem(strArray2[0]));
                ArrayList list = new ArrayList();
                for (int i = int.Parse(strArray[1]); i <= 12; i++)
                {
                    list.Add(i.ToString());
                }
                this.month.DataSource = list;
                this.month.DataBind();
            }
        }

        private int GetMondayNum()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                return -6;
            }
            return (((int)((int)DateTime.Now.DayOfWeek * (int)~DayOfWeek.Sunday)) + 1);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.DataToBind();
                this.HdnV.Value = "1";
                this.BindDay(this.HdnV.Value);
            }
        }

        protected void year_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] strArray = DateTime.Now.AddDays(-364.0).ToString("yyyy-MM-dd").Split(new char[] { '-' });
            string[] strArray2 = DateTime.Now.ToString("yyyy-MM-dd").Split(new char[] { '-' });
            if (strArray[0] == strArray2[0])
            {
                string[] strArray3 = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                this.month.DataSource = strArray3;
                this.month.DataBind();
            }
            else
            {
                int num = 1;
                int num2 = 12;
                if (this.year.SelectedIndex == 0)
                {
                    num = int.Parse(strArray[1]);
                }
                else
                {
                    num2 = int.Parse(strArray2[1]);
                }
                ArrayList list = new ArrayList();
                for (int i = num; i <= num2; i++)
                {
                    list.Add(i.ToString());
                }
                this.month.DataSource = list;
                this.month.DataBind();
            }
            if ((this.HdnV.Value.Length > 1) || (int.Parse(this.HdnV.Value) < 4))
            {
                this.BindDay(this.HdnV.Value);
            }
            else
            {
                this.BindPeriod(this.HdnV.Value);
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), "show", "SetDisp('" + this.strBegin + "');", true);
        }
    }
}