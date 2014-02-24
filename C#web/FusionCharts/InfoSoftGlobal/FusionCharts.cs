using System;
using System.Text;
using System.Web;

namespace InfoSoftGlobal
{
    public class FusionCharts
    {
        private static int boolToNum(bool value)
        {
            return (value ? 1 : 0);
        }

        public static string EncodeDataURL(string dataURL, bool noCacheStr)
        {
            string str = dataURL;
            if (noCacheStr)
            {
                str = (str + ((dataURL.IndexOf("?") != -1) ? "&" : "?")) + "FCCurrTime=" + DateTime.Now.ToString().Replace(":", "_");
            }
            return HttpUtility.UrlEncode(str);
        }

        public static string RenderChart(string chartSWF, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<!-- START Script Block for Chart {0} -->" + Environment.NewLine, chartId);
            builder.AppendFormat("<div id='{0}Div' align='center'>" + Environment.NewLine, chartId);
            builder.Append("Chart." + Environment.NewLine);
            builder.Append("</div>" + Environment.NewLine);
            builder.Append("<script type=\"text/javascript\">" + Environment.NewLine);
            builder.AppendFormat("var chart_{0} = new FusionCharts(\"{1}\", \"{0}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");" + Environment.NewLine, new object[] { chartId, chartSWF, chartWidth, chartHeight, boolToNum(debugMode), boolToNum(registerWithJS) });
            if (strXML.Length == 0)
            {
                builder.AppendFormat("chart_{0}.setDataURL(\"{1}\");" + Environment.NewLine, chartId, strURL);
            }
            else
            {
                builder.AppendFormat("chart_{0}.setDataXML(\"{1}\");" + Environment.NewLine, chartId, strXML);
            }
            builder.AppendFormat("chart_{0}.render(\"{1}Div\");" + Environment.NewLine, chartId, chartId);
            builder.Append("</script>" + Environment.NewLine);
            builder.AppendFormat("<!-- END Script Block for Chart {0} -->" + Environment.NewLine, chartId);
            return builder.ToString();
        }

        public static string RenderChartHTML(string chartSWF, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode)
        {
            StringBuilder builder = new StringBuilder();
            string str = string.Empty;
            if (strXML.Length == 0)
            {
                str = string.Format("&chartWidth={0}&chartHeight={1}&debugMode={2}&dataURL={3}", new object[] { chartWidth, chartHeight, boolToNum(debugMode), strURL });
            }
            else
            {
                str = string.Format("&chartWidth={0}&chartHeight={1}&debugMode={2}&dataXML={3}", new object[] { chartWidth, chartHeight, boolToNum(debugMode), strXML });
            }
            builder.AppendFormat("<!-- START Code Block for Chart {0} -->" + Environment.NewLine, chartId);
            builder.AppendFormat("<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0\" width=\"{0}\" height=\"{1}\" name=\"{2}\">" + Environment.NewLine, chartWidth, chartHeight, chartId);
            builder.Append("<param name=\"allowScriptAccess\" value=\"always\" />" + Environment.NewLine);
            builder.AppendFormat("<param name=\"movie\" value=\"{0}\"/>" + Environment.NewLine, chartSWF);
            builder.AppendFormat("<param name=\"FlashVars\" value=\"{0}\" />" + Environment.NewLine, str);
            builder.Append("<param name=\"quality\" value=\"high\" />" + Environment.NewLine);
            builder.AppendFormat("<embed src=\"{0}\" FlashVars=\"{1}\" quality=\"high\" width=\"{2}\" height=\"{3}\" name=\"{4}\"  allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />" + Environment.NewLine, new object[] { chartSWF, str, chartWidth, chartHeight, chartId });
            builder.Append("</object>" + Environment.NewLine);
            builder.AppendFormat("<!-- END Code Block for Chart {0} -->" + Environment.NewLine, chartId);
            return builder.ToString();
        }
    }
}
