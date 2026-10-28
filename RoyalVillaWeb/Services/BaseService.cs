using Microsoft.AspNetCore.Http.Json;
using RoyalVilla.DTO;
using RoyalVillaWeb.Models;
using RoyalVillaWeb.Services.IServices;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RoyalVillaWeb.Services
{
    public class BaseService : IBaseService
    {
        public IHttpClientFactory _httpClient { get; set; }

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiResponse<object> ResponseModel { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {

            this.ResponseModel = new ApiResponse<object>();
            this._httpClient = httpClientFactory;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<T?> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                //string typeName = typeof(T).Name;

                var client = _httpClient.CreateClient("RoyalVillaAPI");
                var message = new HttpRequestMessage
                {
                    RequestUri = new Uri(apiRequest.Url ?? "", uriKind: UriKind.Relative),
                    Method = GetHttpMethod(apiRequest.ApiType)
                };

                var token = _httpContextAccessor.HttpContext?.Session?.GetString(SD.SessionToken);

                if(!string.IsNullOrEmpty(token))
                {
                    message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                if (apiRequest.Data != null)
                {
                    message.Content = JsonContent.Create(apiRequest.Data, options: JsonOptions);
                }

                var apiResponse = await client.SendAsync(message);

                var content = await apiResponse.Content.ReadFromJsonAsync<T>(options: JsonOptions);

                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return default;

            }
        }

        private static HttpMethod GetHttpMethod(SD.ApiType apiType)
        {
            HttpMethod httpMethod = apiType switch
            {
                SD.ApiType.POST => HttpMethod.Post,
                SD.ApiType.PUT => HttpMethod.Put,
                SD.ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get,
            };
            return httpMethod;
        }
    }
}
