var roomPriceRQ = {};


$(document).ready(function () {    
    $('#dob').datepicker({
        dateFormat: "yy-mm-dd",       
    });
    $('#expiry-date').datepicker({
        dateFormat: "yy-mm-dd"
    });
    restoreSessionData();
});

function restoreSessionData() {
    roomPriceRQ = JSON.parse(sessionStorage.getItem('roomPriceSearchCriteria'));
}

function guest() {
    var firstName = $('#fname').val();
    var lastName = $('#lname').val();
    var dob = $('#dob').val();
    var mobile = $('#mobile').val();
    var emailId = $('#email').val();

    this.detail = {
        "FirstName": firstName, 
        "LastName": lastName, 
        "DOB": dob,
        "MobileNo": mobile,
        "EmailId": emailId
    }
};

function paymentCard() {
    var cardHolderName = $('#card-holder').val();
    var cardNumber = $('#card-number').val();
    var expiryDate = $('#expiry-date').val();
    var cvv = $('#cvv').val();

    this.detail = {
        "CardHolderName": cardHolderName,
        "CardNumber": cardNumber,
        "ExpiryDate": expiryDate,
        "CVV": cvv,        
    }    
}

$('#procceed').click(function () {    
    $('a[href="#pay-tab"]').tab('show');    
});


function createBookingRQ() {
    var guestDetail = (new guest()).detail;
    var cardDetail = (new paymentCard()).detail;
    var bookingRQ = roomPriceRQ;

    $.extend(bookingRQ, {
        "GuestDetail": guestDetail,
        "CardDetail": cardDetail
    });
    return bookingRQ;
}
    
$('#pay').click(function () {

    var bookingRQ=createBookingRQ();
    var bookingRQString = JSON.stringify(bookingRQ);    

    $.ajax({
        url: "../hotel/book",
        type: "POST",
        data: bookingRQString,
        contentType: "application/json",
        success: function (bookingResponse) {
            sessionStorage.setItem('bookConfirmationInfo', JSON.stringify(bookingResponse));
            window.location = '../html/confirmation.html';
        },
        error: function (xhr) {       
            alert('Sorry payment failed');
        }
    });       
});