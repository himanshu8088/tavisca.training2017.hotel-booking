var hotelItinerary = {};
var hotelRQ={};
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
            _searchResponse = {};
        }
    });
    $('#filters').tooltip();
});

function filterHotels() {
    filterByStarRating();
};

function roomSearchRQ(checkIn, checkOut, latitude, longitude, guestCount, noOfRooms, hotelId,hotelName,sessionId) {
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
function roomClicked(hotelId,hotelName) {
    var result = sessionStorage.getItem('hotelSearchCriteria');
    var searchCriteria = JSON.parse(result).data;
    var sessionId = $('#sessionId').text();
    var amenitiesId = $('#amenities' + sessionId);
    sessionStorage.setItem(amenitiesId, $(amenitiesId).html());   
    var roomSearchObj = new roomSearchRQ(searchCriteria.CheckInDate, searchCriteria.CheckOutDate, searchCriteria.Location.Latitude, searchCriteria.Location.Longitude, searchCriteria.GuestCount, searchCriteria.NoOfRooms, hotelId, hotelName, sessionId);
    sessionStorage.setItem('roomSearchCriteria', JSON.stringify(roomSearchObj));   
    window.location = '../html/rooms.html';
};
function filterByStarRating() {
    var hotels = [];
    var selectedRatings=[]
    $('input[type="checkbox"]:checked').each(function() {
        selectedRatings.push($(this).val());       
    });

    for (var hotelIndex = 0; hotelIndex < hotelItinerary.hotels.length; hotelIndex++) {
        var hotel = hotelItinerary.hotels[hotelIndex];
        for (var ratingIndex = 0; ratingIndex < selectedRatings.length; ratingIndex++) {
            if (hotel.starRating == selectedRatings[ratingIndex]) {
                hotels.push(hotel);
            }
        }            
    }

    var sessionId = hotelItinerary.sessionId;
    filteredHotelItinerary = {
        "sessionId": sessionId,
        "hotels": hotels
    }
    hotelListing(filteredHotelItinerary);
}

