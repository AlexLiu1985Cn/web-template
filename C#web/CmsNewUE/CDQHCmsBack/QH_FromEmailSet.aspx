<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_FromEmailSet.aspx.cs" Inherits="CmsApp20.CmsBack.QH_FromEmailSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
 <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
     <script type ="text/javascript"  src ="js/CmsBack.js"></script> 
  <style type="text/css">
        body{ font-family :Microsoft Yahei;font-size:12px;}
        .tips{color:#999999; margin-left:4px; }
        .ret{ margin-left :160px;}
    </style>
</head>
<script type="text/javascript" >
$=function(id) {return document .getElementById (id);}
String.prototype.Trim = function()
{
	return this.replace( /(^\s*)|(\s*$)/g, '' ) ;
}
function EmailCheck(){
if(!/^$|^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test($("fromEmail").value.Trim()))
{
alert("邮箱地址不合格式！");return false;
}
return true;
}
</script>
<body>
    <form id="form1" runat="server">
    <%--<h5 style="margin-left:200px;">系统邮箱配置</h5>--%>
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：系统设置 >> <a href="QH_FromEmailSet.aspx">系统邮箱设置</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>
 
        <table width =820px align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
  		  <tr> 
            <td  width=200px align=right ></td>
            <td  align=left><font color=green>用于系统发送邮件（如留言提醒），站内所有邮件都由该邮箱发送，所以请务必正确填写。</font></td>
          </tr>
		  <tr> 
            <td align=right>邮箱地址：</td>
            <td align=left><input id="fromEmail" type="text" size="30" runat=server ><span class="tips">用于发送邮件的邮箱地址。</span></td>
          </tr>
		  <tr> 
            <td align=right>邮件SMTP服务器：</td>
            <td align=left><input id="fromSMTP" type="text" size="20"  runat=server ><span class="tips">如163邮箱为smtp.163.com，可上网搜索发送邮箱SMTP服务器地址。</span></td>
          </tr>
		  <tr> 
            <td align=right>邮箱密码：</td>
            <td align=left><input name="fromPsw" id="fromPsw" type="password" runat=server > <span class="tips">用于发送邮件的邮箱密码，若不修改则不需要输入。</span></td>
          </tr>
		  <tr> 
            <td align=right>邮件发送测试：</td>
            <td align=left>
                <asp:LinkButton ID="LBtn1" runat="server" onclick="LBtn1_Click">点击测试</asp:LinkButton> <span class="tips">（接收邮箱为《系统设置》－《网站常用信息》中设置的邮箱地址。）</span><span id="emailtest"></span></td>
          </tr>

        <tr> 
            <td></td>
            <td align=left>
                <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " onclick="BtnSave_Click"  OnClientClick="return EmailCheck();"/>
                <asp:Button ID="BtnReset" runat="server" Text=" 重 置 " CssClass="ret" onclick="BtnReset_Click" />
          </td>
        </tr>
     </table>

  
</div>
</div>
   </form>
</body>
</html>
