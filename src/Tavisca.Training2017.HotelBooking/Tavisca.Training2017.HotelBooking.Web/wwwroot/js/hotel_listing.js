$(document).ready(function () {
    var searchCriteria = JSON.parse(sessionStorage.getItem('hotelSearchCriteria'));
    var jsonData = JSON.stringify(searchCriteria);
    $.ajax({
        url: "../hotel/search",
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
        },
        error: function (xhr) {
            _searchResponse = {};
        }
    });

});


$(function () {

    var result = [{ name: "Hyatt", address: "pune", image: "https://www.google.co.in/images/branding/googlelogo/2x/googlelogo_color_120x44dp.png" },
    { name: "Novotel", address: "Nagar Road", image: "https://www.google.co.in/images/branding/googlelogo/2x/googlelogo_color_120x44dp.png" },
    { name: "Taj", address: "Mumbai" },
    { name: "Bombay Hotel", address: "Guess??" }];

    var template = $('#hotel-item');

    var compiledTemplate = Handlebars.compile(template.html());

    var html = compiledTemplate(result);

    $('#hotelList-container').html(html);
});