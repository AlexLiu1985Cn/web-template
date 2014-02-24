<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Column.aspx.cs" Inherits="CmsApp.CmsBack.Column" MaintainScrollPositionOnPostback="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
<head runat="server">
    <title>无标题页</title>
    <script type ="text/javascript"  src ="js/AddColumn.js"></script> 
   <script type ="text/javascript"  src ="js/NavFrame.js"></script> 
<style type="text/css">
   
.or{ width:30px; }
.or1{ width:90px; }
.or2{ width:90px; }
.ND{display:none;}
td {white-space:nowrap;overflow:hidden;} 
</style>
</head>
<body>
    <form id="form1" runat="server">
  <div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：内容管理 >> <a href="Column.aspx">>栏目设置</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>
 </div>
</div>
  <div style ="text-align :center ">
    <table width =840px id=table1 cellpadding="4" cellspacing="2" align=center  >
    <thead style="background:url(images/1.jpg) repeat-x;font-weight:bold;" >
    <tr style="color:#fff">
    <td width=40px>选择
    </td>
    <td width=40px >标识
    </td>
    <td width=40px >序号
    </td>
    <td width=160px>栏目名称
    </td>
    <td width=140px>导航栏显示
    </td>
    <td width=90px>所属模块
    </td>
    <td width=130px>目录名称
    </td>
    <td width=200px>操作
    </td>
    </tr>
    </thead>
    <%=strColumn %>
<%--    <tr>
    <td><input name="CkT" type="checkbox" /></td>
    <td ><input  name ="TOrT" type="text" class=or  maxlength=3  /></td>
    <td><input name="TNmT" id="TNmT" type="text"  class=or1 /></td>
    <td>
        <select name="SelT1" >
            <option  value=0 >不显示</option>
            <option  value=1 >头部主导航条</option>
            <option  value=2 >尾部导航条</option>
            <option  value=3 >都显示</option>
       </select>
    </td><td>所属模块</td>
    <td>目录名称</td>
    <td align=left ><a href =javascript:Save(Num,id )  >保存</a>&nbsp;&nbsp;<a href =ColumnEdit.aspx?id=1 >编辑</a>&nbsp;&nbsp;<%=strClass1NumInfo %>
    <a href =javascript:AddSub(Num,id ) >添加子栏目</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:Del(id ) >删除</a>
    </td>
    </tr>
--%> 
   <tr>
    <td></td>
    <td colspan =2 align=left  id=Add1 ><a href ="javascript:AddColumn(-1,0,0,0,0,<%=strMaxIDMark %>,<%=strMsg %>,<%=strZhP %>,1,<%=strClass1NumInfo %>);" >+添加一级栏目</a> </td>
   </tr>
  </table>

        <asp:Button ID="BSave1" runat="server" Text="BC" onclick="BSave1_Click" CssClass="ND" /><asp:HiddenField ID="HdSave" runat="server" />
        <asp:Button ID="BSaveN" runat="server" Text="BC" onclick="BSaveN_Click" CssClass="ND" /><asp:HiddenField ID="HdSaveN" runat="server" />
        <asp:Button ID="BDel" runat="server" Text="BC" onclick="BDel_Click" CssClass="ND" /><asp:HiddenField ID="HdDel" runat="server" />
<br />
        <asp:Button ID="BSaveA" runat="server" Text=" 保 存 " onclick="BSave_Click" OnClientClick ="return SaveAll();" /><asp:HiddenField ID="HdSaveID" runat="server" />
    <%--    <asp:Button ID="Button2" runat="server" Text="完成" onclick="Button2_Click" />--%>
    </div>
    </form>
   <%=strAlertJS %>
</body>
</html>
