﻿<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimum-scale=1.0, maximum-scale=1.0">
<title>[QH:SiteTitle]</title>
<link rel="stylesheet" type="text/css" href="[QH:PathPre]template/95csjNew/css/cdqh_mobilecss.css">
<SCRIPT language="JavaScript" src="[QH:PathPre]template/95csjNew/js/jquery-1.6.4.min.js" type="text/javascript"></SCRIPT>
<SCRIPT language="JavaScript" src="[QH:PathPre]template/95csjNew/js/nav.js" type="text/javascript"></SCRIPT>
<script type="text/javascript" src="[QH:PathPre]template/95csjNew/js/pronav.js"></script>

</head>

<body>
<div id="top"><img src="[QH:Logo]" alt="" /></div>
<div id="box">
<div class="topnav">
<ul class="topul">[QH:loopNavUp]
<li><a href="[QH:LinkPath]" title="[QH:Title]">[QH:Title]</a></li>[/QH:loopNavUp]
</ul>
<div class="clear"></div>
</div>

<div class="top"><P class="topleft">最专业的网站管理系统服务商</P>
<P class="topright"><SPAN class="toprights"><A title="展开/收起" href="javascript:void(0)">+</A></SPAN></P>
<div class="clear"></div>
</DIV>

</div>
<div class="pronav">
<div class="view_menu"><span><img src="[QH:PathPre]template/95csjNew/images/view_nav.png" alt="二级菜单按钮"/>展开分类</span>
      <div class="clear"></div>
      <div class="view_menumain">
      [QH:loopFenYeT Show2=0 ,NowClass=classname,TitleNum=18]
          <a href="[QH:LinkPath]" title="[QH:FenLanTitle]" data-ajax="false">[QH:FenLanTitle]</a>
       [/QH:loopFenYeT] 
      </div>
    </div>
    <div class="clear"></div>
</div>

<DIV class="pro">

	<div class="pro-row">
	[QH:loopPage NewsCount=20,TitleNum=22,AddStr=...,Condition=,Role=<hr />,KeyWord=,NewsType=]
			<div class="pro-item"><a href="[QH:NewsPath]"><img src="[QH:ThumbPicPath]" alt="[QH:AltText]" /></a><p>[QH:Title]</p></div>
    [/QH:loopPage]
		</div>

</DIV>
<div class="clear"></div>

<div class="sub1">
<div class="subabout">
<div id="fenye">
[QH:Pager]
<div class="clear"></div>
</div>
</div></div>


<div class="z">
<div class="bj1">
<ul class="nav">
<li><a title="首页" href="[QH:HomeLink]">首页</a></li>[QH:loopNavDown]
<li><a href="[QH:LinkPath]" title="[QH:Title]">[QH:Title]</a></li>[/QH:loopNavDown]
<div class="clear"></div>
</ul>
<div class="clear"></div>
</div>
<div class="copyright">版权所有：[QH:Copyright]<br />地址：[QH:Address]<br />技术支持：[QH:CDQHLink]</div>
</div>
<div class="clear"></div>
<div id="footer"> 
<div class="foot">

<a title="电话" class="tel" href="tel:[QH:Tel]">电话</a>
<a title="短信" class="sms" href="sms:[QH:Mobile]">短信</a>
<a title="地图" class="map" href="[QH:HomeLink]Map.html">地图</a>
[QH:loopLanmu Column=35,NewsType=1]
<a title="留言" class="share" href="[QH:LinkPath]">留言</a>
[/QH:loopLanmu]
<div class="clear"></div>
</div></div>


</body>
</html>
