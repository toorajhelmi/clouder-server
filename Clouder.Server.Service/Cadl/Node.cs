using System.Linq;
using System.Collections.Generic;

namespace Clouder.Server.Service.Cadl
{
    public class Node
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public SortedDictionary<int, string> Connections { get; set; } = new SortedDictionary<int, string>();

        public void PopulateEdges(List<Edge> graph)
        {
            foreach (var edge in graph)
            {
                var vertice = edge.GetVertice(Id);
                if (vertice != null)
                {
                    Connections.Add(vertice.PortIndex, edge.OtherVertice(vertice).NodeId);
                }
            }
        }
    }
}
