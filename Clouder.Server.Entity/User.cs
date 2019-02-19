using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Clouder.Server.Entity
{
	public class User : EntityBase
	{         
		[JsonProperty(PropertyName = "Username")]
		public string Username { get; set; }
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "ActivationCode")]
        public string ActivationCode { get; set; }
        [JsonProperty(PropertyName = "Activated")]
        public bool Activated { get; set; }
        [JsonProperty(PropertyName = "Suspended")]
        public bool Suspended { get; set; }
		[JsonProperty(PropertyName = "Address")]
		public Address Address { get; set; }
		[JsonProperty(PropertyName = "FirstName")]
		public string FirstName { get; set; }
		[JsonProperty(PropertyName = "LastName")]
		public string LastName { get; set; }
		[JsonProperty(PropertyName = "PaymentCard")]
        public string FullName => $"{FirstName} {LastName}";
	}
}
