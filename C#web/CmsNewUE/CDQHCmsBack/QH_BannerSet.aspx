<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_BannerSet.aspx.cs" Inherits="CmsApp20.CmsBack.QH_BannerSet"
    MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript" src="js/NavFrame.js"></script>
    <script type="text/javascript" src="js/BannerStyle.js"></script>
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
        .orTM
        {
            width: 20px;
        }
        .Cln
        {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <%--         <div class="now" style ="width :100px; height :30px; margin-left :140px; background-color:#1F75B7 ; text-align:center;  line-height:30px;float:left;" >Banner设置</div>
        <div style ="width :100px; height :30px; margin-left :140px;  text-align:center;  line-height:30px; "><a href ="QH_BannerManage.aspx">Banner管理</a></div>
        <div style =" clear :both ; margin-left :140px; border-top: solid 1px #555;"><br /></div>
    --%>
    <form id="form1" runat="server">
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：图片设置 >> <a href="QH_BannerSet.aspx">Banner设置</a></div>
            <div class="rightmain1">
                二级位置：Banner设置 - <a href="QH_BannerManage.aspx">Banner管理</a></div>
            <div class="rightmain1">
            </div>
            <table width="840px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF"
                id="table1">
                <thead style="background: #ECECEC; font-weight: bold; text-align: left; height: 23px;">
                    <tr style="background: url(images/1.jpg) repeat-x; height: 30px; color: #FFFFFF;
                        font-weight: bold;">
                        <td>
                            应用栏目
                        </td>
                        <td width="400">
                            Banner模式
                        </td>
                        <td width="70">
                            宽(像素)
                        </td>
                        <td width="70">
                            高(像素)
                        </td>
                        <td width="50" style="text-align: center;">
                            预览
                        </td>
                        <td width="40" style="text-align: center;">
                            默认
                        </td>
                    </tr>
                </thead>
                <tr bgcolor="#e6e6e6" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
                    <td align="left" class="Cln">
                        默认栏目
                    </td>
                    <td width="400" align="left">
                        <select name="BnrMode0" id="BnrMode0" onchange="BnrSel(this,0)" runat="server">
                            <option value="0">关闭</option>
                            <option value="1" style="color: #FF0000">图片轮播</option>
                            <option value="2" style="color: #003399">Flash动画</option>
                            <option value="3" style="color: #339933">单张图片</option>
                        </select>
                    </td>
                    <td width="70">
                        <input name="Width0" id="Width0" type="text" maxlength="4" class="or" />
                    </td>
                    <td width="70">
                        <input name="Height0" id="Height0" type="text" maxlength="3" class="or" />
                    </td>
                    <td width="50" style="text-align: center;">
                        预览
                    </td>
                    <td width="40" style="text-align: center;">
                        <input name="Ck0" id="Ck0" type="checkbox" onclick="CkAll(this)" />
                    </td>
                </tr>
                <tr bgcolor="#f9f9f9" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
                    <td align="left" class="Cln">
                        网站首页
                    </td>
                    <td width="400" align="left">
                        <select name="BnrMode1" id="BnrMode1" onchange="BnrSel(this,1)">
                            <option value="0">关闭</option>
                            <option value="1" style="color: #FF0000">图片轮播</option>
                            <option value="2" style="color: #003399">Flash动画</option>
                            <option value="3" style="color: #339933">单张图片</option>
                        </select>
                    </td>
                    <td width="70">
                        <input name="Width1" id="Width1" type="text" maxlength="4" class="or" />
                    </td>
                    <td width="70">
                        <input name="Height1" id="Height1" type="text" maxlength="3" class="or" />
                    </td>
                    <td width="50" style="text-align: center;">
                        预览
                    </td>
                    <td width="40" style="text-align: center;">
                        <input name="Ck1" id="Ck1" type="checkbox" />
                    </td>
                </tr>
                <%=strColumn %>
            </table>
            <table width="840px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF">
                <tr>
                    <td height="30">
                        <asp:Button ID="BSaveA" runat="server" Text=" 保 存 " OnClick="BSave_Click" OnClientClick="return CheckWH();" />
                        <asp:Button ID="BtnReset" runat="server" Text=" 重 置 " CssClass="ret" OnClick="BtnReset_Click" />
                        <asp:HiddenField ID="HdSaveID" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
    <%=strAlertJS %>
</body>
</html>
