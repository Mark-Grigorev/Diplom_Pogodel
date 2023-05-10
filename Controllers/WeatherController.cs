using Diplom_Pogodel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Policy;

namespace Diplom_Pogodel.Controllers
{
	public class WeatherController : Controller
	{
		private UserContext db;
		public WeatherController(UserContext context)
		{
			db = context;
		}
		//Редиректим авторизовавшегося пользователя на специальную форму, для выбора того,
		//в каком формате пользователь хочет получить данные
		
        public async Task<IActionResult> RequestFormPage()
		{
			return View();
		}

        public async Task<JsonResult> GetWeather(string cityName, double? latitude, double? longitude)
        {
            if (!string.IsNullOrEmpty(cityName))
            {
                // Получаем данные погоды по названию города
                var model = await GetWeatherForCity(cityName);
                return Json(model);
            }
            else if (latitude.HasValue && longitude.HasValue)
            {
                // Получаем данные погоды по геопозиции
                var model = await GetWeatherByGeolocation(latitude.Value, longitude.Value);
                return Json(model);
            }
            else
            {
                // Возвращаем пустую модель данных
                var model = new WeatherViewModel();
                return Json(model);
            }
        }

        public IActionResult WeatherUserPage(string data)
        {
            var json = JObject.Parse(data);
            var value = json["value"];

            var model = new WeatherViewModel
            {
                Data = (string)value["data"],
                CityName = (string)value["cityName"],
                Temperature = (int)value["temperature"],
                WeatherText = (string)value["weatherText"]
            };

            return View(model);
        }



        // Метод для получения данных по городу, который вводит пользователь на странице

        public async Task<ActionResult<WeatherViewModel>> GetWeatherForCity(string cityName)
        {
			try
			{
                //alex1302 password qwerty123
                string apiKey = "bzRjvzfObdPrnZaeFpVWgPjmDmVtfzGT";
				//relax130102 login ola22 password standart
				//string apiKey = "yBFBtyUgept17HhxagPPhLvIxHfbX879";
				//grigorevmark
				//string apiKey = "BrZTvf2HTNBXvQ9MGsUjdTe42DFx7dId";
				//markgrigorev02
				//string apiKey = "8xCYbx31vzStcHLGY0WQqbacjPgkv8wm";
				string url = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={cityName}";

				using (var client = new HttpClient())
				{
					var response = await client.GetAsync(url);
					response.EnsureSuccessStatusCode();

					var json = await response.Content.ReadAsStringAsync();
					var cities = JsonConvert.DeserializeObject<List<CityInfo>>(json);

					var cityKey = cities.FirstOrDefault()?.Key;
					if (cityKey != null)
					{
						url = $"http://dataservice.accuweather.com/currentconditions/v1/{cityKey}?apikey={apiKey}&language=ru&details=false";
						response = await client.GetAsync(url);
						response.EnsureSuccessStatusCode();

						json = await response.Content.ReadAsStringAsync();
						var weatherData = JsonConvert.DeserializeObject<List<WeatherData>>(json);

						if (weatherData.Count > 0)
						{
							var currentWeather = weatherData[0];
							var temperatureValue = currentWeather.Temperature.Metric.Value;
							var weatherText = currentWeather.WeatherText;
							var dataRequest = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

							var model = new WeatherViewModel
							{
								Data = dataRequest,
								CityName = cityName,
								Temperature = (int)temperatureValue,
								WeatherText = weatherText
							};
                            return model;
                        }
						else
						{
							throw new Exception("No weather data available.");
						}
					}
					else
					{
						throw new Exception("City not found.");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				throw; // Пробросить исключение для обработки в вызывающем коде
			}
		}

        //Метод для получения данных, по геопозиции пользователя
        public async Task<ActionResult<WeatherViewModel>> GetWeatherByGeolocation(double latitude, double longitude)
        {
            //alex1302 password qwerty123
            string apiKey = "bzRjvzfObdPrnZaeFpVWgPjmDmVtfzGT";
            //relax130102 login ola22 password standart
            //string apiKey = "yBFBtyUgept17HhxagPPhLvIxHfbX879";
            //grigorevmark
            //string apiKey = "BrZTvf2HTNBXvQ9MGsUjdTe42DFx7dId";
            //markgrigorev02
            //string apiKey = "8xCYbx31vzStcHLGY0WQqbacjPgkv8wm";
            string url = $"http://dataservice.accuweather.com/locations/v1/cities/geoposition/search?apikey={apiKey}&q={latitude},{longitude}";

			using (var client = new HttpClient())
			{
				try
				{
					var response = await client.GetAsync(url);
					response.EnsureSuccessStatusCode();

					var json = await response.Content.ReadAsStringAsync();
					var city = JsonConvert.DeserializeObject<CityInfo>(json);

					var cityKey = city.Key;
					if (cityKey != null)
					{
						url = $"http://dataservice.accuweather.com/currentconditions/v1/{cityKey}?apikey={apiKey}&language=ru&details=false";

						response = await client.GetAsync(url);
						response.EnsureSuccessStatusCode();

						json = await response.Content.ReadAsStringAsync();
						var weatherData = JsonConvert.DeserializeObject<List<WeatherData>>(json);

						if (weatherData.Count > 0)
						{
							var currentWeather = weatherData[0];
							var temperatureValue = currentWeather.Temperature.Metric.Value;
							var weatherText = currentWeather.WeatherText;
							var dataRequest = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

							var model = new WeatherViewModel
							{
								Data = dataRequest,
								CityName = city.LocalizedName,
								Temperature = (int)temperatureValue,
								WeatherText = weatherText
							};

							return model;
                        }
						else
						{
							throw new Exception("No weather data available.");
						}
					}
					else
					{
						throw new Exception("City not found.");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error: {ex.Message}");
					throw; // Пробросить исключение для обработки в вызывающем коде
				}
			}
		}

	}

    }

