using System.Threading.Tasks;
using Clouder.Server.Contract.Controller;
using Clouder.Server.Helper.Http;
using Clouder.Server.Helper.Injection;
using Clouder.Server.Prop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Clouder.Server.Api
{
    public static class FactoryFunctions
    {
        static FactoryFunctions()
        {
            Startup.Spin();
        }     

        [FunctionName("Factory_Update")]
        public static async Task<IActionResult> UpdateFactory(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req,
            ILogger logger)
        {
            var factory = await req.Parse<Factory>();

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
                var factoryId = int.Parse(req.Parse("id"));
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
            ILogger logger)
        {
            try
            {
                var factory = await req.Parse<Factory>();
                factory = await Container.Instance.Get<IFactoryController>().Add(factory);
                return new OkObjectResult(factory);
            }
            catch (System.Exception e)
            {
                logger.LogCritical(e, "Could not update factory");
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}