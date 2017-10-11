
$(document).ready(function () {
    var searchCriteria = JSON.parse(sessionStorage.getItem('roomSearchCriteria')).data;
    var jsonData = JSON.stringify(searchCriteria);
  
    $.ajax({
        url: "../hotel/roomsearch",
        type: "POST",
        data: jsonData,
        contentType: "application/json",
        success: function (resp) {
            var template = $('#room-item');
            var compiledTemplate = Handlebars.compile(template.html());
            var html = compiledTemplate(resp);
            $('#roomList-container').html(html);
            $('#hotel-name').html(searchCriteria.SearchText);
        },
        error: function (xhr) {
            _searchResponse = {};
        }
    });       
});
//var sampleResponse = {
//    "hotelName": "Deshi Hotel",
//    "completeAddress": "Address",
//    "room": {
//        "roomName": "Room Name",
//        "bedType": "Bed Type",
//        "baseFare": "Fare",
//        "description": "description"
//    }
//} 