var hotelItinerary = {};
var hotelRQ = {};
Handlebars.registerHelper('times', function (n, block) {
    var accum = '';
    for (var i = 0; i < n; ++i)
        accum += block.fn(i);
    return accum;
});


$(document).ready(function () {

    hotelRQ = JSON.parse(sessionStorage.getItem('hotelSearchCriteria')).data;
    var jsonData = JSON.stringify(hotelRQ);
    $.ajax({
        url: "../hotel/search",
        type: "POST",
        data: jsonData,
        contentType: "application/json",
        success: function (hotels) {
            hotelItinerary = hotels;
            hotelListing(hotelItinerary);
        },
        error: function (xhr) {
            alert("Sorry server doesn't responding. Please try again.");
            window.location = '../html/search.html';
        }
    });
    $('#filters').tooltip();
});

function filterHotels() {
    filterByStarRating();
};

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
$('#ex1').slider({
    formatter: function (value) {
        return 'Current value: ' + value;
    }
});
function hotelListing(hotelItinerary) {
    var templateData = { hotelRQ, hotelItinerary };
    var template = $('#hotel-item');
    var compiledTemplate = Handlebars.compile(template.html());
    var html = compiledTemplate(templateData);
    $('#hotelList-container').html(html);
}
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
function filterByStarRating() {
    var hotels = [];
    var selectedRatings = [];
    var priceRange=[];

    // Get Value from UI component
    $('input[type="checkbox"]:checked').each(function () {
        selectedRatings.push($(this).val());
    });
    var checkedPriceRange = $('input[name="priceRange"]:checked').val();
    
    if (checkedPriceRange !=null)
        priceRange = checkedPriceRange.split("-");

    //Match according to filter
    for (var hotelIndex = 0; hotelIndex < hotelItinerary.hotels.length; hotelIndex++) {
        var hotel = hotelItinerary.hotels[hotelIndex];
        var ratingFlag = false;
        var priceFlag = false;

        if (selectedRatings.length != 0 ) {
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
        if (priceRange.length != 0 && selectedRatings.length != 0 ) {
            if (ratingFlag == true && priceFlag == true)
                hotels.push(hotel);
        } else if (priceRange.length != 0 && priceFlag == true) {
            hotels.push(hotel);
        } else if (selectedRatings.length != 0  && ratingFlag == true) {
            hotels.push(hotel);
        } else if (priceRange.length == 0 && selectedRatings.length == 0) {
            hotels.push(hotel);
        }
    }

    var sessionId = hotelItinerary.sessionId;
    filteredHotelItinerary = {
        "sessionId": sessionId,
        "hotels": hotels
    }
    hotelListing(filteredHotelItinerary);
}
$('#clearFilter').click(function () {
    hotelListing(hotelItinerary);
    $('input[type="radio"]').prop("checked", false);
    $('input[type="checkbox"]').prop("checked", false);
});


