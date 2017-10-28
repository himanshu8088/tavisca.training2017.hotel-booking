var _hotelSearchRS = {};
var _hotelSearchRQ = {};
var _perPageHotels = 10;
var _totalHotels;
var _paginationArgument;

Handlebars.registerHelper('times', function (n, block) {
    var accum = '';
    for (var i = 0; i < n; ++i)
        accum += block.fn(i);
    return accum;
});

$(document).ready(function () {
    restoreSessionData();
    searchHotels(JSON.stringify(_hotelSearchRQ));
    $('#filters').tooltip();
});

function restoreSessionData() {
    _hotelSearchRQ = JSON.parse(sessionStorage.getItem('hotelSearchCriteria')).data;
}

function searchHotels(hotelSearchRQ) {
    $.ajax({
        url: "../hotel/search",
        type: "POST",
        data: hotelSearchRQ,
        contentType: "application/json",
        success: function (hotelSearchRS) {
            _hotelSearchRS = hotelSearchRS;
            _totalHotels = hotelSearchRS.hotels.length;            
            $.extend(_hotelSearchRS, {
                "hotelsCount": _totalHotels
            });             
            renderPaginatedHotels(_hotelSearchRS.hotels, _totalHotels, _perPageHotels);
        },
        error: function (xhr) {
            alert("Sorry server doesn't responding. Please try again.");
            window.location = '../html/search.html';
        }
    });
}

function renderPaginatedHotels(hotels, totalHotels, perPageHotels) {
    
    _paginationArgument = {
        totalPages: Math.ceil(totalHotels / perPageHotels),
        visiblePages: perPageHotels,
        onPageClick: function (event, page) {
            var currentPageHotels = selectHotelsByPageNo(hotels, perPageHotels, page);
            renderHotelSection(_hotelSearchRQ, currentPageHotels, _hotelSearchRS.sessionId, totalHotels);
        }
    };

    if (totalHotels != 0) {
        $('.pagination-section').show();
        $('.pagination').twbsPagination(_paginationArgument);
    } else {
        renderHotelSection(_hotelSearchRQ, _hotelSearchRQ.hotels, _hotelSearchRS.sessionId, totalHotels);
    }
    
}

function selectHotelsByPageNo(hotels, perPageHotels, pageNo) {
    var firstIndex = (perPageHotels * pageNo) - perPageHotels;
    var lastIndex = (perPageHotels * pageNo);
    var currentPageHotels = hotels.slice(firstIndex, lastIndex);
    return currentPageHotels;
}

function renderHotelSection(hotelRQ, hotels, sessionId, hotelCount) {

    var hotelItinerary = {
        "sessionId": sessionId,
        "hotels": hotels,
        "hotelsCount": hotelCount
    };

    var templateData = { hotelRQ, hotelItinerary };
    var template = $('#hotel-item');
    var compiledTemplate = Handlebars.compile(template.html());
    var html = compiledTemplate(templateData);
    $('#hotelList-container').html(html);

}

function roomSearchRQ(checkIn, checkOut, latitude, longitude, guestCount, noOfRooms, hotelId, hotelName, sessionId) {
    this.data = {
        "SessionId": sessionId,
        "SearchText": hotelName,
        "CheckInDate": checkIn,
        "CheckOutDate": checkOut,
        "Location": {
            "Latitude": latitude,
            "Longitude": longitude
        },
        "NoOfRooms": noOfRooms,
        "GuestCount": guestCount,
        "HotelId": hotelId
    }
};

//function loadMap(latitude, longitude) {
//    var url = "https://www.google.com/maps/embed/v1/place?key=AIzaSyCIYzzzQZGLWDSOSovKWaq2UsyX1dQ796c&q="+ latitude+"," + longitude;
//   $("#map").attr("src", url);  
//}

function roomClicked(hotelId, hotelName) {
    var result = sessionStorage.getItem('hotelSearchCriteria');
    var searchCriteria = JSON.parse(result).data;
    var sessionId = $('#sessionId').text();
    var amenitiesId = $('#amenities' + sessionId);
    sessionStorage.setItem(amenitiesId, $(amenitiesId).html());
    var roomSearchObj = new roomSearchRQ(searchCriteria.CheckInDate, searchCriteria.CheckOutDate, searchCriteria.Location.Latitude, searchCriteria.Location.Longitude, searchCriteria.GuestCount, searchCriteria.NoOfRooms, hotelId, hotelName, sessionId);
    sessionStorage.setItem('roomSearchCriteria', JSON.stringify(roomSearchObj));
    window.location = '../html/room-listing.html';
};

function filterHotelsClicked() {

    var hotels = [];
    var selectedRatings = [];
    var priceRange = [];

    // Get checked values from UI component
    $('input[type="checkbox"]:checked').each(function () {
        selectedRatings.push($(this).val());
    });
    var checkedPriceRange = $('input[name="priceRange"]:checked').val();

    if (checkedPriceRange != null)
        priceRange = checkedPriceRange.split("-");

    //Match according to filter
    for (var hotelIndex = 0; hotelIndex < _hotelSearchRS.hotels.length; hotelIndex++) {
        var hotel = _hotelSearchRS.hotels[hotelIndex];
        var ratingFlag = false;
        var priceFlag = false;

        if (selectedRatings.length != 0) {
            for (var ratingIndex = 0; ratingIndex < selectedRatings.length; ratingIndex++) {
                if (hotel.starRating == selectedRatings[ratingIndex]) {
                    ratingFlag = true;
                    break;
                }
            }
        }
        if (priceRange.length != 0) {
            if (hotel.fare.baseFare >= priceRange[0] && hotel.fare.baseFare <= priceRange[1]) {
                priceFlag = true;
            }
        }
        //Save matched result
        if (priceRange.length != 0 && selectedRatings.length != 0) {
            if (ratingFlag == true && priceFlag == true)
                hotels.push(hotel);
        } else if (priceRange.length != 0 && priceFlag == true) {
            hotels.push(hotel);
        } else if (selectedRatings.length != 0 && ratingFlag == true) {
            hotels.push(hotel);
        } else if (priceRange.length == 0 && selectedRatings.length == 0) {
            hotels.push(hotel);
        }
    }

    var html = '<div class="col-md-4 col-md-offset-4  col-sm-8 col-sm-offset-2 font-big"><nav aria-label="Page navigation"><ul class="pagination"></ul></nav></div>';
    $(".pagination-section").html(html);    
    _paginationArgument.totalPages = Math.ceil(hotels.length / _perPageHotels);
    renderPaginatedHotels(hotels, hotels.length, _perPageHotels);    
    
}

function clearFilterClicked() {
    var html = '<div class="col-md-4 col-md-offset-4  col-sm-8 col-sm-offset-2 font-big"><nav aria-label="Page navigation"><ul class="pagination"></ul></nav></div>';
    $(".pagination-section").html(html);
    _paginationArgument.totalPages = Math.ceil(_hotelSearchRS.hotels.length / _perPageHotels);
    renderPaginatedHotels(_hotelSearchRS.hotels, _hotelSearchRS.hotels.length, _perPageHotels);                
    $('input[type="radio"]').prop("checked", false);
    $('input[type="checkbox"]').prop("checked", false);
};

