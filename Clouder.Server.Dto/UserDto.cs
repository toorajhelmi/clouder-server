using System.Linq;
using System.Collections.Generic;
using Clouder.Server.Entity;
using Newtonsoft.Json;
using Clouder.Server.Prop;

namespace Clouder.Server.Dto
{
    public class UserDto 
    {
        public UserDto(UserEntity userEntity)
        {
            Id = userEntity.Id;
            User = userEntity.User;
        }

        public string Id { get; set; }
        public User User { get; set; }
    }
}
