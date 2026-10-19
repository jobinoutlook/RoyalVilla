using RoyalVilla.DTO;
using RoyalVillaWeb.Models;
using RoyalVillaWeb.Services.IServices;

namespace RoyalVillaWeb.Services
{
    public class AuthService : BaseService, IAuthService
    {
        //IHttpClientFactory _httpClientFactory;

        private const string APIEndpoint = "/api/auth";

        public AuthService(IHttpClientFactory httpClientFactory):base(httpClientFactory) 
        {
            
        }
        public Task<T?> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
        {
            var result = SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDTO,
                Url = APIEndpoint+"/login",
                
            });

            return result;
        }

        public Task<T?> RegisterAsync<T>(RegisterationRequestDTO registerationRequestDTO)
        {
            var result = SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = registerationRequestDTO,
                Url = APIEndpoint+"/register",

            });

            return result;
        }
    }
}
