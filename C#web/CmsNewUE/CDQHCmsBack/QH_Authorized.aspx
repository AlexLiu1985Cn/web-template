<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_Authorized.aspx.cs"
    Inherits="CmsApp20.CmsBack.QH_Authorized" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 200px 10px 10px 200px;">
        本机网址及非正式域名不能授权。<br />
        授权请登录网站<a href="http://www.95c.com.cn" target="_blank">www.95c.com.cn</a><br />
        请输入授权码：<br />
        <asp:TextBox ID="TbAuthorCode" runat="server" MaxLength="64" Columns="80"></asp:TextBox>&nbsp;&nbsp;<asp:Button
            ID="Button1" runat="server" Text="确定" OnClick="Button1_Click" />
        <br />
        <br />
        注：访问官网 95c.com.cn 索取授权码<br />
        &nbsp;&nbsp;&nbsp;&nbsp;通过正常渠道授权码才有效</div>
    </form>
</body>
</html>
