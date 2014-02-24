<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_QH_DefaultTemplate" Codebehind="QH_DefaultTemplate.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
<style>
#table{
	float:none;
	width:620px;
	border-left:#999999 1px solid;
	border-top:#999999 1px solid;
}
#table li{
	float:left;
	width:620px;
	list-style:none outside;
	border-bottom:#999999 1px solid;
	border-right:#999999 1px solid;
	text-align:center;
}
#table div{
	float:left;
}
.left1{
	width:300px;
	border-right:#999999 1px solid;
	padding-bottom:4px; padding-top:4px;
}
.left{
	width:190px;
	border-right:#999999 1px solid;
	padding-bottom:4px; padding-top:4px;
	text-align:right;
	padding-right :110px;
}
.leftP{
	width:270px;
	border-right:#999999 1px solid;
	padding-bottom:4px; padding-top:4px;
	text-align:right;
	padding-right :30px;
}
.right{
	width:290px;
	padding-bottom:4px; padding-top:4px;
	text-align:left;
	padding-left :10px;
}
.right1{
	width:300px;
	padding-bottom:4px; padding-top:4px;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div align=center>
    <br /><br /><br /><br />
    <div id="table">
	<li>
		<div class="left1" style =" padding-bottom:10px; padding-top:10px">页面类型</div>
		<div class="right1" style =" padding-bottom:10px; padding-top:10px" >默认模板文件名</div>
	</li>
	<li>
		<div class="left" >首页</div>
		<div class="right">Module_home.aspx</div>
	</li>
	<li>
		<div class="left">简介模块</div>
		<div class="right">Module_JianJie.aspx</div>
	</li>
	<li>
		<div class="left">新闻模块</div>
		<div class="right">Module_News.aspx</div>
	</li>
	<li>
		<div class="left" >产品模块</div>
		<div class="right">Module_Product.aspx</div>
	</li>
	<li>
		<div class="left" >下载模块</div>
		<div class="right">Module_Download.aspx</div>
	</li>
	<li>
		<div class="left" >图片模块</div>
		<div class="right">Module_Picture.aspx</div>
	</li>
	<li>
		<div class="left" >留言模块</div>
		<div class="right">Module_Message.aspx</div>
	</li>
	<li>
		<div class="left" >招聘模块</div>
		<div class="right">Module_ZhaoPin.aspx</div>
	</li>
	<li>
		<div class="left">新闻内容页</div>
		<div class="right">Module_NewsDetails.aspx</div>
	</li>
	<li>
		<div class="left">产品内容页</div>
		<div class="right">Module_ProductDetails.aspx</div>
	</li>
	<li>
		<div class="left">下载内容页</div>
		<div class="right">Module_DownloadDetails.aspx</div>
	</li>
	<li>
		<div class="left">图片内容页</div>
		<div class="right">Module_PictureDetails.aspx</div>
	</li>
	<li>
		<div class="left">新闻标贴搜索页</div>
		<div class="right">Module_TagsAndSearchNews.aspx</div>
	</li>
	<li>
		<div class="left">产品标贴品牌搜索页</div>
		<div class="right">Module_TagsAndSearchProduct.aspx</div>
	</li>
	<li>
		<div class="left">产品价格区间页</div>
		<div class="right">Module_ProductPrice.aspx</div>
	</li>
	<li>
		<div class="left">地图页</div>
		<div class="right">Module_Map.aspx</div>
	</li>
</div>

 
    </div>
    </form>
</body>
</html>
