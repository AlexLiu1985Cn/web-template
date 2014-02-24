$(document).ready(function(){
	$(function(){
		//little 
		$('.product ul li:last').css('margin','0');
		$('.prolist ul li:odd div').css('margin-right','0');
		$('.footnav ul li:last').css('margin-right','0');
		$('.tj_pro a:last').css('margin-top','2%');
		
		$(".pronav span").click(function(){
			$(".pronavmain").slideToggle(500);	//逐渐的显隐
			return false
		});
		$(".view_menu span").click(function(){
			$(".view_menumain").toggle(500);	//逐渐的显隐
			return false
		});
		
		$(".promoremain .one").click(function()
    {
		$(this).next("div").slideToggle(300).siblings("div").slideUp("slow");
//       	$(this).siblings("li").css({background:"url(images/down.png) no-repeat 95% center #51612a"});
	});
		
	});
});

$(document).ready(function() {
	var numb = $('.box ul li').length;
		ulwid = ( numb * 50 ) + '%';
		liwid = ( 100 / numb ) + '%';
		boxul = $('.box ul');
		boxli = $('.box li');
	function allwidth(){
		boxul.css("width",ulwid);
		boxli.css("width",liwid);
	}
	allwidth();
	var leftcli = 1;
		rightcli = -1;
		numb2 = numb - 1;
		marginleft = - 50 ;
		marginright = marginleft + 50;
	$('.leftbtn').click(function(){
		if(leftcli < numb2){
			boxul.animate({marginLeft:marginleft + '%'},'500');
			leftcli = leftcli + 1;
			rightcli = rightcli + 1;
			marginleft  = (-1) * leftcli * 50 ;
			return leftcli,rightcli;
		}
	});
	$('.rightbtn').click(function(){
		if(rightcli >= 0){
			marginright  = marginleft + 100;
			marginleft = marginleft + 50;
			boxul.animate({marginLeft:marginright + '%'},'500');
			rightcli = rightcli - 1;
			leftcli = leftcli - 1;
			return rightcli,leftcli;
		}
	});
});
	