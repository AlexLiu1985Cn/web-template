namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class MobileColumn : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BSaveA;
        protected HtmlForm form1;
        protected HiddenField HdSaveID;
        protected Repeater RTMobile;

        protected void BSave_Click(object sender, EventArgs e)
        {
            string[] strArray = this.HdSaveID.Value.Trim().Split(new char[] { '|' });
            List<string[]> listData = new List<string[]>();
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i < strArray.Length; i++)
            {
                builder.Append("," + strArray[i]);
                string str = base.Request.Form["TNm" + i];
                string str2 = base.Request.Form["Sel" + i];
                if ((str == null) || (str2 == null))
                {
                    return;
                }
                string[] item = new string[] { strArray[i], str, str2 };
                listData.Add(item);
            }
            if (builder.Length == 0)
            {
                this.DataToBind();
            }
            else
            {
                string strSelect = "select id,ColumnName,NavMobile from QH_Column where id in (" + builder.ToString().Substring(1) + ")";
                string strUpdate = "UPDATE QH_Column SET ColumnName=@ColumnName,NavMobile=@NavMobile WHERE ID=@ID";
                string[] astrField = new string[] { "ColumnName", "NavMobile" };
                if (this.Bll1.DAL1.AdpaterUpdate(strSelect, strUpdate, astrField, listData) == listData.Count)
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

        private void DataToBind()
        {
            List<string[]> list = this.Bll1.DAL1.ReadDataReaderListStr("select id,ColumnName,NavMobile from QH_Column where depth='0' order by CLNG(Order1) asc,id asc", 3);
            List<string[]> list2 = new List<string[]>();
            int num = 0;
            this.HdSaveID.Value = "";
            foreach (string[] strArray2 in list)
            {
                num++;
                this.HdSaveID.Value = this.HdSaveID.Value + "|" + strArray2[0];
                string[] item = new string[] { num.ToString(), strArray2[1], "", "", "", "" };
                int index = (strArray2[2] == "") ? 5 : (int.Parse(strArray2[2]) + 2);
                item[index] = "selected";
                list2.Add(item);
            }
            this.RTMobile.DataSource = list2;
            this.RTMobile.DataBind();
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