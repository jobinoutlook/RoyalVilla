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
        public VillaService(IHttpClientFactory httpClientFactory, IConfiguration configuration,IHttpContextAccessor httpContextAccessor) 
            : base(httpClientFactory, httpContextAccessor)
        {
            //_clientFactory = httpClientFactory;
           // _villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T?> CreateAsync<T>(VillaCreateDTO dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = APIEndpoint,
                
            });
        }

        public Task<T?> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{APIEndpoint}/{id}",
                
            });
        }

        public Task<T?> GetAllAsync<T>()
        {
            return SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = APIEndpoint,
                
                
            });
        }

        public Task<T?> GetAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{APIEndpoint}/{id}",
                
            });
        }

        

        public Task<T?> UpdateAsync<T>(VillaUpdateDTO dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{APIEndpoint}/{dto.Id}",
                
            });
        }

        
    }
}
