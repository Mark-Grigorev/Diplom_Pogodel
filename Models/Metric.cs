#nullable disable
using Newtonsoft.Json;

namespace Diplom_Pogodel.Models
{
	public class Metric
	{
        [JsonProperty("Value")]
        public float Value { get; set; }
		
	}
}
