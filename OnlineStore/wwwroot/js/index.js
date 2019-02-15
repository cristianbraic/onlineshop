$(document).ready(function () {
    var x = 0;
    var s = "";

    console.log("Hello bia");

    var theForm = $("#theForm");
    theForm.show();

    var button = $("#buyButton");
    button.on("click", function () {
        console.log("Produs adaugat in cos");
    });

    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("you clicked on " + $(this).text());
    });

    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click", function () {
        $popupForm.toggle();
    });

});