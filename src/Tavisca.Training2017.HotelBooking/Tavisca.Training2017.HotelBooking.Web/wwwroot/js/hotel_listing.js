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