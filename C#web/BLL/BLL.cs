using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _DAL.OleDBHelper;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using _commen;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


namespace _BLL
{
    public class BLL
    {
        public DAL DAL1;

        private string _Coding;
        private string _EngineName;
        private string[][] _Enginers;
        private string _Regex;
        private string _RegexWord;
        private string[] astrRet;
        private PixelFormat[] indexedPixelFormats;
        private int nMheight;
        private int nMwidth;
        private Page Page1;
        private string strDir1;
        private string strModule1;
        private string strWPlus;
        private string strWPluseEncode;

        public BLL()
        {
            this.DAL1 = new DAL();

            this.Page1 = new Page();
            this.astrRet = new string[0x16];
            PixelFormat[] formatArray = new PixelFormat[6];
            formatArray[2] = PixelFormat.Format16bppArgb1555;
            formatArray[3] = PixelFormat.Format1bppIndexed;
            formatArray[4] = PixelFormat.Format4bppIndexed;
            formatArray[5] = PixelFormat.Format8bppIndexed;
            this.indexedPixelFormats = formatArray;
            this.strWPlus = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
            this.strWPluseEncode = "_1234567890KLMNOPQRSTUVWXYZABCDEFGHIJklmnopqrstuvwxyzabcdefghij";
            this._Enginers = new string[][] { new string[] { "google", "utf8", "q" }, new string[] { "baidu", "utf8", "wd" }, new string[] { "yahoo", "utf8", "p" }, new string[] { "bing", "utf8", "q" }, new string[] { "youdao", "utf8", "q" }, new string[] { "iask", "gb2312", "title" }, new string[] { "soso", "gb2312", "w" }, new string[] { "sogou", "gb2312", "query" }, new string[] { "zhongsou", "gb2312", "w" }, new string[] { "lycos", "utf8", "q" }, new string[] { "exactseek", "utf8", "q" }, new string[] { "so", "utf8", "q" } };
            this._EngineName = "";
            this._Coding = "utf8?";
            this._RegexWord = "";
            this._Regex = "(";         
        }

        public BLL(int nDepth)
        {
            this.Page1 = new Page();
            this.astrRet = new string[0x16];
            PixelFormat[] formatArray = new PixelFormat[6];
            formatArray[2] = PixelFormat.Format16bppArgb1555;
            formatArray[3] = PixelFormat.Format1bppIndexed;
            formatArray[4] = PixelFormat.Format4bppIndexed;
            formatArray[5] = PixelFormat.Format8bppIndexed;
            this.indexedPixelFormats = formatArray;
            this.strWPlus = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
            this.strWPluseEncode = "_1234567890KLMNOPQRSTUVWXYZABCDEFGHIJklmnopqrstuvwxyzabcdefghij";
            this._Enginers = new string[][] { new string[] { "google", "utf8", "q" }, new string[] { "baidu", "utf8", "wd" }, new string[] { "yahoo", "utf8", "p" }, new string[] { "bing", "utf8", "q" }, new string[] { "youdao", "utf8", "q" }, new string[] { "iask", "gb2312", "title" }, new string[] { "soso", "gb2312", "w" }, new string[] { "sogou", "gb2312", "query" }, new string[] { "zhongsou", "gb2312", "w" }, new string[] { "lycos", "utf8", "q" }, new string[] { "exactseek", "utf8", "q" }, new string[] { "so", "utf8", "q" } };
            this._EngineName = "";
            this._Coding = "utf8?";
            this._RegexWord = "";
            this._Regex = "(";
            this.DAL1 = new DAL(nDepth);
        }

