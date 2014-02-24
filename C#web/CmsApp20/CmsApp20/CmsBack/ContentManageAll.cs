using _BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace CmsApp20.CmsBack
{
    public class ContentManageAll : Page
    {
        private BLL Bll1 = new BLL();
        private DataTable dt;
        protected HtmlForm form1;
        private int nDepth1Num;
        private StringBuilder sbTRows = new StringBuilder();
        protected string strColumn;
        private string strDepth;
        private string strFst = "";
        private string strID0 = "";
        private string strID0Temp;
        private string strMargin = "";
        private void DataToBind()
        {
            this.dt = this.Bll1.GetDataTable("select id,ColumnName,ParentID,depth,[Module] from QH_Column order by CLNG(ParentID) asc,CLNG(depth) asc, CLNG(Order1) asc,id asc");
            if (this.dt != null)
            {
                this.DisplayColumns(this.dt, "0", 0);
                this.strColumn = this.sbTRows.ToString();
                string strDepth = this.strDepth;
                if (strDepth != null)
                {
                    if (!(strDepth == "0"))
                    {
                        if ((strDepth == "1") || (strDepth == "2"))
                        {
                            this.strColumn = this.strColumn + "</div></div>";
                        }
                    }
                    else
                    {
                        this.strColumn = this.strColumn + "</div>";
                    }
                }
            }
        }

        private void DisplayColumns(DataTable dt, string pid, int blank)
        {
            string str = " ";
            DataView view = new DataView(dt)
            {
                RowFilter = "ParentID = '" + pid + "'"
            };
            string str2 = "";
            if (blank > 0)
            {
                if (blank == 1)
                {
                    str2 = "&nbsp;&nbsp;";
                }
                for (int i = 2; i <= blank; i++)
                {
                    str2 = str2 + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
            }
            int num2 = 0;
            foreach (DataRowView view2 in view)
            {
                string str3 = view2["ID"].ToString();
                string s = view2["Depth"].ToString();
                num2++;
                if (blank > 0)
                {
                    if (view.Count != num2)
                    {
                        str = str2 + "├";
                    }
                    else
                    {
                        str = str2 + "└";
                    }
                }
                this.SetColumsRow(str, view2);
                int num3 = int.Parse(s) + 1;
                this.DisplayColumns(dt, str3, num3);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.DataToBind();
            }
        }

        private void SetColumsRow(string str, DataRowView drv)
        {
            this.strDepth = drv["depth"].ToString();
            string str2 = drv["ID"].ToString();
            string str3 = drv["ParentID"].ToString();
            if (this.strDepth == "0")
            {
                this.strID0Temp = str2;
                if (this.strID0 == "aa")
                {
                    this.sbTRows.Append("</div></div>");
                }
                string str4 = string.Concat(new object[] { "id1=", str2, "&Mdl=", drv["Module"] });
                if (this.strID0 == "")
                {
                    this.sbTRows.Append(string.Concat(new object[] { "<div class=\"hf1\"><a href=\"ContentManageNav.aspx?", str4, "\" target=\"Nav-frame\" >", drv["ColumnName"], "</a>\n" }));
                }
                else
                {
                    this.sbTRows.Append(string.Concat(new object[] { "<div style=\"clear:both;/\" class=\"hf1\"><a href=\"ContentManageNav.aspx?", str4, "\" target=\"Nav-frame\" >", drv["ColumnName"], "</a>\n" }));
                }
                this.strID0 = str2;
            }
            if (this.strDepth == "1")
            {
                if (this.strID0 == str3)
                {
                    this.sbTRows.Append("<div style=\"clear:both;width:auto;\">");
                    this.strID0 = "aa";
                    this.strFst = "└";
                    this.strMargin = "30";
                    this.nDepth1Num = 0;
                }
                else
                {
                    this.strFst = "";
                    this.strMargin = "60";
                    this.nDepth1Num++;
                }
                string str5 = string.Concat(new object[] { "id1=", str3, "&id2=", str2, "&Mdl=", drv["Module"] });
                if (this.nDepth1Num == 6)
                {
                    this.sbTRows.Append("<div style=\"clear:left;\"></div>\n");
                    this.nDepth1Num = 0;
                    this.strFst = "└";
                    this.strMargin = "30";
                }
                this.sbTRows.Append(string.Concat(new object[] { "<div class=\"hf2\" id=\"", str3, "_", str2, "\" style=\"float:left;margin-left:", this.strMargin, "px;\">", this.strFst, "<a href=\"ContentManageNav.aspx?", str5, "\" target=\"Nav-frame\" >", drv["ColumnName"], "</a></div>\n" }));
            }
            if (this.strDepth == "2")
            {
                string str6 = string.Concat(new object[] { "id1=", this.strID0Temp, "&id2=", str3, "&id3=", str2, "&Mdl=", drv["Module"] });
                string str7 = string.Concat(new object[] { str, "<a href='ContentManageNav.aspx?", str6, "' target='Nav-frame' >", drv["ColumnName"], "</a>" });
                base.ClientScript.RegisterStartupScript(base.GetType(), this.strID0Temp + "_" + str3 + "_" + str2, "var para=document.createElement(\"div\");para.className=\"hf3\";para.innerHTML=\"" + str7 + "\";$(\"" + this.strID0Temp + "_" + str3 + "\").appendChild(para);", true);
            }
        }

    }
}
