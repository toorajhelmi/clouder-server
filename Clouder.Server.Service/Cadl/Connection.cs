using System;
namespace Clouder.Server.Service.Cadl
{
    public class Connection
    {
        public Node OtherNode { get; set; }
        public Node Via { get; set; }
        public bool IsInward { get; set; }
    }
}
