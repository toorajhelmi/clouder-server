using System;
namespace Clouder.Server.Service.Cadl
{
    public class SqlStatementNode : Node
    {
        public string DbScript { get; set; }
        public string StatementType { get; set; }
        public string ReturnType { get; set; }
        public string OutputName { get; set; }
    }
}
