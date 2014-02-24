<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_SafeSet.aspx.cs" Inherits="CmsApp20.CmsBack.QH_SafeSet" %>

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
    </style>
</head>
<script type="text/jscript">
    $ = function (id) { return document.getElementById(id); }
    String.prototype.Trim = function () {
        return this.replace(/(^\s*)|(\s*$)/g, '');
    }
    function SafeCheck() {
        if (!/^[0-9]+$/.test($("file_max").value.Trim())) {
            alert("文件上传最大值必须为整数数字！"); return false;
        }
        if (!/^[0-9]+$/.test($("Img_max").value.Trim())) {
            alert("图片上传最大值必须为整数数字！"); return false;
        }
        if (!/^[0-9]+$/.test($("AdminSsnT").value.Trim())) {
            alert("后台重新登录时间必须为数字！"); return false;
        }
    }
</script>
<body>
    <form id="form1" runat="server">
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：系统设置 >> <a href="QH_SafeSet.aspx">安全信息设置</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
            <table width="840px" align="center" cellpadding="2" cellspacing="2">
                <tr id="trDel" runat="server">
                    <td width="200px" align="right">
                        删除安装文件：
                    </td>
                    <td align="left">
                        <%=strDelTip %>
                        <asp:LinkButton ID="LB1" runat="server" OnClick="LB1_Click">删除</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        后台文件夹名称：
                    </td>
                    <td align="left">
                        <input name="AdminDir" type="text" value="<%=strAdminDir%>" maxlength="60" size="10"
                            disabled /><span class="tips">仅创始人可修改，当前后台网址：<%=strDomian %></span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        管理员登录验证码：
                    </td>
                    <td align="left">
                        <input type="radio" name="ChkCode" value="1" <%=strChkOn %> />开启&nbsp;&nbsp;
                        <input type="radio" name="ChkCode" value="0" <%=strChkOff %> />关闭
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        图片上传最大值：
                    </td>
                    <td align="left">
                        <input name="Img_max" id="Img_max" type="text" maxlength="5" size="5" value="<%=strImgMax %>" /><span
                            class="tips">Kb</span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        文件上传最大值：
                    </td>
                    <td align="left">
                        <input name="file_max" id="file_max" type="text" maxlength="5" size="5" value="<%=strFileMax %>" /><span
                            class="tips">Kb</span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        后台重新登录时间：
                    </td>
                    <td align="left">
                        <input name="AdminSsnT" id="AdminSsnT" type="text" maxlength="3" size="10" value="<%=strAdminSsnT%>" /><span
                            class="tips">分钟 设为0取消后台重新登录</span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        允许上传的文件格式：
                    </td>
                    <td align="left">
                        <textarea name="FileExt" cols="50" rows="4" onkeydown="checklength(this,200);" onblur="maxtext(this,200)"><%=strFileExt%></textarea><span
                            class="tips">多种格式请用“|”隔开</span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        网站屏蔽版权保护：
                    </td>
                    <td align="left">
                        <input type="radio" name="AntiDown" value="1" <%=strAntiOn %> />开启&nbsp;&nbsp;
                        <input type="radio" name="AntiDown" value="0" <%=strAntiOff %> />关闭
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " OnClick="BtnSave_Click" OnClientClick="return SafeCheck();" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        安全建议:
                        <ol>
                            <li>修改网站后台文件夹名称（默认为cdqhCmsBack）;<%--同时查看配置文件Web.Config中FCKEditor路径是否正确。--%></li>
                        </ol>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
