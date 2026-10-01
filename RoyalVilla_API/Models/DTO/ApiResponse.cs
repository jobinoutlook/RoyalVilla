namespace RoyalVilla_API.Models.DTO
{
    public class ApiResponse<TData>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public TData? Data { get; set; }
        public object? Errors { get; set; }
        public ApiResponse(int statusCode, bool isSuccess, string? message = null, TData? data = default)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
    }
}
