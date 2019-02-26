using System;
using Microsoft.Extensions.DependencyInjection;

namespace Clouder.Server.Helper.Injection
{
    public class Container
    {
        private readonly IServiceCollection services;

        public Container()
        {
            services = new ServiceCollection();
        }

        public Container RegisterModule(IModule module = null)
        {
            module.Load(this.services);

            return this;
        }

        public IServiceProvider Build()
        {
            var provider = this.services.BuildServiceProvider();
            return provider;
        }
    }
}
