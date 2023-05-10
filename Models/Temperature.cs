#nullable disable
using Newtonsoft.Json;

namespace Diplom_Pogodel.Models
{
	public class Temperature
	{
        [JsonProperty("Metric")]
        public Metric Metric { get; set; }
	}
}
