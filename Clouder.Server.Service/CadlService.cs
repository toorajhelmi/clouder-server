using System;
using Clouder.Server.Contract.Service;
using Clouder.Server.Entity;
using Clouder.Server.Helper.Injection;
using Microsoft.Extensions.DependencyInjection;

namespace Clouder.Server.Service
{
    public class CadlService : ICadlService, IModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<ICadlService, CadlService>();
        }

        public string Compile(Factory factory)
        {
            throw new NotImplementedException();
        }
    }
}
