namespace _BLL
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Xml;

    public class CompressAcDb
    {
        private const double GBCount = 1073741824.0;
        private const double KBCount = 1024.0;
        private const double MBCount = 1048576.0;
        private const double TBCount = 1099511627776;

        public static void CompactAccessDB(string connectionString, string mdwfilename, string strTempFile)
        {
            object target = Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));
            object[] args = new object[] { connectionString, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strTempFile + ";Jet OLEDB:Engine Type=5" };
            target.GetType().InvokeMember("CompactDatabase", BindingFlags.InvokeMethod, null, target, args);
            File.Delete(mdwfilename);
            File.Move(strTempFile, mdwfilename);
            Marshal.ReleaseComObject(target);
            target = null;
        }

        public static string CountSize(long Size)
        {
            string str = "";
            long num = 0L;
            num = Size;
            if (num < 1024.0)
            {
                return (num.ToString("F2") + " Byte");
            }
            if ((num >= 1024.0) && (num < 1048576.0))
            {
                double num2 = ((double)num) / 1024.0;
                return (num2.ToString("F0") + " KB");
            }
            if ((num >= 1048576.0) && (num < 1073741824.0))
            {
                double num3 = ((double)num) / 1048576.0;
                return (num3.ToString("F2") + " MB");
            }
            if ((num >= 1073741824.0) && (num < 1099511627776))
            {
                double num4 = ((double)num) / 1073741824.0;
                return (num4.ToString("F2") + " GB");
            }
            if (num >= 1099511627776)
            {
                str = ((((double)num) / 1099511627776)).ToString("F2") + " TB";
            }
            return str;
        }

        public static void Modify(string key, string strvalue, bool bRoot)
        {
            string str = bRoot ? "" : "../";
            string str2 = "/configuration/appSettings/add[@key='Count']";
            XmlDocument document = new XmlDocument();
            document.Load(HttpContext.Current.Server.MapPath(str + "web.config"));
            XmlNode node = document.SelectSingleNode(str2.Replace("Count", key));
            if (node == null)
            {
                throw new ArgumentException("没有找到<add key=" + key + " value=.../>的配置节");
            }
            node.Attributes["value"].InnerText = strvalue;
            document.Save(HttpContext.Current.Server.MapPath(str + "web.config"));
        }
    }
}