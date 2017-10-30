
var bookingRQ = {};

function restoreRoomPriceRQ() {
    bookingRQ = JSON.parse(sessionStorage.getItem('bookingRQ'));
}

$(document).ready(function () {
    restoreRoomPriceRQ();

    $.ajax({
        url: "../hotel/book",
        type: "POST",
        data: JSON.stringify(bookingRQ),
        contentType: "application/json",
        success: function (bookingRS) {
            var compiledTemplate = Handlebars.compile($('#confirmation-template').html());
            var html = compiledTemplate(bookingRS);
            $('#confirmation-section').html(html);
            if (bookingRS.status == 0) {
                $('.success').show();                
                $('.confirmation-row').show();
            }                
            else
                $('.fail').show();
            
        },
        error: function (xhr) {
            alert("Sorry server doesn't responding. Please try again.");
            window.location = '../html/book.html';
        }
    });
});

function printBookingTicket() {
    window.print();
};

function home() {
    window.location = '../html/search.html';
};

