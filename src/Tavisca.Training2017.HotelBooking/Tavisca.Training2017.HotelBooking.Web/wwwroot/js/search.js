
var jsonFormatData = {};

function hotelSearchRQ (searchText, checkIn, checkOut) {
     this.data = {
        "SearchText": searchText,
        "CheckInDate": checkIn,
        "CheckOutDate": checkOut
    }  
     jsonFormatData = this.data
    
};


$("#searchClick").click(function () {

    searchPageReader();
    var jsonData = JSON.stringify(jsonFormatData);
   
    $.ajax({
        url: "../hotel/search.html",
        type: "POST",
        data: jsonData,
        contentType: "application/json",
        success: function (resp) {
            var poi = resp[2].ItemList;
            var list = new Array();
            for (var i = 0; i < poi.length; i++) {
                var data = poi[i];
                list.push({
                    value: data.CulturedText,
                    data: data
                });
            }
            $("#destination").autocomplete({
                source: list,
                select: function (ev, data) {
                    console.log(data);
                }
            })
        },
        error: function (xhr) {
            _searchResponse = {};
        }
    });
    window.location = '../hotel/hotel_listing.html';
});

function searchPageReader() {
    var _searchText = $('#destination').val();
    var _checkIn = $('#check-in').val();
    var _checkOut = $('#check-out').val();
    var hotelSearchObj = new hotelSearchRQ(_searchText, _checkIn, _checkOut);
    sessionStorage.setItem('hotelSearchCriteria', JSON.stringify(hotelSearchObj));
}

