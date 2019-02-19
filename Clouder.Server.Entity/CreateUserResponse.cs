using System;

namespace Clouder.Server.Entity
{
    public enum ResultCode
    {
        Successful,
        AccountExists
    }

    public class CreateUserResponse
    {
        public User User { get; set; }
        public ResultCode ResultCode { get; set; }
    }
}
