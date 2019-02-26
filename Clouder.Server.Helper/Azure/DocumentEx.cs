using System;
using Microsoft.Azure.Documents;

namespace Clouder.Server.Helper.Azure
{
    public static class DocumentEx
    {
        public static TDto To<TDto>(this Document from)
        {
            return (TDto)Activator.CreateInstance(typeof(TDto), from);
        }
    }
}
