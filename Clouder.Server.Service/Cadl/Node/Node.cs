using System.Linq;
using System.Collections.Generic;

namespace Clouder.Server.Service.Cadl
{
    public class Node
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public SortedDictionary<int, Connection> Connections { get; set; } = new SortedDictionary<int, Connection>();

        public void PopulateEdges(List<Edge> graph, List<Node> nodes)
        {
            foreach (var edge in graph)
            {
                var vertice = edge.GetVertice(Id);
                if (vertice != null)
                {
                    var nodeId = edge.OtherVertice(vertice).NodeId;
                    Connections.Add(vertice.PortIndex, new Connection
                    {
                        OtherNode = nodes.First(n => n.Id == nodeId),
                        IsInward = edge.TargetVertice.NodeId == Id,
                        Via = nodes.First(n => n.Id == edge.Connection)
                    });
                }
            }
        }

        public bool HasOutputQueue()
        {
            return Connections.Any(c => c.Value.OtherNode is QueueNode &&
                !c.Value.IsInward);
        }

        public virtual List<string> Validate()
        {
            return new List<string>();
        }
    }
}
