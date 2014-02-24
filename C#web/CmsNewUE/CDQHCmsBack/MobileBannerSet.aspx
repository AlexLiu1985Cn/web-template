<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileBannerSet.aspx.cs" Inherits="CmsApp20.CmsBack.MobileBannerSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
<style type="text/css">
        body{ font-family :Microsoft Yahei;font-size:12px;}
        .tips{color:#999999; }
        .ret{ margin-left :160px;}
        .now{font-weight:bold;	color:#fff;}
        div a{text-decoration:none;color:#555;}
       .or{ width:46px; }
       .Cln{font-weight:bold;}
   </style>
</head>
<script language =javascript >
$=function(id) {return document .getElementById (id);}
String.prototype.Trim = function()
{
	return this.replace( /(^\s*)|(\s*$)/g, '' ) ;
}
function MobileCheck(){
if(!(/^\d+%?$/.test($("WidthM").value))){
alert("宽度应为数字！");
return false;
}
if(!(/^\d+$/.test($("HeightM").value))){
alert("高度应为数字！");
return false;
}
return true;
}

</script>
<body>
    <form id="form1" runat="server">
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：手机管理 >> <a href="MobileBannerSet.aspx">手机Banner设置</a></div>
<div class="rightmain1">二级位置：手机Banner设置 - <a href="MobileBannerManage.aspx">手机Banner管理</a></div>
<div class="rightmain1"> </div>
   <div style ="text-align :center ">
        <table cellpadding="2" cellspacing="6" width =820px>
  		  <tr> 
            <td  width=200px align=right >Banner模式：</td>
            <td  align=left> 
            <select name="BnrModeM" id="BnrModeM"  runat=server >
				<option value="0" >关闭</option>
				<option value="1"  style="color:#FF0000">图片轮播</option>
				<option value="3"  style="color:#339933">单张图片</option>
			</select>
          </tr>
		  <tr>
			<td align=right >宽(像素)：</td>
			<td align=left><input  name ="WidthM" id="WidthM" type="text" maxlength=4 class="or"  runat=server />
			</td>
		  </tr>
		  <tr>
			<td align=right >高(像素)：</td>
			<td align=left><input  name ="HeightM" id="HeighthM" type="text" maxlength=4 class="or"  runat=server />
			</td>
		  </tr>
       <tr> 
            <td></td>
            <td align=left>
                <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " onclick="BtnSave_Click"  OnClientClick="return MobileCheck();"/>
                <asp:Button ID="Button1" runat="server" Text=" 重 置 " CssClass="ret" onclick="BtnReset_Click" />
            </td>
        </tr>
     </table>
    </div>
 
</div>
</div>
    </form>
</body>
</html>
