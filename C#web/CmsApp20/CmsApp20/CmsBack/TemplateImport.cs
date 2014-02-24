namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;
    using _commen;

    public class TemplateImport : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnDel;
        protected Button BtnEmp;
        protected Button BtnImport;
        protected HiddenField Continue;
        protected HtmlInputFile File1;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        private string[] strErrMsg = new string[] { "您没有选择上传文件", "上传文件不能大于100M", "未能上传文件，请重试！或查看Systemlog.log日志文件", "上传文件必须为.cdqh文件" };
        private string strFileName = "";

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            base.ClientScript.RegisterStartupScript(base.GetType(), "set0", "<script>document.getElementById('Continue').value=0;</script>");
            string str = base.Server.MapPath("~/template/");
            if (File.Exists(str + ((string)this.ViewState["xmlName"])))
            {
                File.Delete(str + ((string)this.ViewState["xmlName"]));
            }
        }

        protected void BtnEmp_Click(object sender, EventArgs e)
        {
            base.ClientScript.RegisterStartupScript(base.GetType(), "set0", "<script>document.getElementById('Continue').value=0;</script>");
            bool flag = true;
            FileStream output = null;
            BinaryWriter writer = null;
            string str = base.Server.MapPath("~/template/");
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(str + ((string)this.ViewState["xmlName"]));
                foreach (XmlNode node in document.SelectNodes("/list/folder"))
                {
                    XmlNode node2 = node.SelectSingleNode("Path");
                    string path = str + node2.InnerText;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
                foreach (XmlNode node3 in document.SelectNodes("/list/file"))
                {
                    XmlNode node4 = node3.SelectSingleNode("Path");
                    string str3 = str + node4.InnerText;
                    XmlNode node5 = node3.SelectSingleNode("content");
                    output = new FileStream(str3, FileMode.Create, FileAccess.Write);
                    writer = new BinaryWriter(output);
                    writer.Write(Convert.FromBase64String(node5.InnerText.Substring(10)));
                    writer.Close();
                    output.Close();
                }
            }
            catch (Exception exception)
            {
                flag = false;
                SystemError.CreateErrorLog("还原模板文件错误： " + exception.ToString());
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
                if (output != null)
                {
                    output.Close();
                }
            }
            if (File.Exists(str + ((string)this.ViewState["xmlName"])))
            {
                File.Delete(str + ((string)this.ViewState["xmlName"]));
            }
            bool flag2 = this.setTemplateMdbData(1);
            if (flag && flag2)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "success", "<script>alert('已成功导入模板，请点击模板展示查看模板。');</script>");
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "failure", "<script>alert('导入模板错误，请重试，详细信息请查看错误日志Systemlog.log。');</script>");
            }
        }

        protected void BtnImport_Click(object sender, EventArgs e)
        {
            int index = this.uploadFile();
            if (index != -1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "Err" + index.ToString(), "<script>alert('" + this.strErrMsg[index] + "');location=document.URL;</script>");
            }
            else
            {
                XmlDocument document = new XmlDocument();
                string str = base.Server.MapPath("~/template/");
                try
                {
                    document.Load(str + ((string)this.ViewState["xmlName"]));
                }
                catch (Exception)
                {
                    if (File.Exists(str + ((string)this.ViewState["xmlName"])))
                    {
                        File.Delete(str + ((string)this.ViewState["xmlName"]));
                    }
                    base.ClientScript.RegisterStartupScript(base.GetType(), "ErrFile", "<script>alert('文件格式不对！');location=document.URL;</script>");
                    return;
                }
                XmlNodeList list = document.SelectNodes("/list/folder");
                int num2 = 0;
                bool flag = true;
                FileStream output = null;
                BinaryWriter writer = null;
                try
                {
                    foreach (XmlNode node in list)
                    {
                        XmlNode node2 = node.SelectSingleNode("Path");
                        string path = str + node2.InnerText;
                        switch (num2)
                        {
                            case 0:
                                this.ViewState["rootpath"] = node2.InnerText;
                                num2++;
                                if (Directory.Exists(path))
                                {
                                    base.ClientScript.RegisterStartupScript(base.GetType(), "replace", "<script>var r=confirm('已有同名模板在模板路径下，是否覆盖此同名模板文件夹？');if(r==false) {document.getElementById('Continue').value=2;}else{ document.getElementById('Continue').value=1;}</script>");
                                    return;
                                }
                                Directory.CreateDirectory(path);
                                break;
                        }
                    }
                    foreach (XmlNode node3 in document.SelectNodes("/list/file"))
                    {
                        XmlNode node4 = node3.SelectSingleNode("Path");
                        string str3 = str + node4.InnerText;
                        XmlNode node5 = node3.SelectSingleNode("content");
                        output = new FileStream(str3, FileMode.Create, FileAccess.Write);
                        writer = new BinaryWriter(output);
                        writer.Write(Convert.FromBase64String(node5.InnerText.Substring(10)));
                        writer.Close();
                        output.Close();
                    }
                }
                catch (Exception exception)
                {
                    flag = false;
                    SystemError.CreateErrorLog("还原模板文件错误： " + exception.ToString());
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                    if (output != null)
                    {
                        output.Close();
                    }
                }
                if (File.Exists(str + ((string)this.ViewState["xmlName"])))
                {
                    File.Delete(str + ((string)this.ViewState["xmlName"]));
                }
                bool flag2 = this.setTemplateMdbData(0);
                if (flag && flag2)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "success", "<script>alert('已成功导入模板，请点击模板展示查看模板。');</script>");
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "failure", "<script>alert('导入模板错误，请重试，详细信息请查看错误日志Systemlog.log。');</script>");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Commen1.JudgeLogin(this.Page);
        }

        private bool setTemplateMdbData(int nReplace)
        {
            StringBuilder builder;
            bool flag = true;
            string path = base.Server.MapPath(@"..\template\" + ((string)this.ViewState["rootpath"]) + "/AccessData.txt");
            Encoding encoding = Encoding.GetEncoding("gb2312");
            StreamReader reader = null;
            string str2 = null;
            try
            {
                reader = new StreamReader(path, encoding);
                str2 = reader.ReadToEnd();
            }
            catch (Exception exception)
            {
                flag = false;
                SystemError.CreateErrorLog("读模版数据库错误： " + exception.ToString());
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            if ((str2 == null) || !flag)
            {
                base.Response.Write("<script language=javascript>window.alert('未能读取模版数据库文件！');</script>");
                return false;
            }
            string[] strArray = str2.Split(new char[] { ',' });
            if (nReplace == 0)
            {
                builder = new StringBuilder("insert into TemplateInfo (TemplateName,BigClassName,ThumbImag,Designer,TemplatePath,TemplateType,");
                builder.Append("price,LinkUrl,DownloadUrl,AddDate,Abstract,Detail,IsMobile) values('");
                builder.Append(strArray[0] + "','" + strArray[1] + "','" + strArray[2] + "','" + strArray[3]);
                builder.Append("','" + strArray[4] + "'," + strArray[6]);
                builder.Append("," + strArray[7] + ",'" + strArray[8] + "','" + strArray[9]);
                builder.Append("','" + strArray[10] + "','" + strArray[11] + "','" + strArray[12] + "','" + strArray[13] + "')");
            }
            else
            {
                if (Convert.ToInt32(this.Bll1.DAL1.ExecuteScalar("select count(*) from TemplateInfo  where TemplatePath='" + strArray[4] + "'")) >= 1)
                {
                    builder = new StringBuilder("Update TemplateInfo Set TemplateName='" + strArray[0] + "',BigClassName='" + strArray[1] + "',ThumbImag='" + strArray[2] + "',Designer='" + strArray[3] + "',TemplateType='" + strArray[6] + "',price='" + strArray[7] + "' LinkUrl='" + strArray[8] + "',DownloadUrl='" + strArray[8] + "',AddDate='" + strArray[10] + "',Abstract='" + strArray[11] + "',Detail='" + strArray[12] + "',IsMobile='" + strArray[13] + "' where TemplatePath='" + strArray[4] + "'");
                }
                else
                {
                    builder = new StringBuilder("insert into TemplateInfo (TemplateName,BigClassName,ThumbImag,Designer,TemplatePath,TemplateType,");
                }
                builder.Append("price,LinkUrl,DownloadUrl,AddDate,Abstract,Detail,IsMobile) values('");
                builder.Append(strArray[0] + "','" + strArray[1] + "','" + strArray[2] + "','" + strArray[3]);
                builder.Append("','" + strArray[4] + "'," + strArray[6]);
                builder.Append("," + strArray[7] + ",'" + strArray[8] + "','" + strArray[9]);
                builder.Append("','" + strArray[10] + "','" + strArray[11] + "','" + strArray[12] + "','" + strArray[13] + "')");
            }
            try
            {
                this.Bll1.DAL1.ExecuteNonQuery(builder.ToString());
            }
            catch (Exception exception2)
            {
                flag = false;
                SystemError.CreateErrorLog("设置模版数据库错误： " + exception2.ToString());
            }
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            return flag;
        }

        private int uploadFile()
        {
            HttpPostedFile postedFile = this.File1.PostedFile;
            int contentLength = postedFile.ContentLength;
            string str = this.File1.Value;
            if (str.Substring(str.LastIndexOf(".") + 1).ToUpper() != "CDQH")
            {
                return 3;
            }
            if (contentLength == 0)
            {
                return 0;
            }
            if (contentLength > 0x5ff1b80)
            {
                return 1;
            }
            this.ViewState["xmlName"] = this.strFileName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".cdqh";
            try
            {
                postedFile.SaveAs(base.Server.MapPath("/template") + @"\" + this.strFileName);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("上传文件错误： " + exception.ToString());
                return 2;
            }
            return -1;
        }
    }
}