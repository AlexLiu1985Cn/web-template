namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using _commen;

    public class QH_DataBackup : Page
    {
        protected Button BtnBak;
        protected Button BtnComP;
        protected HtmlForm form1;
        protected string strFileSize = "";

        protected void BtnBak_Click(object sender, EventArgs e)
        {
            string path = base.Server.MapPath("../DBBAK/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string str2 = "../DBBAK/DB_" + DateTime.Now.ToString("yyyyMMddhhmmssfffBak") + ".mdb";
            bool flag = true;
            try
            {
                File.Copy(base.Server.MapPath("../App_Data/" + ConfigurationManager.AppSettings["dbPath"]), base.Server.MapPath(str2));
            }
            catch (Exception exception)
            {
                flag = false;
                SystemError.CreateErrorLog("备份数据库错误： " + exception.ToString());
            }
            if (flag)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "OK", "<script>alert('备份成功')</script>");
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "NotOK", "<script>alert('备份失败')</script>");
            }
            this.SetDBFileSize();
        }

        protected void BtnComP_Click(object sender, EventArgs e)
        {
            bool flag = true;
            try
            {
                CompressAcDb.CompactAccessDB("provider=microsoft.jet.oledb.4.0;data source=" + base.Server.MapPath("../App_Data/" + ConfigurationManager.AppSettings["dbPath"]), base.Server.MapPath("../App_Data/" + ConfigurationManager.AppSettings["dbPath"]), base.Server.MapPath("../CompressTempfile.mdb"));
            }
            catch (Exception exception)
            {
                flag = false;
                SystemError.CreateErrorLog("备份数据库错误： " + exception.ToString());
            }
            if (flag)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "COK", "<script>alert('压缩成功')</script>");
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "CNotOK", "<script>alert('压缩失败')</script>");
            }
            this.SetDBFileSize();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.SetDBFileSize();
            }
        }

        private void SetDBFileSize()
        {
            long size = 0L;
            string path = base.Server.MapPath("../App_Data/" + ConfigurationManager.AppSettings["dbPath"]);
            if (File.Exists(path))
            {
                size = new FileInfo(path).Length;
            }
            this.strFileSize = CompressAcDb.CountSize(size);
        }
    }
}