using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Clouder.Server.Api.Extension
{
    public static class ImageContainer
    {
        public static CloudBlobContainer GetContainer(string containerName, out CloudBlobContainer container)
        {
#if DEBUG
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=kardastiitemphotosdev;AccountKey=Bjdc5Ap2jdZZ844Cttxx7B/VfJei8U2Qac+Hc4Q30prf1gaWWX9GZ25v8SsDoeA/ziatBSk+QtB5Wrqcy+PgXA==;EndpointSuffix=core.windows.net";
#else
            var connectionString = KeyVault.GetSecret("ItemImagesConnStringId");
#endif
            var connectionParts = connectionString.Split(new[] { "DefaultEndpointsProtocol=", "AccountName=", "AccountKey=", "EndpointSuffix" }, 4, StringSplitOptions.RemoveEmptyEntries);
            var accountName = connectionParts[1].Trim(new[] { ';' });
            var accountKey = connectionParts[2].Trim(new[] { ';' });
            var storageCredentials = new StorageCredentials(accountName, accountKey);
            var storageAccount = new CloudStorageAccount(storageCredentials, true);
            var blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference(containerName);
            return container;
        }

        public static async Task<string> UploadImage(this CloudBlobContainer container, string imageId, HttpRequest req)
        {
            using (var reader = new StreamReader(req.Body))
            {
                var blob = container.GetBlockBlobReference(imageId);
                blob.Properties.ContentType = "image/jpeg";
                await blob.UploadFromStreamAsync(req.Body);
                return blob.Uri.AbsoluteUri;
            }
        }

        private static bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }
}
