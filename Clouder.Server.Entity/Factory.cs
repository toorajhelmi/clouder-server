using System;
namespace Clouder.Server.Entity
{
    public class Factory : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Diagram { get; set; }
        public string NodeSettings { get; set; }
        public string Graph { get; set; }
    }
}
