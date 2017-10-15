var userDetail = {};

function setUserDetail(firstName, lastName,dob, mobile, emailId) {
    this.userDetail = {
        "FirstName": firstName, 
        "LastName": lastName, 
        "DOB": dob,
        "MobileNo": mobile,
        "EmailId": emailId
    }
};

function setCardDetail(cardHolderName, cardNumber, expiryDate, cvv) {
    this.cardDetail = {
        "CardHolderName": cardHolderName,
        "CardNumber": cardNumber,
        "ExpiryDate": expiryDate,
        "CVV": cvv,        
    }
}

function setBookingDetail(journeyDetail,userDetail,cardDetail) {
    bookDetail = journeyDetail;
    $.extend(bookDetail, {
        "UserDetail": userDetail,
        "CardDetail": cardDetail
    });
}

$('#procceed').click(function () {
    var fname = $('#fname').html();
    var lname = $('#lname').html();
    var dob = $('#dob').html();
    var mobile = $('#mobile').html();
    var email = $('#email').html();
    this.userDetail = new setUserDetail(fname, lname, dob, mobile, email);
    
});

    
$('#pay').click(function () {

    var cardHolderName = $('#card-holder').html();
    var cardNumber = $('#card-number').html();
    var expiryDate = $('#expiry-date').html();
    var cvv = $('#cvv').html();
    var cardDetail = new setCardDetail(cardHolderName, cardNumber, expiryDate, cvv);
    var journeyDetail = Json.parse(sessionStorage.getItem('roomSearchCriteria'));
    var bookingDetail = new setBookingDetail(journeyDetail, this.userDetail, bookDetail);

    var jsonData = JSON.stringify(bookingDetail);

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