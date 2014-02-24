<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_User_Login" CodeBehind="Admin_User_Login.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>管理员登陆</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta http-equiv="Expires" content="0">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Pragma" content="no-cache">
    <link href="../Images/myweb.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="js/NavFrame.js"></script>
    <style type="text/css">
        TD
        {
            font-size: 12px;
            color: #000000;
            line-height: 17px;
            font-family: "宋体";
        }
    </style>
    <meta content="MSHTML 6.00.2800.1106" name="GENERATOR">
</head>
<body bgcolor="#ffffff" leftmargin="0" topmargin="0" marginheight="0" marginwidth="0">
    <form id="form1" runat="server">
    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tbody>
            <tr>
                <td valign="bottom" align="middle" bgcolor="#ffffff">
                    <table cellspacing="0" cellpadding="0" width="500" align="center" border="0">
                        <tbody>
                            <tr height="80px">
                                <td width="490px" align="center">
                                    &nbsp;请输入用户名和密码
                                </td>
                                <td width="10">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" bgcolor="#ffffff">
                    <table cellspacing="0" cellpadding="0" width="500" align="center" border="0">
                        <tbody>
                            <tr>
                                <td width="108">
                                </td>
                                <td width="352">
                                    <table cellspacing="0" cellpadding="4" width="282" align="center" border="0">
                                        <tbody>
                                            <tr>
                                                <td align="right" style="width: 60px; height: 30px;">
                                                    <font face="Verdana, Arial, Helvetica, sans-serif">用户名:</font>
                                                </td>
                                                <td style="width: 140px; height: 30px;">
                                                    <asp:TextBox ID="UserName" runat="server" CssClass="web_ipt" Width="98px" Height="21"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                        ControlToValidate="UserName"></asp:RequiredFieldValidator>
                                                </td>
                                                <td height="30" rowspan="2" style="width: 100px;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap align="right" style="width: 60px">
                                                    <font face="Verdana, Arial, Helvetica, sans-serif">密&nbsp;&nbsp;&nbsp;码:</font>
                                                </td>
                                                <td style="width: 124px">
                                                    <asp:TextBox ID="UserPwd" runat="server" CssClass="web_ipt" Width="98px" TextMode="Password"
                                                        Height="21px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                        ControlToValidate="UserPwd"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr align="middle">
                                                <td colspan="2" height="70">
                                                    <asp:Button ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" Text="确定"
                                                        Height="26px" Width="62px" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="2">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
