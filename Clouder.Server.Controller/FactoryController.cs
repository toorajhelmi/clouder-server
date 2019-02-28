using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clouder.Server.Contract.Controller;
using Clouder.Server.Dto;
using Clouder.Server.Helper.Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Clouder.Server.Controller
{
    public class FactoryController : IFactoryController
    {
        private const int MaxItemCount = 100;
        private const string colId = "Factory";
        private static FeedOptions feedOptions = new FeedOptions { MaxItemCount = MaxItemCount };

        public Task<IActionResult> Create()
        {
            return HttpF.Create(colId, new IndexingPolicy(new HashIndex(DataType.Number))
            {
                IndexingMode = IndexingMode.Consistent,
            });
        }

        public void Update(FactoryDto factory)
        {
            //return await HttpF.Update<Entity.Factory>(req, colId);
        }
    
        public Task<FactoryDto> Get(string id)
        {
            var factory = new[]
                {
                    new FactoryDto { Id = "1".ToString(), Name = "Factory 1",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."},
                    new FactoryDto { Id = "2", Name = "Factory 2", Description = "E-Commerce"},
                    new FactoryDto { Id = "3", Name = "Factory 3", Description = "Ticketing"},
                    new FactoryDto { Id = "4", Name = "Factory 4", Description = "Trading"},
                    new FactoryDto { Id = "5", Name = "Factory 5", Description = "Media"},
                    new FactoryDto { Id = "6", Name = "Factory 6", Description = "Multi-tennant"},
                }.First(f => f.Id == id);
            return Task.FromResult(factory);
        }
    }
}
