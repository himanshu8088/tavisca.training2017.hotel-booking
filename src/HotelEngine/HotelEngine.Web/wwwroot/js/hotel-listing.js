var hotelItinerary = {};
var hotelRQ = {};
var perPageItemCount = 10;
var totalPageCount;
var totalHotelsCount;
var pagination;

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
            totalHotelsCount = hotelItinerary.hotels.length;
            $.extend(hotelItinerary, {
                "hotelsCount": totalHotelsCount
            });

            if (totalHotelsCount != 0) {               
                totalPageCount = Math.ceil(totalHotelsCount / perPageItemCount);
                hotelsPagination(hotelItinerary);   
            } else {
                hotelListing(hotelItinerary);
            }            
        },
        error: function (xhr) {
            alert("Sorry server doesn't responding. Please try again.");
            window.location = '../html/search.html';
        }
    });
    $('#filters').tooltip();
});

function hotelsPagination(hotelItinerary) {
    
        $('.pagination-section').show();
        pagination=$('.pagination').twbsPagination({
        totalPages: totalPageCount,
        visiblePages: perPageItemCount,
        onPageClick: function (event, page) {
            var hotelArr = jQuery.makeArray(hotelItinerary.hotels);
            var firstInd = (perPageItemCount * page) - perPageItemCount;
            var lastInd = (perPageItemCount * page);
            var hotelItineraryArr = hotelArr.slice(firstInd, lastInd);
            var itinerary = {
                "sessionId": hotelItinerary.sessionId,
                "hotels": hotelItineraryArr,
                "hotelsCount": totalHotelsCount
            };
            function getOnPageClickContext()
            {
                return this.onPageClick;
            }
            hotelListing(itinerary);
        }
    });
}

function hotelListing(hotelItinerary) {
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
    var priceRange=[];

    // Get checked values from UI component
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
        "hotels": hotels,
        "hotelsCount": hotels.length
    }
    hotelListing(filteredHotelItinerary); 
    $('.pagination-section').hide();
}
function clearFilterClicked() {
    hotelListing(hotelItinerary); 
    $('.pagination-section').show();
    $('input[type="radio"]').prop("checked", false);
    $('input[type="checkbox"]').prop("checked", false);
};

