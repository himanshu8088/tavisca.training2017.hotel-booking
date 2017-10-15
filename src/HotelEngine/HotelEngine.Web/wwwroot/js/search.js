
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
    saveSearchCriteriaInSession();
    window.location = '../html/hotel.html';
});

function saveSearchCriteriaInSession() {
    var searchLocation = JSON.parse(sessionStorage.getItem('selectedLocation')).item;
    console.log(searchLocation);
    var _searchText = $('#destination').val();
    var _checkIn = $('#check-in').val();
    var _checkOut = $('#check-out').val();
    var _guestCount = $('#guest').find(":selected").text();
    var _roomCount = $('#room').find(":selected").text();
    var hotelSearchObj = new hotelSearchRQ(_searchText, _checkIn, _checkOut, searchLocation.data.Latitude, searchLocation.data.Longitude, _guestCount, _roomCount);
    sessionStorage.setItem('hotelSearchCriteria', JSON.stringify(hotelSearchObj));
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

