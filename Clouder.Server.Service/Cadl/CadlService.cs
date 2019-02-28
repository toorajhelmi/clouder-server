using System.Linq;
using System.Collections.Generic;
using Clouder.Server.Contract.Service;
using Clouder.Server.Entity;
using Clouder.Server.Helper.Injection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Clouder.Server.Service.Cadl
{
    public class CadlService : ICadlService, IModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<ICadlService, CadlService>();
        }

        public string Compile(Factory factory)
        {
            factory = GetFactory();
            var nodes = JsonConvert.DeserializeObject<List<Node>>(factory.NodeSettings);
            var graph = JsonConvert.DeserializeObject<List<Edge>>(factory.Graph);

            var functions = nodes.Where(n =>
                n.Type == "HTTP Request" ||
                n.Type == "Timer Trigger" ||
                n.Type == "Queue Trigger");

            var output = "";
            //Look in the graph for each 
            foreach (var function in functions)
            {
                function.PopulateEdges(graph);
                output = $"{function.Id}: [{string.Join('\n', function.Connections.Select(kv => $"({kv.Key}, {kv.Value})"))}]";
            }

            return output;
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
