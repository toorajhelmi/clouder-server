using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Clouder.Server.Api.Util;
using Clouder.Server.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Clouder.Server.Dto;

namespace Clouder.Server.Api.Extension
{
    public static class NakedF
	{
        public static DocumentClient documentClient;

        public static DocumentClient DocumentClient => GetDocumentClient();

		public static async Task Create(HttpRequest req, string colId, IndexingPolicy indexingPolicy)
		{
            await DocumentClient.CreateCollectionAsync(colId, indexingPolicy);
		}

		public static async Task<T> Add<T>(T entity, string colId)
			where T : EntityBase
		{
			DataAnotationsValidator.TryValidate(entity);
            await DocumentClient.AddAsync(colId, entity);
            return entity;
		}

        public static async Task Delete<T>(T entity, string colId)
            where T : EntityBase
        {
            await DocumentClient.DeleteAync(colId, entity);
        }

        public static async Task<T> Get<T>(string id, string colId)
            where T : EntityBase
        {
            var entity = await DocumentClient.GetAsync<T>(colId, id);
            return entity;
        }

        public static async Task<T> Get<T>(HttpRequest req, string colId)
            where T : EntityBase
        {
            var id = req.Parse("id");
            var entity = await DocumentClient.GetAsync<T>(colId, id);
            return entity;
        }

        public static async Task<TDto> Get<T, TDto>(HttpRequest req, string colId)
            where T : EntityBase, new()
		{
			var id = req.Parse("id");
            var entity = await DocumentClient.GetAsync<T>(colId, id);
            return entity.To<TDto>();
		}

        public static async Task<List<T>> Get<T>(string colId, FeedOptions feedOptions,
                                                 Func<IOrderedQueryable<T>, IQueryable<T>> predicate)
            where T : EntityBase
        {
            return await DocumentClient.GetAsync<T>(colId, feedOptions, predicate);
        }

        public static async Task<List<TDto>> Get<T, TDto>(string colId, FeedOptions feedOptions,
                                                          Func<IOrderedQueryable<T>, IQueryable<T>> predicate)
			where T : EntityBase
		{
            var entities = await DocumentClient.GetAsync<T>(colId, feedOptions, predicate);
            return entities.Select(e => e.To<TDto>()).ToList();
		}

        public static async Task<T> Get<T>(string colId, Func<IOrderedQueryable<T>, IQueryable<T>> predicate)
           where T : EntityBase
        {
            var results = await DocumentClient.GetAsync<T>(colId, new FeedOptions { MaxItemCount = 1 }, predicate);
            if (results.Any())
            {
                return results.First();
            }
            else
            {
                return default(T);
            }
        }

        public static async Task<TDto> Get<T, TDto>(string colId, Func<IOrderedQueryable<T>, IQueryable<T>> predicate)
            where T : EntityBase
        {
            var results = await DocumentClient.GetAsync<T>(colId, new FeedOptions { MaxItemCount = 1}, predicate);
            if (results.Any())
            {
                return results.First().To<TDto>();
            }
            else
            {
                return default(TDto);
            }
        }

		public static async Task Update<T>(HttpRequest req, string colId)
			where T : EntityBase
		{
			string body = string.Empty;
			using (StreamReader reader = new StreamReader(req.Body))
			{
				body = await reader.ReadToEndAsync();
			}

            var entity = JsonConvert.DeserializeObject<T>(body);
			DataAnotationsValidator.TryValidate(entity);
            await DocumentClient.UpdateAsync(colId, entity);
		}

        public static async Task Update<T>(T entity, string colId)
            where T : EntityBase
        {
            DataAnotationsValidator.TryValidate(entity);
            await DocumentClient.UpdateAsync(colId, entity);
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
