
//$(document).ready(function () {
//    $(function () {
//        $("#datepicker").datepicker({
//            changeMonth: true,
//            changeYear: true,
//            minDate: '0m+1d',
//            showAnim: "fadeIn",
//            dayNamesMin: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
//        }
//        );
//    });
//    $(function () {
//        $("#tabs").tabs();
//    });
//    $("#sel2").on("change", function () {
//        $("#age-box").empty();
//        val = $("#sel2 option:selected").val();
//        for (i = 0; i < val; i++) {
//            $("#age-box").append(Agebox);
//        }
//        AgeBox = "<label>Age</label>\
//            <div id=\"age-box\">\
//                <select id=\"age\">\
//                    <option value=\"1\">1</option>\
//                    <option value=\"2\">2</option>\
//                    <option value=\"3\">3</option>\
//                    <option value=\"4\">4</option>\
//                    <option value=\"5\">5</option>\
//                    <option value=\"6\">6</option>\
//                </select>\
//            </div>"
//    });
//});
$("#searchClick").click(function () {
    var xmlhttp = new XMLHttpRequest();   // new HttpRequest instance 
    xmlhttp.open("POST", "/json-handler");
    xmlhttp.setRequestHeader("Content-Type", "application/json");
    xmlhttp.send(JSON.stringify({ name: "John Rambo", time: "2pm" }));
});