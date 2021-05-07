// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const urlParams = new URLSearchParams(window.location.search);
const notification = urlParams.get('notify');

function scrollToSelected() {
    if (document.getElementById("selected")) {
        $('html, body').animate({
            scrollTop: $('#selected').offset().top
        }, 'slow');
    }
}

$(document).ready(function () {
    // Handler for .ready() called.
    scrollToSelected();
});

$(function () {
    $("#effect").hide();

    var options = {};
    $("#effect").show("fade", options, 600);
});


$(document).ready(function () {

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

});

