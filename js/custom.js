jQuery('.bg-image').each(function(){
    var url = jQuery(this).attr("data-image");
    if(url){
        jQuery(this).css("background-image", "url(" + url + ")");
    }
});
jQuery(window).load(function() {
    jQuery('#preloader').slideUp();
});
jQuery('body').on('mousewheel', function(e){
    if(jQuery(window).width() <= 767){
        if(e.originalEvent.wheelDelta > 0) {
            jQuery('.custom_nav .navbar').css('padding', '10px 0px');
        }
        else {
            jQuery('.custom_nav .navbar').css('padding', '8px 0');
        }
    } else {
        if(e.originalEvent.wheelDelta > 0) {
            jQuery('.custom_nav .navbar').css('padding', '25px 0px');
        }
        else {
            jQuery('.custom_nav .navbar').css('padding', '8px 0');
        }
    }
});
//jQuery(window).load(function(){
//    var heightofWindow = jQuery(window).height();
//    jQuery('body').css('height', heightofWindow+ 'px');
//});