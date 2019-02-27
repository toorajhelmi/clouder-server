using System;
using Microsoft.Extensions.DependencyInjection;

namespace Clouder.Server.Helper.Injection
{
    public class Container
    {
        private readonly IServiceCollection services;
        private IServiceProvider serviceProvider;
        private static Container instance;

        public static Container Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Container();
                }

                return instance;
            }
        }

        private Container()
        {
            services = new ServiceCollection();
        }

        public void Register(IModule module)
        {
            module.Load(this.services);
            serviceProvider = services.BuildServiceProvider();
        }

        public T Get<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}
