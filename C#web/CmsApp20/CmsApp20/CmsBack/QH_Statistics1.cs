using _BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InfoSoftGlobal;

namespace CmsApp20.CmsBack
{
    public class QH_Statistics1 : Page
    {
        private int[] anIP = new int[30];
        private int[] anPV = new int[30];
        private int[] anUV = new int[30];
        private string[] astrStat;
        private BLL Bll1 = new BLL();
        private DataTable dt;
        protected Literal FCLiteral;
        protected Literal FCLiteral1;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected HtmlGenericControl mUV;
        private int nIPTotle;
        private int nIPTotle7;
        private int nUVTotle;
        private int nUVTotle7;
        private int nValueEmpty;
        private int nValueEmpty7;
        protected HtmlTable stat;
        protected string str10m;
        protected string str15m;
        protected string str5m;
        protected string strAVDIP;
        protected string strAVDPV;
        protected string strAVDUV;
        protected string strClose = "";
        protected string strMaxIP;
        protected string strMaxPV;
        protected string strMaxUV;
        protected string strMIP;
        protected string strMPV;
        protected string strMUV;
        protected string strTDIP;
        protected string strTDPV;
        protected string strTDUV;
        protected string strTLIP;
        protected string strTLPV;
        protected string strTLUV;
        protected string strTMIP;
        protected string strTMPV;
        protected string strTMUV;
        protected string strYDIP;
        protected string strYDPV;
        protected string strYDUV;

        private void BindFC()
        {
            this.astrStat = this.Bll1.DAL1.ReadDataReaderStringArray("select [open],SaveData from QH_StatisticSet where id=1 ", 2);
            if (this.astrStat[0] == "False")
            {
                this.FCLiteral.Text = "网站统计功能未开启";
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<graph yAxisMinValue='0' decimalPrecision='0' showValues='0'  anchorRadius='4' baseFontSize='12' canvasBorderThickness='1' numVDivLines ='28' numDivLines='9' outCnvbaseFontSize='10' anchorRadius='4' anchorSides='20' formatNumberScale='0' canvasBorderColor='777777'{0}>");
                builder.Append("<categories>");
                string[] strArray = new string[30];
                for (int i = 0; i < 30; i++)
                {
                    strArray[i] = DateTime.Now.AddDays((double)(-29 + i)).ToString("yyyy-MM-dd");
                    if ((i % 5) == 1)
                    {
                        builder.Append("<category name='" + strArray[i] + "'/>");
                    }
                    else
                    {
                        builder.Append("<category name='" + strArray[i] + "'showName='0'/>");
                    }
                }
                builder.Append("</categories>");
                this.dt = this.Bll1.GetDataTable("select pv,uv,ip,[Day] from QH_StatDay where Day>=#" + strArray[0] + "# and Day<=#" + strArray[0x1d] + "#");
                builder.Append("<dataset seriesname='pv' color='ff6600' anchorBgColor='ff6600' anchorBorderColor='ffffff' >");
                for (int j = 0; j < 30; j++)
                {
                    DataRow[] rowArray = this.dt.Select("Day=#" + strArray[j] + "#");
                    if (rowArray.Length == 0)
                    {
                        int num5;
                        this.anUV[j] = num5 = 0;
                        this.anIP[j] = this.anPV[j] = num5;
                    }
                    else
                    {
                        this.anIP[j] = rowArray[0]["ip"].ToString().Split(new char[] { ',' }).Length;
                        this.anPV[j] = int.Parse(rowArray[0][0].ToString());
                        this.anUV[j] = int.Parse(rowArray[0][1].ToString());
                    }
                    builder.Append("<set value='" + this.anPV[j] + "'/>");
                    this.nValueEmpty += this.anPV[j];
                    this.nIPTotle += this.anIP[j];
                    this.nUVTotle += this.anUV[j];
                    if (j > 0x16)
                    {
                        this.nValueEmpty7 += this.anPV[j];
                        this.nIPTotle7 += this.anIP[j];
                        this.nUVTotle7 += this.anUV[j];
                    }
                }
                builder.Append("</dataset>");
                builder.Append("<dataset seriesname='uv' color='04d215' anchorBgColor='04d215' anchorBorderColor='ffffff' >");
                for (int k = 0; k < 30; k++)
                {
                    builder.Append("<set value='" + this.anUV[k] + "'/>");
                }
                builder.Append("</dataset>");
                builder.Append("<dataset seriesname='ip' color='0d8ecf' anchorBgColor='0d8ecf' anchorBorderColor='ffffff' >");
                for (int m = 0; m < 30; m++)
                {
                    builder.Append("<set value='" + this.anIP[m] + "'/>");
                }
                builder.Append("</dataset>");
                builder.Append("</graph>");
                string strXML = (this.nValueEmpty < 10) ? string.Format(builder.ToString(), "yAxisMaxValue='10'") : string.Format(builder.ToString(), "");
                this.FCLiteral.Text = FusionCharts.RenderChartHTML("../FusionCharts/FCF_MSLine.swf", "", strXML, "my30day", "800", "300", false);
            }
        }

