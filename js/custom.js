$(function () {
       
    var pageID = location.hash;
    pageID = pageID.replace("#", "");
    if (pageID != "home" && pageID != "services" && pageID != "insurance" && pageID != "info" && pageID != "contact" && pageID != "thankyou")
    {
        pageID = "home";
    } 
    $(".page-" + pageID).fadeIn();
     
    $(".navmenu a, .navlink").click(function () {
        var visiblePage;  
        visiblePage = "page-" + $(this).attr("rel");
        //$(".content").css("background-image", "url(/images/design/header." + visiblePage.replace("page-", "") + ".jpg)");       
        
        $(".pages").fadeOut('slow');
        $(".navmenu a").removeClass("active");
        $(this).addClass("active");
        $("." + visiblePage).fadeIn('slow');
    });
});