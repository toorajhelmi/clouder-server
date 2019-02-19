using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System;

namespace Clouder.Server.Entity
{
    public class EntityBase : Document
    {
        public EntityBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void OverwriteId(string id)
        {
            Id = id;
        }
    }

    public static class EntityExtensions
    {
        public static TDto To<TDto>(this EntityBase from)
        {
            return (TDto)Activator.CreateInstance(typeof(TDto), from);
        }
    }
}
