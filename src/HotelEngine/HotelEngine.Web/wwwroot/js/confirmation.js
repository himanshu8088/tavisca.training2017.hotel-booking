
var bookingRQ = {};

function restoreSessionData() {
    bookingRQ = JSON.parse(sessionStorage.getItem('bookingRQ'));
}

$(document).ready(function () {
    restoreSessionData();

    $.ajax({
        url: "../hotel/book",
        type: "POST",
        data: JSON.stringify(bookingRQ),
        contentType: "application/json",
        success: function (bookingRS) {
            var compiledTemplate = Handlebars.compile($('#confirmation-template').html());
            var html = compiledTemplate(bookingRS);
            if (bookingRS.status == 0)
                $('#success').show();
            else
                $('#fail').show();

            $('#confirmation-section').html(html);
        }
    });
});

function printBookingTicket() {
    window.print();
};

function home() {
    window.location = '../html/search.html';
};

