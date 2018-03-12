$(document).ready(function () {
debugger;
var $theForm = $("#theForm");
$theForm.on("click", function () {
    $theForm.toggle(1000);
});

var $sendButton = $("#sendMessageButton");
$sendButton.on("click", function () {
    $theForm.toggle();
});

var button = $("#buyButton");
button.on("click", function () {
   alert("Buing Item");
});

var productInfo = $(".product-props li");
productInfo.on("click", function () {
    alert("You clicked on" + $(this).text());
});

var $loginToggle = $("#loginToggle");
var $popupForm = $(".popup-form");

$loginToggle.on("click", function () {
     $popupForm.slideToggle(500);
});

});

   
        
