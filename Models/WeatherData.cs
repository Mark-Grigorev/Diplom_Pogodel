#nullable disable
using Newtonsoft.Json;

namespace Diplom_Pogodel.Models
{
	public class WeatherData
	{
		[JsonProperty("Data")]
		public string LocalObservationDateTime { get; set; }

        [JsonProperty("Temperature")]
		public Temperature Temperature { get; set; }
        [JsonProperty("WeatherText")]
        public string WeatherText { get; set; }
	}
}
