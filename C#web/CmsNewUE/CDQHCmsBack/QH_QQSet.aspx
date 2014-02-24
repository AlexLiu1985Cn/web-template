<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_QQSet.aspx.cs" Inherits="CmsApp20.CmsBack.QH_QQSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
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
        .now
        {
            font-weight: bold;
            color: #fff;
        }
        div a
        {
            text-decoration: none;
            color: #555;
        }
        .or
        {
            width: 46px;
        }
        .Cln
        {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：常规管理 >> <a href="QH_QQSet.aspx">在线客服设置</a></div>
            <div class="rightmain1">
                二级位置：在线客服设置 - <a href="QH_QQManage.aspx">在线客服管理</a></div>
            <div class="rightmain1">
            </div>
            <table width="820px" align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
                <tr>
                    <td width="200px" align="right">
                        在线客服开关：
                    </td>
                    <td align="left">
                        <input type="radio" name="QQOn" value="1" <%=strQQOn %> />开启
                        <input type="radio" name="QQOn" value="0" <%=strQQOff %> />关闭&nbsp;&nbsp;&nbsp;&nbsp;<span
                            class="tips">（修改后不须重新生成静态页）</span>
                    </td>
                </tr>
                <tr>
                    <td width="200px" align="right">
                        在线客服样式：
                    </td>
                    <td align="left">
                        <select name="QQStyle" id="QQStyle" runat="server">
                            <option value="0">关闭</option>
                            <option value="1" style="color: #FF0000">样式一</option>
                            <option value="2" style="color: #339933">样式二</option>
                        </select>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " OnClick="BtnSave_Click" />
                        <asp:Button ID="Button1" runat="server" Text=" 重 置 " CssClass="ret" OnClick="BtnReset_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
