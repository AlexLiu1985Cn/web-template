//控制菜单出现隐藏
$(function () {
	/*
	$('img').each(function () {
        var dataSrc = $(this).attr("data-src");
        if (dataSrc) {
            $(this).attr("src", dataSrc).removeAttr("data-src")
        }
    });
	*/
    $('img').unveil(300);
    $("#nav_wrapper,#divMask").fix({ top: 0, left: 0 });
    $("#menu_Btn").fix({ bottom: 10, left: 10 });
    $("#divMask,.nav_tit,.nav_wrapper").hide();
    $('#Nav_btn,#divCloseNav,#divMask').click(function () {//导航隐藏触发事件
        Out_ceng();
        //location.href = "/menu.aspx";
    });
    $("#Top_btn,#menu_Btn,#top_Back_btn").click(function () {//导航出现触发事件
        In_ceng();
        //location.href = "/menu.aspx";
    })
    $(window).trigger('scroll');
})
var IsOpenMenu = false;
function In_ceng() {//导航出现
    IsOpenMenu = true;
    $("body").css({ overflow: "hidden" });
    $(".nav_tit,.nav_wrapper,#divMask").show();
    if ($(window).height() < $("#scroller").height()) $("#nav_wrapper").css({ "overflow-y": "visible" });
    //$("#header,#wrapper,#divMask").attr("style", "overflow:hidden;-webkit-transition: 400ms;transition: 400ms; -webkit-transform: translateX(260px);");
    $("#divMask").height($(document).height());
    $('#menu_Btn').fadeOut(500);
    //$("#wrapper").bind("click", function (e) {
    //    Out_ceng();
    //    return false;
    //});
};
function Out_ceng() {//导航隐藏
    IsOpenMenu = false;
    //$("#header,#wrapper").attr("style", "-webkit-transition: 400ms;transition: 400ms; -webkit-transform: translateX(0px);");
    $(".nav_tit,.nav_wrapper,#divMask").hide();
    //setTimeout(function () {
        $("body,#wrapper").removeAttr("style");
    //}, 400);
    $('#menu_Btn').fadeIn(500);
    //$("#wrapper").unbind("click");
};
function openmenu() {
    if (IsOpenMenu == false) In_ceng();
    else Out_ceng();
}
(function ($, undefined) {
    $.extend($.fn, {
        fix: function (opts) {
            var me = this;                      //如果一个集合中的第一元素已fix，则认为这个集合的所有元素已fix，
            if (me.attr('isFixed')) return me;   //这样在操作时就可以针对集合进行操作，不必单独绑事件去操作
            me.css(opts).css('position', 'fixed').attr('isFixed', true);
            var buff = $('<div style="position:fixed;top:10px;"></div>').appendTo('body'),
                top = buff[0].getBoundingClientRect().top,
                checkFixed = function () {
                    if (window.pageYOffset > 0) {
                        if (buff[0].getBoundingClientRect().top !== top) {
                            me.css('position', 'absolute');
                            doFixed();
                            $(document).on('scrollStop', doFixed);
                            $(window).on('ortchange', doFixed);
                        }
                        $(document).off('scrollStop', checkFixed);
                        buff.remove();
                    }
                },
                doFixed = function () {
                    me.css({
                        top: window.pageYOffset + (opts.bottom !== undefined ? window.innerHeight - me.height() - opts.bottom : (opts.top || 0)),
                        left: opts.right !== undefined ? document.body.offsetWidth - me.width() - opts.right : (opts.left || 0)
                    });
                    opts.width == '100%' && me.css('width', document.body.offsetWidth);
                };
            $(document).on('scrollStop', checkFixed);
            return me;
        }
    });
}(jQuery));
function GetWidth(ele) {
    //var defaultView = (ele.ownerDocument || document).defaultView;
    //if (defaultView && defaultView.getComputedStyle) {
    //    var eleComputedStyle = defaultView.getComputedStyle(ele, null);
    //    var width = parseInt(eleComputedStyle.width), height = parseInt(eleComputedStyle.height);
    //    var parentElem = ele.parentNode.parentNode;
    //    if (parentElem != undefined && parentElem.nodeType == 1) {
    //        var computedStyle = defaultView.getComputedStyle(parentElem, null);
    //        var eleWidth = parseInt(computedStyle.width), eleHeight = parseInt(computedStyle.height);
    //        if (eleHeight > height) {
    //            ele.style.marginTop = (eleHeight - height) / 2 + "px";
    //        }
    //        else {
    //            if (width > height) {
    //                ele.style.height = eleHeight + "px";
    //                var minWidth = parseInt(eleComputedStyle.width);
    //                ele.style.marginLeft = -(minWidth - eleWidth) / 2 + "px";
    //            }
    //            else if (width < height) {
    //                ele.style.width = eleWidth + "px";
    //                var minHeight = parseInt(eleComputedStyle.height);
    //                ele.style.marginTop = -(minHeight - eleHeight) / 2 + "px";
    //            }
    //            else if (width == height) {
    //                ele.style.maxWidth = eleWidth + "px";
    //            }
    //        }
    //    }
    //}
}