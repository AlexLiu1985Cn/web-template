<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_DataBackup.aspx.cs" Inherits="CmsApp20.CmsBack.QH_DataBackup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
 <style type="text/css">
        body{ font-family :Microsoft Yahei;font-size:12px;}
        .tips{color:#999999; }
        .ret{ margin-left :160px;}
        .now{font-weight:bold;	color:#fff;}
        div a{text-decoration:none;color:#555;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
<%--        <div class="now" style ="width :100px; height :30px; margin-left :140px; background-color:#1F75B7 ; text-align:center;  line-height:30px;float:left;" >数据库备份</div>
        <div style ="width :100px; height :30px; margin-left :140px;  text-align:center;  line-height:30px; "><a href ="QH_DataRestore.aspx">数据恢复</a></div>
        <div style =" clear :both ; margin-left :140px; border-top: solid 1px #555;"><br /></div>
--%>
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：系统设置 >> <a href="QH_DataBackup.aspx">数据备份及还原</a></div>
<div class="rightmain1">二级位置：数据备份 - <a href="QH_DataRestore.aspx">数据还原</a></div>
<div class="rightmain1"> </div>
    
        <table width =820px align="center" cellpadding="2" cellspacing="2">
    <tr>
			<td align=right>数据库备份：</td>
            <td align=left>
                <asp:Button ID="BtnBak" runat="server" Text="   备份    " 
                    onclick="BtnBak_Click" /><span class="tips"> 备份网站添加的内容和网站后台所有的设置</span>
				</td>
          </tr> 
		<tr>
			<td align=right>数据库压缩：</td>
            <td align=left>
                <asp:Button ID="BtnComP" runat="server" Text="   压缩    " 
                    onclick="BtnComP_Click" /><span class="tips"> 当前数据库文件大小：<%=strFileSize %></span>
				</td>
          </tr> 
         <tr> 
		    <td align=right></td>
            <td align=left style="color:Green;"></td> 
        </tr>
         <tr> 
		    <td align=right></td>
            <td align=left style="color:Green;">说明：
 				<ol>
					<li>建议每月至少备份一次数据库</li>
					<li>压缩数据库可显著降低数据库文件大小</li>
				</ol>           
            </td> 
        </tr>
     </table>

 
</div>
</div>
    </form>
</body>
</html>
