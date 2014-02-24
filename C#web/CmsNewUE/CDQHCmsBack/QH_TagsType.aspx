<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_TagsType.aspx.cs" Inherits="QH_TagsType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cms网站管理系统</title>
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
<STYLE type=text/css>BODY {
	MARGIN-TOP: 0px; FONT-SIZE: 12px; }
</STYLE>
</head>
<script language="JavaScript" type="text/JavaScript">
function ConfirmDelBig()
{
   if(confirm("确定要删除此标签吗？"))
     return true;
   else
     return false;
	 
}

</script>
<body>
 <div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：其它设置 >> 标签管理</div>
<div class="rightmain1">二级位置：<%=strTagsType %></div>
<div class="rightmain1"> </div>
</div>
</div> 
   <form id="form1" runat="server">
    <div>
    <!-- <div style="text-align:center"><a href="QH_News_AddBig.aspx" ><strong><font color="#FF0000"><u>添加标签类别</u></font></strong></a></div> -->
     <br />
    <div style="text-align:center" id="CategoryTable" runat="server">
    </div>
    
    </div>
    
    <div style="text-align:center" id="NewsTypeTable" runat="server">
   
    </div>
     <br />
     <br />
      <div style="text-align:center"><a href="QH_TagsAdd.aspx?Mdl=<%=strMdl %>" ><strong><font color="#FF0000"><u><%=strMdlName %></u></font></strong></a></div> 
 
    
    </form>
</body>
</html>
