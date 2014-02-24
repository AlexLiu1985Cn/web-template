using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace _commen
{
    public class SystemError
    {
        private static string m_fileName = HttpContext.Current.Server.MapPath("~/CmsSystemlog.log");

        public static void CreateErrorLog(string message)
        {
            if (File.Exists(FileName))
            {
                FileInfo info = new FileInfo(FileName);
                if (info.Length > 0x30d40L)
                {
                    info.Delete();
                }
            }
            if (File.Exists(FileName))
            {
                StreamWriter writer = File.AppendText(FileName);
                writer.WriteLine("\n");
                writer.WriteLine(DateTime.Now.ToString() + message);
                writer.Close();
            }
            else
            {
                StreamWriter writer2 = File.CreateText(FileName);
                writer2.WriteLine("\n");
                writer2.WriteLine(DateTime.Now.ToString() + message);
                writer2.Close();
            }
        }

        public static string FileName
        {
            get
            {
                return m_fileName;
            }
            set
            {
                if ((value != null) || (value != ""))
                {
                    m_fileName = value;
                }
            }
        }
    }
}
