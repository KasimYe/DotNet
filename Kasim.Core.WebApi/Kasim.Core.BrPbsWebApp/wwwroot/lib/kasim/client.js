$(document).ready(function () {
    $('#format').click(function () {        
        format();
    });
});

function format() {        
    if ($('#ToJson').prop("checked")) {
        var result = JSON.parse($("#ResponseText").text());
        var formatVal = JSON.stringify(result, null, 2);
        $("#ResponseText").text(formatVal);
    } else {
        $("#ResponseText").text(formatXml($('#ResponseText').text()));
    }
    
}