// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
let poster = document.getElementById("Poster");
let img = document.getElementById("image");
let posterContainer = document.getElementById("poster-container");


// Function To Display image Which came from Input Type File "UploadFile"
poster.onchange = function () {
    console.log(poster.value);
    var image = window.URL.createObjectURL(this.files[0]);
    posterContainer.classList.remove('d-none');
    img.src = image;
    console.log(image);
}

// DatePicker Library
//let year = document.getElementById("Year");
//year.datepicker({
//    format: "dd.mm.yyyy",
//    todayBtn: "linked",
//    language: "de"
//    });
$(document).ready(function () {
    $('#Year').datepicker({
        format: "yyyy",
        viewMode: "years",
        minViewMode: "years",
        autoclose: true,
        startDate: new Date("1950-01-01"),
        endDate: new Date()
        });
});
