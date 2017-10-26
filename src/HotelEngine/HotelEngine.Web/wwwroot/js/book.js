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
    var isValidGuestDetail = validateGuestDetails();
    if (isValidGuestDetail == true) {
        $('a[href="#pay-tab"]').tab('show');
    }
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
    var isValidCardDetail = validateCardDetails();
    if (isValidCardDetail == true) {
        var bookingRQ = createBookingRQ();
        sessionStorage.setItem('bookingRQ', JSON.stringify(bookingRQ));
        window.location = '../html/confirmation.html';  
    }
     
});
function validateGuestDetails() {
    var isValid = true;
    var guestDetail = (new guest()).detail;
    if (guestDetail.FirstName == "" || guestDetail.LastName == "" || guestDetail.MobileNo == "" || guestDetail.EmailId == "" || guestDetail.DOB == "") {

        alert("Please fill all required fields .");
        isValid = false;
    }
    return isValid;
}
function validateCardDetails() {
    var isValid = true;
    var cardDetails = (new paymentCard()).detail;
    if (cardDetails.CardHolderName == "" || cardDetails.CardNumber == "" || cardDetails.ExpiryDate == "" || cardDetails.CVV == "") {

        alert("Please fill all card details .");
        isValid = false;
    }
    return isValid;
}