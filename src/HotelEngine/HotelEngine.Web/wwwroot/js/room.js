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
            var extraCharge = resp.chargebleFare.totalFare - resp.chargebleFare.baseFare;
            var rondedValue = extraCharge.toFixed(2);
            $.extend(resp.chargebleFare, {
                "rondedValue": rondedValue
            });
            var template = $('#price-template');
            var compiledTemplate = Handlebars.compile(template.html());
            var html = compiledTemplate(resp);
            $(".modal .modal-title").html("Price Summary :");
            $(".modal .modal-body").html(html);
            $(".modal").modal("show");
            $('#proceed').click(function () {
                window.location = '../html/book.html';   
            });
        },
        error: function (xhr) {
            _roomResponse = {};
        }
    });
};
