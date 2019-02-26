using System;
using Clouder.Server.Entity;

namespace Clouder.Server.Contract.Service
{
    public interface ICadlService
    {
        string Compile(Factory factory);
    }
}
