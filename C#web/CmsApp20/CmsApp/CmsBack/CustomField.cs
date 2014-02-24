namespace CmsApp.CmsBack
{
    using _BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using _commen;

    public class CustomField : Page
    {
        protected Button BDel;
        private BLL Bll1 = new BLL();
        protected Button BSave1;
        protected Button BSaveA;
        protected Button BSaveN;
        private DataTable dt;
        protected HtmlForm form1;
        protected HiddenField HdDel;
        protected HiddenField HdSave;
        protected HiddenField HdSaveID;
        protected HiddenField HdSaveN;
        protected HtmlHead Head1;
        private int nRNum;
        private StringBuilder sbTRows = new StringBuilder();
        protected string strField;
        protected string strMdl;
        protected string strMdlName;

        protected void BDel_Click(object sender, EventArgs e)
        {
            this.Bll1.Delete("delete from QH_Parameter where id=" + this.HdDel.Value);
            this.Bll1.Delete("delete from QH_List where BigID='" + this.HdDel.Value + "'");
            this.Bll1.Delete("delete from QH_pList where paraid='" + this.HdDel.Value + "' and [module]='" + ((string)this.ViewState["Mdl"]) + "'");
            this.DataToBind();
        }

        protected void BSave_Click(object sender, EventArgs e)
        {
            string str = (base.Request.Form["CkN"] == null) ? "" : base.Request.Form["CkN"].ToString();
            if ((str == "on") && (this.SaveNewColumn() == 3))
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "IDMN", "<script>alert('其它栏目已有此字段标识，字段标识不能相同！');</script>");
                this.DataToBind();
            }
            else
            {
                string[] strArray = this.HdSaveID.Value.Trim().Split(new char[] { '|' });
                List<List<string>> listData = new List<List<string>>();
                StringBuilder builder = new StringBuilder();
                for (int i = 1; i < strArray.Length; i++)
                {
                    string str2 = (base.Request.Form["Ck" + i] == null) ? "" : base.Request.Form["Ck" + i].ToString();
                    if (str2 == "on")
                    {
                        List<string> list3;
                        builder.Append("," + strArray[i]);
                        string str3 = base.Request.Form["IDM" + i];
                        string str4 = base.Request.Form["TOr" + i];
                        string str5 = base.Request.Form["TNm" + i];
                        string str6 = base.Request.Form["Sel" + i];
                        if (((str3 == null) || (str4 == null)) || ((str5 == null) || (str6 == null)))
                        {
                            return;
                        }
                        foreach (string str7 in this.Bll1.DAL1.ReadDataReaderAL("select IDMark from QH_Parameter where [Module]='" + ((string)this.ViewState["Mdl"]) + "' and id<>" + strArray[i]))
                        {
                            if (str7 == str3)
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "IDMA", "<script>alert('其它栏目已有此字段标识，字段标识不能相同！');</script>");
                                this.DataToBind();
                                return;
                            }
                        }
                        list3 = new List<string>(new string[] { strArray[i], base.Request.Form["TNm" + i].Trim(), base.Request.Form["TOr" + i].Trim(), base.Request.Form["Type" + i].Trim(), base.Request.Form["access" + i].Trim(), (base.Request.Form["wr_ok_" + i] == null) ? "false" : "true", base.Request.Form["Sel" + i].Trim(), str3 })
                        {
                            //  list3
                        };
                    }
                }
                if (builder.Length != 0)
                {
                    string strSelect = "select id,name,no_order,type,Access,wr_ok,columnID,IDMark from QH_Parameter where id in (" + builder.ToString().Substring(1) + ")";
                    string strUpdate = "UPDATE QH_Parameter SET name=@name,no_order=@no_order,type=@type,Access=@Access,wr_ok=@wr_ok,columnID=@columnID,IDMark=@IDMark WHERE ID=@ID";
                    string[] astrField = new string[] { "name", "no_order", "type", "Access", "wr_ok", "columnID", "IDMark" };
                    if (this.Bll1.UpdateSelAll(strSelect, strUpdate, astrField, listData) == listData.Count)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "saveOk", "<script>alert('修改成功！');</script>");
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "saveFail", "<script>alert('修改失败！');</script>");
                    }
                    this.DataToBind();
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "saveOk", "<script>alert('添加成功！');</script>");
                    this.DataToBind();
                }
            }
        }

        protected void BSave1_Click(object sender, EventArgs e)
        {
            string[] strArray = this.HdSave.Value.Trim().Split(new char[] { '|' });
            if (strArray.Length == 2)
            {
                string str = base.Request.Form["IDM" + strArray[0]];
                foreach (string str2 in this.Bll1.DAL1.ReadDataReaderAL("select IDMark from QH_Parameter where [Module]='" + ((string)this.ViewState["Mdl"]) + "' and id<>" + strArray[1]))
                {
                    if (str2 == str)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "IDM1", "<script>alert('其它栏目已有此字段标识，字段标识不能相同！');</script>");
                        this.DataToBind();
                        return;
                    }
                }
                string str3 = (base.Request.Form["wr_ok_" + strArray[0]] == null) ? "0" : "1";
                string strField = "name,no_order,type,Access,wr_ok,columnID,IDMark,id";
                string[] astrValue = new string[] { base.Request.Form["TNm" + strArray[0]].Trim(), base.Request.Form["TOr" + strArray[0]].Trim(), base.Request.Form["Type" + strArray[0]].Trim(), base.Request.Form["access" + strArray[0]].Trim(), str3, base.Request.Form["Sel" + strArray[0]].Trim(), str, strArray[1] };
                string strUpdate = "UPDATE QH_Parameter SET name=@name,no_order=@no_order ,type=@type,Access=@Access,wr_ok=@wr_ok,columnID=@columnID,IDMark=@IDMark WHERE ID=@id";
                if (this.Bll1.UpdateTable(strUpdate, strField, astrValue) == 1)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "saveOk", "<script>alert('修改成功！');</script>");
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "saveFail", "<script>alert('修改失败！');</script>");
                }
                this.DataToBind();
            }
        }

        protected void BSaveN_Click(object sender, EventArgs e)
        {
            switch (this.SaveNewColumn())
            {
                case 1:
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "AddOk", "<script>alert('添加成功！');</script>");
                    break;

                case 3:
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "IDM", "<script>alert('其它栏目已有此字段标识，字段标识不能相同！');</script>");
                    break;

                default:
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "AddFail", "<script>alert('添加失败！');</script>");
                    break;
            }
            this.DataToBind();
        }

        private void DataToBind()
        {
            this.strMdl = (string)this.ViewState["Mdl"];
            this.strMdl = base.Server.HtmlEncode(this.strMdl);
            this.strMdlName = this.Bll1.GetTmpltFileName(this.strMdl);
            this.dt = this.Bll1.GetDataTable("select * from QH_Parameter where [Module]='" + this.strMdl + "' order by CLNG(no_order) asc,id asc");
            if (this.dt != null)
            {
                DataTable dataTable = this.Bll1.GetDataTable("select id, ColumnName from QH_Column where [Module]='" + this.strMdl + "' order by CLNG(order1) asc,id asc");
                if (dataTable != null)
                {
                    foreach (DataRow row in this.dt.Rows)
                    {
                        this.nRNum++;
                        this.HdSaveID.Value = this.HdSaveID.Value + "|" + row["ID"].ToString();
                        string str2 = row["columnID"].ToString();
                        string str3 = row["Access"].ToString();
                        string str4 = row["type"].ToString();
                        string str5 = ((str4 == "0") || (str4 == "2")) ? "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp" : string.Concat(new object[] { "<a href =ListEdit.aspx?id=", row["ID"], "&Mdl=", (string)this.ViewState["Mdl"], " >设置选项 </a>" });
                        this.sbTRows.Append("<tr  bgcolor=\"#" + (((this.nRNum % 2) == 1) ? "e6e6e6" : "f9f9f9") + "\" onmouseover=\"this.style.backgroundColor='#9ec0fe'\" onmouseout=\"this.style.backgroundColor=''\"  >\n");
                        this.sbTRows.Append("<td><input name=\"Ck" + this.nRNum + "\" type=\"checkbox\" /></td>\n");
                        this.sbTRows.Append(string.Concat(new object[] { "<td><input  name =\"IDM", this.nRNum, "\" id=\"IDM", this.nRNum, "\" type=\"text\" class=or  maxlength=3 value='", row["IDMark"], "' /></td>\n" }));
                        this.sbTRows.Append(string.Concat(new object[] { "<td ><input  name =\"TOr", this.nRNum, "\" id=\"TOr", this.nRNum, "\" type=\"text\" class=or  maxlength=3 value='", row["no_order"], "' /></td>\n" }));
                        this.sbTRows.Append(string.Concat(new object[] { "<td><input name=\"TNm", this.nRNum, "\"  id=\"TNm", this.nRNum, "\" type=\"text\"  class=or1 value='", row["name"], "'/></td>\n" }));
                        this.sbTRows.Append("<td>\n");
                        this.sbTRows.Append("    <select name=\"Sel" + this.nRNum + "\" >\n");
                        this.sbTRows.Append("        <option  value=0 " + ((str2 == "0") ? "selected" : "") + " >所有栏目</option>\n");
                        foreach (DataRow row2 in dataTable.Rows)
                        {
                            string str = row2["ColumnName"].ToString();
                            if (str.Length > 8)
                            {
                                str = str.Substring(0, 8) + "...";
                            }
                            this.sbTRows.Append(string.Concat(new object[] { "        <option  value=", row2["id"], " ", (str2 == row2["id"].ToString()) ? "selected" : "", " >", str, "</option>\n" }));
                        }
                        this.sbTRows.Append("   </select>\n");
                        this.sbTRows.Append("</td>\n");
                        this.sbTRows.Append("<td><select name=\"access" + this.nRNum + "\" >\n");
                        this.sbTRows.Append("    <option value='0' " + ((str3 == "0") ? "selected" : "") + " >不限    </option>\n");
                        this.sbTRows.Append("    <option value='1' " + ((str3 == "1") ? "selected" : "") + " >普通会员</option>\n");
                        this.sbTRows.Append("    <option value='2' " + ((str3 == "2") ? "selected" : "") + " >高级会员</option>\n");
                        this.sbTRows.Append("    <option value='3' " + ((str3 == "3") ? "selected" : "") + " >管理员  </option></select>\n");
                        this.sbTRows.Append("   </td>\n");
                        this.sbTRows.Append("<td><select name=\"Type" + this.nRNum + "\" >\n");
                        this.sbTRows.Append("    <option value='0' " + ((str4 == "0") ? "selected" : "") + " >简短</option>\n");
                        this.sbTRows.Append("    <option value='1' " + ((str4 == "1") ? "selected" : "") + " >下拉</option>\n");
                        this.sbTRows.Append("    <option value='2' " + ((str4 == "2") ? "selected" : "") + " >文本</option>\n");
                        this.sbTRows.Append("    <option value='3' " + ((str4 == "3") ? "selected" : "") + " >多选</option>\n");
                        this.sbTRows.Append("    <option value='4' " + ((str4 == "4") ? "selected" : "") + " >单选</option>\n");
                        this.sbTRows.Append("    </select>\n");
                        this.sbTRows.Append("</td>\n");
                        this.sbTRows.Append(string.Concat(new object[] { "<td><input type=\"checkbox\" name=wr_ok_", this.nRNum, " value=1 ", ((bool)row["wr_ok"]) ? "checked='checked'" : "", "/></td>\n" }));
                        this.sbTRows.Append(string.Concat(new object[] { "<td align=left ><a href =javascript:Save(", this.nRNum, ",", row["ID"], ")  >保存</a>&nbsp;&nbsp;", str5, "&nbsp;&nbsp;\n" }));
                        this.sbTRows.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:DelF(" + row["ID"] + ") >删除</a>\n");
                        this.sbTRows.Append("</td>\n");
                        this.sbTRows.Append("</tr>\n");
                    }
                    this.strField = this.sbTRows.ToString();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.strMdl = (base.Request.QueryString["Mdl"] == null) ? "" : base.Request.QueryString["Mdl"];
                if (((this.strMdl != "3") && (this.strMdl != "4")) && (this.strMdl != "5"))
                {
                    base.Response.Write("<script>alert('参数不对！');</script>");
                }
                else
                {
                    this.ViewState["Mdl"] = this.strMdl;
                    this.DataToBind();
                }
            }
        }

        private int SaveNewColumn()
        {
            string str = base.Request.Form["IDMN"];
            foreach (string str2 in this.Bll1.DAL1.ReadDataReaderAL("select IDMark from QH_Parameter where [Module]='" + ((string)this.ViewState["Mdl"]) + "'"))
            {
                if (str2 == str)
                {
                    return 3;
                }
            }
            string str3 = (base.Request.Form["wr_ok_N"] == null) ? "0" : "1";
            string strField = "name,no_order,type,Access,wr_ok,columnID,Module,IDMark";
            string[] astrValue = new string[] { base.Request.Form["TNmN"].Trim(), base.Request.Form["TOrN"].Trim(), base.Request.Form["TypeN"].Trim(), base.Request.Form["accessN"].Trim(), str3, base.Request.Form["SelN"].Trim(), (string)this.ViewState["Mdl"], str };
            string strUpdate = "insert into QH_Parameter(name,no_order,type,Access,wr_ok,columnID,[Module],IDMark) values(@name,@no_order,@type,@Access,@wr_ok,@columnID,@Module,@IDMark)";
            return this.Bll1.UpdateTable(strUpdate, strField, astrValue);
        }
    }
}