        private void BindFC1()
        {
            if (this.astrStat[0] == "False")
            {
                this.mUV.InnerText = "";
                this.FCLiteral1.Text = "网站统计功能未开启";
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<graph showValues='0'decimalPrecision='0' formatNumberScale='0' showNames='1'  numVDivLines ='13' numDivLines='9' color='ff6600' anchorRadius='4' anchorBgColor='ff6600' anchorBorderColor='ffffff'  canvasBorderColor='777777'canvasBorderThickness='1' baseFontSize='12'outCnvbaseFontSize='10'{0}>");
                DataTable dataTable = this.Bll1.GetDataTable("select IsUV,ATime from QH_Statistics where ATime>=#" + DateTime.Now.AddMinutes(-14.0).ToString("yyyy-MM-dd HH:mm") + ":00# and ATime<=#" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "#");
                int num = 0;
                int num3 = 0;
                int num4 = 0;
                for (int i = 0; i < 15; i++)
                {
                    DateTime time = DateTime.Now.AddMinutes((double)(-14 + i));
                    int num2 = Convert.ToInt32(dataTable.Compute("count(isuv)", "isuv=true and ATime>=#" + time.ToString("yyyy-MM-dd HH:mm") + ":00# and ATime<=#" + time.ToString("yyyy-MM-dd HH:mm") + ":59#"));
                    num += num2;
                    if (i > 4)
                    {
                        num3 += num2;
                    }
                    if (i > 9)
                    {
                        num4 += num2;
                    }
                    if ((i % 3) == 1)
                    {
                        builder.Append(string.Concat(new object[] { "<set name='", time.ToString("HH:mm"), "' value='", num2, "'/>" }));
                    }
                    else
                    {
                        builder.Append(string.Concat(new object[] { "<set name='", time.ToString("HH:mm"), "' value='", num2, "' showName='0'/>" }));
                    }
                }
                builder.Append("</graph>");
                string strXML = (num < 10) ? string.Format(builder.ToString(), "yAxisMaxValue='10'") : string.Format(builder.ToString(), "");
                this.FCLiteral1.Text = FusionCharts.RenderChartHTML("../FusionCharts/FCF_Line.swf", "", strXML, "test1", "800", "200", false);
                this.str15m = num.ToString();
                this.str10m = num3.ToString();
                this.str5m = num4.ToString();
            }
        }

