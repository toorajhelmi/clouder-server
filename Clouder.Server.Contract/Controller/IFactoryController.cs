using System.Threading.Tasks;
using Clouder.Server.Prop;

namespace Clouder.Server.Contract.Controller
{
    public interface IFactoryController
    {
        Task<Factory> Add(Factory factory);
        void Update(Factory factory);
        Task<Factory> Get(int id);

    }
}
