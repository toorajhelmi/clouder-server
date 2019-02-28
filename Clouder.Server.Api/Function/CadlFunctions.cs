using System.Threading.Tasks;
using Clouder.Server.Contract.Controller;
using Clouder.Server.Controller;
using Clouder.Server.Dto;
using Clouder.Server.Helper.Http;
using Clouder.Server.Helper.Injection;
using Clouder.Server.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Clouder.Server.Api.Function
{
    public static class CadlFunctions
    {
        static CadlFunctions()
        {
            Startup.Spin();
        }

        [FunctionName("Cadl_Get")]
        public static async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            ILogger logger)
        {
            try
            {
                var factoryId = req.Parse("id").ToLower();
                return new OkObjectResult(await Container.Instance.Get<ICadlController>().Get(factoryId));
            }
            catch (System.Exception e)
            {
                logger.LogCritical(e, "Could not retrive factory");
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}