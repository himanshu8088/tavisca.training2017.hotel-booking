
$('#destination').keyup(function () {
    var searchText = $('#destination').val();
    if (searchText.length > 1) {
        search(searchText);        
    }

});

function search(searchText) {
    $.ajax({
        type: "GET",
        url: "http://portal.dev-rovia.com/Services/api/Content/GetAutoCompleteDataGroups?type=city%7Cairport%7Cpoi&query=" + searchText,
        dataType: "jsonp",
        crossDomain: true,
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
            $("#destination").autocomplete({
                autoFocus: true,
                source: list,
                minLength:1,
                select: function (event, data) {
                    sessionStorage.setItem('selectedLocation', JSON.stringify(data));
                }
            })
        },
        error: function (xhr) {
            _searchResponse = {};
        }
    });
}

