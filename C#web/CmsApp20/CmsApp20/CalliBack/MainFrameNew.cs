using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using _BLL;

namespace CmsApp20.CalliBack
{
    public class MainFrameNew : Page
    {
        private BLL Bll1 = new BLL();
        protected HtmlGenericControl topFrame;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.topFrame.Attributes.Add("src", "topframe.aspx");
            }
        }
    }
}
