namespace CmsApp20.CmsBack
{
    using _DAL.OleDBHelper;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class QH_StatLast : Page
    {
        private DAL DAL1 = new DAL();
        protected HtmlForm form1;
        protected Repeater RStatList;

        private void DataToBind()
        {
            List<string[]> list = this.DAL1.ReadDataReaderListStr("select CIP,CUrl,iif(CRef='','直接输入',CRef),Format(ATime,'yyyy-MM-dd HH:mm:ss') from QH_Statistics order by ATime desc", 4);
            this.RStatList.DataSource = list;
            this.RStatList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.DataToBind();
            }
        }

        public string SetLastLength(string str, int len)
        {
            if (str.Length > len)
            {
                str = str.Substring(str.Length - len, len);
                return str;
            }
            return str;
        }

        public string SetLength(string str, int len)
        {
            if (str.Length > len)
            {
                str = str.Substring(0, len);
                return str;
            }
            return str;
        }
    }
}