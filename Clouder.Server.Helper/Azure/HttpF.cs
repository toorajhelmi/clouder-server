using System;
using System.Linq;
using System.Threading.Tasks;
using Clouder.Server.Helper.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Clouder.Server.Helper.Azure
{
    public static class HttpF
    {
        private const string DbId = "Clouder";
        public static DocumentClient documentClient;

        public static DocumentClient DocumentClient => GetDocumentClient();

        [FunctionName("Test_DbConnection")]
        public static IActionResult TestDbConnection(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            ILogger log)
        {
            try
            {
                return new OkObjectResult(KeyVault.GetSecret("DocumentDbConnStringId"));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [FunctionName("Test_Basic")]
        public static IActionResult TestBasic(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            ILogger log)
        {
            try
            {
                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

		public static async Task<IActionResult> Create(string colId, IndexingPolicy indexingPolicy)
		{
			try
			{
                await DocumentClient.CreateCollectionAsync(colId, indexingPolicy);
				return new OkObjectResult("Collection Created");
            }
            catch (Exception e)
            {
				return new BadRequestObjectResult(e.Message);
            }       
		}

		public static async Task<IActionResult> Add<T, TDto>(HttpRequest req, string colId)
			where T : Document
        {        
            try
            {
                var entity = await req.Parse<T>();         
                await DocumentClient.AddAsync(colId, entity);
                return new OkObjectResult(entity.To<TDto>());
            }
            catch (Exception e)
            {
				return new BadRequestObjectResult(e.Message);
            }         
        }    

		public static async Task<IActionResult> Get<T, TDto>(HttpRequest req, string colId)
            where T : Document
		{
            string id = "";

			try
			{
				id = req.Parse("id");
                var entity = await DocumentClient.GetAsync<T>(colId, id);
                return new OkObjectResult(entity.To<TDto>());
			}
			catch (DocumentClientException dce)
			{
				if (dce.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					return new NotFoundResult();
				}
				else
				{
					string errorMessage = $"An error occurred finding document id {id}. {dce.Message}";
					throw new Exception(errorMessage);
				}
			}
			catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
		}
          
		public static async Task<IActionResult> Get<T, TDto>(string colId, FeedOptions feedOptions, 
		                                               Func<IOrderedQueryable<T>, IQueryable<T>> predicate)
			where T : Document
        {
            try
            {
                var results = await DocumentClient.GetAsync<T>(colId, feedOptions, predicate);
                if (!results.Any())
                {
                    return null;
                }
                else if (feedOptions.MaxItemCount == 1)
                {
                    return new OkObjectResult(results.First().To<TDto>());
                }
                else
                {
                    return new OkObjectResult(results.Select(r => r.To<TDto>()));
                }
            }
			catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }        
        }

        public static async Task<IActionResult> Get<T, TDto>(string colId,
                                                       Func<IOrderedQueryable<T>, IQueryable<T>> predicate)
            where T : Document
        {
            try
            {
                var results = await DocumentClient.GetAsync<T>(colId, new FeedOptions { MaxItemCount = 1}, predicate);
                if (results.Any())
                {
                    return new OkObjectResult(results.FirstOrDefault().To<TDto>());
                }
                else
                {
                    return new OkObjectResult(null);
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
           
		public static async Task<IActionResult> Update<T>(HttpRequest req, string colId)
			where T : Document
        {         
            try
            {   
                var entity = await req.Parse<T>();        
                await DocumentClient.UpdateAsync(colId, entity);          
				return new OkResult();       
            }         
            catch (DocumentClientException dce)
            {
                if (dce.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new NotFoundResult();
                }
                else
                {
                    return new BadRequestResult();
                }
            }
			catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        private static DocumentClient GetDocumentClient()
        {
            if (documentClient == null)
            {
#if DEBUG
                var connectionString = "AccountEndpoint=https://kardastidev.documents.azure.com:443/;AccountKey=YbIfGnsaDDdLRIj7uaE7ClZq1jXka37Xv4bxcJXzeM8Mp8k0fVh3l4VkHd8bCVs3LifJxRIRru9HPYqSv5mdsg==;";
#else
                var connectionString = KeyVault.GetSecret("DocumentDbConnStringId");
#endif
                documentClient = DocumentClientEx.GetDocumentClient(connectionString);
            }
            return documentClient;
        }
    }
}
