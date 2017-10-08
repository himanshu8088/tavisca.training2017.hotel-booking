Handlebars.registerHelper('times', function (n, block) {
    var accum = '';
    for (var i = 0; i < n; ++i)
        accum += block.fn(i);
    return accum;
});

$(document).ready(function () {
    var searchCriteria = JSON.parse(sessionStorage.getItem('hotelSearchCriteria')).data;
    
    var jsonData = JSON.stringify(searchCriteria);
    $.ajax({
        url: "../hotel/search",
        type: "POST",
        data: jsonData,
        contentType: "application/json",
        success: function (resp) {
            var template = $('#hotel-item');

            var compiledTemplate = Handlebars.compile(template.html());

            var html = compiledTemplate(resp);

            $('#hotelList-container').html(html);
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

    
});