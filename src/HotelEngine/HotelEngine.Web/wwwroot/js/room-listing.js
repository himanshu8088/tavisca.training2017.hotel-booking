$(document).ready(function () {   
    $.ajax({
        url: "../hotel/roomsearch",
        type: "POST",
        data: createRoomSearchRQ(),
        contentType: "application/json",
        success: function (roomSearchRS) {
            renderRooms(roomSearchRS);            
        },
        error: function (xhr) {
            alert("Sorry server doesn't responding. Please try again.");
            window.location = '../html/hotel-listing.html';
        }
    });
});

function createRoomSearchRQ() {
    var roomSearchCriteria = JSON.parse(sessionStorage.getItem('roomSearchCriteria')).data;
    var roomSearchRQ = JSON.stringify(roomSearchCriteria);
    return roomSearchRQ;
}

function renderRooms(roomSearchRS) {
    var template = $('#room-item');
    var compiledTemplate = Handlebars.compile(template.html());
    var html = compiledTemplate(roomSearchRS);
    $('#roomList-container').html(html);
}

function priceBtnClicked(hotelId, roomName) {
   
    renderPriceLoader();
    var priceSearchRQ = createPriceSearchRQ(roomName);

    $.ajax({
        url: "../hotel/price",
        type: "POST",
        data: priceSearchRQ,
        contentType: "application/json",
        success: function (priceSearchRS) {
            var extraCharge = (priceSearchRS.chargebleFare.totalFare - priceSearchRS.chargebleFare.baseFare).toFixed(2);            
            $.extend(priceSearchRS.chargebleFare, {
                "extraCharge": extraCharge
            });
            renderPricing(priceSearchRS);
        },
        error: function (xhr) {
            _roomResponse = {};
        }
    });
};


function renderPriceLoader() {
    $(".modal-body").css({ "height": "170px" });
    $('.btn-default').hide();
    $(".modal-body").html('<img id="load" src="../img/loading.svg" alt="loading..." />');
    $(".modal").modal("show");
}

function createPriceSearchRQ(roomName) {
    var priceRQ = JSON.parse(sessionStorage.getItem('roomSearchCriteria')).data;
    $.extend(priceRQ, {
        "RoomName": roomName
    });
    sessionStorage.setItem('roomPriceSearchCriteria', JSON.stringify(priceRQ));
    var priceSearchRQ = JSON.stringify(priceRQ);
    return priceSearchRQ;
}

function renderPricing(priceSearchRS) {
    var template = $('#price-template');
    var compiledTemplate = Handlebars.compile(template.html());
    var html = compiledTemplate(priceSearchRS);
    $(".modal .modal-title").html("Price Summary :");
    $(".modal .modal-body").html(html);
    $(".modal-body").css({ "height": "auto" });
    $(".modal").modal("show");
    $(".modal-footer").show();
    $('.btn-default').show();
    $('#proceed').click(function () {
        window.location = '../html/book.html';
    });
}

Handlebars.registerHelper('ifCond', function (v1, v2, options) {
    if (v1 != v2) {
        return options.fn(this);
    }
    return options.inverse(this);
});