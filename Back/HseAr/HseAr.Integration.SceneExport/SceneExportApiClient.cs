﻿using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HseAr.Core.Settings;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Integration.SceneExport.Requests;
using HseAr.Integration.SceneExport.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HseAr.Integration.SceneExport
{
    public class SceneExportApiClient : ISceneExportApiClient
    {
        private const string Url = "http://localhost:3000";
        private const string AuthTokenHeaderName = "X-Auth-Token";

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver() {NamingStrategy = new SnakeCaseNamingStrategy()},
            NullValueHandling = NullValueHandling.Include
        };

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Configuration _configuration;

        public SceneExportApiClient(IHttpClientFactory httpClientFactory, Configuration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        
        public async Task<string> ExportScene(Scene scene)
        {
            var result = await Execute(new ExportSceneRequest(scene));
            return result.Error
                ? throw new InvalidOperationException("чо-то не так")
                : $"/data/scenes/gltfs/{scene.Id}.gltf";
        }
        
        private async Task<TResponse> Execute<TResponse>(BaseRequest<TResponse> request ) where TResponse : class
        {
            var uri = $"{Url}{request.Path}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, uri);

            var stringBody = request.Content != null
                ? JsonConvert.SerializeObject(request.Content, SerializerSettings)
                : string.Empty;

            if (request.Content != null)
            {
                httpRequest.Content = new StringContent(
                    stringBody,
                    Encoding.UTF8,
                    "application/json");
            }

            var headers = new NameValueCollection();
            headers.Add(AuthTokenHeaderName,_configuration.SceneExportAccessToken);

            foreach (string headerName in headers)
            {
                httpRequest.Headers.Add(headerName, headers[headerName]);
            }

            try
            {
                /*var clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                var client = new HttpClient(clientHandler);

                using var response = await client.SendAsync(httpRequest);*/
                
                /*HttpClient httpClient = HttpClients
                    .custom()
                    .setSSLContext(new SSLContextBuilder().loadTrustMaterial(null, TrustAllStrategy.INSTANCE).build())
                    .setSSLHostnameVerifier(NoopHostnameVerifier.INSTANCE)
                    .build();*/
                
                using var response = await _httpClientFactory.CreateClient()
                    .SendAsync(httpRequest);
                
                using var content = response.Content;
                var responseString = await content.ReadAsStringAsync().ConfigureAwait(false);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse =
                        JsonConvert.DeserializeObject<ErrorResponse>(responseString, SerializerSettings);
                    ThrowException(stringBody, response.StatusCode, errorResponse);
                }

                var result = JsonConvert.DeserializeObject<TResponse>(responseString, SerializerSettings);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        private void ThrowException(string request, HttpStatusCode httpStatus, ErrorResponse responseBody)
        {
            var exceptionMessage = responseBody == null
                ? $"Error response. httpStatusCode={httpStatus}. Request: {request}."
                : $"Error response. httpStatusCode={httpStatus}. Response: {string.Join("; ", responseBody.Message)}.";

            var exception = new InvalidOperationException(exceptionMessage);
            throw exception;
        }
    }
}