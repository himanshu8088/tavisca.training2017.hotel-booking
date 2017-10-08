function hotelSearchRQ (searchText, checkIn, checkOut, latitude,longitude) {
     this.data = {
        "SearchText": searchText,
        "CheckInDate": checkIn,
        "CheckOutDate": checkOut,
        "Location": {
            "Latitude": latitude,
            "Longitude": longitude
        }
    }  
};


$("#searchClick").click(function () {
    saveSearchCriteriaInSession();
    window.location = '../hotel/hotel_listing.html';
});

function saveSearchCriteriaInSession() {
    var searchLocation = JSON.parse(sessionStorage.getItem('selectedLocation')).item;
    console.log(searchLocation);
    var _searchText = $('#destination').val();
    var _checkIn = $('#check-in').val();
    var _checkOut = $('#check-out').val();
    var hotelSearchObj = new hotelSearchRQ(_searchText, _checkIn, _checkOut, searchLocation.data.Latitude, searchLocation.data.Longitude);
    sessionStorage.setItem('hotelSearchCriteria', JSON.stringify(hotelSearchObj));
}

