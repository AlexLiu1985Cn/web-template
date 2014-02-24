<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileColumn.aspx.cs" Inherits="CmsApp20.CmsBack.MobileColumn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
   <script type ="text/javascript"  src ="js/NavFrame.js"></script> 
<style type="text/css">
        body
        {
            font-family: Microsoft Yahei;
            font-size: 12px;
        }
.or{ width:30px; }
.or1{ width:90px; }
.or2{ width:90px; }
.ND{display:none;}
</style>
</head>
<body>
    <form id="form1" runat="server">
   
    <table width =240px align="center" cellpadding="2" cellspacing="2" id=table1  >
    <thead style="background:url(images/1.jpg) repeat-x;font-weight:bold;color:#fff;"  >
    <tr>
    <td width=160px>栏目名称
    </td>
    <td width=140px>手机导航栏显示
    </td>
    </tr>
    </thead>
        <asp:Repeater ID="RTMobile" runat="server" EnableViewState=false >
        <ItemTemplate>
  <tr  bgcolor="#e6e6e6" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
    <td><input name="TNm<%#((string[])Container.DataItem)[0] %>" id="TNm" type="text"  class=or1 value=<%#((string[])Container.DataItem)[1] %> /></td>
    <td>
        <select name="Sel<%#((string[])Container.DataItem)[0] %>" >
            <option  value=0 <%#((string[])Container.DataItem)[2] %> >不显示</option>
            <option  value=1 <%#((string[])Container.DataItem)[3] %> >头部主导航条</option>
            <option  value=2 <%#((string[])Container.DataItem)[4] %> >尾部导航条</option>
            <option  value=3 <%#((string[])Container.DataItem)[5] %> >都显示</option>
       </select>
    </tr>
   </ItemTemplate>
   <AlternatingItemTemplate >
  <tr bgcolor="#f9f9f9" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
    <td><input name="TNm<%#((string[])Container.DataItem)[0] %>" id="TNm" type="text"  class=or1 value=<%#((string[])Container.DataItem)[1] %> /></td>
    <td>
        <select name="Sel<%#((string[])Container.DataItem)[0] %>" >
            <option  value=0 <%#((string[])Container.DataItem)[2] %> >不显示</option>
            <option  value=1 <%#((string[])Container.DataItem)[3] %> >头部主导航条</option>
            <option  value=2 <%#((string[])Container.DataItem)[4] %> >尾部导航条</option>
            <option  value=3 <%#((string[])Container.DataItem)[5] %> >都显示</option>
       </select>
    </tr>
   </AlternatingItemTemplate>

  </asp:Repeater>
  </table>
 <table width =240px align="center" cellpadding="2" cellspacing="2" id=table1  ><tr><td height="30" style="text-align:center;">
        <asp:Button ID="BSaveA" runat="server" Text=" 保 存 " onclick="BSave_Click" OnClientClick ="return SaveAll();" /><asp:HiddenField ID="HdSaveID" runat="server" />
</td>
 </tr></table>
</form>
</body>
</html>
