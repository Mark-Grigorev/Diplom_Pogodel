$(document).ready(function () {
    // Обработчик нажатия кнопки "Получить по геоданным"
    $("#geoButton").click(function () {
        if ("geolocation" in navigator) {
            navigator.geolocation.getCurrentPosition(function (position) {
                var latitude = position.coords.latitude;
                var longitude = position.coords.longitude;
                getWeatherByGeoPosition(latitude, longitude);
            }, function (error) {
                alert("Ошибка получения геолокации " + error.message);
            });
        } else {
            alert("Геолокация не поддерживается вашим браузером.");
        }
    });

    // Обработчик нажатия кнопки "Получить по названию города" - работает
    $("#cityButton").click(function () {
        var cityName = document.getElementById('cityNameInput').value;
        console.log("Получить данные по названию города, наименование города =", cityName);
        if (cityName !== "") {
            getWeatherByCityName(cityName);
        } else {
            alert("Введите название города");
        }
    });

    // Функция для обнуления флагов при загрузке страницы - работает
    function resetFlags() {
        weatherDataRequested = false;
    }

    var weatherDataRequested = false; // Флаг для отслеживания уже запрошенных данных
    resetFlags(); // Обнуление флагов при загрузке страницы

    function getWeatherByGeoPosition(latitude, longitude) {
        if (!weatherDataRequested) {
            weatherDataRequested = true; // Установка флага в true, чтобы данные запросились только один раз
            $.ajax({
                url: "/Weather/GetWeather",
                type: "GET",
                data: {
                    latitude: latitude,
                    longitude: longitude
                },
                success: function (response) {
                    // Обработка полученных данных
                    var redirectUrl = "/Weather/WeatherUserPage?data=" + encodeURIComponent(JSON.stringify(response));
                    window.location.href = redirectUrl;
                },
                error: function (error) {
                    alert("Разрешите доступ сайтам к вашим геоданным в браузере");
                }
            });
        }
    }


    //Функция работает
    function getWeatherByCityName(cityName) {
        if (!weatherDataRequested) {
            weatherDataRequested = true; // Установка флага в true, чтобы данные запросились только один раз
            $.ajax({
                url: "/Weather/GetWeather",
                type: "GET",
                data: {
                    cityName: cityName
                },
                success: function (response) {
                    // Обработка полученных данных
                    var redirectUrl = "/Weather/WeatherUserPage?data=" + encodeURIComponent(JSON.stringify(response));
                    window.location.href = redirectUrl;
                },
                error: function (error) {
                    alert("Произошла ошибка при обработке запроса: " + error.responseText);
                }
            });
        }
    }
});
