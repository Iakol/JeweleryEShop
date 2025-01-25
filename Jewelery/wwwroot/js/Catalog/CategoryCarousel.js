$(document).ready(function () {
    var $carousel = $('.CategoryListCarusel');
        $carousel.slick({
            dots: false,
            infinite: false,
            speed: 300,
            swipeToSlide: true,
            slidesToShow: 6,
            prevArrow: '<button type="button" class="slick-prev"><</button>',
            nextArrow: '<button type="button" class="slick-next">></button>',
            slidesToScroll: 1,
            responsive: [
                {
                    breakpoint: 1024,
                    settings: {
                        slidesToShow: 4, // Или другое значение, которое лучше подходит
                        slidesToScroll: 1,
                        infinite: false,
                        dots: false,
                        swipeToSlide: true
                    }
                },
                {
                    breakpoint: 600,
                    settings: {
                        slidesToShow: 3, // Меньшее количество для мобильных
                        slidesToScroll: 1
                    }
                },
                {
                    breakpoint: 480,
                    settings: {
                        slidesToShow: 2, // Меньшее количество для мобильных
                        slidesToScroll: 1
                    }
                }
            ]
        });
    /*}*/
    // else
    //{
    //    $(".CategoryListCarusel div").each(function ()
    //    {
    //        $(this).toggleClass("Notslick-slide");
                

    //    })
    //}
});