        protected void AddWater(string Path, string Path_sy, bool bBig)
        {
            int num = (int)((float.Parse(this.astrRet[0x13]) / 100f) * 255f);
            CultureInfo provider = new CultureInfo("en-us");
            string newValue = num.ToString("X", provider);
            int argb = int.Parse(this.astrRet[0x12].Replace("#", newValue), NumberStyles.HexNumber);
            float dx = 0f;
            float dy = 0f;
            int num5 = bBig ? int.Parse(this.astrRet[15]) : int.Parse(this.astrRet[14]);
            num5 = (num5 < 1) ? 1 : num5;
            int num6 = int.Parse(this.astrRet[0x11]);
            Image image = Image.FromFile(Path);
            Graphics graphics = null;
            Bitmap bitmap = null;
            bool flag = true;
            if (this.IsPixelFormatIndexed(image.PixelFormat))
            {
                bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
                flag = false;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
            }
            else
            {
                graphics = Graphics.FromImage(image);
            }
            float num7 = (image.Width * 1f) / 3f;
            float num8 = (image.Height * 1f) / 3f;
            int num9 = int.Parse(this.astrRet[20]);
            int num10 = num9 % 3;
            float num11 = num9 / 3;
            Font font = null;
            PrivateFontCollection fonts = null;
            string path = (this.strDir1 == "0") ? ("../" + this.astrRet[0x10].Trim()) : this.astrRet[0x10].Trim();
            if (path == "")
            {
                font = new Font("宋体", (float)num5, GraphicsUnit.Pixel);
            }
            else if (!System.IO.File.Exists(this.Page1.Server.MapPath(path)))
            {
                font = new Font("宋体", (float)num5, GraphicsUnit.Pixel);
            }
            else
            {
                fonts = new PrivateFontCollection();
                fonts.AddFontFile(this.Page1.Server.MapPath(path));
                font = new Font(fonts.Families[0], (float)num5, GraphicsUnit.Pixel);
            }
            SizeF ef = new SizeF();
            string text = this.astrRet[13].Trim();
            ef = graphics.MeasureString(text, font);
            double num12 = 0.017453292519943295;
            float num13 = 0f;
            float num14 = 0f;
            num13 = ef.Width * ((float)Math.Abs(Math.Cos(num12 * num6)));
            num14 = ef.Width * ((float)Math.Abs(Math.Sin(num12 * num6)));
            float num15 = 0f;
            float num16 = 0f;
            num15 = num5 * ((float)Math.Abs(Math.Sin(num12 * num6)));
            num16 = num5 * ((float)Math.Abs(Math.Cos(num12 * num6)));
            float num17 = num13 + num15;
            float num18 = num14 + num16;
            if (((num6 >= 0) && (num6 < 90)) || ((num6 < -270) && (num6 >= -360)))
            {
                if (num17 > image.Width)
                {
                    dx = 0f;
                }
                else if (num7 < num17)
                {
                    dx = (((image.Width - num17) / 2f) * num10) + num15;
                }
                else
                {
                    dx = ((num10 * num7) + ((num7 - num17) / 2f)) + num15;
                }
                if (num18 > image.Height)
                {
                    dy = 0f;
                }
                else if (num8 < num18)
                {
                    dy = ((image.Height - num18) / 2f) * num11;
                }
                else
                {
                    dy = (num11 * num8) + ((num8 - num18) / 2f);
                }
            }
            else if (((num6 >= 90) && (num6 < 180)) || ((num6 < -180) && (num6 >= -270)))
            {
                if (num17 > image.Width)
                {
                    dx = image.Width;
                }
                else if (num7 < num17)
                {
                    dx = ((((image.Width - num17) / 2f) * num10) + num15) + num13;
                }
                else
                {
                    dx = (((num10 * num7) + ((num7 - num17) / 2f)) + num15) + num13;
                }
                if (num18 > image.Height)
                {
                    dy = num16;
                }
                else if (num8 < num18)
                {
                    dy = (((image.Height - num18) / 2f) * num11) + num16;
                }
                else
                {
                    dy = ((num11 * num8) + ((num8 - num18) / 2f)) + num16;
                }
            }
            else if (((num6 >= 180) && (num6 < 270)) || ((num6 < -90) && (num6 >= -180)))
            {
                if (num17 > image.Width)
                {
                    dx = image.Width;
                }
                else if (num7 < num17)
                {
                    dx = (((image.Width - num17) / 2f) * num10) + num13;
                }
                else
                {
                    dx = ((num10 * num7) + ((num7 - num17) / 2f)) + num13;
                }
                if (num18 > image.Height)
                {
                    dy = image.Height - (4f * num16);
                }
                else if (num8 < num18)
                {
                    dy = ((((image.Height - num18) / 2f) * num11) + num14) + (num16 / 2f);
                }
                else
                {
                    dy = (((num11 * num8) + ((num8 - num18) / 2f)) + num14) + (num16 / 2f);
                }
            }
            if (((num6 >= 270) && (num6 <= 360)) || ((num6 < 0) && (num6 >= -90)))
            {
                if (num17 > image.Width)
                {
                    dx = 0f;
                }
                else if (num7 < num17)
                {
                    dx = (((image.Width - num17) / 2f) * num10) + num15;
                }
                else
                {
                    dx = (num10 * num7) + ((num7 - num17) / 2f);
                }
                if (num18 > image.Height)
                {
                    dy = image.Height - num15;
                }
                else if (num8 < num18)
                {
                    dy = ((((image.Height - num18) / 2f) * num11) + num14) - (num16 / 2f);
                }
                else
                {
                    dy = (((num11 * num8) + ((num8 - num18) / 2f)) + num14) - (num16 / 2f);
                }
            }
            SolidBrush brush = new SolidBrush(Color.FromArgb(argb));
            graphics.TranslateTransform(dx, dy);
            graphics.RotateTransform((float)num6);
            graphics.DrawString(text, font, brush, (float)0f, (float)0f);
            graphics.ResetTransform();
            try
            {
                string str6;
                string str4 = System.IO.Path.GetExtension(Path_sy).ToLower();
                if (!flag)
                {
                    goto Label_06EB;
                }
                string str5 = str4;
                if (str5 == null)
                {
                    goto Label_06DC;
                }
                if (!(str5 == ".gif") && !(str5 == ".jpg"))
                {
                    if (str5 == ".bmp")
                    {
                        goto Label_06B8;
                    }
                    if (str5 == ".png")
                    {
                        goto Label_06CA;
                    }
                    goto Label_06DC;
                }
                image.Save(Path_sy, ImageFormat.Jpeg);
                goto Label_07B7;
            Label_06B8:
                image.Save(Path_sy, ImageFormat.Bmp);
                goto Label_07B7;
            Label_06CA:
                image.Save(Path_sy, ImageFormat.Png);
                goto Label_07B7;
            Label_06DC:
                image.Save(Path_sy, ImageFormat.Jpeg);
                goto Label_07B7;
            Label_06EB:
                if ((str6 = str4) == null)
                {
                    goto Label_0759;
                }
                if (!(str6 == ".gif") && !(str6 == ".jpg"))
                {
                    if (str6 == ".bmp")
                    {
                        goto Label_073B;
                    }
                    if (str6 == ".png")
                    {
                        goto Label_074A;
                    }
                    goto Label_0759;
                }
                bitmap.Save(Path_sy, ImageFormat.Jpeg);
                goto Label_07B7;
            Label_073B:
                bitmap.Save(Path_sy, ImageFormat.Bmp);
                goto Label_07B7;
            Label_074A:
                bitmap.Save(Path_sy, ImageFormat.Png);
                goto Label_07B7;
            Label_0759:
                bitmap.Save(Path_sy, ImageFormat.Jpeg);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("添加文字水印错误：" + exception.ToString());
            }
            finally
            {
                font.Dispose();
                if (fonts != null)
                {
                    fonts.Dispose();
                }
                brush.Dispose();
                image.Dispose();
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                graphics.Dispose();
            }
        Label_07B7:
            System.IO.File.Delete(Path);
        }

        public void AddWaterMark(string originalImagePath, string strC_Img, string strDir, bool bBig)
        {
            string str;
            this.DAL1.ReadDataReader("select * from QH_ImgSet where id=1", ref this.astrRet, 0x16);
            this.Page1.Server.MapPath(strC_Img);
            bool flag = this.astrRet[7].Trim() == "True";
            if (bBig && (this.astrRet[0x15] != ""))
            {
                string[] strArray = this.astrRet[0x15].Split(new char[] { 'x' });
                int num = (strArray[0] == "") ? 0 : int.Parse(strArray[0]);
                int num2 = (strArray[1] == "") ? 0 : int.Parse(strArray[1]);
                Image image = Image.FromFile(originalImagePath);
                bool flag2 = ((num != 0) && (image.Width < num)) || ((num2 != 0) && (image.Height < num2));
                image.Dispose();
                if (flag2)
                {
                    System.IO.File.Move(originalImagePath, this.Page1.Server.MapPath(strC_Img));
                    return;
                }
            }
            if (bBig)
            {
                str = (strDir == "0") ? ("../" + this.astrRet[11].Trim()) : this.astrRet[11].Trim();
            }
            else
            {
                str = (strDir == "0") ? ("../" + this.astrRet[10].Trim()) : this.astrRet[10].Trim();
            }
            if (flag)
            {
                if (this.astrRet[9].Trim() == "1")
                {
                    if (this.astrRet[13].Trim() == "")
                    {
                        flag = false;
                    }
                    else
                    {
                        this.AddWater(originalImagePath, this.Page1.Server.MapPath(strC_Img), bBig);
                    }
                }
                else if (!System.IO.File.Exists(this.Page1.Server.MapPath(str)))
                {
                    flag = false;
                }
                else
                {
                    this.AddWaterPic(originalImagePath, this.Page1.Server.MapPath(strC_Img), this.Page1.Server.MapPath(str));
                }
            }
            if (flag)
            {
                System.IO.File.Delete(originalImagePath);
            }
            else
            {
                System.IO.File.Move(originalImagePath, this.Page1.Server.MapPath(strC_Img));
            }
        }

