<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateMng_Mobile.aspx.cs"
    Inherits="CmsApp20.CmsBack.TemplateMng_Mobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
    <script type="text/javascript">
        $ = function (id) { return document.getElementById(id); }
        String.prototype.Trim = function () {
            return this.replace(/(^\s*)|(\s*$)/g, '');
        }
        var regD = /^\$$|^[0-9]{1,3}$/;
        function CheckNum() {
            for (var Num = 0; Num < parseInt($("HdnNumList").value); Num++) {
                if (!/^\$$|^\d+$/.test($("IDM" + Num).value.replace(/\|/g, ""))) {
                    alert("列表显示第" + (Num + 1) + "行标识必须为数字！如2|4"); return false;
                }
                if (!regD.test($("Count" + Num).value.Trim())) {
                    alert("列表显示第" + (Num + 1) + "行显示条数必须为数字！"); return false;
                }
            }
            return true;
        }
        function CheckNumLM() {
            for (var Num = 0; Num < parseInt($("HdnNumLM").value); Num++) {
                if (!regD.test($("IDMLM" + Num).value.Trim())) {
                    alert("栏目显示第" + (Num + 1) + "行标识必须为数字！"); return false;
                }
            }
            return true;
        }
        function CheckNumClass() {
            for (var Num = 0; Num < parseInt($("HdnNumClass").value); Num++) {
                if (!regD.test($("IDMCL" + Num).value.Trim())) {
                    alert("分类列表显示第" + (Num + 1) + "行标识必须为数字！"); return false;
                }
            }
            return true;
        }
    </script>
    <style type="text/css">
        body
        {
            font-family: Microsoft Yahei;
            font-size: 12px;
        }
        .or
        {
            width: 30px;
        }
        .or1
        {
            width: 80px;
        }
        .BtnS
        {
            margin-left: 100px;
        }
    </style>
</head>
<body>
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：页面风格 >> <a href="TemplateMng_Mobile.aspx?Mdl=0">模块模板设置</a></div>
            <div class="rightmain1">
                二级位置：<%=strTemplate %></div>
            <div class="rightmain1">
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    <div style="margin: 20px 100px 0px 100px;">
        <%=strMdlFName %><br />
        <br />
        列表显示：<br />
        <%=strList %>
        <br />
        <asp:HiddenField ID="HdnNumList" runat="server" />
        <asp:Button ID="BtnSaveList" runat="server" Text="保存列表修改" OnClick="BtnSaveList_Click"
            CssClass="BtnS" OnClientClick="return CheckNum();" />
        <br />
        <br />
        栏目显示：<br />
        <%=strLanmu %>
        <br />
        <asp:HiddenField ID="HdnNumLM" runat="server" />
        <asp:Button ID="BtnSaveLM" runat="server" Text="保存栏目修改" OnClick="BtnSaveLM_Click"
            CssClass="BtnS" OnClientClick="return CheckNumLM();" />
        <br />
        <br />
        产品分类列表：<br />
        <%=strClass %>
        <br />
        <asp:HiddenField ID="HdnNumClass" runat="server" />
        <asp:Button ID="BtnSaveClass" runat="server" Text="保存分类列表修改" OnClick="BtnSaveClass_Click"
            CssClass="BtnS" OnClientClick="return CheckNumClass();" />
    </div>
    <div id="Btn" runat="server" style="margin-left: 400px;">
        <br />
        <br />
        <asp:Button ID="BtnEdit" runat="server" Text="编辑此模板" OnClick="BtnEdit_Click" />
    </div>
    <div style="display: none;">
        <asp:HiddenField ID="HdnPath" runat="server" />
        <asp:Button ID="BtnCreateEmp" runat="server" Text="Button" OnClick="BtnCreateEmp_Click" />
    </div>
    </form>
</body>
</html>
