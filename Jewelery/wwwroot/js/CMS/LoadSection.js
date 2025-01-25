$(document).ready(function () {
    
    $(".ListLink li a").click(function (e) {
        var url = $('.ListLink').data("url");
        var section = $(this).data("section");
        $.ajax({
            url: url,
            type: 'GET',
            data: { section: section },
            success: function (result) {
                $(".Content").html(result);
            }
        })
    })
})