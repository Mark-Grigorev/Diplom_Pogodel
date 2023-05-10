$(document).ready(function () {
    // Обработчик события нажатия кнопки
    $("#goBackButton").click(function () {
        window.location.href = "/Weather/RequestFormPage"; // Замените "ControllerName" и "ActionName" на соответствующие значения вашего контроллера и действия
    });
});
