using _DAL.OleDBHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace CmsApp20.CmsBack
{
    public class Left_ContentNew : Page
    {
        private DAL DAL1 = new DAL();
        protected HtmlInputHidden HdnNum;
        protected string strContentMng;
        protected string strJsNum;
        protected string strScript;

        private void bindClmumnFst()
        {
            List<string[]> list = this.DAL1.ReadDataReaderListStr("select id,ColumnName,[Module] from QH_Column where ParentID='0' order by CLNG(Order1) asc", 3);
            this.strScript = "<script type='text/javascript' >$1('HdnNum').value='" + list.Count.ToString() + "';</script>";
            if (list.Count != 0)
            {
                StringBuilder builder = new StringBuilder();
                int num = int.Parse(this.HdnNum.Value);
                foreach (string[] strArray in list)
                {
                    strArray[1] = (strArray[1].Length > 8) ? strArray[1].Substring(0, 8) : strArray[1];
                    builder.Append(string.Concat(new object[] { "<div class=\"left02down01C\"><span style =\"float :left ;\"><a href=\"ContentManageNav.aspx?id1=", strArray[0], "&Mdl=", strArray[2], "\" target=\"Nav-frame\" onclick=SetNow(", num, ") id=a", num, " >", strArray[1], "</a></span><span style =\"float :right ;margin-right:30px;\"><a href=\"CreateSFColumn.aspx?id=", strArray[0], "\" target=\"main-frame\" onclick=SetNow(", num++, ") >生成</a></span></div>\n" }));
                }
                this.strContentMng = builder.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.bindClmumnFst();
            }
        }
    }
}
