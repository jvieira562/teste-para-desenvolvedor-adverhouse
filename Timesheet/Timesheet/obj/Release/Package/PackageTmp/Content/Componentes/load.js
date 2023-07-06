function showLoading() {
    var spinner = new Spinner().spin(document.getElementById('spinner'));
    $('#loading').fadeIn('slow');
}
function hideLoading() {
    $('#loading').fadeOut('slow', function () {
        $('#spinner').empty();
    });
}
$(document).ajaxStart(function () {
    
    showLoading();
});

$(document).ajaxStop(function () {
    hideLoading();
});