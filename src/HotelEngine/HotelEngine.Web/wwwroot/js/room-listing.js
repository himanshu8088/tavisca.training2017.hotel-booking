$(document).ready(function () {
    var roomProvider = new RoomProvider();
    var renderer = new Renderer();
    var rooms = roomProvider.fetchRooms();
    renderer.renderRooms(rooms);
});

function RoomProvider() {
    this.fetchRooms = function () {
        var rooms;
        $.ajax({
            url: "../hotel/roomsearch",
            type: "POST",
            data: createRoomSearchRQ(),
            contentType: "application/json",
            success: function (roomSearchRS) {
                rooms = roomSearchRS;
                //renderRooms(roomSearchRS);
            },
            error: function (xhr) {
                alert("Sorry server doesn't responding. Please try again.");
                window.location = '../html/hotel-listing.html';
            }
        });
        return rooms;
    }
}

function priceBtnClicked(hotelId, roomName) {
    var pricing = new Pricing();
    var renderer = new Renderer();
    renderer.renderPriceLoader();
    pricing.getPrice(roomName);
    pricing.setPrice();
    renderer.renderPricing();
};

function Pricing() {
    this.getPrice = function (roomName) {
        var priceRQ = JSON.parse(sessionStorage.getItem('roomSearchCriteria')).data;
        $.extend(priceRQ, {
            "RoomName": roomName
        });
        sessionStorage.setItem('roomPriceSearchCriteria', JSON.stringify(priceRQ));
        var priceSearchRQ = JSON.stringify(priceRQ);
        return priceSearchRQ;
    }
    this.setPrice = function () {
        var price;
        var priceSearch = $.ajax({
            url: "../hotel/price",
            type: "POST",
            data: priceSearchRQ,
            contentType: "application/json",
            success: function (priceSearchRS) {
                var extraCharge = (priceSearchRS.chargebleFare.totalFare - priceSearchRS.chargebleFare.baseFare).toFixed(2);
                $.extend(priceSearchRS.chargebleFare, {
                    "extraCharge": extraCharge
                });
                price = priceSearchRS;
            },
            error: function (xhr) {
                _roomResponse = {};
            }
        });
        return price;
    }
}

function Renderer() {

    this.renderPriceLoader = function () {
        $(".modal-body").css({ "height": "170px" });
        $('.btn-default').hide();
        $(".modal-body").html('<img id="load" src="../img/loading.svg" alt="loading..." />');
        $(".modal").modal("show");
    }

    this.renderPricing = function (priceSearchRS) {
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

    this.renderRooms = function (rooms) {
        var template = $('#room-item');
        var compiledTemplate = Handlebars.compile(template.html());
        var html = compiledTemplate(rooms);
        $('#roomList-container').html(html);
    }
}


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



Handlebars.registerHelper('ifCond', function (v1, v2, options) {
    if (v1 != v2) {
        return options.fn(this);
    }
    return options.inverse(this);
});