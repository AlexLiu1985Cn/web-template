<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_StatisticsSet.aspx.cs"
    Inherits="CmsApp20.CmsBack.QH_StatisticsSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    </style>
</head>
<script type="text/javascript">
    $ = function (id) { return document.getElementById(id); }
    String.prototype.Trim = function () {
        return this.replace(/(^\s*)|(\s*$)/g, '');
    }
    function CheckMax() {
        if (!/^[0-9]+$/.test($("max").value.Trim())) {
            alert("每日访问最大值必须为数字！"); return false;
        }
        var JsCtn = $("JScriptStat3");
        JsCtn.value = JsCtn.value.replace(/</g, '&lt;').replace(/>/g, '&gt;');
        JsCtn.style.color = "#ffffff";
        return true;
    }
</script>
<body>
    <form id="form1" runat="server">
    <%--    <h5 style="margin-left:200px;">统计设置</h5>--%>
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：访问统计 >> <a href="QH_StatisticsSet.aspx">统计设置</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
        </div>
    </div>
    <table width="820px" align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
        <tr>
            <td align="right">
                访问统计功能：
            </td>
            <td align="left">
                <input type="radio" name="stat" value="1" id="Open" runat="server" />开启&nbsp;&nbsp;
                <input type="radio" name="stat" value="0" id="Close" runat="server" />关闭 &nbsp;&nbsp;<span
                    style="color: #999;"> 关闭后不再记录来访信息</span>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td align="left">
                <br />
                <font color="green">下面为自动清空方式设置(统计数据会占用一定的数据库大小)，建议使用默认配置</font>
            </td>
        </tr>
        <tr>
            <td width="200px" align="right">
                统计数据：
            </td>
            <td align="left">
                <select name="SaveData" id="SaveData" runat="server">
                    <option value="1">仅保留当天</option>
                    <option value="2">保留近七天</option>
                    <option value="3">保留近一个月</option>
                    <option value="4">保留近一年</option>
                </select>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td align="left">
                <br />
                <font color="green">下面为清空统计数据功能设置，请谨慎操作，清空后无法恢复数据。</font>
            </td>
        </tr>
        <tr id="titletd" runat="server">
            <td align="right">
                清空统计数据：
            </td>
            <td align="left">
                <asp:LinkButton ID="LBtn1" runat="server" ToolTip="一键清空所有统计数据" OnClientClick="var c=confirm('确定要清空今日以前所有数据吗？');if(c) $('JScriptStat3').value=''; return c;"
                    OnClick="LBtn1_Click"><font color=red >清空所有数据</font></asp:LinkButton>
                <span style="color: #999;">清空今日以前所有数据</span>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td align="left">
                <br />
                <font color="green">下面为安全设置</font>
            </td>
        </tr>
        <tr id="LinkUrl" runat="server">
            <td align="right">
                每日访问最大值：
            </td>
            <td align="left">
                <input type="text" name="max" id="max" runat="server" maxlength="6" />&nbsp;PV <span
                    class="tips">为防止恶意攻击，超出后不再记录来访信息</span>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td align="left">
                <br />
                <font color="green">下面是第三方统计代码设置</font>
            </td>
        </tr>
        <tr>
            <td align="right">
                第三方统计代码：
            </td>
            <td align="left">
                <textarea name="JScriptStat3" id="JScriptStat3" cols="60" rows="6"><%=strJScriptStat3%></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                第三方统计代码开关：
            </td>
            <td align="left">
                <input type="radio" name="stat3" value="1" id="stat3On" runat="server" />开启&nbsp;&nbsp;
                <input type="radio" name="stat3" value="0" id="stat3Off" runat="server" />关闭 &nbsp;&nbsp;<span
                    style="color: #999;"> 关闭后第三方统计代码被屏蔽（需重新生成静态页）。</span>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td align="left">
                <br />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="left">
                <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " OnClick="BtnSave_Click" OnClientClick="return CheckMax();" />
                <asp:Button ID="BtnReset" runat="server" Text=" 重 置 " CssClass="ret" OnClick="BtnReset_Click"
                    OnClientClick="$('JScriptStat3').value=''; return true;" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
