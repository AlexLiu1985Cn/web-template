<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_OptimizationBak.aspx.cs"
    Inherits="CmsApp20.CmsBack.QH_OptimizationBak" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
<script type="text/javascript">
    $ = function (id) { return document.getElementById(id); }
    String.prototype.Trim = function () {
        return this.replace(/(^\s*)|(\s*$)/g, '');
    }
    function htmlEncode1() {
        for (var i = 1; i < 4; i++) {
            var Ctn = $("Bak" + i);
            Ctn.value = Ctn.value.replace(/</g, '&lt;').replace(/>/g, '&gt;');
            Ctn.style.color = "#ffffff";
            //alert(value);
        }
        return true;
    }
</script>
<body>
    <form id="form1" runat="server">
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：优化设置 >> <a href="QH_OptimizationBak.aspx">备用字段设置</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
            <table width="820px" align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
                <tr>
                    <td align="right">
                        备用字段1：
                    </td>
                    <td align="left">
                        <input name="Bak1" type="text" id="Bak1" size="76" maxlength="255" runat="server">
                    </td>
                    <%--value="<%=strBak1%>"--%>
                </tr>
                <tr>
                    <td align="right">
                        备用字段2：
                    </td>
                    <td align="left">
                        <input name="Bak2" type="text" id="Bak2" size="76" maxlength="255" runat="server">
                    </td>
                    <%--value="<%=strBak2%>"--%>
                </tr>
                <tr>
                    <td align="right">
                        备用字段3：
                    </td>
                    <td align="left">
                        <input name="Bak3" type="text" id="Bak3" size="76" maxlength="255" runat="server">
                    </td>
                    <%--value="<%=strBak3%>"--%>
                </tr>
                <tr>
                    <td align="right">
                        备用图片地址1：
                    </td>
                    <td align="left">
                        <input name="BakImg1" type="text" id="BakImg1" value="<%=strBakImg1%>" size="40"
                            maxlength="255"><input id="ImgFile1" type="file" onchange="CheckImg('ImgFile1')"
                                size="26" runat="server" name="ImgFile1" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        备用图片地址2：
                    </td>
                    <td align="left">
                        <input name="BakImg2" type="text" id="BakImg2" value="<%=strBakImg2%>" size="40"
                            maxlength="255"><input id="ImgFile2" type="file" onchange="CheckImg('ImgFile2')"
                                size="26" runat="server" name="ImgFile2" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        备用图片地址3：
                    </td>
                    <td align="left">
                        <input name="BakImg3" type="text" id="BakImg3" value="<%=strBakImg3%>" size="40"
                            maxlength="255"><input id="ImgFile3" type="file" onchange="CheckImg('ImgFile3')"
                                size="26" runat="server" name="ImgFile2" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " OnClick="BtnSave_Click" OnClientClick="return htmlEncode1();" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
