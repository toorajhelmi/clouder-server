using Microsoft.Extensions.DependencyInjection;

namespace Clouder.Server.Helper.Injection
{
    public interface IModule
    {
        void Load(IServiceCollection services);
    }
}