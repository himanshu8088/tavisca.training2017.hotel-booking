
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
        url: "http://localhost:56851/api/search",
        type: "POST",
        data: jsonData,
        contentType: "application/json",
        
    });
});

function searchPageReader() {
    var searchText = $('#destination').val;
    var checkIn = $('#check-in').val;
    var checkOut = $('#check-out').val;
    var hotelSearchObj = new hotelSearchRQ("pune", "11/2/", "3242");
}

