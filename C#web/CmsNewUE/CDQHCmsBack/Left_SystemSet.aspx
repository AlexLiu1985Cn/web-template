<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left_SystemSet.aspx.cs"
    Inherits="CmsApp20.CalliBack.SystemSet_Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
    <title>创都启航企业网站管理系统</title>
    <script type="text/javascript" src="../Ajax/jquery.js"></script>
</head>
<script language="javascript">
    function SetNow(nOrder) {
        for (var i = 0; i <= 11; i++) {

            var vA = "a" + i;
            var vAID = document.getElementById(vA);
            if (vAID && vAID.className == "leftnow") {
                vAID.className = "";
                break;
            }
        }
        vA = "a" + nOrder;
        document.getElementById(vA).className = "leftnow";
    }
    $(document).ready(function () {
        $('.left02').each(function () {
            $(this).find(".left02top").bind('click', function (event) {
                $(this).parent().find(".left02down").slideToggle();
            });
        });
    });
</script>
<body>
    <div class="left" id="LeftBox">
        <div class="left01">
            <div class="left01_right">
            </div>
            <div class="left01_left">
            </div>
            <div class="left01_c">
                超级管理员：<%=strAdmin %></div>
        </div>
        <div class="left02">
            <div class="left02top">
                <div class="left02top_right">
                </div>
                <div class="left02top_left">
                </div>
                <div class="left02top_c">
                    系统设置</div>
            </div>
            <div class="left02down">
                <div class="left02down01">
                    <a href="QH_SystemInfoNew.aspx" target="main-frame" class="leftnow" onclick="SetNow(0)"
                        id="a0">系统信息</a></div>
                <div class="left02down01">
                    <a href="QH_BaseConfig.aspx" target="main-frame" onclick="SetNow(1)" id="a1">基本信息设置</a></div>
                <div class="left02down01">
                    <a href="QH_CommenSet.aspx" target="main-frame" onclick="SetNow(2)" id="a2">网站常用信息设置</a></div>
                <div class="left02down01">
                    <a href="QH_SafeSet.aspx" target="main-frame" onclick="SetNow(3)" id="a3">安全信息设置</a></div>
                <div class="left02down01">
                    <a href="QH_DataBackup.aspx" target="main-frame" onclick="SetNow(4)" id="a4">数据备份及还原</a></div>
                <div class="left02down01">
                    <a href="QH_FromEmailSet.aspx" target="main-frame" onclick="SetNow(5)" id="a5">系统邮箱设置</a></div>
                <div class="left02down01">
                    <a href="QH_OtherSet.aspx" target="main-frame" onclick="SetNow(6)" id="a6">页面设置</a></div>
                <div class="left02down01">
                    <a href="QH_QQSet.aspx" target="main-frame" onclick="SetNow(9)" id="a9">在线客服设置</a></div>
            </div>
        </div>
        <div class="left02">
            <div class="left02top">
                <div class="left02top_right">
                </div>
                <div class="left02top_left">
                </div>
                <div class="left02top_q">
                    其它设置</div>
            </div>
            <div class="left02down">
                <div class="left02down01">
                    <a href="QH_FriendLinkManage.aspx" target="main-frame" onclick="SetNow(8)" id="a8">友情链接管理</a></div>
                <div class="left02down01">
                    <a href="ProductPriceInterval.aspx" target="main-frame" onclick="SetNow(7)" id="a7">
                        产品价格区间</a></div>
                <div class="left02down01">
                    <a href="ProductBrandManage.aspx" target="main-frame" onclick="SetNow(11)" id="a11">
                        产品品牌管理</a></div>
            </div>
        </div>
        <div class="left02">
            <div class="left02top">
                <div class="left02top_right">
                </div>
                <div class="left02top_left">
                </div>
                <div class="left02top_q">
                    在线升级</div>
            </div>
            <div class="left02down">
                <div class="left02down01">
                    <a href="QH_OnlineUpgrade.aspx" target="main-frame" onclick="SetNow(10)" id="a10">在线升级</a></div>
            </div>
        </div>
        <div class="left02">
            <div class="left02top">
                <div class="left02top_right">
                </div>
                <div class="left02top_left">
                </div>
                <div class="left02top_q">
                    帮助信息</div>
            </div>
            <div class="left02down">
                <div class="left02down01">
                    <a href="http://www.95c.com.cn/" target="_blank">服务网址</a></div>
            </div>
        </div>
    </div>
    <form id="f111" runat="server">
    </form>
</body>
</html>
