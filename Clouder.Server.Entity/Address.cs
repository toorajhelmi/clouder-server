using System;
using Newtonsoft.Json;

namespace Clouder.Server.Entity
{
	public class Address
	{
		[JsonProperty(PropertyName = "State")]
		public string State { get; set; }
		[JsonProperty(PropertyName = "Street")]
		public string Street { get; set; }
		[JsonProperty(PropertyName = "PostalCode")]
		public string PostalCode { get; set; }
		[JsonProperty(PropertyName = "CellPhone")]
		public string CellPhone { get; set; }
	}
}
