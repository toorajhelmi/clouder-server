using System;
using System.Threading.Tasks;
using Clouder.Server.Contract.Controller;
using Clouder.Server.Contract.Service;
using Clouder.Server.Helper.Injection;
using Clouder.Server.Prop;

namespace Clouder.Server.Controller
{
    public class CadlController : ICadlController
    {
        public CadlController(ICadlService cadlService)
        {
        }

        public Task<string> Get(int factoryId)
        {
            //TGet Factory
            var factory = new Factory();
            var cadlService = Container.Instance.Get<ICadlService>();
            var cadl = cadlService.Compile(factory);
            return Task.FromResult(cadl);
        }
    }
}
