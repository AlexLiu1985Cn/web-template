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
<span id="[QH:IsLand]"  style ="display :none; "><span id="[QH:UserName]"></span>  &nbsp; 您好！ |  <a  href="[QH:ExitLand]"  >退出</a>  |  <a  href="[QH:MemberCenter]"  >会员中心</a>  | </span>
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

<div class="banner">
[QH:Banner]
</div>

<div class="zhong">


<div class="zt">
<div class="ztl">
<div class="ztl1">
<div class="b1b">
[QH:loopLanmu Column=6,NewsType=1]
<div class="jbt">[QH:FenLanTitle]</div>
<div class="jbtr"><a href="[QH:LinkPath]">&gt;&gt;更多</a></div>
[/QH:loopLanmu]
<div class="clear"></div>
</div>
<div class="ztl2n">
<ul>[QH:loop Module=2,NewsCount=5,TitleNum=26,AddStr=...,Condition=,Role=,KeyWord=,Column=6,NewsType=,Order=Desc,Sort=,NewsDate=]
<li><a href="[QH:NewsPath]" title="[QH:AltText]"><span>[[QH:Date]]</span>[QH:Title]</a></li>[/QH:loop]
</ul>
</div>

</div>
<div class="ztl2">

<div class="b1b">[QH:loopLanmu Column=25,NewsType=1]
<div class="jbt">[QH:FenLanTitle]</div>
<div class="jbtr"><a href="[QH:LinkPath]">&gt;&gt;更多</a></div>[/QH:loopLanmu]
<div class="clear"></div>
</div>
<div class="b1n">
<div class="b1nl">
<div class="b1nlimg">
<script language="JavaScript"> 
  [QH:loop Module=5,NewsCount=6,TitleNum=,AddStr=...,Condition=,Role=<hr />,KeyWord=,Column=25,NewsType=Fps,Order=Desc,Sort=DateTime,NewsDate=,JS_type=2]

var focus_width=178
var focus_height=138
var text_height=0 
var swf_height = focus_height+text_height 
var pics='[QH:JS2pic]' 
var links='[QH:JS2link]' 
var texts='||' 
document.write('<object ID="focus_flash" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="'+ focus_width +'" height="'+ swf_height +'">'); 
document.write('<param name="allowScriptAccess" value="sameDomain"><param name="movie" value="ajax/pixviewer.swf"><param name="quality" value="high"><param name="bgcolor" value="#cccccc">'); 
document.write('<param name="menu" value="false"><param name=wmode value="opaque">'); 
document.write('<param name="FlashVars" value="pics='+pics+'&links='+links+'&borderwidth='+focus_width+'&borderheight='+focus_height+'&textheight='+text_height+'">'); 
document.write('<embed ID="focus_flash" src="ajax/pixviewer.swf" wmode="opaque" FlashVars="pics='+pics+'&links='+links+'&borderwidth='+focus_width+'&borderheight='+focus_height+'&textheight='+text_height+'" menu="false" bgcolor="#ffffff" quality="high" width="'+ focus_width +'" height="'+ swf_height +'" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />'); 
document.write('</object>'); 
   [/QH:loop]
</script>          
</div>
</div>
<div class="b1nr">
<ul>[QH:loop Module=5,NewsCount=5,TitleNum=26,AddStr=...,Condition=,Role=,KeyWord=,Column=25,NewsType=,Order=Desc,Sort=,NewsDate=]
<li><a href="[QH:NewsPath]" title="[QH:AltText]">[QH:Title]</a></li>[/QH:loop]
</ul>

</div>
<div class="clear"></div>
</div>



</div>
<div class="ztl3"></div>


</div>
<div class="ztr">
<div class="ztl1">
<div class="b1b">[QH:loopLanmu Column=31,NewsType=1]
<div class="jbt">[QH:FenLanTitle]</div>
<div class="jbtr"><a href="[QH:LinkPath]">&gt;&gt;更多</a></div>[/QH:loopLanmu]
<div class="clear"></div>
</div>
<div class="b1n">

<div class="ztl2n">
<ul>
[QH:loop Module=2,NewsCount=5,TitleNum=26,AddStr=...,Condition=,Role=,KeyWord=,Column=31,NewsType=,Order=Desc,Sort=,NewsDate=]
<li><a href="[QH:NewsPath]" title="[QH:AltText]"><span>[[QH:Date]]</span>[QH:Title]</a></li>
[/QH:loop]
</ul>
</div>

<div class="clear"></div>
</div>
</div>

<div class="ztl2">
<div class="b1b">[QH:loopLanmu Column=9,NewsType=1]
<div class="jbt">[QH:FenLanTitle]</div>
<div class="jbtr"><a href="[QH:LinkPath]">&gt;&gt;更多</a></div>[/QH:loopLanmu]
<div class="clear"></div>
</div>
<div class="ztl2n1">


<div id="demo">
      <div id="indemo">
      <div id="demo1">
[QH:loop Module=3,NewsCount=10,TitleNum=18,AddStr=...,Condition=,Role=,KeyWord=,Column=9,NewsType=,Order=Desc,Sort=,NewsDate=]
		<div class="sygd">
		  <table width="140" border="0" cellspacing="0" cellpadding="0">
            <tbody><tr>
              <td height="100" class="picbk"><a href="[QH:NewsPath]" target="_blank"><img src="[QH:PicPath]" alt="[QH:AltText]" width="140" height="100" /></a></td>
            </tr>
            <tr>
              <td height="28" align="center"><a href="[QH:NewsPath]" target="_blank">[QH:Title]</a></td>
            </tr>
          </tbody></table>
		</div>
 [/QH:loop]

        <div id="demo2"></div>
		   <script> 
    <!-- 
    var speed=18; //数字越大速度越慢 
    var tab=document.getElementById("demo"); 
    var tab1=document.getElementById("demo1"); 
    var tab2=document.getElementById("demo2"); 
    tab2.innerHTML=tab1.innerHTML; 
    function Marquee(){ 
        if(tab2.offsetWidth-tab.scrollLeft<=0) 
        tab.scrollLeft-=tab1.offsetWidth 
        else{ 
            tab.scrollLeft++; 
        } 
    } 
    var MyMar=setInterval(Marquee,speed); 
    tab.onmouseover=function() {clearInterval(MyMar)}; 
    tab.onmouseout=function() {MyMar=setInterval(Marquee,speed)}; 
    --> 
</script>  
		  </div></div></div>





</div>

</div>


</div>
<div class="clear"></div>

</div>

<div class="youqing">
<div class="yqb">
<div class="jbt">友情链接</div>
<div class="jbtr"> </div>
<div class="clear"></div>
</div>
<div class="yqn">
<div class="yqimg">
<ul>
[QH:loop Module=15,NewsCount=,TitleNum=18,AddStr=...,NewsType=Img,Order=Desc,Sort=,NewsDate=]
<li><a href="[QH:LinkPath]" target="_blank"><img src="[QH:PicPath]" alt="[QH:AltText]" width="88" height="31" /></a></li>
[/QH:loop]
</ul>
</div>
<div class="clear"></div>
<div class="yqtitle">
[QH:loop Module=15,NewsCount=,TitleNum=18,AddStr=...,NewsType=Text,Order=Desc,Sort=,NewsDate=]
<a href="[QH:LinkPath]" target="_blank" title="[QH:AltText]">[QH:Title]</a> 
[/QH:loop]
</div>



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

