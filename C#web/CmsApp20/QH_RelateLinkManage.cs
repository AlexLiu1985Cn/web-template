using _BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public class QH_RelateLinkManage : Page
{
    private BLL Bll1 = new BLL();
    protected HtmlGenericControl CategoryTable;
    protected DataSet ds_Big;
    protected DataSet ds_small;
    protected HtmlForm form1;
    protected HtmlGenericControl NewsTypeTable;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Commen1.JudgeLogin(this.Page) && !this.Page.IsPostBack)
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append("<table width=\"840\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#ffffff\" align=\"center\">\n");
            builder.Append("<tr style=\"background:url(images/1.jpg) repeat-x; height:30px; color:#FFFFFF; font-weight:bold;\"> \n");
            builder.Append("<td width=\"40%\" height=\"30\" align=\"center\" ><strong>热点关键词</strong></td>\n");
            builder.Append("<td width=\"40%\" height=\"30\" align=\"center\" ><strong>链接地址</strong></td>\n");
            builder.Append("<td width=\"20%\" height=\"30\" align=\"center\"><strong>操作选项</strong></td>\n");
            builder.Append("</tr>\n");
            this.ReadCategoryBig();
            for (int i = 0; i < this.ds_Big.Tables[0].Rows.Count; i++)
            {
                string str = this.ds_Big.Tables[0].Rows[i]["Tags"].ToString();
                string str2 = this.ds_Big.Tables[0].Rows[i]["Link"].ToString();
                string str3 = this.ds_Big.Tables[0].Rows[i]["id"].ToString();
                builder.Append("<tr  height=\"28\" bgcolor=\"#" + (((i % 2) == 0) ? "e6e6e6" : "f9f9f9") + "\" class=\"tdbg\"  onmouseover=\"this.style.backgroundColor='#9ec0fe'\" onmouseout=\"this.style.backgroundColor=''\">");
                builder.Append("<td align=left width=\"233\" style=\"margin-left:10px;\">&nbsp;&nbsp;" + str + "</td>");
                builder.Append("<td align=left width=\"233\" style=\"margin-left:10px;\">&nbsp;&nbsp;" + str2 + "</td>");
                builder.Append(" <td align=\"center\" style=\"padding-right:0\"><a href=\"QH_RelateLinkModify.aspx?ID=" + str3 + "\">修改</a>");
                builder.Append("  &nbsp;&nbsp;|&nbsp;&nbsp;<a href=\"QH_RelateLinkDel.aspx?ID=" + str3 + "\" onClick=\"return ConfirmDelBig();\">删除</a></td>");
                builder.Append("</tr>");
            }
            builder.Append("</table>");
            this.NewsTypeTable.InnerHtml = builder.ToString();
        }
    }

    protected void ReadCategoryBig()
    {
        DataTable dataTable = this.Bll1.GetDataTable("select * from QH_RelatedLink Order by id asc");
        if (dataTable != null)
        {
            this.ds_Big = new DataSet();
            this.ds_Big.Merge(dataTable);
        }
    }
}