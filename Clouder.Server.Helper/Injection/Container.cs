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

        public void RegisterSingleton<I, T>()
            where I : class
            where T : class, I
        {
            services.AddSingleton<I, T>();

        }

        public void RegisterTransient<I, T>()
            where I : class
            where T : class, I
        {
            services.AddTransient<I, T>();
        }

        public void Build()
        {
            serviceProvider = services.BuildServiceProvider();
        }

        public T Get<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}
