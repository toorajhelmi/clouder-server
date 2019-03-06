using System.Threading.Tasks;
using Clouder.Server.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Clouder.Server.Contract.Controller
{
    public interface IFactoryController
    {
        Task<IActionResult> Create();
        Task<FactoryDto> Add(FactoryDto factory);
        void Update(FactoryDto factory);
        Task<FactoryDto> Get(string id);

    }
}
