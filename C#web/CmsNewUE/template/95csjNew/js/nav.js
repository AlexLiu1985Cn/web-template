$(document).ready(function a(){
	var topnav = $('.topnav'),btn = $('.subtopright'),topright = $('.topright'),toprights = $('.toprights');
	if(topnav.is(":hidden")){
		btn.click(function(){
			topnav.show();
			btn.children('a').addClass('show');
			btn.click(a);
		})
		topright.click(function(){
			topnav.show();
			topnav.css('height','155px');
			toprights.children('a').addClass('show');
			topright.click(a);
			$('.logo').css('top','375px');
		})
		
		}else{
			topnav.hide();
			btn.children('a').removeClass('show');
			toprights.children('a').removeClass('show');
			$('.logo').css('top','170px');
		}
});