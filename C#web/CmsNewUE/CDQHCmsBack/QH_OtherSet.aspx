<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="QH_OtherSet.aspx.cs" Inherits="CmsApp20.CmsBack.QH_OtherSet" %>

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
    <script type="text/javascript" src="ueditor/ueditor.config.js"></script>
    <script type="text/javascript" src="ueditor/ueditor.all.min.js"></script>
</head>
<script language="javascript">
    $ = function (id) { return document.getElementById(id); }
    function htmlEncode1() {
        var JsCtn = $("JScript");
        JsCtn.value = JsCtn.value.replace(/</g, '&lt;').replace(/>/g, '&gt;');
        JsCtn.style.color = "#ffffff";
        var vErr = ["", "第一行", "第二行", "第三行", "底部其它信息"];
        for (var i = 1; i <= 4; i++) {
            var Ctn = $("Bottom" + (i == 4 ? "QT" : i));
            if (Ctn.value.indexOf('<') >= 0 || Ctn.value.indexOf('>') >= 0) {
                alert(vErr[i] + "内容不能包含html标签！"); return false;
            }
            //Ctn.value=Ctn.value.replace(/</g,'&lt;').replace(/>/g,'&gt;');
            //Ctn.style.color="#ffffff";
            //alert(value);
        }
        return true;
    }
</script>
<body style="f">
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：系统设置 >> <a href="QH_OtherSet.aspx">页面设置</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    <table width="820px" align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
        <tr>
            <td width="200px" align="right">
                首页内容：
            </td>
            <td align="left">
                <textarea id="TxtContent" name="TxtContent" runat="server" style="width: 500px; height: 160px;"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td align="left" style="color: Green;">
                底部信息：
            </td>
        </tr>
        <tr>
            <td align="right">
                第一行：
            </td>
            <td align="left">
                <input name="Bottom1" type="text" size="40" maxlength="200" value="<%=strBottom1%>">
            </td>
        </tr>
        <tr>
            <td align="right">
                第二行：
            </td>
            <td align="left">
                <input name="Bottom2" type="text" size="40" maxlength="200" value="<%=strBottom2%>">
            </td>
        </tr>
        <tr>
            <td align="right">
                第三行：
            </td>
            <td align="left">
                <input name="Bottom3" type="text" size="40" maxlength="200" value="<%=strBottom3%>">
            </td>
        </tr>
        <tr>
            <td align="right">
                底部其它信息：
            </td>
            <td align="left">
                <input name="BottomQT" type="text" size="40" maxlength="200" value="<%=strBottomQT%>">
            </td>
        </tr>
        <tr>
            <td align="right">
                第三方Js代码：
            </td>
            <td align="left">
                <textarea name="JScript" id="JScript" cols="60" rows="6"><%=strJScript%></textarea>
            </td>
        </tr>
        <tr style="height: 60px;">
            <td>
            </td>
            <td align="left">
                <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " OnClick="BtnSave_Click" OnClientClick="UEHtmlEncode();return htmlEncode1();" /><%--<asp:Button ID="Button1" runat="server" Text=" 返 回 " onclick="Button1_Click"  CssClass="ret"/>--%>
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
        editor = new baidu.editor.ui.Editor({
            toolbars: [['fullscreen', 'source', 'undo', 'redo',
                'bold', 'italic', 'strikethrough', 'pasteplain', 'forecolor', 'insertorderedlist', 'insertunorderedlist',
                'lineheight', 'link', 'unlink',
                  'imagenone', 'imageleft', 'imageright',
                 'fontfamily', 'fontsize',
                'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify',
                'insertimage', 'insertvideo', 'music', 'attachment', 'map', 'gmap',
                'horizontal', 'date', 'time']]
        });
        editor.render("TxtContent");
        function UEHtmlEncode() {
            var Ctn = $("TxtContent");
            Ctn.value = Ctn.value.replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/&#39;/g, "#39;").replace(/\'/g, "#39;");
        }
    </script>
</body>
</html>
