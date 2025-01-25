$(window).on("load", function () {
    var screenSize = window.innerWidth < 768 ? "true" : "false";
    document.cookie = "IsMobile=" + screenSize + "; path=/";
});

$(window).on("resize", function () {
    var screenSize = window.innerWidth < 768 ? "true" : "false";
    document.cookie = "IsMobile=" + screenSize + "; path=/";
});
