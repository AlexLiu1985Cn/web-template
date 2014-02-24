<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_BaseConfig.aspx.cs" Inherits="CmsApp20.CmsBack.QH_BaseConfig" %>

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
<body>
    <form id="form1" runat="server">
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：系统设置 >> <a href="QH_BaseConfig.aspx">基本信息设置</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>

        <table width =840px align="center" cellpadding="2" cellspacing="2">
  		  <tr> 
            <td  width=200px align=right ><font color=red>*</font>网站标题：</td>
            <td  align=left> <input name="SiteTitle1" type="text" id="SiteTitle2" value="<%=SiteTitle%>" size="40" maxlength="255" ></td>
          </tr>
  		  <tr> 
            <td  width=200px align=right >公司名称：</td>
            <td  align=left> <input name="CompanyName" type="text" id="CompanyName" value="<%=strCompanyName%>" size="40" maxlength="255" ></td>
          </tr>
		<tr>
			<td align=right>网站Logo：</td>
            <td align=left>
				<input  name="LogoUrl" type="text" style="width: 200px;" value="<%=strLogoUrl%>" maxlength="200" /> 
				<input  type="file" id="myFile" runat=server onchange="CheckImg('myFile')"  />
				</td>
          </tr> 
		<tr>
			<td align=right>网站图标：</td>
            <td align=left>
				<input  name="FaviconUrl" type="text" style="width: 200px;" value="<%=strFaviconUrl%>" maxlength="200" /> 
				<input  type="file" id="FaviconFile" runat=server onchange="CheckFavicon(this)"  />
				</td>
          </tr>		  <tr> 
            <td align=right>网站地址：</td>
            <td align=left><input name="SitePath1" id="SitePath1" type="text" size="40" maxlength="200" value="<%=SitePath%>"></td>
          </tr>
         <tr> 
		    <td align=right></td>
            <td align=left style="color:Green;">搜索引擎优化设置(seo)</td> 
        </tr>
     	<tr> 
            <td align=right>关键词：</td>
            <td align=left><input name="keywords" type="text"  maxlength="255" size=40  value="<%=SiteKeyword%>" /><span class="tips">多个关键词请用","隔开</span></td>
        </tr>
 		<tr> 
            <td align=right>简短描述：</td>
            <td align=left><textarea name="description" cols="50" rows="4" onkeydown="checklength(this,254);" onblur="maxtext(this,254)" ><%=SiteDescription%></textarea></td>
        </tr>
  		  <tr> 
            <td align=right>友情链接开关：</td>
            <td align=left>
	        <input type="radio" name="FriendL" value="1" <%=strFLOn %> />开启&nbsp;&nbsp;
	        <input type="radio" name="FriendL" value="0" <%=strFLOff %> />关闭
            </td>
          </tr>
      	<tr> 
            <td align=right>所属行业：</td>
            <td align=left>
<select name="TMShowType" id="TMShowType" runat=server >
	<option selected="selected" value="0">所有行业</option>
	<option value="37">通用企业</option>
	<option value="38">服装、纺织行业</option>
	<option value="39">机电、化工行业</option>
	<option value="40">能源、矿产行业</option>
	<option value="41">环保、农林行业</option>
	<option value="42">家居、装饰行业</option>
	<option value="43">交通、运输行业</option>
	<option value="44">旅游、会展行业</option>
	<option value="45">外贸、出口行业</option>
	<option value="46">电脑、网络行业</option>
	<option value="47">生物、医药行业</option>
	<option value="48">传媒、娱乐行业</option>
	<option value="49">金融、投资行业</option>
	<option value="50">餐饮、食品行业</option>
	<option value="51">管理、咨询行业</option>
	<option value="52">房产、建筑行业</option>
	<option value="53">科研、院所行业</option>
	<option value="54">电器、电子行业</option>
	<option value="55">教育、培训行业</option>
	<option value="56">汽车、配件行业</option>
	<option value="57">法律、法规行业</option>
	<option value="58">文化、艺术行业</option>
	<option value="59">日用、家政行业</option>
	<option value="60">其它行业</option>
</select>            <span class="tips"></span></td>
        </tr>
  		  <tr> 
            <td align=right>多语言版本开关：</td>
            <td align=left>
	        <input type="radio" name="MultiLang" id="MultiLang" value="1" <%=strLangOn %> />开启&nbsp;&nbsp;
	        <input type="radio" name="MultiLang" value="0" <%=strLangOff %> />关闭
            </td>
          </tr>
  		  <tr id="MLangSet" > 
            <td align=right></td>
            <td align=left>
	        <a href="MultiLLangManage.aspx" target="main-frame" id="LangAnchor" >多语言设置管理</a><span class="tips" id="Mtips" style ="display :none; margin-left :60px;">请将多语言版本开关打开并保存，来激活此链接</span>
            </td>
          </tr>
       <tr> 
            <td></td>
            <td align=left>
                <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " onclick="BtnSave_Click" /><%--<asp:Button ID="Button1" runat="server" Text=" 返 回 " onclick="Button1_Click"  CssClass="ret"/>--%></td>
        </tr>
     </table>

 
</div>
</div>
    </form>
    <script type ="text/javascript" >
    $2=function(id) {return document .getElementById (id);}
    $2("SitePath1").disabled=true ;
    var objRadio=$2("MultiLang");
    var objAnchor=$2("LangAnchor");
    $2("LangAnchor").disabled=!objRadio.checked ;
    if(!objRadio.checked)
    {
     objAnchor.href="javascript:void(0)";
     $2("Mtips").style.display="";
    }
    </script>
</body>
</html>
