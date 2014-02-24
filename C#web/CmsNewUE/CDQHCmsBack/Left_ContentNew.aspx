<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left_ContentNew.aspx.cs" Inherits="CmsApp20.CmsBack.Left_ContentNew" %>

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
    for (var i=0;i<=$1("HdnNum").value+7;i++)
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
  $(document).ready(function(){$(".left02down:eq(0)").slideUp();$('.left02').each(function () {
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
			<div class="left02top_q">栏目配置</div>
		</div>
	  <div class="left02down">
			<div class="left02down01C"><a href="Column.aspx" target="main-frame" class="leftnow" onclick=SetNow(0) id=a0 >栏目配置</a></div>
			<div class="left02down01C"><a href="CustomField.aspx?Mdl=3" target="main-frame"  onclick=SetNow(1) id=a1 >产品模块字段</a></div>
			<div class="left02down01C"><a href="CustomField.aspx?Mdl=4" target="main-frame"  onclick=SetNow(2) id=a2 >下载模块字段</a></div>
			<div class="left02down01C"><a href="CustomField.aspx?Mdl=5" target="main-frame"  onclick=SetNow(3) id=a3 >图片模块字段</a></div>
		</div>
	</div>
	<div class="left02">
		<div class="left02top">
			<div class="left02top_right"></div>
			<div class="left02top_left"></div>
			<div class="left02top_q">内容管理</div>
		</div>
		<div class="left02down">
							<%=strContentMng %>
		</div>
	</div>
    
   	<div class="left02">
		<div class="left02top">
			<div class="left02top_right"></div>
			<div class="left02top_left"></div>
			<div class="left02top_q">生成静态页</div>
		</div>
		<div class="left02down">
			<div class="left02down01C"><a href="TMCreateStaticF.aspx?Mdl=0" target="main-frame" onclick=SetNow(6) id=a6 >生成静态页</a></div>
			<div class="left02down01C"><a href="TMCreateDetailSF.aspx?Mdl=ND" target="main-frame" onclick=SetNow(4) id=a4 >生成内容静态页</a></div>
			<div class="left02down01C"><a href="TMCreateAll.aspx?Mdl=0" target="main-frame" onclick=SetNow(5) id=a5 >生成全部</a></div>
		</div>
	</div> 
</div>
    <input id="HdnNum" type="hidden" value ="7"  runat=server  />
    <%=strScript%>
</body>
</html>
