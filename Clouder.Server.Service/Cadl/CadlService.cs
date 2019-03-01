using System.Linq;
using System.Collections.Generic;
using Clouder.Server.Contract.Service;
using Clouder.Server.Entity;
using Clouder.Server.Helper.Injection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Text;
using Clouder.Server.Constant;

namespace Clouder.Server.Service.Cadl
{
    public class CadlService : ICadlService, IModule
    {
        private StringBuilder cadl = new StringBuilder();

        public void Load(IServiceCollection services)
        {
            services.AddSingleton<ICadlService, CadlService>();
        }

        public string Compile(Factory factory)
        {
            factory = GetFactory();

            var nodes = GetNodes(factory.NodeSettings);
            var graph = JsonConvert.DeserializeObject<List<Edge>>(factory.Graph);

            foreach (var function in nodes.OfType<FunctionNode>())
            {
                function.PopulateEdges(graph, nodes);
            }

            foreach (var database in nodes.OfType<SqlNode>())
            {
                GenerateSqlCadl(database);
            }

            foreach (var queue in nodes.OfType<QueueNode>())
            {
                GenerateQueueCadl(queue);
            }

            foreach (var code in nodes.OfType<CodeNode>())
            {
                GenerateCodeCadl(code);
            }

            foreach (var function in nodes.OfType<FunctionNode>())
            {
                //output = $"{function.Id}: [{string.Join('\n', function.Connections.Select(kv => $"({kv.Key}, {kv.Value})"))}]";
                GenerateFunctionCadl(function);
            }

            return cadl.ToString();
        }

        private List<Node> GetNodes(string nodeSettings)
        {
            var baseNodes = JsonConvert.DeserializeObject<List<Node>>(nodeSettings);
            var nodeObjects = JsonConvert.DeserializeObject<List<object>>(nodeSettings);

            var nodes = new List<Node>();
            for (int i=0; i< baseNodes.Count; i++)
            {
                switch (baseNodes[i].Type)
                {
                    case "HTTP Request":
                    case "Timer Trigger":
                    case "Queue Trigger":
                        nodes.Add(JsonConvert.DeserializeObject<FunctionNode>(
                            JsonConvert.SerializeObject(nodeObjects[i])));
                        break;
                    case "Queue":
                        nodes.Add(JsonConvert.DeserializeObject<QueueNode>(
                            JsonConvert.SerializeObject(nodeObjects[i])));
                        break;
                    case "SQL":
                        nodes.Add(JsonConvert.DeserializeObject<SqlNode>(
                            JsonConvert.SerializeObject(nodeObjects[i])));
                        break;
                    case "Code":
                        nodes.Add(JsonConvert.DeserializeObject<CodeNode>(
                            JsonConvert.SerializeObject(nodeObjects[i])));
                        break;
                    case "Sql Statement":
                        nodes.Add(JsonConvert.DeserializeObject<SqlStatementNode>(
                            JsonConvert.SerializeObject(nodeObjects[i])));
                        break;
                }
            }

            return nodes;
        }

        private void GenerateQueueCadl(QueueNode queue)
        {
            var template = GetTemplate(queue);
            cadl.Append(template
                .Replace("#database", queue.QueueName)
                .Replace("#comp-name", queue.ComponentName)
                .Replace("#size", queue.Size)
                .Replace("#cadl-help-sources", ClouderConstants.HelpUrl.Replace("#section", "datasource")));
            cadl.AppendLine();
            cadl.AppendLine();
        }

        private void GenerateSqlCadl(SqlNode database)
        {
            var template = GetTemplate(database);
            cadl.Append(template
                .Replace("#database", database.DatabaseName)
                .Replace("#comp-name", database.ComponentName)
                .Replace("#size", database.Size)
                .Replace("#body", database.DbScript)
                .Replace("#cadl-help-sources", ClouderConstants.HelpUrl.Replace("#section", "datasource")));
            cadl.AppendLine();
            cadl.AppendLine();
        }

        private void GenerateCodeCadl(CodeNode code)
        {
            var template = GetTemplate(code);
            cadl.Append(template
                .Replace("#method", code.MethodName)
                .Replace("#inputs", code.inputVariables)
                .Replace("#output", code.OutputName)
                .Replace("#body", code.Code)
                .Replace("#cadl-help-syntax", ClouderConstants.HelpUrl.Replace("#section", "syntax")));
            cadl.AppendLine();
            cadl.AppendLine();
        }

