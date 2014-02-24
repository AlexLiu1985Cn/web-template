<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left_PageStyle.aspx.cs" Inherits="CmsApp20.CalliBack.PageStyle_Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
<title>创都启航企业网站管理系统</title>
<script type="text/javascript" src="../Ajax/jquery.js"></script>
</head>
<script language="javascript">
 $1=function(id) {return document .getElementById (id);}
 function  SetNow(nOrder)
  {
    for (var i=0;i<=$1("HdnNum").value+8;i++)
    {
      
       var vA="a"+i;
       var vAID=$1(vA);
       if(vAID&&vAID.className=="leftnow")
       {
          vAID.className="";
          break ;
       }
    }
   vA="a"+nOrder;
   $1(vA).className="leftnow";
  }
  $(document).ready(function(){$('.left02').each(function () {
                $(this).find(".left02top").bind('click', function (event) {
                $(this).parent().find(".left02down").slideToggle();
                });
        });});
</script>
<body>
<div class="left" id="LeftBox">
	<div class="left02">
		<div class="left02top">
			<div class="left02top_right"></div>
			<div class="left02top_left"></div>
			<div class="left02top_q">图片设置</div>
		</div>
	  <div class="left02down">
			<div class="left02down01"><a href="QH_BannerSet.aspx" target="main-frame" class="leftnow" onclick=SetNow(0) id=a0 >Banner设置</a></div>
			<div class="left02down01"><a href="QH_ThumbnailSet.aspx" target="main-frame" onclick=SetNow(1) id=a1 >缩略图设置</a></div>
		    <div class="left02down01"><a href="QH_WaterMarkSet.aspx" target="main-frame" onclick=SetNow(2) id=a2 >水印设置</a></div>
		    <div class="left02down01"><a href="QH_PicEffect.aspx" target="main-frame" onclick=SetNow(8) id=a8 >图片显示效果</a></div>
		</div>
	</div>
	
	<div class="left02">
		<div class="left02top">
			<div class="left02top_right"></div>
			<div class="left02top_left"></div>
			<div class="left02top_q">当前模板管理</div>
		</div>
	  <div class="left02down">
			<div class="left02down01"><a href="TemplateMng.aspx?Mdl=0" target="main-frame" onclick=SetNow(3) id=a3 >模块模板设置</a></div>
			<%=strTemplate %>
		</div>
	</div>
	
	<div class="left02">
		<div class="left02top">
			<div class="left02top_right"></div>
			<div class="left02top_left"></div>
			<div class="left02top_q">模板管理</div>
		</div>
	  <div class="left02down">
			<div class="left02down01"><a href="QH_DefaultTemplate.aspx" target="main-frame" onclick=SetNow(6) id=a6 >默认模板文件名</a></div>
			<div class="left02down01"><a href="TemplateShow.aspx" target="main-frame" onclick=SetNow(4) id=a4 >模板展示</a></div>
			<div class="left02down01"><a href="TemplateImport.aspx" target="main-frame" onclick=SetNow(5) id=a5 >导入模板</a></div>
		</div>
	</div>
	
 	<div class="left02">
		<div class="left02top">
			<div class="left02top_right"></div>
			<div class="left02top_left"></div>
			<div class="left02top_q">广告管理</div>
		</div>
	  <div class="left02down">
			<div class="left02down01"><a href="QH_AdvertiseManage.aspx" target="main-frame" onclick=SetNow(7) id=a7 >广告管理</a></div>
		</div>
	</div>
   
   	<div class="left02">
		<div class="left02top">
			<div class="left02top_right"></div>
			<div class="left02top_left"></div>
			<div class="left02top_q">帮助信息</div>
		</div>
		<div class="left02down">
			<div class="left02down01"><a href="http://www.95c.com.cn/" target=_blank >服务网址</a></div>
		</div>
	</div> 
</div>
    <input id="HdnNum" type="hidden" value ="9"  runat=server />
    <%=strScript%>
</body>
</html>
