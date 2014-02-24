<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<LINK rel="stylesheet" type="text/css" href="[QH:PathPre]template/CDQHTM/css/css.css">
<!--[if IE 6]><script type="text/javascript" src="[QH:PathPre]template/CDQHTM/js/li.js"></script><![endif]-->
<title>[QH:SiteTitle]</title>
<meta content="[QH:SiteDescription]" name="description"/>
<meta content="[QH:SiteKeyword]" name="keywords" />
<link rel="icon" type="image/x-icon" href="[QH:Favicon]" />
<script src="[QH:PathPre]template/CDQHTM/js/public.js" type="text/javascript"></script>

</head>
<script type="text/jscript" >
String.prototype.Trim = function()
{
	return this.replace( /(^\s*)|(\s*$)/g, '' ) ;
}
function Empty1(objtxt){
if(objtxt.value.Trim()==''||objtxt.value=='- 请输入您要搜索的产品 -' ){
objtxt.value ="";
objtxt.style.color="Black";
}
}
function EmptyOrTxt1(objtxt){
if(objtxt.value.Trim()==''||objtxt.value=='- 请输入您要搜索的产品 -'){
objtxt.value='- 请输入您要搜索的产品 -';
objtxt.style.color="Gray";
}
}
</script>
<body>
<div class="top">
<div class="logo"><img src="[QH:Logo]" /></div>
<div class="biao">
<div class="by">
<span id="[QH:ToLand]" ><a  href="[QH:Land]">登录</a> |  <a href="[QH:Regist]">注册</a>  |  </span>
<span id="[QH:IsLand]"  style ="display :none; "><span id="[QH:UserName]"></span>  &nbsp; 您好！ |  <a  href="[QH:ExitLand]"  >退出</a>  |  <a  href="[QH:MemberCenter]"  >会员中心</a>  |  </span>
<a href="#" onclick='SetHome(this,window.location);' title='设为首页'>设为首页</a> <a href="#" onclick='addFavorite();' title='收藏本站' >加入收藏</a>
</div>
<div class="dh1">[QH:Tel] [QH:Mobile]</div>
<div class="ci">
  <table border="0" align="right" cellpadding="0" cellspacing="0" class="tab_search">
	<tr>
		<td>
			<input type="text" name="q" title="Search Products" class="searchinput" id="searchinput"  value="- 请输入您要搜索的产品 -" size="10" style="color:Gray;" onfocus=Empty1(this) onblur =EmptyOrTxt1(this) />
		</td>
		<td>
			<input type="image" width="21" height="17" id="PSearch" class="searchaction"  alt="搜索" src="[QH:PathPre]template/CDQHTM/images/magglass.gif" border="0" hspace="2"
			 onclick="if(searchinput.value=='- 请输入您要搜索的产品 -'){alert('请输入您要搜索的产品！');return} [QH:SearchOnclickProduct:searchinput]" />
		</td>
	</tr>
</table>
</div>
<div class="clear"></div>
</div>
<div class="clear"></div>
</div>

<div class="nav">
<ul>
<li ><a href="[QH:HomeLink]">网站首页</a></li>
[QH:loopNavUp]<li class="bj"><a href="[QH:LinkPath]">[QH:Title]</a></li>[/QH:loopNavUp]
</ul>
</div>

<div class="fbanner">
[QH:Banner]
</div>

<div class="fzhong">
<div class="fzl">
<div class="fzl1">
<div class="flbt">
<div class="flbtl">[QH:FenLanTitle1NoLink]</div>
</div>
<div class="flnr">
<ul>[QH:loopFenYeT NowClass=fnow]
<li><a href="[QH:LinkPath]" class="[QH:NowClass]">[QH:FenLanTitle]</a></li>[/QH:loopFenYeT]
</ul>

</div>
<div class="clear"></div>
</div>
<div class="clear"></div>
</div>
<div class="fzr">
<div class="fzr1">
<div class="frbt">
<div class="frbt1">[QH:FenLanTitle]  [QH:sFenLanTitle] [QH:ssFenLanTitle]</div>
</div>
<div class="frnr">

<div class="newsn">
<div class="newstitle">[QH:Title]</div>
<div class="newsft1">[QH:Author] [QH:MDate]</div>
<div class="webimg">
  [QH:DetailPicture]
<div class="clear"></div>
</div>
<div class="newsnr">
[QH:Contents]    

</div>
<div class="clear"></div>
</div>

<div class="tages">
<span><b><a href="#" target="_blank">TAG标签：</a></b> [QH:NewsTags]</span></div>

<div class="xg">
<div class="xgl">
上一条：[QH:PrevOne]<br />
下一条：[QH:NextOne]</div>
<div class="xgr">

<!-- Baidu Button BEGIN -->
    <div id="bdshare" class="bdshare_b" style="line-height: 12px;"><img src="http://bdimg.share.baidu.com/static/images/type-button-1.jpg" />
		<a class="shareCount"></a>
	</div>
<script type="text/javascript" id="bdshare_js" data="type=button&amp;uid=480999" ></script>
<script type="text/javascript" id="bdshell_js"></script>
<script type="text/javascript">
	document.getElementById("bdshell_js").src = "http://bdimg.share.baidu.com/static/js/shell_v2.js?t=" + new Date().getHours();
</script>
<!-- Baidu Button END -->

</div>
<div class="clear"></div>
</div>

 <div class="clear"></div>
</div>
<div class="clear"></div>
</div>
</div>
<div class="clear"></div>
</div>

<div class="foot">
<div class="ge1"></div>
<div class="xianav"><a href="[QH:HomeLink]">首页</a>[QH:loopNavDown] | <a href="[QH:LinkPath]">[QH:Title]</a> [/QH:loopNavDown]</div>
<div class="footn">[QH:Copyright] 版权所有    电话：[QH:Tel] 传真:[QH:Fax]  [QH:JSStatistic]<br />
  24小时服务：[QH:Mobile]   邮编：100000 Email:[QH:Email]    地址：[QH:Address]  <br />
  [QH:ICP] [QH:CDQHLink] 
  
  </div>
</div>

[QH:JScript]
</body>
</html>
