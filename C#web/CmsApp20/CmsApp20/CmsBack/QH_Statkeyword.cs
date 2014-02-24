namespace CmsApp20.CmsBack
{
    using InfoSoftGlobal;
    using _DAL.OleDBHelper;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class QH_Statkeyword : Page
    {
        private int[] anKNum;
        private string[] astrColor = new string[] { "0d8ecf", "04d215", "b0de09", "f8ff01", "ff9e01", "ff6600", "ff1f11", "814ee6", "f234b0", "54f034", "3c67e8", "2c36f8", "408080" };
        private string[] astrKword;
        protected Button Btndate;
        protected Button BtnOK;
        private DAL DAL1 = new DAL();
        protected Literal FCLiteral;
        protected HtmlForm form1;
        protected HiddenField HdnV;
        protected HtmlHead Head1;
        protected HtmlSelect month;
        protected Repeater REList;
        protected string strBegin;
        protected string strEnd;
        protected HtmlGenericControl table1;
        protected DropDownList year;

        private void BindDay(string strDay)
        {
            string str;
            string str3 = strDay;
            if (str3 != null)
            {
                if (!(str3 == "1"))
                {
                    if (str3 == "2")
                    {
                        this.strBegin = this.strEnd = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
                        goto Label_00E3;
                    }
                    if (str3 == "3")
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
            str = this.DAL1.ReadDataReader("select Keyword from QH_StatDay where Day=#" + this.strBegin + "#");
            if (str == "<NULL>")
            {
                this.FCLiteral.Text = "<font color=red>对不起，该时段内无数据。</font>";
                this.table1.Style.Add("display", "none");
            }
            else
            {
                this.GetkeyworInfo(str);
                string strXML = "<graph showPercentageInLabel='1' pieSliceDepth='12'  decimalPrecision='0' showNames='1' baseFontSize='13' >";
                StringBuilder builder = new StringBuilder();
                int num = 0;
                int num2 = 0;
                for (int i = 0; i < this.astrKword.Length; i++)
                {
                    num += this.anKNum[i];
                    if (this.anKNum[i] == 0)
                    {
                        break;
                    }
                    if (i < 12)
                    {
                        builder.Append(string.Concat(new object[] { "<set name='", this.astrKword[i], "' value='", this.anKNum[i], "' color='", this.astrColor[i], "' />" }));
                    }
                    else
                    {
                        num2 += this.anKNum[i];
                    }
                }
                if (num2 > 0)
                {
                    builder.Append(string.Concat(new object[] { "<set name='其它' value='", num2, "' color='", this.astrColor[12], "' />" }));
                }
                if (builder.Length == 0)
                {
                    this.FCLiteral.Text = "<font color=red>对不起，该时段内无关键词进入。</font>";
                    this.table1.Style.Add("display", "none");
                }
                else
                {
                    strXML = strXML + builder.ToString() + "</graph>";
                    this.FCLiteral.Text = FusionCharts.RenderChartHTML("../FusionCharts/FCF_Pie3D.swf", "", strXML, "Sales", "600", "350", false);
                    string[][] strArray = new string[this.astrKword.Length][];
                    for (int j = 0; j < this.astrKword.Length; j++)
                    {
                        strArray[j] = new string[] { this.astrKword[j], this.anKNum[j].ToString(), (((float)this.anKNum[j]) / ((float)num)).ToString("#0.00%") };
                    }
                    this.REList.DataSource = strArray;
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
            ArrayList aListKwordInfo = this.DAL1.ReadDataReaderAL("select Keyword from QH_StatDay where Day>=#" + this.strBegin + "# and Day<=#" + this.strEnd + "#");
            if (aListKwordInfo.Count == 0)
            {
                this.FCLiteral.Text = "<font color=red>对不起，该时段内无数据。</font>";
                this.table1.Style.Add("display", "none");
            }
            else
            {
                this.GetkeyworInfo(aListKwordInfo);
                string strXML = "<graph showPercentageInLabel='1' pieSliceDepth='12'  decimalPrecision='0' showNames='1' baseFontSize='13' >";
                StringBuilder builder = new StringBuilder();
                int num2 = 0;
                int num3 = 0;
                for (int i = 0; i < this.astrKword.Length; i++)
                {
                    num2 += this.anKNum[i];
                    if (this.anKNum[i] == 0)
                    {
                        break;
                    }
                    if (i < 12)
                    {
                        builder.Append(string.Concat(new object[] { "<set name='", this.astrKword[i], "' value='", this.anKNum[i], "' color='", this.astrColor[i], "' />" }));
                    }
                    else
                    {
                        num3 += this.anKNum[i];
                    }
                }
                if (num3 > 0)
                {
                    builder.Append(string.Concat(new object[] { "<set name='其它' value='", num3, "' color='", this.astrColor[12], "' />" }));
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
                    string[][] strArray = new string[this.astrKword.Length][];
                    for (int j = 0; j < this.astrKword.Length; j++)
                    {
                        strArray[j] = new string[] { this.astrKword[j], this.anKNum[j].ToString(), (((float)this.anKNum[j]) / ((float)num2)).ToString("#0.00%") };
                    }
                    this.REList.DataSource = strArray;
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

        private void GetkeyworInfo(ArrayList aListKwordInfo)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (string str in aListKwordInfo)
            {
                if (str != "")
                {
                    string[] strArray = str.Split(new char[] { '`' });
                    if (strArray.Length != 0)
                    {
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            string[] strArray2 = strArray[i].Split(new char[] { '~' });
                            if (dictionary.ContainsKey(strArray2[0]))
                            {
                                Dictionary<string, int> dictionary2;
                                string str2;
                                (dictionary2 = dictionary)[str2 = strArray2[0]] = dictionary2[str2] + int.Parse(strArray2[1]);
                            }
                            else
                            {
                                dictionary.Add(strArray2[0], int.Parse(strArray2[1]));
                            }
                        }
                    }
                }
            }
            if (dictionary.Count == 0)
            {
                this.astrKword = new string[0];
                this.anKNum = new int[0];
            }
            else
            {
                this.astrKword = new string[dictionary.Count];
                this.anKNum = new int[dictionary.Count];
                dictionary.Keys.CopyTo(this.astrKword, 0);
                dictionary.Values.CopyTo(this.anKNum, 0);
                Array.Sort<int, string>(this.anKNum, this.astrKword, new IntComparer("DESC"));
            }
        }

        private void GetkeyworInfo(string strKwordInfo)
        {
            if (strKwordInfo == "")
            {
                this.astrKword = new string[0];
                this.anKNum = new int[0];
            }
            else
            {
                string[] strArray = strKwordInfo.Split(new char[] { '`' });
                this.astrKword = new string[strArray.Length];
                this.anKNum = new int[strArray.Length];
                if (strArray.Length != 0)
                {
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        string[] strArray2 = strArray[i].Split(new char[] { '~' });
                        this.astrKword[i] = strArray2[0];
                        this.anKNum[i] = int.Parse(strArray2[1]);
                    }
                    Array.Sort<int, string>(this.anKNum, this.astrKword, new IntComparer("DESC"));
                }
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

        private class IntComparer : IComparer<int>
        {
            private string _type = "ASC";

            public IntComparer(string type)
            {
                this._type = type;
            }

            public int Compare(int x, int y)
            {
                if (this._type == "DESC")
                {
                    return y.CompareTo(x);
                }
                return x.CompareTo(y);
            }
        }
    }
}