﻿Handlebars.registerHelper('times', function (n, block) {
    var accum = '';
    for (var i = 0; i < n; ++i)
        accum += block.fn(i);
    return accum;
});

$(document).ready(function () {
    var searchCriteria = JSON.parse(sessionStorage.getItem('hotelSearchCriteria')).data;    
    var jsonData = JSON.stringify(searchCriteria);
    $.ajax({
        url: "../hotel/search",
        type: "POST",
        data: jsonData,
        contentType: "application/json",
        success: function (resp) {
            var template = $('#hotel-item');

            var compiledTemplate = Handlebars.compile(template.html());

            var html = compiledTemplate(resp);

            $('#hotelList-container').html(html);
        },
        error: function (xhr) {
            _searchResponse = {};
        }
    });

});

function roomSearchRQ(checkIn, checkOut, latitude, longitude, guestCount, noOfRooms, hotelId) {
    this.data = {        
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

$('#viewRooms').click(function () {
    var result = sessionStorage.getItem('hotelSearchCriteria');
    var searchCriteria = result.data;
    var roomSearchObj = new roomSearchRQ(searchCriteria.CheckInDate, searchCriteria.CheckOutDate, searchCriteria.Location.Latitude, searchCriteria.Location.Longitude, searchCriteria.GuestCount, searchCriteria.NoOfRooms, searchCriteria.HotelId);    
    storeOnSession(roomSearchObj);    
    window.location = '../html/room.html';
});

function storeOnSession(roomSearchObj) {    
    sessionStorage.setItem('roomSearchCriteria', JSON.stringify(roomSearchObj));
}

