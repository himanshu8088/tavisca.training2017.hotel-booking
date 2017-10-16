
function getUserDetail() {
    var firstName = $('#fname').html();
    var lastName = $('#lname').html();
    var dob = $('#dob').val();
    var mobile = $('#mobile').html();
    var emailId = $('#email').html();

    userDetail = {
        "FirstName": firstName, 
        "LastName": lastName, 
        "DOB": dob,
        "MobileNo": mobile,
        "EmailId": emailId
    }
    return userDetail;
};

function getCardDetail() {
    var cardHolderName = $('#card-holder').html();
    var cardNumber = $('#card-number').html();
    var expiryDate = $('#expiry-date').html();
    var cvv = $('#cvv').html();

    cardDetail = {
        "CardHolderName": cardHolderName,
        "CardNumber": cardNumber,
        "ExpiryDate": expiryDate,
        "CVV": cvv,        
    }
    return cardDetail;
}

$('#procceed').click(function () {    
    $('a[href="#pay-tab"]').tab('show');    
});

    
$('#pay').click(function () {

    var userDetail = getUserDetail();
    var cardDetail = getCardDetail();
    var bookDetail = JSON.parse(sessionStorage.getItem('roomSearchCriteria')).data;

    //var journeyDetail = JSON.parse(sessionStorage.getItem('roomSearchCriteria')).data;    
    //var bookDetail = {
    //    "SessionId": journeyDetail.SessionId,
    //    "SearchText": journeyDetail.SearchText,
    //    "CheckInDate": journeyDetail.CheckInDate,
    //    "CheckOutDate": journeyDetail.CheckOutDate,
    //    "Location": {
    //        "Latitude": journeyDetail.Latitude,
    //        "Longitude": journeyDetail.Longitude
    //    },
    //    "NoOfRooms": parseInt(journeyDetail.NoOfRooms),
    //    "GuestCount": parseInt(journeyDetail.GuestCount),
    //    "HotelId": parseInt(journeyDetail.HotelId) 
    //}

    $.extend(bookDetail, {
        "GuestDetail": userDetail,
        "CardDetail": cardDetail
    });

    var jsonData = JSON.stringify(bookDetail);

    $.ajax({
        url: "../hotel/book",
        type: "POST",
        data: jsonData,
        contentType: "application/json",
        success: function (resp) {
            alert('Booking confirmed')
        },
        error: function (xhr) {       
            alert('Sorry payment failed')
        }
    });       
});