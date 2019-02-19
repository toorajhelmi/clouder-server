using System;
using System.Net.Http;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace Clouder.Server.Api.Extension
{
    public static class KeyVault
    {
        private static HttpClient client;

        public static string GetSecret(string secretKey)
        {
            client = new HttpClient();
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var kvClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback), client);
            var connectionStringId = Environment.GetEnvironmentVariable(secretKey);
            var secret = kvClient.GetSecretAsync(connectionStringId).Result;
            var connectionString = secret.Value;
            return connectionString;
        }
    }
}
