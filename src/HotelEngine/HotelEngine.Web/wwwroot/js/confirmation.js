
var bookingResponse = {};

function restoreSessionData() {
    bookingResponse = JSON.parse(sessionStorage.getItem('bookConfirmationInfo'));
}

$(document).ready(function () {
    restoreSessionData();
    $.extend(bookingResponse, {
        "date": "",
        "chargedFare": ""
    });  
    var compiledTemplate = Handlebars.compile($('#confirmation-template').html());
    var html = compiledTemplate(bookingResponse);
    $('#confirmation-section').html(html);
});