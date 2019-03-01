using System;
namespace Clouder.Server.Service.Cadl
{
    public class FunctionNode : Node
    {
        public string ComponentName { get; set; }
        public string FunctionName { get; set; }
        public string InputName { get; set; }
        public string TimerPeriod { get; set; }
        public string Queue { get; set; }
    }
}
