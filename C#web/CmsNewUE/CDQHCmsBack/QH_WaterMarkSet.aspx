<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_WaterMarkSet.aspx.cs" Inherits="CmsApp.CmsBack.QH_WaterMarkSet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
 <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
      <script type ="text/javascript"  src ="js/CmsBack.js"></script> 
  <style type="text/css">
        body{ font-family :Microsoft Yahei;font-size:12px;}
        .tips{color:#999999; }
        .tipsL{color:#333333; margin-left :100px;}
        .ret{ margin-left :120px;}
        .ret1{ margin-left :60px;}
    </style>
</head>
<script language =javascript type="text/javascript" >
$=function(id) {return document .getElementById (id);}
String.prototype.Trim = function()
{
	return this.replace( /(^\s*)|(\s*$)/g, '' ) ;
}
function MarkCheck(){
if(!/^$|^[1-9][0-9]*$/.test($("img_x").value)){
alert("添加水印的大图最小宽度应为整数！");return false;}
if(!/^$|^[1-9][0-9]*$/.test($("img_y").value)){
alert("添加水印的大图最小高度应为整数！");return false;}
if(!/^(?:\d{1,2}|100)$/.test($("PicTransp").value)){
alert("水印图片透明度应为0－100之间的整数！");return false;}
if(!/^(?:\d{1,3})$/.test($("text_size").value)){
alert("缩略图水印文字大小应为正整数！");return false;}
if(!/^(?:\d{1,3})$/.test($("text_bigsize").value)){
alert("大图水印文字大小应为正整数！");return false;}
if(!/^(-0|-[1-9]|(-[1-9][0-9])|(-[1-3][0-5][0-9])|-360|0|[1-9]|([1-9][0-9])|([1-3][0-5][0-9])|360)$/.test($("text_angle").value)){
alert("水印文字角度大小应为－360到360的整数！");return false;}
if(!/^#[0-9a-fA-F]{6}$/.test($("text_color").value)){
alert("水印文字颜色应为如#F1F1F1格式，#应为英文键盘！");return false;}
if(!/^(?:\d{1,2}|100)$/.test($("TextTransp").value)){
alert("水印文字透明度应为0－100之间的整数！");return false;}
return true;
}
function showDialog(Url,W,H,Re,Sc){
var features="status:0;dialogWidth:"+W+"px;dialogHeight:"+H+"px;resizable:"+Re+";scroll:"+Sc+";center:1,depended:1";
showModalDialog(Url+"?"+Math .random () ,1,features);
}
</script>
<%--^(?:0|[1-9][0-9]?|100)$--%>
<body>
    <form id="form1" runat="server">
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：页面风格 >> <a href="QH_WaterMarkSet.aspx">水印设置</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>    
        <table width =820px align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
  		  <tr> 
            <td  width=200px align=right >图片添加水印：</td>
            <td  align=left> 
            <input type="checkbox" value=1 id=big runat =server onclick="if(this.checked) $('A').style.display='';else  $('A').style.display='none';"  >详细大图片添加&nbsp;&nbsp;
	        <input type="checkbox" value=1 id=thumb runat =server >缩略图片添加</td>
          </tr>
		  <tr id=A runat=server > 
            <td align=right>添加水印的大图最小尺寸：</td>
            <td align=left>
	        <input name="img_x"  id="img_x" type="text" size=6 maxlength="4" runat=server />× 
	        <input name="img_y"  id="img_y" type="text" size=6 maxlength="4" runat=server /><span class="tips">(宽 × 高)(像素)</span></td>
          </tr>
		  <tr>
			<td align=right >水印类型：</td>
			<td align=left>
	        <input type="radio" name="water_class" value="1"  id=Text runat=server />文字水印&nbsp;&nbsp;
	        <input type="radio" name="water_class" value="2"  id=pic runat=server />图片水印
			</td>
		  </tr>
		<tr>
			<td align=right>缩略图水印图片：</td>
            <td align=left>
				<input  id="ThumbImg" type="text" style="width: 200px;" runat=server maxlength=200 /> 
				<input  type="file" id="ThumbUp" runat=server onchange="CheckImg('ThumbUp')"  />
		  </td>
          </tr> 
		<tr>
			<td align=right>大图水印图片：</td>
            <td align=left>
				<input  id="BigImg" type="text" style="width: 200px;" runat=server maxlength=200 /> 
				<input  type="file" id="BigUp" runat=server onchange="CheckImg('BigUp')"  />
		  </td>
          </tr> 
<%--		<tr>
			<td align=right>水印图片：</td>
            <td align=left>
				<input type="text" id="BigImgpt" style="color:#cbcbcb;font-weight:bold;" runat=server maxlength=200 /> 
				<input  type="file" id="BigUp" runat=server onchange="CheckImg('BigUp')"  />
		  </td>
          </tr> 
--%>	    <tr> 
	    <td align=right>水印图片透明度：</td>
	    <td align=left><input name="PicTransp" type="text"  id="PicTransp" runat=server size=3 maxlength=3 ><span class="tips">0－100</span></td>
	    </tr>
	    <tr> 
	    <td align=right>水印文字：</td>
	    <td align=left><input name="WaterText" type="text"  id="WaterText" runat=server maxlength=200 ><span class="tips"><%--不支持中文（中文水印需要下载中文字体才能支持）--%></span></td>
	    </tr>
	    <tr> 
	    <td align=right>缩略图水印文字大小：</td>
	    <td align=left><input name="text_size" id="text_size" type="text"  runat=server size=3 maxlength=3 ><span class="tips">像素</span></td>
	    </tr>
	    <tr> 
	    <td align=right>大图水印文字大小 ：</td>
	    <td align=left><input name="text_bigsize" id="text_bigsize" type="text"  runat=server size=3 maxlength=3 ><span class="tips">像素</span></td>
	    </tr> 
	    <tr> 
	    <td align=right>水印文字字体：</td>
	    <td align=left><input name="text_fonts" id="text_fonts" type="text" runat=server maxlength=200  size=30 ><span class="tips">请将字体文件放到<%--后台管理--%>根目录下的include/fonts/下,可不添。</span></td>
	    </tr>
	    <tr> 
	    <td align=right>水印文字角度：</td>
	    <td align=left><input name="text_angle"  id="text_angle" type="text"  runat=server maxlength=4 ><span class="tips">水平为0 (-360～360)</span></td>
	    </tr> 
	    <tr> 
	    <td align=right>水印文字颜色：</td>
	    <td align=left><input name="text_color" id="text_color" type="text" runat=server maxlength=7 >
	    <select name='select_color' class="select" size=1 onChange="if(this.options[this.selectedIndex].value!='') 
	    {$('text_color').value=this.options[this.selectedIndex].value}"> 
	    <option>选择颜色</option>
	    <option style="background-color: #FFFFFF;color:#FFFFFF" value="#FFFFFF">白色</option> 
	    <option style="background-color:Black;color:Black" value="#000000">黑色</option> 
	    <option style="background-color:Red;color:Red" value="#FF0000">红色</option> 
	    <option style="background-color:Yellow;color:Yellow" value="#FFFF00">黄色</option> 
	    <option style="background-color:Green;color:Green" value="#008000">绿色</option> 
	    <option style="background-color:Orange;color:Orange" value="#FF8000">橙色</option> 
	    <option style="background-color:Purple;color:Purple" value="#800080">紫色</option> 
	    <option style="background-color:Blue;color:Blue" value="#0000FF">蓝色</option> 
	    <option style="background-color:Brown;color:Brown" value="#800000">褐色</option> 
	    <option style="background-color:#00FFFF;color: #00FFFF" value="#00FFFF">粉绿</option> 
	    <option style="background-color:#7FFFD4;color: #7FFFD4" value="#7FFFD4">淡绿</option> 
	    <option style="background-color:#FFE4C4;color: #FFE4C4" value="#FFE4C4">黄灰</option> 
	    <option style="background-color:#7FFF00;color: #7FFF00" value="#7FFF00">翠绿</option> 
	    <option style="background-color:#D2691E;color: #D2691E" value="#D2691E">综红</option> 
	    <option style="background-color:#FF7F50;color: #FF7F50" value="#FF7F50">砖红</option> 
	    <option style="background-color:#6495ED;color: #6495ED" value="#6495ED">淡蓝</option> 
	    <option style="background-color:#DC143C;color: #DC143C" value="#DC143C">暗红</option> 
	    <option style="background-color:#FF1493;color: #FF1493" value="#FF1493">玫红</option> 
	    <option style="background-color:#FF00FF;color: #FF00FF" value="#FF00FF">紫红</option> 
	    <option style="background-color:#FFD700;color: #FFD700" value="#FFD700">桔黄</option> 
	    <option style="background-color:#DAA520;color: #DAA520" value="#DAA520">军黄</option> 
	    <option style="background-color:#808080;color: #808080" value="#808080">烟灰</option> 
	    <option style="background-color:#778899;color: #778899" value="#778899">深灰</option> 
	    <option style="background-color:#B0C4DE;color: #B0C4DE" value="#B0C4DE">灰蓝</option> 
	    </select> 
	    </td>
	    </tr>
	    <tr> 
	    <td align=right>水印文字透明度：</td>
	    <td align=left><input name="TextTransp" type="text"  id="TextTransp" runat=server size=3 maxlength=3 ><span class="tips">0－100</span></td>
	    </tr>
	    <tr> 
	    <td align=right>水印位置：</td>
	    <td align=left style=" line-height:25px;"> 
	    <label><input type="radio" value="0" name="waterPos" id="Pos0" runat=server />左上角</label>&nbsp;&nbsp;
	    <label><input type="radio" value="1" name="waterPos" id="Pos1" runat=server />顶中部</label>&nbsp;&nbsp;
	    <label><input type="radio" value="2" name="waterPos" id="Pos2" runat=server />右上角</label>&nbsp;&nbsp;<br/>
	    <label><input type="radio" value="3" name="waterPos" id="Pos3" runat=server />左中部</label>&nbsp;&nbsp;
	    <label><input type="radio" value="4" name="waterPos" id="Pos4" runat=server />中间部</label>&nbsp;&nbsp;
	    <label><input type="radio" value="5" name="waterPos" id="Pos5" runat=server />右中部</label>&nbsp;&nbsp;<br/>
	    <label><input type="radio" value="6" name="waterPos" id="Pos6" runat=server />左下角</label>&nbsp;&nbsp;
	    <label><input type="radio" value="7" name="waterPos" id="Pos7" runat=server />底中部</label>&nbsp;&nbsp;
	    <label><input type="radio" value="8" name="waterPos" id="Pos8" runat=server />右下角</label>&nbsp;
	    </td>
	    </tr>  
       <tr> 
            <td></td>
            <td align=left>
                <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " onclick="BtnSave_Click"  OnClientClick="return MarkCheck();"/>
                <asp:Button ID="BtnReset" runat="server" Text=" 重 置 " CssClass="ret" onclick="BtnReset_Click" />
            </td>
        </tr>
      <tr>
      <td align=left colspan=2 ><span class="tipsL"><%=strWTip %><%--请在保存设置的情况下查看以下水印效果图--%></span></td>
      </tr>
      <tr id=LKT >
        <td align=right><%=strWPic %><%--文字水印效果：--%></td>
       <td align=left >
          <input id="BT1" type="button" value="详细大图文字水印效果"  onclick="showDialog('WMarkEffect/WMarkTextBig.aspx',470,470,0,0);" /><input id="BT2" type="button" value="缩略图文字水印效果"  onclick="showDialog('WMarkEffect/WMarkTextThumb.aspx',800,400,1,1)" Class="ret1" />
          </td>
      </tr>
      <tr id=LKP >
        <td align=right><%=strWText %><%--图片水印效果：--%></td>
       <td align=left >
          <input id="BP1" type="button" value="详细大图图片水印效果" onclick="showDialog('WMarkEffect/WMarkPicBig.aspx',470,470,0,0);" /><input id="BP2" type="button" value="缩略图图片水印效果" Class="ret1" onclick="showDialog('WMarkEffect/WMarkPicThumb.aspx',800,400,1,1);" />&nbsp;&nbsp;
          </td>
      </tr>
     </table>

  
</div>
</div>
    </form>
</body>
</html>

<%--   
      <tr id=LKT runat=server >
      <td align=center colspan =2 >
          文字水印：<input id="B1" type="button" value="大图水印效果"  onclick="window.open('../WaterMark.aspx','','width=800,height=300');" />&nbsp;&nbsp;<input id="B2" type="button" value="产品模块水印效果"  onclick="var features='status:0;dialogWidth:470px;dialogHeight:470px;dialogTop:100px;dialogLeft:100px;resizable:0;scroll:1;center:1';showModelessDialog('../WaterMark.aspx',window,features);" />&nbsp;&nbsp;
          <input id="B3" type="button" value="新闻模块水印效果" />&nbsp;&nbsp;<input id="B4" type="button" value="图片模块水印效果" /></td>
      </tr>
      <tr id=LKP  runat=server >
      <td align=center colspan =2 >
          图片水印：<input id="BP1" type="button" value="大图水印效果"  onclick="window.open('../WaterMark.aspx');" />&nbsp;&nbsp;<input id="BP2" type="button" value="产品模块水印效果" />&nbsp;&nbsp;
          <input id="BP3" type="button" value="新闻模块水印效果" />&nbsp;&nbsp;<input id="BP4" type="button" value="图片模块水印效果" /></td>
      </tr>
--%>