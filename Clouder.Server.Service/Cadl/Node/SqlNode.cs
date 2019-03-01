using System;
namespace Clouder.Server.Service.Cadl
{
    public class SqlNode : Node
    {
        public string ComponentName { get; set; }
        public string DatabaseName { get; set; }
        public string DbScript { get; set; }
    }
}
