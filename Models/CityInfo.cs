#nullable disable

using System.Diagnostics.Metrics;
namespace Diplom_Pogodel.Models
{
	public class CityInfo
	{	
			public string Key { get; set; }
			public string LocalizedName { get; set; }
			public Country Country { get; set; }	
	}
}
