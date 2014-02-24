using _DAL.OleDBHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace CmsApp20.CalliBack
{
    public class PageStyle_Left : Page
    {
        private DAL DAL1 = new DAL();
        protected HtmlInputHidden HdnNum;
        protected string strScript;
        protected string strTemplate;

        private void bindCustomTemplate()
        {
            List<string[]> list = this.DAL1.ReadDataReaderListStr("select ColumnName,ParentID,depth,[Module],TemplateName from QH_Column where TemplateName<>'' order by CLNG(ParentID) asc,CLNG(depth) asc, CLNG(Order1) asc,id asc", 5);
            this.strScript = "<script type='text/javascript' >$1('HdnNum').value='" + list.Count.ToString() + "';</script>";
            if (list.Count != 0)
            {
                StringBuilder builder = new StringBuilder();
                int num = int.Parse(this.HdnNum.Value);
                string str = ",";
                foreach (string[] strArray in list)
                {
                    if (!str.Contains(strArray[4]))
                    {
                        builder.Append(string.Concat(new object[] { 
                            "<div class=\"left02down01\"><a href=\"CustomTmplt.aspx?File=", strArray[4], "&LM=", base.Server.UrlEncode(strArray[0]), "&Depth=", strArray[2], "&PID=", strArray[1], "&Mdl=", strArray[3], "\" target=\"main-frame\" onclick=SetNow(", num, ") id=a", num++, " >", strArray[0], 
                            "</a></div>\n"
                         }));
                        str = str + strArray[4] + ",";
                    }
                }
                this.strTemplate = builder.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.bindCustomTemplate();
            }
        }
    }
}
