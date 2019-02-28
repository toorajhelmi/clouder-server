using System;
using System.Threading.Tasks;
using Clouder.Server.Contract.Controller;
using Clouder.Server.Contract.Service;
using Clouder.Server.Entity;

namespace Clouder.Server.Controller
{
    public class CadlController : ICadlController
    {
        private ICadlService cadlService;

        public CadlController(ICadlService cadlService)
        {
            this.cadlService = cadlService;
        }

        public Task<string> Get(string factoryId)
        {
            //TGet Factory
            var factory = new Factory();
            var cadl = cadlService.Compile(factory);
            return Task.FromResult(cadl);
        }
    }
}
