using System;
namespace Clouder.Server.Prop
{
    public class User
    {
        public int UserId { get; set; }
        public UserInfo UserInfo { get; set; }
        public AccountActivation AccountActivation { get; set; }
        public CloudSettings CloudSettings { get; set; }
    }
}
