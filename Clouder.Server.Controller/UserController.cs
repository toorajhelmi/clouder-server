using System;
using Clouder.Server.Contract.Controller;
using Clouder.Server.Helper.Injection;
using Microsoft.Extensions.DependencyInjection;

namespace Clouder.Server.Controller
{
    public class UserController : IUserController, IModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<IUserController, UserController>();
        }
    }
}
