$(".sidebar-toggle").click(function(e) {
	e.preventDefault();
    $("#sidebar").toggleClass("toggled");
    $("#sidebar-closebox").toggleClass("toggled");
});

$("#sidebar a").click(function(e) {
   $("#sidebar").toggleClass("toggled");
   $("#sidebar-closebox").toggleClass("toggled");
});

$(".sidebar-view-toggle").click(function(e) {
	e.preventDefault();
    $("body").toggleClass("sidebar-icon-view");
});

$(".sidebar-right-active").click(function(e) {
	e.preventDefault();
    $(".sidebar-right").addClass("toggled");
    $(".dash-main-content").addClass("toggled");
    $(".sidebar-right-active").addClass("active");
    $(".sidebar-right-deactive").removeClass("active");
});

$(".sidebar-right-deactive").click(function(e) {
	e.preventDefault();
    $(".sidebar-right").removeClass("toggled");
    $(".dash-main-content").removeClass("toggled");
    $(".sidebar-right-active").removeClass("active");
    $(".sidebar-right-deactive").addClass("active");
});

$(".sidebar-right-toggle").click(function(e) {
	e.preventDefault();
    $(".sidebar-right").toggleClass("toggled");
    $(".dash-main-content").toggleClass("toggled");
});


$(function () {
    $("[data-toggle='tooltip']").tooltip();
});
$(function () {
  $('[data-toggle="popover"]').popover()
});