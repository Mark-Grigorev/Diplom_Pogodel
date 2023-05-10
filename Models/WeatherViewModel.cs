using Newtonsoft.Json;

namespace Diplom_Pogodel.Models
{
	public class WeatherViewModel
	{
        [JsonProperty("data")]
        public string Data { get; set; }
        [JsonProperty("cityName")]
        public string CityName { get; set; }
        [JsonProperty("temperature")]
        public int Temperature { get; set; }
        [JsonProperty("weatherText")]
        public string WeatherText { get; set; }
	}
}
