<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileSiteSet.aspx.cs" Inherits="CmsApp20.CmsBack.MobileSiteSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
      <script type ="text/javascript"  src ="js/CmsBack.js"></script> 
  <style type="text/css">
        body{ font-family :Microsoft Yahei;font-size:12px;}
        .tips{color:#999999; }
        .ret{ margin-left :160px;}
    </style>
   <script type ="text/javascript"  src ="js/NavFrame.js"></script> 
</head>
<body>
    <form id="form1" runat="server">
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：手机管理 >> <a href="MobileSiteSet.aspx">手机网站设置</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>
   <div style="text-align: center; line-height:30px;">
        <table cellpadding="2" cellspacing="6" width =820px>
  		  <tr> 
            <td width=200px align=right>手机网站开关：</td>
            <td align=left>
	        <input type="radio" name="MobileOn" value="1" <%=strMOn %> />开启&nbsp;&nbsp;
	        <input type="radio" name="MobileOn" value="0" <%=strMOff %> />关闭
            </td>
          </tr>
		<tr>
			<td align=right>手机网站Logo：</td>
            <td align=left>
				<input  name="LogoUrl" type="text" style="width: 200px;" value="<%=strLogoUrl%>" maxlength="200" /> 
				<input  type="file" id="myFile" runat=server onchange="CheckImg('myFile')"  />
				</td>
          </tr> 
          <tr> 
            <td align=right>手机网站地址：</td>
            <td align=left><input name="MobilePath" type="text" size="40" maxlength="200" value="<%=MobilePath%>" disabled ></td>
          </tr>
      <tr> 
            <td></td>
            <td align=left>
                <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " onclick="BtnSave_Click" /></td>
        </tr>
     </table>
    </div>
 
</div>
</div>
    </form>
</body>
</html>
