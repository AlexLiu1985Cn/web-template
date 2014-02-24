<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateQRCode.aspx.cs" Inherits="CmsApp20.CmsBack.CreateQRCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
    <script type="text/javascript" src="js/CmsBack.js"></script>
    <style type="text/css">
        body
        {
            font-family: Microsoft Yahei;
            font-size: 12px;
        }
        .tips
        {
            color: #999999;
        }
        .ret
        {
            margin-left: 160px;
        }
        .ND
        {
            display: none;
        }
        .style1
        {
            height: 44px;
        }
    </style>
</head>
<script type="text/javascript">
    function UpLogo(fileID) {
        if (CheckImg(fileID) == 1) return;
        $("BtnUpLogo").click();
    }
    function CheckDataEmpty() {
        if ($("QRCodeData").value.Trim() == "") { alert("二维码内容不能为空！"); return false; }
        return true;
    }
</script>
<body>
    <form id="form1" runat="server">
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：手机二维码管理 >> <a href="CreateQRCode.aspx">生成二维码</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
            <table width="820px" align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
                <tr>
                    <td align="right">
                        二维码Logo：
                    </td>
                    <td align="left">
                        <asp:Image ID="ImageLogo" runat="server" Width="30" Height="30" /><%=strSpace %><input
                            name="LogoUrl" type="text" style="width: 300px;" value="<%=strLogoUrl%>" maxlength="200" />
                        <input type="file" id="myFile" runat="server" onchange="UpLogo('myFile')" style="width: 60px;
                            border: 0px White;" /><span class="tips">可选，可不添</span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        二维码图大小：
                    </td>
                    <td align="left">
                        <input name="QRCodeSize" id="QRCodeSize" type="text" size="4" maxlength="2" value="5"
                            runat="server">
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style1">
                        二维码内容：
                    </td>
                    <td align="left" class="style1">
                        <input name="QRCodeData" id="QRCodeData" type="text" size="40" maxlength="200" runat="server">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 100px; text-align: center;">
                        <asp:Image ID="ImageQRCode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        <asp:Button ID="BtnCreate" runat="server" Text=" 生 成 " OnClick="BtnCreate_Click"
                            OnClientClick="return CheckDataEmpty();" /><asp:Button ID="BtnDown" runat="server"
                                Text=" 下 载 " OnClick="BtnDown_Click" CssClass="ret" />
                        <asp:Button ID="BtnDecode" runat="server" Text=" 解 码 " OnClick="BtnDecode_Click"
                            CssClass="ret" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:Button ID="BtnUpLogo" runat="server" Text="" CssClass="ND" OnClick="BtnUpLogo_Click" />
    </form>
</body>
</html>
