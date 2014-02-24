using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using _commen;
using _BLL;
using _DAL.OleDBHelper;

namespace CmsApp20.CalliBack
{
    public class SystemSet_Left : Page
    {
        private DAL dal1 = new DAL();
        protected HtmlForm f111;
        protected string strAdmin;

        public string GetTemplateContent(string strAbsPath1)
        {
            string str = null;
            Encoding encoding = Encoding.UTF8;
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(strAbsPath1, encoding);
                str = reader.ReadToEnd();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("读模版错误： " + exception.ToString());
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.strAdmin = HttpUtility.UrlDecode(base.Request.Cookies["95CmsAdmin"].Value);
                this.SetDomain();
            }
        }

        private void SetDomain()
        {
            string absoluteUri = base.Request.Url.AbsoluteUri;
            int num = absoluteUri.LastIndexOf('/');
            absoluteUri = absoluteUri.Substring(0, absoluteUri.LastIndexOf('/', num - 1) + 1);
            string str2 = Convert.ToString(this.dal1.ExecuteScalar("select top 1  SitePath from QH_SiteInfo "));
            if (absoluteUri != str2)
            {
                this.dal1.ExecuteNonQuery("update QH_SiteInfo set SitePath='" + absoluteUri + "',MobilePath='" + absoluteUri + "m/' WHERE ID=1");
            }
        }

        public void WriteFile(string strAbsPath, string strTemp)
        {
            Encoding encoding = Encoding.UTF8;
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(strAbsPath, false, encoding);
                writer.Write(strTemp);
                writer.Flush();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("写静态文件错误： " + exception.ToString());
            }
            finally
            {
                writer.Close();
            }
        }
    }
}
