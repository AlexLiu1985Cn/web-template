<%@ Page Language="C#" AutoEventWireup="true" Inherits="QH_FriendLinkManage" EnableEventValidation="false" Codebehind="QH_FriendLinkManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>友情链接管理</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：其它设置 >> <a href="QH_FriendLinkManage.aspx">友情链接管理</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>
</div> 
</div> 
   <table width="840px" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF" >
   <tr><td height="24"> 
 <a href="QH_AddFriendLink.aspx" class="add">+添加友情链接</a>
</td></tr></table>

    <div id="LinkManage" >
<table width="840px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF" >
  <tr style="background:url(images/1.jpg) repeat-x; height:30px; color:#FFFFFF; font-weight:bold;">
    <td width="120"  align="center">网站名称</td>
      
    <td width="180" align="center">网站地址</td>
      
    <td width="180"  align="center">Logo地址</td>  
    <td width="140"  align="center">网站简介</td>
    <td width="100"  align="center">链接类型</td>
      
      
      
    <td width="120" align="center">操作</td>
    </tr>

  <asp:Repeater  ID="RepeaterFriendLink" runat =server >
    <ItemTemplate >

  <tr  bgcolor="#e6e6e6" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
    <td width="122"  height="28" > 
       <%# Eval("SiteName")%></td>
      
    <td width="214" style="word-break:break-all;"><a href="<%# Eval("SiteUrl")%>" target='blank' title="<%# Eval("SiteUrl")%>"><%# Eval("SiteUrl")%></a></td>
      
      
    <td width="220" style="word-break:break-all;"><%# Eval("LogoUrl")%></td>
    <td width="220"><%# Eval("SiteIntro")%></td>
    <td width="60"><%# Eval("LinkType")%></td>
      
      
    <td  align="center"> 
    <a href='QH_FriendLinkModify.aspx?ID=<%# Eval("id")%>'>修改</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp<a href='QH_FriendLinkDel.aspx?ID=<%# Eval("id")%>' onclick='return ConfirmDel();'>删除</a> </td>
    </tr>
    
 </ItemTemplate>
  <AlternatingItemTemplate>
  
  <tr bgcolor="#f9f9f9" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
    <td width="172"  height="28" > 
       <%# Eval("SiteName")%></td>
      
    <td width="214" style="word-break:break-all;"><a href="<%# Eval("SiteUrl")%>" target='blank' title="<%# Eval("SiteUrl")%>"><%# Eval("SiteUrl")%></a></td>
      
      
    <td width="220" style="word-break:break-all;"><%# Eval("LogoUrl")%></td>
    <td width="220"><%# Eval("SiteIntro")%></td>
    <td width="60"><%# Eval("LinkType")%></td>
      
      
    <td  align="center"> 
      <a href='QH_FriendLinkModify.aspx?ID=<%# Eval("id")%>'>修改</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href='QH_FriendLinkDel.aspx?ID=<%# Eval("id")%>' onclick='return ConfirmDel();'>删除</a> </td>
    </tr>
    
    </AlternatingItemTemplate>
 </asp:Repeater >   
   
    
  </table>

 <table width="840px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF" >
    <tr><td>
                              共有<asp:Label ID="lblRecordCount" runat="server" ForeColor="red"></asp:Label>条记录
                              当前为<asp:Label ID="lblCurrentPage" runat="server" ForeColor="red"></asp:Label>/<asp:Label
                                  ID="lblPageCount" runat="server" ForeColor="red"></asp:Label>页
                              <asp:LinkButton ID="lbnPrevPage" runat="server" CommandName="prev" OnCommand="Page_OnClick"
                                  Text="上一页"></asp:LinkButton>
                              <asp:LinkButton ID="lbnNextPage" runat="server" CommandName="next" OnCommand="Page_OnClick"
                                  Text="下一页">下一页</asp:LinkButton>
                                  
                                  
                              &nbsp;转到：<asp:DropDownList ID="DropDownListPage" runat="server" 
                                   onselectedindexchanged="DropDownListPage_SelectedIndexChanged" AutoPostBack=true>
                              </asp:DropDownList>      
</td> </tr> 
</table> 
                

    </div>

    </form>
</body>
</html>
