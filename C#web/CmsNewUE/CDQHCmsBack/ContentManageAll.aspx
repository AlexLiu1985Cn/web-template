<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentManageAll.aspx.cs" Inherits="CmsApp20.CmsBack.ContentManageAll" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
 <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
    <style type="text/css">
        body
        {
            font-family: Microsoft Yahei;
            font-size: 12px;
            line-height :21px ;
        }
 		.hf1 a { font-size:14px; font-weight:bold; }
		.hf2 a { font-size:12px; font-weight:bold; }
		.hf3 a { font-size:12px; font-weight: normal; }
   </style>
</head>
<script type ="text/javascript" >
$=function(id) {return document .getElementById (id);}
</script>
<body>
    <div style="margin-left: 100px; margin-top :20px; ">
    <%=strColumn%>
    </div>
    <form id="form1" runat =server ></form>
</body>
</html>
