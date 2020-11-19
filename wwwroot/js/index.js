$(document).ready(function () {
    x = 0;
    var s = "";

    console.log("Hello Pluraldignt");

    var theForm = $("#theForm");
    theForm.hide();

    var button = $("#buyButton");
    button.on("click", function () {
        console.log("Buying Item");
    });

    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("You clicled on " + $(this).text());
    });

    var $loginToogle = $("#loginToogle");
    var $popupForm = $(".popup-form");

    $loginToogle.on("click", function () {
        $popupForm.fadeToggle(1000);
    });

});  


