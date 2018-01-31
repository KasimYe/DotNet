$(document).ready(function () {
    $('#format').click(function () {
        format();
    });
});

function format() {    
    if ($('#ToJson').prop("checked")) {
        var result = JSON.parse($("#ResponseText").val());
        var formatVal = JSON.stringify(result, null, 2);
        $("#ReqJson").text(formatVal);
    } else {
        $("#ReqJson").text(formatXml($('#ResponseText').val()));
    }
    
}