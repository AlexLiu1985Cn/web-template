(function($){
	$.fn.Sonline = function(options,qqlist,wwlist,Tellist){
        var opts = $.extend({}, $.fn.Sonline.defualts, options);
		$.fn.setList(opts,qqlist,wwlist,Tellist); //调用列表设置
		if(opts.DefaultsOpen == false){
			$.fn.Sonline.close(opts.Position,0);
		}
		//展开
		$("#SonlineBox > .openTrigger").live("click",function(){$.fn.Sonline.open(opts.Position);});
		//关闭
		$("#SonlineBox > .contentBox > .closeTrigger").live("click",function(){$.fn.Sonline.close(opts.Position,"fast");});
		
		//Ie6兼容或滚动方式显示
		if ($.browser.msie && ($.browser.version == "6.0") && !$.support.style||opts.Effect==true) {$.fn.Sonline.scrollType();}
		else if(opts.Effect==false){$("#SonlineBox").css({position:"fixed"});}
	}
	//plugin defaults
	$.fn.Sonline.defualts ={
		Position:"left",//left或right
		Top:200,//顶部距离，默认200px
		Effect:true, //滚动或者固定两种方式，布尔值：true或
		DefaultsOpen:true, //默认展开：true,默认收缩：false
		Qqlist:"" //多个QQ用','隔开，QQ和客服名用'|'隔开
	}
	
	//展开
	$.fn.Sonline.open = function(positionType){
		var widthValue = $("#SonlineBox > .contentBox").width();
		if(positionType=="left"){$("#SonlineBox > .contentBox").animate({left: 0},"fast");}
		else if(positionType=="right"){$("#SonlineBox > .contentBox").animate({right: 0},"fast");}
		$("#SonlineBox").css({width:widthValue+4});
		$("#SonlineBox > .openTrigger").hide();
	}

	//关闭
	$.fn.Sonline.close = function(positionType,speed){
		$("#SonlineBox > .openTrigger").show();
		var widthValue =$("#SonlineBox > .openTrigger").width();
		var allWidth =(-($("#SonlineBox > .contentBox").width())-6);
		if(positionType=="left"){$("#SonlineBox > .contentBox").animate({left: allWidth},speed);}
		else if(positionType=="right"){$("#SonlineBox > .contentBox").animate({right: allWidth},speed);}
		$("#SonlineBox").animate({width:widthValue},speed);
		
	}

	//子插件：设置列表参数
	$.fn.setList = function(opts,qqlist,wwlist,Tellist){
		$("body").append("<div class='SonlineBox' id='SonlineBox' style='top:-600px;'><div class='openTrigger' style='display:none' title='展开'></div><div class='contentBox'><div class='closeTrigger'><img src='/Ajax/QQStyle/2/images/closeBtnImg.gif' title='关闭' /></div><div class='titleBox'><span>客服中心</span></div><div class='listBox'></div></div></div>");
		if(opts.Qqlist==""){$("#SonlineBox > .contentBox > .listBox").append("<p style='padding:15px'>暂无在线客服。</p>")}
		else{var qqListHtml = $.fn.Sonline.splitStr(qqlist,wwlist,Tellist);$("#SonlineBox > .contentBox > .listBox").append(qqListHtml);	}
		if(opts.Position=="left"){$("#SonlineBox").css({left:0});}
		else if(opts.Position=="right"){$("#SonlineBox").css({right:0})}
		$("#SonlineBox").css({top:opts.Top});
		var allHeights=0;
		if($("#SonlineBox > .contentBox").height() < $("#SonlineBox > .openTrigger").height()){
			allHeights = $("#SonlineBox > .openTrigger").height()+4;
		} else{allHeights = $("#SonlineBox > .contentBox").height()+4;}
		$("#SonlineBox").height(allHeights);
		if(opts.Position=="left"){$("#SonlineBox > .openTrigger").css({left:0});}
		else if(opts.Position=="right"){$("#SonlineBox > .openTrigger").css({right:0});}
	}
	
	//滑动式效果
	$.fn.Sonline.scrollType = function(){
		$("#SonlineBox").css({position:"absolute"});
		var topNum = parseInt($("#SonlineBox").css("top")+"");
		$(window).scroll(function(){
			var scrollTopNum = $(window).scrollTop();//获取网页被卷去的高
			$("#SonlineBox").stop(true,true).delay(0).animate({top:scrollTopNum+topNum},"slow");
		});
	}
	
	//分割QQ
	$.fn.Sonline.splitStr = function(qqlist,wwlist,Tellist){
	    var qqlist=eval (qqlist);
	    var wwlist=eval (wwlist);
	    var Tellist=eval (Tellist);
		var QqHtml="";
		for (var i=0;i<qqlist.length;i++){	
			QqHtml += "<div class='QQList'><span>"+qqlist[i].title+"：</span><a target='_blank' href='http://wpa.qq.com/msgrd?v=3&uin="+qqlist[i].No+"&site=qq&menu=yes'><img border='0' src='http://wpa.qq.com/pa?p=2:"+qqlist[i].No+":41 &amp;r=0.22914223582483828' alt='点击这里'></a></div>"
		}
		for (var j=0;j<wwlist.length;j++){	
			QqHtml += "<div class='QQList'><span>"+wwlist[j].title+"：</span><a target='_blank' href='http://amos.im.alisoft.com/msg.aw?v=2&uid="+wwlist[j].No+"&site=cntaobao&s=2&charset=utf-8'><img border='0' src='http://amos.im.alisoft.com/online.aw?v=2&uid="+wwlist[j].No+"&site=cntaobao&s=2&charset=utf-8' alt='点击这里'></a></div>"
		}
		for (var k=0;k<Tellist.length;k++){	
		    var vTel=Tellist[k].title.Trim();
		    var vTel=vTel==""?"电话":vTel;
			QqHtml += "<div class='QQList'><span>"+vTel+"：</span>"+Tellist[k].No+"</div>"
		}
		return QqHtml;
	}
})(jQuery);
String.prototype.Trim = function()
{
	return this.replace( /(^\s*)|(\s*$)/g, '' ) ;
}
    


 