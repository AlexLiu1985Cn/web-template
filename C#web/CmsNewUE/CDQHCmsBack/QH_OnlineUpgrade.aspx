<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_OnlineUpgrade.aspx.cs" Inherits="CmsApp20.CmsBack.QH_OnlineUpgrade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
<%--<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：在线升级 >> <a href="QH_OnlineUpgrade.aspx">在线升级</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>
    </div>
    </div>--%>
  
        <table width =840px align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF" style ="font-size :12px; font-family :'Microsoft YaHei';">
  		  <tr> 
            <td  width=200px align=right>当前版本号：</td>
            <td  width=640px align=left><%=strVersion %>&nbsp;&nbsp;（<%=strNextVersionDisplay%>）</td><%--<%=strLastVersion %>--%>
          </tr>
        <%=strAllVer %>
		<tr>
			<td align=right>官方最新版本：</td>
            <td align=left>
				<%=strLastHighVersion %>
		  </td>
          </tr> 
        <tr> 
            <td></td>
            <td align=left>
                <asp:Button ID="BtnSave" runat="server" Text=" 升级到最新版本 " onclick="BtnSave_Click"  OnClientClick="return confirm('确定要升级？');"/></td>
        </tr>
        <tr ><td></td></tr>
     </table>

  
    <%--<asp:Button ID="Button1" runat="server" Text="添加数据库表" onclick="Button1_Click" Visible=false />--%>
    </form>
</body>
</html>
