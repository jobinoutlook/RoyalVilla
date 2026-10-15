using RoyalVilla.DTO.VillaDTOs;
using RoyalVillaWeb.Models;
using RoyalVillaWeb.Services.IServices;


namespace RoyalVillaWeb.Services
{
    public class VillaService : BaseService, IVillaService
    {
        //private readonly IHttpClientFactory _clientFactory;
        //private readonly string _villaUrl;
        private const string APIEndpoint = "/api/villa";
        public VillaService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            //_clientFactory = httpClientFactory;
           // _villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T?> CreateAsync<T>(VillaCreateDTO dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = APIEndpoint,
                Token = token
            });
        }

        public Task<T?> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{APIEndpoint}/{id}",
                Token = token
            });
        }

        public Task<T?> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = APIEndpoint,
                Token = token
                
            });
        }

        public Task<T?> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = APIEndpoint,
                Token = token
            });
        }

        

        public Task<T?> UpdateAsync<T>(VillaUpdateDTO dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{APIEndpoint}/{dto.Id}",
                Token = token
            });
        }

        
    }
}
