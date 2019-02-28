using System;
namespace Clouder.Server.Service.Cadl
{
    public class Vertice
    {
        public string NodeId { get; set; }
        public int PortIndex { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Vertice;
            return NodeId == other.NodeId && PortIndex == other.PortIndex;
        }

        public override int GetHashCode()
        {
            return $"{NodeId}-{PortIndex}".GetHashCode();
        }
    }
}
