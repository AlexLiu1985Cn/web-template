<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="CmsMemberNews.aspx.cs" Inherits="CmsApp20.CDQHCmsBack.CmsMemberNews" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
<head id="Head1" runat="server">
    <title>会员消息</title>
    <style type="text/css">
        body
        {
            font-family: Microsoft Yahei;
            font-size: 12px;
        }
        .add{  font-weight:bold;}
        .srch{ width :140px;}
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
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：会员管理 >> <a href="CmsMemberNews.aspx">会员消息</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>
</div> </div> 

    <form id="form1" runat="server">
   <table width="840px" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF" >
   <tr><td width="180" height="35"> 
 <span id=add1 runat =server  class="add" ><a href="CmsMember_AddNews.aspx?BackPage=" >+添加会员消息</a></span></td>
 <td  width="640" height="35">
 </td></tr></table>

  
<table width="840px" bordercolor="#FFFFFF" align="center" cellpadding="0" cellspacing="2" >
   <tr style="background:url(images/1.jpg) repeat-x; height:30px; color:#FFFFFF; font-weight:bold;">
  	<td width="60" height="30"><div align="center" >选择</div></td>
    <td width="100" height="30"><div align="center" >ID</div></td>
    <td width="420" height="30"><div align="center" >会员消息标题</div></td>
     <td width="100"><div align="center" >更新日期</div>    </td>
   <td width="160"><div align="center" >具体操作</div></td>
  </tr>
  <asp:Repeater  ID="RepeaterNews" runat =server  >
  <ItemTemplate >
  <tr  bgcolor="#e6e6e6" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
  	<td height="30" ><div align="center" ><input type="checkbox" name="checkbox" id="checkbox" value="checkbox" runat=server  /></div></td>
  	<input type="hidden" id="SelectedID" runat="server" 
                                value='<% # DataBinder.Eval(Container.DataItem, "id")%>'/>
    <td height="30"><div align="center" > <%# Eval("id") %></div></td>
    <td height="30" ><div align="left"  style=" overflow:hidden;" > <%# Eval("Title") %></div></td>
    <td height="30"><div align="center"><%# Eval("ModyDate1") %></div></td>
    <td height="30"><div align="center"><span >
     <a href="CmsMember_ModifyNews.aspx?id=<%# Eval("id") %>" >修改</a> 
    &nbsp;&nbsp;| &nbsp;&nbsp;<a href="CmsMember_DelNews.aspx?id=<%# Eval("id") %>"  onclick ="return fDel();">删除</a>&nbsp;</span></div></td>
  </tr>
 </ItemTemplate>
 <AlternatingItemTemplate>
  <tr bgcolor="#f9f9f9" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
  	<td height="30"><div align="center" ><input type="checkbox" name="checkbox" id="checkbox" value="checkbox" runat=server /></div></td>
  	<input type="hidden" id="SelectedID" runat="server" 
                                value='<% # DataBinder.Eval(Container.DataItem, "id")%>'/>
    <td height="30"><div align="center" > <%# Eval("id") %></div></td>
    <td height="30"><div align="left"  style=" overflow:hidden;" ><%# Eval("Title") %></div></td>
    <td height="30"><div align="center"><%# Eval("ModyDate1")%></div></td>
    <td height="30"><div align="center"><span >
     <a href="CmsMember_ModifyNews.aspx?id=<%# Eval("id") %>" >修改</a> 
    &nbsp;&nbsp;| &nbsp;&nbsp;<a href="CmsMember_DelNews.aspx?id=<%# Eval("id") %>"  onclick ="return fDel();">删除</a>&nbsp;</span></div></td>
  </tr>
</AlternatingItemTemplate>
</asp:Repeater> 
   <!-- 是否全选开始 -->
   <tr>
    <td height="30" colspan="4" bgcolor="" align=left> <asp:CheckBox   Text= "全选"   ID= "Checkbox1"   Runat= "server"   AutoPostBack= "true"    OnCheckedChanged= "CheckboxslctallSV_CheckedChanged"   /> 
    <span ><asp:Button ID="Button1"  runat="server" Text="删除选定的项目" Height="30px" OnClientClick="return confirm('确定要删除选定的项目吗？');" OnClick="ButtondeleteSV_Clicked"/></span>    
    </td>
    <td height="30" align=center colspan =2 >每页显示：<asp:DropDownList ID="DDLPage" 
            runat="server" AutoPostBack=true 
            onselectedindexchanged="DDLPage_SelectedIndexChanged" ></asp:DropDownList> </td>
    </tr>
     <tr>
    </tr> <!-- 是否全选结束 -->
</table>
<table width="840px" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF" >
<tr><td height="30">
                              共有
                                  <asp:Label ID="lblRecordCount" runat="server" ForeColor="red"></asp:Label>条记录
                              当前为<asp:Label ID="lblCurrentPage" runat="server" ForeColor="red"></asp:Label>/<asp:Label
                                  ID="lblPageCount" runat="server" ForeColor="red"></asp:Label>页
                              <asp:LinkButton ID="lbnPrevPage" runat="server" CommandName="prev" OnCommand="Page_OnClick"
                                  Text="上一页"></asp:LinkButton>
                              <asp:LinkButton ID="lbnNextPage" runat="server" CommandName="next" OnCommand="Page_OnClick"
                                  Text="下一页">下一页</asp:LinkButton>
                                  
                                  
                              &nbsp;转到：<asp:DropDownList ID="DropDownListPage" runat="server" 
                                   onselectedindexchanged="DropDownListPage_SelectedIndexChanged" AutoPostBack=true>
                              </asp:DropDownList>      
        </td></tr></table>
      

        
        
    </form>
</body>
</html>
