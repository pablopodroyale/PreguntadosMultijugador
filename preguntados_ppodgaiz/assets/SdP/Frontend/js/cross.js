$(document).ready(function () {
    $('#index').owlCarousel({
        loop: true,
        nav: true,
        navText: ['<i class="pe-7f-angle-left"></i>', '<i class="pe-7f-angle-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 3
            }
        }
    })

    $('#carousel-product-detail').owlCarousel({
        loop: true,
        nav: true,
        navText: ['<i class="pe-7f-angle-left"></i>', '<i class="pe-7f-angle-right"></i>'],
        responsive: {
            0: {
                items: 2
            },
            992: {
                items: 3
            }
        }
    })

    $('#checkout-carousel').owlCarousel({
        loop: true,
        nav: true,
        navText: ['<i class="pe-7f-angle-left"></i>', '<i class="pe-7f-angle-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 3
            }
        }
    })


});


//Script para copiar código
new Clipboard('.btn-copy');

//Script de Bootstrap
$(function () {
    $("[data-toggle='tooltip']").tooltip();
});
$(function () {
    $('[data-toggle="popover"]').popover()
})

//<!-- Script para animación de navbar fixed -->

// get header height (without border)
var getHeaderHeight = $('.navbar-sticky-animated').outerHeight();

// init variable for last scroll position
var lastScrollPosition = 0;

// set negative top position to create the animated header effect
$('.navbar-sticky-animated').css('top', '-' + (getHeaderHeight) + 'px');

$(window).scroll(function () {
    var currentScrollPosition = $(window).scrollTop();

    if ($(window).scrollTop() > 2 * (getHeaderHeight)) {

        $('body').addClass('scrollActive').css('padding-top', getHeaderHeight);
        $('.navbar-sticky-animated').css('top', 0);

        if (currentScrollPosition < lastScrollPosition) {
            $('.navbar-sticky-animated').css('top', '-' + (getHeaderHeight) + 'px');
        }
        lastScrollPosition = currentScrollPosition;

    } else {
        $('body').removeClass('scrollActive').css('padding-top', 0);
    }
});


$(document).ready(function () {
    $(".carousel").on("touchstart", function (event) {
        var xClick = event.originalEvent.touches[0].pageX;
        $(this).one("touchmove", function (event) {
            var xMove = event.originalEvent.touches[0].pageX;
            if (Math.floor(xClick - xMove) > 5) {
                $(this).carousel('next');
            }
            else if (Math.floor(xClick - xMove) < -5) {
                $(this).carousel('prev');
            }
        });
        $(".carousel").on("touchend", function () {
            $(this).off("touchmove");
        });
    });
});

//<!-- Script Form Material Design -->
$(".form-material .form-control").focus(function () {
    $(this).closest('.form-group').addClass("is-focused");
}).blur(function () {
    $(this).closest('.form-group').removeClass('is-focused');
});

$('.form-control').blur(function () {
    if ($(this).val().length === 0) {
        $(this).closest('.form-group').addClass('is-empty');
        $(this).closest('.form-group').removeClass('is-fixed');
    }
    else {
        $(this).closest('.form-group').removeClass('is-empty');
        $(this).closest('.form-group').addClass('is-fixed');
    }
});

//<!-- Script para el navbar -->
$(document).ready(function () {
    $(window).scroll(function () {
        if ($(window).scrollTop() >= 100) {
            $('.navbar-animation-top').addClass('scroll');
        }
        else {
            $('.navbar-animation-top').removeClass('scroll');
        }
    });
});