        protected void AddWaterPic(string Path, string Path_syp, string Path_sypf)
        {
            int x = 0;
            int y = 0;
            float num3 = float.Parse(this.astrRet[12].Trim()) / 100f;
            Image image = Image.FromFile(Path);
            Graphics graphics = null;
            Bitmap bitmap = null;
            bool flag = true;
            if (this.IsPixelFormatIndexed(image.PixelFormat))
            {
                bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
                flag = false;
                graphics = Graphics.FromImage(bitmap);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.DrawImage(image, 0, 0);
            }
            else
            {
                graphics = Graphics.FromImage(image);
            }
            Image image2 = Image.FromFile(Path_sypf);
            float num4 = (image.Width * 1f) / 3f;
            float num5 = (image.Height * 1f) / 3f;
            float num6 = image2.Width * 1f;
            float num7 = image2.Height * 1f;
            int num8 = int.Parse(this.astrRet[20].Trim());
            int num9 = num8 % 3;
            float num10 = num8 / 3;
            if (num6 > image.Width)
            {
                x = 0;
            }
            else
            {
                x = (num4 < num6) ? ((int)(((image.Width - num6) / 2f) * num9)) : ((int)((num9 * num4) + ((num4 - num6) / 2f)));
            }
            if (num7 > image.Height)
            {
                y = 0;
            }
            else
            {
                y = (num5 < num7) ? ((int)(((image.Height - num7) / 2f) * num10)) : ((int)((num10 * num5) + ((num5 - num7) / 2f)));
            }
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = num3;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
            graphics.DrawImage(image2, new Rectangle(x, y, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, imageAttr);
            try
            {
                string str3;
                string str = System.IO.Path.GetExtension(Path_syp).ToLower();
                if (!flag)
                {
                    goto Label_02D9;
                }
                string str2 = str;
                if (str2 == null)
                {
                    goto Label_02CB;
                }
                if (!(str2 == ".gif") && !(str2 == ".jpg"))
                {
                    if (str2 == ".bmp")
                    {
                        goto Label_02A9;
                    }
                    if (str2 == ".png")
                    {
                        goto Label_02BA;
                    }
                    goto Label_02CB;
                }
                image.Save(Path_syp, ImageFormat.Jpeg);
                goto Label_0392;
            Label_02A9:
                image.Save(Path_syp, ImageFormat.Bmp);
                goto Label_0392;
            Label_02BA:
                image.Save(Path_syp, ImageFormat.Png);
                goto Label_0392;
            Label_02CB:
                image.Save(Path_syp, ImageFormat.Jpeg);
                goto Label_0392;
            Label_02D9:
                if ((str3 = str) == null)
                {
                    goto Label_0347;
                }
                if (!(str3 == ".gif") && !(str3 == ".jpg"))
                {
                    if (str3 == ".bmp")
                    {
                        goto Label_0329;
                    }
                    if (str3 == ".png")
                    {
                        goto Label_0338;
                    }
                    goto Label_0347;
                }
                bitmap.Save(Path_syp, ImageFormat.Jpeg);
                goto Label_0392;
            Label_0329:
                bitmap.Save(Path_syp, ImageFormat.Bmp);
                goto Label_0392;
            Label_0338:
                bitmap.Save(Path_syp, ImageFormat.Png);
                goto Label_0392;
            Label_0347:
                bitmap.Save(Path_syp, ImageFormat.Jpeg);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("添加图片水印错误：" + exception.ToString());
            }
            finally
            {
                image.Dispose();
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                image2.Dispose();
                graphics.Dispose();
            }
        Label_0392:
            System.IO.File.Delete(Path);
        }

        public bool BatchInsert(string strTableSchema, string strInsert, string[] strCommen, string[] strCmnValue, string[] astrField, List<List<string>> listData, string[] astrFieldAll)
        {
            return this.DAL1.BatchInsert(strTableSchema, strInsert, strCommen, strCmnValue, astrField, listData, astrFieldAll);
        }

        public int BatchUpdate(string strSelect, string strUpdate, string[] astrField, List<List<string>> listData)
        {
            try
            {
                return this.DAL1.AdpaterUpdate(strSelect, strUpdate, astrField, listData);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("批量修改数据库错误： " + exception.ToString());
            }
            return 0;
        }

        public string Date6Decode(string strEncDate6)
        {
            if (!Regex.IsMatch(strEncDate6, "^[a0c6812f3d]{6}$"))
            {
                return "cbcbcb";
            }
            string str = "";
            char[] chArray = new char[] { 'a', '0', 'c', '6', '8', '1', '2', 'f', '3', 'd' };
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (strEncDate6[i] == chArray[j])
                    {
                        str = str + j.ToString();
                        break;
                    }
                }
            }
            return str;
        }

