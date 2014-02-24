<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left_WebAppManage.aspx.cs"
    Inherits="CmsApp20.CDQHCmsBack.Left_WebAppManage" %>

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
        for (var i = 0; i <= 8; i++) {

            var vA = "a" + i;
            var vAID = document.getElementById(vA);
            if (vAID && vAID.className == "leftnow") {
                vAID.className = "";
                break;
            }
        }
        vA = "a" + nOrder;
        document.getElementById(vA).className = "leftnow"; ;
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
        <div class="left02">
            <div class="left02top">
                <div class="left02top_right">
                </div>
                <div class="left02top_left">
                </div>
                <div class="left02top_q">
                    微信管理</div>
            </div>
            <div class="left02down">
                <div class="left02down01">
                    <a href="WeixinSet.aspx" target="main-frame" class="leftnow" onclick="SetNow(0)"
                        id="a0">微信设置</a></div>
                <div class="left02down01">
                    <a href="WeixinBackDingYue.aspx" target="main-frame" onclick="SetNow(1)" id="a1">自动回复设置</a></div>
                <%=strWeixinFuwuSet%>
                <%--			<div class="left02down01"><a href="TMCreateStaticF_Mobile.aspx?Mdl=0" target="main-frame"  onclick=SetNow(3) id=a3 >生成手机静态页</a></div>
                --%>
            </div>
        </div>
        <div class="left02">
            <div class="left02top">
                <div class="left02top_right">
                </div>
                <div class="left02top_left">
                </div>
                <div class="left02top_q">
                    地图设置</div>
            </div>
            <div class="left02down">
                <div class="left02down01">
                    <a href="MapSet.aspx" target="main-frame" onclick="SetNow(3)" id="a3">设置动态地图</a></div>
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
</body>
</html>
