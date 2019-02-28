using System;
namespace Clouder.Server.Service.Cadl
{
    public class Edge
    {
        public string Source { get; set; }
        public string Target { get; set; }

        public Vertice SourceVertice => new Vertice
        {
            NodeId = Source.Split(new[] { '-' })[0],
            PortIndex = int.Parse(Source.Split(new[] { '-' })[1])
        };

        public Vertice TargetVertice => new Vertice
        {
            NodeId = Target.Split(new[] { '-' })[0],
            PortIndex = int.Parse(Target.Split(new[] { '-' })[1])
        };

        public Vertice GetVertice(string nodeId)
        {
            if (SourceVertice.NodeId == nodeId)
            {
                return SourceVertice;
            }
            else if (TargetVertice.NodeId == nodeId)
            {
                return TargetVertice;
            }
            else
            {
                return null;
            }
        }

        public Vertice OtherVertice(Vertice vertice)
        {
            if (SourceVertice.Equals(vertice))
            {
                return TargetVertice;
            }
            else if (TargetVertice.Equals(vertice))
            {
                return SourceVertice;
            }
            else
            {
                return null;
            }
        }
    }
}
