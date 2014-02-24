<%@ Page Language="C#" AutoEventWireup="True" Inherits="Admin_Login" CodeBehind="Admin_Login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>创都启航网站管理系统管理员登陆</title>
    <link href="LoginCssImage/css/style2.css" type="text/css" rel="Stylesheet" />
</head>
<script language="javascript" type="text/javascript">
    function getimgcode() {
        var getimagecode = document.getElementById("getcode");
        getimagecode.src = "../Ajax/VerifyCode.aspx?" + Math.random();
    }
</script>
<body>
    <div class="top">
        <div class="topleft">
        </div>
        <div class="topcen">
            <div class="topcen1">
            </div>
            <div class="topcen2">
                <form id="loginform" runat="server">
                <img src="LoginCssImage/image/mingcheng.gif" alt="用户登录" class="name1">
                <div class="clear">
                </div>
                <div id="loginBox">
                    <ul>
                        <li><span>登录账号： </span>
                            <asp:TextBox ID="UserName" runat="server" CssClass="txtSty"></asp:TextBox></li>
                        <li><span>登录密码： </span>
                            <asp:TextBox ID="UserPwd" runat="server" CssClass="txtSty" TextMode="Password"></asp:TextBox></li>
                        <li id="Lang" runat="server" style="display: none;"><span>语言版本： </span>
                            <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </li>
                        <li id="ChkCode" runat="server"><span>验证码： </span>
                            <asp:TextBox ID="CheckCode" runat="server" CssClass="txtSty1" />
                            <a href="javascript:getimgcode()">
                                <img src="../Ajax/VerifyCode.aspx" id="getcode" style="border: 1px #919a99 solid;
                                    width: 50px; height: 23px;" alt="看不清，点击换一张"></a> </li>
                    </ul>
                </div>
                <asp:Button ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="loginBtn"
                    Text=" 登 录 " />
                <div class="clear">
                </div>
                </form>
                <div class="zi">
                    <a href="http://www.95c.com.cn">北京创都启航网络科技有限公司</a></div>
                <div class="zi">
                    <a href="http://www.95c.com.cn">www.95c.com.cn</a></div>
                <br />
            </div>
            <div class="clear">
            </div>
            <div class="topcen3">
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="topright">
        </div>
        <div class="clear">
        </div>
        <div class="topbottom">
        </div>
        <div class="clear">
        </div>
    </div>
</body>
</html>
