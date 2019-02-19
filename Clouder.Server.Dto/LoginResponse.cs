using System;
namespace Clouder.Server.Dto
{
    public class LoginResponse
    {
        public bool Succeeded { get; set; }
        public bool WrongLogin { get; set; }
        public bool NotActivated { get; set; }
        public bool Suspended { get; set; }
        public UserDto User { get; set; }
    }
}
