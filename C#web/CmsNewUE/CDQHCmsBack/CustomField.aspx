<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomField.aspx.cs" Inherits="CmsApp.CmsBack.CustomField" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
 <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
    <script type ="text/javascript"  src ="js/AddColumn.js"></script> 
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
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：内容管理 >><%strMdl = Server.HtmlEncode(strMdl); %> <a href="CustomField.aspx?Mdl=<%=strMdl %>"><%=strMdlName %>字段设置</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>
</div>
</div>
    <div style ="text-align :center ">
    <table width =840px id=table1 cellpadding="4" cellspacing="2"  align=center  >
    <thead style="background:url(images/1.jpg) repeat-x;font-weight:bold;"  >
    <tr style="color:#fff">
    <td width=40px>选择
    </td>
    <td width=40px >标识
    </td>
    <td width=40px >排序
    </td>
    <td width=140px>字段名称
    </td>
    <td width=170px>所属栏目
    </td>
    <td width=70px>访问权限
    </td>
    <td width=80px>字段类型
    </td>
    <td width=80px>是否必填
    </td>
    <td width=240px>操作
    </td>
    </tr>
    </thead>
    <%=strField %>
<%--    <tr>
    <td><input name="CkT" type="checkbox" /></td>
    <td ><input  name ="TOrT" type="text" class=or  maxlength=3  /></td>
    <td><input name="TNmT" id="TNmT" type="text"  class=or1 /></td>
    <td>
        <select name="SelT1" >
            <option  value=0 >所有栏目</option>
            <option  value=1 >头部主导航条</option>
            <option  value=2 >尾部导航条</option>
            <option  value=3 >都显示</option>
       </select>
    </td>
    <td><select name="access" >
        <option value='0' >不限</option>
        <option value='1' >普通会员</option>
        <option value='2' >高级会员</option>
        <option value='3' >管理员</option></select>
       </td>
    <td><select name="Type" >
        <option value='0' >简短</option>
        <option value='1' >下拉</option>
        <option value='2' >文本</option>
        <option value='3' >多选</option>
        <option value='4' >单选</option>
        </select>
    </td>
    <td><input type="checkbox" name=wr_ok_1 value=1 checked='checked'/></td>
    <td align=left ><a href =javascript:Save(Num,id )  >保存</a>&nbsp;&nbsp;<a href =ColumnEdit.aspx?id=1 >设置选项 </a>&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:Del(id ) >删除</a>
    </td>
    </tr>--%>
   <tr>
    <td></td>
    <td colspan =2 align=left  id=Add1 ><a href =javascript:AddField(<%=strMdl %>) >+添加新字段</a> </td>
    </tr>
  </table>

        <asp:Button ID="BSave1" runat="server" Text="BC" onclick="BSave1_Click" CssClass="ND" /><asp:HiddenField ID="HdSave" runat="server" />
        <asp:Button ID="BSaveN" runat="server" Text="BC" onclick="BSaveN_Click" CssClass="ND" /><asp:HiddenField ID="HdSaveN" runat="server" />
        <asp:Button ID="BDel" runat="server" Text="BC" onclick="BDel_Click" CssClass="ND" /><asp:HiddenField ID="HdDel" runat="server" />
<br />
        <asp:Button ID="BSaveA" runat="server" Text=" 保 存 " onclick="BSave_Click" OnClientClick ="return FSaveAll();" /><asp:HiddenField ID="HdSaveID" runat="server" />
    </div>
  
   </form>
</body>
</html>
