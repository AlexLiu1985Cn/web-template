namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using ThoughtWorks.QRCode.Codec;
    using ThoughtWorks.QRCode.Codec.Data;
    using _commen;

    public class CreateQRCode : Page
    {
        private BLL Bll1 = new BLL();
        protected Button BtnCreate;
        protected Button BtnDecode;
        protected Button BtnDown;
        protected Button BtnUpLogo;
        protected HtmlForm form1;
        protected System.Web.UI.WebControls.Image ImageLogo;
        protected System.Web.UI.WebControls.Image ImageQRCode;
        protected HtmlInputFile myFile;
        protected HtmlInputText QRCodeData;
        protected HtmlInputText QRCodeSize;
        protected string strLogoUrl = "";
        protected string strQRCodeData = "";
        protected string strSpace;

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            switch ("Byte")
            {
                case "Byte":
                    encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;

                case "AlphaNumeric":
                    encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                    break;

                case "Numeric":
                    encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                    break;
            }
            try
            {
                string input = this.QRCodeSize.Value;
                if (!Regex.IsMatch(input, @"^\d+$"))
                {
                    input = "4";
                }
                int num = Convert.ToInt16(input);
                encoder.QRCodeScale = num;
            }
            catch (Exception exception)
            {
                base.Response.Write("Invalid size!" + exception.ToString());
                return;
            }
            try
            {
                int num2 = Convert.ToInt16("5");
                encoder.QRCodeVersion = num2;
            }
            catch (Exception exception2)
            {
                base.Response.Write("Invalid version !" + exception2.ToString());
            }
            string str3 = "H";
            if (str3 == "L")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            }
            else if (str3 == "M")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            }
            else if (str3 == "Q")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            }
            else if (str3 == "H")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            }
            string content = base.Request.Form["QRCodeData"].Trim();
            if (!(content == ""))
            {
                System.Drawing.Image image = encoder.Encode(content);
                string path = base.Request.Form["LogoUrl"].Trim();
                string str6 = base.Server.MapPath(path);
                Graphics graphics = null;
                Graphics graphics2 = null;
                Graphics graphics3 = null;
                System.Drawing.Image image2 = null;
                System.Drawing.Image image3 = System.Drawing.Image.FromFile(base.Server.MapPath("images/QR1.png"));
                if ((path != "") && File.Exists(str6))
                {
                    graphics = Graphics.FromImage(image);
                    image2 = (Bitmap)System.Drawing.Image.FromFile(str6);
                    int width = image.Width / 4;
                    int height = image.Height / 4;
                    int x = (image.Width / 2) - (width / 2);
                    int y = (image.Height / 2) - (height / 2);
                    int num7 = width / 0x10;
                    int num8 = height / 0x10;
                    graphics.DrawImage(image3, new Rectangle(x, y, width, height), 0, 0, image3.Width, image3.Height, GraphicsUnit.Pixel);
                    int num9 = (width - num7) - num7;
                    int num10 = (height - num8) - num8;
                    System.Drawing.Image image4 = new Bitmap(num9, num10);
                    graphics2 = Graphics.FromImage(image4);
                    graphics2.InterpolationMode = InterpolationMode.High;
                    graphics2.SmoothingMode = SmoothingMode.HighQuality;
                    graphics2.Clear(Color.Transparent);
                    graphics2.DrawImage(image2, new Rectangle(0, 0, num9, num10), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel);
                    graphics2.DrawRectangle(new Pen(ColorTranslator.FromHtml("#cbcbcb"), 1f), 0, 0, image4.Width - 1, image4.Height - 1);
                    this.Chamfer(graphics2, 0, image4.Width, image4.Height);
                    this.Chamfer(graphics2, 1, image4.Width, image4.Height);
                    this.Chamfer(graphics2, 2, image4.Width, image4.Height);
                    this.Chamfer(graphics2, 3, image4.Width, image4.Height);
                    graphics.DrawImage(image4, new Rectangle(x + num7, y + num8, (width - num7) - num7, (height - num8) - num8), 0, 0, image4.Width, image4.Height, GraphicsUnit.Pixel);
                    image4.Dispose();
                }
                try
                {
                    image.Save(base.Server.MapPath("../upload/TempQrCode.JPG"), ImageFormat.Jpeg);
                }
                catch (Exception exception3)
                {
                    base.Response.Write(exception3.ToString());
                }
                finally
                {
                    image.Dispose();
                    image3.Dispose();
                    if (image2 != null)
                    {
                        image2.Dispose();
                    }
                    if (graphics != null)
                    {
                        graphics.Dispose();
                    }
                    if (graphics2 != null)
                    {
                        graphics2.Dispose();
                    }
                    if (graphics3 != null)
                    {
                        graphics3.Dispose();
                    }
                }
                this.ImageQRCode.ImageUrl = "../upload/TempQrCode.jpg?" + new Random().Next();
                this.ImageQRCode.Visible = true;
                this.GetSiteInfoContents("ISP");
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "Empty", "alert(\"二维码内容不能为空！\");", true);
            }
        }

        protected void BtnDecode_Click(object sender, EventArgs e)
        {
            this.GetSiteInfoContents("ISP");
            if (!this.ImageQRCode.Visible)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "NoCreate", "alert(\"请先生成二维码！\");", true);
            }
            else
            {
                string str = this.ImageQRCode.ImageUrl.Trim();
                string path = str.Substring(0, str.IndexOf('?'));
                path = base.Server.MapPath(path);
                if ((str == "") || !File.Exists(path))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "NoCreate", "alert(\"请先生成二维码！\");", true);
                }
                else
                {
                    Bitmap image = null;
                    image = (Bitmap)System.Drawing.Image.FromFile(path);
                    string str3 = "";
                    try
                    {
                        str3 = new QRCodeDecoder().decode(new QRCodeBitmapImage(image));
                    }
                    catch (Exception exception)
                    {
                        base.Response.Write(exception.Message + path);
                    }
                    finally
                    {
                        image.Dispose();
                    }
                    base.ClientScript.RegisterStartupScript(base.GetType(), "Decode", "alert(\"二维码内容为：" + str3 + "\");", true);
                }
            }
        }

        protected void BtnDown_Click(object sender, EventArgs e)
        {
            this.GetSiteInfoContents("ISP");
            if (!this.ImageQRCode.Visible)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "NoCreate", "alert(\"请先生成二维码！\");", true);
            }
            else
            {
                string str = this.ImageQRCode.ImageUrl.Trim();
                string path = str.Substring(0, str.IndexOf('?'));
                path = base.Server.MapPath(path);
                if ((str == "") || !File.Exists(path))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "NoCreate", "alert(\"请先生成二维码！\");", true);
                }
                else
                {
                    FileInfo info = new FileInfo(path);
                    base.Response.Clear();
                    base.Response.ClearHeaders();
                    base.Response.Buffer = false;
                    base.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
                    base.Response.AddHeader("Content-Disposition", "attachment; filename=QrCode.jpg");
                    base.Response.ContentType = "appliction/octet-stream";
                    base.Response.TransmitFile(info.FullName);
                    base.Response.Flush();
                }
            }
        }

        protected void BtnUpLogo_Click(object sender, EventArgs e)
        {
            string path = base.Request.Form["LogoUrl"].Trim();
            string str2 = this.myFile.Value;
            HttpPostedFile postedFile = this.myFile.PostedFile;
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
                                this.GetSiteInfoContents("ISP");
                                return;
                            }
                            string str6 = (string)this.ViewState["LogoUrl"];
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
                            goto Label_01A5;
                        }
                }
                base.Response.Write("<script>alert('对不起，请选择正确的图片！');</script>");
                this.GetSiteInfoContents("ISP");
                return;
            }
        Label_01A5:
            try
            {
                this.Bll1.DAL1.ExecuteNonQuery("UPDATE QH_SiteInfo SET QRCodeLogo='" + path + "' WHERE ID=" + ((string)this.ViewState["id"]));
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("修改手机网站二维码Logo错误：" + exception.ToString());
            }
            this.GetSiteInfoContents("ISP");
        }

        private void Chamfer(Graphics g1, int nAngNum, int nWidth, int nHeight)
        {
            Point[][] pointArray2 = new Point[4][];
            pointArray2[0] = new Point[] { new Point(6, 0), new Point(0, 0), new Point(0, 6) };
            pointArray2[1] = new Point[] { new Point(nWidth - 6, 0), new Point(nWidth, 0), new Point(nWidth, 6) };
            pointArray2[2] = new Point[] { new Point(0, nHeight - 6), new Point(0, nHeight), new Point(6, nHeight) };
            pointArray2[3] = new Point[] { new Point(nWidth, nHeight - 6), new Point(nWidth, nHeight), new Point(nWidth - 6, nHeight) };
            Point[][] pointArray = pointArray2;
            int[][] numArray2 = new int[4][];
            numArray2[0] = new int[] { 0, 0, 10, 10, 180, 90 };
            int[] numArray3 = new int[] { 0, 0, 10, 10, 0, -90 };
            numArray3[0] = nWidth - 11;
            numArray2[1] = numArray3;
            int[] numArray4 = new int[] { 0, 0, 10, 10, 180, -90 };
            numArray4[1] = nHeight - 11;
            numArray2[2] = numArray4;
            int[] numArray5 = new int[] { 0, 0, 10, 10, 0, 90 };
            numArray5[0] = nWidth - 11;
            numArray5[1] = nHeight - 11;
            numArray2[3] = numArray5;
            int[][] numArray = numArray2;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(numArray[nAngNum][0], numArray[nAngNum][1], numArray[nAngNum][2], numArray[nAngNum][3], (float)numArray[nAngNum][4], (float)numArray[nAngNum][5]);
            path.AddCurve(pointArray[nAngNum], 3f);
            path.CloseFigure();
            Region region = new Region(path);
            g1.FillRegion(Brushes.White, region);
            path = new GraphicsPath();
            path.AddArc(numArray[nAngNum][0], numArray[nAngNum][1], numArray[nAngNum][2], numArray[nAngNum][3], (float)numArray[nAngNum][4], (float)numArray[nAngNum][5]);
            g1.DrawPath(new Pen(ColorTranslator.FromHtml("#cbcbcb"), 1f), path);
        }

        private void GetSiteInfoContents(string strIsPostBack)
        {
            string[] astrRet = new string[3];
            string strQuery = "select top 1 id,SitePath,QRCodeLogo from QH_SiteInfo ";
            this.Bll1.ReadDataReader(strQuery, ref astrRet, 3);
            this.ViewState["id"] = astrRet[0];
            this.ViewState["LogoUrl"] = this.strLogoUrl = astrRet[2].Trim();
            if (strIsPostBack == "NOISP")
            {
                this.QRCodeData.Value = astrRet[1];
            }
            if (this.strLogoUrl == "")
            {
                this.strSpace = "";
                this.ImageLogo.Visible = false;
            }
            else
            {
                this.strSpace = "&nbsp;&nbsp;";
                this.ImageLogo.ImageUrl = this.strLogoUrl;
                this.ImageLogo.Visible = true;
            }
            if (this.ImageQRCode.ImageUrl == "")
            {
                this.ImageQRCode.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !base.IsPostBack)
            {
                this.GetSiteInfoContents("NOISP");
            }
        }
    }
}