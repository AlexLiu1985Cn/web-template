using _BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public class QH_TagsType : Page
{
    private string[] astrTitle = new string[] { "<a href=\"QH_TagsType.aspx?Mdl=2\" >新闻标签管理</a>", "<a href=\"QH_TagsType.aspx?Mdl=3\" > - 产品标签管理</a>", "<a href=\"QH_TagsType.aspx?Mdl=4\" > - 下载标签管理</a>", "<a href=\"QH_TagsType.aspx?Mdl=5\" > - 图片标签管理</a>" };
    private BLL Bll1 = new BLL();
    protected HtmlGenericControl CategoryTable;
    protected DataSet ds_Big;
    protected HtmlForm form1;
    protected HtmlHead Head1;
    protected HtmlGenericControl NewsTypeTable;
    protected string strMdl;
    protected string strMdlEnc;
    protected string strMdlName;
    private string strMdlName1;
    protected string strTagsType;

    private int GetFileOrder(string strMdl)
    {
        int num = 0;
        string str = strMdl;
        if (str == null)
        {
            return num;
        }
        if (!(str == "2"))
        {
            if (str != "3")
            {
                if (str == "4")
                {
                    return 2;
                }
                if (str != "5")
                {
                    return num;
                }
                return 3;
            }
        }
        else
        {
            return 0;
        }
        return 1;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Commen1.JudgeLogin(this.Page) && !this.Page.IsPostBack)
        {
            this.strMdlEnc = base.Server.HtmlEncode("2");
            this.strMdl = base.Request.QueryString["Mdl"];
            if (!string.IsNullOrEmpty(this.strMdl) && (((this.strMdl == "2") || (this.strMdl == "3")) || ((this.strMdl == "4") || (this.strMdl == "5"))))
            {
                string strMdl = this.strMdl;
                if (strMdl != null)
                {
                    if (!(strMdl == "2"))
                    {
                        if (strMdl == "3")
                        {
                            this.strMdlName1 = "产品";
                        }
                        else if (strMdl == "4")
                        {
                            this.strMdlName1 = "下载";
                        }
                        else if (strMdl == "5")
                        {
                            this.strMdlName1 = "图片";
                        }
                    }
                    else
                    {
                        this.strMdlName1 = "新闻";
                    }
                }
                this.strMdlName = string.Format("添加{0}标签", this.strMdlName1);
                string str = (this.strMdl == "2") ? "" : " - ";
                this.astrTitle[this.GetFileOrder(this.strMdl)] = str + this.strMdlName1 + "标签管理";
                StringBuilder builder = new StringBuilder();
                foreach (string str2 in this.astrTitle)
                {
                    builder.Append(str2);
                }
                this.strTagsType = builder.ToString();
                StringBuilder builder2 = new StringBuilder("");
                builder2.Append("<table width=\"840\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#ffffff\" align=\"center\">\n");
                builder2.Append("<tr style=\"background:url(images/1.jpg) repeat-x; height:30px; color:#FFFFFF; font-weight:bold;\"> \n");
                builder2.Append("<td width=\"40%\" height=\"30\" align=\"center\" ><strong>" + this.strMdlName1 + "标签名称</strong></td>\n");
                builder2.Append("<td width=\"40%\" height=\"30\" align=\"center\"><strong>操作选项</strong></td>\n");
                builder2.Append("</tr>\n");
                this.ReadCategoryBig();
                if (this.ds_Big != null)
                {
                    for (int i = 0; i < this.ds_Big.Tables[0].Rows.Count; i++)
                    {
                        string str3 = this.ds_Big.Tables[0].Rows[i]["TagsName"].ToString();
                        string str4 = this.ds_Big.Tables[0].Rows[i]["id"].ToString();
                        builder2.Append("<tr  height=\"28\" bgcolor=\"#" + (((i % 2) == 0) ? "e6e6e6" : "f9f9f9") + "\" class=\"tdbg\"  onmouseover=\"this.style.backgroundColor='#9ec0fe'\" onmouseout=\"this.style.backgroundColor=''\">");
                        builder2.Append("<td align=left width=\"233\" height=\"22\" style=\"margin-left:10px;\">&nbsp;&nbsp;" + str3 + "</td>");
                        builder2.Append(" <td align=\"center\" style=\"padding-right:0\"><a href=\"QH_TagsModify.aspx?ID=" + str4 + "\">修改</a>");
                        builder2.Append("  |&nbsp;<a href=\"QH_TagsDel.aspx?ID=" + str4 + "&Mdl=" + this.strMdl + "\" onClick=\"return ConfirmDelBig();\">删除</a></td>");
                        builder2.Append("</tr>");
                    }
                }
                builder2.Append("</table>");
                this.NewsTypeTable.InnerHtml = builder2.ToString();
            }
        }
    }

    protected void ReadCategoryBig()
    {
        DataTable dataTable = this.Bll1.GetDataTable("select id,TagsName from QH_Tags where [Module]='" + this.strMdl + "' Order by id asc");
        if (dataTable != null)
        {
            this.ds_Big = new DataSet();
            this.ds_Big.Merge(dataTable);
        }
    }
}