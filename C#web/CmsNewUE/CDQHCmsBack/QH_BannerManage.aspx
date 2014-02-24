<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_BannerManage.aspx.cs" Inherits="CmsApp20.CmsBack.QH_BannerManage" %>

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
        .now{font-weight:bold;	color:#fff;}
        .topa{text-decoration:none;color:#555;}
  </style>
</head>
<script type="text/jscript" >
$=function(id) {return document .getElementById (id);}
String.prototype.Trim = function()
{
	return this.replace( /(^\s*)|(\s*$)/g, '' ) ;
}
function fDel(){
return confirm ("确定要删除？");
}
</script>
<body>
<%--        <div style ="width :100px; height :30px; margin-left :140px;  text-align:center;  line-height:30px;float:left;" ><a class="topa" href ="QH_BannerSet.aspx">Banner设置</a></div>
        <div class="now" style ="width :100px; height :30px; margin-left :140px; background-color:#1F75B7 ; text-align:center;  line-height:30px; ">Banner管理</div>
        <div style =" clear :both ; margin-left :140px; border-top: solid 1px #555;"><br /></div>
--%>
    <form id="form1" runat="server">
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：系统设置 >> <a href="QH_BannerManage.aspx">Banner管理</a></div>
<div class="rightmain1">二级位置：<a href="QH_BannerSet.aspx">Banner设置</a> - Banner管理</div>
<div class="rightmain1"> </div>
    <table width="840px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF" >
   <tr><td width="180" height="35">
 <a href="QH_AddBanner.aspx" class="add">+添加Banner</a></td><td>
 <span >所属栏目<asp:DropDownList ID="DDLClmn" 
        runat="server" AutoPostBack=true onselectedindexchanged="DDLClmn_SelectedIndexChanged" 
             ></asp:DropDownList> &nbsp;&nbsp;类型 <asp:DropDownList ID="DDLImgType" 
        runat="server" AutoPostBack=true onselectedindexchanged="DDLClmn_SelectedIndexChanged" 
             ></asp:DropDownList>
     &nbsp;&nbsp;&nbsp;&nbsp </span> </td></tr></table>
  
<table width="840px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF" >
  <tr style="background:url(images/1.jpg) repeat-x; height:30px; color:#FFFFFF; font-weight:bold;">
  	<td width="30" height="30" ><div align="center" >选择</div></td>
    <td width="40" height="30" ><div align="center" >排序</div></td>
    <td width="300" height="30" ><div align="center" >所属栏目</div></td>
    <td width="60" ><div align="center">类型</div></td>
    <td width="120" ><div align="center" >图片标题</div>    </td>
    <td width="210" ><div align="center">图片地址/Flash地址</div></td>
    <td width="100" ><div align="center" >操作</div></td>
  </tr>
  <asp:Repeater  ID="RepeaterBanner" runat =server  EnableViewState=true >
  <ItemTemplate >
  <tr  bgcolor="#e6e6e6" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
  	<td height="30"><div align="center">
  	  <input type="checkbox" name="checkbox" id="checkbox" value="checkbox" runat=server  />
	  </div></td>
  	<input type="hidden" id="SelectedID" runat="server" 
                                value='<% # DataBinder.Eval(Container.DataItem, "id")%>'/>
    <td height="30"> <div align="center"><%# Eval("no_order")%></div></td><td height="30"><div align="left" title="<%# Eval("Belongs1")%>" > <%# Eval("Belongs")%></div></td>
    <td height="30" ><div align="center"><%# Eval("type")%></div></td>
    <td height="30"><div align="left"><%# Eval("Title")%></div></td>
    <td height="30"><div align="left" ><%# Eval("Url")%></div></td>
    <td height="30"><div align="right"><span ><a href="QH_ModifyBanner.aspx?id=<%# Eval("id") %>&Page=<%=strPage %>" >修改</a> &nbsp;|&nbsp;<a href="QH_DelBanner.aspx?id=<%# Eval("id") %>&Page=<%=strPage %>"  onclick ="return fDel();">删除</a></span>&nbsp;&nbsp;</div></td>
  </tr>
 </ItemTemplate>
 <AlternatingItemTemplate>
  <tr bgcolor="#f9f9f9" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
  	<td height="30"><div align="center">
  	  <input type="checkbox" name="checkbox" id="checkbox" value="checkbox" runat=server />
	  </div></td>
  	<input type="hidden" id="SelectedID" runat="server" 
                                value='<% # DataBinder.Eval(Container.DataItem, "id")%>'/>
    <td height="30"> <div align="center"><%# Eval("no_order")%></div></td><td height="30"><div align="left" title="<%# Eval("Belongs1")%>" ><%# Eval("Belongs")%></div></td>
    <td height="30" ><div align="center"><%# Eval("type")%></div></td>
    <td height="30"><div align="left"><%# Eval("Title")%></div></td>
    <td height="30"><div align="left" ><%# Eval("Url")%></div></td>
    <td height="30"><div align="right"><span ><a href="QH_ModifyBanner.aspx?id=<%# Eval("id") %>&Page=<%=strPage %>" >修改</a> &nbsp;|&nbsp;<a href="QH_DelBanner.aspx?id=<%# Eval("id") %>&Page=<%=strPage %>"  onclick ="return fDel();">删除</a></span>&nbsp;&nbsp;</div></td>
  </tr>
</AlternatingItemTemplate>
</asp:Repeater> 
   <!-- 是否全选开始 -->
   <tr>
    <td height="30" colspan="4" bgcolor="" align=left> <asp:CheckBox   Text= "全选"   ID= "Checkbox1"   Runat= "server"   AutoPostBack= "true"    OnCheckedChanged= "CheckboxslctallSV_CheckedChanged"   /> 
    <span ><asp:Button ID="Button1"  runat="server" Text="删除选定的Banner图" Height="28px" OnClientClick="return confirm('确定要删除选定的项目吗？');" OnClick="ButtondeleteSV_Clicked"/></span>    
    </td>
    <td height="30" align= right colspan =2 >每页显示：<asp:DropDownList ID="DDLPage" 
            runat="server" AutoPostBack=true 
            onselectedindexchanged="DDLPage_SelectedIndexChanged" ></asp:DropDownList> </td>
    </tr>
  <!-- 是否全选结束 -->
</table>
<table width="840px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF" >
<tr><td height="30">
                              共有<asp:Label ID="lblRecordCount" runat="server" ForeColor="red"></asp:Label>条记录
                              当前为<asp:Label ID="lblCurrentPage" runat="server" ForeColor="red"></asp:Label>/<asp:Label
                                  ID="lblPageCount" runat="server" ForeColor="red"></asp:Label>页
                              <asp:LinkButton ID="lbnPrevPage" runat="server" CommandName="prev" OnCommand="Page_OnClick"
                                  Text="上一页"></asp:LinkButton>
                              <asp:LinkButton ID="lbnNextPage" runat="server" CommandName="next" OnCommand="Page_OnClick"
                                  Text="下一页"></asp:LinkButton>
                                  
                                  
                              &nbsp;转到：<asp:DropDownList ID="DropDownListPage" runat="server" 
                                   onselectedindexchanged="DropDownListPage_SelectedIndexChanged" AutoPostBack=true>
                              </asp:DropDownList>      
 </td></tr></table>
      

    <asp:HiddenField ID="HdnClmn" runat="server" />  
  
</div>
</div>
    </form>
</body>
</html>
