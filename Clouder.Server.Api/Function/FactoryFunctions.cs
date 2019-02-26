using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clouder.Server.Dto;
using Clouder.Server.Helper.Azure;
using Clouder.Server.Helper.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Clouder.Server.Api.Function
{
    public static class FactoryFunctions
    {
        private const int MaxItemCount = 100;
        private const string colId = "Factory";
        private static FeedOptions feedOptions = new FeedOptions { MaxItemCount = MaxItemCount };

        static FactoryFunctions()
        {
        }     

        [FunctionName("Factory_Create")]
        public static async Task<IActionResult> CreateFactory(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            ILogger log)
        {
            return await HttpF.Create(req, colId, new IndexingPolicy(new HashIndex(DataType.Number))
            {
                IndexingMode = IndexingMode.Consistent,
            });
        }

        [FunctionName("Factory_Update")]
        public static async Task<IActionResult> UpdateFactory(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req,
            ILogger log)
        {
            var factory = await req.Parse<FactoryDto>();
            return new OkResult();

            //return await HttpF.Update<Entity.Factory>(req, colId);
        }

        [FunctionName("Factory_Get")]
        public static async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            ILogger log)
        {
            try
            {
                var factoryId = req.Parse("id").ToLower();
                return new OkObjectResult(new List<FactoryDto>
                {
                    new FactoryDto { Id = "1".ToString(), Name = "Factory 1",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."},
                    new FactoryDto { Id = "2", Name = "Factory 2", Description = "E-Commerce"},
                    new FactoryDto { Id = "3", Name = "Factory 3", Description = "Ticketing"},
                    new FactoryDto { Id = "4", Name = "Factory 4", Description = "Trading"},
                    new FactoryDto { Id = "5", Name = "Factory 5", Description = "Media"},
                    new FactoryDto { Id = "6", Name = "Factory 6", Description = "Multi-tennant"},
                }.First(f => f.Id == factoryId));

            }
            catch (System.Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [FunctionName("Factory_Add")]
        public static async Task<IActionResult> Add(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req,
            ILogger log)
        {
            return new OkObjectResult(null);
        }
    }
}