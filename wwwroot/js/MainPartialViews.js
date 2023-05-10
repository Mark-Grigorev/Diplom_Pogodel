$(document).ready(function () {
    $('#GetAboutUs').click(function (e) {
        console.log('GetAboutUs clicked');
        $('#MainPartialViews').load('@Url.Action("AboutUsPage","Home")');
    });
});