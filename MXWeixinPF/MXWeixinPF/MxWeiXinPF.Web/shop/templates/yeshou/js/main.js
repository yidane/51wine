/**
 * Created by xr.kong on 14-5-12.
 */
$(document).ready(function(){

    /*菜单栏--二级菜单展开、收起*/
    $('.J_MenuList>li').each(function(e){
        ($(this).find('.J_SubMenu').data('display') == undefined)  && $(this).find('.J_SubMenu').data('display','closed');
        $(this).on('click', function(){
            var $this = $(this);
            var isShow = $this.find('.J_SubMenu').data('display');
            isShow == 'open' ?  $this.find('.J_SubMenu').css({display:'none'}).data('display','closed') : $this.find('.J_SubMenu').css({display:'block'}).data('display','open');
            $this.siblings().find('.J_SubMenu').css({display:'none'}).data('display','closed');
        });
    });
    $('.J_MenuList').css({height:$('body').height()-105+'px'});


    /*菜单显示、隐藏*/
    $('#J_MenuShow').on('click', function(e){
        e.preventDefault();
        $(this).data('sub','open'),menuOpen();
    });
    $('#J_Mask').on('click', function(e){
        e.preventDefault();
        $(this).data('sub','closed'),menuCLose();
    });
    $('#J_Mask').on('swipeLeft', menuCLose);
    $('#J_MenuSwipe').on('swipeRight', menuOpen);
    function menuOpen(){
        $('#J_Mask').css({ opacity: 0.7 });
        $('#J_Wrappear').css({overflow:'hidden'}), $('.J_Menu').css({display:'block'});
        $('.J_MenuBody').css({ '-webkit-animation':'swipeleft 0.2s ease-in forwards'});
        $('#J_Mask').css({display:'block'})
    }
    function menuCLose(){
        $('#J_Mask').css({ display: 'none' });
        $('#J_Wrappear').css({overflow:'initial'});
        $('.J_MenuBody').css({  opacity:1, '-webkit-animation':'swiperight 0.2s ease-in forwards'});
        setTimeout(function(){ $('.J_Menu').css({ display: 'none' }) },200);
    }

    /*触摸屏向下滑动隐藏/向上滑动显示*/
    (function(){
        var startX, startY, endX, endY, mainbody = $('#J_MainBody').get(0);
        mainbody.addEventListener('touchstart',function(event){
            startX = event.changedTouches[0].pageX;
            startY = event.changedTouches[0].pageY;
        });
        mainbody.addEventListener('touchend', function(event){
            endX = event.changedTouches[0].pageX;
            endY = event.changedTouches[0].pageY;
            if( (startY-endY) > 50 ){
                $('#J_SubHead').css({top:0});
            }else if( (startX-endY) < -50 ){
                $('#J_SubHead').css({top:'45px'});
            }
        })
    })();

});
/**
 * Created by kongxiangran on 14-12-30.
 * @param type 传入类型，值为‘success’或 ‘error’;
 * @param msg 传入消息，为字符串类型;
 * @param times 时间值，为数字类型；
 */
(function ($){
    function Tips(opts){
        this.type = opts.type;
        this.msg = opts.msg;
        this.times = opts.times;
        this.init();
    }
    Tips.prototype = {
        init : function(){
            var fontColor = '',bgColor = '',borderColor = '', src='',html='';
            if( this.type == 'success' ){
                fontColor = '#3d810c',bgColor = '#dff0d8',borderColor = '#89ca55', src='&quot;images/success-bg.png&quot;';
            }else{
                fontColor = '#ff3f00',bgColor = '#ffe5dc',borderColor = '#ec8561', src='&quot;images/error-bg.png&quot;';
            }
            html = '<p class="J_Tips" style="position: fixed; top: 30%; left: 5%; z-index: 99999; width: 90%; padding: 12px 0;background: '+bgColor+'; font: 14px &quot;Microsoft yahei&quot;, Arial, serif;border-radius: 6px;border: 1px solid '+borderColor+';box-shadow: 0 0 10px #acacac;color: '+fontColor+';text-align: center;line-height: 30px;"><span style="background: url('+src+') no-repeat 0 0;width: 30px;height: 30px;display: inline-block;background-size: contain;vertical-align: middle;margin-right: 10px;margin-top: -3px;"></span>'+this.msg+'</p>';
            this.show(html);
        },
        show : function(html){
            var that = this;
            $('body').append(html);
            setTimeout(function(){
                that.hide();
            }, this.times);
        },
        hide : function(){
            $('.J_Tips').remove();
        }
    };
    $.tips = function (opts){
        opts = $.extend({
            type:'error',
            msg : '请检查！',
            times : 2000
        },opts);
        return new Tips(opts);
    };
})($);
/*阻止屏幕旋转(暂时无用)*/
window.addEventListener('orientationchange', function(event){
    event.preventDefault();
    return false;
});
