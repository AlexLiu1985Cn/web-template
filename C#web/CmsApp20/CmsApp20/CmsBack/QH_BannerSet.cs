using _BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _commen;

namespace CmsApp20.CmsBack
{
    public class QH_BannerSet : Page
    {
        private BLL Bll1 = new BLL();
        protected HtmlSelect BnrMode0;
        protected Button BSaveA;
        protected Button BtnReset;
        private DataTable dt;
        protected HtmlForm form1;
        protected HiddenField HdSaveID;
        private int nRNum;
        private StringBuilder sbTRows = new StringBuilder();
        protected string strAlertJS = "";
        protected string strColumn;

        protected void BSave_Click(object sender, EventArgs e)
        {
            try
            {
                string[] strArray;
                string strField = "BnrMode,BnrStyle,BnrWidth,BnrHeight,BnrDefault,id";
                string str2 = (base.Request.Form["TM0"] == null) ? "0" : base.Request.Form["TM0"].Trim();
                string str3 = (base.Request.Form["EF0"] == null) ? "0" : base.Request.Form["EF0"].Trim();
                string str4 = (base.Request.Form["TM1"] == null) ? "0" : base.Request.Form["TM1"].Trim();
                string str5 = (base.Request.Form["EF1"] == null) ? "0" : base.Request.Form["EF1"].Trim();
                string[] astrValue = strArray = new string[] { base.Request.Form["BnrMode0"].Trim(), (base.Request.Form["SelS0"] == null) ? "0" : (base.Request.Form["SelS0"].Trim() + "|" + str2 + "|" + str3), base.Request.Form["Width0"].Trim(), base.Request.Form["Height0"].Trim(), (base.Request.Form["Ck0"] == null) ? "0" : "1", "1" };
                string strUpdate = "update QH_ClmnBanner set BnrMode=@BnrMode,BnrStyle=@BnrStyle,BnrWidth=@BnrWidth,BnrHeight=@BnrHeight,BnrDefault=@BnrDefault WHERE ID=@id";
                int num = this.Bll1.UpdateTable(strUpdate, strField, astrValue);
                if (base.Request.Form["Ck1"] == null)
                {
                    astrValue = new string[] { base.Request.Form["BnrMode1"].Trim(), (base.Request.Form["SelS1"] == null) ? "0" : (base.Request.Form["SelS1"].Trim() + "|" + str4 + "|" + str5), base.Request.Form["Width1"].Trim(), base.Request.Form["Height1"].Trim(), "0", "2" };
                }
                else
                {
                    astrValue[4] = "1";
                    astrValue[5] = "2";
                }
                int num2 = this.Bll1.UpdateTable(strUpdate, strField, astrValue);
                string[] strArray3 = this.HdSaveID.Value.Trim().Split(new char[] { '|' });
                List<List<string>> listData = new List<List<string>>();
                StringBuilder builder = new StringBuilder();
                for (int i = 1; i < strArray3.Length; i++)
                {
                    List<string> list2;
                    builder.Append("," + strArray3[i]);
                    string str7 = base.Request.Form["BnrMode" + (i + 1)];
                    string str8 = (base.Request.Form["SelS" + (i + 1)] == null) ? "0" : base.Request.Form["SelS" + (i + 1)].Trim();
                    string str9 = (base.Request.Form["TM" + (i + 1)] == null) ? "0" : base.Request.Form["TM" + (i + 1)].Trim();
                    string str10 = (base.Request.Form["EF" + (i + 1)] == null) ? "0" : base.Request.Form["EF" + (i + 1)].Trim();
                    string str15 = str8;
                    str8 = str15 + "|" + str9 + "|" + str10;
                    string str11 = base.Request.Form["Width" + (i + 1)];
                    string str12 = base.Request.Form["Height" + (i + 1)];
                    string str13 = (base.Request.Form["Ck" + (i + 1)] == null) ? "false" : "true";
                    if (((str7 == null) || (str11 == null)) || (str12 == null))
                    {
                        return;
                    }
                    if (str13 == "false")
                    {
                        list2 = new List<string>(new string[] { strArray3[i], str7, str8, str11, str12, str13 });
                    }
                    else
                    {
                        list2 = new List<string>(new string[] { strArray3[i], strArray[0], strArray[1], strArray[2], strArray[3], str13 });
                    }
                    listData.Add(list2);
                }
                string strSelect = "select id,BnrMode,BnrStyle,BnrWidth,BnrHeight,BnrDefault from QH_Column where id in (" + builder.ToString().Substring(1) + ")";
                strUpdate = "UPDATE QH_Column SET BnrMode=@BnrMode,BnrStyle=@BnrStyle,BnrWidth=@BnrWidth,BnrHeight=@BnrHeight,BnrDefault=@BnrDefault WHERE ID=@ID";
                string[] astrField = new string[] { "BnrMode", "BnrStyle", "BnrWidth", "BnrHeight", "BnrDefault" };
                if (((this.Bll1.UpdateSelAll(strSelect, strUpdate, astrField, listData) == listData.Count) && (num == 1)) && (num2 == 1))
                {
                    this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('修改成功！');</script>";
                }
                else
                {
                    this.strAlertJS = "<script>WebForm_RestoreScrollPosition();alert('添加失败！');</script>";
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("修改Banner设置失败：" + exception.ToString());
            }
            this.DataToBind();
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.DataToBind();
        }

        private void DataToBind()
        {
            DataTable dataTable = this.Bll1.GetDataTable("select top 2 * from QH_ClmnBanner order by id asc");
            for (int i = 0; i < 2; i++)
            {
                string[] strArray = dataTable.Rows[i]["BnrStyle"].ToString().Split(new char[] { '|' });
                string str = (strArray.Length <= 1) ? "" : strArray[1];
                string str2 = (strArray.Length <= 2) ? "" : strArray[2];
                string str3 = dataTable.Rows[i]["BnrMode"].ToString();
                string str4 = (str3 == "1") ? strArray[0] : "0";
                base.ClientScript.RegisterStartupScript(base.GetType(), "Row" + i, string.Concat(new object[] { 
                    "<script>$('BnrMode", i, "').selectedIndex=", str3, ";if(", str3, "==1){ AddSelS($('BnrMode", i, "').parentNode,", i, ",", str4, ");$('TM", i, "').value=\"", str, 
                    "\";$('EF", i, "').value=\"", str2, "\";}$('Width", i, "').value=\"", dataTable.Rows[i]["BnrWidth"], "\";$('Height", i, "').value=\"", dataTable.Rows[i]["BnrHeight"], "\";$('Ck", i, "').checked=", dataTable.Rows[i]["BnrDefault"].ToString().ToLower(), 
                    ";  </script>"
                 }));
            }
            this.HdSaveID.Value = "";
            this.dt = this.Bll1.GetDataTable("select id,ColumnName,ParentID,depth,BnrMode,BnrStyle,BnrWidth,BnrHeight,BnrDefault from QH_Column order by CLNG(ParentID) asc,CLNG(depth) asc, CLNG(Order1) asc,id asc");
            if (this.dt != null)
            {
                this.DisplayColumns(this.dt, "0", 0);
                this.strColumn = this.sbTRows.ToString();
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
            this.nRNum++;
            this.HdSaveID.Value = this.HdSaveID.Value + "|" + drv["ID"].ToString();
            string[] strArray = new string[] { "", "", "", "" };
            strArray[int.Parse((drv["BnrMode"].ToString() == "") ? "0" : drv["BnrMode"].ToString())] = "selected";
            string str2 = "";
            if (drv["BnrMode"].ToString() == "1")
            {
                string[] strArray2 = drv["BnrStyle"].ToString().Split(new char[] { '|' });
                string str3 = (strArray2.Length <= 1) ? "" : strArray2[1];
                string str4 = (strArray2.Length <= 2) ? "" : strArray2[2];
                StringBuilder builder = new StringBuilder(string.Concat(new object[] { "<select name=SelS", this.nRNum + 1, " id=SelS", this.nRNum + 1, " onChange=\"ClearTM(", this.nRNum + 1, ");\" >\n" }));
                string[] strArray3 = new string[] { "", "", "", "", "", "", "", "" };
                strArray3[int.Parse((strArray2[0] == "") ? "0" : strArray2[0]) - 1] = "selected";
                for (int i = 1; i <= 5; i++)
                {
                    switch (i)
                    {
                        case 4:
                        case 5:
                            builder.Append(string.Concat(new object[] { "        <option  value=", i, " ", strArray3[i - 1], " >样式", i, "(全屏)</option>\n" }));
                            break;

                        default:
                            builder.Append(string.Concat(new object[] { "        <option  value=", i, " ", strArray3[i - 1], " >样式", i, "</option>\n" }));
                            break;
                    }
                }
                builder.Append("   </select>\n");
                builder.Append(string.Concat(new object[] { "&nbsp;&nbsp;切换时间：<input name =\"TM", this.nRNum + 1, "\" id =\"TM", this.nRNum + 1, "\" type=\"text\" value=\"", str3, "\" class=orTM  maxlength=3 />秒\n" }));
                builder.Append(string.Concat(new object[] { "&nbsp;&nbsp;效果编号：<input name =\"EF", this.nRNum + 1, "\" id =\"EF", this.nRNum + 1, "\" type=\"text\" value=\"", str4, "\" class=orTM  maxlength=3 />\n" }));
                str2 = builder.ToString();
            }
            this.sbTRows.Append("<tr bgcolor=\"#" + (((this.nRNum % 2) == 1) ? "e6e6e6" : "f9f9f9") + "\" onmouseover=\"this.style.backgroundColor='#9ec0fe'\" onmouseout=\"this.style.backgroundColor=''\" height=25px >\n");
            this.sbTRows.Append(string.Concat(new object[] { "<td align=left class=\"Cln\" >", str, drv["ColumnName"], "</td>\n" }));
            this.sbTRows.Append(string.Concat(new object[] { "<td align=left >    <select name=\"BnrMode", this.nRNum + 1, "\" onChange=\"BnrSel(this,", this.nRNum + 1, ")\" >\n" }));
            this.sbTRows.Append("        <option  value=0 " + strArray[0] + " >关闭</option>\n");
            this.sbTRows.Append("        <option  value=1 " + strArray[1] + " style=\"color:#FF0000\">图片轮播</option>\n");
            this.sbTRows.Append("        <option  value=2 " + strArray[2] + " style=\"color:#003399\">Flash动画</option>\n");
            this.sbTRows.Append("        <option  value=3 " + strArray[3] + " style=\"color:#339933\">单张图片</option>\n");
            this.sbTRows.Append("   </select>\n");
            this.sbTRows.Append(str2);
            this.sbTRows.Append(string.Concat(new object[] { "</td><td><input  name =\"Width", this.nRNum + 1, "\" type=\"text\" maxlength=4 class=\"or\" value='", drv["BnrWidth"], "' /></td>\n" }));
            this.sbTRows.Append(string.Concat(new object[] { "<td><input  name =\"Height", this.nRNum + 1, "\" type=\"text\" maxlength=3 class=\"or\" value='", drv["BnrHeight"], "' /></td>\n" }));
            this.sbTRows.Append("<td style=\"text-align:center;\">预览</td>\n");
            this.sbTRows.Append(string.Concat(new object[] { "<td style=\"text-align:center;\"><input name=\"Ck", this.nRNum + 1, "\" id=\"Ck", this.nRNum + 1, "\" type=\"checkbox\" ", ((bool)drv["BnrDefault"]) ? "checked" : "", " /></td>\n" }));
            this.sbTRows.Append("</tr>\n");
        }
    }
}
