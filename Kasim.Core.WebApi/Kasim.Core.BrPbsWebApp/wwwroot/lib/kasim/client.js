$(document).ready(function () {
    $('#submit').click(function () {
        submit();
    });
});

function submit() {
    var url = "/doPost";
    $.get(url, function (data) {
        alert(data);
    });
}