using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clouder.Server.Contract.Controller;
using Clouder.Server.Controller;
using Clouder.Server.Dto;
using Clouder.Server.Helper.Azure;
using Clouder.Server.Helper.Http;
using Clouder.Server.Helper.Injection;
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
        static FactoryFunctions()
        {
            Container.Instance.Register(new FactoryController());
        }     

        [FunctionName("Factory_Create")]
        public static async Task<IActionResult> CreateFactory(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            ILogger logger)
        {
            return await Container.Instance.Get<IFactoryController>().Create();
        }

        [FunctionName("Factory_Update")]
        public static async Task<IActionResult> UpdateFactory(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req,
            ILogger logger)
        {
            var factory = await req.Parse<FactoryDto>();

            try
            {
                Container.Instance.Get<IFactoryController>().Update(factory);
                return new OkResult();
            }
            catch (System.Exception e)
            {
                logger.LogCritical(e, "Could not update factory");
                return new BadRequestObjectResult(e.Message);
            }
        }

        [FunctionName("Factory_Get")]
        public static async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            ILogger logger)
        {
            try
            {
                var factoryId = req.Parse("id").ToLower();
                return new OkObjectResult(await Container.Instance.Get<IFactoryController>().Get(factoryId));
            }
            catch (System.Exception e)
            {
                logger.LogCritical(e, "Could not retrive factory");
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