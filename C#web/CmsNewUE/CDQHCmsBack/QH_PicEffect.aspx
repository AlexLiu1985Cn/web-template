<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_PicEffect.aspx.cs" Inherits="CmsApp20.CmsBack.QH_PicEffect" %>

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
    <script language="javascript">
        function CheckPicWH() {
            var vID = ["imgP_x", "imgP_y", "img_x", "img_y"];
            var vMsg = ["产品内容页图片", "图片内容页图片"];
            for (var i = 0; i < 4; i++) {
                if (!(/^$|^[1-9][0-9]*$/.test($(vID[i]).value))) {
                    var vWH = i % 2 == 0 ? "宽度" : "高度";
                    alert(vMsg[Math.floor(i / 2)] + vWH + "应为数字！");
                    return false;
                }
            }
            return true;
        }
    </script>
</head>
<body style="f">
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：图片设置 >> <a href="QH_PicEffect.aspx">图片显示效果</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    <table width="820px" align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
        <tr style="height: 30px;">
            <td width="200px" align="right">
                产品内容页图片样式：
            </td>
            <td align="left">
                <select name="picStyle" id="picStyle" runat="server">
                    <option value='0'>多图放大镜效果</option>
                    <option value='1'>多图普通切换</option>
                    <option value='4'>单张图片放大镜效果</option>
                    <option value='5'>单张图片放大效果</option>
                    <option value='2'>单张图片</option>
                    <option value='3'>不设置</option>
                </select>
                &nbsp;&nbsp;<input name="imgP_x" id="imgP_x" type="text" size="6" maxlength="3" runat="server" />×
                <input name="imgP_y" id="imgP_y" type="text" size="6" maxlength="3" runat="server" /><span
                    class="tips">(宽 × 高)(像素)</span>
            </td>
        </tr>
        <tr style="height: 30px;">
            <td align="right">
                图片内容页图片样式：
            </td>
            <td align="left">
                <select name="ImgStyle" id="ImgStyle" runat="server">
                    <option value='0'>多图切换</option>
                    <option value='1'>单张图片</option>
                    <option value='2'>不设置</option>
                </select>
                &nbsp;&nbsp;<input name="img_x" id="img_x" type="text" size="6" maxlength="3" runat="server" />×
                <input name="img_y" id="img_y" type="text" size="6" maxlength="3" runat="server" /><span
                    class="tips">(宽 × 高)(像素)</span>
            </td>
        </tr>
        <tr style="height: 30px;">
            <td align="right">
                图片列表页样式：
            </td>
            <td align="left">
                <select name="ImgListStyle" id="ImgListStyle" runat="server">
                    <option value='0'>列表</option>
                    <option value='1'>瀑布流</option>
                </select>
            </td>
        </tr>
        <tr style="height: 60px;">
            <td>
            </td>
            <td align="left">
                <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " OnClick="BtnSave_Click" OnClientClick="return CheckPicWH();" /><%--<asp:Button ID="Button1" runat="server" Text=" 返 回 " onclick="Button1_Click"  CssClass="ret"/>--%>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
