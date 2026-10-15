using static RoyalVillaWeb.SD;

namespace RoyalVillaWeb.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;

        public string? Url { get; set; } = string.Empty;
        public object? Data { get; set; }
        public string? Token { get; set; } = string.Empty;

    }
}
