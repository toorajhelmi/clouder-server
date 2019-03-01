using System;
using Clouder.Server.Contract.Controller;
using Clouder.Server.Contract.Service;
using Clouder.Server.Controller;
using Clouder.Server.Helper.Injection;
using Clouder.Server.Service.Cadl;

namespace Clouder.Server.Api
{
    public static class Startup
    {
        static Startup()
        {
            RegisterServices();
            RegisterControllers();
            Container.Instance.Build();
        }

        public static void Spin()
        {
        }

        static void RegisterServices()
        {
            Container.Instance.RegisterTransient<ICadlService, CadlService>();
        }

        static void RegisterControllers()
        {
            Container.Instance.RegisterSingleton<ICadlController, CadlController>();
            Container.Instance.RegisterSingleton<IFactoryController, FactoryController>();
            Container.Instance.RegisterSingleton<IUserController, UserController >();
        }
    }
}
