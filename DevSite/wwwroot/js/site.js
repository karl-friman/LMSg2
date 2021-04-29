﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function scrollToSelected(){
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

const urlParams = new URLSearchParams(window.location.search);
const notification = urlParams.get('selected');

if (notification == "true") {
    alert("here");
    scrollToSelected();
}
