<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_CommenSet.aspx.cs" Inherits="CmsApp20.CmsBack.QH_CommenSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
        <script type ="text/javascript"  src ="js/CmsBack.js"></script> 
<style type="text/css">
        body{ font-family :Microsoft Yahei;font-size:12px;}
        .tips{color:#999999; }
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
if(!/^$|^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test($("Email2").value.Trim()))
{
alert("邮箱地址不合格式！");return false;
}
return true;
}
</script>
<body>
    <form id="form1" runat="server">
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：系统设置 >> <a href="QH_CommenSet.aspx">网站常用信息设置</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>
 
        <table width =820px align="center" cellpadding="2" cellspacing="2">
  		  <tr > 
            <td  width=200px align=right >留言咨询QQ地址：</td>
            <td  align=left><input name="LiuyanQQNo1" type="text" id="LiuyanQQNo2" value="<%=LiuyanQQNo%>" size="40" maxlength="200"> </td>
          </tr>
		<tr>
			<td align=right>在线咨询QQ地址：</td>
            <td align=left>
				<input name="ZaixianQQNo1" type="text" id="ZaixianQQNo2" value="<%=ZaixianQQNo%>" size="40" maxlength="200">
		  </td>
          </tr> 
		  <tr> 
            <td align=right>版权信息：</td>
            <td align=left>
                 <input name="CopyrightInfo" type="text" id="CopyrightInfo" value="<%=strCopyrightInfo%>" size="40" maxlength="200"> </td>
          </tr>
         <tr> 
		    <td align=right>电话：</td>
            <td align=left ><input name="Tel" type="text" id="Tel" value="<%=strTel%>" size="40" maxlength="200"> </td> 
        </tr>
         <tr> 
		    <td align=right>传真：</td>
            <td align=left ><input name="Fax" type="text" id="Fax2" value="<%=strFax%>" size="40" maxlength="200"></td> 
        </tr>
     	<tr> 
            <td align=right>手机：</td>
            <td align=left><input name="Mobile" type="text" id="Mobile2" value="<%=strMobile%>" size="40" maxlength="200"></td>
        </tr>
  		  <tr > 
            <td  width=200px align=right >Email地址：</td>
            <td  align=left><input name="Email" type="text" id="Email2" value="<%=strEmail%>" size="40" maxlength="200"> </td>
          </tr>
		<tr>
			<td align=right>地址：</td>
            <td align=left>
				<input name="Address" type="text" id="Address" value="<%=strAddress%>" size="40" maxlength="200">
		  </td>
          </tr> 
		  <tr> 
            <td align=right>联系人：</td>
            <td align=left>
                 <input name="Contact" type="text" id="Contact" value="<%=strContact%>" size="40" maxlength="200"> </td>
          </tr>
		  <tr> 
            <td align=right>ICP备案：</td>
            <td align=left>
                 <input name="ICPBackup" type="text" id="ICPBackup" value="<%=strICPBackup%>" size="40" maxlength="200"> </td>
          </tr>
		  <tr> 
            <td align=right>公司简称：</td>
            <td align=left>
                 <input name="Author" type="text" id="Author" value="<%=strAuthor%>" size="40" maxlength="200"> </td>
          </tr>
<%--         <tr> 
		    <td align=right>备用字段1：</td>
            <td align=left > <input name="Bak1" type="text" id="Bak1" value="<%=strBak1%>" size="40" maxlength="255"> </td> 
        </tr>
         <tr> 
		    <td align=right>备用字段2：</td>
            <td align=left ><input name="Bak2" type="text" id="Bak2" value="<%=strBak2%>" size="40" maxlength="255"></td> 
        </tr>
     	<tr> 
            <td align=right>备用图片地址1：</td>
            <td align=left><input name="BakImg1" type="text" id="BakImg1" value="<%=strBakImg1%>" size="40" maxlength="200"><input id="ImgFile1" type="file" onchange="CheckImg('ImgFile1')" size="26"  runat="server" name="ImgFile1" /> </td>
        </tr>
     	<tr> 
            <td align=right>备用图片地址2：</td>
            <td align=left><input name="BakImg2" type="text" id="BakImg2" value="<%=strBakImg2%>" size="40" maxlength="200"><input id="ImgFile2" type="file" onchange="CheckImg('ImgFile2')" size="26"  runat="server" name="ImgFile2" /> </td>
        </tr>
--%>        <tr> 
            <td></td>
            <td align=left>
                <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " onclick="BtnSave_Click"  OnClientClick="return EmailCheck();" /></td>
        </tr>
     </table>

 
</div>
</div>
    </form>
</body>
</html>
