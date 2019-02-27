using System.Threading.Tasks;
using Clouder.Server.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Clouder.Server.Contract.Controller
{
    public interface IFactoryController
    {
        Task<IActionResult> Create();
        void Update(FactoryDto factory);
        Task<FactoryDto> Get(string id);
    }
}
