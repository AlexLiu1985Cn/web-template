<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topframe.aspx.cs" Inherits="CmsApp20.CalliBack.topframe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
    <title>创都启航企业网站管理系统</title>
    <!-- Power by 创都启航企业网站管理系统 http://www.95c.com.cn -->
    <!--[if IE 6]>
<script src="js/iepngfix.js" mce_src="js/iepngfix.js"></script>
<script type="text/javascript">DD_belatedPNG.fix('*');</script>
<![endif]-->
</head>
<script language="javascript">
    function changframe(left, main, nOrder) {
        var leftFrame = parent.document.getElementById("menu-frame");
        var mainFrame = parent.document.getElementById("main-frame");
        leftFrame.src = left + "?" + Math.random();
        mainFrame.src = main + "?" + Math.random();
        if (nOrder == 2) {
            var NavFrame = parent.document.getElementById("Nav-frame");
            NavFrame.src = main + "?" + Math.random();
        }
        for (var i = 1; i <= 7; i++) {

            var vLi = "li" + i;
            var vLiID = document.getElementById(vLi);
            var vClName = vLiID.className;
            var v1 = vClName.indexOf('1');
            if (v1 > 0) {
                vLiID.className = vClName.substr(0, v1);
                break;
            }
        }
        vLi = "li" + nOrder;
        vLiID = document.getElementById(vLi);
        vClName = vLiID.className;
        v1 = vClName.indexOf('1');
        if (v1 < 0)
            vLiID.className = vClName + "1";
        //    var vHidden1=document.getElementById("Hidden1");
        //    vHidden1 .value = nOrder;
    }
    function Home() {
        window.parent.open("../", '_blank');
    } 
</script>
<body>
    <div id="top">
        <a href="http://www.95c.com.cn" target="_blank" class="logo">
            <img alt="创都启航企业网站管理系统" title="创都启航企业网站管理系统" src="images/logo.jpg" width="162" height="37" /></a>
        <div class="main" id="NavMain">
            <ul>
                <li id="li1" class="xitong1"><a href="javascript:changframe('Left_SystemSet.aspx','QH_SystemInfoNew.aspx',1)"
                    title="系统设置">系统设置</a></li>
                <li id="li2" class="neirong"><a href="javascript:changframe('Left_ContentNew.aspx','ContentManageAllNav.aspx',2)"
                    title="内容管理">内容管理</a></li>
                <li id="li3" class="fengge"><a href="javascript:changframe('Left_PageStyle.aspx','QH_BannerSet.aspx',3)"
                    title="页面风格">页面风格</a></li>
                <li id="li4" class="tongji"><a href="javascript:changframe('Left_Statistics.aspx','QH_Statistics1.aspx',4)"
                    title="优化统计">优化统计</a></li>
                <li id="li5" class="yonghu"><a href="javascript:changframe('Left_UserManage.aspx','Admin_User_Login.aspx',5)"
                    title="用户管理">用户管理</a></li>
                <li id="li6" class="shouji"><a href="javascript:changframe('Left_MobileManage.aspx','MobileSiteSet.aspx',6)"
                    title="手机管理">手机管理</a></li>
                <li id="li7" class="webapp"><a href="javascript:changframe('Left_WebAppManage.aspx','WeixinSet.aspx',7)"
                    title="网站应用">网站应用</a></li>
            </ul>
        </div>
        <div class="sub">
            <ul>
                <li class="username noborder">
                    <%=strAdmin %></li>
                <li class="username noborder">
                    <%=strAuthorize %></li>
                <li><a href="javascript:Home()" title="网站首页">网站首页</a></li>
                <li><a href="Admin_LoginOut.aspx" title="退出登录">退出登录</a></li>
            </ul>
        </div>
    </div>
</body>
</html>
