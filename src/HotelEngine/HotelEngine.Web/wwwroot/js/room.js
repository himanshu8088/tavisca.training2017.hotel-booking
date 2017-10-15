Handlebars.registerHelper('ifCond', function (v1, v2, options) {
    if (v1 != v2) {
        return options.fn(this);
    }
    return options.inverse(this);
});

$(document).ready(function () {
    var searchCriteria = JSON.parse(sessionStorage.getItem('roomSearchCriteria')).data;
    var jsonData = JSON.stringify(searchCriteria);
    $.ajax({
        url: "../hotel/roomsearch",
        type: "POST",
        data: jsonData,
        contentType: "application/json",
        success: function (resp) {
            var template = $('#room-item');
            var compiledTemplate = Handlebars.compile(template.html());
            var html = compiledTemplate(resp);
            $('#roomList-container').html(html);
            $('#hotel-name').html(searchCriteria.SearchText);
        },
        error: function (xhr) {
            _searchResponse = {};
        }
    });       
});
function price(hotelId,roomName) {
    var priceRQ = JSON.parse(sessionStorage.getItem('roomSearchCriteria')).data;
    $.extend(priceRQ, {
        "RoomName" :roomName
    });
    var jsonData = JSON.stringify(priceRQ);
    $.ajax({
        url: "../hotel/price",
        type: "POST",
        data: jsonData,
        contentType: "application/json",
        success: function (resp) {
            $("#priceModal").modal().find('#price').html();
            
        },
        error: function (xhr) {
            _roomResponse = {};
        }
    });
};
function book(hotelId) {

    $("#priceModal").modal();
};