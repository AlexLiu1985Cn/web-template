namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using _commen;

    public class QH_FromEmailSet : Page
    {
        private string[] astrRet = new string[3];
        private BLL Bll1 = new BLL();
        protected Button BtnReset;
        protected Button BtnSave;
        protected HtmlForm form1;
        protected HtmlInputText fromEmail;
        protected HtmlInputPassword fromPsw;
        protected HtmlInputText fromSMTP;
        protected HtmlHead Head1;
        protected LinkButton LBtn1;

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.DataToBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string str;
                string[] strArray;
                string str2;
                string strPassword = base.Request.Form["fromPsw"].Trim();
                if (strPassword == "")
                {
                    str = "fromEmail,fromSMTP,id";
                    strArray = new string[] { this.fromEmail.Value.Trim(), this.fromSMTP.Value.Trim(), "1" };
                    str2 = "UPDATE QH_SiteInfo SET fromEmail=@fromEmail,fromSMTP=@fromSMTP  WHERE ID=@id";
                }
                else
                {
                    strPassword = this.Bll1.EncodePassword(strPassword);
                    str = "fromEmail,fromSMTP,fromPsw,id";
                    strArray = new string[] { this.fromEmail.Value.Trim(), this.fromSMTP.Value.Trim(), strPassword, "1" };
                    str2 = "UPDATE QH_SiteInfo SET fromEmail=@fromEmail,fromSMTP=@fromSMTP ,fromPsw=@fromPsw WHERE ID=@id";
                }
                if (this.Bll1.UpdateTable(str2, str, strArray) == 1)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "UpdateOk", "<script>alert('修改成功！');</script>");
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "UpdateFail", "<script>alert('修改失败！');</script>");
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("修改系统邮箱配置记录错误：" + exception.ToString());
                base.Response.Write("<script>alert('修改系统邮箱配置设置错误！');</script>");
            }
            this.DataToBind();
        }

        private void DataToBind()
        {
            string strQuery = "select fromEmail,fromSMTP,fromPsw from QH_SiteInfo where id=1";
            this.Bll1.ReadDataReader(strQuery, ref this.astrRet, 3);
            this.fromEmail.Value = this.astrRet[0];
            this.fromSMTP.Value = this.astrRet[1];
        }

        protected void LBtn1_Click(object sender, EventArgs e)
        {
            string[] astrRet = new string[4];
            string strQuery = "select Email,fromEmail,fromSMTP,fromPsw from QH_SiteInfo ";
            this.Bll1.ReadDataReader(strQuery, ref astrRet, 4);
            if (astrRet[0] == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "ToEmpty", "<script>alert('接受邮箱为空，请在<系统设置>－<网站常用信息>中设置接受邮箱地址！');</script>");
            }
            else if (astrRet[1] == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "fromEmpty", "<script>alert('发送邮箱为空，请设置发送邮箱地址并保存！');</script>");
            }
            else if (astrRet[2] == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "ToEmpty", "<script>alert('邮件SMTP服务器为空，请设置邮件SMTP服务器并保存！可上网搜索发送邮箱SMTP服务器地址。');</script>");
            }
            else
            {
                bool flag = true;
                try
                {
                    MailAddress from = new MailAddress(astrRet[1]);
                    MailAddress to = new MailAddress(astrRet[0]);
                    MailMessage message = new MailMessage(from, to)
                    {
                        Subject = "邮件发送测试",
                        IsBodyHtml = true,
                        Body = "测试时间：" + DateTime.Now.ToString()
                    };
                    new SmtpClient(astrRet[2]) { Credentials = new NetworkCredential(astrRet[1], this.Bll1.DecodePassword(astrRet[3])) }.Send(message);
                }
                catch (SmtpException exception)
                {
                    flag = false;
                    if (exception.StatusCode == SmtpStatusCode.MailboxUnavailable)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Err1", "<script>alert('邮箱不可用。请检查接受邮箱和发送邮箱是否正确。');</script>");
                    }
                    else if (exception.StatusCode == SmtpStatusCode.MailboxNameNotAllowed)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Err1", "<script>alert('指定邮箱的语法不正确。请检查发送邮箱密码是否正确。');</script>");
                    }
                    else if (exception.StatusCode == SmtpStatusCode.GeneralFailure)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Err1", "<script>alert('发送邮箱SMTP服务器地址不正确。');</script>");
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Err1", "<script>alert('邮件发送错误。" + exception.StatusCode + "');</script>");
                    }
                }
                catch (Exception exception2)
                {
                    flag = false;
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Err", "<script>alert('发送邮件错误。" + exception2.Message + "');</script>");
                }
                if (flag)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "OK", "<script>alert('邮件已发送。');</script>");
                }
                this.DataToBind();
            }
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
