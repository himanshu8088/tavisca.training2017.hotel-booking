Handlebars.registerHelper('ifCond', function (v1, v2, options) {
    if (v1 != v2) {
        return options.fn(this);
    }
    return options.inverse(this);
});

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

function book(context) {
    window.location = '../html/guestdetails.html';
};