        public string Date6Decode2(string strEncDate6)
        {
            if (!Regex.IsMatch(strEncDate6, "^[abcdeghfik1]{8}$"))
            {
                return "eaderText";
            }
            string str = "";
            char[] chArray = new char[] { 'a', 'b', 'c', 'd', 'e', 'g', 'h', 'f', 'i', 'k' };
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (strEncDate6[i] == chArray[j])
                    {
                        str = str + j.ToString();
                        break;
                    }
                }
            }
            return str;
        }

        public string Date6Encode(string strDate6)
        {
            if (!Regex.IsMatch(strDate6, @"^\d{6}$"))
            {
                return "cbcbcb";
            }
            string str = "";
            char[] chArray = new char[] { 'a', '0', 'c', '6', '8', '1', '2', 'f', '3', 'd' };
            for (int i = 0; i < 6; i++)
            {
                char ch = strDate6[i];
                str = str + chArray[int.Parse(ch.ToString())];
            }
            return str;
        }

        public string Date6Encode2(string strDate6)
        {
            if (!Regex.IsMatch(strDate6, @"^\d{6}$"))
            {
                return "eaderText";
            }
            string str = "";
            char[] chArray = new char[] { 'a', 'b', 'c', 'd', 'e', 'g', 'h', 'f', 'i', 'k' };
            for (int i = 0; i < 6; i++)
            {
                char ch = strDate6[i];
                str = str + chArray[int.Parse(ch.ToString())];
            }
            return (str + "11");
        }

        public string DateDay10Decode(string strEncDateDay10)
        {
            if (!Regex.IsMatch(strEncDateDay10, "^[A9B8C7D6F4]{10}$"))
            {
                return "0101010001";
            }
            string str = "";
            char[] chArray = new char[] { 'A', '9', 'B', '8', 'C', '7', 'D', '6', 'F', '4' };
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (strEncDateDay10[i] == chArray[j])
                    {
                        str = str + j.ToString();
                        break;
                    }
                }
            }
            return str;
        }

        public string DateDay10Encode(string strDateDay10)
        {
            if (!Regex.IsMatch(strDateDay10, @"^\d{10}$"))
            {
                return "AAAAAAAAAA";
            }
            string str = "";
            char[] chArray = new char[] { 'A', '9', 'B', '8', 'C', '7', 'D', '6', 'F', '4' };
            for (int i = 0; i < 10; i++)
            {
                char ch = strDateDay10[i];
                str = str + chArray[int.Parse(ch.ToString())];
            }
            return str;
        }

        public string DecodeAuthorCode(string strEnCode)
        {
            char[] chArray = new char[0x40];
            for (int i = 0; i < 6; i++)
            {
                int index = i * 10;
                chArray[index + 9] = strEnCode[index];
                chArray[index + 2] = strEnCode[index + 1];
                chArray[index + 4] = strEnCode[index + 2];
                chArray[index + 6] = strEnCode[index + 3];
                chArray[index + 5] = strEnCode[index + 4];
                chArray[index + 7] = strEnCode[index + 5];
                chArray[index + 3] = strEnCode[index + 6];
                chArray[index + 8] = strEnCode[index + 7];
                chArray[index] = strEnCode[index + 8];
                chArray[index + 1] = strEnCode[index + 9];
            }
            chArray[0x3d] = strEnCode[60];
            chArray[60] = strEnCode[0x3d];
            chArray[0x3e] = strEnCode[0x3e];
            chArray[0x3f] = strEnCode[0x3f];
            return new string(chArray);
        }

        public string DecodeAuthorCodeTry(string strEnCode)
        {
            char[] chArray = new char[0x40];
            for (int i = 0; i < 6; i++)
            {
                int index = i * 10;
                chArray[index + 2] = strEnCode[index];
                chArray[index + 6] = strEnCode[index + 1];
                chArray[index + 7] = strEnCode[index + 2];
                chArray[index + 8] = strEnCode[index + 3];
                chArray[index + 1] = strEnCode[index + 4];
                chArray[index] = strEnCode[index + 5];
                chArray[index + 3] = strEnCode[index + 6];
                chArray[index + 5] = strEnCode[index + 7];
                chArray[index + 4] = strEnCode[index + 8];
                chArray[index + 9] = strEnCode[index + 9];
            }
            chArray[0x3e] = strEnCode[60];
            chArray[60] = strEnCode[0x3d];
            chArray[0x3d] = strEnCode[0x3e];
            chArray[0x3f] = strEnCode[0x3f];
            return new string(chArray);
        }

        public string DecodePassword(string strEncodePassword)
        {
            string str = "";
            Regex regex = new Regex(@"[\w]");
            for (int i = 0; i < strEncodePassword.Length; i++)
            {
                char ch = strEncodePassword[i];
                str = str + (regex.IsMatch(ch.ToString()) ? this.strWPlus[this.strWPluseEncode.IndexOf(strEncodePassword[i])] : strEncodePassword[i]);
            }
            return str;
        }

        public int Delete(string strDel)
        {
            return this.DAL1.ExecuteNonQuery(strDel);
        }

        public long DirectorySize(string strDirectory)
        {
            if (!Directory.Exists(strDirectory))
            {
                return 0L;
            }
            long num = 0L;
            foreach (string str in Directory.GetFiles(strDirectory))
            {
                FileInfo info = new FileInfo(str);
                num += info.Length;
            }
            foreach (string str2 in Directory.GetDirectories(strDirectory))
            {
                num += this.DirectorySize(str2);
            }
            return num;
        }

        public string EncodePassword(string strPassword)
        {
            string str = "";
            Regex regex = new Regex(@"[\w]");
            for (int i = 0; i < strPassword.Length; i++)
            {
                char ch = strPassword[i];
                str = str + (regex.IsMatch(ch.ToString()) ? this.strWPluseEncode[this.strWPlus.IndexOf(strPassword[i])] : strPassword[i]);
            }
            return str;
        }

        public void EngineRegEx(string myString)
        {
            this._Regex = "(";
            this._EngineName = "";
            int index = 0;
            int length = this._Enginers.Length;
            while (index < length)
            {
                if (myString.Contains(this._Enginers[index][0]))
                {
                    this._EngineName = this._Enginers[index][0];
                    this._Coding = this._Enginers[index][1];
                    this._RegexWord = this._Enginers[index][2];
                    string str = this._Regex;
                    this._Regex = str + this._EngineName + ".+.*[?/&]" + this._RegexWord + "[=:])(?<key>[^&]*)";
                    return;
                }
                index++;
            }
        }

        public int ExecuteNonQuery(string strQuery)
        {
            return this.DAL1.ExecuteNonQuery(strQuery);
        }

        public object ExecuteScalar(string strQuery)
        {
            return this.DAL1.ExecuteScalar(strQuery);
        }

        public string GB2312ToUTF8(string myString)
        {
            string[] strArray = myString.Split(new char[] { '%' });
            byte[] bytes = new byte[] { Convert.ToByte(strArray[1], 0x10), Convert.ToByte(strArray[2], 0x10) };
            Encoding srcEncoding = Encoding.GetEncoding("GB2312");
            Encoding dstEncoding = Encoding.UTF8;
            bytes = Encoding.Convert(srcEncoding, dstEncoding, bytes);
            char[] chars = new char[dstEncoding.GetCharCount(bytes, 0, bytes.Length)];
            dstEncoding.GetChars(bytes, 0, bytes.Length, chars, 0);
            return new string(chars);
        }

        public string GetClientIP()
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request.ServerVariables["HTTP_VIA"] != null)
            {
                //debug 
                string ip = request.ServerVariables["HTTP_VIA"].ToString();
                String ip1 = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                String ip2 = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(new char[] { ',' })[0].Trim();
                //
                return request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(new char[] { ',' })[0].Trim();
            }
            return request.UserHostAddress;
        }

        public string GetContentBetweenTags(string strStartTags, string strEndTags, string strTempContent)
        {
            int length = strStartTags.Length;
            int index = strTempContent.IndexOf(strStartTags);
            if (index == -1)
            {
                return "";
            }
            int num2 = strTempContent.IndexOf(strEndTags, (int)(index + length));
            return strTempContent.Substring(index + length, (num2 - index) - length);
        }

        public DataTable GetDataTable(string strQuery)
        {
            DataSet dataSet = this.DAL1.GetDataSet(strQuery);
            if (dataSet == null)
            {
                return null;
            }
            return dataSet.Tables[0];
        }

        public DataTable GetDataTable(string strQuery, OleDbParameter[] OlePara)
        {
            DataSet dataSet = this.DAL1.GetDataSet(strQuery, OlePara);
            if (dataSet == null)
            {
                return null;
            }
            return dataSet.Tables[0];
        }

        public DataTable GetDataTablePage(int nStart, int nSize, string strQuery)
        {
            DataSet set = this.DAL1.GetDataTablePage(nStart, nSize, strQuery);
            if (set == null)
            {
                return null;
            }
            return set.Tables[0];
        }

        public string GetDomainName(string strAbsUrl)
        {
            string str = strAbsUrl.Substring(strAbsUrl.IndexOf(":") + 3);
            return str.Substring(0, str.IndexOf('/'));
        }

        public string GetDomainUrl(int nDepth)
        {
            string str = HttpContext.Current.Request.Url.ToString();
            int length = str.LastIndexOf('/');
            str = str.Substring(0, length);
            if (nDepth == 2)
            {
                length = str.LastIndexOf('/');
                str = str.Substring(0, length);
            }
            length = str.LastIndexOf('/');
            return str.Substring(0, length + 1);
        }

        public int GetFileSize(ref string strSize)
        {
            float num;
            string s = Convert.ToString(this.ExecuteScalar("select top 1 UpFileSize from QH_SiteInfo"));
            if (s.IndexOf(".") == -1)
            {
                s = s + ".0";
            }
            try
            {
                num = float.Parse(s) * 1024f;
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("转换图片文件大小错误：" + exception.ToString());
                num = 1048576f;
            }
            switch ((num < 1048576f))
            {
                case false:
                    strSize = (num / 1048576f).ToString("#0.00M");
                    break;

                case true:
                    strSize = (num / 1024f).ToString("#0.00K");
                    break;
            }
            return (int)num;
        }

        public string GetFileString(string strAbsPath)
        {
            string str = null;
            if (!System.IO.File.Exists(strAbsPath))
            {
                return null;
            }
            Encoding encoding = Encoding.UTF8;
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(strAbsPath, encoding);
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

        public int GetPicSize(ref string strSize)
        {
            float num;
            string s = Convert.ToString(this.ExecuteScalar("select top 1 UpPicSize from QH_SiteInfo"));
            if (s.IndexOf(".") == -1)
            {
                s = s + ".0";
            }
            try
            {
                num = float.Parse(s) * 1024f;
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("转换图片文件大小错误：" + exception.ToString());
                num = 1048576f;
            }
            switch ((num < 1048576f))
            {
                case false:
                    strSize = (num / 1048576f).ToString("#0.00M");
                    break;

                case true:
                    strSize = (num / 1024f).ToString("#0.00K");
                    break;
            }
            return (int)num;
        }

        public static int GetStringLength(string str)
        {
            int num = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (Regex.IsMatch(str.Substring(i, 1), @"[\u4e00-\u9fa5]"))
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
            }
            return num;
        }

        public static string GetSubString(string str, int length)
        {
            string str2 = str;
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < str2.Length; i++)
            {
                if (Regex.IsMatch(str2.Substring(i, 1), @"[\u4e00-\u9fa5]"))
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                if (num <= length)
                {
                    num2++;
                }
                if (num >= length)
                {
                    return str2.Substring(0, num2);
                }
            }
            return str2;
        }

        public string getTemplateAbsPath(string strTFName, string TAbsPath, string strMdlName, out string strPath, out string strRetMsg)
        {
            strRetMsg = "";
            strPath = Convert.ToString(this.DAL1.ExecuteScalar("select TemplatePath from TemplateInfo where IsUsed=true and IsMobile=false"));
            string path = TAbsPath + strPath;
            if (!Directory.Exists(path))
            {
                strRetMsg = "模板路径/template/" + strPath + "不存在！";
                return "";
            }
            string str2 = path + "/" + strTFName;
            if ((strTFName != "Module_TagsAndSearch.aspx") && !System.IO.File.Exists(str2))
            {
                strRetMsg = strMdlName + "模板文件/template/" + strPath + "/" + strTFName + "不存在！";
                return "";
            }
            return str2;
        }

        public string getTemplateAbsPath(string strTFName, string TAbsPath, string strMdlName, string strPath, out string strRetMsg)
        {
            strRetMsg = "";
            string path = TAbsPath + strPath;
            if (!Directory.Exists(path))
            {
                strRetMsg = "模板路径/template/" + strPath + "不存在！";
                return "";
            }
            string str2 = path + "/" + strTFName;
            if (!System.IO.File.Exists(str2))
            {
                strRetMsg = strMdlName + "模板文件/template/" + strPath + "/" + strTFName + "不存在！";
                return "";
            }
            return str2;
        }

        public string getTemplateAbsPath_Mobile(string strTFName, string TAbsPath, string strMdlName, out string strPath, out string strRetMsg)
        {
            strRetMsg = "";
            strPath = Convert.ToString(this.DAL1.ExecuteScalar("select TemplatePath from TemplateInfo where IsUsed=true and IsMobile=true"));
            string path = TAbsPath + strPath + "/";
            if (!Directory.Exists(path))
            {
                strRetMsg = "模板路径/template/" + strPath + "/不存在！";
                return "";
            }
            string str2 = path + strTFName;
            if (!System.IO.File.Exists(str2))
            {
                strRetMsg = strMdlName + "模板文件/template/" + strPath + "/" + strTFName + "不存在！";
                return "";
            }
            strPath = "";
            return str2;
        }

        public string getTemplateAbsPath_Mobile(string strTFName, string TAbsPath, string strMdlName, string strPath, out string strRetMsg)
        {
            strRetMsg = "";
            string path = TAbsPath + strPath + "/";
            if (!Directory.Exists(path))
            {
                strRetMsg = "模板路径/template/" + strPath + "/不存在！";
                return "";
            }
            string str2 = path + strTFName;
            if (!System.IO.File.Exists(str2))
            {
                strRetMsg = strMdlName + "模板文件/template/" + strPath + "/" + strTFName + "不存在！";
                return "";
            }
            strPath = "";
            return str2;
        }

        public string GetTmpltFileName(string strMdlName)
        {
            switch (strMdlName)
            {
                case "0":
                    strMdlName = "首页";
                    return strMdlName;

                case "1":
                    strMdlName = "简介模块";
                    return strMdlName;

                case "2":
                    strMdlName = "新闻模块";
                    return strMdlName;

                case "3":
                    strMdlName = "产品模块";
                    return strMdlName;

                case "4":
                    strMdlName = "下载模块";
                    return strMdlName;

                case "5":
                    strMdlName = "图片模块";
                    return strMdlName;

                case "6":
                    strMdlName = "留言反馈";
                    return strMdlName;

                case "7":
                    strMdlName = "外部模块";
                    return strMdlName;

                case "8":
                    strMdlName = "招聘模块";
                    return strMdlName;
            }
            strMdlName = "未知模块";
            return strMdlName;
        }

        public string GetTmpltFileName(string strMdl, out string strMdlName)
        {
            strMdlName = "NoFind";
            switch (strMdl)
            {
                case "0":
                    strMdlName = "首页";
                    return "Module_home.aspx";

                case "1":
                    strMdlName = "简介模块";
                    return "Module_JianJie.aspx";

                case "2":
                    strMdlName = "新闻模块";
                    return "Module_News.aspx";

                case "3":
                    strMdlName = "产品模块";
                    return "Module_Product.aspx";

                case "4":
                    strMdlName = "下载模块";
                    return "Module_Download.aspx";

                case "5":
                    strMdlName = "图片模块";
                    return "Module_Picture.aspx";

                case "6":
                    strMdlName = "留言反馈";
                    return "Module_Message.aspx";

                case "8":
                    strMdlName = "招聘模块";
                    return "Module_ZhaoPin.aspx";

                case "ND":
                    strMdlName = "新闻内容页";
                    return "Module_NewsDetails.aspx";

                case "PD":
                    strMdlName = "产品内容页";
                    return "Module_ProductDetails.aspx";

                case "DD":
                    strMdlName = "下载内容页";
                    return "Module_DownloadDetails.aspx";

                case "MD":
                    strMdlName = "图片内容页";
                    return "Module_PictureDetails.aspx";

                case "TS":
                    strMdlName = "标贴搜索页";
                    return "Module_TagsAndSearch.aspx";

                case "NS":
                    strMdlName = "新闻标贴搜索页";
                    return "Module_TagsAndSearchNews.aspx";

                case "PS":
                    strMdlName = "产品标贴搜索页";
                    return "Module_TagsAndSearchProduct.aspx";

                case "PI":
                    strMdlName = "产品价格区间页";
                    return "Module_ProductPrice.aspx";

                case "Map":
                    strMdlName = "地图页";
                    return "Module_Map.aspx";
            }
            return "";
        }

        public string GetTmpltFileName_mobile(string strMdl, out string strMdlName)
        {
            strMdlName = "NoFind";
            switch (strMdl)
            {
                case "0":
                    strMdlName = "首页";
                    return "Mobile_home.aspx";

                case "1":
                    strMdlName = "简介模块";
                    return "Mobile_JianJie.aspx";

                case "2":
                    strMdlName = "新闻模块";
                    return "Mobile_News.aspx";

                case "3":
                    strMdlName = "产品模块";
                    return "Mobile_Product.aspx";

                case "4":
                    strMdlName = "下载模块";
                    return "Mobile_Download.aspx";

                case "5":
                    strMdlName = "图片模块";
                    return "Mobile_Picture.aspx";

                case "6":
                    strMdlName = "留言反馈";
                    return "Mobile_Message.aspx";

                case "8":
                    strMdlName = "招聘模块";
                    return "Mobile_ZhaoPin.aspx";

                case "ND":
                    strMdlName = "新闻内容页";
                    return "Mobile_NewsDetails.aspx";

                case "PD":
                    strMdlName = "产品内容页";
                    return "Mobile_ProductDetails.aspx";

                case "DD":
                    strMdlName = "下载内容页";
                    return "Mobile_DownloadDetails.aspx";

                case "MD":
                    strMdlName = "图片内容页";
                    return "Mobile_PictureDetails.aspx";

                case "Map":
                    strMdlName = "地图页";
                    return "Mobile_Map.aspx";
            }
            return "";
        }

        public string GetToEncryptDomain(string strDomain)
        {
            string[] strArray = new string[] { 
                ".com.cn", ".edu.cn", ".net.cn", ".org.cn", ".gov.cn", "ac.cn", ".bj.cn", ".sh.cn", ".tj.cn", ".cq.cn", ".he.cn", ".sx.cn", ".nm .cn", ".ln.cn", ".jl.cn", ".hl.cn", 
                ".js.cn", ".zj.cn", ".ah.cn", ".fj.cn", ".jx.cn", ".sd.cn", ".ha.cn", ".hb.cn", ".hn.cn", ".gd.cn", ".gx.cn", ".hi.cn", ".sc.cn", ".gz.cn", ".yn .cn", ".xz.cn", 
                ".sn.cn", ".gs.cn", ".qh.cn", ".nx.cn", ".xj.cn", ".tw.cn", ".hk.cn", ".mo.cn", ".co.jp", ".co.uk"
             };
            strDomain = strDomain.ToLower();
            string oldValue = "";
            foreach (string str2 in strArray)
            {
                if (strDomain.Contains(str2))
                {
                    oldValue = str2;
                    strDomain = strDomain.Replace(oldValue, "$$");
                    break;
                }
            }
            if (oldValue == "")
            {
                oldValue = strDomain.Substring(strDomain.LastIndexOf('.'));
                strDomain = strDomain.Replace(oldValue, "$$");
            }
            int num = strDomain.LastIndexOf('.');
            if (num != -1)
            {
                strDomain = strDomain.Substring(num + 1);
            }
            strDomain = strDomain.Replace("$$", oldValue).ToUpper();
            return strDomain;
        }

        public string GetUTF8String(string myString)
        {
            MatchCollection matchs = new Regex("(?<key>%..%..)", RegexOptions.IgnoreCase).Matches(myString);
            int num = 0;
            int count = matchs.Count;
            while (num < count)
            {
                string oldValue = matchs[num].Groups["key"].Value.ToString();
                myString = myString.Replace(oldValue, this.GB2312ToUTF8(oldValue));
                num++;
            }
            return myString;
        }

        public int InsertOrUpdate(string strUpInsert)
        {
            return this.DAL1.ExecuteNonQuery(strUpInsert);
        }

        public string isCrawler(string SystemInfo)
        {
            string[] strArray = new string[] { "google", "baidu", "yahoo", "bing", "youdao", "iask", "soso", "sogou", "zhongsou", "lycos", "exactseek", "so" };
            foreach (string str in strArray)
            {
                if (SystemInfo.ToLower().Contains(str.ToLower()))
                {
                    return str;
                }
            }
            if (SystemInfo.ToLower().Contains(Convert.ToString(this.DAL1.ExecuteScalar("select top 1 SitePath from QH_SiteInfo")).ToLower()))
            {
                return "SelfJump";
            }
            return "otherEngine";
        }

        public bool IsIPAddress(string str1)
        {
            if (((str1 == null) || (str1 == string.Empty)) || ((str1.Length < 7) || (str1.Length > 15)))
            {
                return false;
            }
            string pattern = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        private bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            foreach (PixelFormat format in this.indexedPixelFormats)
            {
                if (format.Equals(imgPixelFormat))
                {
                    return true;
                }
            }
            return false;
        }

        public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, bool bDel)
        {
            string str2;
            Image image = Image.FromFile(originalImagePath);
            int num = width;
            int num2 = height;
            int x = 0;
            int y = 0;
            int num5 = image.Width;
            int num6 = image.Height;
            int num7 = 0;
            int num8 = 0;
            if (((str2 = mode) != null) && (str2 != "HW"))
            {
                if (!(str2 == "W"))
                {
                    if (str2 == "H")
                    {
                        num = (image.Width * height) / image.Height;
                        num7 = (width - num) / 2;
                    }
                    else if (str2 == "LB")
                    {
                        if (((image.Width * 1f) / ((float)image.Height)) > 1f)
                        {
                            num2 = (image.Height * width) / image.Width;
                            num8 = (height - num2) / 2;
                        }
                        else
                        {
                            num = (image.Width * height) / image.Height;
                            num7 = (width - num) / 2;
                        }
                    }
                    else if (str2 == "Cut")
                    {
                        if ((((double)image.Width) / ((double)image.Height)) > (((double)num) / ((double)num2)))
                        {
                            num6 = image.Height;
                            num5 = (image.Height * num) / num2;
                            y = 0;
                            x = (image.Width - num5) / 2;
                        }
                        else
                        {
                            num5 = image.Width;
                            num6 = (image.Width * height) / num;
                            x = 0;
                            y = (image.Height - num6) / 2;
                        }
                    }
                }
                else
                {
                    num2 = (image.Height * width) / image.Width;
                    num8 = (height - num2) / 2;
                }
            }
            Image image2 = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(ColorTranslator.FromHtml("#d2d2d2"));
            graphics.DrawImage(image, new Rectangle(num7, num8, num, num2), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
            try
            {
                string str3 = Path.GetExtension(originalImagePath).ToLower();
                if (str3 == null)
                {
                    goto Label_023B;
                }
                if (!(str3 == ".gif") && !(str3 == ".jpg"))
                {
                    if (str3 == ".bmp")
                    {
                        goto Label_021D;
                    }
                    if (str3 == ".png")
                    {
                        goto Label_022C;
                    }
                    goto Label_023B;
                }
                image2.Save(thumbnailPath, ImageFormat.Jpeg);
                goto Label_027B;
            Label_021D:
                image2.Save(thumbnailPath, ImageFormat.Bmp);
                goto Label_027B;
            Label_022C:
                image2.Save(thumbnailPath, ImageFormat.Png);
                goto Label_027B;
            Label_023B:
                image2.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("添加图片水印错误：" + exception.ToString());
            }
            finally
            {
                image.Dispose();
                image2.Dispose();
                graphics.Dispose();
            }
        Label_027B:
            if (bDel)
            {
                System.IO.File.Delete(originalImagePath);
            }
        }

        public ArrayList ReadColumnID(string strTemp, string strTags)
        {
            ArrayList list = new ArrayList();
            int startIndex = 0;
            for (int i = 0; i < 100; i++)
            {
                int index = strTemp.IndexOf(strTags, startIndex);
                if (index == -1)
                {
                    return list;
                }
                startIndex = index + 14;
                int num2 = strTemp.IndexOf("]", startIndex);
                if (num2 == -1)
                {
                    return list;
                }
                string strLoopRule = strTemp.Substring(index, (num2 - index) + 1).Replace(']', ',');
                list.Add(this.ReadValue(strLoopRule, "Column"));
            }
            return list;
        }

        public void ReadColumnID(ref string strTemp, string[] astrValue, string strTags)
        {
            int startIndex = 0;
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    string str;
                    int index = strTemp.IndexOf(strTags, startIndex);
                    if (index == -1)
                    {
                        return;
                    }
                    startIndex = index + 9;
                    int num2 = strTemp.IndexOf("]", startIndex);
                    if (num2 == -1)
                    {
                        return;
                    }
                    string str3 = str = strTemp.Substring(index, (num2 - index) + 1);
                    str = str.Replace(']', ',');
                    string newValue = this.SetValue(str, "Column", astrValue[i]);
                    newValue = newValue.Substring(0, newValue.Length - 1) + "]";
                    strTemp = strTemp.Insert(index, "{{");
                    strTemp = strTemp.Replace("{{" + str3, newValue);
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("替换模板文件内容错误： " + exception.ToString());
            }
        }

        public string ReadDataReader(string strQuery)
        {
            try
            {
                return this.DAL1.ReadDataReader(strQuery);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("ReadDataReader读数据库错误： " + exception.ToString());
            }
            return "<NULL>";
        }

        public string ReadDataReader(string strQuery, OleDbParameter[] OlePara)
        {
            try
            {
                return this.DAL1.ReadDataReader(strQuery, OlePara);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("ReadDataReaderOlePara读数据库错误： " + exception.ToString());
            }
            return "<NULL>";
        }

        public void ReadDataReader(string strQuery, ref string[] astrRet, string[] astrField)
        {
            try
            {
                this.DAL1.ReadDataReader(strQuery, ref astrRet, astrField);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("ReadDataReader读数据库错误： " + exception.ToString());
            }
        }

        public void ReadDataReader(string strQuery, ref string[] astrRet, int nNum)
        {
            try
            {
                this.DAL1.ReadDataReader(strQuery, ref astrRet, nNum);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("ReadDataReader读数据库错误： " + exception.ToString());
            }
        }

        public string ReadDataReader1(string strField1, string strID)
        {
            string[] strValue = new string[] { "" };
            string[] strField = new string[] { strField1 };
            string strQuery = "select " + strField[0] + " from QH_Column where id=" + strID;
            try
            {
                this.DAL1.ReadDataReader(strQuery, ref strValue, strField);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("读栏目数据库错误： " + exception.ToString());
                return "";
            }
            return strValue[0];
        }

        public ArrayList ReadDataReaderAL(string strQuery)
        {
            try
            {
                return this.DAL1.ReadDataReaderAL(strQuery);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("ReadDataReaderArrayList读数据库错误： " + exception.ToString());
            }
            return null;
        }

        public List<List<string>> ReadDataReaderList(string strQuery, int nNum)
        {
            try
            {
                return this.DAL1.ReadDataReaderList(strQuery, nNum);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("ReadDataReaderList读数据库错误： " + exception.ToString());
            }
            return null;
        }

        public void ReadDataReaderPara(string strQuery, ref string[] astrRet, string[] astrField, string[] astrParaName, string[] astrParaVal)
        {
            OleDbParameter[] olePara = new OleDbParameter[astrParaName.Length];
            try
            {
                for (int i = 0; i < astrParaName.Length; i++)
                {
                    olePara[i] = new OleDbParameter("@" + astrParaName[i], astrParaVal[i]);
                }
                this.DAL1.ReadDataReader(strQuery, ref astrRet, astrField, olePara);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("ReadDataReaderPara读数据库错误： " + exception.ToString());
            }
        }

        public void ReaderBind(string strQuery, object obj2Bind, string strBindField)
        {
            this.DAL1.ReadDataReader(strQuery, obj2Bind, strBindField);
        }

        public List<string[]> ReadListModule(string strTemp)
        {
            List<string[]> list = new List<string[]>();
            int startIndex = 0;
            for (int i = 0; i < 100; i++)
            {
                int index = strTemp.IndexOf("[QH:loop ", startIndex);
                if (index == -1)
                {
                    return list;
                }
                startIndex = index + 9;
                int num2 = strTemp.IndexOf("]", startIndex);
                if (num2 == -1)
                {
                    return list;
                }
                string strLoopRule = strTemp.Substring(index, (num2 - index) + 1).Replace(']', ',');
                string str2 = this.ReadValue(strLoopRule, "Module");
                if (!(str2 == "14") && !(str2 == "15"))
                {
                    string[] item = new string[] { str2, this.ReadValue(strLoopRule, "Column"), this.ReadValue(strLoopRule, "NewsCount") };
                    list.Add(item);
                }
            }
            return list;
        }

        public void ReadListModule(ref string strTemp, string[,] a2dstrValue)
        {
            int startIndex = 0;
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    string str;
                    int index = strTemp.IndexOf("[QH:loop ", startIndex);
                    if (index == -1)
                    {
                        return;
                    }
                    startIndex = index + 9;
                    int num2 = strTemp.IndexOf("]", startIndex);
                    if (num2 == -1)
                    {
                        return;
                    }
                    string str3 = str = strTemp.Substring(index, (num2 - index) + 1);
                    str = str.Replace(']', ',');
                    switch (this.ReadValue(str, "Module"))
                    {
                        case "14":
                        case "15":
                            i--;
                            break;

                        default:
                            {
                                string strLoopRule = this.SetValue(str, "Column", a2dstrValue[i, 0]);
                                strLoopRule = this.SetValue(strLoopRule, "NewsCount", a2dstrValue[i, 1]);
                                strLoopRule = strLoopRule.Substring(0, strLoopRule.Length - 1) + "]";
                                strTemp = strTemp.Insert(index, "{{");
                                strTemp = strTemp.Replace("{{" + str3, strLoopRule);
                                break;
                            }
                    }
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("替换模板文件内容错误： " + exception.ToString());
            }
        }

        private string ReadValue(string strLoopRule, string strName)
        {
            int index = strLoopRule.IndexOf(strName);
            string str = "";
            if (index != -1)
            {
                str = strLoopRule.Substring(index);
                index = str.IndexOf('=');
                if (index == -1)
                {
                    return "";
                }
                int num2 = str.IndexOf(',');
                if (num2 != -1)
                {
                    return str.Substring(index + 1, (num2 - index) - 1);
                }
            }
            return "$";
        }

        public static bool RemoteIsExist(string url)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            bool flag;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";
                request.Timeout = 0x7d0;
                response = (HttpWebResponse)request.GetResponse();
                flag = response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (request != null)
                {
                    request.Abort();
                    request = null;
                }
            }
            return flag;
        }

        public string SearchKey(string myString)
        {
            this.EngineRegEx(myString.ToLower());
            if (!(this._EngineName != ""))
            {
                return "";
            }
            Regex regex = new Regex(this._Regex, RegexOptions.IgnoreCase);
            myString = regex.Match(myString).Groups["key"].Value;
            myString = myString.Replace("+", " ");
            if (this._Coding == "gb2312")
            {
                myString = this.GetUTF8String(myString);
                return myString;
            }
            if (this._Coding == "utf8")
            {
                myString = Uri.UnescapeDataString(myString);
                return myString;
            }
            myString = Uri.UnescapeDataString(myString);
            byte[] bytes = Encoding.UTF8.GetBytes(myString);
            myString = Encoding.UTF8.GetString(bytes);
            return myString;
        }

        public static void Send(string to, string from, string subject, string body, string userName, string password, string smtpHost)
        {
            try
            {
                MailAddress address = new MailAddress(from);
                MailAddress address2 = new MailAddress(to);
                MailMessage message = new MailMessage(address, address2)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = body
                };
                new SmtpClient(smtpHost) { Credentials = new NetworkCredential(userName, password) }.Send(message);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("发送邮件错误： " + exception.ToString());
            }
        }

        public bool SetAuthorDatabase(string strValue, string strAbsPath)
        {
            FileInfo info = new FileInfo(strAbsPath);
            DateTime lastWriteTime = info.LastWriteTime;
            DateTime lastAccessTime = info.LastAccessTime;
            strValue = this.Date6Encode(strValue);
            string fileString = this.GetFileString(strAbsPath);
            if (fileString == null)
            {
                return false;
            }
            string str2 = this.GetContentBetweenTags("pt\" style=\"color:#", ";", fileString);
            if (str2 == "")
            {
                return false;
            }
            if (str2 != strValue)
            {
                fileString = fileString.Replace("pt\" style=\"color:#" + str2 + ";", "pt\" style=\"color:#" + strValue + ";");
                this.WriteFile(strAbsPath, fileString);
                info.LastWriteTime = lastWriteTime;
                info.LastAccessTime = lastAccessTime;
                info.Refresh();
            }
            return true;
        }

        public bool SetAuthorDatabase2(string strValue, string strAbsPath)
        {
            FileInfo info = new FileInfo(strAbsPath);
            DateTime lastWriteTime = info.LastWriteTime;
            DateTime lastAccessTime = info.LastAccessTime;
            strValue = this.Date6Encode2(strValue);
            string fileString = this.GetFileString(strAbsPath);
            if (fileString == null)
            {
                return false;
            }
            string str2 = this.GetContentBetweenTags("<th class=\"th", "\"", fileString);
            if (str2 == "")
            {
                return false;
            }
            if (str2 != strValue)
            {
                fileString = fileString.Replace("<th class=\"th" + str2 + "\"", "<th class=\"th" + strValue + "\"");
                this.WriteFile(strAbsPath, fileString);
                info.LastWriteTime = lastWriteTime;
                info.LastAccessTime = lastAccessTime;
                info.Refresh();
            }
            return true;
        }

        private string SetValue(string strLoopRule, string strName, string strValue)
        {
            int index = strLoopRule.IndexOf(strName);
            string str = "";
            if (index != -1)
            {
                index = strLoopRule.IndexOf('=', index);
                if (index == -1)
                {
                    return strLoopRule;
                }
                int num2 = strLoopRule.IndexOf(',', index);
                if (num2 != -1)
                {
                    str = strLoopRule.Substring(index + 1, (num2 - index) - 1);
                    strLoopRule = strLoopRule.Insert(index + 1, "{{");
                    strLoopRule = strLoopRule.Replace("{{" + str, strValue);
                    return strLoopRule;
                }
            }
            return strLoopRule;
        }

        public bool TestIfIsNotIPaddressOrLocalhost(string strDomain)
        {
            if (Regex.IsMatch(strDomain, @"((?:(?:25[0-5]|2[0-4]\d|[01]?\d?\d)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d?\d))"))
            {
                return false;
            }
            if (strDomain.IndexOf("localhost") != -1)
            {
                return false;
            }
            return true;
        }

        public void ThumbImg(string originalImagePath, string strC_Img, string strModule, string strDir, bool bDel, string strWidth, string strHeigh)
        {
            this.strModule1 = strModule;
            this.strDir1 = strDir;
            this.DAL1.ReadDataReader("select * from QH_ImgSet where id=1", ref this.astrRet, 0x16);
            string mode = "Cut";
            string str4 = this.astrRet[1];
            if (str4 != null)
            {
                if (!(str4 == "1"))
                {
                    if (str4 == "2")
                    {
                        mode = "LB";
                    }
                    else if (str4 == "3")
                    {
                        mode = "Cut";
                    }
                }
                else
                {
                    mode = "HW";
                }
            }
            if (this.astrRet[2].Trim() == "0")
            {
                string[] strArray = this.astrRet[3].Split(new char[] { 'x' });
                this.nMwidth = int.Parse(strArray[0]);
                this.nMheight = int.Parse(strArray[1]);
            }
            else
            {
                int index = 4;
                string str5 = strModule;
                if (str5 != null)
                {
                    if (!(str5 == "P"))
                    {
                        if (str5 == "I")
                        {
                            index = 5;
                        }
                        else if (str5 == "N")
                        {
                            index = 6;
                        }
                    }
                    else
                    {
                        index = 4;
                    }
                }
                string[] strArray2 = this.astrRet[index].Split(new char[] { 'x' });
                this.nMwidth = int.Parse(strArray2[0]);
                this.nMheight = int.Parse(strArray2[1]);
            }
            this.nMwidth = (strWidth == "") ? this.nMwidth : int.Parse(strWidth);
            this.nMheight = (strHeigh == "") ? this.nMheight : int.Parse(strHeigh);
            string thumbnailPath = this.Page1.Server.MapPath(strC_Img);
            bool flag = this.astrRet[8].Trim() == "True";
            string path = (strDir == "0") ? ("../" + this.astrRet[10].Trim()) : this.astrRet[10].Trim();
            if (flag)
            {
                if (this.astrRet[9].Trim() == "1")
                {
                    if (this.astrRet[13].Trim() == "")
                    {
                        flag = false;
                    }
                }
                else if (!System.IO.File.Exists(this.Page1.Server.MapPath(path)))
                {
                    flag = false;
                }
            }
            if (flag)
            {
                thumbnailPath = this.Page1.Server.MapPath(strC_Img.Insert(strC_Img.LastIndexOf('.'), "Temp"));
            }
            this.MakeThumbnail(originalImagePath, thumbnailPath, this.nMwidth, this.nMheight, mode, bDel);
            if (flag)
            {
                if (this.astrRet[9].Trim() == "1")
                {
                    this.AddWater(thumbnailPath, this.Page1.Server.MapPath(strC_Img), false);
                }
                else
                {
                    this.AddWaterPic(thumbnailPath, this.Page1.Server.MapPath(strC_Img), this.Page1.Server.MapPath(path));
                }
            }
        }

        public int TimesCharInString(string str, char ch)
        {
            int num = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ch)
                {
                    num++;
                }
            }
            return num;
        }

        public int UpdateMove(string strSelect, string strUpdate, string[] astrField, List<List<string>> listData)
        {
            try
            {
                return this.DAL1.AdpaterUpdate(strSelect, strUpdate, astrField, listData);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("移动选择的产品栏目数据库错误： " + exception.ToString());
            }
            return 0;
        }

        public int UpdateMoveNews(string strWhere, List<List<string>> listData)
        {
            string strSelect = "select id,ColumnID from QH_News where id in (" + strWhere + ")";
            string strUpdate = "UPDATE QH_News SET ColumnID=@ColumnID WHERE ID=@ID";
            string[] astrField = new string[] { "ColumnID" };
            try
            {
                return this.DAL1.AdpaterUpdate(strSelect, strUpdate, astrField, listData);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("移动选择的新闻栏目数据库错误： " + exception.ToString());
            }
            return 0;
        }

        public int UpdateSelAll(string strSelect, string strUpdate, string[] astrField, List<List<string>> listData)
        {
            try
            {
                return this.DAL1.AdpaterUpdate(strSelect, strUpdate, astrField, listData);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("更新选择栏目数据库错误： " + exception.ToString());
            }
            return 0;
        }

        public int UpdateTable(string strUpdate, string strField, string[] astrValue)
        {
            string[] strArray = strField.Split(new char[] { ',' });
            OleDbParameter[] olePara = new OleDbParameter[strArray.Length];
            try
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    olePara[i] = new OleDbParameter("@" + strArray[i], astrValue[i]);
                }
                return this.DAL1.ExecuteNonQuery(strUpdate, olePara);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("更新栏目数据库错误： " + exception.ToString());
            }
            return 0;
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

        public string Coding
        {
            get
            {
                return this._Coding;
            }
        }

        public string EngineName
        {
            get
            {
                return this._EngineName;
            }
        }

        public string RegexWord
        {
            get
            {
                return this._RegexWord;
            }
        }
    }
}
