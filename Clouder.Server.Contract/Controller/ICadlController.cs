using System;
using System.Threading.Tasks;

namespace Clouder.Server.Contract.Controller
{
    public interface ICadlController
    {
        Task<string> Get(int factoryId);
    }
}
