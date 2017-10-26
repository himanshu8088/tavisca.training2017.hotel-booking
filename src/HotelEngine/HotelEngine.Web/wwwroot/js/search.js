var searchLocation = {};

function hotelSearchRQ(searchText, checkIn, checkOut, latitude, longitude, guestCount, roomCount) {
    this.data = {
        "SearchText": searchText,
        "CheckInDate": checkIn,
        "CheckOutDate": checkOut,
        "Location": {
            "Latitude": latitude,
            "Longitude": longitude
        },
        "GuestCount": guestCount,
        "NoOfRooms": roomCount
    }
};
$("#searchClick").click(function () {  
    var isValid = validateSearchCriteria();
    if (isValid == true) {
        saveSearchRQ();
        window.location = '../html/hotel-listing.html';
    }        
});

function saveSearchRQ() {     
    var hotelSearchObj = getSearchValues();
    sessionStorage.setItem('hotelSearchCriteria', JSON.stringify(hotelSearchObj));
}

function getSearchValues() {
    var searchLocation = JSON.parse(sessionStorage.getItem('selectedLocation')).item;
    var _searchText = $('#destination').val();
    var _checkIn = $('#check-in').val();
    var _checkOut = $('#check-out').val();
    var _guestCount = $('#guest').find(":selected").text();
    var _roomCount = $('#room').find(":selected").text();
    var hotelSearchObj = new hotelSearchRQ(_searchText, _checkIn, _checkOut, searchLocation.data.Latitude, searchLocation.data.Longitude, _guestCount, _roomCount);
    return hotelSearchObj;
}

$(document).ready(function () {
    $("#check-in").datepicker({
        dateFormat: "yy-mm-dd",
        minDate: 0,
        onSelect: function () {
            var checkOutDate = $('#check-out');
            var startDate = $(this).datepicker('getDate');
            startDate.setDate(startDate.getDate() + 1);
            var minDate = $(this).datepicker('getDate');
            checkOutDate.datepicker('setDate', startDate);
            checkOutDate.datepicker('option', 'minDate', minDate);
        }
    });
    $('#check-out').datepicker({
        dateFormat: "yy-mm-dd"
    });
});

function validateSearchCriteria() {
    isValid = true;
    var searchDetail = getSearchValues();
    var searchRQ = searchDetail.data;
    if (searchRQ.SearchText == "") {
        $('#destination').addClass("error");
        isValid = false;        
    }  
    else {
        $('#destination').removeClass("error");
    }
    if (searchRQ.CheckInDate == "") {
        $('#check-in').addClass("error");
        isValid = false;       
    }   
    else {
        $('#check-in').removeClass("error");
    }
    if (searchRQ.CheckOutDate == "") {
        $('#check-out').addClass("error");
        isValid = false;        
    }
    else {
        $('#check-out').removeClass("error");
    }
    return isValid;
}
