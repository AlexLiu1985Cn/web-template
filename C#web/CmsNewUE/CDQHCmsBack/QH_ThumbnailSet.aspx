<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_ThumbnailSet.aspx.cs" Inherits="CmsApp.CmsBack.QH_ThumbnailSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
   <style type="text/css">
        body{ font-family :Microsoft Yahei;font-size:12px;}
        .tips{color:#999999; }
        .ret{ margin-left :120px;}
    </style>
</head>
<script language =javascript >
$=function(id) {return document .getElementById (id);}
String.prototype.Trim = function()
{
	return this.replace( /(^\s*)|(\s*$)/g, '' ) ;
}
function thumbCheck(){
var vID=["img_x","img_y","imgP_x","imgP_y","imgI_x","imgI_y","imgN_x","imgN_y"];
var vMsg=["统一设置","产品模块","图片模块","新闻模块"];
for(var i=0;i<8;i++){
if(!(/^[1-9][0-9]*$/.test($(vID[i]).value))){
var vWH=i%2==0?"宽度":"高度";
alert(vMsg[Math.floor(i/2)]+vWH+"应为数字！");
return false;
}
}
return true;
}
function disp(Num){
if(Num==0)
{
$("A").style.display="";
$("P").style.display="none";
$("I").style.display="none";
$("N").style.display="none";
}
else if(Num==1)
{
$("A").style.display="none";
$("P").style.display="";
$("I").style.display="";
$("N").style.display="";
}
}
</script>
<body>
    <form id="form1" runat="server">
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：页面风格 >> <a href="QH_ThumbnailSet.aspx">缩略图设置</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>

        <table width =820px align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
  		  <tr> 
            <td  width=200px align=right >缩略图生成方式：</td>
            <td  align=left> 
            <input type="radio" name="thumb_kind"  id="kind1" value="1" runat=server />自动拉伸&nbsp;&nbsp;
	        <input type="radio" name="thumb_kind"  id="kind2" value="2" runat=server />自动留白&nbsp;&nbsp;
	        <input type="radio" name="thumb_kind"  id="kind3" value="3" runat=server />自动裁减</td>
          </tr>
		  <tr>
			<td align=right >缩略图大小：</td>
			<td align=left>
	        <input type="radio" name="img_size" value="0" onclick=disp(0) id=SizeS1 runat=server />统一设置&nbsp;&nbsp;
	        <input type="radio" name="img_size" value="1" onclick=disp(1) id=SizeS2 runat=server />分模块设置
			</td>
		  </tr>
		  <tr id=A runat=server > 
            <td align=right><font color=red>*</font>统一设置：</td>
            <td align=left>
	        <input name="img_x"  id="img_x" type="text" size=6 maxlength="3" runat=server />× 
	        <input name="img_y"  id="img_y" type="text" size=6 maxlength="3" runat=server /><span class="tips">(宽 × 高)(像素)</span></td>
          </tr>
		  <tr id=P runat=server > 
            <td align=right><font color=red>*</font>产品模块：</td>
            <td align=left>
	        <input name="imgP_x"  id="imgP_x" type="text" size=6 maxlength="3" runat=server />× 
	        <input name="imgP_y"  id="imgP_y" type="text" size=6 maxlength="3" runat=server /><span class="tips">(宽 × 高)(像素)</span></td>
          </tr>
		  <tr id=I runat=server > 
            <td align=right><font color=red>*</font>图片模块：</td>
            <td align=left>
	        <input name="imgI_x"  id="imgI_x" type="text" size=6 maxlength="3" runat=server />× 
	        <input name="imgI_y"  id="imgI_y" type="text" size=6 maxlength="3" runat=server /></td>
          </tr>
		  <tr id=N runat=server > 
            <td align=right><font color=red>*</font>新闻模块：</td>
            <td align=left>
	        <input name="imgN_x"  id="imgN_x" type="text" size=6 maxlength="3" runat=server />× 
	        <input name="imgN_y"  id="imgN_y" type="text" size=6 maxlength="3" runat=server /></td>
          </tr>
       <tr> 
            <td></td>
            <td align=left>
                <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " onclick="BtnSave_Click"  OnClientClick="return thumbCheck();"/>
                <asp:Button ID="BtnReset" runat="server" Text=" 重 置 " CssClass="ret" onclick="BtnReset_Click" />
            </td>
        </tr>
     </table>

  
</div>
</div>
   </form>
  <%--  <a id='view_bigimg' href='../images/Temp.jpg' title=查看大图 target='_blank'><img id='view_img' border='0' alt='示例产品八' title='示例产品八' width=380 height=350 src='../images/Temp.jpg'></a>--%>
</body>
</html>
