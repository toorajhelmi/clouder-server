using System;
namespace Clouder.Server.Service.Cadl
{
    public class CodeNode : Node
    {
        public bool HasOutput { get; set; }
        public string MethodName { get; set; }
        public string OutputName { get; set; }
        public string inputVariables { get; set; }
        public string Code { get; set; }
    }
}