        private void BindStatistics()
        {
            if (this.astrStat[0] == "False")
            {
                this.strClose = "网站统计功能未开启";
                this.stat.Style.Add("display", "none");
            }
            else
            {
                this.strTDPV = this.anPV[0x1d].ToString();
                this.strTDUV = this.anUV[0x1d].ToString();
                this.strTDIP = this.anIP[0x1d].ToString();
                this.strYDPV = this.anPV[0x1c].ToString();
                this.strYDUV = this.anUV[0x1c].ToString();
                this.strYDIP = this.anIP[0x1c].ToString();
                if (this.astrStat[1] == "1")
                {
                    this.strYDPV = "统计设置为仅当天";
                    this.strYDUV = this.strYDIP = "";
                    this.strAVDPV = this.strMPV = this.strTLPV = this.strMaxPV = this.strTDPV;
                    this.strAVDUV = this.strMUV = this.strTLUV = this.strMaxUV = this.strTDUV;
                    this.strAVDIP = this.strMIP = this.strTLIP = this.strMaxIP = this.strTDIP;
                    this.strTMPV = this.strTMUV = this.strTMIP = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else if (this.astrStat[1] == "2")
                {
                    this.strAVDPV = Math.Ceiling((double)(((double)this.nValueEmpty7) / 7.0)).ToString();
                    this.strAVDUV = Math.Ceiling((double)(((double)this.nUVTotle7) / 7.0)).ToString();
                    this.strAVDIP = Math.Ceiling((double)(((double)this.nIPTotle7) / 7.0)).ToString();
                    this.strMPV = "统计设置为仅7天";
                    this.strMUV = this.strMIP = "";
                    this.strTLPV = this.nValueEmpty7.ToString();
                    this.strTLUV = this.nUVTotle7.ToString();
                    this.strTLIP = this.nIPTotle7.ToString();
                    int num = 0;
                    int num2 = 0;
                    int num3 = 0;
                    int num4 = 0;
                    int num5 = 0;
                    int num6 = 0;
                    for (int i = 0x17; i < 30; i++)
                    {
                        if (num <= this.anPV[i])
                        {
                            num = this.anPV[i];
                            num4 = i;
                        }
                        if (num2 <= this.anUV[i])
                        {
                            num2 = this.anUV[i];
                            num5 = i;
                        }
                        if (num3 <= this.anIP[i])
                        {
                            num3 = this.anIP[i];
                            num6 = i;
                        }
                    }
                    this.strMaxPV = num.ToString();
                    this.strMaxUV = num2.ToString();
                    this.strMaxIP = num3.ToString();
                    this.strTMPV = DateTime.Now.AddDays((double)(num4 - 0x1d)).ToString("yyyy-MM-dd");
                    this.strTMUV = DateTime.Now.AddDays((double)(num5 - 0x1d)).ToString("yyyy-MM-dd");
                    this.strTMIP = DateTime.Now.AddDays((double)(num6 - 0x1d)).ToString("yyyy-MM-dd");
                }
                else if (this.astrStat[1] == "3")
                {
                    this.strAVDPV = Math.Ceiling((double)(((double)this.nValueEmpty) / 30.0)).ToString();
                    this.strAVDUV = Math.Ceiling((double)(((double)this.nUVTotle) / 30.0)).ToString();
                    this.strAVDIP = Math.Ceiling((double)(((double)this.nIPTotle) / 30.0)).ToString();
                    this.strMPV = this.strTLPV = this.nValueEmpty.ToString();
                    this.strMUV = this.strTLUV = this.nUVTotle.ToString();
                    this.strMIP = this.strTLIP = this.nIPTotle.ToString();
                    int num8 = 0;
                    int num9 = 0;
                    int num10 = 0;
                    int num11 = 0;
                    int num12 = 0;
                    int num13 = 0;
                    for (int j = 0; j < 30; j++)
                    {
                        if (num8 <= this.anPV[j])
                        {
                            num8 = this.anPV[j];
                            num11 = j;
                        }
                        if (num9 <= this.anUV[j])
                        {
                            num9 = this.anUV[j];
                            num12 = j;
                        }
                        if (num10 <= this.anIP[j])
                        {
                            num10 = this.anIP[j];
                            num13 = j;
                        }
                    }
                    this.strMaxPV = num8.ToString();
                    this.strMaxUV = num9.ToString();
                    this.strMaxIP = num10.ToString();
                    this.strTMPV = DateTime.Now.AddDays((double)(num11 - 0x1d)).ToString("yyyy-MM-dd");
                    this.strTMUV = DateTime.Now.AddDays((double)(num12 - 0x1d)).ToString("yyyy-MM-dd");
                    this.strTMIP = DateTime.Now.AddDays((double)(num13 - 0x1d)).ToString("yyyy-MM-dd");
                }
                else if (this.astrStat[1] == "4")
                {
                    int nValueEmpty = this.nValueEmpty;
                    int nUVTotle = this.nUVTotle;
                    int nIPTotle = this.nIPTotle;
                    int[] numArray = new int[0x16d];
                    int[] numArray2 = new int[0x16d];
                    int[] numArray3 = new int[0x16d];
                    this.dt = this.Bll1.GetDataTable("select pv,uv,ip,[Day] from QH_StatDay where Day>=#" + DateTime.Now.AddDays(-364.0).ToString("yyyy-MM-dd") + "# and Day<=#" + DateTime.Now.ToString("yyyy-MM-dd") + "#");
                    for (int k = 0; k < 0x16d; k++)
                    {
                        if (k > 0x14e)
                        {
                            numArray[k] = this.anPV[k - 0x14f];
                            numArray2[k] = this.anUV[k - 0x14f];
                            numArray3[k] = this.anIP[k - 0x14f];
                        }
                        else
                        {
                            DataRow[] rowArray = this.dt.Select("Day=#" + DateTime.Now.AddDays((double)(-364 + k)).ToString("yyyy-MM-dd") + "#");
                            if (rowArray.Length == 0)
                            {
                                int num32;
                                numArray2[k] = num32 = 0;
                                numArray[k] = numArray[k] = num32;
                            }
                            else
                            {
                                numArray[k] = rowArray[0]["ip"].ToString().Split(new char[] { ',' }).Length;
                                numArray[k] = int.Parse(rowArray[0][0].ToString());
                                numArray2[k] = int.Parse(rowArray[0][1].ToString());
                            }
                            nValueEmpty += numArray[k];
                            nIPTotle += numArray[k];
                            nUVTotle += numArray2[k];
                        }
                    }
                    this.strAVDPV = Math.Ceiling((double)(((double)nValueEmpty) / 365.0)).ToString();
                    this.strAVDUV = Math.Ceiling((double)(((double)nUVTotle) / 365.0)).ToString();
                    this.strAVDIP = Math.Ceiling((double)(((double)nIPTotle) / 365.0)).ToString();
                    this.strMPV = this.nValueEmpty.ToString();
                    this.strMUV = this.nUVTotle.ToString();
                    this.strMIP = this.nIPTotle.ToString();
                    this.strTLPV = nValueEmpty.ToString();
                    this.strTLUV = nUVTotle.ToString();
                    this.strTLIP = nIPTotle.ToString();
                    int num19 = 0;
                    int num20 = 0;
                    int num21 = 0;
                    int num22 = 0;
                    int num23 = 0;
                    int num24 = 0;
                    for (int m = 0; m < 0x16d; m++)
                    {
                        if (num19 <= numArray[m])
                        {
                            num19 = numArray[m];
                            num22 = m;
                        }
                        if (num20 <= numArray2[m])
                        {
                            num20 = numArray2[m];
                            num23 = m;
                        }
                        if (num21 <= numArray3[m])
                        {
                            num21 = numArray3[m];
                            num24 = m;
                        }
                    }
                    this.strMaxPV = num19.ToString();
                    this.strMaxUV = num20.ToString();
                    this.strMaxIP = num21.ToString();
                    this.strTMPV = DateTime.Now.AddDays((double)(num22 - 0x16c)).ToString("yyyy-MM-dd");
                    this.strTMUV = DateTime.Now.AddDays((double)(num23 - 0x16c)).ToString("yyyy-MM-dd");
                    this.strTMIP = DateTime.Now.AddDays((double)(num24 - 0x16c)).ToString("yyyy-MM-dd");
                }
            }
        }

        private void DataToBind()
        {
            this.BindFC();
            this.BindFC1();
            this.BindStatistics();
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
