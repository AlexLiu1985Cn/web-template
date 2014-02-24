namespace CmsApp.CmsBack
{
    using _BLL;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class QH_WaterMarkSet : Page
    {
        protected HtmlTableRow A;
        protected HtmlInputCheckBox big;
        protected HtmlInputText BigImg;
        protected HtmlInputFile BigUp;
        private BLL Bll1 = new BLL();
        protected Button BtnReset;
        protected Button BtnSave;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected HtmlInputText img_x;
        protected HtmlInputText img_y;
        protected HtmlInputRadioButton pic;
        protected HtmlInputText PicTransp;
        protected HtmlInputRadioButton Pos0;
        protected HtmlInputRadioButton Pos1;
        protected HtmlInputRadioButton Pos2;
        protected HtmlInputRadioButton Pos3;
        protected HtmlInputRadioButton Pos4;
        protected HtmlInputRadioButton Pos5;
        protected HtmlInputRadioButton Pos6;
        protected HtmlInputRadioButton Pos7;
        protected HtmlInputRadioButton Pos8;
        protected string strWPic;
        protected string strWText;
        protected string strWTip;
        protected HtmlInputRadioButton Text;
        protected HtmlInputText text_angle;
        protected HtmlInputText text_bigsize;
        protected HtmlInputText text_color;
        protected HtmlInputText text_fonts;
        protected HtmlInputText text_size;
        protected HtmlInputText TextTransp;
        protected HtmlInputCheckBox thumb;
        protected HtmlInputText ThumbImg;
        protected HtmlInputFile ThumbUp;
        protected HtmlInputText WaterText;

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.DataToBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string str8;
            string str15;
            string path = this.ThumbImg.Value;
            string str2 = this.ThumbUp.Value;
            HttpPostedFile postedFile = this.ThumbUp.PostedFile;
            int contentLength = postedFile.ContentLength;
            if (contentLength != 0)
            {
                string str3 = base.Server.MapPath("../upload/");
                if (!Directory.Exists(str3))
                {
                    Directory.CreateDirectory(str3);
                }
                string str4 = str2.Substring(str2.LastIndexOf(".") + 1).ToUpper();
                switch (str4)
                {
                    case "JPG":
                    case "BMP":
                    case "GIF":
                    case "PNG":
                        {
                            string strSize = "";
                            if (contentLength > this.Bll1.GetPicSize(ref strSize))
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "2big", "<script>alert('对不起，栏目图片大小不能大于" + strSize + "');</script>");
                                return;
                            }
                            string str6 = (string)this.ViewState["ThumbImg"];
                            if (str6 != "")
                            {
                                string str7 = base.Server.MapPath(str6);
                                if (File.Exists(str7))
                                {
                                    File.Delete(str7);
                                }
                            }
                            path = "../upload/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "." + str4;
                            postedFile.SaveAs(base.Server.MapPath(path));
                            goto Label_0180;
                        }
                }
                base.Response.Write("<script>alert('对不起，请选择正确的图片！');</script>");
                return;
            }
        Label_0180:
            str8 = this.BigImg.Value;
            string str9 = this.BigUp.Value;
            HttpPostedFile file2 = this.BigUp.PostedFile;
            int num2 = file2.ContentLength;
            if (num2 != 0)
            {
                string str10 = base.Server.MapPath("../upload/");
                if (!Directory.Exists(str10))
                {
                    Directory.CreateDirectory(str10);
                }
                string str11 = str9.Substring(str9.LastIndexOf(".") + 1).ToUpper();
                switch (str11)
                {
                    case "JPG":
                    case "BMP":
                    case "GIF":
                    case "PNG":
                        {
                            string str12 = "";
                            if (num2 > this.Bll1.GetPicSize(ref str12))
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "2big", "<script>alert('对不起，栏目图片大小不能大于" + str12 + "');</script>");
                                return;
                            }
                            string str13 = (string)this.ViewState["BigImg"];
                            if (str13 != "")
                            {
                                string str14 = base.Server.MapPath(str13);
                                if (File.Exists(str14))
                                {
                                    File.Delete(str14);
                                }
                            }
                            str8 = "../upload/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "." + str11;
                            file2.SaveAs(base.Server.MapPath(str8));
                            goto Label_030C;
                        }
                }
                base.Response.Write("<script>alert('对不起，请选择正确的图片！');</script>");
                return;
            }
        Label_030C:
            str15 = "BigWater,ThumbWater,WaterClass,ThumbWtImg,BigWtImg,PicTransp,WaterText,text_size,text_bigsize,text_fonts,text_angle,text_color,TextTransp,waterPos,BigMinSize,id";
            string[] astrValue = new string[] { this.big.Checked ? "1" : "0", this.thumb.Checked ? "1" : "0", base.Request.Form["water_class"], path, str8, this.PicTransp.Value, this.WaterText.Value, this.text_size.Value, this.text_bigsize.Value, this.text_fonts.Value, this.text_angle.Value, this.text_color.Value, this.TextTransp.Value, base.Request.Form["waterPos"], this.img_x.Value.Trim() + "x" + this.img_y.Value.Trim(), "1" };
            string strUpdate = "UPDATE QH_ImgSet SET BigWater=@BigWater,ThumbWater=@ThumbWater ,WaterClass=@WaterClass,ThumbWtImg=@ThumbWtImg,BigWtImg=@BigWtImg,PicTransp=@PicTransp,WaterText=@WaterText,text_size=@text_size,text_bigsize=@text_bigsize,text_fonts=@text_fonts,text_angle=@text_angle,text_color=@text_color ,TextTransp=@TextTransp,waterPos=@waterPos,BigMinSize=@BigMinSize WHERE ID=@id";
            if (this.Bll1.UpdateTable(strUpdate, str15, astrValue) == 1)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "saveOk", "<script>alert('修改成功！');</script>");
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "saveFail", "<script>alert('修改失败！');</script>");
            }
            this.DataToBind();
        }

        private void DataToBind()
        {
            this.strWTip = "请在保存设置的情况下查看以下水印效果图";
            this.strWPic = "文字水印效果：";
            this.strWText = "图片水印效果：";
            string[] astrRet = new string[15];
            string[] astrField = new string[] { "BigWater", "ThumbWater", "WaterClass", "ThumbWtImg", "BigWtImg", "PicTransp", "WaterText", "text_size", "text_bigsize", "text_fonts", "text_angle", "text_color", "TextTransp", "waterPos", "BigMinSize" };
            string strQuery = "select BigWater,ThumbWater,WaterClass,ThumbWtImg,BigWtImg,PicTransp,WaterText,text_size,text_bigsize,text_fonts,text_angle,text_color,TextTransp,waterPos ,BigMinSize from QH_ImgSet where id=1";
            this.Bll1.ReadDataReader(strQuery, ref astrRet, astrField);
            this.big.Checked = astrRet[0] == "True";
            this.thumb.Checked = astrRet[1] == "True";
            if (astrRet[0] == "False")
            {
                this.A.Style.Add("display", "none");
            }
            string str2 = astrRet[2];
            if (str2 != null)
            {
                if (!(str2 == "1"))
                {
                    if (str2 == "2")
                    {
                        this.pic.Checked = true;
                        this.Text.Checked = false;
                    }
                }
                else
                {
                    this.Text.Checked = true;
                    this.pic.Checked = false;
                }
            }
            if ((astrRet[0] == "False") || (astrRet[1] == "False"))
            {
                this.strWTip = this.strWPic = this.strWText = "";
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), "Disp", "<script>  if(" + astrRet[2] + "==1) {$('LKT').style.display=''; $('LKP').style.display='none';if('" + astrRet[0] + "'=='True') $('BT1').style.display=''; else  $('BT1').style.display='none';if('" + astrRet[1] + "'=='True') $('BT2').style.display=''; else  $('BT2').style.display='none';} else {$('LKT').style.display='none'; $('LKP').style.display='';if('" + astrRet[0] + "'=='True') $('BP1').style.display=''; else  $('BP1').style.display='none';if('" + astrRet[1] + "'=='True') $('BP2').style.display=''; else  $('BP2').style.display='none';}</script>");
            this.ViewState["ThumbImg"] = this.ThumbImg.Value = astrRet[3];
            this.ViewState["BigImg"] = this.BigImg.Value = astrRet[4];
            this.PicTransp.Value = astrRet[5];
            this.WaterText.Value = astrRet[6];
            this.text_size.Value = astrRet[7];
            this.text_bigsize.Value = astrRet[8];
            this.text_fonts.Value = astrRet[9];
            this.text_angle.Value = astrRet[10];
            this.text_color.Value = astrRet[11];
            this.TextTransp.Value = astrRet[12];
            this.Pos0.Checked = false;
            this.Pos1.Checked = false;
            this.Pos2.Checked = false;
            this.Pos3.Checked = false;
            this.Pos4.Checked = false;
            this.Pos5.Checked = false;
            this.Pos6.Checked = false;
            this.Pos7.Checked = false;
            this.Pos8.Checked = false;
            switch (astrRet[13])
            {
                case "0":
                    this.Pos0.Checked = true;
                    break;

                case "1":
                    this.Pos1.Checked = true;
                    break;

                case "2":
                    this.Pos2.Checked = true;
                    break;

                case "3":
                    this.Pos3.Checked = true;
                    break;

                case "4":
                    this.Pos4.Checked = true;
                    break;

                case "5":
                    this.Pos5.Checked = true;
                    break;

                case "6":
                    this.Pos6.Checked = true;
                    break;

                case "7":
                    this.Pos7.Checked = true;
                    break;

                case "8":
                    this.Pos8.Checked = true;
                    break;
            }
            if (astrRet[14].Trim() != "")
            {
                string[] strArray3 = astrRet[14].Split(new char[] { 'x' });
                this.img_x.Value = strArray3[0];
                this.img_y.Value = strArray3[1];
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