        private void GenerateFunctionCadl(FunctionNode function)
        {
            var functionTemplate = GetTemplate(function);
            cadl.Append(functionTemplate
                .Replace("#func-name", function.FunctionName)
                .Replace("#comp-name", function.ComponentName)
                .Replace("#size", function.Size)
                .Replace("#inputs", function.InputName)
                .Replace("#period", function.TimerPeriod)
                .Replace("#queue", function.Queue)
                .Replace("#cadl-help-func", ClouderConstants.HelpUrl.Replace("#section", "function")));

            foreach (var connection in function.Connections.Values)
            {
                switch (connection.OtherNode.Type)
                {
                    case "Queue":
                        //Inward is just a triggering queue function and will
                        //be handelled via bindings
                        if (!connection.IsInward)
                        {
                            var targetQueue = connection.OtherNode as QueueNode;
                            cadl.Append(CodeTemplates.Enqueue
                                .Replace("#queue", targetQueue.QueueName)
                                .Replace("#variable", function.InputName));
                        }
                        break;
                    case "Sql":
                        var database = connection.OtherNode as SqlNode;
                        var statement = connection.Via as SqlStatementNode;
                        var sqlTemplate = GetTemplate(database);
                        cadl.Append(sqlTemplate
                             .Replace("#output", statement.OutputName)
                             .Replace("#database", database.DatabaseName)
                             .Replace("#entity", statement.ReturnType)
                             .Replace("#sql", statement.DbScript));
                        break;
                    case "If":
                        cadl.Append(CodeTemplates.If
                            .Replace("#condition", (connection.OtherNode as IfNode).Condition));
                        break;
                    case "Else": 
                        cadl.Append(CodeTemplates.Else);
                        break;
                    case "End":
                        cadl.Append(CodeTemplates.End);
                        break;
                    case "Iterate":
                        cadl.Append(CodeTemplates.Iterate
                            .Replace("#list", (connection.OtherNode as IterateNode).List)
                            .Replace("#variable", (connection.OtherNode as IterateNode).Variable));
                        break;
                    case "Return":
                        cadl.Append(CodeTemplates.Return
                            .Replace("#expression", (connection.OtherNode as ReturnNode).Expression));
                        break;
                    case "Variable":
                        cadl.Append(CodeTemplates.Variable
                            .Replace("#variableName", (connection.OtherNode as VariableNode).VariableName)
                            .Replace("#definition", (connection.OtherNode as VariableNode).Definition));
                        break;
                }
            }

            cadl.AppendLine();
            cadl.AppendLine();
        }

        private string GetTemplate(Node node)
        {
            switch (node.Type)
            {
                case "HTTP Request": 
                    return node.HasOutputQueue() ? 
                        FunctionTemplates.ReqInQueueOutFunction :
                        FunctionTemplates.ReqInFunction;
                case "Timer Trigger":
                    return node.HasOutputQueue() ?
                        FunctionTemplates.TimerInQueueOutFunction :
                        FunctionTemplates.TimerInFunction;
                case "Queue Trigger":
                    return node.HasOutputQueue() ?
                        FunctionTemplates.QueueInQueueOutFunction :
                        FunctionTemplates.QueueInFunction;
                case "Queue":
                    return DataSourceTemplates.Queue;
                case "SQL":
                    return DataSourceTemplates.SqlDatabase;
                case "Code":
                    return (node as CodeNode).HasOutput ?
                        CodeTemplates.CodeWithOutput :
                        CodeTemplates.CodeWithoutOutput;
                case "Sql Statement":
                    var statement = node as SqlStatementNode;
                    switch (statement.StatementType)
                    {
                        case "Select": 
                            if (string.IsNullOrWhiteSpace(statement.ReturnType))
                            {
                                return SqlStatementTemplates.SelectScalar;
                            }
                            else
                            {
                                return SqlStatementTemplates.SelectEntity;
                            }
                        case "Insert":
                            if (string.IsNullOrWhiteSpace(statement.OutputName))
                            {
                                return SqlStatementTemplates.UpsertOrDeleteNoOutout;
                            }
                            else
                            {
                                return SqlStatementTemplates.InsertWithOutout;
                            }
                        default: return SqlStatementTemplates.UpsertOrDeleteNoOutout;
                    }
                default: return "";
            }
        }

        private Factory GetFactory()
        {
            return new Factory
            {
                NodeSettings = "[{\"id\":\"SQLFT0cx\",\"componentName\":\"SQLFT0cx\",\"type\":\"SQL\"},{\"id\":\"QueueBaFn2\",\"componentName\":\"QueueBaFn2\",\"type\":\"Queue\"},{\"id\":\"HTTP RequestrIqlm\",\"componentName\":\"HTTPRequestrIqlm\",\"type\":\"HTTP Request\"},{\"id\":\"Straightbn6ov\",\"componentName\":\"Straightbn6ov\",\"type\":\"Straight\"},{\"id\":\"StraightHIiQN\",\"componentName\":\"StraightHIiQN\",\"type\":\"Straight\"}]",
                Graph = "[{source: \"HTTP RequestrIqlm-3\", target: \"SQLFT0cx-0\"}, {source: \"HTTP RequestrIqlm-5\", target: \"QueueBaFn2-1\"}]"
            };
        }
    }
}
