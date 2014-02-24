<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_SiteMap.aspx.cs" Inherits="CmsApp20.CmsBack.QH_SiteMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 200px; text-align: center; padding-top: 170px;">
        二级目录：<asp:TextBox ID="TBRootDir" runat="server"></asp:TextBox>（若网站放在根目录下则不需填写）<br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text=" 生成网站地图 " OnClick="Button1_Click" Height="41px"
            Width="130px" />
    </div>
    </form>
</body>
</html>
