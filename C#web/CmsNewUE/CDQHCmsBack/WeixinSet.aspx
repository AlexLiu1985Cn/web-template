<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeixinSet.aspx.cs" Inherits="CmsApp20.CDQHCmsBack.WeixinSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>微信设置</title>
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
    </style>
    <script type="text/javascript" src="js/NavFrame.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：微信管理 >> <a href="WeixinSet.aspx">微信设置</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
            <div style="text-align: center; line-height: 30px; width: 820px; margin: 0 auto;">
                <table cellpadding="2" cellspacing="6" width="820px">
                    <tr>
                        <td width="100px" align="right">
                            微信开关：
                        </td>
                        <td align="left">
                            <input type="radio" name="WeixinOn" value="1" <%=strWOn %> />开启&nbsp;&nbsp;
                            <input type="radio" name="WeixinOn" value="0" <%=strWOff %> />关闭
                        </td>
                    </tr>
                    <tr style="display: none;" align="right">
                        <td width="100px" align="right">
                            微信号类型：
                        </td>
                        <td align="left">
                            <input type="radio" name="WeixinType" value="0" <%=strWeixinType0 %> />订阅号&nbsp;&nbsp;
                            <input type="radio" name="WeixinType" value="1" <%=strWeixinType1 %> />服务号
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            微信URL：
                        </td>
                        <td align="left">
                            <input name="URL" id="URL" type="text" size="60" value="<%=strURL%>" maxlength="200"
                                onblur="CheckURL(this);" />
                            <span class="tips" id="URLTips">
                                <%=strURLErr %></span>
                        </td>
                    </tr>
                    <tr id="URLTips_td" style="display: none">
                        <td align="right">
                        </td>
                        <td align="left" style="color: #999999;" id="URLTips2">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            微信Token：
                        </td>
                        <td align="left">
                            <input name="Token" id="Token" type="text" size="60" maxlength="200" value="<%=Token%>">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="left" style="color: Green;">
                            请按以上添写的URL和Token到微信公众平台申请订阅号或服务号。<br>
                            域名必须为真实域名，同时将系统上传至此域名空间下，微信公共帐号才可使用。
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="100px">
                            微信关注统计：
                        </td>
                        <td align="left" width="700px">
                            今日关注人数：<asp:Label ID="subToday" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                            关注总人数：<asp:Label ID="subTotle" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                            历史关注人数：<asp:Label ID="subHistory" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<span
                                class="tips">（此统计数仅供参考）</span>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px" align="right">
                            关注欢迎词：
                        </td>
                        <td align="left">
                            <textarea name="eventWelcom" cols="70" rows="4"><%=strEventWelcom%></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px" align="right">
                            缺省自动回复：
                        </td>
                        <td align="left">
                            <textarea name="DefaultBack" cols="70" rows="4"><%=strDefaultBack%></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            缺省回复图片：
                        </td>
                        <td align="left">
                            <input name="PicUrl" type="text" style="width: 300px;" value="<%=strPicUrl%>" maxlength="200" />
                            <input type="file" id="myFile" runat="server" onchange="CheckImg('myFile')" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            新闻条数：
                        </td>
                        <td align="left">
                            <input name="NewsNum" id="NewsNum" type="text" maxlength="1" size="10" value="<%=strNewsNum%>" /><span
                                class="tips">最多9条</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            产品条数：
                        </td>
                        <td align="left">
                            <input name="ProductNum" id="ProductNum" type="text" maxlength="1" size="10" value="<%=strProductNum%>" /><span
                                class="tips">最多9条</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            图片条数：
                        </td>
                        <td align="left">
                            <input name="PictureNum" id="PictureNum" type="text" maxlength="1" size="10" value="<%=strPictureNum%>" /><span
                                class="tips">最多9条</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button ID="BtnSave" runat="server" Text=" 保 存 " OnClick="BtnSave_Click" OnClientClick="return CheckData();" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $2 = function (id) { return document.getElementById(id); }
        String.prototype.Trim = function () { return this.replace(/(^\s*)|(\s*$)/g, ''); }
        var reg = /^http:\/\/(\w+)\.(\w+).[\w\.]+?\/WebApp\/WeiXin\/CdqhWeiXin.aspx$/i;
        function CheckURL(obj) {
            var strURL = obj.value;
            var objTips = $2("URLTips");
            var objTips2 = $2("URLTips2");
            var objTips_td = $2("URLTips_td");
            if (strURL.indexOf("localhost") != -1 || strURL.indexOf("127.0.0.1") != -1 || strURL.indexOf("http://xxxx/") != -1)
            { objTips.innerHTML = "请填写正式域名"; return; }
            if (strURL.indexOf("WebApp/WeiXin/CdqhWeiXin.aspx") == -1)
            { objTips_td.style.display = ""; objTips2.innerHTML = "<img src='images/Err.jpg' />请填写连接微信的URL文件（WebApp/WeiXin/CdqhWeiXin.aspx）"; return; }
            else
            { objTips_td.style.display = "none"; }
            if (reg.test(strURL))
                objTips.innerHTML = "<img src='images/Ok.jpg' />";
            else
                objTips.innerHTML = "<img src='images/Err.jpg' />";
        }
        function CheckData() {
            //if(!reg.test($2("URL").value.Trim())){alert("微信URL不对！");return false}
            if (!/^\w+$/.test($2("Token").value.Trim())) { alert("微信Token不对！"); return false }
            if (!/^\d{1}$/.test($2("NewsNum").value.Trim())) { alert("新闻条数必须为数字！"); return false }
            if (!/^\d{1}$/.test($2("ProductNum").value.Trim())) { alert("产品条数必须为数字！"); return false }
            if (!/^\d{1}$/.test($2("PictureNum").value.Trim())) { alert("图片条数必须为数字！"); return false }
            return true;
        }
    </script>
</body>
</html>
