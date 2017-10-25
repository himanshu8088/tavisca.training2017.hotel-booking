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
            var amenityId = "amenities";/* + resp.sessionId;*/
            var amenitiesData = sessionStorage.getItem(amenityId);
            var am = JSON.parse(amenitiesData);
            $('#amenities').html();
        },
        error: function (xhr) {
            alert("Sorry server doesn't responding. Please try again.");
            window.location = '../html/hotel-listing.html';
        }
    });
});
function price(hotelId, roomName) {
    $(".modal-body").css({ "height": "300px" });
    $(".modal-footer").hide();
    $(".modal-body").html('<img id="load" src="../img/loader.gif" alt="loading..." />');    
    $(".modal").modal("show");
    var priceRQ = JSON.parse(sessionStorage.getItem('roomSearchCriteria')).data;
    $.extend(priceRQ, {
        "RoomName": roomName
    });
    sessionStorage.setItem('roomPriceSearchCriteria', JSON.stringify(priceRQ));
    var jsonData = JSON.stringify(priceRQ);

    $.ajax({
        url: "../hotel/price",
        type: "POST",
        data: jsonData,
        contentType: "application/json",
        success: function (resp) {
            var extraCharge = resp.chargebleFare.totalFare - resp.chargebleFare.baseFare;
            var roundedValue = extraCharge.toFixed(2);
            $.extend(resp.chargebleFare, {
                "roundedValue": roundedValue
            });
            var template = $('#price-template');
            var compiledTemplate = Handlebars.compile(template.html());
            var html = compiledTemplate(resp);
            $(".modal .modal-title").html("Price Summary :");
            $(".modal .modal-body").html(html);
            $(".modal-body").css({ "height": "auto" });
            $(".modal").modal("show");
            $(".modal-footer").show();
            $('#proceed').click(function () {
                window.location = '../html/book.html';
            });
        },
        error: function (xhr) {
            _roomResponse = {};
        }
    });
};