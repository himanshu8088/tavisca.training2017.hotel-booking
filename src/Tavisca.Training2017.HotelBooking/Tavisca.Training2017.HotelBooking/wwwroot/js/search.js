
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
        
    });
});

function searchPageReader() {
    var _searchText = $('#destination').val();
    var _checkIn = new Date($('#check-in').val());
    var _checkOut = new Date($('#check-out').val());
    var hotelSearchObj = new hotelSearchRQ(_searchText, _checkIn, _checkOut);
}

