using System;
namespace Clouder.Server.Service.Cadl
{
    public class QueueNode : Node
    {
        public string ComponentName { get; set; }
        public string QueueName { get; set; }
    }
}
