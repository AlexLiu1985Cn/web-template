<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left_Statistics.aspx.cs"
    Inherits="CmsApp20.CmsBack.Left_Statistics" %>

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
            if (vAID.className == "leftnow") {
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
        <div class="left02">
            <div class="left02top">
                <div class="left02top_right">
                </div>
                <div class="left02top_left">
                </div>
                <div class="left02top_q">
                    访问统计</div>
            </div>
            <div class="left02down">
                <div class="left02down01">
                    <a href="QH_Statistics1.aspx" target="main-frame" class="leftnow" onclick="SetNow(0)"
                        id="a0">综合报告</a></div>
                <div class="left02down01">
                    <a href="QH_StatLast.aspx" target="main-frame" onclick="SetNow(1)" id="a1">最近访客</a></div>
                <div class="left02down01">
                    <a href="QH_StatEngine.aspx" target="main-frame" onclick="SetNow(2)" id="a2">搜索引擎分析</a></div>
                <div class="left02down01">
                    <a href="QH_Statkeyword.aspx" target="main-frame" onclick="SetNow(3)" id="a3">关键词分析</a></div>
                <div class="left02down01">
                    <a href="QH_StatisticsSet.aspx" target="main-frame" onclick="SetNow(4)" id="a4">统计设置</a></div>
            </div>
        </div>
        <div class="left02">
            <div class="left02top">
                <div class="left02top_right">
                </div>
                <div class="left02top_left">
                </div>
                <div class="left02top_q">
                    优化设置</div>
            </div>
            <div class="left02down">
                <div class="left02down01">
                    <a href="QH_OptimizationBak.aspx" target="main-frame" onclick="SetNow(7)" id="a7">备用字段</a></div>
                <div class="left02down01">
                    <a href="QH_RelateLinkManage.aspx" target="main-frame" onclick="SetNow(5)" id="a5">热点关键词管理</a></div>
                <div class="left02down01">
                    <a href="QH_TagsType.aspx?Mdl=2" target="main-frame" onclick="SetNow(6)" id="a6">标签管理</a></div>
                <div class="left02down01">
                    <a href="QH_SiteMap.aspx" target="main-frame" onclick="SetNow(8)" id="a8">生成网站地图</a></div>
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
