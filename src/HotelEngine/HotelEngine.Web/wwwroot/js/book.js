var _roomPriceRQ = {};


$(document).ready(function () {    
    setCalender();
    restoreRoomPriceRQ();
});

function setCalender() {
    $('#dob').datepicker({
        dateFormat: "yy-mm-dd",
    });
    $('#dob').datepicker('setDate', new Date("1990-01-01"));

    $('#expiry-date').datepicker({
        dateFormat: "yy-mm-dd"
    });
    $('#expiry-date').datepicker('setDate', new Date("2022-01-01"));
}

function restoreRoomPriceRQ() {
    _roomPriceRQ = JSON.parse(sessionStorage.getItem('roomPriceSearchCriteria'));
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

$('.payment-procced').click(function () {    
    var isValidGuestDetail = validateGuestDetails();
    if (isValidGuestDetail == true) {        
        $('a[href="#pay-tab"]').tab('show');      
    }
});


function createBookingRQ() {
    var guestDetail = (new guest()).detail;
    var cardDetail = (new paymentCard()).detail;
    var bookingRQ = _roomPriceRQ;

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

function validateCardDetails() {    
    isValid = true;
    var cardDetails = (new paymentCard()).detail;
    if (cardDetails.CardHolderName == "") {
        showErrorMsg();
        $('#card-holder').addClass("error");
        isValid = false;
    }
    else {
        $('#error').html("");
        $('#card-holder').removeClass("error");
    }

    var isValidCardNumber = validationCardNumber(cardDetails.CardNumber);
    if (isValidCardNumber==false) {
        showErrorMsg();
        $('#card-number').addClass("error");
        isValid = false;
    }
    else {
        $('#error').html("");
        $('#card-number').removeClass("error");
    }
    if (cardDetails.ExpiryDate == "") {
        showErrorMsg();
        $('#expiry-date').addClass("error");
        isValid = false;
    }
    else {
        $('#error').html("");
        $('#expiry-date').removeClass("error");
    }
    if (cardDetails.CVV.length!=4) {
        showErrorMsg();
        $('#cvv').addClass("error");
        isValid = false;
    }
    else {
        $('#error').html("");
        $('#cvv').removeClass("error");
    }
    return isValid;
}
function validateGuestDetails() {
    isValid = true;
    var guestDetail = (new guest()).detail;
    if (guestDetail.FirstName == "") {
        showErrorMsg();
        $('#fname').addClass("error");
        isValid = false;
    }
    else {
        $('#error').html("");
        $('#fname').removeClass("error");
    }
    if (guestDetail.LastName == "") {
        showErrorMsg();
        $('#lname').addClass("error");
        isValid = false;
    }
    else {
        $('#error').html("");
        $('#lname').removeClass("error");
    }
    if (guestDetail.DOB == "") {
        showErrorMsg();
        $('#dob').addClass("error");
        isValid = false;
    }
    else {
        $('#error').html("");
        $('#dob').removeClass("error");
    }
    if (guestDetail.MobileNo.length!=10) {
        showErrorMsg();
        $('#mobile').addClass("error");
        isValid = false;
    }
    else {
        $('#error').html("");
        $('#mobile').removeClass("error");
    }
    var isValidEmail = validateEmail(guestDetail.EmailId);
    if (isValidEmail==false) {
        showErrorMsg();
        $('#email').addClass("error");
        isValid = false;
    }
    else {
        $('#error').html("");
        $('#email').removeClass("error");
    }
    return isValid;
}

function validateEmail(email) {
    var pattern = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

    return $.trim(email).match(pattern) ? true : false;
}

function validationCardNumber(cardNumber) {
    /*
        Luhn algorithm:
        Starting at the last digit in the cardNumber (the check digit),
        double every other digit’s value.
        If any of the doubled digits are greater than nine,
        then the number is divided by 10 and the remainder is added to one.
        This value is added together with the appropriate values for every other digit to get a sum.
        If this sum can be equally divisible by 10, then the number is valid.
        The check digit serves the purpose of ensuring that the identifier will by equally divisible by 10.
     */
    var sum = 0,
        alt = false,
        i = cardNumber.length - 1,
        num;

    if (cardNumber.length < 13 || cardNumber.length > 19) {
        return false;
    }

    while (i >= 0) {

        //get the next digit
        num = parseInt(cardNumber.charAt(i), 10);

        //if it's not a valid number, abort
        if (isNaN(num)) {
            return false;
        }

        //if it's an alternate number...
        if (alt) {
            num *= 2;
            if (num > 9) {
                num = (num % 10) + 1;
            }
        }

        //flip the alternate bit
        alt = !alt;

        //add to the rest of the sum
        sum += num;

        //go to next digit
        i--;
    }

    //determine if it's valid
    return (sum % 10 == 0);
}

function showErrorMsg() {
    $('#error').html("<h6>All fields are mandatory. Provide valid values</h6>").css({ "color": "red" });
    $('#error').show();
}