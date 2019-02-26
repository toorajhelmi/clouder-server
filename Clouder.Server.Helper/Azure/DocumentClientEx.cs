using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Clouder.Server.Helper.Azure
{
    public static class DocumentClientEx
	{
        private const string DbId = "Clouder";

        public static DocumentClient GetDocumentClient(string connectionString)
        {
            var connectionParts = connectionString.Split(new[] { "AccountEndpoint=", "AccountKey=" }, 2, StringSplitOptions.RemoveEmptyEntries);
            var endpoint = connectionParts[0].Trim(new[] { ';' });
            var key = connectionParts[1].Trim(new[] { ';' });
            return new DocumentClient(new Uri(endpoint), key, new ConnectionPolicy { ConnectionMode = ConnectionMode.Direct });
        }

		public static async Task CreateCollectionAsync(this DocumentClient client, string colId, IndexingPolicy policy)
		{
			try
			{
				await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DbId));
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					await client.CreateDatabaseAsync(new Database { Id = DbId });
				}
				else
				{
					throw;
				}
			}

			var collection = new DocumentCollection { Id = colId };
			collection.IndexingPolicy = policy;
			await client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(DbId), collection);
		}

		public static async Task<T> GetAsync<T>(this DocumentClient client, string colId, string id)
			where T : Document
		{
            var resource = (await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DbId, colId, id))).Resource;
            return (dynamic)resource;
		}  

		public static async Task<List<T>> GetAsync<T>(this DocumentClient client, string colId, FeedOptions feedOptions, 
		                                              Func<IOrderedQueryable<T>, IQueryable<T>> predicate)
            where T : Document
        {
			var query = predicate(client.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(DbId, colId), feedOptions));          
			var docQuery = query.AsDocumentQuery();
			var results = new List<T>();
			while (docQuery.HasMoreResults)
            {
				results.AddRange(await docQuery.ExecuteNextAsync<T>());
            }

			return results as List<T>;
        }
      
		public static async Task<Document> AddAsync<T>(this DocumentClient client, string colId, T entity)
			where T : Document
		{        
			return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DbId, colId), entity);
		}

        public static async Task<Document> DeleteAync<T>(this DocumentClient client, string colId, T entity)
            where T : Document
        {
            return await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DbId, colId, entity.Id));
        }

		public static async Task<Document> UpdateAsync<T>(this DocumentClient client, string colId, T entity)
            where T : Document
        {
            return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DbId, colId, entity.Id), entity);
        }      
	}
}
