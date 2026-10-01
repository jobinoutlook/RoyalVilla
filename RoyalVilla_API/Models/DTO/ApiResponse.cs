using RoyalVilla_API.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoyalVilla_API.Models.DTO
{
    public class ApiResponse<TData>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public TData? Data { get; set; }
        public object? Errors { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;  

        public static ApiResponse<TData> Create(bool success, int statusCode, string? message = null, TData? data = default,
            object? errors=null)
        {
            var response = new ApiResponse<TData>
            {
                IsSuccess = success,
                StatusCode = statusCode,
                Message = message,
                Data = data,
                Errors = errors
            };

            return response;
        }


        public static ApiResponse<TData> Ok(TData data, string message)
        {
            return Create(true,200, message,data);
        }

        public static ApiResponse<TData> CreatedAt(TData data, string message)
        {
            return Create(true, 201, message, data);
        }

        public static ApiResponse<TData> NoContent(string message = "Operation completed successfully")
        {
            return Create(true, 204, message);
        }

        public static ApiResponse<TData> NotFound(string message = "Resource not found")
        {
            return Create(false, 404, message);
        }

        public static ApiResponse<TData> BadRequest(string message, object? errors = null)
        {
            return Create(false, 400, message, errors:errors);
        }

        public static ApiResponse<TData> Conflict(string message)
        {
            return Create(false, 409, message);
        }

        public static ApiResponse<TData> Error(int statusCode,string message,object errors)
        {
            return Create(false, statusCode, message, errors: errors);
        }
    }
}


//public ApiResponse(int statusCode, bool isSuccess, string? message = null, TData? data = default)
//{
//    StatusCode = statusCode;
//    IsSuccess = isSuccess;
//    Message = message;
//    Data = data;
//}