using System.Linq;
using System.Collections.Generic;
using Clouder.Server.Entity;
using Newtonsoft.Json;

namespace Clouder.Server.Dto
{
    public class UserDto 
    {
        public UserDto(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
            Activated = user.Activated;
            Suspended = user.Suspended;
            Email = user.Email;
            Address = user.Address;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Activated { get; set; }
        public bool Suspended { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
